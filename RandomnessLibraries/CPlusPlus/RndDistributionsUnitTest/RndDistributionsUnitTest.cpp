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
#include <memory>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace RndDistributionsUnitTest
{
	TEST_CLASS(RndDistributionsUnitTest), public TestBaseClass
	{
	public:

#pragma region Simple Random Data


		TEST_METHOD(test_rand_1)
		{
			Random *random = new Random();

			double *arr = random->rand(2);
			//Assert.AreEqual(arr.GetType(), typeof(System.Double[]));

			(void)random->seed(8765);
			double f = random->rand();
			print(f);
			Assert::AreEqual(0.032785430047761466, f, doubleTolerance);

			delete arr;

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

			delete arr;
			delete random;
		}

		TEST_METHOD(test_randn_1)
		{
			Random *random = new Random();

			double fr = random->randn();
			double *arr = random->randn(2 * 3 * 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.Double[]));

			(void)random->seed(1234);

			fr = random->randn();
			print(fr);
			Assert::AreEqual(0.47143516373249306, fr);

			delete arr;

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

			delete arr;
			delete random;

		}

		TEST_METHOD(test_randbool_1)
		{
			Random *random = new Random();

			bool *arr = random->randbool(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.Boolean[]));
			print(arr);
			delete arr;
			//AssertArray(arr, new bool[] { false, false, false, true });

			arr = random->randbool(20, 0, 20);
			print(arr);
			delete arr;

			arr = random->randbool(20, 21, 6);
			print(arr);
			delete arr;

			int size = 5000000;
			arr = random->randbool(-2, 3, size);
			print(GetMax(arr, size));
			print(GetMin(arr, size));
			//print(GetAverage(arr));

			delete arr;
			delete random;

		}

		TEST_METHOD(test_randint8_1)
		{
			Random *random = new Random();

			(void)random->seed(9292);

			char *arr = random->randint8(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.SByte[]));
			print(arr);
			char ExpectedArray[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedArray, 4);

			delete arr;

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
			delete arr;

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

			delete arr;
			delete random;
		}

		TEST_METHOD(test_randuint8_1)
		{
			Random *random = new Random();

			(void)random->seed(1313);

			uchar *arr = random->randuint8(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.SByte[]));
			print(arr);
			uchar ExpectedArray[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedArray, 4);
			delete arr;

			int size = 5000000;
			arr = random->randuint8(2, 128, size);
			//Assert.AreEqual(arr.GetType(), typeof(System.SByte[]));

			uchar amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((uchar)127, amax);

			uchar amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((uchar)2, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(64.5080776, avg);

			uchar first10[10];
			uchar Expected1[10] = { 114, 37, 22, 53, 90, 27, 34, 98, 123, 114 };
			FirstTen(arr, first10);
			print(first10);
			AssertArray(first10, Expected1, 10);

			delete arr;
			delete random;
		}

		TEST_METHOD(test_randint16_1)
		{
			Random *random = new Random();

			(void)random->seed(8381);

			short *arr = random->randint16(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int16[]));
			print(arr);
			short ExpectedData[4]{ 2, 2, 2, 2 };
			AssertArray(arr, ExpectedData, 4);

			int size = 5000000;
			arr = random->randint16(2, 2478, size);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int16[]));

			short amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((short)2477, amax);

			short amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((short)2, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1239.6188784, avg, doubleTolerance);

			short first10[10];
			short Expected1[10] = { 1854, 1641, 1219, 1190, 1714, 644, 441, 484, 579, 393 };
			FirstTen(arr, first10);
			print(first10);
			AssertArray(first10, Expected1, 10);


			size = 5000000;
			arr = random->randint16(-2067, 3000, 5000000);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int16[]));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((short)2999, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((short)(-2067), amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(466.3735356, avg, doubleTolerance);

			short Expected2[10] = { -1761, -1165, 2645, -1210, 1741, -1692, -1042, -354, 2637, -706 };
			FirstTen(arr, first10);
			print(first10);
			AssertArray(first10, Expected2, 10);

			delete random;
		}

		TEST_METHOD(test_randuint16_1)
		{
			Random *random = new Random();

			(void)random->seed(5555);

			ushort* arr = random->randuint16(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.UInt16[]));
			print(arr);

			ushort ExpectedData[4]{ 2, 2, 2, 2 };
			//AssertArray(arr, ExpectedData, 4);
			delete arr;

			int size = 5000000;
			arr = random->randuint16(23, 12801, size);
			//Assert.AreEqual(arr.GetType(), typeof(System.UInt16[]));

			ushort amax = GetMax(arr,size);
			print(amax);
			//Assert::AreEqual((ushort)12800, amax);

			ushort amin = GetMin(arr, size);
			print(amin);
			//Assert::AreEqual((ushort)23, amin);

			double avg = GetAverage(arr,size);
			print(avg);
			Assert::AreEqual(6411.5873838, avg);

			ushort first10[10];
			ushort Expected1[10] = { 3781, 11097, 4916, 2759, 12505, 827, 12017, 3982, 7669, 11128 };
			FirstTen(arr, first10);
			print(first10);
			//AssertArray(first10, Expected1, 10);
			delete arr;
			delete random;
		}

		TEST_METHOD(test_randint32_1)
		{
			Random *random = new Random();

			(void)random->seed(701);

			int * arr = random->randint32(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int32[]));
			print(arr);

			int ExpectedData[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedData, 4);

			delete arr;

			int size = 5000000;
			arr = random->randint32(9, 128000, size);
			//Assert::AreEqual(arr.GetType(), typeof(System.Int32[]));

			int amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((int)127999, amax);

			int amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((int)9, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(64017.8102496, avg,doubleTolerance);


			int first10[10];
			int Expected1[10] = { 116207, 47965, 90333, 86510, 49239, 115311, 100417, 99653, 14878, 92386 };
			FirstTen(arr, first10);
			print(first10);
			AssertArray(first10, Expected1, 10);

			delete arr;

			size = 5000000;
			arr = random->randint32(-20000, 300000, size);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int32[]));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((int)299999, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((int)(-20000), amin);

			avg = GetAverage(arr,size);
			print(avg);
			Assert::AreEqual(139952.5096864, avg, doubleTolerance);

			int Expected2[10] = { 211742, 98578, 96618, 50714, 285136, 270233, 186899, 278442, 215280, 268431 };
			FirstTen(arr, first10);
			print(first10);
			AssertArray(first10, Expected2, 10);

			delete arr;
			delete random;
		}

		TEST_METHOD(test_randuint_1)
		{
			Random *random = new Random();

			(void)random->seed(8357);

			uint* arr = random->randuint32(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.UInt32[]));
			print(arr);

			uint ExpectedData[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedData, 4);

			delete arr;

			int size = 5000000;
			arr = random->randuint32(29, 13000, 5000000);
			//Assert.AreEqual(arr.GetType(), typeof(System.UInt32[]));

			uint amax = GetMax(arr,size);
			print(amax);
			Assert::AreEqual((uint)12999, amax);

			uint amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((uint)29, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(6512.7336774, avg, doubleTolerance);

			uint first10[10];
			uint Expected1[10] = { 6511, 10414, 12909, 5962, 353, 8634, 8330, 1452, 5009, 5444 };
			FirstTen(arr, first10);
			print(first10);
			AssertArray(first10, Expected1, 10);

			delete arr;
			delete random;
		}


		TEST_METHOD(test_randint64_1)
		{
			Random *random = new Random();

			(void)random->seed(10987);

			long* arr = random->randint64(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int64[]));
			print(arr);
			long ExpectedData[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedData, 4);

			delete arr;

			int size = 5000000;
			arr = random->randint64(20, 9999999, 5000000);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int64[]));

			long amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)9999995, amax);

			long amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)24, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(5000256.718873, avg);


			long first10[10];
			long Expected1[10] = { 680213, 6755395, 2873894, 6180772, 6542022, 4878058, 9894982, 6332894, 1846901, 8550855 };
			FirstTen(arr, first10);
			print(first10);
			AssertArray(first10, Expected1, 10);

			delete arr;

			size = 5000000;
			arr = random->randint64(-9999999, 9999999, 5000000);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int64[]));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)9999994, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)(-9999994), amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-3134.7668014, avg);

			long Expected2[10] = { 8539427, -3969151, -3294108, 7156061, 6272194, 9698115, 1323314, -4364865, -124057, -6816292 };
			FirstTen(arr, first10);
			print(first10);
			AssertArray(first10, Expected2, 10);

			delete arr;

			delete random;
		}

	
		TEST_METHOD(test_randuint64_1)
		{
			Random *random = new Random();

			(void)random->seed(1990);

			ulong *arr = random->randuint64(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.UInt64[]));
			print(arr);
			ulong ExpectedData[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedData, 4);

			delete arr;

			int size = 5000000;
			arr = random->randuint64(64, 64000, size);
			//Assert.AreEqual(arr.GetType(), typeof(System.UInt64[]));

			ulong amax = GetMax(arr,size);
			print(amax);
			Assert::AreEqual((ulong)63999, amax);

			ulong amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((ulong)64, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(32014.4591844, avg);

			ulong first10[10];
			ulong Expected1[10] = { 32948, 9226, 21086, 58281, 51816, 58826, 18566, 13253, 28502, 39394 };
			FirstTen(arr, first10);
			print(first10);
			AssertArray(first10, Expected1, 10);

			delete arr;

			delete random;
		}

		TEST_METHOD(test_rand_bytes_1)
		{
			Random *random = new Random();

			(void)random->seed(6432);

			char br = random->getbyte();

			char *bytes = random->bytes(24);
			//Assert.AreEqual(bytes.Length, 24);
			delete bytes;

			br = random->getbyte();
			char *arr = random->bytes(24);
			//Assert.AreEqual(arr.GetType(), typeof(System.Byte[]));

			print(br);
			Assert::AreEqual((char)193, br);

			print(arr);

			char ExpectedData[24] =
			{ 18, 59, 87, 188, 116, 37, 28, 134, 204, 39, 5, 212,147, 165, 1, 136, 198, 108, 203, 151, 39, 187, 144, 209 };

			AssertArray(arr, ExpectedData, 24);

			delete arr;
			delete random;
		}

