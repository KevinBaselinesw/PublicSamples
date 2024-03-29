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

//////////////////////////

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
