#ifndef _STIME_H__
#define _STIME_H__
#include <ctime>
#include <list>

namespace Tool {
	/**
	* @brief ���Ⱥ���
	*/
	class ClockTime final {
	public:
		ClockTime() {
			clock_gettime(CLOCK_REALTIME, &_clockTime);
		}

		/**
		* @brief ˢ��ʱ��
		*/
		void RefreshTimer() {
			clock_gettime(CLOCK_REALTIME, &_clockTime);
		}

		/**
		* @brief �������
		*
		* @return
		*/
		DWORD GetSec()const {
			return _clockTime.tv_sec;
		}

		/**
		* @brief ��ú�����
		*
		* @return
		*/
		QWORD GetMsec()const {
			return _clockTime.tv_sec * 1000LL + _clockTime.tv_nsec / 1000000LL;
		}

	private:
		timespec		_clockTime;


	};
	/**
	* @brief ʱ�亯��������
	*/
	class Time final
	{
	public:
		/**
		* @brief ������
		*
		* @return
		*/
		Time() {
			time(&_tTime);
			gmtime_r(&_tTime, &_tmTime);
		}

		/**
		* @brief dwTime ����
		*
		* @param
		*/
		Time(time_t dwTime) :_tTime(dwTime) {
			gmtime_r(&_tTime, &_tmTime);
		}

		/**
		* @brief ���utcʱ��
		*
		* @return
		*/
		DWORD GetUtcSec() {
			return _tTime;
		};

		/**
		* @brief ˢ��ʱ��
		*/
		void Refresh() {
			time(&_tTime);
			gmtime_r(&_tTime, &_tmTime);
		}


		/**
		* @brief ����
		*
		* @return
		*/
		DWORD GetYear()const
		{
			return _tmTime.tm_year + 1900;
		}

		/**
		* @brief �ĸ��� [1,12]
		*
		* @return
		*/
		DWORD GetMonth()const
		{
			return _tmTime.tm_mon + 1;
		}

		/**
		* @brief һ�����е�����[1,31]
		*
		* @return
		*/
		DWORD GetMDay()const
		{
			return _tmTime.tm_mday;
		}

		/**
		* @brief �ܼ�(1-7)
		*
		* @return
		*/
		DWORD GetWDay()const
		{
			if (_tmTime.tm_wday == 0) {
				return 7;
			}
			return _tmTime.tm_wday;
		}

		/**
		* @brief ��ÿ���1��1�տ�ʼ������ [0,365]������0����1��1��
		*
		* @return
		*/
		DWORD GetYDay()const
		{
			return _tmTime.tm_yday;

		}

		/**
		* @brief Сʱ[0,23]��
		*
		* @return
		*/
		DWORD GetHour()const
		{
			return _tmTime.tm_hour;
		}
		/**
		* @brief ������[0,59]
		*
		* @return
		*/
		DWORD GetMinute()const
		{
			return _tmTime.tm_min;
		}

		/**
		* @brief ����
		*
		* @return [0,59]
		*/
		DWORD GetSec()const
		{
			return _tmTime.tm_sec;
		}


		/**
		* @brief ���������
		*
		* @return
		*/
		DWORD GetElapseSec()
		{
			return ::time(NULL) - _tTime;
		}

		/**
		* @brief ��������
		*
		* @param time
		*/
		void SetSec(const time_t&sec)
		{
			_tTime = sec;
			gmtime_r(&_tTime, &_tmTime);
		}

		/**
		* @brief �����ӳ���
		*
		* @param sec
		*/
		void AddDelaySec(const time_t&sec)
		{
			_tTime += sec;
			gmtime_r(&_tTime, &_tmTime);

		};



		/**
		* @brief ͬһ��
		*
		* @param time
		*
		* @return
		*/
		bool IsSameSec()const
		{

			if (GetSec() == Time().GetSec())
			{
				return true;
			}
			return false;
		}

		bool IsSameSec(const time_t &time)const
		{

			if (GetSec() == Time(time).GetSec())
			{
				return true;
			}
			return false;
		}


		bool IsSameSec(const Time &other)const
		{
			if (GetSec() == other.GetSec())
			{
				return true;
			}
			return false;
		}


