#include "Compress.h"
#include "../3rd/zip/lz4.h"
int Tool::Compress(const char* source, char* dest, int sourceSize, int maxDestSize)
{
		return LZ4_compress_default(source,dest,sourceSize,maxDestSize);
}

int Tool::Decompress(const char* source, char* dest, int compressedSize, int maxDecompressedSize)
{
	return LZ4_decompress_safe(source,dest,compressedSize,maxDecompressedSize);

}

