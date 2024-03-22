using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


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

        internal double[] arange_Double(int start, int end)
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
            bool[] output = new bool[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                output[i] = arr[i] == c;
            }
            return output;
        }


        internal int CountTrues(bool[] v)
        {
            int count = 0;

            foreach (var b in v)
            {
                count += b == true ? 1 : 0;
            }

            return count;
        }

        internal double GetMax(double[] arr)
        {
            double highNum = double.MinValue;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] > highNum)
                    highNum = arr[i];
            }
            return highNum;
        }
        internal long GetMax(long[] arr)
        {
            long highNum = long.MinValue;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] > highNum)
                    highNum = arr[i];
            }
            return highNum;
        }
        internal ulong GetMax(ulong[] arr)
        {
            ulong highNum = ulong.MinValue;
            int i;

            for (i = 0; i < arr.Length; i++) 
            {
                if (arr[i] > highNum)
                    highNum = arr[i];
            }
            return highNum;
        }
        internal int GetMax(int[] arr)
        {
            int highNum = int.MinValue;
            int i;

            for (i = 0; i < arr.Length; i++) 
            {
                if (arr[i] > highNum)
                    highNum = arr[i];
            }
            return highNum;
        }
        internal uint GetMax(uint[] arr)
        {
            uint highNum = uint.MinValue;
            int i;

            for (i = 0; i < arr.Length; i++) 
            {
                if (arr[i] > highNum)
                    highNum = arr[i];
            }
            return highNum;
        }
        internal short GetMax(short[] arr)
        {
            short highNum = short.MinValue;
            int i;

            for (i = 0; i < arr.Length; i++) 
            {
                if (arr[i] > highNum)
                    highNum = arr[i];
            }
            return highNum;
        }
        internal ushort GetMax(ushort[] arr)
        {
            ushort highNum = ushort.MinValue;
            int i;

            for (i = 0; i < arr.Length; i++) 
            {
                if (arr[i] > highNum)
                    highNum = arr[i];
            }
            return highNum;
        }
        internal sbyte GetMax(sbyte[] arr)
        {
            sbyte highNum = sbyte.MinValue;
            int i;

            for (i = 0; i < arr.Length; i++) 
            {
                if (arr[i] > highNum)
                    highNum = arr[i];
            }
            return highNum;
        }
        internal byte GetMax(byte[] arr)
        {
            byte highNum = byte.MinValue;
            int i;

            for (i = 0; i < arr.Length; i++) 
            {
                if (arr[i] > highNum)
                    highNum = arr[i];
            }
            return highNum;
        }
        internal bool GetMax(bool[] arr)
        {
            bool highNum = false;
            int i;

            for (i = 0; i < arr.Length; i++) 
            {
                if (arr[i] == true)
                    highNum = arr[i];
            }
            return highNum;
        }

        internal double GetMin(double[] arr)
        {
            double lowNum = double.MaxValue;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] < lowNum)
                    lowNum = arr[i];
            }
            return lowNum;
        }
        internal long GetMin(long[] arr)
        {
            long lowNum = long.MaxValue;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] < lowNum)
                    lowNum = arr[i];
            }
            return lowNum;
        }
        internal ulong GetMin(ulong[] arr)
        {
            ulong lowNum = ulong.MaxValue;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] < lowNum)
                    lowNum = arr[i];
            }
            return lowNum;
        }
        internal int GetMin(int[] arr)
        {
            int lowNum = int.MaxValue;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] < lowNum)
                    lowNum = arr[i];
            }
            return lowNum;
        }
        internal uint GetMin(uint[] arr)
        {
            uint lowNum = uint.MaxValue;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] < lowNum)
                    lowNum = arr[i];
            }
            return lowNum;
        }
        internal short GetMin(short[] arr)
        {
            short lowNum = short.MaxValue;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] < lowNum)
                    lowNum = arr[i];
            }
            return lowNum;
        }
        internal ushort GetMin(ushort[] arr)
        {
            ushort lowNum = ushort.MaxValue;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] < lowNum)
                    lowNum = arr[i];
            }
            return lowNum;
        }
        internal sbyte GetMin(sbyte[] arr)
        {
            sbyte lowNum = sbyte.MaxValue;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] < lowNum)
                    lowNum = arr[i];
            }
            return lowNum;
        }
        internal byte GetMin(byte[] arr)
        {
            byte lowNum = byte.MaxValue;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] < lowNum)
                    lowNum = arr[i];
            }
            return lowNum;
        }
        internal bool GetMin(bool[] arr)
        {
            bool lowNum = true;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] == false)
                    lowNum = arr[i];
            }
            return lowNum;
        }

        internal double GetAverage(double[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        internal double GetAverage(long[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        internal double GetAverage(ulong[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        internal double GetAverage(int[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        internal double GetAverage(uint[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        internal double GetAverage(short[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        internal double GetAverage(ushort[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        internal double GetAverage(sbyte[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        internal double GetAverage(byte[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a;
            }

            return total / array.Length;
        }
        internal double GetAverage(bool[] array)
        {
            double total = 0;

            foreach (var a in array)
            {
                total += a == true ? 1 : 0;
            }

            return total / array.Length;
        }

    }


    public static class Extensions
    {
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
