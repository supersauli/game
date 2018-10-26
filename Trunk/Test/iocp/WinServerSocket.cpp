#include <cassert>
#define  _WINSOCK_DEPRECATED_NO_WARNINGS 0
#include <WS2tcpip.h>

#include "WinServerSocket.h"
#include "Consummer.h"
#include <iostream>

bool WinServerSocket::Start()
{
	if (!LoadLib())
	{
		return false;
	}

	if (!InitOCompletionPort())
	{
		return false;
	}

	if(!InitEvent())
	{
		return false;
	}

	if (!InitWorkerThread())
	{
		return false;
	}

	if (!InitSocket())
	{
		return false;
	}

	

	return true;
}

void WinServerSocket::Stop()
{
	SetEvent(_handleEvent);
	for(const auto& it: _workerThread)
	{
		PostQueuedCompletionStatus(_comletionPort, 0, THREAD_EVENT_EXIT, nullptr);
	}

	for (const auto& it : _workerThread)
	{
		WaitForMultipleObjects(1, &it, TRUE, INFINITE);
	}


}

bool WinServerSocket::LoadLib()
{
	WSADATA wsaData;
	int nResult = WSAStartup(MAKEWORD(2, 2), &wsaData);
	if (NO_ERROR != nResult)
	{
		//TODO
		return false;
	}

	return true;
}

bool WinServerSocket::InitOCompletionPort()
{
	_comletionPort = CreateIoCompletionPort(INVALID_HANDLE_VALUE, NULL, 0, 9);
	return _comletionPort != nullptr;
}
DWORD WINAPI WinServerSocket::WorkerThread(LPVOID lpParam)
{
	WinServerSocket* pServerSocket = (WinServerSocket*)lpParam;

	OVERLAPPED           *pOverlapped = nullptr;
	Consummer			 *pConsummer = nullptr;
	DWORD                dwBytesTransfered = 0;

	// ѭ����������֪�����յ�Shutdown��ϢΪֹ
	while (WAIT_OBJECT_0 != WaitForSingleObject(pServerSocket->_handleEvent, 0))
	
	{
		BOOL bReturn = GetQueuedCompletionStatus(
			pServerSocket->_comletionPort,
			&dwBytesTransfered,
			(PULONG_PTR)&pConsummer,
			&pOverlapped,
			INFINITE);

		//// ����յ������˳���־����ֱ���˳�
		if (THREAD_EVENT_EXIT == reinterpret_cast<DWORD>(pConsummer))
		{
			break;
		}

		// �ж��Ƿ�����˴���
		if (!bReturn)
		{
			DWORD dwErr = GetLastError();

			//// ��ʾһ����ʾ��Ϣ
			//if (!pIOCPModel->HandleError(pSocketContext, dwErr))
			//{
			//	break;
			//}

			continue;
		}
		else
		{
			// ��ȡ����Ĳ���
			PER_IO_CONTEXT* pIoContext = CONTAINING_RECORD(pOverlapped, PER_IO_CONTEXT, _overlapped);
			// �ж��Ƿ��пͻ��˶Ͽ���
			if ((0 == dwBytesTransfered) && (RECV_POSTED == pIoContext->_opType || SEND_POSTED == pIoContext->_opType))
			{
				//pIOCPModel->_ShowMessage(_T("�ͻ��� %s:%d �Ͽ�����."), inet_ntoa(pSocketContext->m_ClientAddr.sin_addr), ntohs(pSocketContext->m_ClientAddr.sin_port));
				// �ͷŵ���Ӧ����Դ
				//pIOCPModel->_RemoveContext(pSocketContext);
				//TODO
				pServerSocket->CloseSocket(pIoContext);
				continue;
			}
			else
			{
				switch (pIoContext->_opType)
				{
					case ACCEPT_POSTED:{
						pServerSocket->DoAccept(pIoContext);
					}break;

					case RECV_POSTED:{
						pServerSocket->DoRecv(pIoContext);
					}break;
					case SEND_POSTED:{
						pServerSocket->DoSend(pIoContext);
					}break;
				default:
					//TRACE(_T("_WorkThread�е� pIoContext->m_OpType �����쳣.\n"));
					break;
				} //switch
			}//if
		}//if

	}//while

	//TRACE(_T("�������߳� %d ���˳�.\n"), nThreadNo);

	// �ͷ��̲߳���
	//RELEASE(lpParam);

	delete pServerSocket;

	return 0;
}

