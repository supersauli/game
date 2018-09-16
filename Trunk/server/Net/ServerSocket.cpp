#include "ServerSocket.h"
#include <Winsock2.h>
#include <winsock.h>
#include <vector>
#include <iostream>
#include <thread>
#pragma comment(lib,"Ws2_32.lib")
#pragma comment(lib,"Kernel32.lib")
typedef struct
{
	SOCKET _socket;
	SOCKADDR_STORAGE  _clientAdder;
}CLIENT_DATA_INFO, *LPCLIENT_DATA_INFO;

std::vector<CLIENT_DATA_INFO> clientGroup;

bool ServerSocket::Init()
{
	const auto versionRequested = MAKEWORD(2,2);
	WSADATA wsaData;
	const auto ret = WSAStartup(versionRequested, &wsaData);
	if(ret != 0)
	{
		std::cerr << "Request Windows Socket Library Error!\n"<<std::endl;
		system("pause");
		return false;
	}
	_completionPort = CreateIoCompletionPort(INVALID_HANDLE_VALUE, nullptr,0,0);
	if(_completionPort == nullptr)
	{
		std::cerr << "CreateIoCompletionPort failed. Error:" << GetLastError() << std::endl;
		system("pause");
		return false;
	}



	return true;
}

void ServerSocket::ServerReadThread()
{
	while(true)
	{
		DWORD byteTransfeered;
		LPCLIENT_DATA_INFO PerHandleData = NULL;
		/*auto ret = GetQueuedCompletionStatus(_completionPort,
			&byteTransfeered, 0, NULL, INFINITE);*/
	}










}

bool ServerSocket::InitPthread()
{
	SYSTEM_INFO sysInfo;
	GetSystemInfo(&sysInfo);

	for(auto i =0;i < (sysInfo.dwNumberOfProcessors*2);++i)
	{
		std::thread readThread(&ServerSocket::ServerReadThread);
		readThread.join();
	}



	return true;
}

bool ServerSocket::CreateSocket()
{
	int bindPort = 9090;
	_listenSocket = socket(AF_INET,SOCK_STREAM,0);
	_srvAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	_srvAddr.sin_family = AF_INET;
	_srvAddr.sin_port = htons(bindPort);
	return true;
}

bool ServerSocket::Bind()
{
	const auto ret = ::bind(_listenSocket,reinterpret_cast<struct sockaddr*>(&_srvAddr),sizeof(_srvAddr));
	if(ret < 0)
	{
		std::cerr << "Bind failed. Error:" << GetLastError() << std::endl;
		system("pause");
		return false;
	}

	return true;
}
void SendInfo()
{
	
}
bool ServerSocket::Listen()
{
	const auto ret = listen(_listenSocket,24);
	if(ret <0)
	{
		std::cerr << "Listen failed. Error: " << GetLastError() << std::endl;
		system("pause");
		return false;
	}
	return true;
}

void ServerSocket::Run()
{
	while(true)
	{
		sockaddr_in addr{};
		int len = sizeof(addr);
		const int retSocket = ::accept(_listenSocket,reinterpret_cast<sockaddr*>(&addr),&len);
		if(SOCKET_ERROR  > retSocket)
		{
			std::cerr << "Accept Socket Error: " << GetLastError() << std::endl;
			system("pause");
			continue;
		}

		CreateIoCompletionPort(reinterpret_cast<void*>(retSocket), _completionPort, 0, 0);

		WSABUF dataBuf ;
		DWORD recvBytes;
		DWORD Flags = 0;
		OVERLAPPED overlapped;
		WSARecv(_listenSocket, &dataBuf, 1, &recvBytes, &Flags, &overlapped, nullptr);
	}


}
