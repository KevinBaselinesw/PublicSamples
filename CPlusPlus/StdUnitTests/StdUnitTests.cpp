#include "pch.h"
#include "CppUnitTest.h"
#include <array>
#include <iostream>

using namespace std;

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace StdUnitTests
{
	TEST_CLASS(StdUnitTests)
	{
	public:
		
		TEST_METHOD(StdArray_Test1)
		{
			array<int, 5> ar1{ {3, 4, 5, 1, 2} };

			Assert::AreEqual(5, (int)ar1.size());

			// access the first element
			Assert::AreEqual(3, ar1.at(0));
			Assert::AreEqual(3, ar1[0]);
			Assert::AreEqual(3, ar1.front());
			
			// access the last element
			Assert::AreEqual(2, ar1.at(4));
			Assert::AreEqual(2, ar1[4]);
			Assert::AreEqual(2, ar1.back());


			// iterator
			int count = 0;
			for (auto it = ar1.begin(); it != ar1.end(); it++)
			{
				std::cout << *it << " ";
				count++;
			}
			Assert::AreEqual(5, count);

			// reverse iterator
			count = 0;
			for (auto it = ar1.rbegin(); it != ar1.rend(); it++)
			{
				std::cout << *it << " ";
				count++;
			}
			Assert::AreEqual(5, count);


			return;

		}

		TEST_METHOD(StdArray_Test2)
		{
			array<string, 5> ar1{ {"3", "4", "5", "1", "2"} };

			Assert::AreEqual(5, (int)ar1.size());

			// access the first element
			Assert::AreEqual((string)"3", ar1.at(0));
			Assert::AreEqual((string)"3", ar1[0]);
			Assert::AreEqual((string)"3", ar1.front());

			// access the last element
			Assert::AreEqual((string)"2", ar1.at(4));
			Assert::AreEqual((string)"2", ar1[4]);
			Assert::AreEqual((string)"2", ar1.back());


			// iterator
			int count = 0;
			for (auto it = ar1.begin(); it != ar1.end(); it++)
			{
				std::cout << *it << " ";
				count++;
			}
			Assert::AreEqual(5, count);

			// reverse iterator
			count = 0;
			for (auto it = ar1.rbegin(); it != ar1.rend(); it++)
			{
				std::cout << *it << " ";
				count++;
			}
			Assert::AreEqual(5, count);


			return;

		}

	};
}
