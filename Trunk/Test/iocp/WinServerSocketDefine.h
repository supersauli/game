#ifndef __WIN_SERVER_SOCKET_DEFINE_H__
#define __WIN_SERVER_SOCKET_DEFINE_H__
//////////////////////////////////////////////////////////////////
#define MAX_BUFFER_LEN  8*1024
#include <datetimeapi.h>
#include <WinSock2.h>

// 在完成端口上投递的I/O操作的类型
typedef enum _OPERATION_TYPE
{
	ACCEPT_POSTED,                     // 标志投递的Accept操作
	SEND_POSTED,                       // 标志投递的是发送操作
	RECV_POSTED,                       // 标志投递的是接收操作
	NULL_POSTED                        // 用于初始化，无意义

}OPERATION_TYPE;
typedef struct _PER_IO_CONTEXT
{
	OVERLAPPED     _overlapped;                               // 每一个重叠网络操作的重叠结构(针对每一个Socket的每一个操作，都要有一个)              
	SOCKET         _sockAccept;                               // 这个网络操作所使用的Socket
	WSABUF         _wsaBuf;                                   // WSA类型的缓冲区，用于给重叠操作传参数的
	char           _szBuffer[MAX_BUFFER_LEN];                 // 这个是WSABUF里具体存字符的缓冲区
	OPERATION_TYPE _opType;                                   // 标识网络操作的类型(对应上面的枚举)

	// 初始化
	_PER_IO_CONTEXT()
	{
		ZeroMemory(&_overlapped, sizeof(_overlapped));
		ZeroMemory(_szBuffer, MAX_BUFFER_LEN);
		_sockAccept = INVALID_SOCKET;
		_wsaBuf.buf = _szBuffer;
		_wsaBuf.len = MAX_BUFFER_LEN;
		_opType = NULL_POSTED;
	}
	// 释放掉Socket
	~_PER_IO_CONTEXT()
	{
		if (_sockAccept != INVALID_SOCKET)
		{
			closesocket(_sockAccept);
			_sockAccept = INVALID_SOCKET;
		}
	}
	// 重置缓冲区内容
	void ResetBuffer()
	{
		ZeroMemory(_szBuffer, MAX_BUFFER_LEN);
	}

} PER_IO_CONTEXT, *PPER_IO_CONTEXT;

enum
{
	THREAD_EVENT_EXIT,//现成退出	
};



#endif