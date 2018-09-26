#ifndef	 __CLIENT_SOCKET_H___
#define  __CLIENT_SOCKET_H___
#include "../3rd/asio/include/asio.hpp"
#include "Consumer.h"
using namespace asio;
class ClientSocket
{
public:
	ClientSocket(const char* ip,const char* port)
	:_socket(_ioContext),
	_consummer(_socket)
	{
		_connectIP = ip;
		_port = port;
		
	}
	void Start()
	{
		ip::tcp::resolver resolver(_ioContext);
		asio::connect(_socket, resolver.resolve(_connectIP, _port));
	}
	void Write(std::string&str)
	{
		asio::error_code ec;
		_consummer.Write(str);
		_consummer.Write(ec);
	};
	void Read()
	{
		asio::error_code ec;
		_consummer.Read(ec);
	}
private:
	asio::io_context _ioContext;
	ip::tcp::socket _socket;
	Consummer  _consummer;
	std::string _connectIP;
	std::string _port;
};





#endif