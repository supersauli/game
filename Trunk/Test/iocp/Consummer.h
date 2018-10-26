#ifndef __CONSUMMER_H__
#define __CONSUMMER_H__
#include <WinSock2.h>

class Consummer
{
public:
	void Close()
	{
		closesocket(_socket);
	};


public:
	SOCKET _socket;
	SOCKADDR_IN _addr;

	
};




#endif