using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomDistributions;
using System.Linq;
using NumpyDotNet;

namespace RandomnessDistributionsUnitTest
{
    [TestClass]
    public class RandomDistributionsTests : TestBaseClass 
    {
        #region Simple Random Data
        [TestMethod]
        public void test_rand_1()
        {
            var random = new RandomDistributions.random();
            var arr = random.rand(2);
            Assert.AreEqual(arr.GetType(), typeof(System.Double[]));

            random.seed(8765);
            double f = random.rand();
            print(f);
            Assert.AreEqual(0.032785430047761466, f);

            arr = random.rand(5000000);

            arr.Max();
            var amax = arr.Max();
            print(amax);
            Assert.AreEqual(0.99999999331246381, amax);

            var amin = arr.Min();
            print(amin);
            Assert.AreEqual(7.223258213784334e-08, amin);

            var avg = arr.Average();
            print(avg);
            Assert.AreEqual(0.49987999522694609, avg);
        }

        [TestMethod]
        public void test_randn_1()
        {
            var random = new RandomDistributions.random();

            double fr = random.randn();
            var arr = random.randn(2 * 3 * 4);
            Assert.AreEqual(arr.GetType(), typeof(System.Double[]));

            random.seed(1234);

            fr = random.randn();
            print(fr);
            Assert.AreEqual(0.47143516373249306, fr);

            arr = random.randn(5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.Double[]));

            var amax = arr.Max();
            print(amax);
            Assert.AreEqual(5.19094054905629, amax);

            var amin = arr.Min();
            print(amin);
            Assert.AreEqual(-5.387212036872835, amin);

            var avg = arr.Average();
            print(avg);
            Assert.AreEqual(-0.00028345468151361842, avg);
        }

        [TestMethod]
        public void test_randbool_1()
        {
            var random = new RandomDistributions.random();

            var arr = random.randbool(2, 3, size: 4);
            Assert.AreEqual(arr.GetType(), typeof(System.Boolean[]));
            print(arr);
            //AssertArray(arr, new bool[] { false, false, false, true });

            arr = random.randbool(20, null, size:20);
            print(arr);

            arr = random.randbool(20, 21, size: 6);
            print(arr);

            arr = random.randbool(-2, 3, size: 5000000);
            print(arr.Max());
            print(arr.Min());
            //print(arr.Average());
        }

        [TestMethod]
        public void test_randint8_1()
        {
            var random = new RandomDistributions.random();

            random.seed(9292);

            var arr = random.randint8(2, 3, size: 4);
            Assert.AreEqual(arr.GetType(), typeof(System.SByte[]));
            print(arr);
            AssertArray(arr, new SByte[] { 2, 2, 2, 2 });

            arr = random.randint8(2, 8, size:5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.SByte[]));

            var amax = arr.Max();
            print(amax);
            Assert.AreEqual((sbyte)7, amax);

            var amin = arr.Min();
            print(amin);
            Assert.AreEqual((sbyte)2, amin);

            var avg = arr.Average();
            print(avg);
            Assert.AreEqual(4.4994078, avg);

