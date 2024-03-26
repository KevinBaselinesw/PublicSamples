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

// This class is exported from the dll
class RANDOM_API CRandom {
public:
	CRandom(void);

	bool seed(long seed);

	double rand();
	double* rand(long size);

	// TODO: add your methods here.
};

extern RANDOM_API int nRndDistributions;

RANDOM_API int fnRndDistributions(void);
