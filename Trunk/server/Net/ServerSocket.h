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
		_acceptor.async_accept(
		[this](std::error_code ec, tcp::socket socket)
		{
			if (!ec)
			{
				std::make_shared<Connection>(std::move(socket))->Start();
			}
			Start();

		});
	}
	asio::io_context _ioContext;
	tcp::acceptor _acceptor;
};





#endif
