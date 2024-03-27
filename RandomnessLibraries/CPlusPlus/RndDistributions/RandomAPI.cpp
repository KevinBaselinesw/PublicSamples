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

double Random::random_sample()
{
	double *rndArray = cont0_array(internal_state, RandomDistributions::rk_double, 0);
	double retVal = rndArray[0];
	delete rndArray;
	return retVal;
}

double* Random::random_sample(long size)
{
	double* rndArray = cont0_array(internal_state, RandomDistributions::rk_double, size);
	return rndArray;
}

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

double* Random::cont1_array(rk_state *state, double(*func)(rk_state *state, double), long size, double* oa)
{
	double* array_data;
	double* oa_data;

	if (size == 0)
	{
		array_data = new double[size];

		oa_data = oa;
		for (long i = 0; i < size; i++)
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





