#ifndef _STIME_H__
#define _STIME_H__
#include <ctime>
#include <list>

namespace Tool {
	/**
	* @brief 精度毫秒
	*/
	class ClockTime final {
	public:
		ClockTime() {
			clock_gettime(CLOCK_REALTIME, &_clockTime);
		}

		/**
		* @brief 刷新时间
		*/
		void RefreshTimer() {
			clock_gettime(CLOCK_REALTIME, &_clockTime);
		}

		/**
		* @brief 获得秒数
		*
		* @return
		*/
		DWORD GetSec()const {
			return _clockTime.tv_sec;
		}

		/**
		* @brief 获得毫秒数
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
	* @brief 时间函数精度秒
	*/
	class Time final
	{
	public:
		/**
		* @brief 几几年
		*
		* @return
		*/
		Time() {
			time(&_tTime);
			gmtime_r(&_tTime, &_tmTime);
		}

		/**
		* @brief dwTime 是秒
		*
		* @param
		*/
		Time(time_t dwTime) :_tTime(dwTime) {
			gmtime_r(&_tTime, &_tmTime);
		}

		/**
		* @brief 获得utc时间
		*
		* @return
		*/
		DWORD GetUtcSec() {
			return _tTime;
		};

		/**
		* @brief 刷新时间
		*/
		void Refresh() {
			time(&_tTime);
			gmtime_r(&_tTime, &_tmTime);
		}


		/**
		* @brief 哪年
		*
		* @return
		*/
		DWORD GetYear()const
		{
			return _tmTime.tm_year + 1900;
		}

		/**
		* @brief 哪个月 [1,12]
		*
		* @return
		*/
		DWORD GetMonth()const
		{
			return _tmTime.tm_mon + 1;
		}

		/**
		* @brief 一个月中的日期[1,31]
		*
		* @return
		*/
		DWORD GetMDay()const
		{
			return _tmTime.tm_mday;
		}

		/**
		* @brief 周几(1-7)
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
		* @brief 从每年的1月1日开始的天数 [0,365]，其中0代表1月1日
		*
		* @return
		*/
		DWORD GetYDay()const
		{
			return _tmTime.tm_yday;

		}

		/**
		* @brief 小时[0,23]　
		*
		* @return
		*/
		DWORD GetHour()const
		{
			return _tmTime.tm_hour;
		}
		/**
		* @brief 分钟数[0,59]
		*
		* @return
		*/
		DWORD GetMinute()const
		{
			return _tmTime.tm_min;
		}

		/**
		* @brief 秒数
		*
		* @return [0,59]
		*/
		DWORD GetSec()const
		{
			return _tmTime.tm_sec;
		}


		/**
		* @brief 获得流逝秒
		*
		* @return
		*/
		DWORD GetElapseSec()
		{
			return ::time(NULL) - _tTime;
		}

		/**
		* @brief 设置秒数
		*
		* @param time
		*/
		void SetSec(const time_t&sec)
		{
			_tTime = sec;
			gmtime_r(&_tTime, &_tmTime);
		}

		/**
		* @brief 增加延迟秒
		*
		* @param sec
		*/
		void AddDelaySec(const time_t&sec)
		{
			_tTime += sec;
			gmtime_r(&_tTime, &_tmTime);

		};



		/**
		* @brief 同一秒
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
		* @brief 同一分钟
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
		* @brief 同一小时
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
		//单位秒
		time_t _tTime;
	};


	static ClockTime _globaTime;


	/**
	* @brief 循环时间精度毫秒
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
		* @brief 重新计算时间
		*/
		void Reset(const time_t &time) {
			_nextMsec = time + _cycleMsec;
		};

		/**
		* @brief 设置毫秒周期
		*
		* @param msec 毫秒数
		*/
		void SetMsecCycle(int msec) {
			_cycleMsec = msec;
		}


		/**
		* @brief 秒设置秒周期
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
		* @brief 周期毫秒单位
		*/
		int _cycleMsec;

		/**
		* @brief 下一次时间(毫秒)
		*/
		time_t _nextMsec;
	};


	/**
	* @brief 时间辅助函数
	*/
	class TimeHelp
	{
	public:
		/**
		* @brief 是否是同一年
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
		* @brief 获得下一个整小时utc时间
		*
		* @return
		*/
		static DWORD GetNextHourSec() {
			Time time;
			return time.GetUtcSec() + (59 - time.GetHour()) * 60 + (59 - time.GetSec());
		}


		/**
		* @brief 是否是同一月
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
		* @brief 是否是同一天
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
		* @brief 是否是同一周
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
		* @brief 是否是同一小时(不关心是否为同一天)
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
		* @brief 是否是同一分钟(不关心是否为同一天同一小时)
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
		* @brief 增加循环任务
		*
		* @param cycleTime 周期执行时间
		* @param func 执行函数
		*/
		void AddCycleTask(int cycleTime, TimerTaskFunc&func, bool runNow = false) {
			AddCountCycleTask(cycleTime, -1, func, runNow);
		};

		/**
		* @brief 增加循环任务有次数限制
		*
		* @param cycleTime 周期执行时间
		* @param cycleCounts 周期执行次数
		* @param func 执行函数
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
			DWORD _cycleMSec{ 0 };//周期毫秒
			QWORD _nextRunMSec{ 0 };//下一次运行毫秒
			int _cycleCounts{ -1 };//周期执行次数
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