#pragma endregion

#pragma region shuffle/permutation

		TEST_METHOD(test_rand_shuffle_1)
		{
			Random *random = new Random();

			(void)random->seed(1964);

			int *arr = arange_Int32(0, 10);
			random->shuffle(arr, 10);
			print(arr);

			int ExpectedData1[10] = { 2, 9, 3, 6, 1, 7, 5, 0, 4, 8 };
			AssertArray(arr, ExpectedData1, 10);

			arr = arange_Int32(0, 10);
			print(arr);

			random->shuffle(arr, 10);
			print(arr);

			int ExpectedData2[10] = { 0 , 3 ,  7 ,  8 ,  5 ,  9 ,  4 ,  6 ,  1 ,  2 };
			AssertArray(arr, ExpectedData2, 10);

			delete random;
		}

		TEST_METHOD(test_rand_permutation_1)
		{
			Random *random = new Random();

			(void)random->seed(1963);

			int *arr = random->permutation(10);
			print(arr);

			int ExpectedData1[10] = { 6, 7, 4, 5, 1, 2, 9, 0, 8, 3 };
			AssertArray(arr, ExpectedData1, 10);

			arr = random->permutation(arange_Int32(0, 5), 5);
			print(arr);

			int ExpectedData2[5] = { 2, 3, 1, 0, 4 };
			AssertArray(arr, ExpectedData2, 5);

			delete random;
		}