void WinServerSocket::CloseSocket(Consummer* pConsummer)
{
	pConsummer->Close();
}

void WinServerSocket::ShowError()
{
	//LPVOID lpMsgBuf;
	//FormatMessage(
	//	FORMAT_MESSAGE_ALLOCATE_BUFFER
	//	| FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
	//	NULL,
	//	GetLastError(),
	//	MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), // Default language
	//	(LPTSTR)&lpMsgBuf,
	//	0,
	//	NULL
	//);
	std::cout << GetLastError() <<std::endl;
	// Process any inserts in lpMsgBuf.
	// ...
	// Display the string.
	//MessageBox(NULL, (LPCTSTR)lpMsgBuf, _T("Error"), MB_OK | MB_ICONINFORMATION);
	// Free the buffer.
	//LocalFree(lpMsgBuf);
	
	
}

bool WinServerSocket::InitWorkerThread()
{
	_threadNums = 1;
	for (int i = 0; i < _threadNums; i++)
	{
		auto handle =  ::CreateThread(nullptr, 0, WorkerThread, static_cast<void*>(this), 0, nullptr);
		_workerThread.push_back(handle);
	}
	return true;
}

int WinServerSocket::GetPorcessorsNum() const
{
	SYSTEM_INFO si;

	GetSystemInfo(&si);

	return si.dwNumberOfProcessors;
}

bool WinServerSocket::GetLocalIP(std::string& ip) const
{
	struct addrinfo *result = NULL;
	struct addrinfo *ptr = NULL;
	struct addrinfo hints;
	struct sockaddr_in* sockaddrIP;
	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_UNSPEC;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;

	int dwRetval = getaddrinfo("", nullptr, &hints, &result);
	if (dwRetval == 0) {
		for (ptr = result; ptr != nullptr; ptr = ptr->ai_next) {
			//printf("getaddrinfo response %d\n", i++);
			//printf("\tFlags: 0x%x\n", ptr->ai_flags);
			//printf("\tFamily: ");
			switch (ptr->ai_family) {
			case AF_UNSPEC:
				printf("Unspecified\n");
				break;
			case AF_INET:
				printf("AF_INET (IPv4)\n");
				sockaddrIP = reinterpret_cast<struct sockaddr_in *>(ptr->ai_addr);
				/*printf("\tIPv4 address %s\n",
					inet_ntoa(inAddr->sin_addr));*/
				break;
			case AF_INET6:
				/*printf("AF_INET6 (IPv6)\n");
				sockaddrIP = (LPSOCKADDR)ptr->ai_addr;
				ipbufferlength = 46;
				auto iRetval = WSAAddressToString(sockaddr_ip, (DWORD)ptr->ai_addrlen, NULL,
					ipstringbuffer, &ipbufferlength);
				if (iRetval)
					printf("WSAAddressToString failed with %u\n", WSAGetLastError());
				else
					printf("\tIPv6 address %s\n", ipstringbuffer);*/
				break;
			case AF_NETBIOS:
				printf("AF_NETBIOS (NetBIOS)\n");
				break;
			default:
				printf("Other %ld\n", ptr->ai_family);
				break;
			}
		}
	}
	return true;
}



