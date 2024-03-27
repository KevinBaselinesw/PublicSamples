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

#define ulong unsigned long
#define uint unsigned int
#define ushort unsigned short
#define uchar unsigned char

class IRandomGenerator;

class rk_state
{
public:

	rk_state(IRandomGenerator *rndGenerator)
	{
		this->rndGenerator = rndGenerator;
	}

	IRandomGenerator *rndGenerator;

	int pos;
	bool has_gauss; /* !=0: gauss contains a gaussian deviate */
	double gauss;

	///* The rk_state structure has been extended to store the following
	// * information for the binomial generator. If the input values of n or p
	// * are different than nsave and psave, then the other parameters will be
	// * recomputed. RTK 2005-09-02 */

	bool has_binomial; /* !=0: following parameters initialized for binomial */
	double psave;
	long nsave;
	double r;
	double q;
	double fm;
	long m;
	double p1;
	double xm;
	double xl;
	double xr;
	double c;
	double laml;
	double lamr;
	double p2;
	double p3;
	double p4;

};

class IRandomGenerator
{
public:
	
	IRandomGenerator()
	{
	
	}

	virtual void Seed(unsigned long seedValue, rk_state *state) = 0;
	virtual unsigned long getNextUInt64(rk_state *state) = 0;
	virtual double getNextDouble(rk_state *state) = 0;
};