		/**
		* @brief ͬһ����
		*
		* @param time
		*
		* @return
		*/
		bool IsSameMinute()const
		{
			if (GetMinute() == Time().GetMinute())
			{
				return true;
			}
			return false;

		};

		bool IsSameMinute(const time_t &time)const
		{
			if (GetMinute() == Time(time).GetMinute())
			{
				return true;
			}
			return false;
		};


		bool IsSameMinute(const Time &other)const
		{
			if (GetMinute() == other.GetMinute())
			{
				return true;
			}
			return false;
		}

		/**
		* @brief ͬһСʱ
		*
		* @return
		*/
		bool IsSameHour()const
		{
			if (GetHour() == Time().GetHour()) {
				return true;
			}
			return false;
		}


		bool IsSameHour(const time_t &time)const
		{
			if (GetHour() == Time(time).GetHour()) {
				return true;
			}
			return false;

		};


		bool IsSameHour(const Time &other)const
		{
			if (GetHour() == other.GetHour()) {
				return true;
			}
			return false;
		}



		bool operator > (const Time&other) const {
			return _tTime > other._tTime;
		}

		bool operator >= (const Time&other) const {
			return _tTime >= other._tTime;
		}

		bool operator < (const Time&other) const {
			return _tTime < other._tTime;
		}

		bool operator <= (const Time&other) const {
			return _tTime <= other._tTime;
		}

		bool operator == (const Time&other) const {
			return _tTime == other._tTime;
		}

		bool operator != (const Time&other) const {
			return _tTime != other._tTime;
		}

		Time& operator +(const Time&other) {
			_tTime += other._tTime;
			gmtime_r(&_tTime, &_tmTime);
			return *this;
		}

		Time& operator - (const Time&other) {
			_tTime -= other._tTime;
			gmtime_r(&_tTime, &_tmTime);
			return *this;
		}

		Time& operator = (const Time&other) {
			_tTime = other._tTime;
			gmtime_r(&_tTime, &_tmTime);
			return *this;
		}



	private:
		struct tm _tmTime;
		//��λ��
		time_t _tTime;
	};


	static ClockTime _globaTime;


	/**
	* @brief ѭ��ʱ�侫�Ⱥ���
	*/
	class CycleTime
	{

	public:

		CycleTime(const int& cycleMsec, const time_t&  msec = ClockTime().GetMsec()) :_cycleMsec(cycleMsec) {
			_nextMsec = msec + _cycleMsec;
		}

		CycleTime(const int& cycleMsec, const Time& time) :_cycleMsec(cycleMsec) {
			_nextMsec = time.GetSec() * 1000 + cycleMsec;
		}


		/**
		* @brief ���¼���ʱ��
		*/
		void Reset(const time_t &time) {
			_nextMsec = time + _cycleMsec;
		};

		/**
		* @brief ���ú�������
		*
		* @param msec ������
		*/
		void SetMsecCycle(int msec) {
			_cycleMsec = msec;
		}


		/**
		* @brief ������������
		*
		* @param sec
		*/
		void SetSecCycle(int sec) {
			_cycleMsec = sec * 1000;
		}


		bool operator ()(const time_t& Msec) {
			if (_nextMsec >= Msec) {
				Reset(Msec);
				return true;
			}
			return false;
		}


	private:

		/**
		* @brief ���ں��뵥λ
		*/
		int _cycleMsec;

		/**
		* @brief ��һ��ʱ��(����)
		*/
		time_t _nextMsec;
	};


	/**
	* @brief ʱ�丨������
	*/
	class TimeHelp
	{
	public:
		/**
		* @brief �Ƿ���ͬһ��
		*
		* @param src
		* @param other
		*
		* @return
		*/
		static bool IsSameYear(time_t&src, time_t&other) {
			return Time(src).GetYear() == Time(other).GetYear();
		}


		/**
		* @brief �����һ����Сʱutcʱ��
		*
		* @return
		*/
		static DWORD GetNextHourSec() {
			Time time;
			return time.GetUtcSec() + (59 - time.GetHour()) * 60 + (59 - time.GetSec());
		}


		/**
		* @brief �Ƿ���ͬһ��
		*
		* @param src
		* @param other
		*
		* @return
		*/
		static bool IsSameMonth(time_t&src, time_t&other) {
			return Time(src).GetMonth() == Time(other).GetMonth();
		}

