#ifndef __CONNECTION_H__
#define __CONNECTION_H__
#include <memory>
#include "../3rd/asio/include/asio/ip/tcp.hpp"
#include "Consumer.h"
using namespace asio;

class Connection :public std::enable_shared_from_this<Connection>
{
public:
typedef std::shared_ptr<Connection>	Pointer;
	static Pointer Create(asio::io_context&ioContext)
	{
		return Pointer(new Connection(ioContext));
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


private:
	Connection(asio::io_context& ioContext)
	:_socket(ioContext),
	_consummer(_socket)
	{}
	void StartOperations()
	{
		if(_consummer.CanRead()&&!_readInProgress)
		{
			_readInProgress = true;
			_socket.async_wait(tcp::socket::wait_read,
				std::bind(&Connection::Read
					,shared_from_this()
					,asio::placeholders::error
					));
		}

		if(_consummer.CanWrite()&&!_writeInProgress)
		{
			_writeInProgress = true;
			_socket.async_wait(tcp::socket::wait_write,
				std::bind(&Connection::Write
					, shared_from_this()
					, asio::placeholders::error));
		}
	};
	void Read(asio::error_code ec)
	{
		_readInProgress = false;
		if(!ec)
		{
			_consummer.Read(ec);
		}
		Accident(ec);
	}

	void Write(asio::error_code ec)
	{
		_writeInProgress = false;
		if(!ec)
		{
			_consummer.Write(ec);
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
			_socket.close();
		}
	}

private:
	asio::ip::tcp::socket _socket;
	Consummer  _consummer;
	bool _readInProgress{false};
	bool _writeInProgress{false};

};




#endif