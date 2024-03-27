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
#include "pch.h"
#include "framework.h"
#include "RandomAPI.h"
#include "IRandomGenerator.h"
#include "RandomState.h"
#include "RandomDistributions.h"
#include <math.h>
#include <time.h>
#include <limits.h>

// This is the constructor of a class that has been exported.
Random::Random(IRandomGenerator *rndGenerator)
{
	if (rndGenerator == NULL)
	{
		rndGenerator = new RandomState();
	}
	internal_state = new rk_state(rndGenerator);
	seed(NULL);
	_rndGenerator = rndGenerator;

    return;
}
#pragma region seed


bool Random::seed()
{
	time_t now = time(0);

	long _seed = (long)now;
	
	seed(_seed);

	double tmp1 = rand();

	_seed += (long)tmp1;
	seed(_seed);
	tmp1 = rand();
	tmp1 = rand();

	_seed ^= (long)tmp1;

	seed(_seed);

	return true;
}

bool Random::seed(long seed)
{
	internal_state->rndGenerator->Seed(seed, internal_state);
	return true;
}

#pragma endregion

void ValidateSize(long size)
{	
	if (size < 0)
		throw "size must not be less than 0";
}

#pragma region rand

double Random::rand()
{
	return random_sample();
}
double* Random::rand(long size)
{
	ValidateSize(size);
	return random_sample(size);
}

#pragma endregion

#pragma region randn

double Random::randn()
{
	return standard_normal();
}

double* Random::randn(long size)
{
	ValidateSize(size);

	return standard_normal(size);
}

#pragma endregion


#pragma region randbool

bool* Random::randbool(long low, ulong high, long size)
{
	ValidateSize(size);

	bool* randomData = new bool[size];

	RandomDistributions::rk_random_bool(false, true, size, randomData, internal_state);

	return randomData;
}

#pragma endregion

#pragma region randint8

char* Random::randint8(long low, long high, long size)
{
	ValidateSize(size);

	if (low < -SCHAR_MIN)
		throw "low is out of bounds for Int8";
	if (high > SCHAR_MAX)
		throw "high is out of bounds for Int8";

	char * randomData = new char[size];

	char _low = (char)low;
	char _high = (char)high;

	
	char rng = _high - _low;
	char off = _low;

	RandomDistributions::rk_random_int8(off, (char)(rng - 1), size, randomData, internal_state);

	return randomData;
}

#pragma endregion

#pragma region randuint8

uchar *Random::randuint8(long low, ulong high, long size)
{
	ValidateSize(size);

	if (low < 0)
		throw "low is out of bounds for UInt8";
	if (high > UCHAR_MAX)
		throw "high is out of bounds for UInt8";

	uchar* randomData = new uchar[size];

	uchar _low = (uchar)low;
	uchar _high = (uchar)high;
	uchar rng = _high - _low;
	uchar off = _low;

	RandomDistributions::rk_random_uint8(off, (uchar)(rng - 1), size, randomData, internal_state);
	return randomData;
}

#pragma endregion

#pragma region randint16

short * Random::randint16(long low, ulong high, long size)
{
	ValidateSize(size);

	if (low < SHRT_MIN)
		throw "low is out of bounds for Int16";
	if (high > SHRT_MAX)
		throw "high is out of bounds for Int16";

	short* randomData = new short[size];

	short _low = (short)low;
	short _high = (short)high;

	short rng = _high - _low;
	short off = _low;

	RandomDistributions::rk_random_int16(off, (short)(rng - 1), size, randomData, internal_state);

	return randomData;
}

#pragma endregion

#pragma region randuint16

ushort * Random::randuint16(long low, ulong high, long size)
{
	ValidateSize(size);

	if (low < 0)
		throw "low is out of bounds for UInt16";
	if (high > USHRT_MAX)
		throw "high is out of bounds for UInt16";

	ushort * randomData = new ushort[size];

	ushort _low = (ushort)low;
	ushort _high = (ushort)high;


	ushort rng = _high - _low;
	ushort off = _low;

	RandomDistributions::rk_random_uint16(off, (ushort)(rng - 1), size, randomData, internal_state);

	return randomData;
}

#pragma endregion

#pragma region randint

int * Random::randint32(long low, ulong high, long size)
{
	ValidateSize(size);

	if (low < INT_MIN)
		throw "low is out of bounds for Int32";
	if (high > INT_MAX)
		throw "high is out of bounds for Int32";

	int *randomData = new int[size];

	int _low = (int)low;
	int _high = (int)high;

	int rng = _high - _low;
	int off = _low;
	RandomDistributions::rk_random_int32(off, rng - 1, size, randomData, internal_state);

	return randomData;
}