		/**
		* @brief �Ƿ���ͬһ��
		*
		* @param src
		* @param other
		*
		* @return
		*/
		static bool IsSameDay(time_t&src, time_t&other)
		{

			Time sTime(src);
			Time oTime(other);
			if (sTime.GetYDay() == oTime.GetYDay()) {
				if (sTime.GetYear() == oTime.GetYear()) {
					return true;
				}
			}
			return false;
		}


		/**
		* @brief �Ƿ���ͬһ��
		*
		* @param src
		* @param other
		*
		* @return
		*/
		static bool IsSameWeek(time_t&src, time_t& other)
		{
			Time sTime(src);
			Time oTime(other);
			if (sTime.GetYDay() - sTime.GetWDay() == oTime.GetYDay() - sTime.GetWDay()) {
				if (sTime.GetYear() == oTime.GetYear()) {
					return true;
				}
			}
			return false;
		}


		/**
		* @brief �Ƿ���ͬһСʱ(�������Ƿ�Ϊͬһ��)
		*
		* @param src
		* @param other
		*
		* @return
		*/
		static bool isSameHour(time_t&src, time_t&other) {
			return 	Time(src).GetHour() == Time(other).GetHour();
		}


		/**
		* @brief �Ƿ���ͬһ����(�������Ƿ�Ϊͬһ��ͬһСʱ)
		*
		* @param src
		* @param other
		*
		* @return
		*/
		static	bool isSameMinute(time_t&src, time_t&other) {
			if (Time(src).GetMinute() == Time(other).GetMinute()) {
				return true;
			}
			return false;
		}
	};

	typedef std::function<void()> TimerTaskFunc;

	class TimerTaskManage
	{
	public:

		/**
		* @brief ����ѭ������
		*
		* @param cycleTime ����ִ��ʱ��
		* @param func ִ�к���
		*/
		void AddCycleTask(int cycleTime, TimerTaskFunc&func, bool runNow = false) {
			AddCountCycleTask(cycleTime, -1, func, runNow);
		};

		/**
		* @brief ����ѭ�������д�������
		*
		* @param cycleTime ����ִ��ʱ��
		* @param cycleCounts ����ִ�д���
		* @param func ִ�к���
		*/
		void AddCountCycleTask(int cycleTime, int cycleCounts, TimerTaskFunc& func, bool runNow = false) {
			if (func == nullptr) {
				return;
			}
			if (runNow) {
				func();
				--cycleCounts;
				if (cycleCounts == 0) {
					return;
				}
			}
			TimerTask task;
			task._cycleMSec = cycleTime;
			task._nextRunMSec = ClockTime().GetMsec() + cycleTime;
			task._func = func;
			task._cycleCounts = cycleCounts;
			_taskList.push_back(task);
			_taskList.sort();


		};

		struct TimerTask {
			TimerTaskFunc _func;
			DWORD _cycleMSec{ 0 };//���ں���
			QWORD _nextRunMSec{ 0 };//��һ�����к���
			int _cycleCounts{ -1 };//����ִ�д���
			bool operator < (const TimerTask &othrer) {
				return _nextRunMSec < othrer._nextRunMSec;
			}
		};

		void Update() {
			Update(ClockTime().GetMsec());
		};

		void Update(QWORD nowMSec) {
			bool delTask = false;
			for (auto it = _taskList.begin(); it != _taskList.end(); it++) {
				if (it->_nextRunMSec <= nowMSec) {
					if (it->_func != nullptr) {
						it->_func();
						it->_nextRunMSec += it->_cycleMSec;
						if (it->_cycleCounts != -1) {
							it->_cycleCounts -= 1;
							if (it->_cycleCounts == 0) {
								delTask = true;
							}
						}
					}
				}
				else {
					break;
				}
			}

			if (delTask) {
				auto delTask = [&](const TimerTask& timerTask) {
					if (timerTask._cycleCounts == 0) {
						return true;
					}
					return false;
				};
				_taskList.remove_if(delTask);
			}
			_taskList.sort();
		};
	private:
		std::list<TimerTask> _taskList;
	};
};
#endif




