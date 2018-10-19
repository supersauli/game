//#ifndef	 __CLIENT_SOCKET_H___
//#define  __CLIENT_SOCKET_H___
//#include "../3rd/asio/include/asio.hpp"
//#include "MessageMange.h"
////#include "Consumer.h"
//using namespace asio;
//class ClientSocket
//{
//public:
//	ClientSocket(const char* ip,int port)
//	:_socket(_ioContext)
//	//_consummer(_socket)
//	{
//		_connectIP = ip;
//		_port = Util::AsString(port);
//		
//	}
//	void Start()
//	{
//		ip::tcp::resolver resolver(_ioContext);
//		asio::connect(_socket, resolver.resolve(_connectIP, _port));
//		auto connection = std::make_shared<Connection>(std::move(_socket));
//		MessageManage::GetInstance().AddConnection(connection);
//		connection->Start();
//	}
//	
//private:
//	asio::io_context _ioContext;
//	ip::tcp::socket _socket;
//	std::string _connectIP;
//	std::string _port;
//};
//
//
//
//
//
//#endif