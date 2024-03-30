#pragma once
#include <stdio.h>
#include "pch.h"
#include "CppUnitTest.h"

#define ulong unsigned long
#define uint unsigned int
#define ushort unsigned short
#define uchar unsigned char

const double doubleTolerance = .00000000001;

class TestBaseClass
{
public:

	void print(double d);
	void print(double *d, int dsize);
	void print(bool b);
	void print(bool *b, int bsize);
	void print(char c);
	void print(char *c, int csize);
	void print(uchar c);
	void print(uchar *c, int csize);
	void print(short c);
	void print(short *c, int csize);
	void print(ushort c);
	void print(ushort *c, int csize);
	void print(int c);
	void print(int *c, int csize);
	void print(uint c);
	void print(uint *c, int csize);
	void print(long c);
	void print(long *c, int csize);
	void print(ulong c);
	void print(ulong *c, int csize);

	double GetMax(double *arr, int size);
	double GetMin(double *arr, int size);
	double GetAverage(double *arr, int size);
	void FirstTen(double *arr, double *firstTen);

	bool GetMax(bool *arr, int size);
	bool GetMin(bool *arr, int size);
	//bool TestBaseClass::GetAverage(bool *arr, int size);
	void FirstTen(bool *arr, bool *firstTen);

	char GetMax(char *arr, int size);
	char GetMin(char *arr, int size);
	double GetAverage(char *arr, int size);
	void FirstTen(char *arr, char *firstTen);

	uchar GetMax(uchar *arr, int size);
	uchar GetMin(uchar *arr, int size);
	double GetAverage(uchar *arr, int size);
	void FirstTen(uchar *arr, uchar *firstTen);

	short GetMax(short *arr, int size);
	short GetMin(short *arr, int size);
	double GetAverage(short *arr, int size);
	void FirstTen(short *arr, short *firstTen);

	ushort GetMax(ushort *arr, int size);
	ushort GetMin(ushort *arr, int size);
	double GetAverage(ushort *arr, int size);
	void FirstTen(ushort *arr, ushort *firstTen);

	int GetMax(int *arr, int size);
	int GetMin(int *arr, int size);
	double GetAverage(int *arr, int size);
	void FirstTen(int *arr, int *firstTen);

	uint GetMax(uint *arr, int size);
	uint GetMin(uint *arr, int size);
	double GetAverage(uint *arr, int size);
	void FirstTen(uint *arr, uint *firstTen);

	long GetMax(long *arr, int size);
	long GetMin(long *arr, int size);
	double GetAverage(long *arr, int size);
	void FirstTen(long *arr, long *firstTen);

	ulong GetMax(ulong *arr, int size);
	ulong GetMin(ulong *arr, int size);
	double GetAverage(ulong *arr, int size);
	void FirstTen(ulong *arr, ulong *firstTen);


	void AssertArray(bool *a, bool *b, int size);
	void AssertArray(char *a, char *b, int size);
	void AssertArray(uchar *a, uchar *b, int size);
	void AssertArray(short *a, short *b, int size);
	//void AssertArray(ushort *a, ushort *b, int size);
	void AssertArray(int *a, int *b, int size);
	void AssertArray(uint *a, uint *b, int size);
	void AssertArray(long *a, long *b, int size);
	void AssertArray(ulong *a, ulong *b, int size);
	void AssertArray(double *a, double *b, int size);

	int * arange_Int32(int start, int end);
	double* arange_Double(int start, int end);
	bool* CompareArray(long* arr, int arrlen, int c);
	int CountTrues(bool* v, int vlength);

};

