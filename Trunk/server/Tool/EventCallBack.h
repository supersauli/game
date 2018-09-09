#include <functional>
#include <map>
#include <unordered_map>
template<class ...M>
class EventCallBack {
	typedef std::function<void(M...arg)> EventFunc;
public:
	void AddEventCallBackFunc(int eventID, EventFunc func, int priority = 0) {
		EventSort event;
		event._func = func;
		event._priority = priority;
		_eventGroup[eventID].push_back(event);

	};

	template<class ...T>
	void SendEvent(int eventID, T ...arg) {
		auto it = _eventGroup.find(eventID);
		if (it == _eventGroup.end()) {
			return;
		}

		for (auto its : it->second) {
			if (its._func != nullptr) {
				its._func(arg...);
			}
		}

	};

private:
	struct EventSort {
		EventFunc _func;
		int _priority;
		bool operator < (const EventSort&other) {
			return _priority < other._priority;
		}
	};

	std::unordered_map<int, std::list<EventSort> > _eventGroup;
};



