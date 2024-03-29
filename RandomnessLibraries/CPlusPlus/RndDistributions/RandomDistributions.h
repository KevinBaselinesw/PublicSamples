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
#include "IRandomGenerator.h"

class RandomDistributions
{
public:

	static double rk_normal(rk_state *state, double loc, double scale);
	static double rk_standard_exponential(rk_state *state);
	static double rk_exponential(rk_state *state, double scale);
	static double rk_uniform(rk_state *state, double loc, double scale);
	static double rk_standard_gamma(rk_state *state, double shape);
	static double rk_gamma(rk_state *state, double shape, double scale);
	static double rk_beta(rk_state *state, double a, double b);
	static double rk_chisquare(rk_state *state, double df);
	static double rk_noncentral_chisquare(rk_state *state, double df, double nonc);
	static double rk_f(rk_state *state, double dfnum, double dfden);
	static double rk_noncentral_f(rk_state *state, double dfnum, double dfden, double nonc);
	static long rk_binomial_btpe(rk_state *state, long n, double p);
	static long rk_binomial_inversion(rk_state *state, long n, double p);
	static long rk_binomial(rk_state *state, long n, double p);
	static long rk_negative_binomial(rk_state *state, double n, double p);
	static long rk_poisson_mult(rk_state *state, double lam);
	static long rk_poisson_ptrs(rk_state *state, double lam);
	static long rk_poisson(rk_state *state, double lam);
	static double rk_standard_cauchy(rk_state *state);
	static double rk_standard_t(rk_state *state, double df);
	static double rk_vonmises(rk_state *state, double mu, double kappa);
	static double rk_pareto(rk_state *state, double a);
	static double rk_weibull(rk_state *state, double a);
	static double rk_power(rk_state *state, double a);
	static double rk_laplace(rk_state *state, double loc, double scale);
	static double rk_gumbel(rk_state *state, double loc, double scale);
	static double rk_logistic(rk_state *state, double loc, double scale);
	static double rk_lognormal(rk_state *state, double mean, double sigma);
	static double rk_rayleigh(rk_state *state, double mode);
	static double rk_wald(rk_state *state, double mean, double scale);
	static long rk_zipf(rk_state *state, double a);
	static long rk_geometric_search(rk_state *state, double p);
	static long rk_geometric_inversion(rk_state *state, double p);
	static long rk_geometric(rk_state *state, double p);
	static long rk_hypergeometric_hyp(rk_state *state, long good, long bad, long sample);
	static long rk_hypergeometric_hrua(rk_state *state, long good, long bad, long sample);
	static long rk_hypergeometric(rk_state *state, long good, long bad, long sample);
	static double rk_triangular(rk_state *state, double left, double mode, double right);
	static long rk_logseries(rk_state *state, double p);

	static void rk_seed(ulong seed, rk_state *state);
	static ulong rk_random(rk_state *state);
	static double rk_double(rk_state *state);
	static ulong rk_uint64(rk_state *state);
	static long rk_int64(rk_state *state);
	static unsigned int rk_uint32(rk_state *state);
	static int rk_int32(rk_state *state);
	static void rk_random_int64(long off, long rng, long cnt, long* _out, rk_state *state);
	static void rk_random_uint64(ulong off, ulong rng, long cnt, ulong* _out, rk_state *state);
	static void rk_random_int32(int off, int rng, long cnt, int* _out, rk_state *state);
	static void rk_random_uint32(uint off, uint rng, long cnt, uint * _out, rk_state *state);
	static void rk_random_int16(short off, short rng, long cnt, short* _out, rk_state *state);
	static void rk_random_uint16(ushort off, ushort rng, long cnt, ushort* _out, rk_state *state);
	static void rk_random_int8(char off, char rng, long cnt, char* _out, rk_state *state);
	static void rk_random_uint8(uchar off, uchar rng, long cnt, uchar* _out, rk_state *state);
	static void rk_random_bool(bool off, bool rng, long cnt, bool* _out, rk_state *state);
	static long rk_long(rk_state *state);
	static ulong rk_ulong(rk_state *state);
	static ulong rk_interval(ulong max, rk_state *state);
	static void rk_fill(char * buffer, int size, rk_state *state);
	
	static double rk_gauss(rk_state *state);

private:
	static double loggam(double x);



};

