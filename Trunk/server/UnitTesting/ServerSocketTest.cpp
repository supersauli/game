
#include "../Net/ServerSocket.h"
#include <iostream>
#include "../Net/ClientSocket.h"
int port = 1234;
void StartServer()
{
	ServerSocket s(port);
	s.Run();
}
void StartClient()
{

	
}

int main()
{
	auto th1 = std::thread(StartServer);
	
	//auto th2 = std::thread(StartClient);
	th1.join();
	//th2.join();
	
	return 0;
}



//
// async_tcp_echo_server.cpp
// ~~~~~~~~~~~~~~~~~~~~~~~~~
//
// Copyright (c) 2003-2018 Christopher M. Kohlhoff (chris at kohlhoff dot com)
//
// Distributed under the std Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at http://www.std.org/LICENSE_1_0.txt)
//

#include <cstdlib>
#include <iostream>
#include "../3rd/asio/include/asio.hpp"

using asio::ip::tcp;
//class session
//{
//public:
//	session(asio::io_context& io_context)
//		: socket_(io_context)
//	{
//	}
//
//	tcp::socket& socket()
//	{
//		return socket_;
//	}
//
//	void start()
//	{
//		socket_.async_read_some(asio::buffer(data_, max_length),
//			std::bind(&session::handle_read, this,
//				std::placeholders::_1,
//				std::placeholders::_2));
//	}
//
//private:
//	void handle_read(const asio::error_code& error,
//		size_t bytes_transferred)
//	{
//		if (!error)
//		{
//			asio::async_write(socket_,
//				asio::buffer(data_, bytes_transferred),
//				std::bind(&session::handle_write, this,
//					std::placeholders::_1));
//		}
//		else
//		{
//			delete this;
//		}
//	}
//
//	void handle_write(const asio::error_code& error)
//	{
//		if (!error)
//		{
//			socket_.async_read_some(asio::buffer(data_, max_length),
//				std::bind(&session::handle_read, this,
//					std::placeholders::_1,
//					std::placeholders::_2));
//		}
//		else
//		{
//			delete this;
//		}
//	}
//
//	tcp::socket socket_;
//	enum { max_length = 1024 };
//	char data_[max_length];
//};
//
//class server
//{
//public:
//	server(asio::io_context& io_context, short port)
//		: io_context_(io_context),
//		acceptor_(io_context, tcp::endpoint(tcp::v4(), port))
//	{
//		start_accept();
//	}
//
//private:
//	void start_accept()
//	{
//		session* new_session = new session(io_context_);
//		acceptor_.async_accept(new_session->socket(),
//			std::bind(&server::handle_accept, this, new_session,
//				std::placeholders::_1));
//	}
//
//	void handle_accept(session* new_session,
//		const asio::error_code& error)
//	{
//		if (!error)
//		{
//			new_session->start();
//		}
//		else
//		{
//			delete new_session;
//		}
//
//		start_accept();
//	}
//
//	asio::io_context& io_context_;
//	tcp::acceptor acceptor_;
//};
//
//int main(int argc, char* argv[])
//{
//	try
//	{
//		/*if (argc != 2)
//		{
//			std::cerr << "Usage: async_tcp_echo_server <port>\n";
//			return 1;
//		}*/
//
//		asio::io_context io_context;
//
//		using namespace std; // For atoi.
//		server s(io_context, atoi("1234"));
//
//		io_context.run();
//	}
//	catch (std::exception& e)
//	{
//		std::cerr << "Exception: " << e.what() << "\n";
//	}
//
//	return 0;
//}
//
//
//
//

//
// third_party_lib.cpp
// ~~~~~~~~~~~~~~~~~~~
//
// Copyright (c) 2003-2018 Christopher M. Kohlhoff (chris at kohlhoff dot com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at http://www.boost.org/LICENSE_1_0.txt)
//
#if 0
#include <asio.hpp>
#include <array>
#include <iostream>
#include <memory>

using asio::ip::tcp;

namespace third_party_lib {

	// Simulation of a third party library that wants to perform read and write
	// operations directly on a socket. It needs to be polled to determine whether
	// it requires a read or write operation, and notified when the socket is ready
	// for reading or writing.
	class session
	{
	public:
		session(tcp::socket& socket)
			: socket_(socket)
		{
		}