bool WinServerSocket::InitSocket()
{
	GUID guidAcceptEx = WSAID_ACCEPTEX;
	GUID guidGetAcceptExsockAddrs = WSAID_GETACCEPTEXSOCKADDRS;
	struct sockaddr_in serverAddress;
	_socket = WSASocketW(AF_INET, SOCK_STREAM, 0, NULL, 0, WSA_FLAG_OVERLAPPED);
	if (_socket == INVALID_SOCKET)
	{
		return false;
	}

	if (CreateIoCompletionPort(reinterpret_cast<HANDLE>(_socket), _comletionPort, 0, 0) == NULL) {
		return false;
	}

	ZeroMemory(&serverAddress, sizeof(serverAddress));
	serverAddress.sin_family = AF_INET;
	if (_ip.empty())
	{
		GetLocalIP(_ip);
		serverAddress.sin_addr.s_addr = htonl(INADDR_ANY);
		
	}
	else
	{
		serverAddress.sin_addr.s_addr = inet_addr(_ip.c_str());
	}
	serverAddress.sin_port = htons(_port);
	if (bind(_socket, reinterpret_cast<struct sockaddr*>(&serverAddress), sizeof(serverAddress)) == SOCKET_ERROR) {
		return false;
	}

	if (listen(_socket, SOMAXCONN) == SOCKET_ERROR)
	{
		return false;
	}
	DWORD bytes = 0;
	
	if (WSAIoctl(_socket, SIO_GET_EXTENSION_FUNCTION_POINTER, &guidAcceptEx, sizeof(
		guidAcceptEx), &_acceptEx, sizeof(_acceptEx), &bytes, NULL, NULL) == SOCKET_ERROR)
	{
		ShowError();
		return false;
	}
	if (WSAIoctl(_socket, SIO_GET_EXTENSION_FUNCTION_POINTER,
		&guidGetAcceptExsockAddrs,
		sizeof(guidGetAcceptExsockAddrs),
		&_acceptExSockAddrs,
		sizeof(_acceptExSockAddrs),
		&bytes,
		NULL, NULL) == SOCKET_ERROR)
	{
		return false;
	}
	for (int i = 0; i < 1; i++)
	{
		auto pAcceptIoContext = new PER_IO_CONTEXT;
		if (pAcceptIoContext == nullptr)
		{
			break;
		}

		if (!PostAccept(pAcceptIoContext))
		{
			delete pAcceptIoContext;
			return false;
		}
	}

	return true;
}

bool WinServerSocket::InitEvent()
{
	_handleEvent = CreateEvent(NULL, TRUE, FALSE, NULL);
	return true;
}

bool WinServerSocket::PostAccept(PER_IO_CONTEXT* pAcceptIoContext)
{
	assert(INVALID_SOCKET != _socket);
	DWORD bytes = 0;
	pAcceptIoContext->_opType = ACCEPT_POSTED;
	WSABUF *pWBuf = &pAcceptIoContext->_wsaBuf;
	OVERLAPPED*pOl = &pAcceptIoContext->_overlapped;
	auto socket = WSASocketW(AF_INET, SOCK_STREAM, IPPROTO_TCP, NULL, 0, WSA_FLAG_OVERLAPPED);
	if (socket == INVALID_SOCKET)
	{
		return false;
	}
	const int sockaddrSize = sizeof(SOCKADDR_IN);
	pAcceptIoContext->_sockAccept = socket;
	if (!_acceptEx(_socket, socket, pWBuf->buf, pWBuf->len - ((sizeof(SOCKADDR_IN) + 16) * 2),
		sockaddrSize + 16, sockaddrSize + 16, &bytes, pOl))
	{
		if (WSA_IO_PENDING != WSAGetLastError())
		{
			//TODO
			ShowError();
			return false;
		}
	}

	return true;
}

bool WinServerSocket::PostRecv(PER_IO_CONTEXT* pIoContext)
{
	// ��ʼ������
	DWORD dwFlags = 0;
	DWORD dwBytes = 0;
	WSABUF *p_wbuf = &pIoContext->_wsaBuf;
	OVERLAPPED *p_ol = &pIoContext->_overlapped;

	pIoContext->ResetBuffer();
	pIoContext->_opType = RECV_POSTED;

	// ��ʼ����ɺ󣬣�Ͷ��WSARecv����
	int nBytesRecv = WSARecv(pIoContext->_sockAccept, p_wbuf, 1, &dwBytes, &dwFlags, p_ol, NULL);

	// �������ֵ���󣬲��Ҵ���Ĵ��벢����Pending�Ļ����Ǿ�˵������ص�����ʧ����
	if ((SOCKET_ERROR == nBytesRecv) && (WSA_IO_PENDING != WSAGetLastError()))
	{
		//TODO
		return false;
	}
	return true;
}

