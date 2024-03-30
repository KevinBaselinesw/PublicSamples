#include "pch.h"
#include "TestBaseClass.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

void TestBaseClass::print(double d)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", d);
	Logger::WriteMessage(buffer);
}
void TestBaseClass::print(double *d, int dsize)
{
	char buffer[256];

	for (int i = 0; i < dsize; i++)
	{
		sprintf_s(buffer, sizeof(buffer), "%d", d[i]);
		Logger::WriteMessage(buffer);
	}
}

void TestBaseClass::print(char b)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", b);
	Logger::WriteMessage(buffer);
}
void TestBaseClass::print(char *b, int dsize)
{
	char buffer[256];

	for (int i = 0; i < dsize; i++)
	{
		sprintf_s(buffer, sizeof(buffer), "%d", b[i]);
		Logger::WriteMessage(buffer);
	}
}

void TestBaseClass::print(uchar b)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", b);
	Logger::WriteMessage(buffer);
}
void TestBaseClass::print(uchar *b, int dsize)
{
	char buffer[256];

	for (int i = 0; i < dsize; i++)
	{
		sprintf_s(buffer, sizeof(buffer), "%d", b[i]);
		Logger::WriteMessage(buffer);
	}
}

void TestBaseClass::print(short b)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", b);
	Logger::WriteMessage(buffer);
}
void TestBaseClass::print(short *b, int dsize)
{
	char buffer[256];

	for (int i = 0; i < dsize; i++)
	{
		sprintf_s(buffer, sizeof(buffer), "%d", b[i]);
		Logger::WriteMessage(buffer);
	}
}

void TestBaseClass::print(ushort b)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", b);
	Logger::WriteMessage(buffer);
}
void TestBaseClass::print(ushort *b, int dsize)
{
	char buffer[256];

	for (int i = 0; i < dsize; i++)
	{
		sprintf_s(buffer, sizeof(buffer), "%d", b[i]);
		Logger::WriteMessage(buffer);
	}
}
void TestBaseClass::print(int b)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", b);
	Logger::WriteMessage(buffer);
}
void TestBaseClass::print(int *b, int dsize)
{
	char buffer[256];

	for (int i = 0; i < dsize; i++)
	{
		sprintf_s(buffer, sizeof(buffer), "%d", b[i]);
		Logger::WriteMessage(buffer);
	}
}
void TestBaseClass::print(uint b)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", b);
	Logger::WriteMessage(buffer);
}
void TestBaseClass::print(uint *b, int dsize)
{
	char buffer[256];

	for (int i = 0; i < dsize; i++)
	{
		sprintf_s(buffer, sizeof(buffer), "%d", b[i]);
		Logger::WriteMessage(buffer);
	}
}
void TestBaseClass::print(long b)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", b);
	Logger::WriteMessage(buffer);
}
void TestBaseClass::print(long *b, int dsize)
{
	char buffer[256];

	for (int i = 0; i < dsize; i++)
	{
		sprintf_s(buffer, sizeof(buffer), "%d", b[i]);
		Logger::WriteMessage(buffer);
	}
}
void TestBaseClass::print(ulong b)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", b);
	Logger::WriteMessage(buffer);
}
void TestBaseClass::print(ulong *b, int dsize)
{
	char buffer[256];

	for (int i = 0; i < dsize; i++)
	{
		sprintf_s(buffer, sizeof(buffer), "%d", b[i]);
		Logger::WriteMessage(buffer);
	}
}


void TestBaseClass::print(bool b)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", b);
	Logger::WriteMessage(buffer);
}
void TestBaseClass::print(bool *b, int dsize)
{
	char buffer[256];

	for (int i = 0; i < dsize; i++)
	{
		sprintf_s(buffer, sizeof(buffer), "%d", b[i]);
		Logger::WriteMessage(buffer);
	}
}

///////////////////////////////////

double TestBaseClass::GetMax(double *arr, int size)
{
	double highNum = -DBL_MAX;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] > highNum)
			highNum = arr[i];
	}
	return highNum;
}

double TestBaseClass::GetMin(double *arr, int size)
{
	double lowNum = DBL_MAX;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] < lowNum)
			lowNum = arr[i];
	}
	return lowNum;
}

double TestBaseClass::GetAverage(double *arr, int size)
{
	double total = 0;

	for (int i = 0; i < size; i++)
	{
		total += arr[i];
	}

	return total / size;
}

void TestBaseClass::FirstTen(double *arr, double *firstTen)
{
	for (int i = 0; i < 10; i++)
	{
		firstTen[i] = arr[i];
	}
}
//////////////////////////////


bool TestBaseClass::GetMax(bool *arr, int size)
{
	double highNum = true;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] > highNum)
			highNum = arr[i];
	}
	return highNum;
}