		// Returns true if the third party library wants to be notified when the
		// socket is ready for reading.
		bool want_read() const
		{
			return state_ == reading;
		}

		// Notify that third party library that it should perform its read operation.
		void do_read(std::error_code& ec)
		{
			if (std::size_t len = socket_.read_some(asio::buffer(data_), ec))
			{
				write_buffer_ = asio::buffer(data_, len);
				state_ = writing;
			}
		}

		// Returns true if the third party library wants to be notified when the
		// socket is ready for writing.
		bool want_write() const
		{
			return state_ == writing;
		}

		// Notify that third party library that it should perform its write operation.
		void do_write(std::error_code& ec)
		{
			if (std::size_t len = socket_.write_some(
				asio::buffer(write_buffer_), ec))
			{
				write_buffer_ = write_buffer_ + len;
				state_ = asio::buffer_size(write_buffer_) > 0 ? writing : reading;
			}
		}

	private:
		tcp::socket& socket_;
		enum { reading, writing } state_ = reading;
		std::array<char, 128> data_;
		asio::const_buffer write_buffer_;
	};

} // namespace third_party_lib

// The glue between asio's sockets and the third party library.
class connection
	: public std::enable_shared_from_this<connection>
{
public:
	connection(tcp::socket socket)
		: socket_(std::move(socket))
	{
	}

	void start()
	{
		// Put the socket into non-blocking mode.
		socket_.non_blocking(true);

		do_operations();
	}

private:
	void do_operations()
	{
		auto self(shared_from_this());

		// Start a read operation if the third party library wants one.
		if (session_impl_.want_read() && !read_in_progress_)
		{
			read_in_progress_ = true;
			socket_.async_wait(tcp::socket::wait_read,
				[this, self](std::error_code ec)
			{
				read_in_progress_ = false;

				// Notify third party library that it can perform a read.
				if (!ec)
					session_impl_.do_read(ec);

				// The third party library successfully performed a read on the
				// socket. Start new read or write operations based on what it now
				// wants.
				if (!ec || ec == asio::error::would_block)
					do_operations();

				// Otherwise, an error occurred. Closing the socket cancels any
				// outstanding asynchronous read or write operations. The
				// connection object will be destroyed automatically once those
				// outstanding operations complete.
				else
					socket_.close();
			});
		}

		// Start a write operation if the third party library wants one.
		if (session_impl_.want_write() && !write_in_progress_)
		{
			write_in_progress_ = true;
			socket_.async_wait(tcp::socket::wait_write,
				[this, self](std::error_code ec)
			{
				write_in_progress_ = false;

				// Notify third party library that it can perform a write.
				if (!ec)
					session_impl_.do_write(ec);

				// The third party library successfully performed a write on the
				// socket. Start new read or write operations based on what it now
				// wants.
				if (!ec || ec == asio::error::would_block)
					do_operations();

				// Otherwise, an error occurred. Closing the socket cancels any
				// outstanding asynchronous read or write operations. The
				// connection object will be destroyed automatically once those
				// outstanding operations complete.
				else
					socket_.close();
			});
		}
	}

private:
	tcp::socket socket_;
	third_party_lib::session session_impl_{ socket_ };
	bool read_in_progress_ = false;
	bool write_in_progress_ = false;
};

class server
{
public:
	server(asio::io_context& io_context, unsigned short port)
		: acceptor_(io_context, { tcp::v4(), port })
	{
		do_accept();
	}

private:
	void do_accept()
	{
		acceptor_.async_accept(
			[this](std::error_code ec, tcp::socket socket)
		{
			if (!ec)
			{
				std::make_shared<connection>(std::move(socket))->start();
			}

			do_accept();
		});
	}

	tcp::acceptor acceptor_;
};

int main(int argc, char* argv[])
{
	try
	{
		/*if (argc != 2)
		{
			std::cerr << "Usage: third_party_lib <port>\n";
			return 1;
		}*/

		asio::io_context io_context;

		server s(io_context, std::atoi("1234"));

		io_context.run();
	}
	catch (std::exception& e)
	{
		std::cerr << "Exception: " << e.what() << "\n";
	}

	return 0;
}



#endif










