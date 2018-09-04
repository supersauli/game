#include "Redis.h"


bool Redis::Connect(const char* host,int port)
{
	bool resultBool = false;
	_connect = redisConnect(host,port);
	if(_connect != nullptr)
	{
		struct timeval timeout = { 1, 500000 }; // 1.5 seconds
		redisConnectWithTimeout(host, port, timeout);
		resultBool =  !_connect->err;
	}

	return resultBool;
}

std::string Redis::Get(const char* key)
{
	_reply = Command("get %s",key); 
	std::string str;
	if(_reply != nullptr){ 
		str = _reply->str; 
		//freeReplyObject(_reply); 
	}
	return str;
}

bool Redis::Set(const char* key,const char* value)
{   
	bool replyBool = false;
	_reply= Command("set %s %s",key,value); 
	if(_reply != nullptr)
	{		
		if(strcmp(_reply->str,REDIS_REPLY_TRUE) == 0){
			replyBool = true;
		}
		//freeReplyObject(_reply);
	}
	return replyBool;
}   