#pragma endregion

#pragma region randuint

uint *Random::randuint32(long low, ulong high, long size)
{
	ValidateSize(size);

	if (low < 0)
		throw "low is out of bounds for UInt32";
	if (high > UINT_MAX)
		throw "high is out of bounds for UInt32";

	uint *randomData = new uint[size];

	uint _low = (uint)low;
	uint _high = (uint)high;


	uint rng = _high - _low;
	uint off = _low;

	RandomDistributions::rk_random_uint32(off, rng - 1, size, randomData, internal_state);

	return randomData;
}

#pragma endregion

#pragma region randint64

long* Random::randint64(long low, ulong high, long size)
{
	ValidateSize(size);

	if (low < LLONG_MIN)
		throw "low is out of bounds for long";
	if (high > LLONG_MAX)
		throw "high is out of bounds for long";

	long* randomData = new long[size];

	long _low = (long)low;
	long _high = (long)high;

	long rng = _high - _low;
	long off = _low;

	RandomDistributions::rk_random_int64(off, rng - 1, size, randomData, internal_state);

	return randomData;
}

#pragma endregion

#pragma region randuint64

ulong * Random::randuint64(long low, ulong high, long size)
{
	ValidateSize(size);

	if (low < 0)
		throw "low is out of bounds for UInt64";
	if (high > ULLONG_MAX)
		throw "high is out of bounds for UInt64";

	ulong* randomData = new ulong[size];

	ulong _low = (ulong)low;
	ulong _high = (ulong)high;

	ulong rng = _high - _low;
	ulong off = _low;

	RandomDistributions::rk_random_uint64(off, rng - 1, size, randomData, internal_state);

	return randomData;
}

#pragma endregion

#pragma region random_integers

int *Random::random_integers(long low, ulong high, long size)
{
	return randint32(low, high + 1, size);
}

#pragma endregion

#pragma region random_sample

double Random::random_sample()
{
	double* rndArray = cont0_array(internal_state, RandomDistributions::rk_double, 0);
	double retVal = rndArray[0];
	delete rndArray;
	return retVal;
}

double* Random::random_sample(long size)
{
	double* rndArray = cont0_array(internal_state, RandomDistributions::rk_double, size);
	return rndArray;
}

#pragma endregion

#pragma region bytes

char Random::getbyte()
{
	char b[1];

	RandomDistributions::rk_fill(&b[0], 1, internal_state);
	return b[0];
}


char * Random::bytes(int size)
{
	char* b = new char[size];

	RandomDistributions::rk_fill(b, size, internal_state);
	return b;
}
#pragma endregion

#pragma region shuffle / permutation

#if false

public void shuffle<T>(T x)
{
	shuffle(new T[]{ x });
}

public void shuffle<T>(T[] x)
{
	int n = x.Length;

	T buf = default(T);

	for (ulong i = (ulong)n - 1; i >= 1; i--)
	{
		ulong j = RandomDistributions.rk_interval(i, internal_state);
		buf = x[j];
		x[j] = x[i];
		x[i] = buf;
	}
	return;
}

public T[] permutation<T>(T arr)
{
	return permutation(new T[]{ arr });
}

public T[] permutation<T>(T[] arr)
{
	if (arr.Length == 1 && IsInteger(arr))
	{
		arr = arange<T>(Convert.ToInt32(arr[0]));
	}

	shuffle(arr);
	return arr;
}

private bool IsInteger<T>(T[] arr)
{
	if (arr[0] is System.Byte)
		return true;
	if (arr[0] is System.SByte)
		return true;
	if (arr[0] is System.Int16)
		return true;
	if (arr[0] is System.UInt16)
		return true;
	if (arr[0] is System.Int32)
		return true;
	if (arr[0] is System.UInt32)
		return true;
	if (arr[0] is System.Int64)
		return true;
	if (arr[0] is System.UInt64)
		return true;
	return false;
}

