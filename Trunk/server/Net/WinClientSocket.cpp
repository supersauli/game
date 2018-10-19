#include "WinClientSocket.h"
#include <iostream>
#pragma comment(lib, "Ws2_32.lib")	
void WinClientSocket::Run()
{
	_socket = socket(AF_INET, SOCK_STREAM, 0);
	if(_socket == INVALID_SOCKET)
	{
		//TODO
		return;
	}

	SOCKADDR_IN addr;
	addr.sin_addr.S_un.S_addr = inet_addr(_ip.c_str());
	addr.sin_family = AF_INET;
	addr.sin_port = htons(_port);
	int ret = connect(_socket, reinterpret_cast<SOCKADDR*>(&addr), sizeof(SOCKADDR));
	if(ret == SOCKET_ERROR){
		return;
	}

	while(true)
	{
		Send();
		Recv();
	}
}

void WinClientSocket::Send()
{
	std::string buf;
	getline(std::cin, buf);
	if(buf.empty())
	{
		return;
	}
	send(_socket,buf.c_str(),100,0);

}

void WinClientSocket::Recv()
{
	char buf[1024] = {0};
	recv(_socket,buf,1000,0);
	printf("client recv %s",buf);
}
