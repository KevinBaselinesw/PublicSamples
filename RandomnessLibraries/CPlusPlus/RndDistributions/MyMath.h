#pragma once
#define _USE_MATH_DEFINES
#include <math.h>

class Math
{
public:

	static constexpr double PI = M_PI;

	static double Min(double x, double y)
	{
		return min(x, y);
	}
	static double Max(double x, double y)
	{
		return max(x, y);
	}

	static long Min(long x, long y)
	{
		return min(x, y);
	}
	static long Max(long x, long y)
	{
		return max(x, y);
	}


	static double Log(double x)
	{
		return log(x);
	}

	static double Sqrt(double x)
	{
		return sqrt(x);
	}

	static double Floor(double x)
	{
		return floor(x);
	}
	static double Ceiling(double x)
	{
		return ceil(x);
	}

	static double IEEERemainder(double x, double y)
	{
		return 0;
	}

	static double Pow(double x, double y)
	{
		return pow(x,y);
	}

	static double Exp(double x)
	{
		return exp(x);
	}

	static double Abs(double x)
	{
		return fabs(x);
	}

	static long Abs(long x)
	{
		return abs(x);
	}

	static double Cos(double x)
	{
		return cos(x);
	}

	static double Acos(double x)
	{
		return acos(x);
	}

};