private T[] arange<T>(int length)
{
	T[] arr = new T[length];

	if (arr[0] is System.Byte)
	{
		System.Byte[] _arr = arr as System.Byte[];
		for (System.Byte i = 0; i < length; i++)
		{
			_arr[i] = i;
		}
	}
	if (arr[0] is System.SByte)
	{
		System.SByte[] _arr = arr as System.SByte[];
		for (System.SByte i = 0; i < length; i++)
		{
			_arr[i] = i;
		}
	}
	if (arr[0] is System.Int16)
	{
		System.Int16[] _arr = arr as System.Int16[];
		for (System.Int16 i = 0; i < length; i++)
		{
			_arr[i] = i;
		}
	}
	if (arr[0] is System.UInt16)
	{
		System.UInt16[] _arr = arr as System.UInt16[];
		for (System.UInt16 i = 0; i < length; i++)
		{
			_arr[i] = i;
		}
	}
	if (arr[0] is System.Int32)
	{
		System.Int32[] _arr = arr as System.Int32[];
		for (System.Int32 i = 0; i < length; i++)
		{
			_arr[i] = i;
		}
	}
	if (arr[0] is System.UInt32)
	{
		System.UInt32[] _arr = arr as System.UInt32[];
		for (System.UInt32 i = 0; i < length; i++)
		{
			_arr[i] = i;
		}
	}
	if (arr[0] is System.Int64)
	{
		System.Int64[] _arr = arr as System.Int64[];
		for (System.Int64 i = 0; i < length; i++)
		{
			_arr[i] = i;
		}
	}
	if (arr[0] is System.Int64)
	{
		System.UInt64[] _arr = arr as System.UInt64[];
		for (System.UInt64 i = 0; i < (UInt64)length; i++)
		{
			_arr[i] = i;
		}
	}

	return arr;
}
#endif
#pragma endregion


#pragma region beta

double* Random::beta(double *a, int asize, double* b, int bsize, long size)
{

	if (any_less_equal(a, asize, 0))
	{
		throw "a <= 0";
	}
	if (any_less_equal(b, bsize, 0))
	{
		throw "b <= 0";
	}

	return cont2_array(internal_state, RandomDistributions::rk_beta, size, a, asize, b, bsize);

}

#pragma endregion

#pragma region binomial

long* Random::binomial(long n, double p, long size)
{
	return binomial(new long[1] { n }, 1, new double[1] { p }, 1, size);
}

long* Random::binomial(long* on, int onsize, double* op, int opsize, long size)
{
	long ln;
	double fp;

	ValidateSize(size);

	if (any_less(on,onsize, 0))
		throw "on < 0";
	if (any_less(op, opsize, 0))
		throw "op < 0";
	if (any_greater(op, opsize, 1))
		throw "op > 1";
	if (any_nan(op, opsize))
		throw "op is NAN";

	if (onsize == 1 && opsize == 1)
	{
		ln = on[0];
		fp = op[0];

		return discnp_array_sc(internal_state, RandomDistributions::rk_binomial, size, ln, fp);
	}

	return discnp_array(internal_state, RandomDistributions::rk_binomial, size, on, onsize, op, opsize);
}

long* Random::negative_binomial(long n, double p, long size)
{
	return negative_binomial(new long[1] { n }, 1, new double[1] { p }, 1, size);
}
long* Random::negative_binomial(long* on, int onsize, double* op, int opsize, long size)
{
	ValidateSize(size);

	if (any_less(on, onsize, 0))
		throw "on < 0";
	if (any_less(op, opsize, 0))
		throw "op < 0";
	if (any_greater(op, opsize, 1))
		throw "op > 1";
	if (any_nan(op, opsize))
		throw "op is NAN";

	if (onsize == 1 && opsize == 1)
	{
		return discdd_array_sc(internal_state, RandomDistributions::rk_negative_binomial, size, on[0], op[0]);
	}

	return discdd_array(internal_state, RandomDistributions::rk_negative_binomial, size, on, onsize, op, opsize);
}

#pragma endregion

#pragma region chisquare

double* Random::chisquare(double df, long size)
{
	return chisquare(new double[1] { df }, 1, size);
}

double * Random::chisquare(double *odf, int odfsize, long size)
{
	ValidateSize(size);


	if (any_less_equal(odf,odfsize, 0.0))
	{
		throw "odf <= 0.0";
	}

	if (odfsize == 1)
	{
		return cont1_array_sc(internal_state, RandomDistributions::rk_chisquare, size, odf[0]);
	}

	return cont1_array(internal_state, RandomDistributions::rk_chisquare, size, odf, odfsize);
}

double* Random::noncentral_chisquare(double* odf, int odfsize, double ononc, long size)
{
	return noncentral_chisquare(odf, odfsize, new double[1] { ononc }, 1, size);
}

