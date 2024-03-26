#pragma once
#include <stdio.h>
#include "pch.h"
#include "CppUnitTest.h"

class TestBaseClass
{
public:

	void print(double d);

	double TestBaseClass::GetMax(double *arr, int size);
	double TestBaseClass::GetMin(double *arr, int size);
	double TestBaseClass::GetAverage(double *arr, int size);
};

