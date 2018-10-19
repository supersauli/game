#ifndef __WIN_CLIENT_SOCKET_H__
#define __WIN_CLIENT_SOCKET_H__
#include <winsock.h>
#include <string>

class WinClientSocket
{

public:
	WinClientSocket(std::string ip,int port):_ip(ip),_port(port)
	{}
	void Run();
private:
	void Send();
	void Recv();
	
	SOCKET _socket;
	std::string _ip;
	int _port{0};
};



#endif
