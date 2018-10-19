//#ifndef __SERVER_SOCKET_H__
//#define __SERVER_SOCKET_H__
//#include "../3rd/asio/include/asio.hpp"
//#include "Connection.h"
//#include "MessageMange.h"
//using namespace asio;
//class ServerSocket
//{
//public:
//	ServerSocket(unsigned short port)
//		:_acceptor(_ioContext,tcp::endpoint(tcp::v4(),port))
//	{
//		Accept();
//	}
//	void Run()
//	{
//		_ioContext.run();
//	}
//
//private:
//	void Accept()
//	{
//		_acceptor.async_accept(
//		[this](std::error_code ec, tcp::socket socket)
//		{
//			if (!ec)
//			{
//				auto connection = std::make_shared<Connection>(std::move(socket));
//				MessageManage::GetInstance().AddConnection(connection);
//				connection->Start();
//			}
//			Accept();
//
//		});
//	}
//	asio::io_context _ioContext;
//	ip::tcp::acceptor _acceptor;
//};
//
//
//
//
//
//#endif
