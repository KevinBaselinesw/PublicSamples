#include "pch.h"
#include "TestBaseClass.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

void TestBaseClass::print(double d)
{
	char buffer[256];

	sprintf_s(buffer, sizeof(buffer), "%d", d);
	Logger::WriteMessage(buffer);
}