#pragma endregion

		TEST_METHOD(test_rand_beta_1)
		{
			Random *random = new Random();

			(void)random->seed(5566);

			double *a = arange_Double(1, 11);
			double *b = arange_Double(1, 11);

			double *arr = random->beta(a, 10, b, 10, 10);
			//Assert.AreEqual(arr.GetType(), typeof(System.Double[]));
			print(arr);

			double ExpectedData[10]
			{ 0.890517356256563, 0.532484155787344, 0.511396509650771, 0.862418837558698, 0.492458004172681,
				0.504662615187708, 0.621477223753938, 0.41702032419038, 0.492984829781569, 0.47847289036676 };

			AssertArray(arr, ExpectedData, 10);

			delete random;
		}

		TEST_METHOD(test_rand_binomial_1)
		{
			Random *random = new Random();

			(void)random->seed(123);

			int size = 20;
			long*arr = random->binomial(9, 0.1, size);

			long ExpectedData[20] = { 1, 0, 0, 1, 1, 1, 3, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 1 };
			AssertArray(arr, ExpectedData, 20);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int64[]));

			int s = CountTrues(CompareArray(arr, size, 0), size);
			Assert::AreEqual(6, s);

			size = 20000;
			arr = random->binomial(9, 0.1, size);
			s = CountTrues(CompareArray(arr, size, 0), size);
			Assert::AreEqual(7711, s);
			print(s);

			delete random;
		}

		TEST_METHOD(test_rand_negative_binomial_1)
		{
			Random *random = new Random();

			(void)random->seed(123);

			int size = 20;
			long*arr = random->negative_binomial(1, 0.1, size);

			long ExpectedData[20] = { 8, 9, 4, 10, 8, 5, 11, 7, 21, 0, 8, 1, 7, 3, 1, 17, 4, 5, 6, 8 };
			AssertArray(arr, ExpectedData, 20);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int64[]));

			int s = CountTrues(CompareArray(arr, size, 0), size);
			Assert::AreEqual(1, s);

			size = 20000;
			arr = random->negative_binomial(1, 0.1, size);
			s = CountTrues(CompareArray(arr, size, 0), size);
			Assert::AreEqual(1992, s);
			print(s);

			delete random;
		}

		TEST_METHOD(test_rand_chisquare_1)
		{
			Random *random = new Random();

			(void)random->seed(904);

			int size = 40;
			double *arr = random->chisquare(2, size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(5.375544801685989, amax, doubleTolerance);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.08589992390559097, amin, doubleTolerance);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.7501237764369322, avg, doubleTolerance);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double Expected1[10] = { 0.449839203939145, 1.92402228590093, 3.34447894813104, 5.34660224752626, 1.15307500835178,
				3.7142340997291, 0.137140658434128, 1.69505253573874, 1.5675310912308, 3.1550000636764 };

			AssertArray(first10, Expected1, 10);

			size = 25 * 25;
			arr = random->chisquare(arange_Double(1, (size) + 1), (size));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(686.8423283498724, amax, doubleTolerance);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.5907891154976891, amin, doubleTolerance);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(313.32691732965679, avg, doubleTolerance);

			FirstTen(arr, first10);
			print(first10);

			double Expected2[10] = { 0.590789115497689, 6.66144890165653, 4.3036608098316, 3.81142549081703, 3.43184119361682,
				7.72738961829532, 8.62984666080682, 7.93816304470877, 4.58662123588299, 10.139304988442 };

			AssertArray(first10, Expected2, 10);

			delete random;
		}

		//TEST_METHOD(test_rand_dirichlet_1)
		//{
		//	// "This requires multi-dimensional arrays.  Use numpydotnet if you need this function."
		//}

		TEST_METHOD(test_rand_exponential_1)
		{
			Random *random = new Random();

			(void)random->seed(914);

			int size = 40;
			double *arr = random->exponential(2.0, size);

			double amax = GetMax(arr,size);
			print(amax);
			Assert::AreEqual(9.197509067464914, amax, doubleTolerance);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.0029207069544253516, amin, doubleTolerance);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(2.2703450760147272, avg, doubleTolerance);

			double first10[10];
			FirstTen(arr, first10);

			double ExpectedData1[10] = { 0.747521091531369, 2.07455044402581, 0.266251619077963, 0.853502719631811, 6.17373567708619,
				9.19750906746491, 0.716019724979618, 0.00292070695442535, 1.07083473949392, 2.71744206583613 };
	
			AssertArray(first10, ExpectedData1, 10);

			//////////////

			size = 4;
			arr = random->exponential(new double[size] { 1.75, 2.25, 3.5, 4.1 },4, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(6.4389292872403, amax, doubleTolerance);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.8405196701524901, amin, doubleTolerance);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(2.3954160266720965, avg, doubleTolerance);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] = { 0.84051967015249, 1.41367329358207, 0.888541855713523, 6.4389292872403 };

			AssertArray(first10, ExpectedData2, 4);

			//////////////

			size = 200000;
			arr = random->exponential(1.75, 200000);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(21.318986653331216, amax, doubleTolerance);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(3.55593171092486e-06, amin, doubleTolerance);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.7531454665721702, avg, doubleTolerance);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10] = { 1.36762201783298, 2.8658070450868, 0.624351703816035, 0.968550532587695, 4.27788701329609,
				0.366187067053844, 1.8701491040941, 0.322938134269804, 1.77904860176968, 0.311565039308104 };


			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_exponential_2)
		{
			Random *random = new Random();

			(void)random->seed(914);

			double *arr = random->exponential();
			Assert::AreEqual(0.37376054576568468, arr[0], doubleTolerance);
			print(arr);

			delete random;
		}

		TEST_METHOD(test_rand_f_1)
		{
			Random *random = new Random();

			(void)random->seed(94);

			int size = 1000;
			double *arr = random->f(1, 48, size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(19.14437059096128, amax, doubleTolerance);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(5.680490866939581e-07, amin, doubleTolerance);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.0515442503540988, avg, doubleTolerance);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10] = { 1.46465345913042, 0.222306914656698, 1.32950770405259, 0.653851369266928, 0.00813948648495581,
				0.882291524250553, 1.81050180312571, 0.000604113781846987, 0.153342107000664, 0.977254029029637 };

			AssertArray(first10, ExpectedData1, 10);


			//////////////

			size = 4;
			arr = random->f(new double[4] { 1.75, 2.25, 3.5, 4.1 }, 4, new double[1] { 48 }, 1, 4);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(1.2732463363388247, amax, doubleTolerance);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.546310315192394, amin, doubleTolerance);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.9827519798898189, avg, doubleTolerance);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] = { 1.05458800808553, 1.05686325994253, 1.27324633633882, 0.546310315192394 };
			AssertArray(first10, ExpectedData2, 4);

			//////////////

			size = 200000;
			arr = random->f(1.75, 53, 200000);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(15.625089837222493, amax, doubleTolerance);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(8.535252347895407e-07, amin, doubleTolerance);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.0363937363959668, avg, doubleTolerance);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10] = { 0.00532782005062579, 0.864448569799127, 4.62790636498241, 0.0383338245858999, 0.0484018547471839,
				0.351354338193848, 0.178901619826393, 1.16925307162684, 2.99295008429782, 0.0505560091503879 };
			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_gamma_1)
		{
			Random *random = new Random();

			(void)random->seed(99);

			int size = 2;
			double *arr = random->gamma(new double[2] { 4, 4 }, 2, new double[1] { 2 }, 1);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(18.370134973057713, amax, doubleTolerance);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(6.801539561549457, amin, doubleTolerance);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(12.585837267303585, avg, doubleTolerance);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData[] = { 6.80153956154946, 18.3701349730577 };
			AssertArray(first10, ExpectedData, 2);

			//////////////

			size = 4;
			arr = random->gamma(new double[4] { 1.75, 2.25, 3.5, 4.1 }, 4, new double[1] { 48 }, 1, 4);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(269.13817401122844, amax, doubleTolerance);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(85.50090380856885, amin, doubleTolerance);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(198.09580609835598, avg, size);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[] = { 85.5009038085689, 211.684158840965, 226.059987732662, 269.138174011228 };

			AssertArray(first10, ExpectedData1, 4);

			//////////////

			size = 200000;
			arr = random->gamma(1.75, 53, 200000);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(845.754380588075, amax, doubleTolerance);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.04372693615388623, amin, doubleTolerance);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(92.505472695434207, avg, doubleTolerance);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[10] = { 65.0307272217409, 127.150322850433, 93.4719711853725, 16.0658132351959, 47.6795500662279,
				118.872229264334, 33.0026431406179, 63.8640974905758, 34.4577922345043, 72.2726570834378 };
	
			AssertArray(first10, ExpectedData2, 10);

			delete random;
		}

		TEST_METHOD(test_rand_geometric_1)
		{
			Random *random = new Random();

			(void)random->seed(101);

			int size = 1;
			long *arr = random->geometric(0.35);

			long amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)2, amax);

			long amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)2, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(2.0, avg);

			long first10[10];
			FirstTen(arr, first10);
			print(first10);
					
			long ExpectedData[1] = { 2 };

			AssertArray(first10, ExpectedData, 1);

			//////////////

			size = 400;
			arr = random->geometric(new double[4] { .75, .25, .5, .1 }, 4, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)47, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)1, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(4.42, avg);

			FirstTen(arr, first10);
			print(first10);

	/*		var ExpectedData2 = new long[, ]
			{ { 1, 1, 1, 11 }, { 2, 2, 4, 13 }, { 1, 3, 1, 2 }, { 2, 12, 1, 1 }, { 1, 5, 1, 11 },
			  { 1, 1, 1, 2 },  { 4, 3, 2, 13 }, { 1, 9, 3, 5 }, { 1, 11, 1, 20 }, { 2, 3, 4, 1 } };*/

			//AssertArray(first10, ExpectedData2);

			//////////////

			size = 200000;
			arr = random->geometric(.75, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)10, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)1, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.332595, avg);

			FirstTen(arr, first10);
			print(first10);

			long ExpectedData2[] = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 };
			AssertArray(first10, ExpectedData2, 10);

			delete random;
		}

		TEST_METHOD(test_rand_gumbel_1)
		{
			Random *random = new Random();

			(void)random->seed(1431);

			double *arr = random->gumbel(0.32);

			int size = 1;
			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(1.76573325397214, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(1.76573325397214, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.76573325397214, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData[] = { 1.76573325397214 };

			AssertArray(first10, ExpectedData, size);

			//////////////

			size = 4;
			arr = random->gumbel(new double[4] { .75, .25, .5, .1 }, 4, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(7.625593114379172, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-2.48113529309385, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(3.319780095180179, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] { 4.79769794330828, -2.48113529309385, 3.33696461612712, 7.62559311437917 };

			AssertArray(first10, ExpectedData2, 4);

			//////////////

			size = 200000;
			arr = random->gumbel(.75, 0.5, 200000);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(7.205833825082223, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-0.5506620623963436, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.0376445397766865, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10]
			{ 1.21622910498044, 3.00226498101696, 0.475407027157304, 1.65190860500953, 1.57140421867578,
				0.93864144637321, -0.200732799247397, 0.242196470741022, 0.0803181023284516, 1.52710248786222 };
			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_hypergeometric_1)
		{
			Random *random = new Random();
	
			(void)random->seed(1631);

			int size = 1000;
			long* arr = random->hypergeometric(100, 2, 10, size);

			long amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)10, amax);

			long amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)8, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(9.827, avg);

			long first10[10];
			FirstTen(arr, first10);
			print(first10);

			long ExpectedData[10] { 10, 10, 10, 8, 9, 10, 10, 10, 10, 9 };

			AssertArray(first10, ExpectedData, 10);

			//////////////

			size = 4;
			arr = random->hypergeometric(new long[4] { 75, 25, 5, 1 }, 4, new long[1] { 5 }, 1, new long[4] { 80, 30, 10, 6 }, 4);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)75, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)1, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(26.5, avg);

			FirstTen(arr, first10);
			print(first10);

			long ExpectedData2[4] { 75, 25, 5, 1 };

			AssertArray(first10, ExpectedData2, size);

			//////////////

			size = 200000;
			arr = random->hypergeometric(15, 15, 15, 200000);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)13, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)2, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(7.500615, avg);

			FirstTen(arr, first10);
			print(first10);

			long ExpectedData3[10] = { 7, 10, 7, 7, 6, 9, 9, 8, 7, 8 };
			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_laplace_1)
		{
			Random *random = new Random();

			(void)random->seed(1400);

			int size = 1;
			double *arr = random->laplace(0.32);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(-1.0764218589534678, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-1.0764218589534678, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-1.0764218589534678, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData[1] { -1.0764218589534678 };

			AssertArray(first10, ExpectedData, 1);

			//////////////

			size = 4;
			arr = random->laplace(new double[4] { .75, .25, .5, .1 },4, 4);

			amax = GetMax(arr,size);
			print(amax);
			Assert::AreEqual(0.8742419694128964, amax);

			amin = GetMin(arr,size);
			print(amin);
			Assert::AreEqual(-4.635588148164478, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-1.6171201483154878, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] { -4.63558814816448, -2.54523090186133, 0.874241969412896, -0.16190351264904 };

			AssertArray(first10, ExpectedData2, size);

			//////////////

			size = 200000;
			arr = random->laplace(.75, 0.5, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(6.432291394535725, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-5.141321026441119, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.75284129642070252, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[] = 
			{ 0.610570284884035, 0.905721127018003, 1.89887869162796, 0.718937532523341, 0.551393381091485,
				0.57540806081121, 0.213861746932043, 0.714426358585987, 0.518993828675387, 0.54266874996464 };
			AssertArray(first10, ExpectedData3, 10);
		
			delete random;
		}

		TEST_METHOD(test_rand_logistic_1)
		{
			Random *random = new Random();

			(void)random->seed(1400);

			int size = 1;
			double *arr = random->logistic(0.32);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(-1.6374760957412662, amax);

			double amin = GetMin(arr,size);
			print(amin);
			Assert::AreEqual(-1.6374760957412662, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-1.6374760957412662, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData[1] { -1.6374760957412662 };

			AssertArray(first10, ExpectedData, 1);

			//////////////

			size = 4;
			arr = random->logistic(new double[4] { .75, .25, .5, .1 },4, 4);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(1.2164458169982604, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-6.850724032638232, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-2.5541489883857613, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] { -6.85072403263823, -4.17461033521845, 1.21644581699826, -0.407707402684622 };

			AssertArray(first10, ExpectedData2, size);

			//////////////

			size = 200000;
			arr = random->logistic(.75, 0.5, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(6.77886208503632, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-5.487892707727758, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.75353194203189111, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10]
			{ 0.501664104949021, 1.02428778459822, 2.21967826633745, 0.689692463292968, 0.409628150582071,
				0.446254436188489, -0.0388753549104508, 0.68121644303921, 0.361593774055577, 0.39654413010928 };
			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_lognormal_1)
		{
			Random *random = new Random();

			(void)random->seed(990);

			int size = 2;
			double *arr = random->lognormal(new double[2] { 4, 4 },2, size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(39.029797845778916, amax);

			double amin = GetMin(arr,size);
			print(amin);
			Assert::AreEqual(9.323290074349073, amin);

			double avg = GetAverage(arr,size);
			print(avg);
			Assert::AreEqual(24.176543960063995, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData[2] { 39.0297978457789, 9.32329007434907 };

			AssertArray(first10, ExpectedData, 2);

			//////////////

			size = 4;
			arr = random->lognormal(new double[4] { 1.75, 2.25, 3.5, 4.1 },4, 48, 4);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(1.4424793885444833E+23, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(3.95031874515845e-18, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(3.6061984713612083E+22, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] { 2413869.9068035507, 3.95031874515845E-18, 75047.424505237665, 1.4424793885444833E+23 };

			AssertArray(first10, ExpectedData2, 4);

			//////////////

			size = 200000;
			arr = random->lognormal(1.75, 53, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(1.7279293118875759e+100, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(1.937968152068317e-111, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(8.7801786884914109E+94, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10]
			{ 2.3407442872272514e-05, 2.1893911721303013e-26, 3.1873844722876965e-05, 373137879.47156078, 68.604572013266051,
				2.2378072683770408e+21, 89011700746.666718, 1.2089545786916587e-48, 5.42793001022139E-34, 7.4173419950699808e-44 };

			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_logseries_1)
		{
			Random *random = new Random();

			(void)random->seed(9909);

			int size = 40;
			long *arr = random->logseries(new double[2] { 0.1, 0.99 }, 2, size);

			long amax = GetMax(arr,size);
			print(amax);
			Assert::AreEqual((long)184, amax);

			long amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)1, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(13.925, avg);

			long first10[10];
			FirstTen(arr, first10);
			print(first10);

			long ExpectedData[10] { 1, 26, 1, 3, 1, 26, 1, 23, 1, 3 };
			AssertArray(first10, ExpectedData, 10);

			//////////////

			size = 1600;
			arr = random->logseries(new double[4] { .75, .25, .5, .1 }, 4, 1600);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)14, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)1, amin);

			avg = GetAverage(arr,size);
			print(avg);
			Assert::AreEqual(1.466875, avg);

			FirstTen(arr, first10);
			print(first10);

			long ExpectedData2[10]
			{ 5, 1, 4, 1, 1, 1, 1, 1, 5, 1 };

			AssertArray(first10, ExpectedData2, 10);

			//////////////

			size = 200000;
			arr = random->logseries(.334455, 200000);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)10, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)1, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.2335, avg);

			FirstTen(arr, first10);
			print(first10);

			long ExpectedData3[10] { 1, 2, 1, 1, 1, 1, 2, 1, 1, 1 };

			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_multinomial_1)
		{
			Random *random = new Random();

			(void)random->seed(9909);

			int size = 6000;
			double dv = 1.0 / 6.0;
			long *arr = random->multinomial(20, new double[6] { dv, dv, dv, dv, dv, dv },6,1000);


			long amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)10, amax);

			long amin = GetMin(arr,size);
			print(amin);
			Assert::AreEqual((long)0, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(3.3333333333333335, avg);

			long first10[10];
			FirstTen(arr, first10);
			print(first10);

			long ExpectedData[10]
			{ 3, 1, 4, 6, 5, 1, 4, 7, 1, 3 };

			AssertArray(first10, ExpectedData, 10);

			//////////////

			size = 6;
			dv = 1.0 / 7.0;
			arr = random->multinomial(100, new double[6] { dv, dv, dv, dv, dv, 2 / 7 }, 6);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)33, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)12, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(16.666666666666668, avg);

			FirstTen(arr, first10);
			print(first10);

			long ExpectedData2[6] { 17, 12, 12, 12, 14, 33 };


			AssertArray(first10, ExpectedData2, 6);

			//////////////
			size = 20000;
			dv = 1.0 / 6.0;
			arr = random->multinomial(20, new double[6] { dv, dv, dv, dv, dv, dv }, 6, size);

			amax = GetMax(arr, size*6);
			print(amax);
			Assert::AreEqual((long)12, amax);

			amin = GetMin(arr, size*6);
			print(amin);
			Assert::AreEqual((long)0, amin);

			avg = GetAverage(arr, size*6);
			print(avg);
			Assert::AreEqual(3.3333333333333335, avg);

			FirstTen(arr, first10);
			print(first10);

			long ExpectedData3[10]
			{ 4, 3, 4, 0, 1, 8, 5, 1, 1, 4 };

			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_multivariate_normal_1)
		{
		}

		TEST_METHOD(test_rand_noncentral_chisquare_1)
		{
			Random *random = new Random();

			(void)random->seed(904);

			int size = 100000;
			double *arr = random->noncentral_chisquare(3, 20, size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(78.04001829193612, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.6572503967444088, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(22.999416481542255, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]{ 32.507966101474, 29.5670038054405, 35.4646231172001, 48.8509128727834, 13.8878542193038,
				22.5887773956417, 18.8177210998047, 6.62646485637076, 14.7716354200521, 17.592124636122 };

			AssertArray(first10, ExpectedData1, 10);

			size = 25 * 25;
			arr = random->noncentral_chisquare(arange_Double(1, (25 * 25) + 1),size, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(1393.919486168914, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(526.5979763470843, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(938.60218769525386, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[10]{ 693.625713231652, 667.317783374469, 628.424318152006, 585.208004459504, 758.222190671585,
				611.122162793199, 659.549979398019, 611.408655748793, 689.18666064001, 657.624462137622 };
			AssertArray(first10, ExpectedData2, 10);

			delete random;
		}

		TEST_METHOD(test_rand_noncentral_f_1)
		{
			Random *random = new Random();

			(void)random->seed(95);

			int size = 1000;
			double *arr = random->noncentral_f(1, 20, 48, size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(166.41578272134745, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(10.185919423402394, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(54.230679084788719, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData[10]
			{  37.2983096021906, 62.5743394917022, 68.0628233514137, 40.5905707097552, 44.4521020211498,
				44.5743262503621, 31.4443325580866, 79.5522652120909, 44.9345096357582, 43.0357724523705  };

			AssertArray(first10, ExpectedData, 10);

			//////////////

			size = 4;
			arr = random->noncentral_f(new double[4] { 1.75, 2.25, 3.5, 4.1 }, 4, 20, 48, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(32.707452977686614, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(7.906607829343024, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(20.832891192705823, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] { 32.7074529776866, 21.9051515376809, 20.8123524261127, 7.90660782934302 };

			AssertArray(first10, ExpectedData2, 4);

			//////////////

			size = 200000;
			arr = random->noncentral_f(1.75, 3, 53, 200000);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(195494.05842774129, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(1.7899396924741655, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(90.217557446706948, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10]
			{  27.8838758530967, 15.3064263302261, 19.9558173630626, 79.9200280113332, 20.6836524868276, 30.6831175354627,
				5.30452049710313, 26.3487042626856, 68.0419650815286, 33.3860982701053 };

			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_normal_1)
		{
			Random *random = new Random();

			(void)random->seed(96);

			int size = 1000;
			double *arr = random->normal(1, 48, size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(174.27339866101488, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-184.13383136259313, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.3384280414551479, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]
			{  4.35269318876786, -1.3359262138713, -30.667818087132, 18.1537231001315, 7.33791680475021, -40.6840900065982,
				-23.9673401331787, 4.86724597788797, -56.7059911346956, -46.8495243913698 };

			AssertArray(first10, ExpectedData1, 10);

			//////////////

			size = 4;
			arr = random->normal(new double[4] { 1.75, 2.25, 3.5, 4.1 }, 4, 48, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(8.4980511462390664, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-11.788045757012442, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-0.6369144011832439, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] { 8.49805114623907, 4.5687120247507, -3.8263750187103, -11.7880457570124 };

			AssertArray(first10, ExpectedData2, 4);

			//////////////

			size = 200000;
			arr = random->normal(1.75, 53, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(244.03504143481928, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-219.87697184024597, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.6568977790583879, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10]
			{ -121.150720139349, 21.11738895264, -51.4329434180368, -20.2277628440144, 11.3070623804567, 68.6964951609887,
				14.9030666848521, 14.5434279312901, -75.4223378124162, -77.6809906511862 };

			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_pareto_1)
		{
			Random *random = new Random();

			(void)random->seed(993);

			int size = 1000;
			double *arr = random->pareto(3.0, size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(24.96518603159547, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.00036157241516243666, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.477574229815943, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]
			{  0.0993272506077987, 0.0632216551625344, 0.229595889665275, 0.0490841003810061, 0.14960141331638,
				0.198550887147615, 0.409402255566846, 1.41809527963073, 0.234607767811714, 1.36090103390802 };

			AssertArray(first10, ExpectedData1, 10);

			//////////////

			size = 4;
			arr = random->pareto(new double[4] { 1.75, 2.25, 3.5, 4.1 }, 4, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(0.6528914455240786, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.21118736038282426, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.3502539908521613, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] { 0.211187360382824, 0.281760708780893, 0.652891445524079, 0.255176448720849 };

			AssertArray(first10, ExpectedData2, 4);

			//////////////

			size = 200000;
			arr = random->pareto(1.75, 200000);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(3859.2400626168223, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(4.668377588057382e-06, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.3040180422148346, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10]
			{  0.254637206129305, 0.201592604699369, 0.459924936060773, 4.74643227245266, 0.201824172600912,
				7.16520979040805, 1.23325292973775, 0.583951168774776, 1.25967279675382, 0.606609260017725  };

			AssertArray(first10, ExpectedData3, 10);
	
			delete random;
		}

		TEST_METHOD(test_rand_poisson_1)
		{
			Random *random = new Random();

			(void)random->seed(993);

			int size = 1000;
			long *arr = random->poisson(3.0, size);

			long amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)10, amax);

			long amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)0, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(2.971, avg);

			long first10[10];
			FirstTen(arr, first10);
			print(first10);

			long ExpectedData1[10] { 1, 2, 5, 2, 3, 2, 1, 3, 1, 1 };

			AssertArray(first10, ExpectedData1, 10);

			//////////////

			size = 4;
			arr = random->poisson(new double[4] { 1.75, 2.25, 3.5, 4.1 },4,  size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)8, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)2, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(4.0, avg);

			FirstTen(arr, first10);
			print(first10);

			long ExpectedData2[4] { 3, 2, 3, 8 };

			AssertArray(first10, ExpectedData2, size);

			//////////////

			size = 200000;
			arr = random->poisson(1.75, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)10, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)0, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.750835, avg);

			FirstTen(arr, first10);
			print(first10);

			long ExpectedData3[10] { 1, 2, 1, 2, 1, 1, 3, 1, 0, 1 };
			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_power_1)
		{
			Random *random = new Random();

			(void)random->seed(339);

			int size = 1000;
			double *arr = random->power(3.0,size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(0.9997291715154072, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.0796295209657556, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.736198803204049, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]
			{ 0.860761589057571, 0.555711431358782, 0.647865885680754, 0.805516152993246, 0.994108927361731,
				0.291749587351287, 0.491236160654437, 0.91187220874109, 0.895559077755727, 0.512379748843543 };

			AssertArray(first10, ExpectedData1, 10);

			//////////////

			size = 4;
			arr = random->power(new double[4] { 1.75, 2.25, 3.5, 4.1 }, 4, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(0.9395632737653083, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.22030398605902315, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.66946509635916251, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] { 0.220303986059023, 0.584420028685334, 0.939563273765308, 0.933573096926984 };

			AssertArray(first10, ExpectedData2, 4);

			//////////////

			size = 200000;
			arr = random->power(1.75, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(0.999997969432272, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.0015667150072222488, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.63524484726740371, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10]
			{ 0.957438465302022, 0.656302195683223, 0.870285272269396, 0.65563575104881, 0.382317153477581,
				0.473016685638606, 0.269503426405144, 0.618646989401598, 0.689636449821986, 0.968887821301467 };
			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_rayleigh_1)
		{
			Random *random = new Random();

			(void)random->seed(340);

			int size = 1000;
			double *arr = random->rayleigh(3.0, size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(12.655472555426252, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.03751339800630575, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(3.7877220061900312, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]
			{ 2.43897640059091, 8.01106477940565, 3.75636773557997, 2.75071627042742, 3.50044794805526,
				2.3348708243971, 2.72360123915397, 1.493300451875, 3.45229839186894, 3.49037922967409 };

			AssertArray(first10, ExpectedData1, 10);

			//////////////

			size = 4;
			arr = random->rayleigh(new double[4] { 1.75, 2.25, 3.5, 4.1 }, 4, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(6.167496650864407, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(2.1822268243240774, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(3.6682412908284565, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] { 2.18222682432408, 6.16749665086441, 4.02795272869782, 2.29528895942752 };

			AssertArray(first10, ExpectedData2, 4);

			//////////////

			size = 200000;
			arr = random->rayleigh(1.75, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(9.652865073262221, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.001193901667090907, amin);

			avg = GetAverage(arr,size);
			print(avg);
			Assert::AreEqual(2.1895198415066424, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10]
			{ 2.61634373132961, 1.07692356475904, 1.11315967338487, 1.78054118171648, 3.3673639819406,
				2.17148238882592, 1.51452779296601, 1.5852297946265, 1.34314761271206, 1.7460907476427 };
			AssertArray(first10, ExpectedData3, 10);
	
			delete random;
		}

		TEST_METHOD(test_rand_standard_cauchy_1)
		{
			Random *random = new Random();

			(void)random->seed(341);

			int size = 1000;
			double *arr = random->standard_cauchy(size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(119.96347941712416, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-1762.3426492479496, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-2.3636040352933994, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]
			{ -1.81609229068342, 1.91173957116114, 0.975089553177401, -1.04547287084333, 0.683693504006227,
				0.291855386511429, 0.293944262698167, -0.516960388849179, -0.555803130754829, 0.450452949336812 };

			AssertArray(first10, ExpectedData1, 10);

			//////////////

			size = 40 * 40 * 40;
			arr = random->standard_cauchy(size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(6790.0984049732115, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-7960.846314730899, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-0.082440258910342012, avg);

			FirstTen(arr, first10);
			print(first10);

			//double ExpectedData2[4] { 2.18222682432408, 6.16749665086441, 4.02795272869782, 2.29528895942752 };

			//AssertArray(first10, ExpectedData2, 4);

			//////////////

			size = 200000;
			arr = random->standard_cauchy(200000);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(339868.595567882931, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-531632.9516028948, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-1.1522703389124869, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10]
			{  -606.705600585211, 1.00099219805413, 3.84125038943826, 3.29242690381019, 0.762390596914478,
				-0.693882237673906, -1.58555557371248, 0.0288085451238071, 0.223559949952255, -0.491252720933577};
			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_standard_exponential_1)
		{
			Random *random = new Random();

			(void)random->seed(342);

			int size = 1000;
			double *arr = random->standard_exponential((size));

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(7.845450080613971, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.000957677168862067, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.95747448591944373, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]
			{ 0.0672455384363819, 1.72773856689459, 0.0416591226003737, 0.859995164850942, 2.7223649344324,
				0.387003534097772, 0.566575522833212, 0.471953892051724, 0.943842382849267, 0.462705110060641  };

			AssertArray(first10, ExpectedData1, 10);

			//////////////

			size = 40 * 40 * 40;
			arr = random->standard_exponential((size));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(14.670851706894148, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(3.2483019803437364e-06, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.0005051554345421, avg);

			FirstTen(arr, first10);
			//print(first10);

			//ExpectedData = new double[] { 2.18222682432408, 6.16749665086441, 4.02795272869782, 2.29528895942752 };

			//AssertArray(first10, ExpectedData);

			//////////////

			size = 200000;
			arr = random->standard_exponential((size));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(13.182081709247655, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(2.027596493697772e-06, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.99718696494509074, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10]
			{ 0.488334808779715, 0.168223919940456, 0.0136412274229035, 2.0590019919057, 0.796347313391503,
				0.33887126547427, 3.22401306826448, 0.0166442087394634, 0.388772583086659, 1.66698190745057};
			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_standard_gamma_1)
		{
			Random *random = new Random();

			(void)random->seed(343);

			int size = 1000;
			double *arr = random->standard_gamma(2, (size));

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(8.487881881997914, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.02703203845745107, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(2.0664892234714194, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]
			{ 1.95625608491184, 0.989125986532122, 1.88369935521339, 1.54094528165047, 1.42058613854111,
				1.39144717925845, 3.90202198984505, 2.02962350797562, 1.37579217601442, 0.249730456876706  };

			AssertArray(first10, ExpectedData1, 10);

			//////////////

			size = 40 * 40 * 40;
			arr = random->standard_gamma(4, (size));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(20.303635939520916, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.13958581729156325, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(3.9903202322714604, avg);

			FirstTen(arr, first10);
			//print(first10);

			//ExpectedData = new double[] { 2.18222682432408, 6.16749665086441, 4.02795272869782, 2.29528895942752 };

			//AssertArray(first10, ExpectedData);

			//////////////

			size = 200000;
			arr = random->standard_gamma(.25, (size));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(10.220104410232194, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(3.1679769713465744e-27, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.25077723866032581, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[10]
			{ 0.302178662964724, 0.00088347223482912, 0.0243458718679699, 0.000819952033713318, 0.393307762008716,
				0.0433768892245009, 0.102277516573177, 0.124627645927922, 0.00189026208743131, 0.000392554971650297};
			AssertArray(first10, ExpectedData2, 10);
	

			delete random;
		}

		TEST_METHOD(test_rand_standard_normal_1)
		{
			Random *random = new Random();

			(void)random->seed(8877);

			int size = 5000000;
			double *arr = random->standard_normal((size));

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(5.1189159770119135, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-5.151481557636637, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			//Assert.AreEqual(-0.00054225209998402292, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData[10]{ 0.345140531263051, 0.38484191742645, 1.08309197380133, 0.586824429286654,
				-1.10076748173233, 1.2922798767235, -0.604010755236405, -0.191509685425675, 0.539265713947259, 2.01982669933162 };


			AssertArray(first10, ExpectedData, 10);

			delete random;
		}

		TEST_METHOD(test_rand_standard_t_1)
		{
			Random *random = new Random();

			(void)random->seed(344);

			int size = 1000;
			double *arr = random->standard_t(10, size);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(3.6181129587245118, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-4.331442584542253, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.008243788245885007, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]
			{ 1.32919988910514, 1.41988054853764, -0.350014562835158, -0.491530274577331, -0.560206293757909,
				-0.414149339849108, -0.117540851340351, 0.536494981378295, 0.555551140675012, 0.97583215939303  };

			AssertArray(first10, ExpectedData1, 10);

			//////////////

			size = 40 * 40 * 40;
			arr = random->standard_t(40, (size));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(4.8492343665605855, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-4.830322764567837, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(-0.0013889376393183856, avg);

			FirstTen(arr, first10);
			//print(first10);

			//ExpectedData = new double[] { 2.18222682432408, 6.16749665086441, 4.02795272869782, 2.29528895942752 };

			//AssertArray(first10, ExpectedData);

			//////////////

			size = 200000;
			arr = random->standard_t(20000, (size));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(4.79681839607772, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-4.509851741097332, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.0034736156950519494, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[10]
			{ 1.59719945912947, 0.588822724180777, 2.46314177327573, 1.21826362909316, 0.891175891085618,
				1.04841765769683, 0.272819255529087, -0.750835572730511, 0.166532753161041, 0.120173214909434};
			AssertArray(first10, ExpectedData2, 10);

			delete random;
		}

		TEST_METHOD(test_rand_triangular_1)
		{
			Random *random = new Random();

			(void)random->seed(967);

			int size = 1000;
			double *arr = random->triangular(1, 20, 48, (size));

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(47.283590040731276, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(1.3745444760000458, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(22.84033382253185, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]
			{  9.50587777489781, 31.0858101320923, 24.4425252216325, 26.4386740158624, 23.2596363787258,
				18.4928025305063, 34.0429111775129, 35.5906768428026, 26.6387923344804, 13.1207671453104   };

			AssertArray(first10, ExpectedData1, 10);

			//////////////

			size = 4;
			arr = random->triangular(new double[4] { 1.75, 2.25, 3.5, 4.1 }, 4, 20, 48, (size));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(36.98020459545957, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(11.377646176644985, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(21.71455146888546, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[4] { 11.377646176645, 14.7556728019968, 23.7446823014404, 36.9802045954596 };

			AssertArray(first10, ExpectedData2, 4);

			//////////////
			size = 200000;
			arr = random->triangular(1.75, 3, 53, (size));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(52.898787213103205, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(1.769502136707926, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(19.270976111586553, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData3[10]
			{ 7.49338977326848, 30.4470685970528, 7.42130797027446, 22.4573572092656, 11.1780179210463,
				32.2198352230619, 21.1615117904956, 22.2143198349095, 4.23809367722936, 42.6411462335993 };

			AssertArray(first10, ExpectedData3, 10);

			delete random;
		}

		TEST_METHOD(test_rand_uniform_1)
		{
			Random *random = new Random();

			(void)random->seed(5461);

			int size = 5000000;
			double *arr = random->uniform(-1, 1, 5000000);

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(0.9999998733604805, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(-0.999999696666168, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(6.643562403733275E-05, avg);


			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData[10]{ -0.0861951950449646, -0.916354322633752, 0.979020117468637,
				-0.196568936650096, -0.875996989224622, 0.106127858765562, 0.143469747306076, 0.375363669127145,
				-0.590520508804662, -0.18455495968856 };

			AssertArray(first10, ExpectedData, 10);

			delete random;
		}

		TEST_METHOD(test_rand_uniform_2)
		{
			Random *random = new Random();

			(void)random->seed(5461);

			double low[4] { 9.0, 8.0, 7.0, 1.0 };
			double high[4] { 30.0, 22.0, 10.0, 3.0 };
			
			int size = 4;
			double *arr = random->uniform(low, 4, high, 4, (size));

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(18.594950452027874, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(1.803431063349904, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(9.7381078582861171, avg);


			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData[4]{ 18.594950452027874, 8.5855197415637328, 9.9685301762029539, 1.803431063349904 };
			AssertArray(first10, ExpectedData, 4);

			delete random;
		}

		TEST_METHOD(test_rand_vonmises_1)
		{
			Random *random = new Random();

			(void)random->seed(909);

			int size = 100000;
			double *arr = random->vonmises(0.0, 4.0, (size));

			double amax = GetMax(arr, size);
			print(amax);
			//Assert.AreEqual(2.966642390532069, amax);

			double amin = GetMin(arr, size);
			print(amin);
			//Assert.AreEqual(-3.1288073003877273, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			//Assert.AreEqual(0.001468156147666098, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);
			//AssertArray(first10, new double[]
			//{ 32.507966101474, 29.5670038054405, 35.4646231172001, 48.8509128727834, 13.8878542193038,
			//  22.5887773956417, 18.8177210998047, 6.62646485637076, 14.7716354200521, 17.592124636122  });

			size = 25 * 25;
			arr = random->vonmises(arange_Double(1, (25 * 25) + 1), size, size);

			amax = GetMax(arr, size);
			print(amax);
			//Assert.AreEqual(3.1128949170937865, amax);

			amin = GetMin(arr, size);
			print(amin);
			//Assert.AreEqual(-3.141056472523122, amin);

			avg = GetAverage(arr, size);
			print(avg);
			//Assert.AreEqual(0.004769515035667695, avg);

			FirstTen(arr, first10);
			print(first10);
			//AssertArray(first10, new double[]
			//{ 693.625713231652, 667.317783374469, 628.424318152006, 585.208004459504, 758.222190671585,
			//  611.122162793199, 659.549979398019, 611.408655748793, 689.18666064001, 657.624462137622 });

			delete random;
		}

		TEST_METHOD(test_rand_wald_1)
		{
			Random *random = new Random();

			(void)random->seed(964);

			int size = 100000;
			double *arr = random->wald(3, 20, (size));

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(12.349279351617012, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.6001989474303744, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(3.0025012751386755, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]{ 5.03038470107277, 2.90812891815563, 1.80122277240371, 2.88227044267389, 2.36018483421244,
				5.25349661810997, 4.0632949031683, 3.43307360806721, 5.10604488857464, 2.87984364192777 };
			AssertArray(first10, ExpectedData1, 10);

			size = 25 * 25;
			arr = random->wald(arange_Double(1, (25 * 25) + 1), size, size);

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(2928.8874732379727, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.9848685492507449, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(309.35420489337906, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[10]{ 0.984868549250745, 1.96127772419652, 2.99050918468426, 4.17290733434684, 5.07438725183429,
				6.01195019448229, 7.48340958133606, 8.07272756076527, 11.1740022984504, 11.024796322117 };
			AssertArray(first10, ExpectedData2, 10);

			delete random;
		}

		TEST_METHOD(test_rand_weibull_1)
		{
			Random *random = new Random();

			(void)random->seed(974);

			int size = 100000;
			double *arr = random->weibull(5, (size));

			double amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(1.6435302608550746, amax);

			double amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.07812030376364504, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.91828955683190416, avg);

			double first10[10];
			FirstTen(arr, first10);
			print(first10);

			double ExpectedData1[10]{ 0.802382756919613, 0.846126922902903, 0.849922504835084, 1.00496226793646, 1.24355344592726,
				0.936599096578893, 0.798344330459658, 0.870912076415209, 1.05590855634161, 1.08944476675504 };

			AssertArray(first10, ExpectedData1, 10);

			size = 25 * 25;
			arr = random->weibull(arange_Double(1, (25 * 25) + 1), size,(size));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual(1.13239084970226, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual(0.3435024689351767, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(0.99456850758012483, avg);

			FirstTen(arr, first10);
			print(first10);

			double ExpectedData2[10]{ 0.343502468935177, 1.07141491719323, 1.11355099054115, 0.599151817962907, 0.745122210078631,
				1.13239084970226, 0.897388933089434, 0.945279231187368, 0.915902530093467, 1.13094361305955 };

			AssertArray(first10, ExpectedData2, 10);

			delete random;
		}

		TEST_METHOD(test_rand_zipf_1)
		{
			Random *random = new Random();

			(void)random->seed(979);

			int size = 100000;
			long *arr = random->zipf(5.2, (100000));

			long amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)9, amax);

			long amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)1, amin);

			double avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.03708, avg);

			long first10[10];
			FirstTen(arr, first10);
			long ExpectedData1[10]{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

			AssertArray(first10, ExpectedData1, 10);

			size = 25 * 25;
			arr = random->zipf(arange_Double(2, (25 * 25) + 2), size, (size));

			amax = GetMax(arr, size);
			print(amax);
			Assert::AreEqual((long)2, amax);

			amin = GetMin(arr, size);
			print(amin);
			Assert::AreEqual((long)1, amin);

			avg = GetAverage(arr, size);
			print(avg);
			Assert::AreEqual(1.0016, avg);

			FirstTen(arr, first10);
			print(first10);

			long ExpectedData2[10]{ 2, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
			AssertArray(first10, ExpectedData2, 10);

			delete random;
		}

	};
}
