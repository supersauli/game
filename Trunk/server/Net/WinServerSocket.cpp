#include "WinServerSocket.h"
#include <functional>
#include <iostream>
#include <thread>
#include "Consumer.h"

#pragma comment(lib,"Ws2_32.lib")
#pragma comment(lib,"Kernel32.lib")
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

DWORD ReadThread(LPVOID CompletionPortID)
{
	auto completionPort = CompletionPortID;
	while (true)
	{
		DWORD bytes = 0;
		LPOVERLAPPED IpOverlapped;
		ULONG_PTR socket;
		auto ret = GetQueuedCompletionStatus(completionPort, &bytes, &socket, static_cast<LPOVERLAPPED*>(&IpOverlapped), INFINITE);
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
		int ret1 = WSARecv(socket, &buffer, 1, &recvBytes,&flags, &overlapped,NULL);
		std::cout << buf << std::endl;
		send(socket, buf, 1024, 0);
		//Send(socket,buf);
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
	// 加载socket动态链接库
	WORD wVersionRequested = MAKEWORD(2, 2); // 请求2.2版本的WinSock库
	WSADATA wsaData;	// 接收Windows Socket的结构信息
	DWORD err = WSAStartup(wVersionRequested, &wsaData);

	if (0 != err) {	// 检查套接字库是否申请成功
		system("pause");
		return -1;
	}
	if (LOBYTE(wsaData.wVersion) != 2 || HIBYTE(wsaData.wVersion) != 2) {// 检查是否申请了所需版本的套接字库
		WSACleanup();
		system("pause");
		return -1;
	}
	return 0;
}


void WinServerSocket::InitReadThread(int threadNum)
{
	for (auto i = 0; i < threadNum; i++)
	{
		HANDLE ThreadHandle = CreateThread(NULL, 0, ReadThread, _completionPort, 0, NULL);
		if (NULL == ThreadHandle) {
			system("pause");
			return;
		}
		CloseHandle(ThreadHandle);
	}
}


int WinServerSocket::GetSysteamProcessNum() const
{
	SYSTEM_INFO mySysInfo;
	GetSystemInfo(&mySysInfo);
	return  mySysInfo.dwNumberOfProcessors;
}

void WinServerSocket::InitSocket()
{
	// 建立流式套接字
	_socket = socket(AF_INET, SOCK_STREAM, 0);

	// 绑定SOCKET到本机
	SOCKADDR_IN srvAddr;
	srvAddr.sin_addr.S_un.S_addr = htonl(INADDR_ANY);
	srvAddr.sin_family = AF_INET;
	srvAddr.sin_port = htons(_port);
	int bindResult = ::bind(_socket, (SOCKADDR*)&srvAddr, sizeof(SOCKADDR));
	if (SOCKET_ERROR == bindResult) {
		//TODO
		return ;
	}
	// 将SOCKET设置为监听模式
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
		int len = sizeof(SOCKADDR);
		auto acceptSocket = accept(_socket, (SOCKADDR*)&info,&len);
		if(acceptSocket == SOCKET_ERROR)
		{
			std::cerr << "Accept Socket Error: " << GetLastError() << std::endl;
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
		HANDLE ff1  = CreateIoCompletionPort(reinterpret_cast<HANDLE>(acceptSocket), _completionPort, acceptSocket, 0);



		char buf[1024] ;
		WSABUF buffer;
		buffer.len = 1024;
		buffer.buf = buf;
		DWORD recvBytes = 0;
		DWORD flags = 0;
		OVERLAPPED overlapped;
		ZeroMemory(&overlapped, sizeof(overlapped));

		int ret = WSARecv(acceptSocket, &buffer, 1, &recvBytes, &flags, &overlapped, NULL);
		if(WSA_IO_PENDING == WSAGetLastError())
		{
			std::cout << WSAGetLastError() << std::endl;
		}

	}
}
