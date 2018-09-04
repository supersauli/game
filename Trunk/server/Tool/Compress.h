#ifndef _SCOMPARESS_H__
#define _SCOMPARESS_H__

namespace Tool
{
	/**
	 * @brief 压缩
	 *
	 * @param source 源
	 * @param dest 目的
	 * @param sourceSize 源大小
	 * @param maxDestSize 目的大小
	 *
	 * @return
	 */
	int Compress(const char* source, char* dest, int sourceSize, int maxDestSize);
	/**
	 * @brief 解压缩
	 *
	 * @param source 源
	 * @param dest 目的
	 * @param compressedSize 源大小
	 * @param maxDecompressedSize 最大解压大小
	 *
	 * @return
	 */
	int Decompress(const char* source, char* dest, int compressedSize, int maxDecompressedSize);

};

#endif
