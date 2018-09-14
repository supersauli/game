#ifndef __SERVER_SOCKET_H__
#define __SERVER_SOCKET_H__
#include <Winsock2.h>

class ServerSocket
{
public:
	bool Init();
private:
	bool InitPthread();
	bool CreateSocket();
	bool Bind();
	bool Listen();
	void Run();
	static void ServerReadThread();


private:
	sockaddr_in _srvAddr{};
	int _listenSocket{0};
	void * _completionPort{ nullptr };
};




#endif
