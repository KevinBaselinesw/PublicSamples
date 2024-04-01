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
	allocatedDefaultGenerator = false;
	if (rndGenerator == NULL)
	{
		rndGenerator = new RandomState();
		allocatedDefaultGenerator = true;
	}
	rk_lock = (void *)(new char[1]);
	internal_state = new rk_state(rndGenerator);
	(void)seed(NULL);
	_rndGenerator = rndGenerator;

    return;
}

Random::~Random()
{
	delete []rk_lock;
	delete internal_state;

	if (allocatedDefaultGenerator)
	{
		delete _rndGenerator;
	}
}


#pragma region seed


bool Random::seed()
{
	time_t now = time(0);

	long _seed = (long)now;
	
	(void)seed(_seed);

	double tmp1 = rand();

	_seed += (long)tmp1;
	(void)seed(_seed);
	tmp1 = rand();
	tmp1 = rand();

	_seed ^= (long)tmp1;

	(void)seed(_seed);

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

	if (low < SCHAR_MIN)
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

	if (low < LONG_MIN)
		throw "low is out of bounds for long";
	if (high > LONG_MAX)
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
	if (high > ULONG_MAX)
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


void Random::shuffle(int x)
{
	int _data[1];
	_data[0] = x;

	shuffle(_data, 1);
}

void Random::shuffle(int * x, int xsize)
{
	int n = xsize;

	int buf = 0;

	for (ulong i = (ulong)n - 1; i >= 1; i--)
	{
		ulong j = RandomDistributions::rk_interval(i, internal_state);
		buf = x[j];
		x[j] = x[i];
		x[i] = buf;
	}
	return;
}

int * Random::permutation(int arr)
{
	int _data[1];
	_data[0] = arr;
	return permutation(_data, 1);
}

int * Random::permutation(int * arr, int arrsize)
{
	if (arrsize == 1)
	{
		arrsize = arr[0];
		arr = new int[arrsize];
		for (int i = 0; i < arrsize; i++)
		{
			arr[i] = i;
		}
	}

	shuffle(arr, arrsize);
	return arr;
}





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
	long on[1]; on[0] = n;
	double op[1]; op[0] = p;
	return binomial(on, 1, op, 1, size);
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
	long on[1]; on[0] = n;
	double op[1]; op[0] = p;
	return negative_binomial(on, 1, op, 1, size);
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
	double _df[1]; _df[0] = df;
	return chisquare(_df, 1, size);
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
	double _ononc[1]; _ononc[0] = ononc;
	return noncentral_chisquare(odf, odfsize, _ononc, 1, size);
}

