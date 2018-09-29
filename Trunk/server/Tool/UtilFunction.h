#ifndef __UTIL_FUNCTION_H__
#define __UTIL_FUNCTION_H__

#include <random>
#include <time.h>
#include <set>
#include <string>
#include "../Define/TypeDefine.h"

namespace Util
{

	/**
	* @brief 范围[numberic_limits<int>::min(),numberic_limits<int>::max()]
	*
	* @return
	*/
	 int RandomInt();

	/**
	* @brief 范围[0,max]
	*
	* @param max
	*
	* @return
	*/
	  int RandomInt(const int& max);

	/**
	* @brief 范围[min,max]
	*
	* @param min
	* @param max
	*
	* @return
	*/
	  int RandomInt(const int& min, const int& max);

	/**
	* @brief 范围[0.0, 1.0)
	*
	* @return
	*/
	  float RandomFloat();

	/**
	* @brief 范围[0.0,max)
	*
	* @param max
	*
	* @return
	*/
	  float RandomFloat(const float max);

	/**
	* @brief 范围[min,max)
	*
	* @param min
	* @param max
	*
	* @return
	*/
	  float RandomFloat(const float& min, const float& max);

	/**
	* @brief 获得一定范围内的不重复整数
	*
	* @param num 获得不重复整数数量
	* @param min
	* @param max
	* @param diffNum 结果
	*
	* @return
	*/
	  bool DifferentInt(const DWORD& num, const int& begin, const int& end, std::set<int>&diffNum);

	/**
	* @brief  字符串是否为空
	*
	* @param str
	*
	* @return
	*/
	 bool CheckIsEmpty(const char* str);

	/**
	* @brief 得到crc
	*
	* @param buf
	* @param nLength
	*
	* @return
	*/
	  DWORD GetCRC(const BYTE * str, int nLength);


	/**
	* @brief string 转int
	*
	* @param str
	*
	* @return
	*/
	  int AsInt(const char* str);

	  inline int AsInt(const double& value) {
		  return  static_cast<int>(value);
	  }
	  inline int AsInt(const float& value) {
		  return  static_cast<int>(value);
	  };
	  inline int AsInt(int& value) {
		  return value;
	  };

	/**
	* @brief string 转double
	*
	* @param str
	* @param defaultVal str 为空时返回的默认值 
	*
	* @return
	*/

	  DOUBLE AsDouble(const char* str,DOUBLE defaultVal = 0.0f);
	/**
	* @brief string 转float
	*
	* @param str
	* @param defaultVal
	*
	* @return
	*/
	   float AsFloat(const char* str, float defaultVal = 0.0f);
	/**
	* @brief *类型转string
	*
	* @tparam T
	* @param value
	*
	* @return
	*/
	template<typename T>
	inline  std::string AsString(T value) {
		return std::string(value);
	}

	template<>
	inline std::string AsString(int value){
		char str[13];
		snprintf(str, 13,"%d", value);
		return str;
	}


	template<class T>
	struct Error {
		enum {
			value = 0,
		};
	};
	template<typename T, typename M>
	  void AsTransform(T&out, M&in) {
		 _assert(Error<T>::value, "error as Transorm");
	}

	template<typename M>
	  void AsTransform(std::string& out, M&in) {
		out = AsString(in);
	}

	template<typename M>
	  void AsTransform(int& out, M&in) {
		out = AsInt(in);
	}

	template<typename M>
	  void AsTransform(double& out, M&in) {
		out = AsDouble(in);
	}

	template<typename M>
	  void AsTransform(float& out, M&in) {
		out = AsFloat(in);
	}


	template<typename T, typename M>
	  T AutoTransform(M m) {
		T value;
		AsTransform(value, m);
		return value;
	}





	/**
	* @brief 秒级别定时器 当前线程会挂起
	*
	* @param seconds
	*/
	  void SSleep(DWORD seconds);

	/**
	* @brief 毫秒级别定时器 当前线程会挂起
	*
	* @param msec
	*/
	  void MSleep(QWORD msec);

	/**
	* @brief 微妙级别定时器 当前线程会挂起
	*
	* @param usec
	*/
	  void USleep(QWORD usec);

	template<typename ResultType>
	  void SplictString(const char* srcStr, std::vector<ResultType>& result, const char* splictStr) {

		std::string src = srcStr;
		std::string::size_type pos1 = 0, pos2 = 0;

		if (!CheckIsEmpty(splictStr)) {
			do {
				pos2 = src.find(splictStr, pos1);
				if (pos2 == std::string::npos) {
					break;
				}
				result.push_back(AutoTransform<ResultType>(src.substr(pos1, pos2 - pos1)));
				pos1 = pos2 + 1;
			} while (true);
		}
		else {
			if (pos1 != src.length()) {
				result.push_back(AutoTransform<ResultType>(src.substr(pos1)));
			}
		}
	}
#ifdef __linux
	typedef char UuidStr[36];
	void GetStrUid(UuidStr* uidStr)
	{
		uuid_t uid;
		uuid_generate_time(uid);
		uuid_unparse(uu, uidStr);
	}

#endif
}
#endif
