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


	};
}
