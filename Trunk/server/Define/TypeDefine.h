#ifndef _DEFINE_H__
#define _DEFINE_H__
#define SAFE_DEL(x){if((x)){delete((x));x=NULL;}}
#define SAFE_DEL_ARRAY(x){if((x)){delete [] ((x)); x=NULL}}
#define CHECK_NULL_RETURN(x)do{if(x==nullptr)return nullptr;}while(false)



/**
* @brief short
*/
typedef short SHORT;

/**
* @brief long long
*/
typedef long long LONGLONG;

/**
* @brief LONG
*/
typedef long LONG;

/**
* @brief FLOAT
*/

typedef float FLOAT;

/**
* @brief char
*/

typedef char CHAR;
/**
* @brief unsingend char
*/

typedef unsigned char BYTE;
/**
* @brief unsigned short
*/

typedef unsigned short WORD;
/**
* @brief signed short
*/

typedef signed short SWORD;
/**
* @brief unsigned int
*/

typedef unsigned int DWORD;

/**
* @brief signed int
*/

typedef signed int SDWORD;
/**
* @brief unsigned long
*/

typedef unsigned long QWORD;
/**
* @brief signed long
*/

typedef signed long SQWORD;
/**
* @brief INT64
*/
typedef long INT64;

/**
* @brief double
*/
typedef double DOUBLE;



#endif
