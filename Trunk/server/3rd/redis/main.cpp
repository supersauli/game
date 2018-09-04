#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <signal.h>
#include <assert.h>

#include "Redis.h"

#define HOST "127.0.0.1"
#define PORT 6379
using namespace std;
typedef int TYPE[2];

template<typename T>
void AA(T t)
{
	// cout<<"check result = "<<Redis::CheckType<std::string,T>::value<<endl;;
	 //cout<<"check result = "<<Redis::CheckType<int,T>::value<<endl;;
	// cout<<"check result a= "<<Redis::CheckType<std::string,T>::a<<endl;;
	 //cout<<"check result a= "<<Redis::CheckType<int,T>::a<<endl;;

}



template<typename T,typename M>
struct Text
{
	template <typename U>
	static long long AB(U a)
	{
		cout<<"111"<<endl;
	}

	static int AB(T m)
	{
		cout<<"2222"<<endl;
		return 0;
	}
	const M a;
	static const bool value = sizeof(AB(EmptyValue<M>::value)) == sizeof(int);
};

#define TEXT_CHECK_TYPE(M,N) \
	cout<<#M<<","<<#N<<"="<< std::is_same<M,N>::value <<endl;


int main()
{



	//Text<int,string> m;




	//cout<<CheckType<int,int>::value<<endl;
	//cout<<CheckType<string,int>::value<<endl;
	//cout<<CheckType<float,int>::value<<endl;
	
	TEXT_CHECK_TYPE(float,int);
	TEXT_CHECK_TYPE(double,int);
	TEXT_CHECK_TYPE(double,float);
	TEXT_CHECK_TYPE(double,float);
	TEXT_CHECK_TYPE(string,float);
	TEXT_CHECK_TYPE(char*,float);
	TEXT_CHECK_TYPE(const char*,float);
	TEXT_CHECK_TYPE(const char*,char*);
	TEXT_CHECK_TYPE(long long,int);
	TEXT_CHECK_TYPE(int,int);

	TEXT_CHECK_TYPE(int,double);
	Redis *redis = new Redis();
	if(!redis->Connect(HOST,PORT))
	{
		printf("connect error \n");	
		return 0;
	}


//	static_assert(Text<int,string>::value,"fdsfa");
//	static_assert(Text<int,int>::value,"fdsfa");
//	cout<<"111111="<<Text<int,int>::value<<endl;

//	m.AB("ss");
//	m.AB(11);

//	AA(10);
//	AA("sss");

#if 1


	int a,x,y;
	float b;
	std::string c;
	//double m;
	std::string type = redis->GetFormatSymbol(a,b,c.c_str());
	//printf("%s \n",type.c_str());
	redis->Set("name","sauli");
	redis->Set("age","sauli");
	redis->Set("user","sauli");

	printf("get name = %s \n",redis->Get("name").c_str());
	redis->Del("age","user");
	std::string delName  = "name";
	if(!redis->Del(delName.c_str()))
	{
		printf("del error \n");	
	
	}

#endif






	return 0;

}

void test()
{

	 redisContext * _connect{nullptr};
	_connect = redisConnect(HOST,PORT);
	if(_connect != nullptr)
	{   
		struct timeval timeout = { 1, 500000 }; // 1.5 seconds
		redisConnectWithTimeout(HOST,PORT, timeout);
	}   


	redisReply* reply= static_cast<redisReply*>(redisCommand(_connect,"del %s","name"));

	if(reply != nullptr)
	{
		if(reply->str == nullptr)	
		{
			printf("error \n");	
		
		}
	}



}


