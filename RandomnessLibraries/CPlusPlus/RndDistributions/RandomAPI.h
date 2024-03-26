// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the RNDDISTRIBUTIONS_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// RNDDISTRIBUTIONS_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef RNDDISTRIBUTIONS_EXPORTS
#define RANDOM_API __declspec(dllexport)
#else
#define RANDOM_API __declspec(dllimport)
#endif

#include "IRandomGenerator.h"

// This class is exported from the dll
class RANDOM_API Random 
{

private:

	rk_state *internal_state;
	IRandomGenerator *_rndGenerator;

public:
	Random(IRandomGenerator *rndGenerator = NULL);

	bool seed();
	bool seed(long seed);

	double rand();
	double* rand(long size);

	// TODO: add your methods here.
};

