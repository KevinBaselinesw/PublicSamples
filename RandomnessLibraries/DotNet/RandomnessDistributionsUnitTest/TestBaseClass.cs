using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumpyDotNet;
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

        internal Int32[] arange_Int32(int start, int end)
        {
            Int32[] buffer = new int[end - start];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = i + start;
            }

            return buffer;
        }

        internal double[] arange_Float64(int start, int end)
        {
            double[] buffer = new double[end - start];

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (double)(i + start);
            }

            return buffer;
        }

        internal bool[] CompareArray(long[] arr, int c)
        {
            ndarray a = np.array(arr) == c;
            return a.AsBoolArray();
        }


        internal int CountTrues(bool[] v)
        {
            ndarray a = np.sum(np.array(v));
            return (Int32)a.GetItem(0);
        }

        internal double GetMax(double[] arr)
        {
            return (double)np.max(arr).GetItem(0);   
        }
        internal long GetMax(long[] arr)
        {
            return (long)np.max(arr).GetItem(0);
        }
        internal ulong GetMax(ulong[] arr)
        {
            return (ulong)np.max(arr).GetItem(0);
        }
        internal int GetMax(int[] arr)
        {
            return (int)np.max(arr).GetItem(0);
        }
        internal uint GetMax(uint[] arr)
        {
            return (uint)np.max(arr).GetItem(0);
        }
        internal short GetMax(short[] arr)
        {
            return (short)np.max(arr).GetItem(0);
        }
        internal ushort GetMax(ushort[] arr)
        {
            return (ushort)np.max(arr).GetItem(0);
        }
        internal sbyte GetMax(sbyte[] arr)
        {
            return (sbyte)np.max(arr).GetItem(0);
        }
        internal byte GetMax(byte[] arr)
        {
            return (byte)np.max(arr).GetItem(0);
        }
        internal bool GetMax(bool[] arr)
        {
            return (bool)np.max(arr).GetItem(0);
        }

        internal double GetMin(double[] arr)
        {
            return (double)np.min(arr).GetItem(0);
        }
        internal long GetMin(long[] arr)
        {
            return (long)np.min(arr).GetItem(0);
        }
        internal ulong GetMin(ulong[] arr)
        {
            return (ulong)np.min(arr).GetItem(0);
        }
        internal int GetMin(int[] arr)
        {
            return (int)np.min(arr).GetItem(0);
        }
        internal uint GetMin(uint[] arr)
        {
            return (uint)np.min(arr).GetItem(0);
        }
        internal short GetMin(short[] arr)
        {
            return (short)np.min(arr).GetItem(0);
        }
        internal ushort GetMin(ushort[] arr)
        {
            return (ushort)np.min(arr).GetItem(0);
        }
        internal sbyte GetMin(sbyte[] arr)
        {
            return (sbyte)np.min(arr).GetItem(0);
        }
        internal byte GetMin(byte[] arr)
        {
            return (byte)np.min(arr).GetItem(0);
        }
        internal bool GetMin(bool[] arr)
        {
            return (bool)np.min(arr).GetItem(0);
        }

        internal double GetAverage(double[] arr)
        {
            return (double)np.average(arr).GetItem(0);
        }
        internal double GetAverage(long[] arr)
        {
            return (double)np.average(arr).GetItem(0);
        }
        internal double GetAverage(ulong[] arr)
        {
            return (double)np.average(arr).GetItem(0);
        }
        internal double GetAverage(int[] arr)
        {
            return (double)np.average(arr).GetItem(0);
        }
        internal double GetAverage(uint[] arr)
        {
            return (double)np.average(arr).GetItem(0);
        }
        internal double GetAverage(short[] arr)
        {
            return (double)np.average(arr).GetItem(0);
        }
        internal double GetAverage(ushort[] arr)
        {
            return (double)np.average(arr).GetItem(0);
        }
        internal double GetAverage(sbyte[] arr)
        {
            return (double)np.average(arr).GetItem(0);
        }
        internal double GetAverage(byte[] arr)
        {
            return (double)np.average(arr).GetItem(0);
        }
        internal double GetAverage(bool[] arr)
        {
            return (double)np.average(arr).GetItem(0);
        }

    }


    public static class Extensions
    {
        private static double Average(this sbyte[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        private static double Average(this byte[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        private static double Average(this Int16[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        private static double Average(this UInt16[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        private static double Average(this UInt32[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        private static double Average(this UInt64[] array)
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
            sbyte[] firstTen = new sbyte[Math.Min(10, array.Length)];
            Array.Copy(array, firstTen, firstTen.Length);
            return firstTen;
        }
        public static byte[] FirstTen(this byte[] array)
        {
            byte[] firstTen = new byte[Math.Min(10, array.Length)];
            Array.Copy(array, firstTen, firstTen.Length);
            return firstTen;
        }
        public static Int16[] FirstTen(this Int16[] array)
        {
            Int16[] firstTen = new Int16[Math.Min(10, array.Length)];
            Array.Copy(array, firstTen, firstTen.Length);
            return firstTen;
        }
        public static UInt16[] FirstTen(this UInt16[] array)
        {
            UInt16[] firstTen = new UInt16[Math.Min(10, array.Length)];
            Array.Copy(array, firstTen, firstTen.Length);
            return firstTen;
        }
        public static Int32[] FirstTen(this Int32[] array)
        {
            Int32[] firstTen = new Int32[Math.Min(10, array.Length)];
            Array.Copy(array, firstTen, firstTen.Length);
            return firstTen;
        }
        public static UInt32[] FirstTen(this UInt32[] array)
        {
            UInt32[] firstTen = new UInt32[Math.Min(10, array.Length)];
            Array.Copy(array, firstTen, firstTen.Length);
            return firstTen;
        }
        public static Int64[] FirstTen(this Int64[] array)
        {
            Int64[] firstTen = new Int64[Math.Min(10, array.Length)];
            Array.Copy(array, firstTen, firstTen.Length);
            return firstTen;
        }
        public static UInt64[] FirstTen(this UInt64[] array)
        {
            UInt64[] firstTen = new UInt64[Math.Min(10, array.Length)];
            Array.Copy(array, firstTen, firstTen.Length);
            return firstTen;
        }
        public static float[] FirstTen(this float[] array)
        {
            float[] firstTen = new float[Math.Min(10, array.Length)];
            Array.Copy(array, firstTen, firstTen.Length);
            return firstTen;
        }
        public static double[] FirstTen(this double[] array)
        {
            double[] firstTen = new double[Math.Min(10, array.Length)];
            Array.Copy(array, firstTen, firstTen.Length);
            return firstTen;
        }


    }
}
