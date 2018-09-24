#ifndef __SERVER_SOCKET_H__
#define __SERVER_SOCKET_H__
#include "../3rd/asio/include/asio.hpp"
#include "Connection.h"

class ServerSocket
{
public:
	ServerSocket(unsigned short port)
		:_acceptor(_ioContext,tcp::endpoint(tcp::v4(),port))
	{
		Start();
	}

private:
	void Start()
	{
		auto newConnection = Connection::Create(_acceptor.get_executor()().context());;
		_acceptor.async_accept(newConnection->_socket(),
			std::bind(&ServerSocket::HandAccept, this, newConnection,
				asio::placeholders::error));
	}
	void HandleAccept(Connection::Pointer &newConnection,
		const asio::error_code&error)
	{
		if(!error)
		{
			newConnection->Start();
		}
	}


	asio::io_context _ioContext;
	tcp::acceptor _acceptor;
};





#endif
