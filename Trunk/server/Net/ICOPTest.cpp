#include <thread>
#include "ICOPClient.h"
#include "IOCPServer.h"
#include "gtest/gtest.h"
#include "WinServerSocket.h"
#include "WinClientSocket.h"
int port = 2333;
void ServerSocket()
{
	WinServerSocket socket(port);
	socket.Run(1);
	
}
void ClientSocket()
{
	WinClientSocket client("127.0.0.1",port);
	client.Run();
}

int TestSocket()
{
#if 1
	auto th1 = std::thread(&ServerSocket);
	auto th2 = std::thread(&ClientSocket);
#else
	auto th1 = std::thread(&IcopServer);
	auto th2 = std::thread(&IcopClient);
#endif
	th1.join();
	th2.join();
	return 0;
}
TEST(TestSocket, case1)
{
	EXPECT_LT(0, TestSocket());
}