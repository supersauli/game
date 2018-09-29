#ifndef __PROTOBUF_MANAGE_H__
#define __PROTOBUF_MANAGE_H__
#include <google/protobuf/message.h>
#include <functional>
#include <string>
#include <map>
#include "../Tool/UtilFunction.h"

typedef google::protobuf::Message			ProtoBuffMessage;
typedef google::protobuf::Descriptor		ProtoBuffDesc;
typedef google::protobuf::DescriptorPool    ProtoBuffDescPool;
typedef google::protobuf::MessageFactory    ProtoBuffMessageFactory;
class ProtobufManage//: std::noncopyable
{
	inline ProtoBuffMessage*  createMessage(const std::string& name) {
		const ProtoBuffDesc *desc = ProtoBuffDescPool::generated_pool()->FindMessageTypeByName(name);
		if (desc != nullptr)
		{
			const ProtoBuffMessage * prototype = ProtoBuffMessageFactory::generated_factory()->GetPrototype(desc);
			if (prototype != nullptr)
			{
				return prototype->New();
			}
		}
		return nullptr;
	};


public:
	/**
	 * @brief 编码
	 *
	 * @param buf
	 * @param message
	 */

	inline DWORD decode(std::string &buffer, const ProtoBuffMessage& message)
	{
		//int sendSize = 0;
		const std::string& typeName = message.GetTypeName();
		DWORD nameLen = static_cast<DWORD>(typeName.size());
		buffer.append(reinterpret_cast<char*>(&nameLen), sizeof(DWORD));
		buffer.append(typeName);
		DWORD byteSize = message.ByteSize();
		buffer.append(reinterpret_cast<char*>(&byteSize), sizeof(DWORD));
		return message.AppendToString(&buffer);
		
		//sendSize += byteSize;
		//return static_cast<DWORD>(buffer.size());
		
	};


	/**
	 * @brief 解码
	 *
	 * @param buf
	 */
	inline  ProtoBuffMessage* ecode(const char* buf)
	{

			const char*ptr = buf;
			int size = 0;
			DWORD nameLen = 0;
			memcpy(&nameLen, ptr, sizeof(int));
			size += sizeof(DWORD);
			char typeName[100] = { 0 };
			if(nameLen > 100)
			{
				int b = 1;
			}
			memcpy(typeName, ptr + size, nameLen);
			size += nameLen;
			int contentSize = 0;
			memcpy(&contentSize, ptr + size, sizeof(int));
			size += sizeof(DWORD);
			ProtoBuffMessage * message = createMessage(typeName);
			if (message)
			{
				if (message->ParseFromArray(ptr + size, contentSize))
				{
					return message;
				}
			}
		

		return nullptr;
	}

//	typedef std::function<void(void*, const char* socketName)> MessageCallBack;
//
//	inline void MessageDel(const char * buf, const char* socketName) {
//		auto message = ecode(buf);
//		if (message == nullptr) {
//			printf("ecode error\n");
//			return;
//		}
//		const std::string& messageName = message->GetTypeName();
//		auto it = _messageDeal.find(messageName.c_str());
//		if (it != _messageDeal.end())
//		{
//			it->second(message, socketName);
//
//			//		printf("recv message %s \n",messageName.c_str());	
//		}
//		else
//		{
//			printf("not found message %s \n", messageName.c_str());
//		}
//		delete (message);
//	};
//public:
//	void AddMessageCallBack(const char* messageName, const MessageCallBack func) {
//		if (Util::CheckIsEmpty(messageName)) {
//			return;
//		}
//		auto it = _messageDeal.find(messageName);
//		if (it != _messageDeal.end()) {
//			printf("message %s has regist \n ", messageName);
//			return;
//		}
//		_messageDeal[messageName] = func;
//	};

//private:
//	std::map<std::string, MessageCallBack> _messageDeal;
//
};

#endif
