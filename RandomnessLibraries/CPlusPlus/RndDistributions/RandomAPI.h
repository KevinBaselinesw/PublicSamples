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

	double randn();
	double* randn(long size);

	bool* randbool(long low, ulong high, long size);
	char* randint8(long low, long high, long size);
	uchar *randuint8(long low, ulong high, long size);
	short * randint16(long low, ulong high, long size);
	ushort * randuint16(long low, ulong high, long size);
	int * randint32(long low, ulong high, long size);
	uint *randuint32(long low, ulong high, long size);
	long* randint64(long low, ulong high, long size);
	ulong * randuint64(long low, ulong high, long size);
	int *random_integers(long low, ulong high, long size = 0);
	double random_sample();
	double* random_sample(long size);

	char getbyte();
	char * bytes(int size);
	
	double* beta(double *a, int asize, double* b, int bsize, long size = 0);

	long* binomial(long n, double p, long size = 0);
	long* binomial(long* on, int onsize, double* op, int opsize, long size);
	long* negative_binomial(long n, double p, long size = 0);
	long* negative_binomial(long* on, int onsize, double* op, int opsize, long size = 0);

	double* chisquare(double df, long size = 0);
	double* chisquare(double *odf, int odfsize, long size = 0);
	double* noncentral_chisquare(double* odf, int odfsize, double ononc, long size = 0);
	double* noncentral_chisquare(double odf, double ononc, long size = 0);
	double* noncentral_chisquare(double* odf, int odfsize, double* ononc, int ononcsize, long size = 0);

	double* exponential(double scale = 1.0, long size = 0);
	double* exponential(double* oscale, int oscalesize, long size = 0);

	double* f(double dfnum, double dfden, long size = 0);
	double* f(double *odfnum, int odfnumsize, double* odfden, int odfdensize, long size = 0);

	double* gamma(double shape, double scale, long size = 0);
	double* gamma(double* oshape, int oshapesize, double* oscale, int oscalesize, long size = 0);

	long* geometric(double op, long size = 0);
	long* geometric(double* op, int opsize, long size = 0);

	double* gumbel(double* oloc, int olocsize, double oscale, long size = 0);
	double* gumbel(double oloc, double* oscale = NULL, int oscalesize = 0, long size = 0);
	double* gumbel(double oloc, double oscale, long size = 0);
	double* gumbel(double* oloc, int olocsize, double *oscale = NULL, int oscalesize = 0, long size = 0);

	long* hypergeometric(long ongood, long onbad, long onsample, long size = 0);
	long* hypergeometric(long* ongood, int ongoodsize, long* onbad, int onbadsize, long* onsample, int onsamplesize, long size = 0);

	double* laplace(double oloc, double oscale, long size = 0);
	double* laplace(double *oloc, int olecsize, double oscale, long size = 0);
	double* laplace(double oloc, double* oscale = NULL, int oscalesize = 0, long size = 0);
	double* laplace(double *oloc, int olocsize, double *oscale = NULL, int oscalsize = 0, long size = 0);

	double* logistic(double oloc, double oscale, long size = 0);
	double* logistic(double* oloc, int olocsize, double oscale, long size = 0);
	double* logistic(double oloc);
	double* logistic(double *oloc, int olocsize, double *oscale = NULL, int oscalesize = 0, long size = 0);

	double* lognormal(double omean, double osigma, long size = 0);
	double* lognormal(double* omean, int omeansize, double osigma, long size = 0);
	double* lognormal(double* omean, int omeansize, double* osigma, int osigmansize, long size = 0);

	long* logseries(double op, long size = 0);
	long* logseries(double* op, int opsize, long size = 0);

	long* multinomial(int n, double* pvals, int pvalsize, long size = 0);

	double* noncentral_f(double odfnum, double odfden, double ononc, long size = 0);
	double* noncentral_f(double* odfnum, int odfnumsize, double odfden, double ononc, long size = 0);
	double* noncentral_f(double* odfnum, int odfnumsize, double* odfden, int odfdensize, double* ononc, int ononcsize, long size = 0);
	
	double* normal(double oloc, double oscale, long size = 0);
	double* normal(double* oloc, int olecsize, double oscale, long size = 0);
	double* normal(double* oloc, int olecsize, double* oscale, int oscalsize, long size = 0);

	double* pareto(double oa, long size = 0);
	double* pareto(double* oa, int oasize, long size = 0);

	long* poisson(double olam, long size = 0);
	long* poisson(double* olam, int olamsize, long size = 0);

	double* power(double oa, long size = 0);
	double* power(double* oa, int oasize, long size = 0);

	double* rayleigh(double oscale, long size = 0);
	double* rayleigh(double* oscale, int oscalesize, long size = 0);

	double standard_cauchy();
	double* standard_cauchy(long size);

	double standard_exponential();
	double* standard_exponential(long size);

	double* standard_gamma(double oshape, long size = 0);
	double* standard_gamma(double* oshape, int oshapesize, long size = 0);

	double* standard_t(double odf, long size = 0);
	double* standard_t(double* odf, int odfsize, long size = 0);

	double* triangular(double oleft, double omode, double oright, long size = 0);
	double* triangular(double *oleft, int oleftsize, double omode, double oright, long size = 0);
	double* triangular(double *oleft, int oleftsize, double* omode, int omodesize, double oright, long size = 0);
	double* triangular(double* oleft, int oleftsize, double* omode, int omodesize, double* oright, int orightsize, long size = 0);
	
	double* uniform(double low = 0.0, double high = 1.0, long size = 0);
	double* uniform(double* olow, int olowsize, double* ohigh, int ohighsize, long size = 0);

	double* vonmises(double omu, double okappa, long size = 0);
	double* vonmises(double* omu, int omusize, double okappa, long size = 0);
	double* vonmises(double* omu, int omusize, double* okappa, int okappasize, long size = 0);

	double* wald(double omean, double oscale, long size = 0);
	double* wald(double* omean, int omeansize, double oscale, long size = 0);
	double* wald(double* omean, int omeansize, double* oscale, int oscalesize, long size = 0);

	double* weibull(double oa, long size = 0);
	double* weibull(double* oa, int oasize, long size = 0);

	long* zipf(double oa, long size = 0);
	long* zipf(double* oa, int oasize, long size = 0);

	// TODO: add your methods here.

