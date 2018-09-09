#ifndef __VAR_LIST_H__
#define __VAR_LIST_H__
#include <list>
#include<vector>
#include <assert.h>
#include <memory>
#include "../Define/TypeDefine.h"


class VarObj
{
public:
	enum class ValueType
	{
		BOOLVALUE,
		INTVALUE,
		DWORDVALUE,
		QWORDVALUE,
		LONGVALUE,
		FLOATVALUE,
		DOUBLEVALUE,
		STRVALUE,
	};



	void setValue(const char* value)
	{
		_value._valueType = ValueType::STRVALUE;
		_value._strValue = value;

	}

	void setValue(char *value)
	{
		_value._valueType = ValueType::STRVALUE;
		_value._strValue = value;

	}

	void setValue(bool value)
	{

		_value._valueType = ValueType::BOOLVALUE;
		_value._numValue.boolValue = value;
	}

	void setValue(int value)
	{

		_value._valueType = ValueType::INTVALUE;
		_value._numValue.intValue = value;
	}

	void setValue(LONG value)
	{
		_value._valueType = ValueType::LONGVALUE;
		_value._numValue.longValue = value;
	}

	void setValue(FLOAT value)
	{
		_value._valueType = ValueType::FLOATVALUE;
		_value._numValue.floatValue = value;

	}
	void setValue(DOUBLE value)
	{
		_value._valueType = ValueType::DOUBLEVALUE;
		_value._numValue.doubleValue = value;

	}

	int GetIntValue() const
	{
		if (_value._valueType != ValueType::INTVALUE) {
			printf("valueType error!");
		}
		return _value._numValue.intValue;
	};

	bool GetBoolValue() const
	{

		if (_value._valueType != ValueType::BOOLVALUE) {
			printf("valueType error!");
		}

		return _value._numValue.boolValue;
	}

	DWORD GetDWValue() const
	{
		if (_value._valueType != ValueType::DWORDVALUE) {
			printf("valueType error!");
		}
		return _value._numValue.dwValue;
	}

	QWORD GetQWValue() const
	{
		if (_value._valueType != ValueType::QWORDVALUE) {
			printf("valueType error!");
		}
		return _value._numValue.qValue;
	}

	std::string GetStrValue() const
	{
		if (_value._valueType != ValueType::STRVALUE) {
			printf("valueType error!");
		}

		return _value._strValue;
	}

	LONG GetLongValue() const
	{
		if (_value._valueType != ValueType::LONGVALUE) {
			printf("valueType error!");
		}
		return _value._numValue.longValue;
	}

	FLOAT GetFloatValue() const
	{
		if (_value._valueType != ValueType::FLOATVALUE) {
			printf("valueType error!");
		}

		return _value._numValue.floatValue;
	}

	double GetDoubleValue() const
	{
		if (_value._valueType != ValueType::DOUBLEVALUE) {
			printf("valueType error!");
		}
		return _value._numValue.doubleValue;
	}

private:

	union NumValue {
		bool		boolValue;
		char*		strValue;
		DWORD		dwValue;
		WORD		wValue;
		QWORD		qValue;
		int			intValue;
		LONG        longValue;
		FLOAT       floatValue;
		DOUBLE      doubleValue;
	};

	struct Value {
		NumValue _numValue;
		std::string _strValue;
		ValueType _valueType;
	};

	Value _value;
};




class VarList
{

public:

	int GetIntValue(DWORD i)
	{
		assert(i < _varList.size());
		return _varList[i].GetIntValue();
	};

	bool GetBoolValue(DWORD i)
	{
		assert(i < _varList.size());
		return _varList[i].GetBoolValue();
	}

	DWORD GetDWValue(DWORD i)
	{
		assert(i < _varList.size());
		return _varList[i].GetDWValue();
	}

	DWORD GetQWValue(DWORD i)
	{
		assert(i < _varList.size());
		return _varList[i].GetQWValue();
	}

	std::string GetStrValue(DWORD i)
	{
		assert(i < _varList.size());
		return _varList[i].GetStrValue();
	}

	LONG GetLongValue(DWORD i)
	{
		assert(i < _varList.size());
		return _varList[i].GetLongValue();
	}

	FLOAT GetFloatValue(DWORD i)
	{
		assert(i < _varList.size());
		return _varList[i].GetFloatValue();
	}

	DOUBLE GetDoubleValue(DWORD i)
	{
		assert(i < _varList.size());
		return _varList[i].GetDoubleValue();

	}

	template<typename T>
	VarList& operator<<(T value)
	{
		VarObj var;
		var.setValue(value);
		_varList.push_back(var);
		return *this;

	}

	VarList& operator<<(VarList& other)
	{
		_varList.insert(_varList.end(), other._varList.begin(), other._varList.end());
		return *this;
	}

	inline DWORD size() const
	{
		return _varList.size();
	}


private:

	std::vector<VarObj> _varList;

};

#endif
