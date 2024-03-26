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

bool Random::seed()
{
	return true;
}

bool Random::seed(long seed)
{
	internal_state->rndGenerator->Seed(seed, internal_state);
	return true;
}

void ValidateSize(long size)
{
	if (size < 0)
		throw "size must not be less than 0";
}


double Random::rand()
{
	return random_sample();
}
double* Random::rand(long size)
{
	ValidateSize(size);
	//return random_sample(size);
}

double random_sample()
{
	double[] rndArray = cont0_array(internal_state, RandomDistributions.rk_double, 0);
	return rndArray[0];
}

double[] random_sample(long size)
{
	double[] rndArray = cont0_array(internal_state, RandomDistributions.rk_double, size);
	return rndArray;
}
