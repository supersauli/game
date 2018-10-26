//#include "../Net/ServerSocket.h"
////#include "../Net/ClientSocket.h"
//#include <thread>
//#include "../Net/ClientSocket.h"
//#include "../proto/Gate.pb.h"
//using namespace std;
//int port = 1200;
//static int global1 = 0;
//static void func1(DWORD id, const void*args)
//{
//	auto rec = (GateServer::NewGate*)(args);
//	if(rec == nullptr)
//	{
//		return;
//	}
//	global1++;
//	//cout << rec->dwid() << endl;
//}
//void StartServer()
//{
//	try
//	{
//		MessageManage::GetInstance().AddMessageCallBack("GateServer.NewGate", func1);
//		ServerSocket s(port);
//		s.Run();
//	}
//	catch (std::exception& e)
//	{
//		std::cerr << "Exception: " << e.what() << "\n";
//	}
//
//}
//void func2(DWORD id, const void*args)
//{
//	cout << id << endl;
//}
//
//void CreateClient()
//{
//	try
//	{
//		MessageManage::GetInstance().AddMessageCallBack("GateServer.UserLogin", func2);
//		ClientSocket c("127.0.0.1", port);
//		while (true)
//		{
//			c.Start();
//			Sleep(1000);
//		}
//	}
//	catch (std::exception& e)
//	{
//		std::cerr << "Exception: " << e.what() << "\n";
//	}
//
//
//}
//void StartClient()
//{
//	auto th1 = std::thread(CreateClient);
//	int i = 0;
//	auto beginTime = ::time(NULL);
//	while(true)
//	{
//		if (::time(NULL) - beginTime<10)
//		{
//			GateServer::NewGate msg;
//			msg.set_dwid(i++);
//			msg.set_strip("127");
//			MessageManage::GetInstance().SendMessage(0, msg);
//		}
//		else
//		{
//			cout << global1 << endl;
//			break;
//		}
//
//	}
//
//	th1.join();
//}
//class Solution {
//public:
//	bool isLeft(const char& symbol) {
//		switch (symbol)
//		{
//		case '(':
//		case '[':
//		case '{':
//			return true;
//		default:
//			return false;
//		}
//		return false;
//	}
//	const char getLeftSymbol(const char& symbol) {
//		switch (symbol) {
//		case ')':
//			return '(';
//		case ']':
//			return '[';
//		case '}':
//			return '{';
//		default:
//			return ' ';
//
//		}
//		return ' ';
//	}
//	bool isValid(string s) {
//		std::vector<char> _leftSymbol;
//		const auto size = s.size();
//		if (size % 2 != 0) {
//			return false;
//		}
//
//		for (auto i = 0; i < size; ++i) {
//			char sym = s[i];
//			if (isLeft(sym)) {
//				_leftSymbol.push_back(sym);
//			}
//			else {
//				if (_leftSymbol.empty()) {
//					return false;
//				}
//				if (*_leftSymbol.rbegin() != getLeftSymbol(sym)) {
//					return false;
//				}
//				_leftSymbol.pop_back();
//			}
//		}
//
//
//		return _leftSymbol.empty();
//	}
//
//	int longestValidParentheses(string s) {
//
//		int ret = 0;
//		const auto size = s.size();
//		for (auto i = 0; i < size; ++i) {
//			for (auto j = 1; j <= size - i; j++)
//			{
//				if (isValid(s.substr(i,j))) {
//					ret = max(ret, j);
//				}
//			}
//		}
//
//		return ret;
//	}
//};
//
//
//int main3()
//{
//	/*Solution s;
//	cout<<s.longestValidParentheses("(()")<<endl;
//	cout<<s.longestValidParentheses("(())")<<endl;
//	cout<<s.longestValidParentheses("())")<<endl;
//	cout<<s.longestValidParentheses("())")<<endl;
//	cout<<s.longestValidParentheses("()(())")<<endl;*/
//	auto th6 = std::thread(StartServer);
//	auto th1 = std::thread(StartClient);
//	auto th2 = std::thread(StartClient);
//	//auto th3 = std::thread(StartClient);
//	//auto th4 = std::thread(StartClient);
//	//auto th5 = std::thread(StartClient);
//	th1.join();
//	th2.join();
//	//th3.join();
//	//th4.join();
//	//th5.join();
//	th6.join();
//	
//	system("pause");
//	return 0;
//}
//