bool WinServerSocket::DoAccept(PER_IO_CONTEXT* pIoContext)
{
	SOCKADDR_IN* ClientAddr = NULL;
	SOCKADDR_IN* LocalAddr = NULL;
	int sockaddrLen = sizeof(SOCKADDR_IN);
	int localLen, remoteLen = sockaddrLen;
	///////////////////////////////////////////////////////////////////////////
	// 1. ����ȡ������ͻ��˵ĵ�ַ��Ϣ
	// ��� m_lpfnGetAcceptExSockAddrs �����˰�~~~~~~
	// ��������ȡ�ÿͻ��˺ͱ��ض˵ĵ�ַ��Ϣ������˳��ȡ���ͻ��˷����ĵ�һ�����ݣ���ǿ����...
	_acceptExSockAddrs(pIoContext->_wsaBuf.buf, pIoContext->_wsaBuf.len - ((sizeof(SOCKADDR_IN) + 16) * 2),
		sockaddrLen + 16, sockaddrLen + 16, (LPSOCKADDR*)&LocalAddr, &localLen, (LPSOCKADDR*)&ClientAddr, &remoteLen);

	//this->_ShowMessage(_T("�ͻ��� %s:%d ����."), inet_ntoa(ClientAddr->sin_addr), ntohs(ClientAddr->sin_port));
	//this->_ShowMessage(_T("�ͻ��� %s:%d ��Ϣ��%s."), inet_ntoa(ClientAddr->sin_addr), ntohs(ClientAddr->sin_port), pIoContext->m_wsaBuf.buf);


	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// 2. ������Ҫע�⣬���ﴫ��������ListenSocket�ϵ�Context�����Context���ǻ���Ҫ���ڼ�����һ������
	// �����һ���Ҫ��ListenSocket�ϵ�Context���Ƴ���һ��Ϊ�������Socket�½�һ��SocketContext

	Consummer* pConsummer = new Consummer();
	if (pConsummer == nullptr)
	{
		return false;
	}
	pConsummer->_socket = pIoContext->_sockAccept;
	memcpy(&(pConsummer->_addr), ClientAddr, sizeof(SOCKADDR_IN));

	// ����������ϣ������Socket����ɶ˿ڰ�(��Ҳ��һ���ؼ�����)
	if (!InitIocp(pConsummer))
	{
		delete pConsummer;
		return false;
	}

	///////////////////////////////////////////////////////////////////////////////////////////////////
	// 3. �������������µ�IoContext�����������Socket��Ͷ�ݵ�һ��Recv��������
	PER_IO_CONTEXT* pNewIoContext = new PER_IO_CONTEXT;
	//pNewIoContext->_opType = RECV_POSTED;
	pNewIoContext->_sockAccept = pConsummer->_socket;
	// ���Buffer��Ҫ���������Լ�����һ�ݳ���
	//memcpy( pNewIoContext->m_szBuffer,pIoContext->m_szBuffer,MAX_BUFFER_LEN );

	// �����֮�󣬾Ϳ��Կ�ʼ�����Socket��Ͷ�����������
	if (!PostRecv(pNewIoContext))
	{
		//pNewSocketContext->RemoveContext(pNewIoContext);
		delete pNewIoContext;
		return false;
	}

	/////////////////////////////////////////////////////////////////////////////////////////////////
	// 4. ���Ͷ�ݳɹ�����ô�Ͱ������Ч�Ŀͻ�����Ϣ�����뵽ContextList��ȥ(��Ҫͳһ���������ͷ���Դ)
	//this->_AddToContextList(pNewSocketContext);

	////////////////////////////////////////////////////////////////////////////////////////////////
	// 5. ʹ�����֮�󣬰�Listen Socket���Ǹ�IoContext���ã�Ȼ��׼��Ͷ���µ�AcceptEx
	pIoContext->ResetBuffer();
	return this->PostAccept(pIoContext);
}

bool WinServerSocket::DoRecv(PER_IO_CONTEXT* pIoContext)
{
	printf("recv %s\n", pIoContext->_wsaBuf.buf);
	return PostRecv(pIoContext);
}

bool WinServerSocket::DoSend(PER_IO_CONTEXT* pIoContext)
{
	return true;
}

bool WinServerSocket::InitIocp(Consummer*pConsummer)
{
	HANDLE hTemp = CreateIoCompletionPort((HANDLE)pConsummer->_socket, _comletionPort, (DWORD)pConsummer, 0);

	if (NULL == hTemp)
	{
		//this->_ShowMessage(("ִ��CreateIoCompletionPort()���ִ���.������룺%d"), GetLastError());
		return false;
	}

	return true;
}
