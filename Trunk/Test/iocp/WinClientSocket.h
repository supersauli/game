#ifndef __WIN_CLIENT_SOCKET_H__
#define __WIN_CLIENT_SOCKET_H__
#include <string>
#include <utility>
#include <winsock2.h>
class WinClientSocket
{
public:
	WinClientSocket(std::string ip,int port):_ip(std::move(ip)),_port(port){}
	bool Start();
private:
	bool LoadLib();
	bool InitSocket();
	void Send(std::string msg);
	std::string _ip;
	int _port;
	SOCKET _socket;
};



#endif
