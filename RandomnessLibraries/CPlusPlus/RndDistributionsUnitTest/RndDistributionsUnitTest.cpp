#include "pch.h"
#include "CppUnitTest.h"
#include "..\RndDistributions\RandomAPI.h"
#include "TestBaseClass.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace RndDistributionsUnitTest
{
	TEST_CLASS(RndDistributionsUnitTest)
	{
	public:

		const double doubleTolerance = .00000000001;
		TestBaseClass *bc = new TestBaseClass();
		
		TEST_METHOD(TestMethod1)
		{
			CRandom *random = new CRandom();

			double *arr = random->rand(2);
			//Assert.AreEqual(arr.GetType(), typeof(System.Double[]));

			random->seed(8765);
			double f = random->rand();
			bc->print(f);
			Assert::AreEqual(0.032785430047761466, f, doubleTolerance);

		}
	};
}
