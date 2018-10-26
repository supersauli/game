 #include "WinClientSocket.h"
#pragma comment(lib,"ws2_32.lib")

bool WinClientSocket::Start()
{
	if(!LoadLib())
	{
		return false;
	}
	if(!InitSocket())
	{
		return false;
	}


	return true;
}

bool WinClientSocket::LoadLib()
{
	WSADATA wsaData;
	int nResult = WSAStartup(MAKEWORD(2, 2), &wsaData);

	if (NO_ERROR != nResult)
	{
		//TODO
		return false; // 错误
	}

	return true;
}

bool WinClientSocket::InitSocket()
{
	_socket = socket(AF_INET,SOCK_STREAM,IPPROTO_TCP);
	if(_socket == INVALID_SOCKET)
	{
		return false;
	}
	struct sockaddr_in ServerAddress;
	struct hostent *Server;
	ZeroMemory((char *)&ServerAddress, sizeof(ServerAddress));
	ServerAddress.sin_family = AF_INET;
	ServerAddress.sin_addr.s_addr = inet_addr(_ip.c_str());
	ServerAddress.sin_port = htons(_port);

	// 开始连接服务器
	if (SOCKET_ERROR == connect(_socket, reinterpret_cast<const struct sockaddr *>(&ServerAddress), sizeof(ServerAddress)))
	{
		closesocket(_socket);
	
		return false;
	}
}

void WinClientSocket::Send(std::string msg)
{
	send(_socket, msg.c_str(), msg.size(),0);
}
