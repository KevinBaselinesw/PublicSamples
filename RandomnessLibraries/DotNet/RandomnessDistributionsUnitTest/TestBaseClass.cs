using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomnessDistributionsUnitTest
{
    public class TestBaseClass
    {
        [ClassInitialize]
        public static void CommonInit(TestContext t)
        {
            //  numpy.InitializeNumpyLibrary();
        }

        [TestInitialize]
        public void FunctionInit()
        {
        }

        protected void print(object obj)
        {
            if (obj == null)
            {
                Console.WriteLine("<null>");
                return;
            }
            if (obj is Array)
            {
                foreach (var o in (Array)obj)
                {
                    Console.Write(o.ToString() + ",");
                }
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine(obj.ToString());
            }
        }
        protected void print(string label, object obj)
        {
            Console.WriteLine(label + obj.ToString());
        }
        //protected void print<T>(string label, T[] array)
        //{
        //    Console.WriteLine(label + ArrayToString(array));
        //}


        internal void AssertArray<T>(T[] arrayData, T[] expectedData)
        {
            int length = expectedData.Length;

            for (int i = 0; i < length; i++)
            {
                T E1 = expectedData[i];
                T A1 = (T)arrayData[i];

                Assert.AreEqual(E1, A1);
            }
        }
        internal void AssertArray(double[] arrayData, double[] expectedData)
        {
            int length = expectedData.Length;

            for (int i = 0; i < length; i++)
            {
                double E1 = expectedData[i];
                double A1 = (double)arrayData[i];

                if (double.IsNaN(E1) && double.IsNaN(A1))
                    continue;
                if (double.IsInfinity(E1) && double.IsInfinity(A1))
                    continue;

                Assert.AreEqual(E1, A1, 0.00000001);
            }
        }

    }


    public static class Extensions
    {
        public static double Average(this sbyte[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        public static double Average(this byte[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        public static double Average(this Int16[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        public static double Average(this UInt16[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        public static double Average(this UInt32[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        public static double Average(this UInt64[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }

        public static sbyte[] FirstTen(this sbyte[] array)
        {
            sbyte[] firstTen = new sbyte[10];
            Array.Copy(array, firstTen, 10);
            return firstTen;
        }
        public static byte[] FirstTen(this byte[] array)
        {
            byte[] firstTen = new byte[10];
            Array.Copy(array, firstTen, 10);
            return firstTen;
        }
        public static Int16[] FirstTen(this Int16[] array)
        {
            Int16[] firstTen = new Int16[10];
            Array.Copy(array, firstTen, 10);
            return firstTen;
        }
        public static UInt16[] FirstTen(this UInt16[] array)
        {
            UInt16[] firstTen = new UInt16[10];
            Array.Copy(array, firstTen, 10);
            return firstTen;
        }
        public static Int32[] FirstTen(this Int32[] array)
        {
            Int32[] firstTen = new Int32[10];
            Array.Copy(array, firstTen, 10);
            return firstTen;
        }
        public static UInt32[] FirstTen(this UInt32[] array)
        {
            UInt32[] firstTen = new UInt32[10];
            Array.Copy(array, firstTen, 10);
            return firstTen;
        }
        public static Int64[] FirstTen(this Int64[] array)
        {
            Int64[] firstTen = new Int64[10];
            Array.Copy(array, firstTen, 10);
            return firstTen;
        }
        public static UInt64[] FirstTen(this UInt64[] array)
        {
            UInt64[] firstTen = new UInt64[10];
            Array.Copy(array, firstTen, 10);
            return firstTen;
        }
        public static float[] FirstTen(this float[] array)
        {
            float[] firstTen = new float[10];
            Array.Copy(array, firstTen, 10);
            return firstTen;
        }
        public static double[] FirstTen(this double[] array)
        {
            double[] firstTen = new double[10];
            Array.Copy(array, firstTen, 10);
            return firstTen;
        }


    }
}
