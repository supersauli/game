#ifndef __CONNECTION_H__
#define __CONNECTION_H__
#include <memory>
#include "../3rd/asio/include/asio.hpp"
#include "Consumer.h"
#include <iostream>
static int global = 0;
using namespace asio;
class Connection :public std::enable_shared_from_this<Connection>
{
public:
	
	Connection(tcp::socket socket)
		: _socket(std::move(socket)),
		 _consummer(_socket)
	{
		_uid = global++;
	}
	
	asio::ip::tcp::socket& GetSocket()
	{
		return _socket;
	};

	void Start()
	{
		_socket.non_blocking(true);
		StartOperations();
	}

	void SendMsg(std::string&msg)
	{
		_msg = msg;

			/*_socket.async_wait(tcp::socket::wait_write,
				std::bind(&Connection::Write
					, shared_from_this()
					, std::placeholders::_1));*/


		asio::error_code ec;
		Write(ec);
	}

private:
	
	void StartOperations()
	{
		/*if(_consummer.CanRead()&&!_readInProgress)
		{
			_readInProgress = true;*/
			_socket.async_wait(tcp::socket::wait_read,
				std::bind(&Connection::Read
					,shared_from_this()
					,std::placeholders::_1
					));
		//}

	/*	if(_consummer.CanWrite()&&!_writeInProgress)
		{
			_writeInProgress = true;
			_socket.async_wait(tcp::socket::wait_write,
				std::bind(&Connection::Write
					, shared_from_this()
					, std::placeholders::_1));
		
		}*/
	};
	void Read(asio::error_code ec)
	{
		_readInProgress = false;
		if(!ec)
		{
			std::string msg;
			_consummer.Read(ec,msg);
			_recvCB(_uid,msg);
		}
		Accident(ec);
	}

	void Write(asio::error_code ec)
	{
		_writeInProgress = false;
		if(!ec&&!_msg.empty())
		{
			_consummer.Write(ec, _msg);
		}
		Accident(ec);
	}

	void Accident(asio::error_code ec)
	{
		if(!ec || ec == asio::error::would_block)
		{

			StartOperations();
		}
		else
		{
			std::cout << ec.message().c_str() << std::endl;;
			CloseSocket();
			
		}
	}
	void CloseSocket()
	{
		asio::error_code ec;
		_socket.shutdown(tcp::socket::shutdown_send,ec);
		_socket.close(ec);
	}
public:
	std::function<void(DWORD, std::string&)> _recvCB;
private:
	asio::ip::tcp::socket _socket;
	Consummer  _consummer;
	bool _readInProgress{false};
	bool _writeInProgress{false};
	std::string _msg;
public:
	DWORD _uid{0};

};




#endif