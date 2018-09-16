#ifndef __MUTEX_H__
#define __MUTEX_H__
#include "../Define/PlatformDefine.h"
#if defined(Q_OS_WIN)
#include <winbase.h>
#include <datetimeapi.h>
#include <synchapi.h>
#else defined (Q_OS_LINUX)
#include <pthread.h>
#endif
class AutoMutex
{
public:
	AutoMutex()
	{
		#if defined(Q_OS_WIN)
			_hMutex = CreateMutex(nullptr, false, nullptr);
		#else defined(Q_OS_LINUX)
			pthread_mutex_init(&_mutex, NULL);
		#endif
	}
	~AutoMutex()
	{
		UnLock();
	}
	
	/**
	 * \brief ÉÏËø
	 */
	inline virtual void Lock()
	{
	#if defined(Q_OS_WIN)
		WaitForSingleObject(_hMutex, INFINITE);
	#else defined(Q_OS_LINUX)
		pthread_mutex_lock(&_mutex);
	#endif
		
	}

	/**
	 * \brief ½âËø
	 */
	inline void UnLock()
	{
	#if defined(Q_OS_WIN)
		ReleaseMutex(_hMutex);
	#else defined(Q_OS_LINUX)
		pthread_mutex_unlock(&_mutex);
	#endif
	}

private:
#if defined(Q_OS_WIN)
	HANDLE _hMutex;
#else defined(Q_OS_LINUX)
	pthread_mutex_t _mutex;
#endif

};






#endif // !__MUTEX_H__
