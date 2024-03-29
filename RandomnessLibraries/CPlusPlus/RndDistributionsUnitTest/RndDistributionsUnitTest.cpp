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
		
		TEST_METHOD(test_rand_1)
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

		TEST_METHOD(test_randn_1)
		{
			Random *random = new Random();

			double fr = random->randn();
			double *arr = random->randn(2 * 3 * 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.Double[]));

			random->seed(1234);

			fr = random->randn();
			print(fr);
			Assert::AreEqual(0.47143516373249306, fr);

			int size = 5000000;
			arr = random->randn(size);
			//Assert.AreEqual(arr.GetType(), typeof(System.Double[]));

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(5.19094054905629, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-5.387212036872835, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-0.00028345468151361842, avg);

		}

		TEST_METHOD(test_randbool_1)
		{
			Random *random = new Random();

			bool *arr = random->randbool(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.Boolean[]));
			print(arr);
			//AssertArray(arr, new bool[] { false, false, false, true });

			arr = random->randbool(20, 0, 20);
			print(arr);

			arr = random->randbool(20, 21, 6);
			print(arr);

			int size = 5000000;
			arr = random->randbool(-2, 3, size);
			print(GetMax(arr, size));
			print(GetMin(arr, size));
			//print(GetAverage(arr));

		}

		TEST_METHOD(test_randint8_1)
		{
			Random *random = new Random();

			random->seed(9292);

			char *arr = random->randint8(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.SByte[]));
			print(arr);
			AssertArray(arr, new char[4]{ 2, 2, 2, 2 }, 4);

			int size = 5000000;
			arr = random->randint8(2, 8, size);
			//Assert.AreEqual(arr.GetType(), typeof(System.SByte[]));

			char amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((char)7, amax);

			char amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((char)2, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(4.4994078, avg);

			char first10[10];
			char Expected1[10] = { 3, 6, 7, 5, 5, 5, 7, 7, 4, 2 };
			FirstTen(arr, first10);
			print(first10);
			AssertArray(first10, Expected1, 10);


			arr = random->randint8(-2, 3, size);
			//Assert.AreEqual(arr.GetType(), typeof(System.SByte[]));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((char)2, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((char)(-2), amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-0.0003146, avg);

			char Expected2[10] = { 0, 1, -1, -2, -2, -1, 2, -1, -2, -2 };
			FirstTen(arr, first10);
			print(first10);
			AssertArray(first10, Expected2, 10);

		}

	};
}
