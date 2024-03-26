#include "pch.h"
#include "CppUnitTest.h"
#include "..\RndDistributions\RandomAPI.h"
#include "TestBaseClass.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace RndDistributionsUnitTest
{
	TEST_CLASS(RndDistributionsUnitTest), public TestBaseClass
	{
	public:

		const double doubleTolerance = .00000000001;
		TestBaseClass *bc = new TestBaseClass();
		
		TEST_METHOD(TestMethod1)
		{
			Random *random = new Random();

			double *arr = random->rand(2);
			//Assert.AreEqual(arr.GetType(), typeof(System.Double[]));

			random->seed(8765);
			double f = random->rand();
			print(f);
			Assert::AreEqual(0.032785430047761466, f, doubleTolerance);

			int size = 5000000;
			arr = random->rand(size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(0.99999999331246381, amax, doubleTolerance);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(7.223258213784334e-08, amin, doubleTolerance);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.49987999522694609, avg, doubleTolerance);


		}

		TEST_METHOD(TestMethod2)
		{
			Random *random = new Random();

			double *arr = random->rand(2);
			//Assert.AreEqual(arr.GetType(), typeof(System.Double[]));

			random->seed(8765);
			double f = random->rand();
			print(f);
			Assert::AreEqual(0.032785430047761466, f, doubleTolerance);

		}

	};
}
