#ifndef __WIN_SERVER_SOCKET_H__
#define __WIN_SERVER_SOCKET_H__
#include<WinSock2.h>
#include<Windows.h>



#pragma comment(lib,"Ws2_32.lib")
#pragma comment(lib,"Kernel32.lib")
//TODO ½ûÓÃ¿½±´
class WinServerSocket
{
public:
	WinServerSocket(int port):_port(port){}
	void SetPort(int port);
	int GetPort();
	void Run(int threadNum = 1);
	DWORD WINAPI ReadThread(LPVOID CompletionPortID);
	DWORD  Send(int socket,const char* buf);
private:
	int InitData();
	void InitReadThread(int threadNum);
	int GetSysteamProcessNum();
	void InitSocket();
	void DoAccept();
	HANDLE _completionPort;
	int _port;
	SOCKET _socket;
};






#endif