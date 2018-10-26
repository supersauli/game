#ifndef __WIN_SERVER_SOCKET_DEFINE_H__
#define __WIN_SERVER_SOCKET_DEFINE_H__
//////////////////////////////////////////////////////////////////
#define MAX_BUFFER_LEN  8*1024
#include <datetimeapi.h>
#include <WinSock2.h>

// ����ɶ˿���Ͷ�ݵ�I/O����������
typedef enum _OPERATION_TYPE
{
	ACCEPT_POSTED,                     // ��־Ͷ�ݵ�Accept����
	SEND_POSTED,                       // ��־Ͷ�ݵ��Ƿ��Ͳ���
	RECV_POSTED,                       // ��־Ͷ�ݵ��ǽ��ղ���
	NULL_POSTED                        // ���ڳ�ʼ����������

}OPERATION_TYPE;
typedef struct _PER_IO_CONTEXT
{
	OVERLAPPED     _overlapped;                               // ÿһ���ص�����������ص��ṹ(���ÿһ��Socket��ÿһ����������Ҫ��һ��)              
	SOCKET         _sockAccept;                               // ������������ʹ�õ�Socket
	WSABUF         _wsaBuf;                                   // WSA���͵Ļ����������ڸ��ص�������������
	char           _szBuffer[MAX_BUFFER_LEN];                 // �����WSABUF�������ַ��Ļ�����
	OPERATION_TYPE _opType;                                   // ��ʶ�������������(��Ӧ�����ö��)

	// ��ʼ��
	_PER_IO_CONTEXT()
	{
		ZeroMemory(&_overlapped, sizeof(_overlapped));
		ZeroMemory(_szBuffer, MAX_BUFFER_LEN);
		_sockAccept = INVALID_SOCKET;
		_wsaBuf.buf = _szBuffer;
		_wsaBuf.len = MAX_BUFFER_LEN;
		_opType = NULL_POSTED;
	}
	// �ͷŵ�Socket
	~_PER_IO_CONTEXT()
	{
		if (_sockAccept != INVALID_SOCKET)
		{
			closesocket(_sockAccept);
			_sockAccept = INVALID_SOCKET;
		}
	}
	// ���û���������
	void ResetBuffer()
	{
		ZeroMemory(_szBuffer, MAX_BUFFER_LEN);
	}

} PER_IO_CONTEXT, *PPER_IO_CONTEXT;

enum
{
	THREAD_EVENT_EXIT,//�ֳ��˳�	
};



#endif