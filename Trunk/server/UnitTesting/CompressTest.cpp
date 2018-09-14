#include <iostream>
#include "../Tool/Compress.h"
using namespace std;
int main1()
{
	string str = "ffsdfafdsafsdfsdfdsfsdafsafdsf";
	cout << str.c_str() << endl;
	char dest[256]= {0};
	Tool::Compress(str.c_str(),dest,str.size(),256);
	cout << dest << endl;
	char dest2[256]={0};
	Tool::Decompress(dest,dest2,256,256);
	cout << dest2 << endl;
	return 0;
}