bool TestBaseClass::GetMin(bool *arr, int size)
{
	double lowNum = false;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] < lowNum)
			lowNum = arr[i];
	}
	return lowNum;
}
void TestBaseClass::FirstTen(bool *arr, bool *firstTen)
{
	for (int i = 0; i < 10; i++)
	{
		firstTen[i] = arr[i];
	}
}
////////////////////////////

char TestBaseClass::GetMax(char *arr, int size)
{
	char highNum = CHAR_MIN;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] > highNum)
			highNum = arr[i];
	}
	return highNum;
}

char TestBaseClass::GetMin(char *arr, int size)
{
	char lowNum = CHAR_MAX;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] < lowNum)
			lowNum = arr[i];
	}
	return lowNum;
}

double TestBaseClass::GetAverage(char *arr, int size)
{
	double total = 0;

	for (int i = 0; i < size; i++)
	{
		total += arr[i];
	}

	return total / size;
}
void TestBaseClass::FirstTen(char *arr, char *firstTen)
{
	for (int i = 0; i < 10; i++)
	{
		firstTen[i] = arr[i];
	}
}

////////////////////////////

uchar TestBaseClass::GetMax(uchar *arr, int size)
{
	uchar highNum = 0;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] > highNum)
			highNum = arr[i];
	}
	return highNum;
}

uchar TestBaseClass::GetMin(uchar *arr, int size)
{
	uchar lowNum = UCHAR_MAX;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] < lowNum)
			lowNum = arr[i];
	}
	return lowNum;
}

double TestBaseClass::GetAverage(uchar *arr, int size)
{
	double total = 0;

	for (int i = 0; i < size; i++)
	{
		total += arr[i];
	}

	return total / size;
}
void TestBaseClass::FirstTen(uchar *arr, uchar *firstTen)
{
	for (int i = 0; i < 10; i++)
	{
		firstTen[i] = arr[i];
	}
}

////////////////////////////

short TestBaseClass::GetMax(short *arr, int size)
{
	short highNum = SHRT_MIN;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] > highNum)
			highNum = arr[i];
	}
	return highNum;
}

short TestBaseClass::GetMin(short *arr, int size)
{
	short lowNum = SHRT_MAX;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] < lowNum)
			lowNum = arr[i];
	}
	return lowNum;
}

double TestBaseClass::GetAverage(short *arr, int size)
{
	double total = 0;

	for (int i = 0; i < size; i++)
	{
		total += arr[i];
	}

	return total / size;
}
void TestBaseClass::FirstTen(short *arr, short *firstTen)
{
	for (int i = 0; i < 10; i++)
	{
		firstTen[i] = arr[i];
	}
}

////////////////////////////

ushort TestBaseClass::GetMax(ushort *arr, int size)
{
	ushort highNum = 0;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] > highNum)
			highNum = arr[i];
	}
	return highNum;
}

ushort TestBaseClass::GetMin(ushort *arr, int size)
{
	ushort lowNum = USHRT_MAX;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] < lowNum)
			lowNum = arr[i];
	}
	return lowNum;
}

double TestBaseClass::GetAverage(ushort *arr, int size)
{
	double total = 0;

	for (int i = 0; i < size; i++)
	{
		total += arr[i];
	}

	return total / size;
}
void TestBaseClass::FirstTen(ushort *arr, ushort *firstTen)
{
	for (int i = 0; i < 10; i++)
	{
		firstTen[i] = arr[i];
	}
}

////////////////////////////

int TestBaseClass::GetMax(int *arr, int size)
{
	int highNum = INT_MIN;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] > highNum)
			highNum = arr[i];
	}
	return highNum;
}

int TestBaseClass::GetMin(int *arr, int size)
{
	int lowNum = INT_MAX;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] < lowNum)
			lowNum = arr[i];
	}
	return lowNum;
}

double TestBaseClass::GetAverage(int *arr, int size)
{
	double total = 0;

	for (int i = 0; i < size; i++)
	{
		total += arr[i];
	}

	return total / size;
}
void TestBaseClass::FirstTen(int *arr, int *firstTen)
{
	for (int i = 0; i < 10; i++)
	{
		firstTen[i] = arr[i];
	}
}

////////////////////////////

uint TestBaseClass::GetMax(uint *arr, int size)
{
	uint highNum = 0;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] > highNum)
			highNum = arr[i];
	}
	return highNum;
}

uint TestBaseClass::GetMin(uint *arr, int size)
{
	uint lowNum = UINT_MAX;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] < lowNum)
			lowNum = arr[i];
	}
	return lowNum;
}

