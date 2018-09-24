#ifndef _CONSUMMER_H__
#define _CONSUMMER_H__
#include "../3rd/asio/include/asio.hpp"
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
		if(auto len = _socket.read_some(asio::buffer(_data),ec))
		{
			_write_buffer = asio::buffer(_data,len);
			_state = writing;
		}
	}
	bool CanWrite()const{
		return _state == writing;
	}
	void Write(asio::error_code& ec)
	{
			if(auto len = _socket.write_some(
				asio::buffer(_write_buffer),ec))
			{
				_write_buffer = _write_buffer + len;
				_state = asio::buffer_size(_write_buffer)>0?writing:reading;
			}
	}
	

private:
	tcp::socket& _socket;
	enum {reading,writing} _state;
	std::array<char, 2024> _data;
	asio::const_buffer _write_buffer;
};

#endif
