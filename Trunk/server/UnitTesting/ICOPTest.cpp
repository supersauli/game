#include <thread>
#include "ICOPClient.h"
#include "IOCPServer.h"
int main()
{
	auto th1 = std::thread(&IcopServer);
	auto th2 = std::thread(&IcopClient);
	th1.join();
	th2.join();
}