            var first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new sbyte[] { 3, 6, 7, 5, 5, 5, 7, 7, 4, 2 });


            arr = random.randint8(-2, 3, size:5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.SByte[]));

            amax = arr.Max();
            print(amax);
            Assert.AreEqual((sbyte)2, amax);

            amin = arr.Min();
            print(amin);
            Assert.AreEqual((sbyte)(-2), amin);

            avg = arr.Average();
            print(avg);
            Assert.AreEqual(-0.0003146, avg);

            first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new sbyte[] { 0, 1, -1, -2, -2, -1, 2, -1, -2, -2 });

        }


        [TestMethod]
        public void test_randuint8_1()
        {
            var random = new RandomDistributions.random();

            random.seed(1313);

            var arr = random.randuint8(2, 3, size: 4);
            Assert.AreEqual(arr.GetType(), typeof(System.Byte[]));
            print(arr);
            AssertArray(arr, new Byte[] { 2, 2, 2, 2 });

            arr = random.randuint8(2, 128, size:5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.Byte[]));

            var amax = arr.Max();
            print(amax);
            Assert.AreEqual((byte)127, amax);

            var amin = arr.Min();
            print(amin);
            Assert.AreEqual((byte)2, amin);

            var avg = arr.Average();
            print(avg);
            Assert.AreEqual(64.5080776, avg);

            var first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new byte[] { 114, 37, 22, 53, 90, 27, 34, 98, 123, 114 });

        }

        [TestMethod]
        public void test_randint16_1()
        {
            var random = new RandomDistributions.random();

            random.seed(8381);

            var arr = random.randint16(2, 3, size:4);
            Assert.AreEqual(arr.GetType(), typeof(System.Int16[]));
            print(arr);
            AssertArray(arr, new Int16[] { 2, 2, 2, 2 });

            arr = random.randint16(2, 2478, size:5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.Int16[]));

            var amax = arr.Max();
            print(amax);
            Assert.AreEqual((Int16)2477, amax);

            var amin = arr.Min();
            print(amin);
            Assert.AreEqual((Int16)2, amin);

            var avg = arr.Average();
            print(avg);
            Assert.AreEqual(1239.6188784, avg);

            var first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new Int16[] { 1854, 1641, 1219, 1190, 1714, 644, 441, 484, 579, 393 });


            arr = random.randint16(-2067, 3000, size:5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.Int16[]));

            amax = arr.Max();
            print(amax);
            Assert.AreEqual((Int16)2999, amax);

            amin = arr.Min();
            print(amin);
            Assert.AreEqual((Int16)(-2067), amin);

            avg = arr.Average();
            print(avg);
            Assert.AreEqual(466.3735356, avg);

            first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new Int16[] { -1761, -1165, 2645, -1210, 1741, -1692, -1042, -354, 2637, -706 });
        }


        [TestMethod]
        public void test_randuint16_1()
        {
            var random = new RandomDistributions.random();

            random.seed(5555);

            var arr = random.randuint16(2, 3, 4);
            Assert.AreEqual(arr.GetType(), typeof(System.UInt16[]));
            print(arr);
            AssertArray(arr, new UInt16[] { 2, 2, 2, 2 });

            arr = random.randuint16(23, 12801, 5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.UInt16[]));

            var amax = arr.Max();
            print(amax);
            Assert.AreEqual((UInt16)12800, amax);

            var amin = arr.Min();
            print(amin);
            Assert.AreEqual((UInt16)23, amin);

            var avg = arr.Average();
            print(avg);
            Assert.AreEqual(6411.5873838, avg);

            var first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new UInt16[] { 3781, 11097, 4916, 2759, 12505, 827, 12017, 3982, 7669, 11128 });
        }

        [TestMethod]
        public void test_randint32_1()
        {
            var random = new RandomDistributions.random();

            random.seed(701);

            var arr = random.randint32(2, 3, 4);
            Assert.AreEqual(arr.GetType(), typeof(System.Int32[]));
            print(arr);
            AssertArray(arr, new Int32[] { 2, 2, 2, 2 });

            arr = random.randint32(9, 128000, 5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.Int32[]));

            var amax = arr.Max();
            print(amax);
            Assert.AreEqual((Int32)127999, amax);

            var amin = arr.Min();
            print(amin);
            Assert.AreEqual((Int32)9, amin);

            var avg = arr.Average();
            print(avg);
            Assert.AreEqual(64017.8102496, avg);

            var first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new Int32[] { 116207, 47965, 90333, 86510, 49239, 115311, 100417, 99653, 14878, 92386 });


            arr = random.randint32(-20000, 300000, 5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.Int32[]));

            amax = arr.Max();
            print(amax);
            Assert.AreEqual((Int32)299999, amax);

            amin = arr.Min();
            print(amin);
            Assert.AreEqual((Int32)(-20000), amin);

            avg = arr.Average();
            print(avg);
            Assert.AreEqual(139952.5096864, avg);

            first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new Int32[] { 211742, 98578, 96618, 50714, 285136, 270233, 186899, 278442, 215280, 268431 });
        }

        [TestMethod]
        public void test_randuint_1()
        {
            var random = new RandomDistributions.random();

            random.seed(8357);

            var arr = random.randuint32(2, 3, size:4);
            Assert.AreEqual(arr.GetType(), typeof(System.UInt32[]));
            print(arr);
            AssertArray(arr, new UInt32[] { 2, 2, 2, 2 });

            arr = random.randuint32(29, 13000, size:5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.UInt32[]));

            var amax = arr.Max();
            print(amax);
            Assert.AreEqual((UInt32)12999, amax);

            var amin = arr.Min();
            print(amin);
            Assert.AreEqual((UInt32)29, amin);

            var avg = arr.Average();
            print(avg);
            Assert.AreEqual(6512.7336774, avg);

            var first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new UInt32[] { 6511, 10414, 12909, 5962, 353, 8634, 8330, 1452, 5009, 5444 });
        }

        [TestMethod]
        public void test_randint64_1()
        {
            var random = new RandomDistributions.random();

            random.seed(10987);

            var arr = random.randint64(2, 3, size:4);
            Assert.AreEqual(arr.GetType(), typeof(System.Int64[]));
            print(arr);
            AssertArray(arr, new Int64[] { 2, 2, 2, 2 });

            arr = random.randint64(20, 9999999, size:5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.Int64[]));

            var amax = arr.Max();
            print(amax);
            Assert.AreEqual((Int64)9999995, amax);

            var amin = arr.Min();
            print(amin);
            Assert.AreEqual((Int64)24, amin);

            var avg = arr.Average();
            print(avg);
            Assert.AreEqual(5000256.718873, avg);

            var first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new Int64[] { 680213, 6755395, 2873894, 6180772, 6542022, 4878058, 9894982, 6332894, 1846901, 8550855 });


            arr = random.randint64(-9999999, 9999999, size:5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.Int64[]));

            amax = arr.Max();
            print(amax);
            Assert.AreEqual((Int64)9999994, amax);

            amin = arr.Min();
            print(amin);
            Assert.AreEqual((Int64)(-9999994), amin);

            avg = arr.Average();
            print(avg);
            Assert.AreEqual(-3134.7668014, avg);

            first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new Int64[] { 8539427, -3969151, -3294108, 7156061, 6272194, 9698115, 1323314, -4364865, -124057, -6816292 });
        }


        [TestMethod]
        public void test_randuint64_1()
        {
            var random = new RandomDistributions.random();

            random.seed(1990);

            var arr = random.randuint64(2, 3, 4);
            Assert.AreEqual(arr.GetType(), typeof(System.UInt64[]));
            print(arr);
            AssertArray(arr, new UInt64[] { 2, 2, 2, 2 });

            arr = random.randuint64(64, 64000, 5000000);
            Assert.AreEqual(arr.GetType(), typeof(System.UInt64[]));

            var amax = arr.Max();
            print(amax);
            Assert.AreEqual((UInt64)63999, amax);

            var amin = arr.Min();
            print(amin);
            Assert.AreEqual((UInt64)64, amin);

            var avg = arr.Average();
            print(avg);
            Assert.AreEqual(32014.4591844, avg);

            var first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new UInt64[] { 32948, 9226, 21086, 58281, 51816, 58826, 18566, 13253, 28502, 39394 });
        }


        [TestMethod]
        public void test_rand_bytes_1()
        {
            var random = new RandomDistributions.random();
            random.seed(6432);

            byte br = random.getbyte();

            var bytes = random.bytes(24);
            Assert.AreEqual(bytes.Length, 24);

            br = random.getbyte();
            var arr = random.bytes(24);
            Assert.AreEqual(arr.GetType(), typeof(System.Byte[]));

            print(br);
            Assert.AreEqual(193, br);

            print(arr);

            var ExpectedData = new byte[]
                { 18, 59, 87, 188, 116, 37, 28, 134, 204, 39, 5, 212,147, 165, 1, 136, 198, 108, 203, 151, 39, 187, 144, 209};

            AssertArray(arr, ExpectedData);
        }
        #endregion

        #region shuffle/permutation

        [TestMethod]
        public void test_rand_shuffle_1()
        {
            var random = new RandomDistributions.random();
            random.seed(1964);
            var arr = np.arange(10).AsInt32Array();
            random.shuffle(arr);
            print(arr);
            AssertArray(arr, new Int32[] { 2, 9, 3, 6, 1, 7, 5, 0, 4, 8 });

            arr = np.arange(10).AsInt32Array();
            print(arr);

            random.shuffle(arr);
            print(arr);
            AssertArray(arr, new Int32[] { 0 , 3 ,  7 ,  8 ,  5 ,  9 ,  4 ,  6 ,  1 ,  2 });
        }

        [TestMethod]
        public void test_rand_permutation_1()
        {
            var random = new RandomDistributions.random();
            random.seed(1963);

            var arr = random.permutation(10);
            print(arr);
            AssertArray(arr, new Int32[] { 6, 7, 4, 5, 1, 2, 9, 0, 8, 3 });

            arr = random.permutation(np.arange(5).AsInt32Array());
            print(arr);
            AssertArray(arr, new Int32[] { 2, 3, 1, 0, 4 });

        }

        #endregion

        [TestMethod]
        public void test_rand_beta_1()
        {
            var random = new RandomDistributions.random();
            random.seed(5566);

            var a = np.arange(1, 11, dtype: np.Float64);
            var b = np.arange(1, 11, dtype: np.Float64);

            var arr = random.beta(b.AsDoubleArray(), b.AsDoubleArray(), size:10);
            Assert.AreEqual(arr.GetType(), typeof(System.Double[]));
            print(arr);

            var ExpectedData = new double[]
                { 0.890517356256563, 0.532484155787344, 0.511396509650771, 0.862418837558698, 0.492458004172681,
                  0.504662615187708, 0.621477223753938, 0.41702032419038, 0.492984829781569, 0.47847289036676 };

            AssertArray(arr, ExpectedData);
        }

        [TestMethod]
        public void test_rand_binomial_1()
        {
            var random = new RandomDistributions.random();
            random.seed(123);

            var arr = random.binomial(9, 0.1, size:20);
            AssertArray(arr, new Int64[] { 1, 0, 0, 1, 1, 1, 3, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 1 });
            Assert.AreEqual(arr.GetType(), typeof(System.Int64[]));

            var s = np.sum(np.array(arr) == 0);
            Assert.AreEqual(6, s.GetItem(0));


            arr = random.binomial(9, 0.1, size: 20000);
            s = np.sum(np.array(arr) == 0);
            Assert.AreEqual(7711, s.GetItem(0));
            print(s);
        }

        [TestMethod]
        public void test_rand_negative_binomial_1()
        {
            var random = new RandomDistributions.random();
            random.seed(123);

            var arr = random.negative_binomial(1, 0.1, size: 20);
            AssertArray(arr, new Int64[] { 8, 9, 4, 10, 8, 5, 11, 7, 21, 0, 8, 1, 7, 3, 1, 17, 4, 5, 6, 8, });
            Assert.AreEqual(arr.GetType(), typeof(System.Int64[]));

            var s = np.sum(np.array(arr) == 0);
            Assert.AreEqual(1, s.GetItem(0));


            arr = random.negative_binomial(1, 0.1, size: 20000);
            s = np.sum(np.array(arr) == 0);
            Assert.AreEqual(1992, s.GetItem(0));
            print(s);
        }
    }
}
