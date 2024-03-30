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

#pragma region Simple Random Data


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
			char ExpectedArray[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedArray, 4);

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

		TEST_METHOD(test_randuint8_1)
		{
			Random *random = new Random();

			random->seed(1313);

			uchar *arr = random->randuint8(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.SByte[]));
			print(arr);
			uchar ExpectedArray[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedArray, 4);

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
		}

		TEST_METHOD(test_randint16_1)
		{
			Random *random = new Random();

			random->seed(8381);

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
		}

		TEST_METHOD(test_randuint16_1)
		{
			Random *random = new Random();

			random->seed(5555);

			ushort* arr = random->randuint16(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.UInt16[]));
			print(arr);

			ushort ExpectedData[4]{ 2, 2, 2, 2 };
			//AssertArray(arr, ExpectedData, 4);

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
		}

		TEST_METHOD(test_randint32_1)
		{
			Random *random = new Random();

			random->seed(701);

			int * arr = random->randint32(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int32[]));
			print(arr);

			int ExpectedData[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedData, 4);

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
		}

		TEST_METHOD(test_randuint_1)
		{
			Random *random = new Random();

			random->seed(8357);

			uint* arr = random->randuint32(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.UInt32[]));
			print(arr);

			uint ExpectedData[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedData, 4);

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
		}


		TEST_METHOD(test_randint64_1)
		{
			Random *random = new Random();

			random->seed(10987);

			long* arr = random->randint64(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.Int64[]));
			print(arr);
			long ExpectedData[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedData, 4);

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

		}

	
		TEST_METHOD(test_randuint64_1)
		{
			Random *random = new Random();

			random->seed(1990);

			ulong *arr = random->randuint64(2, 3, 4);
			//Assert.AreEqual(arr.GetType(), typeof(System.UInt64[]));
			print(arr);
			ulong ExpectedData[4] = { 2, 2, 2, 2 };
			AssertArray(arr, ExpectedData, 4);

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

		}

		TEST_METHOD(test_rand_bytes_1)
		{
			Random *random = new Random();

			random->seed(6432);

			char br = random->getbyte();

			char *bytes = random->bytes(24);
			//Assert.AreEqual(bytes.Length, 24);

			br = random->getbyte();
			char *arr = random->bytes(24);
			//Assert.AreEqual(arr.GetType(), typeof(System.Byte[]));

			print(br);
			Assert::AreEqual((char)193, br);

			print(arr);

			char ExpectedData[24] =
			{ 18, 59, 87, 188, 116, 37, 28, 134, 204, 39, 5, 212,147, 165, 1, 136, 198, 108, 203, 151, 39, 187, 144, 209 };

			AssertArray(arr, ExpectedData, 24);

		}

#pragma endregion

#pragma region shuffle/permutation

		TEST_METHOD(test_rand_shuffle_1)
		{
			Random *random = new Random();

			random->seed(1964);

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

		}

		TEST_METHOD(test_rand_permutation_1)
		{
			Random *random = new Random();

			random->seed(1963);

			int *arr = random->permutation(10);
			print(arr);

			int ExpectedData1[10] = { 6, 7, 4, 5, 1, 2, 9, 0, 8, 3 };
			AssertArray(arr, ExpectedData1, 10);

			arr = random->permutation(arange_Int32(0, 5), 5);
			print(arr);

			int ExpectedData2[5] = { 2, 3, 1, 0, 4 };
			AssertArray(arr, ExpectedData2, 5);

		}


#pragma endregion

		TEST_METHOD(test_rand_beta_1)
		{
			Random *random = new Random();

			random->seed(5566);

			double *a = arange_Double(1, 11);
			double *b = arange_Double(1, 11);

			double *arr = random->beta(a, 10, b, 10, 10);
			//Assert.AreEqual(arr.GetType(), typeof(System.Double[]));
			print(arr);

			double ExpectedData[]
			{ 0.890517356256563, 0.532484155787344, 0.511396509650771, 0.862418837558698, 0.492458004172681,
				0.504662615187708, 0.621477223753938, 0.41702032419038, 0.492984829781569, 0.47847289036676 };

			AssertArray(arr, ExpectedData, 10);

		}

		TEST_METHOD(test_rand_binomial_1)
		{
			Random *random = new Random();

			random->seed(123);

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

		}

		TEST_METHOD(test_rand_negative_binomial_1)
		{
			Random *random = new Random();

			random->seed(123);

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

		}

		TEST_METHOD(test_rand_chisquare_1)
		{
			Random *random = new Random();

			random->seed(904);

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
		}

		//TEST_METHOD(test_rand_dirichlet_1)
		//{
		//	// "This requires multi-dimensional arrays.  Use numpydotnet if you need this function."
		//}

		TEST_METHOD(test_rand_exponential_1)
		{
			Random *random = new Random();

			random->seed(914);

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
		}

		TEST_METHOD(test_rand_exponential_2)
		{
			Random *random = new Random();

			random->seed(914);

			double *arr = random->exponential();
			Assert::AreEqual(0.37376054576568468, arr[0], doubleTolerance);
			print(arr);
		}

		TEST_METHOD(test_rand_f_1)
		{
			Random *random = new Random();

			random->seed(94);

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
		}

		TEST_METHOD(test_rand_gamma_1)
		{
			Random *random = new Random();

			random->seed(99);

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

	
		}
	};
}
