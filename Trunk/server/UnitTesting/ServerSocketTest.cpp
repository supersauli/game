#include "../Net/ServerSocket.h"

int main()
{
	try
	{

		ServerSocket s(2333);
		s.Run();
		
	}
	catch (std::exception& e)
	{
		//std::cerr << "Exception: " << e.what() << "\n";
	}
	return 0;
}
