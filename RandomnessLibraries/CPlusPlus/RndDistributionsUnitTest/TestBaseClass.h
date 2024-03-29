#pragma once
#include <stdio.h>
#include "pch.h"
#include "CppUnitTest.h"

class TestBaseClass
{
public:

	void print(double d);
	void print(double *d, int dsize);
	void print(bool b);
	void print(bool *b, int bsize);
	void print(char c);
	void print(char *c, int csize);

	double GetMax(double *arr, int size);
	double GetMin(double *arr, int size);
	double GetAverage(double *arr, int size);
	void FirstTen(double *arr, double *firstTen);

	char GetMax(char *arr, int size);
	char GetMin(char *arr, int size);
	double GetAverage(char *arr, int size);
	void FirstTen(char *arr, char *firstTen);


	bool GetMax(bool *arr, int size);
	bool GetMin(bool *arr, int size);
	//bool TestBaseClass::GetAverage(bool *arr, int size);
	void FirstTen(bool *arr, bool *firstTen);


	void AssertArray(bool *a, bool *b, int size);
	void AssertArray(char *a, char *b, int size);

};

