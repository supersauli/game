#ifndef __CONSUMMER_H__
#define __CONSUMMER_H__
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
	void Read(asio::error_code&ec,std::string&msg){
		_state = writing;
		auto len = _socket.read_some(asio::buffer(_data), ec);
		if (len >0)
		{
			_write_buffer = asio::buffer(_data, len);
			msg.resize(_write_buffer.size());

			for(int i=0;i< _write_buffer.size();i++)
			{
				msg[i]	= static_cast<const char*>(_write_buffer.data())[i];
			}
			
		}
	}
	bool CanWrite()const{
		return _state == writing;
	}

	void Write(asio::error_code& ec,std::string&msg)
	{
		if (!msg.empty())
		{
			for (int i = 0; i < msg.size(); i++)
			{
				_data[i] = msg[i];
			}
			_data[msg.size()] = '\n';
			//std::fread(&_data[0],1,size,file);
			_write_buffer = asio::buffer(_data);
			if (auto len = _socket.write_some(
				asio::buffer(_write_buffer), ec))
			{
				_write_buffer = _write_buffer + len;
				_state = asio::buffer_size(_write_buffer) > 0 ? writing : reading;
			}
		}
		else
		{
			_state = reading;
		}
	}
private:
	tcp::socket& _socket;
	enum {reading,writing} _state;
	std::array<char,128> _data{0};
	asio::const_buffer _write_buffer;
};

#endif