private:

	double standard_normal();
	double* standard_normal(long size);


	// array generation functions
	double* cont0_array(rk_state *state, double(*func)(rk_state *state), long size);
	double* cont1_array(rk_state *state, double(*func)(rk_state *state, double), long size, double* oa, int oasize);
	double* cont1_array_sc(rk_state *state, double(*func)(rk_state *state, double), long size, double a);
	double* cont2_array_sc(rk_state *state, double(*func)(rk_state *state, double, double), long size, double a, double b);
	double* cont2_array(rk_state *state, double(*func)(rk_state *state, double, double), long size, double* oa, int oasize, double* ob, int obsize);
	double* cont3_array_sc(rk_state *state, double(*func)(rk_state *state, double, double, double), long size, double a, double b, double c);
	double* cont3_array(rk_state *state, double(*func)(rk_state *state, double, double, double), long size, double* oa, int oasize, double* ob, int obsize, double* oc, int ocsize);
	long* discd_array(rk_state *state, long(*func)(rk_state *state, double), long size, double* oa, int oasize);
	long* discd_array_sc(rk_state *state, long(*func)(rk_state *state, double), long size, double a);
	long* discnp_array(rk_state *state, long(*func)(rk_state *state, long, double), long size, long* on, int onsize, double* op, int opsize);
	long* discnp_array_sc(rk_state *state, long(*func)(rk_state *state, long, double), long size, long n, double p);
	long* discdd_array(rk_state *state, long(*func)(rk_state *state, double, double), long size, long* on, int onsize, double* op, int opsize);
	long* discdd_array_sc(rk_state *state, long(*func)(rk_state *state, double, double), long size, long n, double p);
	long* discnmN_array(rk_state *state, long(*func)(rk_state *state, long, long, long), long size, long* on, int onsize, long* om, int omsize, long* oN, int oNsize);
	long* discnmN_array_sc(rk_state *state, long(*func)(rk_state *state, long, long, long), long size, long n, long m, long N);

	// utility functions
	double kahan_sum(double* darr, long n);

	bool any_less(double* array, int array_len, double value);
	bool any_less_equal(double* array, int array_len, double value);
	bool any_greater(double* array, int array_len, double value);
	bool any_greater_equal(double* array, int array_len, double value);
	bool any_less(long* array, int array_len, int value);
	bool any_nan(double* array, int array_len);
	bool any_signbit(double* array, int array_len);
	bool any_less(long* arr1, int arr1_length, long* arr2, int arr2_length);
	bool any_equal(double* arr1, int arr1_length, double* arr2, int arr2_length);
	bool any_greater(double* arr1, int arr1_length, double* arr2, int arr2_length);
	long *add(long* arr1, int arr1_length, long* arr2, int arr2_length);
	double* subtract(double* arr1, int arr1_length, double* arr2, int arr2_length);
	bool any_isfinite(double* array, int array_length);

	// misc (really unused, left for compatibility with .net code)
	void* rk_lock;
	void lock(void *);
};

