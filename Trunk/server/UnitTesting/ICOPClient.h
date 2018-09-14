// IOCP_TCPIP_Socket_Client.cpp
#pragma once
#include <iostream>
#include <cstdio>
#include <string>
#include <cstring>
#include <winsock2.h>
#include <Windows.h>

using namespace std;

#pragma comment(lib, "Ws2_32.lib")		// Socket������õĶ�̬���ӿ�

static SOCKET sockClient;		// ���ӳɹ�����׽���
static HANDLE bufferMutex;		// �����ܻ���ɹ�����ͨ�ŵ��ź������
const int DefaultPort1 = 6000;
#pragma warning(disable:4996) 


inline DWORD  SendMessageThread(LPVOID IpParameter)
{
	while (1) {
		string talk;
		getline(cin, talk);
		WaitForSingleObject(bufferMutex, INFINITE);		// P����Դδ��ռ�ã�  
		if ("quit" == talk) {
			talk.push_back('\0');
			send(sockClient, talk.c_str(), 200, 0);
			break;
		}
		else {
			talk.append("\n");
		}
		printf("\nI Say:(\"quit\"to exit):");
		cout << talk;
		send(sockClient, talk.c_str(), 200, 0);	// ������Ϣ
		ReleaseSemaphore(bufferMutex, 1, NULL);		// V����Դռ����ϣ� 
	}
	return 0;
}


inline DWORD  ReceiveMessageThread(LPVOID IpParameter)
{
	while (1) {
		char recvBuf[300];
		recv(sockClient, recvBuf, 200, 0);
		WaitForSingleObject(bufferMutex, INFINITE);		// P����Դδ��ռ�ã�  

		printf("%s Says: %s", "Server", recvBuf);		// ������Ϣ

		ReleaseSemaphore(bufferMutex, 1, NULL);		// V����Դռ����ϣ� 
	}
	return 0;
}

inline int IcopClient()
{
	// ����socket��̬���ӿ�(dll)
	WORD wVersionRequested;
	WSADATA wsaData;	// ��ṹ�����ڽ���Wjndows Socket�Ľṹ��Ϣ��
	wVersionRequested = MAKEWORD(2, 2);	// ����2.2�汾��WinSock��
	int err = WSAStartup(wVersionRequested, &wsaData);
	if (err != 0) {	// ����ֵΪ���ʱ���Ǳ�ʾ�ɹ�����WSAStartup
		return -1;
	}
	if (LOBYTE(wsaData.wVersion) != 2 || HIBYTE(wsaData.wVersion) != 2) { // ���汾���Ƿ���ȷ
		WSACleanup();
		return -1;
	}

	// ����socket������������ʽ�׽��֣������׽��ֺ�sockClient
	sockClient = socket(AF_INET, SOCK_STREAM, 0);
	if (sockClient == INVALID_SOCKET) {
		printf("Error at socket():%ld\n", WSAGetLastError());
		WSACleanup();
		return -1;
	}

	// ���׽���sockClient��Զ����������
		// int connect( SOCKET s,  const struct sockaddr* name,  int namelen);
		// ��һ����������Ҫ�������Ӳ������׽���
		// �ڶ����������趨����Ҫ���ӵĵ�ַ��Ϣ
		// ��������������ַ�ĳ���
	SOCKADDR_IN addrSrv;
	addrSrv.sin_addr.S_un.S_addr = inet_addr("127.0.0.1");		// ���ػ�·��ַ��127.0.0.1; 
	//InetPton(AF_INET,"127.0.0.1", &addrSrv.sin_addr.S_un.S_addr);
	addrSrv.sin_family = AF_INET;
	addrSrv.sin_port = htons(DefaultPort1);
	while (SOCKET_ERROR == connect(sockClient, (SOCKADDR*)&addrSrv, sizeof(SOCKADDR))) {
		// �����û�����Ϸ�������Ҫ������
		cout << "����������ʧ�ܣ��Ƿ��������ӣ���Y/N):";
		char choice;
		while (cin >> choice && (!((choice != 'Y' && choice == 'N') || (choice == 'Y' && choice != 'N')))) {
			cout << "�����������������:";
			cin.sync();
			cin.clear();
		}
		if (choice == 'Y') {
			continue;
		}
		else {
			cout << "�˳�ϵͳ��...";
			system("pause");
			return 0;
		}
	}
	cin.sync();
	cout << "���ͻ�����׼���������û���ֱ�����������������������Ϣ��\n";

	send(sockClient, "\nAttention: A Client has enter...\n", 200, 0);

	bufferMutex = CreateSemaphore(NULL, 1, 1, NULL);

	
	

	HANDLE sendThread = CreateThread(NULL, 0, SendMessageThread, NULL, 0, NULL);
	HANDLE receiveThread = CreateThread(NULL, 0, ReceiveMessageThread, NULL, 0, NULL);


	WaitForSingleObject(sendThread, INFINITE);  // �ȴ��߳̽���
	closesocket(sockClient);
	CloseHandle(sendThread);
	CloseHandle(receiveThread);
	CloseHandle(bufferMutex);
	WSACleanup();	// ��ֹ���׽��ֿ��ʹ��

	printf("End linking...\n");
	printf("\n");
	system("pause");
	return 0;
}





