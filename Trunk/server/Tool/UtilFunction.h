#ifndef __HELP_FUNCTION_H__
#define __HELP_FUNCTION_H__

#include <random>
#include <time.h>
#include <set>
#include <string>
#include "../Define/PlatformDefine.h"

namespace Tool
{

	/**
	* @brief ��Χ[numberic_limits<int>::min(),numberic_limits<int>::max()]
	*
	* @return
	*/
	static int RandomInt();

	/**
	* @brief ��Χ[0,max]
	*
	* @param max
	*
	* @return
	*/
	static int RandomInt(const int& max);

	/**
	* @brief ��Χ[min,max]
	*
	* @param min
	* @param max
	*
	* @return
	*/
	static int RandomInt(const int& min, const int& max);

	/**
	* @brief ��Χ[0.0, 1.0)
	*
	* @return
	*/
	static float RandomFloat();

	/**
	* @brief ��Χ[0.0,max)
	*
	* @param max
	*
	* @return
	*/
	static float RandomFloat(const float max);

	/**
	* @brief ��Χ[min,max)
	*
	* @param min
	* @param max
	*
	* @return
	*/
	static float RandomFloat(const float& min, const float& max);

	/**
	* @brief ���һ����Χ�ڵĲ��ظ�����
	*
	* @param num ��ò��ظ���������
	* @param min
	* @param max
	* @param diffNum ���
	*
	* @return
	*/
	static bool DifferentInt(const DWORD& num, const int& begin, const int& end, std::set<int>&diffNum);

	/**
	* @brief  �ַ����Ƿ�Ϊ��
	*
	* @param str
	*
	* @return
	*/
	static	bool CheckIsEmpty(const char* str);

	/**
	* @brief �õ�crc
	*
	* @param buf
	* @param nLength
	*
	* @return
	*/
	static DWORD GetCRC(const BYTE * str, int nLength);


	/**
	* @brief string תint
	*
	* @param str
	*
	* @return
	*/
	static int AsInt(const char* str);
	static int AsInt(const std::string& str) {
		return AsInt(str.c_str());
	};

	static int AsInt(const double& value) {
		return static_cast<int>(value);
	}
	static int AsInt(const float& value) {
		return static_cast<int>(value);
	};
	static int AsInt(int& value) {
		return value;
	};

	/**
	* @brief string תdouble
	*
	* @param str
	* @param defaultVal str Ϊ��ʱ���ص�Ĭ��ֵ 
	*
	* @return
	*/

	static DOUBLE AsDouble(const char* str,DOUBLE defaultVal = 0.0f);
	/**
	* @brief string תfloat
	*
	* @param str
	* @param defaultVal
	*
	* @return
	*/
	static  float AsFloat(const char* str, float defaultVal = 0.0f);
	/**
	* @brief *����תstring
	*
	* @tparam T
	* @param value
	*
	* @return
	*/
	template<typename T>
	static std::string AsString(T value) {
		return std::string(value);
	}
	template<class T>
	struct Error {
		enum {
			value = 0,
		};
	};
	template<typename T, typename M>
	static void AsTransform(T&out, M&in) {
		static_assert(Error<T>::value, "error as Transorm");
	}

	template<typename M>
	static void AsTransform(std::string& out, M&in) {
		out = AsString(in);
	}

	template<typename M>
	static void AsTransform(int& out, M&in) {
		out = AsInt(in);
	}

	template<typename M>
	static void AsTransform(double& out, M&in) {
		out = AsDouble(in);
	}

	template<typename M>
	static void AsTransform(float& out, M&in) {
		out = AsFloat(in);
	}


	template<typename T, typename M>
	static T AutoTransform(M m) {
		T value;
		AsTransform(value, m);
		return value;
	}





	/**
	* @brief �뼶��ʱ�� ��ǰ�̻߳����
	*
	* @param seconds
	*/
	static void SSleep(DWORD seconds);

	/**
	* @brief ���뼶��ʱ�� ��ǰ�̻߳����
	*
	* @param msec
	*/
	static void MSleep(QWORD msec);

	/**
	* @brief ΢���ʱ�� ��ǰ�̻߳����
	*
	* @param usec
	*/
	static void USleep(QWORD usec);

	template<typename ResultType>
	static void SplictString(const char* srcStr, std::vector<ResultType>& result, const char* splictStr) {

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
