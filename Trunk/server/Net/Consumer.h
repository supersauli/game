#ifndef _CONSUMMER_H__
#define _CONSUMMER_H__
#include "../3rd/asio/include/asio.hpp"
#include <iostream>
using namespace asio;
using namespace asio::ip;
class Consummer
{
public:
	Consummer(tcp::socket& socket)
		:_socket(socket),
		 _state(reading)
	{
	}

	bool CanRead()const{
		return _state == reading;
	}
	void Read(asio::error_code&ec){
		_state = writing;
		auto len = _socket.read_some(asio::buffer(_data), ec);
		if (len >0)
		{
			_write_buffer = asio::buffer(_data, len);
			std::cout << _data.data() << std::endl;
			char chat[256]={0};
			std::cin >> chat;
			std::string text(chat);
			Write(text);
		}
	}
	bool CanWrite()const{
		return _state == writing;
	}
	void Write(std::string&str)
	{
		_message.push_back(str);
	}


	void Write(asio::error_code& ec)
	{
		if(_message.empty())
		{
			_state = reading;
		}
		
		for (auto it : _message)
		{
			for (int i = 0; i < it.size();i++)
			{
				_data[i] = it[i];
			}
			_data[it.size()] = '\n';

			//std::fread(&_data[0],1,size,file);
			_write_buffer = asio::buffer(_data);
			if (auto len = _socket.write_some(
				asio::buffer(_write_buffer), ec))
			{
				_write_buffer = _write_buffer + len;
				_state = asio::buffer_size(_write_buffer) > 0 ? writing : reading;
			}
			
		}
		_message.clear();
	}
private:
	tcp::socket& _socket;
	enum {reading,writing} _state;
	std::array<char,128> _data{0};
	asio::const_buffer _write_buffer;
	std::vector<std::string> _message;
};

#endif