double* Random::noncentral_chisquare(double odf, double ononc, long size)
{
	return noncentral_chisquare(new double[1] { odf }, 1, new double[1] { ononc }, 1, size);
}

double* Random::noncentral_chisquare(double* odf, int odfsize, double* ononc, int ononcsize, long size)
{
	ValidateSize(size);

	if (any_less_equal(odf, odfsize, 0.0))
	{
		throw "odf <= 0.0";
	}
	if (any_less_equal(ononc, ononcsize, 0.0))
	{
		throw "ononc <= 0.0";
	}

	if (odfsize == 1 && ononcsize == 1)
	{
		return cont2_array_sc(internal_state, RandomDistributions::rk_noncentral_chisquare, size, odf[0], ononc[0]);
	}

	return cont2_array(internal_state, RandomDistributions::rk_noncentral_chisquare, size, odf, odfsize, ononc, ononcsize);
}

#pragma endregion

#pragma region standard_normal


double Random::standard_normal()
{
	double* rndArray = cont0_array(internal_state, RandomDistributions::rk_gauss, 0);
	return rndArray[0];
}

double* Random::standard_normal(long size)
{
	double* rndArray = cont0_array(internal_state, RandomDistributions::rk_gauss, size);
	return rndArray;
}

#pragma endregion


#pragma region array builders

double* Random::cont0_array(rk_state *state, double(*func)(rk_state *state), long size)
{
	double* array_data;
	long i;

	if (size == 0)
	{
		lock(rk_lock);
		{
			double rv = func(state);
			return new double[1]{ rv };
		}
	}
	else
	{
		array_data = new double[size];
		lock(rk_lock);
		{
			for (i = 0; i < size; i++)
			{
				array_data[i] = func(state);
			}
		}
		return array_data;
	}

}

double* Random::cont1_array(rk_state *state, double(*func)(rk_state *state, double), long size, double* oa, int oasize)
{
	double* array_data;
	double* oa_data;

	if (size == 0)
	{
		array_data = new double[oasize];

		oa_data = oa;
		for (long i = 0; i < oasize; i++)
		{
			array_data[i] = func(state, oa_data[i]);
		}
	}
	else
	{
		array_data = new double[size];

		oa_data = oa;
		if (size != size)
		{
			throw ("size is not compatible with inputs");
		}

		for (long i = 0; i < size; i++)
		{
			array_data[i] = func(state, oa_data[i]);
		}
	}

	return array_data;
}

double* Random::cont1_array_sc(rk_state *state, double(*func)(rk_state *state, double), long size, double a)
{
	if (size == 0)
	{
		double rv = func(state, a);
		return new double[1] { rv };
	}
	else
	{
		double* array_data = new double[size];
		lock(rk_lock);
		{
			for (int i = 0; i < size; i++)
			{
				array_data[i] = func(state, a);
			}
		}
		return array_data;
	}
}

double* Random::cont2_array_sc(rk_state *state, double(*func)(rk_state *state, double, double), long size, double a, double b)
{
	if (size == 0)
	{
		double rv = func(state, a, b);
		return new double[1] { rv };
	}
	else
	{
		double *array_data = new double[size];
		lock(rk_lock);
		{
			for (int i = 0; i < size; i++)
			{
				array_data[i] = func(state, a, b);
			}
		}
		return array_data;
	}

}

double* Random::cont2_array(rk_state *state, double(*func)(rk_state *state, double, double), long size, double* oa, int oasize, double* ob, int obsize)
{
	double* array_data;

	if (size == 0)
	{
		array_data = new double[oasize];
		size = oasize;
	}
	else
	{
		if (size != oasize)
		{
			throw "size is not compatible with inputs";
		}

		array_data = new double[size];
	}


	int oa_index = 0;
	int ob_index = 0;
	for (int i = 0; i < size; i++)
	{
		array_data[i] = func(state, oa[oa_index++], ob[ob_index++]);
		if (oa_index >= oasize)
			oa_index = 0;
		if (ob_index >= obsize)
			ob_index = 0;
	}

	return array_data;

}

double* Random::cont3_array_sc(rk_state *state, double(*func)(rk_state *state, double, double, double), long size, double a, double b, double c)
{
	if (size == 0)
	{
		double rv = func(state, a, b, c);
		return new double[1] { rv };
	}
	else
	{
		double *array_data = new double[size];
		lock(rk_lock);
		{
			for (int i = 0; i < size; i++)
			{
				array_data[i] = func(state, a, b, c);
			}
		}
		return array_data;
	}
}