double TestBaseClass::GetAverage(uint *arr, int size)
{
	double total = 0;

	for (int i = 0; i < size; i++)
	{
		total += arr[i];
	}

	return total / size;
}
void TestBaseClass::FirstTen(uint *arr, uint *firstTen)
{
	for (int i = 0; i < 10; i++)
	{
		firstTen[i] = arr[i];
	}
}

////////////////////////////

long TestBaseClass::GetMax(long *arr, int size)
{
	long highNum = LONG_MIN;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] > highNum)
			highNum = arr[i];
	}
	return highNum;
}

long TestBaseClass::GetMin(long *arr, int size)
{
	long lowNum = LONG_MAX;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] < lowNum)
			lowNum = arr[i];
	}
	return lowNum;
}

double TestBaseClass::GetAverage(long *arr, int size)
{
	double total = 0;

	for (int i = 0; i < size; i++)
	{
		total += arr[i];
	}

	return total / size;
}
void TestBaseClass::FirstTen(long *arr, long *firstTen)
{
	for (int i = 0; i < 10; i++)
	{
		firstTen[i] = arr[i];
	}
}

////////////////////////////

ulong TestBaseClass::GetMax(ulong *arr, int size)
{
	ulong highNum = 0;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] > highNum)
			highNum = arr[i];
	}
	return highNum;
}

ulong TestBaseClass::GetMin(ulong *arr, int size)
{
	ulong lowNum = ULLONG_MAX;
	int i;

	for (i = 0; i < size; i++)
	{
		if (arr[i] < lowNum)
			lowNum = arr[i];
	}
	return lowNum;
}

double TestBaseClass::GetAverage(ulong *arr, int size)
{
	double total = 0;

	for (int i = 0; i < size; i++)
	{
		total += arr[i];
	}

	return total / size;
}
void TestBaseClass::FirstTen(ulong *arr, ulong *firstTen)
{
	for (int i = 0; i < 10; i++)
	{
		firstTen[i] = arr[i];
	}
}

void TestBaseClass::AssertArray(bool *a, bool *b, int size)
{
	for (int i = 0; i < size; i++)
	{
		Assert::AreEqual(a[i], b[i]);
	}
}
void TestBaseClass::AssertArray(char *a, char *b, int size)
{
	for (int i = 0; i < size; i++)
	{
		Assert::AreEqual(a[i], b[i]);
	}
}

void TestBaseClass::AssertArray(uchar *a, uchar *b, int size)
{
	for (int i = 0; i < size; i++)
	{
		Assert::AreEqual(a[i], b[i]);
	}
}

void TestBaseClass::AssertArray(short *a, short *b, int size)
{
	for (int i = 0; i < size; i++)
	{
		Assert::AreEqual(a[i], b[i]);
	}
}

//void TestBaseClass::AssertArray(ushort *a, ushort *b, int size)
//{
//	for (int i = 0; i < size; i++)
//	{
//		Assert::AreEqual(a[i], b[i]);
//	}
//}

void TestBaseClass::AssertArray(int *a, int *b, int size)
{
	for (int i = 0; i < size; i++)
	{
		Assert::AreEqual(a[i], b[i]);
	}
}

void TestBaseClass::AssertArray(uint *a, uint *b, int size)
{
	for (int i = 0; i < size; i++)
	{
		Assert::AreEqual(a[i], b[i]);
	}
}

void TestBaseClass::AssertArray(long *a, long *b, int size)
{
	for (int i = 0; i < size; i++)
	{
		Assert::AreEqual(a[i], b[i]);
	}
}

void TestBaseClass::AssertArray(ulong *a, ulong *b, int size)
{
	for (int i = 0; i < size; i++)
	{
		Assert::AreEqual(a[i], b[i]);
	}
}

void TestBaseClass::AssertArray(double *a, double *b, int size)
{
	for (int i = 0; i < size; i++)
	{
		Assert::AreEqual(a[i], b[i], doubleTolerance);
	}
}


int * TestBaseClass::arange_Int32(int start, int end)
{
	int bufLength = end - start;
	int * buffer = new int[bufLength];

	for (int i = 0; i < bufLength; i++)
	{
		buffer[i] = i + start;
	}

	return buffer;
}

double* TestBaseClass::arange_Double(int start, int end)
{
	int bufLength = end - start;
	double* buffer = new double[bufLength];

	for (int i = 0; i < bufLength; i++)
	{
		buffer[i] = (double)(i + start);
	}

	return buffer;
}

bool* TestBaseClass::CompareArray(long* arr, int arrlen, int c)
{
	bool* output = new bool[arrlen];

	for (int i = 0; i < arrlen; i++)
	{
		output[i] = arr[i] == c;
	}
	return output;
}


int TestBaseClass::CountTrues(bool* v, int vlength)
{
	int count = 0;

	for (int i = 0; i < vlength; i++)
	{
		count += v[i] == true ? 1 : 0;
	}

	return count;
}



