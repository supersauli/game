#ifndef __SERVER_SOCKET_H__
#define __SERVER_SOCKET_H__
#include "../3rd/asio/include/asio.hpp"
#include "Connection.h"
using namespace asio;
class ServerSocket
{
public:
	ServerSocket(unsigned short port)
		:_acceptor(_ioContext,tcp::endpoint(tcp::v4(),port))
	{
		Start();
	}
	void Run()
	{
		_ioContext.run();
	}

private:
	void Start()
	{
		auto newConnection = Connection::Create(_acceptor.get_executor().context());;
		_acceptor.async_accept(newConnection->GetSocket(),
			std::bind(&ServerSocket::HandleAccept, this, newConnection,
				std::placeholders::_1));
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