double* Random::cont3_array(rk_state *state, double(*func)(rk_state *state, double, double, double), long size, double* oa, int oasize, double* ob, int obsize, double* oc, int ocsize)
{
	double* array_data;
	int array_data_len = 0;

	if (size == 0)
	{
		array_data_len = oasize;
		array_data = new double[oasize];
	}
	else
	{
		array_data_len = size;
		array_data = new double[size];
	}

	int oa_index = 0;
	int ob_index = 0;
	int oc_index = 0;
	for (int i = 0; i < array_data_len; i++)
	{
		array_data[i] = func(state, oa[oa_index++], ob[ob_index++], oc[oc_index++]);
		if (oa_index >= oasize)
			oa_index = 0;
		if (ob_index >= obsize)
			ob_index = 0;
		if (oc_index >= ocsize)
			oc_index = 0;
	}

	return array_data;
}

long* Random::discd_array(rk_state *state, long(*func)(rk_state *state, double), long size, double* oa, int oasize)
{
	long* array_data;
	int array_data_length = 0;

	if (size == 0)
	{
		array_data_length = oasize;
		array_data = new long[oasize];
	}
	else
	{
		array_data_length = size;
		array_data = new long[size];
	}

	int oa_index = 0;
	for (int i = 0; i < array_data_length; i++)
	{
		array_data[i] = func(state, oa[oa_index++]);
		if (oa_index >= oasize)
			oa_index = 0;
	}

	return array_data;

}

long* Random::discd_array_sc(rk_state *state, long(*func)(rk_state *state, double), long size, double a)
{
	if (size == 0)
	{
		long rv = func(state, a);
		return new long[1] { rv };
	}

	long *array_data = new long[size];
	int length = size;
	for (int i = 0; i < length; i++)
	{
		array_data[i] = func(state, a);
	}

	return array_data;
}

long* Random::discnp_array(rk_state *state, long(*func)(rk_state *state, long, double), long size, long* on, int onsize, double* op, int opsize)
{
	long* array_data;
	int array_data_length;

	if (size == 0)
	{
		array_data_length = onsize;
		array_data = new long[onsize];
	}
	else
	{
		array_data_length = size;
		array_data = new long[size];
	}

	int on_index = 0;
	int op_index = 0;
	for (int i = 0; i < array_data_length; i++)
	{
		array_data[i] = func(state, on[on_index++], op[op_index++]);
		if (on_index >= onsize)
			on_index = 0;
		if (op_index >= opsize)
			op_index = 0;
	}

	return array_data;

}

long* Random::discnp_array_sc(rk_state *state, long(*func)(rk_state *state, long, double), long size, long n, double p)
{
	if (size == 0)
	{
		long rv = func(state, n, p);
		return new long[1] { rv };
	}

	long* array_data = new long[size];

	for (int i = 0; i < size; i++)
	{
		array_data[i] = func(state, n, p);
	}

	return array_data;
}

long* Random::discdd_array(rk_state *state, long(*func)(rk_state *state, double, double), long size, long* on, int onsize, double* op, int opsize)
{
	long* array_data;
	int array_data_length = 0;

	if (size == 0)
	{
		array_data_length = onsize;
		array_data = new long[onsize];
	}
	else
	{
		array_data_length = size;
		array_data = new long[size];
	}

	int on_index = 0;
	int op_index = 0;
	for (int i = 0; i < array_data_length; i++)
	{
		array_data[i] = func(state, on[on_index++], op[op_index++]);
		if (on_index >= onsize)
			on_index = 0;
		if (op_index >= opsize)
			op_index = 0;
	}

	return array_data;

}

long* Random::discdd_array_sc(rk_state *state, long(*func)(rk_state *state, double, double), long size, long n, double p)
{
	if (size == 0)
	{
		long rv = func(state, n, p);
		return new long[1] { rv };
	}

	long* array_data = new long[size];

	for (int i = 0; i < size; i++)
	{
		array_data[i] = func(state, n, p);
	}

	return array_data;
}

long* Random::discnmN_array(rk_state *state, long(*func)(rk_state *state, long, long, long), long size, long* on, int onsize, long* om, int omsize, long* oN, int oNsize)
{
	long* array_data;
	int array_data_length;

	if (size == 0)
	{
		array_data_length = onsize;
		array_data = new long[onsize];
	}
	else
	{
		array_data_length = size;
		array_data = new long[size];
	}

	int on_index = 0;
	int om_index = 0;
	int oN_index = 0;
	for (int i = 0; i < array_data_length; i++)
	{
		array_data[i] = func(state, on[on_index++], om[om_index++], oN[oN_index++]);
		if (on_index >= onsize)
			on_index = 0;
		if (om_index >= omsize)
			om_index = 0;
		if (oN_index >= oNsize)
			oN_index = 0;
	}

	return array_data;
}

