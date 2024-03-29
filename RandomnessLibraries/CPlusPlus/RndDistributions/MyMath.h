/*
 * BSD 3-Clause License
 *
 * Copyright (c) 2024
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its
 *    contributors may be used to endorse or promote products derived from
 *    this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
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

