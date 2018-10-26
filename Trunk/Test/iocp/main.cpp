#include "WinServerSocket.h"

int main()
{
	WinServerSocket server("192.168.1.114",12345);
	server.Start();
	while (true)
	{
		Sleep(1000);
	}
	return 0;
}
