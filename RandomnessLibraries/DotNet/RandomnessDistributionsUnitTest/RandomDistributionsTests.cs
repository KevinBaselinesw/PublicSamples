﻿using System;
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

        [TestMethod]
        public void test_rand_chisquare_1()
        {
            var random = new RandomDistributions.random();

            random.seed(904);

            var arr = random.chisquare(2, 40);

            var amax = arr.Max();
            print(amax);
            Assert.AreEqual(5.375544801685989, amax);

            var amin = arr.Min();
            print(amin);
            Assert.AreEqual(0.08589992390559097, amin);

            var avg = arr.Average();
            print(avg);
            Assert.AreEqual(1.7501237764369322, avg);

            var first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new double[]
            { 0.449839203939145, 1.92402228590093, 3.34447894813104, 5.34660224752626, 1.15307500835178,
              3.7142340997291, 0.137140658434128, 1.69505253573874, 1.5675310912308, 3.1550000636764 });


            arr = random.chisquare(np.arange(1, (25 * 25) + 1, dtype: np.Float64).AsDoubleArray(), (25 * 25));

            amax = arr.Max();
            print(amax);
            Assert.AreEqual(686.8423283498724, amax);

            amin = arr.Min();
            print(amin);
            Assert.AreEqual(0.5907891154976891, amin);

            avg = arr.Average();
            print(avg);
            Assert.AreEqual(313.32691732965679, avg);

            first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new double[]
            { 0.590789115497689, 6.66144890165653, 4.3036608098316, 3.81142549081703, 3.43184119361682,
              7.72738961829532, 8.62984666080682, 7.93816304470877, 4.58662123588299, 10.139304988442 });

        }

        [Ignore]
        [TestMethod]
        public void test_rand_dirichlet_1()
        {
            var random = new RandomDistributions.random();

            random.seed(904);

            //var arr = random.dirichlet(new int[] { 2, 20 }, 40);

            //var amax = np.amax(arr);
            //print(amax);
            //Assert.AreEqual(0.9840840815128786, amax.GetItem(0));

            //var amin = np.amin(arr);
            //print(amin);
            //Assert.AreEqual(0.01591591848712132, amin.GetItem(0));

            //var avg = np.average(arr);
            //print(avg);
            //Assert.AreEqual(0.5, avg.GetItem(0));

            //var first10 = arr["0:10:1"] as ndarray;
            //print(first10);

            //var ExpectedData = new double[,]
            //    { { 0.139631231507379, 0.860368768492621 },
            //      { 0.164792458731906, 0.835207541268094 },
            //      { 0.171803553905849, 0.828196446094151 },
            //      { 0.0644690936340902, 0.93553090636591 },
            //      { 0.0159159184871213, 0.984084081512879 },
            //      { 0.0560190209751487, 0.943980979024851 },
            //      { 0.042863749816706, 0.957136250183294 },
            //      { 0.0629065979497427, 0.937093402050257 },
            //      { 0.176381677869148, 0.823618322130852 },
            //      { 0.173933715950931, 0.826066284049069 } };


            //AssertArray(first10, ExpectedData);

            ////////////////

            //arr = random.dirichlet(new int[] { 25, 1, 25 }, 25 * 25);

            //amax = np.amax(arr);
            //print(amax);
            //Assert.AreEqual(0.7045960873403417, amax.GetItem(0));

            //amin = np.amin(arr);
            //print(amin);
            //Assert.AreEqual(2.6249646396377578e-05, amin.GetItem(0));

            //avg = np.average(arr);
            //print(avg);
            //Assert.AreEqual(0.33333333333333354, avg.GetItem(0));

            //first10 = arr["0:10:1"] as ndarray;
            //print(first10);

            //ExpectedData = new double[,]
            //    { { 0.610930043484893, 0.0193144481201467, 0.369755508394961 },
            //      { 0.442089362038658, 0.0346318991277543, 0.523278738833588 },
            //      { 0.614302268039855, 0.00320374569291326, 0.382493986267232 },
            //      { 0.321145218655756, 0.0299626236169947, 0.648892157727249 },
            //      { 0.39914499391642, 0.0791711737227786, 0.521683832360802 },
            //      { 0.527685226612033, 0.00514173575597135, 0.467173037631995 },
            //      { 0.454743888761555, 0.0548186350528715, 0.490437476185573 },
            //      { 0.449442317277421, 0.0101774794978973, 0.540380203224682 },
            //      { 0.464374656943362, 0.0130023049255488, 0.522623038131089 },
            //      { 0.45310574844845, 0.00503512489512577, 0.541859126656425 } };

            //AssertArray(first10, ExpectedData);

        }

        [TestMethod]
        public void test_rand_exponential_1()
        {
            var random = new RandomDistributions.random();

            random.seed(914);

            var arr = random.exponential(2.0, 40);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(9.197509067464914, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(0.0029207069544253516, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(2.2703450760147272, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new double[]
             { 0.747521091531369, 2.07455044402581, 0.266251619077963, 0.853502719631811, 6.17373567708619,
               9.19750906746491, 0.716019724979618, 0.00292070695442535, 1.07083473949392, 2.71744206583613 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.exponential(new double[] { 1.75, 2.25, 3.5, 4.1 }, 4);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(6.4389292872403, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(0.8405196701524901, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(2.3954160266720965, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[] { 0.84051967015249, 1.41367329358207, 0.888541855713523, 6.4389292872403 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.exponential(1.75, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(21.318986653331216, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(3.55593171092486e-06, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(1.7531454665721702, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[]
            { 1.36762201783298, 2.8658070450868, 0.624351703816035, 0.968550532587695, 4.27788701329609,
              0.366187067053844, 1.8701491040941, 0.322938134269804, 1.77904860176968, 0.311565039308104 };

            AssertArray(first10, ExpectedData);

        }

        [TestMethod]
        public void test_rand_exponential_2()
        {
            var random = new RandomDistributions.random();

            random.seed(914);

            var arr = random.exponential();
            Assert.AreEqual(0.37376054576568468, arr[0]);
            print(arr);
        }

        [TestMethod]
        public void test_rand_f_1()
        {
            var random = new RandomDistributions.random();

            random.seed(94);

            var arr = random.f(1, 48, 1000);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(19.14437059096128, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(5.680490866939581e-07, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(1.0515442503540988, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new double[]
             {  1.46465345913042, 0.222306914656698, 1.32950770405259, 0.653851369266928, 0.00813948648495581,
                0.882291524250553, 1.81050180312571, 0.000604113781846987, 0.153342107000664, 0.977254029029637 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.f(new double[] { 1.75, 2.25, 3.5, 4.1 }, new double[] { 48 }, 4);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(1.2732463363388247, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(0.546310315192394, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(0.9827519798898189, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[] { 1.05458800808553, 1.05686325994253, 1.27324633633882, 0.546310315192394 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.f(1.75, 53, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(15.625089837222493, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(8.535252347895407e-07, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(1.0363937363959668, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[]
            {  0.00532782005062579, 0.864448569799127, 4.62790636498241, 0.0383338245858999, 0.0484018547471839,
               0.351354338193848, 0.178901619826393, 1.16925307162684, 2.99295008429782, 0.0505560091503879 };

            AssertArray(first10, ExpectedData);

        }

        [TestMethod]
        public void test_rand_gamma_1()
        {
            var random = new RandomDistributions.random();

            random.seed(99);

            var arr = random.gamma(new double[] { 4, 4 }, new double[] { 2 });

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(18.370134973057713, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(6.801539561549457, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(12.585837267303585, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new double[] { 6.80153956154946, 18.3701349730577 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.gamma(new double[] { 1.75, 2.25, 3.5, 4.1 }, new double[] { 48 }, 4);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(269.13817401122844, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(85.50090380856885, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(198.09580609835598, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[] { 85.5009038085689, 211.684158840965, 226.059987732662, 269.138174011228 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.gamma(1.75, 53, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(845.754380588075, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(0.04372693615388623, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(92.505472695434207, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[]
            {  65.0307272217409, 127.150322850433, 93.4719711853725, 16.0658132351959, 47.6795500662279,
              118.872229264334, 33.0026431406179, 63.8640974905758, 34.4577922345043, 72.2726570834378 };

            AssertArray(first10, ExpectedData);
        }

        [TestMethod]
        public void test_rand_geometric_1()
        {
            var random = new RandomDistributions.random();

            random.seed(101);

            var arr = random.geometric(0.35);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)2, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)2, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(2.0, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new long[] { 2 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.geometric(new double[] { .75, .25, .5, .1 }, 400);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)47, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)1, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(4.42, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            var ExpectedData2 = new long[,]
                { { 1, 1, 1, 11 }, { 2, 2, 4, 13 }, { 1, 3, 1, 2 }, { 2, 12, 1, 1 }, { 1, 5, 1, 11 },
                  { 1, 1, 1, 2 },  { 4, 3, 2, 13 }, { 1, 9, 3, 5 }, { 1, 11, 1, 20 }, { 2, 3, 4, 1 } };

            //AssertArray(first10, ExpectedData2);

            //////////////

            arr = random.geometric(.75, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)10, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)1, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(1.332595, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new long[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 };
            AssertArray(first10, ExpectedData);
        }

        [TestMethod]
        public void test_rand_gumbel_1()
        {
            var random = new RandomDistributions.random();

            random.seed(1431);

            var arr = random.gumbel(0.32);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(1.76573325397214, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(1.76573325397214, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(1.76573325397214, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new double[] { 1.76573325397214 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.gumbel(new double[] { .75, .25, .5, .1 }, 4);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(7.625593114379172, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(-2.48113529309385, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(3.319780095180179, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            var ExpectedData2 = new double[] { 4.79769794330828, -2.48113529309385, 3.33696461612712, 7.62559311437917 };

            AssertArray(first10, ExpectedData2);

            //////////////

            arr = random.gumbel(.75, 0.5, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(7.205833825082223, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(-0.5506620623963436, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(1.0376445397766865, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[]
            { 1.21622910498044, 3.00226498101696, 0.475407027157304, 1.65190860500953, 1.57140421867578,
              0.93864144637321, -0.200732799247397, 0.242196470741022, 0.0803181023284516, 1.52710248786222 };
            AssertArray(first10, ExpectedData);
        }

        [TestMethod]
        public void test_rand_hypergeometric_1()
        {
            var random = new RandomDistributions.random();

            random.seed(1631);

            var arr = random.hypergeometric(100, 2, 10, size: 1000);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)10, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)8, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(9.827, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new long[] { 10, 10, 10, 8, 9, 10, 10, 10, 10, 9 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.hypergeometric(new long[] { 75, 25, 5, 1 }, new long[] { 5 }, new long[] { 80, 30, 10, 6 });

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)75, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)1, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(26.5, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            var ExpectedData2 = new long[] { 75, 25, 5, 1 };

            AssertArray(first10, ExpectedData2);

            //////////////

            arr = random.hypergeometric(15, 15, 15, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)13, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)2, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(7.500615, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new long[] { 7, 10, 7, 7, 6, 9, 9, 8, 7, 8 };
            AssertArray(first10, ExpectedData);
        }

        [TestMethod]
        public void test_rand_laplace_1()
        {
            var random = new RandomDistributions.random();

            random.seed(1400);

            var arr = random.laplace(0.32);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(-1.0764218589534678, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(-1.0764218589534678, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(-1.0764218589534678, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new double[] { -1.0764218589534678 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.laplace(new double[] { .75, .25, .5, .1 }, 4);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(0.8742419694128964, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(-4.635588148164478, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(-1.6171201483154878, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            var ExpectedData2 = new double[] { -4.63558814816448, -2.54523090186133, 0.874241969412896, -0.16190351264904 };

            AssertArray(first10, ExpectedData2);

            //////////////

            arr = random.laplace(.75, 0.5, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(6.432291394535725, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(-5.141321026441119, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(0.75284129642070252, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[]
            { 0.610570284884035, 0.905721127018003, 1.89887869162796, 0.718937532523341, 0.551393381091485,
              0.57540806081121, 0.213861746932043, 0.714426358585987, 0.518993828675387, 0.54266874996464 };
            AssertArray(first10, ExpectedData);
        }

        [TestMethod]
        public void test_rand_logistic_1()
        {
            var random = new RandomDistributions.random();

            random.seed(1400);

            var arr = random.logistic(0.32);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(-1.6374760957412662, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(-1.6374760957412662, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(-1.6374760957412662, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new double[] { -1.6374760957412662 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.logistic(new double[] { .75, .25, .5, .1 }, 4);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(1.2164458169982604, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(-6.850724032638232, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(-2.5541489883857613, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            var ExpectedData2 = new double[] { -6.85072403263823, -4.17461033521845, 1.21644581699826, -0.407707402684622 };

            AssertArray(first10, ExpectedData2);

            //////////////

            arr = random.logistic(.75, 0.5, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(6.77886208503632, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(-5.487892707727758, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(0.75353194203189111, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[]
            { 0.501664104949021, 1.02428778459822, 2.21967826633745, 0.689692463292968, 0.409628150582071,
              0.446254436188489, -0.0388753549104508, 0.68121644303921, 0.361593774055577, 0.39654413010928 };
            AssertArray(first10, ExpectedData);
        }

        [TestMethod]
        public void test_rand_lognormal_1()
        {
            var random = new RandomDistributions.random();

            random.seed(990);

            var arr = random.lognormal(new double[] { 4, 4 }, 2);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(39.029797845778916, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(9.323290074349073, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(24.176543960063995, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new double[] { 39.0297978457789, 9.32329007434907 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.lognormal(new double[] { 1.75, 2.25, 3.5, 4.1 }, 48, 4);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(1.4424793885444833E+23, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(3.95031874515845e-18, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(3.6061984713612083E+22, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[] { 2413869.90680355, 3.95031874515845E-18, 75047.4245052377, 1.4424793885444833E+23 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.lognormal(1.75, 53, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(1.7279293118875759e+100, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(1.937968152068317e-111, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(8.7801786884914109E+94, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[]
            {  2.34074428722725E-05, 2.1893911721303E-26, 3.1873844722876965E-05, 373137879.47156078, 68.6045720132661,
               2.2378072683770408E+21, 89011700746.666718, 1.20895457869166E-48, 5.42793001022139E-34, 7.41734199506998E-44 };

            AssertArray(first10, ExpectedData);
        }

        [TestMethod]
        public void test_rand_logseries_1()
        {
            var random = new RandomDistributions.random();

            random.seed(9909);

            var arr = random.logseries(new double[] { 0.1, 0.99 }, 40);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)184, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)1, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(13.925, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new long[] { 1, 26, 1, 3, 1, 26, 1, 23, 1, 3 };
            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.logseries(new double[] { .75, .25, .5, .1 }, 1600);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)14, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)1, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(1.466875, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new long[]
                { 5, 1, 4, 1, 1, 1, 1, 1, 5, 1 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.logseries(.334455, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)10, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)1, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(1.2335, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            var ExpectedData2 = new long[] { 1, 2, 1, 1, 1, 1, 2, 1, 1, 1 };

            AssertArray(first10, ExpectedData2);
        }

        [TestMethod]
        public void test_rand_multinomial_1()
        {
            var random = new RandomDistributions.random();

            random.seed(9909);

            double dv = 1.0 / 6.0;
            var arr = random.multinomial(20, new double[] { dv, dv, dv, dv, dv, dv }, size: 1000);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)10, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)0, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(3.3333333333333335, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new long[]
                { 3, 1, 4, 6, 5, 1, 4, 7, 1, 3 };

            AssertArray(first10, ExpectedData);

            //////////////
            dv = 1.0 / 7.0;
            arr = random.multinomial(100, new double[] { dv, dv, dv, dv, dv, 2 / 7 });

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)33, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)12, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(16.666666666666668, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            var ExpectedData2 = new long[] { 17, 12, 12, 12, 14, 33 };


            AssertArray(first10, ExpectedData2);

            //////////////
            dv = 1.0 / 6.0;
            arr = random.multinomial(20, new double[] { dv, dv, dv, dv, dv, dv }, 20000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual((long)12, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual((long)0, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(3.3333333333333335, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new long[]
                { 4, 3, 4, 0, 1, 8, 5, 1, 1, 4 };

            AssertArray(first10, ExpectedData);
        }

        [Ignore]
        [TestMethod]
        public void test_rand_multivariate_normal_1()
        {

        }

        [TestMethod]
        public void test_rand_noncentral_chisquare_1()
        {
            var random = new RandomDistributions.random();
            random.seed(904);

            var arr = random.noncentral_chisquare(3, 20, 100000);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(78.04001829193612, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(0.6572503967444088, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(22.999416481542255, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new double[]
            { 32.507966101474, 29.5670038054405, 35.4646231172001, 48.8509128727834, 13.8878542193038,
              22.5887773956417, 18.8177210998047, 6.62646485637076, 14.7716354200521, 17.592124636122  });


            arr = random.noncentral_chisquare(np.arange(1, (25 * 25) + 1, dtype: np.Float64).AsDoubleArray(), 25 * 25);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(1393.919486168914, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(526.5979763470843, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(938.60218769525386, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);
            AssertArray(first10, new double[]
            { 693.625713231652, 667.317783374469, 628.424318152006, 585.208004459504, 758.222190671585,
              611.122162793199, 659.549979398019, 611.408655748793, 689.18666064001, 657.624462137622 });

        }

        [TestMethod]
        public void test_rand_noncentral_f_1()
        {
            var random = new RandomDistributions.random();

            random.seed(95);

            var arr = random.noncentral_f(1, 20, 48, 1000);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(166.41578272134745, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(10.185919423402394, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(54.230679084788719, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new double[]
             {  37.2983096021906, 62.5743394917022, 68.0628233514137, 40.5905707097552, 44.4521020211498,
                44.5743262503621, 31.4443325580866, 79.5522652120909, 44.9345096357582, 43.0357724523705  };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.noncentral_f(new double[] { 1.75, 2.25, 3.5, 4.1 }, 20, 48, 4);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(32.707452977686614, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(7.906607829343024, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(20.832891192705823, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[] { 32.7074529776866, 21.9051515376809, 20.8123524261127, 7.90660782934302 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.noncentral_f(1.75, 3, 53, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(195494.05842774129, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(1.7899396924741655, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(90.217557446706948, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[]
            {  27.8838758530967, 15.3064263302261, 19.9558173630626, 79.9200280113332, 20.6836524868276, 30.6831175354627,
               5.30452049710313, 26.3487042626856, 68.0419650815286, 33.3860982701053 };

            AssertArray(first10, ExpectedData);

        }

        [TestMethod]
        public void test_rand_normal_1()
        {
            var random = new RandomDistributions.random();

            random.seed(96);

            var arr = random.normal(1, 48, 1000);

            var amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(174.27339866101488, amax.GetItem(0));

            var amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(-184.13383136259313, amin.GetItem(0));

            var avg = np.average(arr);
            print(avg);
            Assert.AreEqual(1.3384280414551479, avg.GetItem(0));

            var first10 = arr.FirstTen();
            print(first10);

            var ExpectedData = new double[]
             {  4.35269318876786, -1.3359262138713, -30.667818087132, 18.1537231001315, 7.33791680475021, -40.6840900065982,
              -23.9673401331787, 4.86724597788797, -56.7059911346956, -46.8495243913698 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.normal(new double[] { 1.75, 2.25, 3.5, 4.1 }, 48, 4);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(8.4980511462390664, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(-11.788045757012442, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(-0.6369144011832439, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[] { 8.49805114623907, 4.5687120247507, -3.8263750187103, -11.7880457570124 };

            AssertArray(first10, ExpectedData);

            //////////////

            arr = random.normal(1.75, 53, 200000);

            amax = np.amax(arr);
            print(amax);
            Assert.AreEqual(244.03504143481928, amax.GetItem(0));

            amin = np.amin(arr);
            print(amin);
            Assert.AreEqual(-219.87697184024597, amin.GetItem(0));

            avg = np.average(arr);
            print(avg);
            Assert.AreEqual(1.6568977790583879, avg.GetItem(0));

            first10 = arr.FirstTen();
            print(first10);

            ExpectedData = new double[]
            { -121.150720139349, 21.11738895264, -51.4329434180368, -20.2277628440144, 11.3070623804567, 68.6964951609887,
                14.9030666848521, 14.5434279312901, -75.4223378124162, -77.6809906511862 };

            AssertArray(first10, ExpectedData);

        }

    }
}
