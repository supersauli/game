#ifndef __MESSAGE_MANAGE_H__
#define __MESSAGE_MANAGE_H__
#include <string>
#include <functional>
#include <map>
#include <vector>
#include <memory>
#include "Protobuf.h"
#include "Connection.h"
#include "Mutex.h"
typedef std::function<void(DWORD, const void*)> MessageCB;
class MessageManage
{
public:
	static MessageManage& GetInstance()
	{
		static MessageManage c;
		return c;
	}
	void AddMessageCallBack(std::string messageName, MessageCB func) const
	{
		AutoMutex mutex;
		mutex.Lock();
		_messageCB[messageName].push_back(func);
	}

	void RecvMessage(DWORD uid, std::string&msg)
	{
		AutoMutex mutex;
		mutex.Lock();
		const auto proto  = _protobufManage.ecode(msg.c_str());
		if(proto == nullptr){
			return;
		}
		auto it = _messageCB.find(proto->GetTypeName());
		if(it != _messageCB.end())
		{
			for(const auto& its:it->second)
			{
				its(uid, proto);
			}
		}
	}
	void SendMessage(DWORD uid, const ProtoBuffMessage& msg)
	{
		AutoMutex mutex;
		mutex.Lock();
		std::string data;
		const auto size = _protobufManage.decode(data, msg);
		if (size == 0) {
			return;
		}
		auto it = _connectionMap.find(uid);
		if (it != _connectionMap.end())
		{
			it->second->SendMsg(data);
		}
	};

	void AddConnection(const std::shared_ptr <Connection> connection)
	{
		AutoMutex mutex;
		mutex.Lock();
		_connectionMap[connection->_uid] = connection;
		connection->_recvCB = std::bind(&MessageManage::RecvMessage,this,std::placeholders::_1,std::placeholders::_2);
	}

	void ReleaseConnection(const std::shared_ptr<Connection> connection)
	{
		AutoMutex mutex;
		mutex.Lock();
		auto it = _connectionMap.find(connection->_uid);
		if(it!=_connectionMap.end())
		{
			_connectionMap.erase(it);
		}
	};

private:
	ProtobufManage _protobufManage;
	MessageManage() = default;
	static	std::map<std::string, std::vector<MessageCB>> _messageCB;
	std::map<DWORD, std::shared_ptr<Connection>>  _connectionMap;
};

std::map<std::string, std::vector<MessageCB>> MessageManage::_messageCB;


#endif