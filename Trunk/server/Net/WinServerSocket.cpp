#include "WinServerSocket.h"
#include <functional>
#include <iostream>
#include <thread>
#include "Consumer.h"

void WinServerSocket::SetPort(int port)
{
	port = _port;
}

int WinServerSocket::GetPort()
{
	return _port;
}


void WinServerSocket::Run(int threadNum)
{
	InitData();
	_completionPort = CreateIoCompletionPort(INVALID_HANDLE_VALUE, NULL, 0, 0);
	if(_completionPort == nullptr)
	{
		//TODO
		return;
	}

	if(threadNum == -1)
	{
		threadNum = GetSysteamProcessNum() * 2;
	}
	InitReadThread(threadNum);
	InitSocket();
	DoAccept();
}

DWORD WinServerSocket::ReadThread(LPVOID CompletionPortID)
{
	auto completionPort = CompletionPortID;
	while (true)
	{
		DWORD bytes = 0;
		LPOVERLAPPED IpOverlapped;
		ULONG_PTR* socket = NULL;

		auto ret = GetQueuedCompletionStatus(_completionPort, &bytes, socket, static_cast<LPOVERLAPPED*>(&IpOverlapped), INFINITE);
		if(bytes == 0)
		{
			continue;	
		}
		if(ret == -1)
		{
			//TODO
			return -1;
		}
		char buf[1024] = { 0 };
		WSABUF buffer;
		buffer.len = 1024;
		buffer.buf = buf;
		DWORD recvBytes = 0;
		DWORD flags = 0;
		OVERLAPPED overlapped;
		WSARecv(*socket, &buffer, 1, &recvBytes,&flags, &overlapped,NULL);
		std::cout << buf << std::endl;
		Send(*socket,buf);
	}




	return 0;
}

DWORD WinServerSocket::Send(int socket, const char* buf)
{
	send(socket,buf,1024,0 );
	return 0;
}

int WinServerSocket::InitData()
{
	// ����socket��̬���ӿ�
	WORD wVersionRequested = MAKEWORD(2, 2); // ����2.2�汾��WinSock��
	WSADATA wsaData;	// ����Windows Socket�Ľṹ��Ϣ
	DWORD err = WSAStartup(wVersionRequested, &wsaData);

	if (0 != err) {	// ����׽��ֿ��Ƿ�����ɹ�
		system("pause");
		return -1;
	}
	if (LOBYTE(wsaData.wVersion) != 2 || HIBYTE(wsaData.wVersion) != 2) {// ����Ƿ�����������汾���׽��ֿ�
		WSACleanup();
		system("pause");
		return -1;
	}
}


void WinServerSocket::InitReadThread(int threadNum)
{
	for(auto i  = 0 ;i< threadNum;i++)
		{
			std::thread(std::bind(&WinServerSocket::ReadThread, this, std::placeholders::_1),_completionPort);
		}
}

int WinServerSocket::GetSysteamProcessNum()
{
	SYSTEM_INFO mySysInfo;
	GetSystemInfo(&mySysInfo);
	return  mySysInfo.dwNumberOfProcessors;
}

void WinServerSocket::InitSocket()
{
	// ������ʽ�׽���
	_socket = socket(AF_INET, SOCK_STREAM, 0);

	// ��SOCKET������
	SOCKADDR_IN srvAddr;
	srvAddr.sin_addr.S_un.S_addr = htonl(INADDR_ANY);
	srvAddr.sin_family = AF_INET;
	srvAddr.sin_port = htons(_port);
	int bindResult = ::bind(_socket, (SOCKADDR*)&srvAddr, sizeof(SOCKADDR));
	if (SOCKET_ERROR == bindResult) {
		//TODO
		return ;
	}
	// ��SOCKET����Ϊ����ģʽ
	int listenResult = listen(_socket, 10);
	if (SOCKET_ERROR == listenResult) {
		//TODO
		return;
	}
}

void WinServerSocket::DoAccept()
{
	while(true)
	{
		SOCKADDR_IN info;
		int len = 0;
		auto acceptSocket = accept(_socket, (SOCKADDR*)&info,&len);
		if(acceptSocket == SOCKET_ERROR)
		{
			//TODO
			continue;
		}
		auto consumer = std::make_shared<Consumer>();
		if(consumer == NULL)
		{
			return;
		}
		consumer->_socket = acceptSocket;
		memcpy(&consumer->_addr, &info, len);
		CreateIoCompletionPort(reinterpret_cast<HANDLE>(acceptSocket), _completionPort, acceptSocket, 0);

	}
}
