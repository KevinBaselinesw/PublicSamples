#include "pch.h"
#include "RandomDistributions.h"


double RandomDistributions::rk_double(rk_state *state)
{
	return state->rndGenerator->getNextDouble(state);
}