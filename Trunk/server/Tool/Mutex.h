#ifndef _SMUTEX_H_
#define _SMUTEX_H_
#ifdef __linux
#include <pthread.h>
#include <cstddef>

class Mutex {
public:
	Mutex() { pthread_mutex_init(&_mutex, NULL); }
	~Mutex() { pthread_mutex_destroy(&_mutex); }
	void lock() { pthread_mutex_lock(&_mutex); }
	void unLock() { pthread_mutex_unlock(&_mutex); }
private:
	pthread_mutex_t _mutex;

};
#endif

#endif
