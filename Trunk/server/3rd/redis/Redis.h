#ifndef __REDIS_H__
#define __REDIS_H__

#include <stdio.h>
#include <iostream>
#include <string.h>


#include "hiredis.h"


#define REDIS_REPLY_TRUE "OK"


class Redis
{
public:
	Redis()	
	{
	}
	~Redis()
	{
	}

	/**
	 * @brief 链接redis  
	 *
	 * @param host redis ip地址
	 * @param port 端口号
	 *
	 * @return 
	 */
	bool Connect(const char* host,int port);

	/**
	 * @brief  获得链接失败原因
	 *
	 * @return 
	 */
	const char* GetConnectError()
	{
		return _connect->errstr;	
	}

	/**
	 * @brief 获取的值
	 *
	 * @param key
	 *
	 * @return 
	 */
	std::string Get(const char* key);

	/**
	 * @brief 设置值
	 *
	 * @param key
	 * @param value
	 *
	 * @return 
	 */
	bool Set(const char* key,const char* value);


	/**
	 * @brief 删除key
	 *
	 * @param key
	 *
	 * @return 
	 */
	template <typename ...T>
	int Del(T ... keyList)
	{

		CheckString(keyList...);
		const char* cmd = GetFormatCmdStr("del",keyList ...);
		int delNum = 0;
		_reply= Command(cmd,keyList...); 
		if(_reply != nullptr)
		{    
			delNum = _reply->integer;
		}   
	
		return delNum;
	};

	template<typename ...T>
	const char* GetFormatCmdStr(const char* cmd,T... keyList)
	{
		
		_formatSymbol.clear();
		std::string formatCmd;
		formatCmd = cmd ;
		formatCmd +=" ";
		formatCmd += GetFormatSymbol(keyList...);	
		return formatCmd.c_str();
	}


	template <typename T,typename ...ValueList>
	void CheckString(T v,ValueList ...list){
		//这里要判断所有参数是不是字符串
		static_assert(std::is_same<const char*,T>::value,"error parameter is not string");
		CheckString(list...);
	}

	template<typename T>
	void CheckString(T v)
	{
		static_assert(std::is_same<const char*,T>::value,"error parameter is not string");
	}

	template<typename ...T>
	redisReply*Command(const char* cmd,T ... value)
	{
		FreeReply(_reply);
		_reply=static_cast<redisReply*>(redisCommand(_connect,cmd,value ...));
		return _reply;
	}
	
	template<typename Type ,typename... TypeList>
    const char* GetFormatSymbol(Type value,TypeList... typeList)
	{
		_formatSymbol +=GetFormatSymbol(value);
		_formatSymbol	+=" ";
		GetFormatSymbol(typeList ...);
		return _formatSymbol.c_str();
	};


	const char* GetFormatSymbol(float)
	{
		return "%f";	
	}

	const char* GetFormatSymbol(int)
	{
		return "%d";	
	}

	const char* GetFormatSymbol(const char*)
	{
		return "%s";	
	}

	template<class T>
	const char* GetFormatSymbol(T value)
	{
		static_assert(error<T>::value,"GetFormatSymbol unknown parameter type !");
		//编译会失败,不用返回值
	}

private:
	void FreeReply(redisReply*reply)
	{
		if(reply != nullptr)
		{
			freeReplyObject(reply);
		}
	
	};



	private:
		std::string _formatSymbol;
		redisContext * _connect{nullptr};
	    redisReply* _reply{nullptr};
	     
};


#endif