long* Random::discnmN_array_sc(rk_state *state, long(*func)(rk_state *state, long, long, long), long size, long n, long m, long N)
{
	if (size == 0)
	{
		long rv = func(state, n, m, N);
		return new long[1] { rv };
	}

	long* array_data = new long[size];

	for (int i = 0; i < size; i++)
	{
		array_data[i] = func(state, n, m, N);
	}

	return array_data;
}

#pragma endregion

#pragma region Utility functions

double Random::kahan_sum(double* darr, long n)
{
	double c, y, t, sum;
	long i;
	sum = darr[0];
	c = 0.0;
	for (i = 0; i < n; i++)
	{
		y = darr[i] - c;
		t = sum + y;
		c = (t - sum) - y;
		sum = t;
	}

	return sum;
}

bool Random::any_less(double* array, int array_len, double value)
{
	for (int i = 0; i < array_len; i++)
	{
		if (array[i] < value)
			return true;
	}
	return false;
}

bool Random::any_less_equal(double* array, int array_len, double value)
{
	for (int i = 0; i < array_len; i++)
	{
		if (array[i] <= value)
			return true;
	}
	return false;
}

bool Random::any_greater(double* array, int array_len, double value)
{
	for (int i = 0; i < array_len; i++)
	{
		if (array[i] > value)
			return true;
	}
	return false;
}

bool Random::any_greater_equal(double* array, int array_len, double value)
{
	for (int i = 0; i < array_len; i++)
	{
		if (array[i] >= value)
			return true;
	}
	return false;
}


bool Random::any_less(long* array, int array_len, int value)
{
	for (int i = 0; i < array_len; i++)
	{
		if (array[i] < value)
			return true;
	}
	return false;
}

bool Random::any_nan(double* array, int array_len)
{
	for (int i = 0; i < array_len; i++)
	{
		if (isnan(array[i]))
			return true;
	}
	return false;
}

bool Random::any_signbit(double* array, int array_len)
{
	for (int i = 0; i < array_len; i++)
	{
		if (array[i] < 0)
			return true;
	}
	return false;
}

bool Random::any_less(long* arr1, int arr1_length, long* arr2, int arr2_length)
{
	int arr2_index = 0;
	for (int i = 0; i < arr1_length; i++)
	{
		if (arr1[i] < arr2[arr2_index++])
			return true;
		if (arr2_index >= arr2_length)
			arr2_index = 0;
	}

	return false;
}

bool Random::any_equal(double* arr1, int arr1_length, double* arr2, int arr2_length)
{
	int arr2_index = 0;
	for (int i = 0; i < arr1_length; i++)
	{
		if (arr1[i] == arr2[arr2_index++])
			return true;
		if (arr2_index >= arr2_length)
			arr2_index = 0;
	}
	return false;
}

bool Random::any_greater(double* arr1, int arr1_length, double* arr2, int arr2_length)
{
	int arr2_index = 0;
	for (int i = 0; i < arr1_length; i++)
	{
		if (arr1[i] > arr2[arr2_index++])
			return true;
		if (arr2_index >= arr2_length)
			arr2_index = 0;
	}
	return false;
}

long* Random::add(long* arr1, int arr1_length, long* arr2, int arr2_length)
{
	long* sum = new long[arr1_length];

	int arr2_index = 0;
	for (int i = 0; i < arr1_length; i++)
	{
		sum[i] = arr1[i] + arr2[arr2_index++];
		if (arr2_index >= arr2_length)
			arr2_index = 0;
	}

	return sum;
}

double* Random::subtract(double* arr1, int arr1_length, double* arr2, int arr2_length)
{
	double* sub = new double[arr1_length];

	int arr2_index = 0;
	for (int i = 0; i < arr1_length; i++)
	{
		sub[i] = arr1[i] - arr2[arr2_index++];
		if (arr2_index >= arr2_length)
			arr2_index = 0;
	}

	return sub;
}

bool Random::any_isfinite(double* array, int array_length)
{
	for (int i = 0; i < array_length; i++)
	{
		if (isinf(array[i]) || isnan(array[i]))
			return true;
	}
	return false;
}


void Random::lock(void *l)
{

}


#pragma endregion





