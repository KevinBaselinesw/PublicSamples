#pragma once

#include "IRandomGenerator.h"

class RandomState : public IRandomGenerator
{
	static const int RK_STATE_LEN = 624;

private:
	
	unsigned long key[RK_STATE_LEN];

public:

	RandomState()
	{

	}

	void Seed(unsigned long seedValue, rk_state *state);
	unsigned long getNextUInt64(rk_state *state);
	double getNextDouble(rk_state *state);

};

