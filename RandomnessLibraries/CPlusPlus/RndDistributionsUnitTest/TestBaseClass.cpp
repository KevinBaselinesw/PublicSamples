#include "pch.h"
#include "TestBaseClass.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

void TestBaseClass::print(double d)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", d);
	Logger::WriteMessage(buffer);
}


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