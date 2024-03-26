// RndDistributions.cpp : Defines the exported functions for the DLL.
//

#include "pch.h"
#include "framework.h"
#include "RandomAPI.h"


// This is an example of an exported variable
RANDOM_API int nRndDistributions=0;

// This is an example of an exported function.
RANDOM_API int fnRndDistributions(void)
{
    return 0;
}

// This is the constructor of a class that has been exported.
CRandom::CRandom()
{
    return;
}

bool CRandom::seed(long seed)
{
	return true;
}

double CRandom::rand()
{
	return 0.032785430047761466;
}
double* CRandom::rand(long size)
{
	double *rnd = new double[size];
	return rnd;
}