double* Random::noncentral_chisquare(double odf, double ononc, long size)
{
	double _odf[1]; _odf[0] = odf;
	double _ononc[1]; _ononc[0] = ononc;
	return noncentral_chisquare(_odf, 1, _ononc, 1, size);
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

#pragma region exponential

double* Random::exponential(double scale, long size)
{
	double _scale[1]; _scale[0] = scale;
	return exponential(_scale, 1, size);
}

double* Random::exponential(double* oscale, int oscalesize, long size)
{
	ValidateSize(size);

	if (any_signbit(oscale, oscalesize))
	{
		throw "scale < 0";
	}

	if (oscalesize == 1)
	{
		return cont1_array_sc(internal_state, RandomDistributions::rk_exponential, size, oscale[0]);
	}

	return cont1_array(internal_state, RandomDistributions::rk_exponential, size, oscale, oscalesize);

}

#pragma endregion

#pragma region f distribution



double* Random::f(double dfnum, double dfden, long size)
{
	double _dfnum[1]; _dfnum[0] = dfnum;
	double _dfden[1]; _dfden[0] = dfden;

	return f(_dfnum, 1, _dfden, 1, size);
}

double* Random::f(double *odfnum, int odfnumsize, double* odfden, int odfdensize, long size)
{
	ValidateSize(size);

	if (any_less_equal(odfnum, odfnumsize, 0.0))
		throw "odfnum < 0";
	if (any_less_equal(odfden, odfdensize, 0.0))
		throw "odfden < 0";

	if (odfnumsize == 1 && odfdensize == 1)
	{
		return cont2_array_sc(internal_state, RandomDistributions::rk_f, size, odfnum[0], odfden[0]);
	}

	return cont2_array(internal_state, RandomDistributions::rk_f, size, odfnum, odfnumsize, odfden, odfdensize);
}

#pragma endregion

#pragma region gamma

double* Random::gamma(double shape, double scale, long size)
{
	double _shape[1]; _shape[0] = shape;
	double _scale[1]; _scale[0] = scale;
	return gamma(_shape, 1, _scale, 1, size);
}

double* Random::gamma(double* oshape, int oshapesize, double* oscale, int oscalesize, long size)
{
	ValidateSize(size);

	if (any_signbit(oshape, oshapesize))
		throw "oshape < 0";

	if (any_signbit(oscale, oscalesize))
		throw "oscale < 0";

	if (oshapesize == 1 && oscalesize == 1)
	{
		return cont2_array_sc(internal_state, RandomDistributions::rk_gamma, size, oshape[0], oscale[0]);
	}

	return cont2_array(internal_state, RandomDistributions::rk_gamma, size, oshape, oshapesize, oscale, oscalesize);
}

#pragma endregion

#pragma region geometric

long* Random::geometric(double op, long size)
{
	double _op[1]; _op[0] = op;
	return geometric(_op, 1, size);
}

long* Random::geometric(double* op, int opsize, long size)
{
	ValidateSize(size);

	if (any_less(op, opsize, 0.0))
		throw "op < 0";
	if (any_greater(op, opsize, 1.0))
		throw "op > 0";

	if (opsize == 1)
	{
		return discd_array_sc(internal_state, RandomDistributions::rk_geometric, size, op[0]);
	}


	return discd_array(internal_state, RandomDistributions::rk_geometric, size, op, opsize);
}

#pragma endregion

#pragma region gumbel

double* Random::gumbel(double* oloc, int olocsize, double oscale, long size)
{
	double _oscale[1]; _oscale[0] = oscale;
	return gumbel(oloc, olocsize, _oscale, 1,size);
}

double* Random::gumbel(double oloc, double* oscale, int oscalesize, long size)
{
	double _oloc[1]; _oloc[0] = oloc;
	return gumbel(_oloc, 1, oscale, oscalesize, size);
}

double* Random::gumbel(double oloc, double oscale, long size)
{
	double _oloc[1]; _oloc[0] = oloc;
	double _oscale[1]; _oscale[0] = oscale;
	return gumbel(_oloc, 1, _oscale, 1, size);
}

double* Random::gumbel(double* oloc, int olocsize, double *oscale, int oscalesize, long size)
{
	ValidateSize(size);

	double _default_oscale[1] = { 1.0 };

	if (oscale == NULL)
		oscale = _default_oscale;

	if (any_signbit(oscale, oscalesize))
		throw "oscale < 0";


	if (olocsize == 1 && oscalesize == 1)
	{
		return cont2_array_sc(internal_state, RandomDistributions::rk_gumbel, size, oloc[0], oscale[0]);
	}

	return cont2_array(internal_state, RandomDistributions::rk_gumbel, size, oloc, olocsize, oscale, oscalesize);
}

#pragma endregion

#pragma region hypergeometric

long* Random::hypergeometric(long ongood, long onbad, long onsample, long size)
{
	long _ongood[1] = { ongood };
	long _onbad[1] = { onbad };
	long _onsample[1] = { onsample };

	return hypergeometric(_ongood, 1, _onbad, 1, _onsample, 1, size);
}

long* Random::hypergeometric(long* ongood, int ongoodsize, long* onbad, int onbadsize, long* onsample, int onsamplesize, long size)
{
	if (any_less(ongood, ongoodsize, 0))
		throw "ongood < 0";
	if (any_less(onbad, onbadsize, 0))
		throw "onbad < 0";
	if (any_less(onsample, onsamplesize, 1))
		throw "onsample < 1";
	if (any_less(add(ongood, ongoodsize, onbad, onbadsize), ongoodsize, onsample, onsamplesize))
		throw "ngood + nbad < nsample";

	if (ongoodsize == 1 && onbadsize == 1 && onsamplesize == 1)
	{
		return discnmN_array_sc(internal_state, RandomDistributions::rk_hypergeometric, size, ongood[0], onbad[0], onsample[0]);
	}

	return discnmN_array(internal_state, RandomDistributions::rk_hypergeometric, size, ongood, ongoodsize, onbad, onbadsize, onsample, onsamplesize);
}

#pragma endregion

#pragma region laplace

double* Random::laplace(double oloc, double oscale, long size)
{
	double _oloc[1] = { oloc };
	double _oscale[1] = { oscale };
	return laplace(_oloc, 1, _oscale, 1, size);
}

double* Random::laplace(double *oloc, int olecsize, double oscale, long size)
{
	double _oscale[1] = { oscale };
	return laplace(oloc, olecsize, _oscale, 1, size);
}

double* Random::laplace(double oloc, double* oscale, int oscalesize, long size)
{
	double _oloc[1] = { oloc };
	return laplace(_oloc, 1, oscale, oscalesize, size);
}

double* Random::laplace(double *oloc, int olocsize, double *oscale, int oscalsize, long size)
{
	double _default_oscale[1] = { 1.0 };

	if (oscale == NULL)
		oscale = _default_oscale;

	if (any_signbit(oscale, oscalsize))
		throw "oscale < 0";

	if (olocsize == 1 && oscalsize == 1)
	{
		return cont2_array_sc(internal_state, RandomDistributions::rk_laplace, size, oloc[0], oscale[0]);
	}

	return cont2_array(internal_state, RandomDistributions::rk_laplace, size, oloc, olocsize, oscale, oscalsize);
}

#pragma endregion

#pragma region logistic

double* Random::logistic(double oloc, double oscale, long size)
{
	double _oloc[1] = { oloc };
	double _oscale[1] = { oscale };

	return logistic(_oloc, 1, _oscale, 1, size);
}

double* Random::logistic(double* oloc, int olocsize, double oscale, long size)
{
	double _oscale[1] = { oscale };

	return logistic(oloc, olocsize, _oscale, 1, size);
}

double* Random::logistic(double oloc)
{
	double _oloc[1] = { oloc };

	return logistic(_oloc, 1, NULL, 0, 0);
}

double* Random::logistic(double *oloc, int olocsize, double *oscale, int oscalesize, long size)
{

	double _default_scale[1] = { 1.0 };

	if (oscale == NULL)
	{
		oscale = _default_scale;
		oscalesize = 1;
	}

	if (any_signbit(oscale, oscalesize))
		throw "oscale < 0";

	if (olocsize == 1 && oscalesize == 1)
	{
		return cont2_array_sc(internal_state, RandomDistributions::rk_logistic, size, oloc[0], oscale[0]);
	}

	return cont2_array(internal_state, RandomDistributions::rk_logistic, size, oloc, olocsize, oscale, oscalesize);
}

#pragma endregion

#pragma region lognormal

double* Random::lognormal(double omean, double osigma, long size)
{
	double _omean[1] = { omean };
	double _osigma[1] = { osigma };

	return lognormal(_omean, 1, _osigma, 1, size);
}

double* Random::lognormal(double* omean, int omeansize, double osigma, long size)
{
	double _osigma[1] = { osigma };

	return lognormal(omean, omeansize, _osigma, 1, size);
}

double* Random::lognormal(double* omean, int omeansize, double* osigma, int osigmansize, long size)
{
	if (any_signbit(osigma, osigmansize))
		throw "osigma < 0";


	if (omeansize == 1 && osigmansize == 1)
	{
		return cont2_array_sc(internal_state, RandomDistributions::rk_lognormal, size, omean[0], osigma[0]);
	}


	return cont2_array(internal_state, RandomDistributions::rk_lognormal, size, omean, omeansize, osigma, osigmansize);
}

#pragma endregion

#pragma region logseries

long* Random::logseries(double op, long size)
{
	double _op[1] = { op };
	return logseries(_op, 1, size);
}

long* Random::logseries(double* op, int opsize, long size)
{

	ValidateSize(size);

	if (any_less_equal(op, opsize, 0.0))
		throw "op <= 0";
	if (any_greater_equal(op, opsize, 1.0))
		throw "op >= 1.0";

	if (opsize == 1)
	{
		return discd_array_sc(internal_state, RandomDistributions::rk_logseries, size, op[0]);
	}

	return discd_array(internal_state, RandomDistributions::rk_logseries, size, op, opsize);
}

#pragma endregion

#pragma region multinomial


long* Random::multinomial(int n, double* pvals, int pvalsize, long size)
{
	long d = pvalsize;

	if (kahan_sum(pvals, d - 1) > (1.0 + 1e-12))
	{
		throw "sum(pvals[:-1]) > 1.0";
	}

	int multinLength = size != 0 ? size * d : d;
	long *multin = new long[multinLength];
	memset(multin, 0, multinLength * sizeof(long));

	long *mnix = multin;
	long sz = multinLength;

	long i = 0;
	while (i < sz)
	{
		double Sum = 1.0;
		long dn = n;
		for (long j = 0; j < d - 1; j++)
		{
			mnix[i + j] = RandomDistributions::rk_binomial(internal_state, dn, pvals[j] / Sum);
			dn = dn - mnix[i + j];
			if (dn <= 0)
				break;
			Sum = Sum - pvals[j];
		}

		if (dn > 0)
			mnix[i + d - 1] = dn;

		i = i + d;

	}

	return multin;
}

#pragma endregion

#pragma region noncentral_f

double* Random::noncentral_f(double odfnum, double odfden, double ononc, long size)
{
	double _odfnum[1] = { odfnum };
	double _odfden[1] = { odfden };
	double _ononc[1] = { ononc };

	return noncentral_f(_odfnum,1, _odfden,1, _ononc, 1, size);
}


double* Random::noncentral_f(double* odfnum, int odfnumsize, double odfden, double ononc, long size)
{
	double _odfden[1] = { odfden };
	double _ononc[1] = { ononc };

	return noncentral_f(odfnum, odfnumsize, _odfden, 1, _ononc, 1, size);
}

double* Random::noncentral_f(double* odfnum, int odfnumsize, double* odfden, int odfdensize, double* ononc, int ononcsize, long size)
{

	ValidateSize(size);

	if (any_less_equal(odfnum, odfnumsize, 0.0))
		throw "odfnum < 0";
	if (any_less_equal(odfden, odfdensize, 0.0))
		throw "odfden < 0";
	if (any_less_equal(ononc, ononcsize, 0.0))
		throw "ononc < 0";

	if (odfnumsize == 1 && odfdensize == 1 && ononcsize == 1)
	{
		return cont3_array_sc(internal_state, RandomDistributions::rk_noncentral_f, size, odfnum[0], odfden[0], ononc[0]);
	}

	return cont3_array(internal_state, RandomDistributions::rk_noncentral_f, size, odfnum, odfnumsize, odfden, odfdensize, ononc, ononcsize);
}

#pragma endregion

#pragma region normal

double* Random::normal(double oloc, double oscale, long size)
{
	double _oloc[1] = { oloc };
	double _oscale[1] = { oscale };

	return normal(_oloc, 1, _oscale, 1, size);
}

double* Random::normal(double* oloc, int olecsize, double oscale, long size)
{
	double _oscale[1] = { oscale };

	return normal(oloc, olecsize, _oscale, 1, size);
}

double* Random::normal(double* oloc, int olecsize, double* oscale, int oscalsize, long size)
{
	ValidateSize(size);

	if (any_signbit(oscale, oscalsize))
		throw "oscale < 0";

	if (olecsize == 1 && oscalsize == 1)
	{
		return cont2_array_sc(internal_state, RandomDistributions::rk_normal, size, oloc[0], oscale[0]);
	}

	return cont2_array(internal_state, RandomDistributions::rk_normal, size, oloc, olecsize, oscale, oscalsize);
}

#pragma endregion

#pragma region pareto

double* Random::pareto(double oa, long size)
{
	double _oa[1] = { oa };

	return pareto(_oa, 1, size);
}

double* Random::pareto(double* oa, int oasize, long size)
{
	ValidateSize(size);

	if (any_less_equal(oa, oasize, 0.0))
		throw "oa < 0";

	if (oasize == 1)
	{
		return cont1_array_sc(internal_state, RandomDistributions::rk_pareto, size, oa[0]);
	}

	return cont1_array(internal_state, RandomDistributions::rk_pareto, size, oa, oasize);
}

#pragma endregion

#pragma region Poisson

long* Random::poisson(double olam, long size)
{
	double _olam[1] = { olam };

	return poisson(_olam, 1, size);
}
long* Random::poisson(double* olam, int olamsize, long size)
{
	const double poisson_lam_max = 9.223372006484771e+18;

	if (any_less(olam, olamsize, 0.0))
		throw "olam < 0";
	if (any_greater(olam, olamsize, poisson_lam_max))
		throw "lam value too large";

	if (olamsize == 1)
	{
		return discd_array_sc(internal_state, RandomDistributions::rk_poisson, size, olam[0]);
	}

	return discd_array(internal_state, RandomDistributions::rk_poisson, size, olam, olamsize);
}

#pragma endregion

#pragma region Power

double* Random::power(double oa, long size)
{
	double _oa[1] = { oa };

	return power(_oa, 1, size);
}
double* Random::power(double* oa, int oasize, long size)
{
	ValidateSize(size);

	if (any_signbit(oa, oasize))
		throw "oa < 0";

	if (oasize == 1)
	{
		return cont1_array_sc(internal_state, RandomDistributions::rk_power, size, oa[0]);
	}

	return cont1_array(internal_state, RandomDistributions::rk_power, size, oa, oasize);
}

#pragma endregion

#pragma region rayleigh

double* Random::rayleigh(double oscale, long size)
{
	double _oscale[1] = { oscale };

	return rayleigh(_oscale, 1, size);
}
double* Random::rayleigh(double* oscale, int oscalesize, long size)
{
	ValidateSize(size);

	if (any_signbit(oscale, oscalesize))
		throw "oscale < 0";

	if (oscalesize == 1)
	{
		return cont1_array_sc(internal_state, RandomDistributions::rk_rayleigh, size, oscale[0]);
	}

	return cont1_array(internal_state, RandomDistributions::rk_rayleigh, size, oscale, oscalesize);
}
#pragma endregion

#pragma region standard_cauchy

double Random::standard_cauchy()
{
	double* rndArray = cont0_array(internal_state, RandomDistributions::rk_standard_cauchy, 0);
	double retVal = rndArray[0];
	delete rndArray;
	return retVal;
}


double* Random::standard_cauchy(long size)
{
	double* rndArray = cont0_array(internal_state, RandomDistributions::rk_standard_cauchy, size);
	return rndArray;
}

#pragma endregion

#pragma region standard_exponential

double Random::standard_exponential()
{
	double* rndArray = cont0_array(internal_state, RandomDistributions::rk_standard_exponential, 0);
	double retVal = rndArray[0];
	delete rndArray;
	return retVal;
}


double* Random::standard_exponential(long size)
{
	double* rndArray = cont0_array(internal_state, RandomDistributions::rk_standard_exponential, size);
	return rndArray;
}

#pragma endregion

#pragma region standard_gamma

double* Random::standard_gamma(double oshape, long size)
{
	double _oshape[1] = { oshape };
	return standard_gamma(_oshape, 1, size);
}

double* Random::standard_gamma(double* oshape, int oshapesize, long size)
{
	ValidateSize(size);

	if (any_signbit(oshape, oshapesize))
		throw "oshape < 0";

	if (oshapesize == 1)
	{
		return cont1_array_sc(internal_state, RandomDistributions::rk_standard_gamma, size, oshape[0]);
	}

	return cont1_array(internal_state, RandomDistributions::rk_standard_gamma, size, oshape, oshapesize);
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

#pragma region standard_t

double* Random::standard_t(double odf, long size)
{
	double _odf[1] = { odf };

	return standard_t(_odf, 1, size);
}

double* Random::standard_t(double* odf, int odfsize, long size)
{
	ValidateSize(size);

	if (any_less_equal(odf, odfsize, 0.0))
		throw "odf < 0";

	if (odfsize == 1)
	{
		return cont1_array_sc(internal_state, RandomDistributions::rk_standard_t, size, odf[0]);
	}

	return cont1_array(internal_state, RandomDistributions::rk_standard_t, size, odf, odfsize);
}

#pragma endregion

#pragma region triangular

double* Random::triangular(double oleft, double omode, double oright, long size)
{
	double _oleft[1] = { oleft };
	double _omode[1] = { omode };
	double _oright[1] = { oright };

	return triangular(_oleft,1, _omode, 1, _oright, 1, size);
}
double* Random::triangular(double *oleft, int oleftsize, double omode, double oright, long size)
{
	double _omode[1] = { omode };
	double _oright[1] = { oright };

	return triangular(oleft, oleftsize, _omode, 1, _oright, 1, size);
}
double* Random::triangular(double *oleft, int oleftsize, double* omode, int omodesize, double oright, long size)
{
	double _oright[1] = { oright };

	return triangular( oleft, oleftsize, omode, omodesize, _oright, 1, size);
}
double* Random::triangular(double* oleft, int oleftsize, double* omode, int omodesize, double* oright, int orightsize, long size)
{
	ValidateSize(size);

	if (any_greater(oleft, oleftsize, omode, omodesize))
		throw "left > mode";
	if (any_greater(omode, omodesize, oright, orightsize))
		throw "mode > right";
	if (any_equal(oleft, oleftsize, oright, orightsize))
		throw "left == right";

	if (oleftsize == 1 && omodesize == 1 && orightsize == 1)
	{
		return cont3_array_sc(internal_state, RandomDistributions::rk_triangular, size, oleft[0], omode[0], oright[0]);
	}

	return cont3_array(internal_state, RandomDistributions::rk_triangular, size, oleft, oleftsize, omode, omodesize, oright, orightsize);
}

#pragma endregion

#pragma region uniform

double* Random::uniform(double low, double high, long size)
{
	ValidateSize(size);

	double lowarr[1] = { low };
	double higharr[1] = { high };

	return uniform(lowarr, 1,higharr, 1, size);
}

double* Random::uniform(double* olow, int olowsize, double* ohigh, int ohighsize, long size)
{
	ValidateSize(size);

	int odiffsize = ohighsize;
	double* odiff = subtract(ohigh, ohighsize, olow, olowsize);
	if (any_isfinite(odiff, odiffsize))
	{
		delete odiff;
		throw "Range exceeds valid bounds";
	}

	if (olowsize == 1 && ohighsize == 1)
	{
		delete odiff;
		return cont2_array_sc(internal_state, RandomDistributions::rk_uniform, size, olow[0], ohigh[0] - olow[0]);
	}

	double *retval= cont2_array(internal_state, RandomDistributions::rk_uniform, size, olow, olowsize, odiff, odiffsize);
	delete odiff;
	return retval;
}

#pragma endregion

#pragma region vonmises

double* Random::vonmises(double omu, double okappa, long size)
{
	double _omu[1] = { omu };
	double _okappa[1] = { okappa };

	return vonmises(_omu, 1, _okappa, 1, size);
}
double* Random::vonmises(double* omu, int omusize, double okappa, long size)
{
	double _okappa[1] = { okappa };

	return vonmises(omu, omusize, _okappa, 1, size);
}
double* Random::vonmises(double* omu, int omusize, double* okappa, int okappasize, long size)
{
	if (any_less(okappa, okappasize, 0.0))
		throw "okappa < 0";

	if (omusize == 1 && okappasize == 1)
	{
		return cont2_array_sc(internal_state, RandomDistributions::rk_vonmises, size, omu[0], okappa[0]);
	}

	return cont2_array(internal_state, RandomDistributions::rk_vonmises, size, omu, omusize, okappa, okappasize);
}

#pragma endregion

#pragma region wald

double* Random::wald(double omean, double oscale, long size)
{
	double _omean[1] = { omean };
	double _oscale[1] = { oscale };

	return wald(_omean,1, _oscale, 1, size);
}
double* Random::wald(double* omean, int omeansize, double oscale, long size)
{
	double _oscale[1] = { oscale };

	return wald(omean, omeansize, _oscale, 1, size);
}
double* Random::wald(double* omean, int omeansize, double* oscale, int oscalesize, long size)
{
	if (any_less_equal(omean, omeansize, 0.0))
		throw "omean <= 0";

	if (any_less_equal(oscale, oscalesize, 0.0))
		throw "oscale <= 0";

	if (omeansize == 1 && oscalesize == 1)
	{
		return cont2_array_sc(internal_state, RandomDistributions::rk_wald, size, omean[0], oscale[0]);
	}

	return cont2_array(internal_state, RandomDistributions::rk_wald, size, omean, omeansize, oscale, oscalesize);
}

#pragma endregion

#pragma region weibull

double* Random::weibull(double oa, long size)
{
	double _oa[1] = { oa };

	return weibull(_oa, 1, size);
}
double* Random::weibull(double* oa, int oasize, long size)
{
	if (any_signbit(oa, oasize))
		throw "oa <= 0";

	if (oasize == 1)
	{
		return cont1_array_sc(internal_state, RandomDistributions::rk_weibull, size, oa[0]);
	}

	return cont1_array(internal_state, RandomDistributions::rk_weibull, size, oa, oasize);
}

#pragma endregion

#pragma region zipf

long* Random::zipf(double oa, long size)
{
	double _oa[1] = { oa };

	return zipf(_oa, 1, size);
}

long* Random::zipf(double* oa, int oasize, long size)
{
	// use logic that ensures NaN is rejected.
	if (!any_greater(oa, oasize, 1.0))
		throw "'a' must contain valid floats > 1.0";

	if (oasize == 1)
	{
		return discd_array_sc(internal_state, RandomDistributions::rk_zipf, size, oa[0]);
	}

	return discd_array(internal_state, RandomDistributions::rk_zipf, size, oa, oasize);
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
			array_data = new double[1];
			array_data[0] = rv;
			return array_data;
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
		double *array_data = new double[1];
		array_data[0] = rv;
		return array_data;
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
		double *array_data = new double[1];
		array_data[0] = rv;
		return array_data;
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
		double *array_data = new double[1];
		array_data[0] = rv;
		return array_data;
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
		long *array_data = new long[1];
		array_data[0] = rv;
		return array_data;
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
		long *array_data = new long[1];
		array_data[0] = rv;
		return array_data;
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
		long *array_data = new long[1];
		array_data[0] = rv;
		return array_data;
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
		long *array_data = new long[1];
		array_data[0] = rv;
		return array_data;
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

