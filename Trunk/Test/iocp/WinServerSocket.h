#ifndef __WINSERVERSOKET_H__
#define __WINSERVERSOKET_H__
#include <WinSock2.h>
#include <MSWSock.h>
#include <string>

#include "WinServerSocketDefine.h"
#include <vector>

#pragma comment(lib,"ws2_32.lib")


class Consummer;

class WinServerSocket
{
public:
	WinServerSocket(int port):_port(port){};
	WinServerSocket(std::string ip, int port) :_ip(ip),_port(port) {};
	bool Start();
	void Stop();

private:
	bool LoadLib();
	bool InitOCompletionPort();
	bool InitWorkerThread();
	bool InitSocket();
	bool InitEvent();
	bool PostAccept(PER_IO_CONTEXT*pAcceptIoContext);
	bool PostRecv(PER_IO_CONTEXT* pIoContext);
	bool DoAccept(PER_IO_CONTEXT* pIoContext);
	bool DoRecv(PER_IO_CONTEXT* pIoContext);
	bool DoSend(PER_IO_CONTEXT* pIoContext);
	bool InitIocp(Consummer*pConsummer);
	int  GetPorcessorsNum() const;
	bool GetLocalIP(std::string&ip) const;
	static DWORD WINAPI WorkerThread(LPVOID lpParam);
	void CloseSocket(Consummer*pIoContext);
	void ShowError();
	std::string _ip;
	int _port;
	SOCKET _socket;
	HANDLE _comletionPort;
	LPFN_ACCEPTEX              _acceptEx{nullptr};
	LPFN_GETACCEPTEXSOCKADDRS    _acceptExSockAddrs{nullptr};
	int _threadNums{0};
	HANDLE _handleEvent;
	std::vector<HANDLE>	_workerThread;
};

#endif