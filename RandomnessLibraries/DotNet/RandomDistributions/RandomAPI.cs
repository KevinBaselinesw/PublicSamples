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

using System;
using System.Collections.Generic;


namespace RandomDistributions
{

    public class random_serializable
    {
        public string randomGeneratorSerializationData;

        public int pos;
        public bool has_gauss; /* !=0: gauss contains a gaussian deviate */
        public double gauss;

        ///* The rk_state structure has been extended to store the following
        // * information for the binomial generator. If the input values of n or p
        // * are different than nsave and psave, then the other parameters will be
        // * recomputed. RTK 2005-09-02 */

        public bool has_binomial; /* !=0: following parameters initialized for binomial */
        public double psave;
        public long nsave;
        public double r;
        public double q;
        public double fm;
        public long m;
        public double p1;
        public double xm;
        public double xl;
        public double xr;
        public double c;
        public double laml;
        public double lamr;
        public double p2;
        public double p3;
        public double p4;
    }

    public class random
    {
        IRandomGenerator _rndGenerator;
        private rk_state internal_state = null;

        private object rk_lock = new object();
        

        #region seed


        public bool seed(Int32? seed)
        {
            if (seed.HasValue)
                return this.seed(Convert.ToUInt64(seed.Value));
            else
                return this.seed((ulong?)null);

        }

        public bool seed(UInt64? seed)
        {
            internal_state.rndGenerator.Seed(seed, internal_state);
            return true;
        }

        public random(IRandomGenerator rndGenerator = null)
        {
            if (rndGenerator == null)
            {
                rndGenerator = new RandomState();
            }
            internal_state = new rk_state(rndGenerator);
            seed(null);
            _rndGenerator = rndGenerator;
        }

        public random_serializable ToSerialization()
        {
            random_serializable serializationData = new random_serializable();
            serializationData.randomGeneratorSerializationData = _rndGenerator.ToSerialization();

            serializationData.pos = internal_state.pos;
            serializationData.has_gauss = internal_state.has_gauss;
            serializationData.gauss = internal_state.gauss;

            serializationData.has_binomial = internal_state.has_binomial;
            serializationData.psave = internal_state.psave;
            serializationData.nsave = internal_state.nsave;
            serializationData.r = internal_state.r;
            serializationData.q = internal_state.q;
            serializationData.fm = internal_state.fm;
            serializationData.m = internal_state.m;
            serializationData.p1 = internal_state.p1;
            serializationData.xm = internal_state.xm;
            serializationData.xl = internal_state.xl;
            serializationData.xr = internal_state.xr;
            serializationData.c = internal_state.c;
            serializationData.laml = internal_state.laml;
            serializationData.lamr = internal_state.lamr;
            serializationData.p2 = internal_state.p2;
            serializationData.p3 = internal_state.p3;
            serializationData.p4 = internal_state.p4;

            return serializationData;
        }

        public void FromSerialization(random_serializable serializationData)
        {
            internal_state.pos = serializationData.pos;
            internal_state.has_gauss = serializationData.has_gauss;
            internal_state.gauss = serializationData.gauss;

            internal_state.has_binomial = serializationData.has_binomial;
            internal_state.psave = serializationData.psave;
            internal_state.nsave = serializationData.nsave;
            internal_state.r = serializationData.r;
            internal_state.q = serializationData.q;
            internal_state.fm = serializationData.fm;
            internal_state.m = serializationData.m;
            internal_state.p1 = serializationData.p1;
            internal_state.xm = serializationData.xm;
            internal_state.xl = serializationData.xl;
            internal_state.xr = serializationData.xr;
            internal_state.c = serializationData.c;
            internal_state.laml = serializationData.laml;
            internal_state.lamr = serializationData.lamr;
            internal_state.p2 = serializationData.p2;
            internal_state.p3 = serializationData.p3;
            internal_state.p4 = serializationData.p4;

            _rndGenerator.FromSerialization(serializationData.randomGeneratorSerializationData);
        }


        #endregion

        private void ValidateSize(long size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException("size", "size must not be less than 0");
        }

        #region Simple random data

        #region rand
        public double rand()
        {
            return random_sample();
        }

        public double[] rand(long size)
        {
            ValidateSize(size);
            return random_sample(size);
        }


        #endregion

        #region randn
        public double randn()
        {
            return standard_normal();
        }

        public double[] randn(long size)
        {
            ValidateSize(size);

            return standard_normal(size);
        }

        #endregion

        #region randbool

        public bool[] randbool(long low, UInt64? high, long size)
        {
            ValidateSize(size);

            bool[] randomData = new bool[size];

            RandomDistributions.rk_random_bool(false, true, randomData.Length, randomData, internal_state);

            return randomData;
        }

        #endregion

        #region randint8

        public sbyte[] randint8(long low, UInt64? high, long size)
        {
            ValidateSize(size);

            if (low < System.SByte.MinValue)
                throw new ArgumentOutOfRangeException("low", string.Format("low is out of bounds for Int8"));
            if (high != null && high.Value > (UInt64)System.SByte.MaxValue)
                throw new ArgumentOutOfRangeException("high", string.Format("high is out of bounds for Int8"));

            SByte[] randomData = new SByte[size];

            SByte _low = Convert.ToSByte(low);
            SByte? _high = null;

            if (!high.HasValue)
            {
                _high = (SByte)Math.Max(0, _low - 1);
                _low = 0;
            }
            else
            {
                _high = Convert.ToSByte(high.Value);
            }
            var rng = _high.Value - _low;
            var off = _low;

            RandomDistributions.rk_random_int8(off, (SByte)(rng - 1), randomData.Length, randomData, internal_state);

            return randomData;
        }

        #endregion

        #region randuint8

        public byte[] randuint8(long low, UInt64? high, long size)
        {
            ValidateSize(size);

            if (low < System.Byte.MinValue)
                throw new ArgumentOutOfRangeException("low", string.Format("low is out of bounds for UInt8"));
            if (high != null && high.Value > (UInt64)System.Byte.MaxValue)
                throw new ArgumentOutOfRangeException("high", string.Format("high is out of bounds for UInt8"));

            Byte[] randomData = new Byte[size];

            Byte _low = Convert.ToByte(low);
            Byte? _high = null;

            if (!high.HasValue)
            {
                _high = (Byte)Math.Max(0, _low - 1);
                _low = 0;
            }
            else
            {
                _high = Convert.ToByte(high.Value);
            }
            var rng = _high.Value - _low;
            var off = _low;

            RandomDistributions.rk_random_uint8(off, (Byte)(rng - 1), randomData.Length, randomData, internal_state);
            return randomData;
        }

        #endregion

        #region randint16

        public Int16[] randint16(long low, UInt64? high, long size)
        {
            ValidateSize(size);

            if (low < System.Int16.MinValue)
                throw new ArgumentOutOfRangeException("low", string.Format("low is out of bounds for Int16"));
            if (high != null && high.Value > (UInt64)System.Int16.MaxValue)
                throw new ArgumentOutOfRangeException("high", string.Format("high is out of bounds for Int16"));

            Int16[] randomData = new Int16[size];

            Int16 _low = Convert.ToInt16(low);
            Int16? _high = null;

            if (!high.HasValue)
            {
                _high = (Int16)Math.Max(0, _low - 1);
                _low = 0;
            }
            else
            {
                _high = Convert.ToInt16(high.Value);
            }
            var rng = _high.Value - _low;
            var off = _low;

            RandomDistributions.rk_random_int16(off, (Int16)(rng - 1), randomData.Length, randomData, internal_state);

            return randomData;
        }

        #endregion

        #region randuint16

        public UInt16[] randuint16(long low, UInt64? high, long size)
        {
            ValidateSize(size);

            if (low < System.UInt16.MinValue)
                throw new ArgumentOutOfRangeException("low", string.Format("low is out of bounds for UInt16"));
            if (high != null && high.Value > System.UInt16.MaxValue)
                throw new ArgumentOutOfRangeException("high", string.Format("high is out of bounds for UInt16"));

            UInt16[] randomData = new UInt16[size];

            UInt16 _low = Convert.ToUInt16(low);
            UInt16? _high = null;

            if (!high.HasValue)
            {
                _high = (UInt16)Math.Max(0, _low - 1);
                _low = 0;
            }
            else
            {
                _high = Convert.ToUInt16(high.Value);
            }
            var rng = _high.Value - _low;
            var off = _low;

            RandomDistributions.rk_random_uint16(off, (UInt16)(rng - 1), randomData.Length, randomData, internal_state);

            return randomData;
        }

        #endregion

        #region randint
 

        public Int32[] randint32(long low, UInt64? high, long size)
        {
            ValidateSize(size);

            if (low < System.Int32.MinValue)
                throw new ArgumentOutOfRangeException("low", string.Format("low is out of bounds for Int32"));
            if (high > System.Int32.MaxValue)
                throw new ArgumentOutOfRangeException("high", string.Format("high is out of bounds for Int32"));

            Int32[] randomData = new Int32[size];

            Int32 _low = Convert.ToInt32(low);
            Int32? _high = null;

            if (!high.HasValue)
            {
                _high = Math.Max(0, _low - 1);
                _low = 0;
            }
            else
            {
                _high = Convert.ToInt32(high.Value);
            }
            var rng = _high.Value - _low;
            var off = _low;
            RandomDistributions.rk_random_int32(off, rng - 1, randomData.Length, randomData, internal_state);

            return randomData;
        }

        #endregion

        #region randuint

        public UInt32[] randuint32(long low, UInt64? high, long size)
        {
            ValidateSize(size);

            if (low < System.UInt32.MinValue)
                throw new ArgumentOutOfRangeException("low", string.Format("low is out of bounds for UInt32"));
            if (high > System.UInt32.MaxValue)
                throw new ArgumentOutOfRangeException("high", string.Format("high is out of bounds for UInt32"));

            UInt32[] randomData = new UInt32[size];

            UInt32 _low = Convert.ToUInt32(low);
            UInt32? _high = null;

            if (!high.HasValue)
            {
                _high = Math.Max(0, _low - 1);
                _low = 0;
            }
            else
            {
                _high = Convert.ToUInt32(high.Value);
            }
            var rng = _high.Value - _low;
            var off = _low;

            RandomDistributions.rk_random_uint32(off, rng - 1, randomData.Length, randomData, internal_state);

            return randomData;
        }

        #endregion

        #region randint64

        public long[] randint64(long low, UInt64? high, long size)
        {
            ValidateSize(size);

            if (low < System.Int64.MinValue)
                throw new ArgumentOutOfRangeException("low", string.Format("low is out of bounds for long"));
            if (high > System.Int64.MaxValue)
                throw new ArgumentOutOfRangeException("high", string.Format("high is out of bounds for long"));

            long[] randomData = new long[size];

            long _low = Convert.ToInt64(low);
            long? _high = null;

            if (!high.HasValue)
            {
                _high = Math.Max(0, _low - 1);
                _low = 0;
            }
            else
            {
                _high = Convert.ToInt64(high.Value);
            }
            var rng = _high.Value - _low;
            var off = _low;

            RandomDistributions.rk_random_int64(off, rng - 1, randomData.Length, randomData, internal_state);

            return randomData;
        }


        #endregion

        #region randuint64

        public UInt64[] randuint64(long low, UInt64? high, long size)
        {
            ValidateSize(size);

            if (low < 0)
                throw new ArgumentOutOfRangeException("low", string.Format("low is out of bounds for UInt64"));
            if (high > System.UInt64.MaxValue)
                throw new ArgumentOutOfRangeException("high", string.Format("high is out of bounds for UInt64"));

            UInt64[] randomData = new UInt64[size];

            UInt64 _low = Convert.ToUInt64(low);
            UInt64? _high = null;

            if (!high.HasValue)
            {
                _high = Math.Max(0, _low - 1);
                _low = 0;
            }
            else
            {
                _high = Convert.ToUInt64(high.Value);
            }
            var rng = _high.Value - _low;
            var off = _low;

            RandomDistributions.rk_random_uint64(off, rng - 1, randomData.Length, randomData, internal_state);

            return randomData;
        }


        #endregion

        #region random_integers
        public Int32[] random_integers(long low, UInt64? high = null, long size = 0)
        {
            return randint32(low, high + 1, size);
        }

        #endregion

        #region random_sample

        public double random_sample()
        {
            double [] rndArray = cont0_array(internal_state, RandomDistributions.rk_double, 0);
            return rndArray[0];
        }

        public double [] random_sample(long size)
        {
            double [] rndArray = cont0_array(internal_state, RandomDistributions.rk_double, size);
            return rndArray;
        }

        #endregion

#region choice
#if false
        public ndarray choice(long a, shape size = null, bool replace = true, double[] p = null)
        {
            return choice((object)a, size, replace, p);
        }

        public ndarray choice(object a, shape size = null, bool replace = true, double[] p = null)
        {
            long pop_size = 0;

            // Format and Verify input
            ndarray aa = np.array(a, copy: false);
            if (aa.ndim == 0)
            {
                if (aa.TypeNum != NPY_TYPES.NPY_INT64)
                {
                    throw new ValueError("a must be a 1-dimensional or an integer");
                }

                pop_size = (long)aa;
                if (pop_size <= 0)
                {
                    throw new ValueError("a must be greater than 0");
                }
            }
            else if (aa.ndim != 1)
            {
                throw new ValueError("a must be 1 dimensional");
            }
            else
            {
                pop_size = aa.shape[0];
                if (pop_size == 0)
                {
                    throw new ValueError("a must be non-empty");
                }
            }

            ndarray _p = null;
            if (p != null)
            {

                //double atol = np.sqrt(np.finfo(np.float64).eps)
                double atol = 0.0001;

                _p = np.array(p);

                if (_p.ndim != 1)
                    throw new ValueError("p must be 1 dimensional");
                if (_p.size != pop_size)
                    throw new ValueError("a and p must have same size");
                if (np.anyb(_p < 0))
                    throw new ValueError("probabilities are not non-negative");
                if (Math.Abs(kahan_sum(p) - 1.0) > atol)
                    throw new ValueError("probabilities do not sum to 1");

            }

            long _size = 0;
            shape shape = size;
            if (size != null)
            {
                _size = (long)np.prod(np.asanyarray(size.iDims));
            }
            else
            {
                _size = 1;
            }

            // Actual sampling
            ndarray idx = null;
            if (replace)
            {
                if (_p != null)
                {
                    var cdf = _p.cumsum();
                    cdf /= cdf[-1];
                    var uniform_samples = random_sample(shape);
                    idx = cdf.searchsorted(uniform_samples, side: NPY_SEARCHSIDE.NPY_SEARCHRIGHT);
                    idx = np.array(idx, copy: false);
                }
                else
                {
                    idx = randint(0, (ulong)pop_size, newshape: shape);
                }
            }
            else
            {
                if (_size > pop_size)
                {
                    throw new ValueError("Cannot take a larger sample than population when 'replace=False'");
                }

                if (_p != null)
                {
                    if (Convert.ToInt64(np.count_nonzero(_p > 0).GetItem(0)) < _size)
                    {
                        throw new ValueError("Fewer non-zero entries in p than size");
                    }

                    npy_intp n_uniq = 0;
                    _p = _p.Copy();
                    var found = np.zeros(shape, dtype: np.Int32);
                    var flat_found = found.ravel();

                    while (n_uniq < _size)
                    {
                        var x = rand(new shape(_size - n_uniq));
                        if (n_uniq > 0)
                        {
                            _p[flat_found["0:" + n_uniq.ToString()]] = 0;
                        }
                        var cdf = np.cumsum(_p);
                        cdf /= cdf[-1];

                        var _new = cdf.searchsorted(x, side: NPY_SEARCHSIDE.NPY_SEARCHRIGHT);
                        var unique_retval = np.unique(_new, return_index: true);
                        unique_retval.indices.Sort();
                        _new = np.take(_new, unique_retval.indices);
                        flat_found[n_uniq.ToString() + ":" + (n_uniq + _new.size).ToString()] = _new;
                        n_uniq += _new.size;
                    }
                    idx = found;
                }
                else
                {
                    ndarray t1 = permutation(pop_size);
                    idx = t1[":" + size.ToString()] as ndarray;
                    if (shape != null)
                    {
                        NpyCoreApi.SetArrayDimsOrStrides(idx, shape.iDims, shape.iDims.Length, true);
                    }
                }
            }


            // Use samples as indices for a if a is array-like
            if (aa.ndim == 0)
            {
                // In most cases a scalar will have been made an array
                if (shape == null)
                {
                    Int32 _idx = (Int32)idx;
                    return np.array(_idx);
                }
                else
                {
                    return idx;
                }
            }

            if (shape != null && idx.ndim == 0)
            {
                throw new Exception("don't currently handle this specific value case");

                //# If size == () then the user requested a 0-d array as opposed to
                //# a scalar object when size is None. However a[idx] is always a
                //# scalar and not an array. So this makes sure the result is an
                //# array, taking into account that np.array(item) may not work
                //# for object arrays.
                //res = np.empty((), dtype = a.dtype)
                //res[()] = a[idx]
                //return res
            }

            return np.array(aa[idx]);

        }
#endif
        private double kahan_sum(double[] darr)
        {
            double c, y, t, sum;
            long i;

            sum = darr[0];
            c = 0.0;

            int n = darr.Length;
            for (i = 1; i < n; i++)
            {
                y = darr[i] - c;
                t = sum + y;
                c = (t - sum) - y;
                sum = t;
            }

            return sum;
        }

#endregion

#region bytes
        public byte getbyte()
        {
            var b = bytes(1);
            return b[0];
        }


        public byte[] bytes(Int32 size)
        {
            byte[] b = new byte[size];

            RandomDistributions.rk_fill(b, size, internal_state);
            return b;
        }


        #endregion
        #endregion

        #region shuffle/permutation

        public void shuffle<T>(T x)
        {
            shuffle(new T[] { x });
        }

        public void shuffle<T>(T[] x)
        {
            int n = x.Length;

            T buf = default(T);

            for (ulong i = (ulong)n - 1; i >= 1; i--)
            {
                ulong j = RandomDistributions.rk_interval(i, internal_state);
                buf = x[j];
                x[j] = x[i];
                x[i] = buf;
            }
            return;
        }

        public T[] permutation<T>(T arr)
        {
            return permutation(new T[] { arr });
        }

        public T[] permutation<T>(T[] arr)
        {
            if (arr.Length == 1 && IsInteger(arr))
            {
                arr = arange<T>(Convert.ToInt32(arr[0]));
            }

            shuffle(arr);
            return arr;
        }

        private bool IsInteger<T>(T[] arr)
        {
            if (arr[0] is System.Byte)
                return true;
            if (arr[0] is System.SByte)
                return true;
            if (arr[0] is System.Int16)
                return true;
            if (arr[0] is System.UInt16)
                return true;
            if (arr[0] is System.Int32)
                return true;
            if (arr[0] is System.UInt32)
                return true;
            if (arr[0] is System.Int64)
                return true;
            if (arr[0] is System.UInt64)
                return true;
            return false;
        }

        private T[] arange<T>(int length)
        {
            T[] arr = new T[length];

            if (arr[0] is System.Byte)
            {
                System.Byte[] _arr = arr as System.Byte[];
                for (System.Byte i = 0; i < length; i++)
                {
                    _arr[i] = i;
                }
            }
            if (arr[0] is System.SByte)
            {
                System.SByte[] _arr = arr as System.SByte[];
                for (System.SByte i = 0; i < length; i++)
                {
                    _arr[i] = i;
                }
            }
            if (arr[0] is System.Int16)
            {
                System.Int16[] _arr = arr as System.Int16[];
                for (System.Int16 i = 0; i < length; i++)
                {
                    _arr[i] = i;
                }
            }
            if (arr[0] is System.UInt16)
            {
                System.UInt16[] _arr = arr as System.UInt16[];
                for (System.UInt16 i = 0; i < length; i++)
                {
                    _arr[i] = i;
                }
            }
            if (arr[0] is System.Int32)
            {
                System.Int32[] _arr = arr as System.Int32[];
                for (System.Int32 i = 0; i < length; i++)
                {
                    _arr[i] = i;
                }
            }
            if (arr[0] is System.UInt32)
            {
                System.UInt32[] _arr = arr as System.UInt32[];
                for (System.UInt32 i = 0; i < length; i++)
                {
                    _arr[i] = i;
                }
            }
            if (arr[0] is System.Int64)
            {
                System.Int64[] _arr = arr as System.Int64[];
                for (System.Int64 i = 0; i < length; i++)
                {
                    _arr[i] = i;
                }
            }
            if (arr[0] is System.Int64)
            {
                System.UInt64[] _arr = arr as System.UInt64[];
                for (System.UInt64 i = 0; i < (UInt64)length; i++)
                {
                    _arr[i] = i;
                }
            }

            return arr;
        }

        #endregion

        #region beta

        public double[] beta(double[] a, double[] b, long size = 0)
        {
 
            if (any_less_equal(a,0))
            {
                throw new ArgumentOutOfRangeException("a", "a <= 0");
            }
            if (any_less_equal(b,0))
            {
                throw new ArgumentOutOfRangeException("b", "b <= 0");
            }

            return cont2_array(internal_state, RandomDistributions.rk_beta, size, a, b);

        }



        #endregion

        #region binomial

        public long[] binomial(long n, double p, long size = 0)
        {
            return binomial(new long[] { n }, new double[] { p }, size);
        }

        public long[] binomial(long[] on, double[] op, long size = 0)
        {
            long ln;
            double fp;

            ValidateSize(size);

            if (any_less(on, 0))
                throw new ArgumentOutOfRangeException("on", "on < 0");
            if (any_less(op, 0))
                throw new ArgumentOutOfRangeException("op", "op < 0");
            if (any_greater(op, 1))
                throw new ArgumentOutOfRangeException("op", "op > 1");
            if (any_nan(op))
                throw new ArgumentOutOfRangeException("op", "op is NAN");

            if (on.Length == 1 && op.Length == 1)
            {
                ln = on[0];
                fp = op[0];
     
                return discnp_array_sc(internal_state, RandomDistributions.rk_binomial, size, ln, fp);
            }

            return discnp_array(internal_state, RandomDistributions.rk_binomial, size, on, op);
        }

        public long[] negative_binomial(long n, double p, long size = 0)
        {
            return negative_binomial(new long[] { n }, new double[] { p }, size);
        }
        public long[] negative_binomial(long[] on, double[] op, long size = 0)
        {
            ValidateSize(size);

            if (any_less(on, 0))
                throw new ArgumentOutOfRangeException("on", "on < 0");
            if (any_less(op, 0))
                throw new ArgumentOutOfRangeException("op", "op < 0");
            if (any_greater(op, 1))
                throw new ArgumentOutOfRangeException("op", "op > 1");
            if (any_nan(op))
                throw new ArgumentOutOfRangeException("op", "op is NAN");

            if (on.Length == 1 && op.Length == 1)
            {
                return discdd_array_sc(internal_state, RandomDistributions.rk_negative_binomial, size, on[0], op[0]);
            }
   
            return discdd_array(internal_state, RandomDistributions.rk_negative_binomial, size, on, op);
        }


        #endregion

        #region chisquare

        public double[] chisquare(double df, long size = 0)
        {
            return chisquare(new double[] { df }, size);
        }

        public double[] chisquare(double []odf, long size = 0)
        {
            ValidateSize(size);


            if (any_less_equal(odf, 0.0))
            {
                throw new ArgumentOutOfRangeException("odf", "odf <= 0.0");
            }

            if (odf.Length == 1)
            {
                return cont1_array_sc(internal_state, RandomDistributions.rk_chisquare, size, odf[0]);
            }

            return cont1_array(internal_state, RandomDistributions.rk_chisquare, size, odf);
        }

        public double[] noncentral_chisquare(double []odf, double ononc, long size = 0)
        {
            return noncentral_chisquare( odf, new double[] { ononc }, size);
        }

        public double[] noncentral_chisquare(double odf, double ononc, long size = 0)
        {
            return noncentral_chisquare(new double[] { odf }, new double[] { ononc }, size);
        } 

        public double[] noncentral_chisquare(double[] odf, double[] ononc, long size = 0)
        {
            ValidateSize(size);

            if (any_less_equal(odf, 0.0))
            {
                throw new ArgumentOutOfRangeException("odf", "odf <= 0.0");
            }
            if (any_less_equal(ononc, 0.0))
            {
                throw new ArgumentOutOfRangeException("ononc", "ononc <= 0.0");
            }

            if (odf.Length == 1 && ononc.Length == 1)
            {
                  return cont2_array_sc(internal_state, RandomDistributions.rk_noncentral_chisquare, size, odf[0], ononc[0]);
            }

            return cont2_array(internal_state, RandomDistributions.rk_noncentral_chisquare, size, odf, ononc);
        }
        #endregion

#region dirichlet
#if false
        public ndarray dirichlet(Int32[] alpha, Int32 size)
        {
            long k;
            npy_intp totsize;
            ndarray alpha_arr, val_arr;
            double[] alpha_data;
            double[] val_data;
            npy_intp i;
            double acc, invacc;

            k = alpha.Length;
            alpha_arr = np.array(alpha).astype(np.Float64);
            if (np.anyb(np.less_equal(alpha_arr, 0)))
            {
                throw new ValueError("alpha <= 0");
            }
            alpha_data = alpha_arr.Array.data.datap as double[];

            shape shape = new shape(size, k);

            ndarray diric = np.zeros(shape, np.Float64);
            val_arr = diric;
            val_data = val_arr.Array.data.datap as double[];

            i = 0;
            totsize = val_arr.size;

            while (i < totsize)
            {
                acc = 0.0;
                for (int j = 0; j < k; j++)
                {
                    val_data[i + j] = RandomDistributions.rk_standard_gamma(internal_state, alpha_data[j]);
                    acc = acc + val_data[i + j];
                }
                invacc = 1 / acc;
                for (int j = 0; j < k; j++)
                {
                    val_data[i + j] = val_data[i + j] * invacc;
                }
                i = i + k;
            }

            return diric;

        }
#endif
#endregion

#region exponential

        public double[] exponential(double scale = 1.0, long size = 0)
        {
            return exponential(new double[] { scale }, size);
        }

        public double[] exponential(double[] oscale, long size = 0)
        {
            ValidateSize(size);

            if (any_signbit(oscale))
            {
                throw new ArgumentOutOfRangeException("scale", "scale < 0");
            }

            if (oscale.Length == 1)
            {
                 return cont1_array_sc(internal_state, RandomDistributions.rk_exponential, size, oscale[0]);
            }
    
            return cont1_array(internal_state, RandomDistributions.rk_exponential, size, oscale);

        }


        #endregion

        #region f distribution
        public double[] f(double dfnum, double dfden, long size = 0)
        {
            return f(new double[] {dfnum}, new double[]{ dfden}, size);
        }

        public double[] f(double[] odfnum, double[] odfden, long size = 0)
        {
            ValidateSize(size);

            if (any_less_equal(odfnum, 0.0))
                throw new ArgumentOutOfRangeException("odfnum", "odfnum < 0");
            if (any_less_equal(odfden, 0.0))
                throw new ArgumentOutOfRangeException("odfden", "odfden < 0");

            if (odfnum.Length == 1 && odfden.Length == 1)
            {
                return cont2_array_sc(internal_state, RandomDistributions.rk_f, size, odfnum[0], odfden[0]);
            }

            return cont2_array(internal_state, RandomDistributions.rk_f, size, odfnum, odfden);
        }

        #endregion

        #region gamma

        public double[] gamma(double shape, double scale, long size = 0)
        {
            return gamma(new double[] { shape }, new double[] { scale }, size);
        }

        public double[] gamma(double[] oshape, double[] oscale, long size = 0)
        {
            ValidateSize(size);

            if (any_signbit(oshape))
                throw new ArgumentOutOfRangeException("oshape", "oshape < 0");

            if (any_signbit(oscale))
                throw new ArgumentOutOfRangeException("oscale", "oscale < 0");

            if (oshape.Length == 1 && oscale.Length == 1)
            {
                return cont2_array_sc(internal_state, RandomDistributions.rk_gamma, size, oshape[0], oscale[0]);
            }

            return cont2_array(internal_state, RandomDistributions.rk_gamma, size, oshape, oscale);
        }

        #endregion

        #region geometric

        public long[] geometric(double op, long size = 0)
        {
            return geometric(new double[] { op }, size);
        }

        public long[] geometric(double[] op, long size = 0)
        {
            ValidateSize(size);

            if (any_less(op, 0.0))
                throw new ArgumentOutOfRangeException("op", "op < 0");
            if (any_greater(op, 1.0))
                throw new ArgumentOutOfRangeException("op", "op > 0");

            if (op.Length == 1)
            {
                 return discd_array_sc(internal_state, RandomDistributions.rk_geometric, size, op[0]);
            }


            return discd_array(internal_state, RandomDistributions.rk_geometric, size, op);
        }



        #endregion

        #region gumbel

        public double[] gumbel(double[] oloc, double oscale, long size = 0)
        {
            return gumbel(oloc, new double[] { oscale }, size);
        }

        public double[] gumbel(double oloc, double[] oscale = null, long size = 0)
        {
            return gumbel(new double[] { oloc }, oscale, size);
        }

        public double[] gumbel(double oloc, double oscale, long size = 0)
        {
            return gumbel(new double[] { oloc }, new double[] { oscale }, size);
        }

        public double[] gumbel(double[] oloc, double[] oscale = null, long size = 0)
        {
            ValidateSize(size);

            if (oscale == null)
                oscale = new double[] { 1.0 };

            if (any_signbit(oscale))
                throw new ArgumentOutOfRangeException("oscale", "oscale < 0");


            if (oloc.Length == 1 && oscale.Length == 1)
            {
                return cont2_array_sc(internal_state, RandomDistributions.rk_gumbel, size, oloc[0], oscale[0]);
            }
   
            return cont2_array(internal_state, RandomDistributions.rk_gumbel, size, oloc, oscale);
        }

        #endregion

        #region hypergeometric

        public long[] hypergeometric(long ongood, long onbad, long onsample, long size = 0)
        {
            return hypergeometric(new long[] { ongood }, new long[] { onbad }, new long[] { onsample }, size);
        }

        public long[] hypergeometric(long[] ongood, long[] onbad, long[] onsample, long size = 0)
        {
            if (any_less(ongood, 0))
                throw new ArgumentOutOfRangeException("ongood", "ongood < 0");
            if (any_less(onbad, 0))
                throw new ArgumentOutOfRangeException("onbad", "onbad < 0");
            if (any_less(onsample, 1))
                throw new ArgumentOutOfRangeException("onsample", "onsample < 1");
            if (any_less(add(ongood,onbad), onsample))
                throw new ArgumentOutOfRangeException("onsample", "ngood + nbad < nsample");

            if (ongood.Length == 1 && onbad.Length == 1 && onsample.Length == 1)
            {
                return discnmN_array_sc(internal_state, RandomDistributions.rk_hypergeometric, size, ongood[0], onbad[0], onsample[0]);
            }

            return discnmN_array(internal_state, RandomDistributions.rk_hypergeometric, size, ongood, onbad, onsample);
        }


        #endregion

        #region laplace

        public double[] laplace(double oloc, double oscale, long size = 0)
        {
            return laplace(new double[] { oloc }, new double[] { oscale }, size);
        }

        public double[] laplace(double []oloc, double oscale, long size = 0)
        {
            return laplace(oloc, new double[] { oscale }, size);
        }

        public double[] laplace(double oloc, double[] oscale = null, long size = 0)
        {
            return laplace(new double[] { oloc }, oscale, size);
        }

        public double[] laplace(double []oloc, double[]oscale = null, long size = 0)
        {
            if (oscale == null)
                oscale = new double[] { 1.0 };

            if (any_signbit(oscale))
                throw new ArgumentOutOfRangeException("oscale", "oscale < 0");

            if (oloc.Length == 1 && oscale.Length == 1)
            {
                return cont2_array_sc(internal_state, RandomDistributions.rk_laplace, size, oloc[0], oscale[0]);
            }

            return cont2_array(internal_state, RandomDistributions.rk_laplace, size, oloc, oscale);
        }

        #endregion

        #region logistic

        public double[] logistic(double oloc, double oscale, long size = 0)
        {
            return logistic(new double[] { oloc }, new double[] { oscale }, size);
        }

        public double[] logistic(double[] oloc, double oscale, long size = 0)
        {
            return logistic(oloc, new double[] { oscale }, size);
        }

        public double[] logistic(double oloc)
        {
            return logistic(new double[] { oloc }, null, 0);
        }

        public double[] logistic(double[] oloc, double[] oscale = null, long size = 0)
        {
   
            if (oscale == null)
                oscale = new double[] { 1.0 };

            if (any_signbit(oscale))
                throw new ArgumentOutOfRangeException("oscale", "oscale < 0");

            if (oloc.Length == 1 && oscale.Length == 1)
            {
                return cont2_array_sc(internal_state, RandomDistributions.rk_logistic, size, oloc[0], oscale[0]);
            }

            return cont2_array(internal_state, RandomDistributions.rk_logistic, size, oloc, oscale);
        }

        #endregion

        #region lognormal

        public double[] lognormal(double omean, double osigma, long size = 0)
        {
            return lognormal(new double[] { omean }, new double[] { osigma }, size);
        }

        public double[] lognormal(double[] omean, double osigma, long size = 0)
        {
            return lognormal(omean, new double[] { osigma }, size);
        }

        public double[] lognormal(double[] omean, double[] osigma, long size = 0)
        {
            if (any_signbit(osigma))
                throw new ArgumentOutOfRangeException("osigma", "osigma < 0");


            if (omean.Length == 1 && osigma.Length == 1)
            {
                 return cont2_array_sc(internal_state, RandomDistributions.rk_lognormal, size, omean[0], osigma[0]);
            }


            return cont2_array(internal_state, RandomDistributions.rk_lognormal, size, omean, osigma);
        }

        #endregion

        #region logseries

        public long[] logseries(double op, long size = 0)
        {
            return logseries(new double[] { op }, size);
        }

        public long[] logseries(double[] op, long size = 0)
        {

            ValidateSize(size);

            if (any_less_equal(op,0.0))
                throw new ArgumentOutOfRangeException("op", "op <= 0");
            if (any_greater_equal(op, 1.0))
                throw new ArgumentOutOfRangeException("op", "op >= 1.0");

            if (op.Length == 1)
            {
                return discd_array_sc(internal_state, RandomDistributions.rk_logseries, size, op[0]);
            }

            return discd_array(internal_state, RandomDistributions.rk_logseries, size, op);
        }

 

        #endregion

        #region multinomial 

        public long[] multinomial(int n, double[] pvals, long size = 0)
        {
            long d = pvals.Length;

            if (kahan_sum(pvals, d - 1) > (1.0 + 1e-12))
            {
                throw new ArgumentOutOfRangeException("sum(pvals[:-1]) > 1.0");
            }

            var multin = new long[size != 0 ? size*d : d];
            long[] mnix = multin;
            long sz = multin.Length;

            long i = 0;
            while (i < sz)
            {
                double Sum = 1.0;
                long dn = n;
                for (long j = 0; j < d - 1; j++)
                {
                    mnix[i + j] = RandomDistributions.rk_binomial(internal_state, dn, pvals[j] / Sum);
                    dn = dn - mnix[i + j];
                    if (dn <= 0)
                        break;
                    Sum = Sum - pvals[j];
                }

                if (dn > 0)
                    mnix[i + d - 1] = dn;

                i = i + d;

            }

            return multin;
        }

        #endregion

        #region noncentral_f

        public double[] noncentral_f(double odfnum, double odfden, double ononc, long size = 0)
        {
            return noncentral_f(new double[] { odfnum }, new double[] { odfden }, new double[] { ononc }, size);
        }


        public double[] noncentral_f(double[] odfnum, double odfden, double ononc, long size = 0)
        {
            return noncentral_f(odfnum, new double[] { odfden}, new double[] { ononc}, size);
        }

        public double[] noncentral_f(double[] odfnum, double[] odfden, double[] ononc, long size = 0)
        {

            ValidateSize(size);

            if (any_less_equal(odfnum, 0.0))
                throw new ArgumentOutOfRangeException("odfnum", "odfnum < 0");
            if (any_less_equal(odfden, 0.0))
                throw new ArgumentOutOfRangeException("odfden", "odfden < 0");
            if (any_less_equal(ononc, 0.0))
                throw new ArgumentOutOfRangeException("ononc", "ononc < 0");

            if (odfnum.Length == 1 && odfden.Length == 1 && ononc.Length == 1)
            {
                return cont3_array_sc(internal_state, RandomDistributions.rk_noncentral_f, size, odfnum[0], odfden[0], ononc[0]);
            }
              
            return cont3_array(internal_state, RandomDistributions.rk_noncentral_f, size, odfnum, odfden, ononc);
        }


        #endregion

        #region normal

        public double[] normal(double oloc, double oscale, long size = 0)
        {
            return normal(new double[] { oloc }, new double[] { oscale }, size);
        }

        public double[] normal(double[] oloc, double oscale, long size = 0)
        {
            return normal(oloc, new double[] { oscale }, size);
        }

        public double[] normal(double[] oloc, double[] oscale, long size = 0)
        {
            ValidateSize(size);

            if (any_signbit(oscale))
                throw new ArgumentOutOfRangeException("oscale", "oscale < 0");

            if (oloc.Length == 1 && oscale.Length == 1)
            {
                return cont2_array_sc(internal_state, RandomDistributions.rk_normal, size, oloc[0], oscale[0]);
            }

            return cont2_array(internal_state, RandomDistributions.rk_normal, size, oloc, oscale);
        }

        #endregion

        #region pareto

        public double[] pareto(double oa, long size = 0)
        {
            return pareto(new double[] { oa }, size);
        }

        public double[] pareto(double[] oa, long size = 0)
        {
            ValidateSize(size);

            if (any_less_equal(oa, 0.0))
                throw new ArgumentOutOfRangeException("oa", "oa < 0");

            if (oa.Length == 1)
            {
                 return cont1_array_sc(internal_state, RandomDistributions.rk_pareto, size, oa[0]);
            }

            return cont1_array(internal_state, RandomDistributions.rk_pareto, size, oa);
        }
        #endregion

        #region Poisson
        public long[] poisson(double olam, long size = 0)
        {
            return poisson(new double[] { olam }, size);
        }
        public long[] poisson(double[] olam, long size = 0)
        {
            const double poisson_lam_max = 9.223372006484771e+18;

            if (any_less(olam, 0.0))
                throw new ArgumentOutOfRangeException("olam", "olam < 0");
            if (any_greater(olam, poisson_lam_max))
                throw new ArgumentOutOfRangeException("olam", "lam value too large");

            if (olam.Length == 1)
            {
                return discd_array_sc(internal_state, RandomDistributions.rk_poisson, size, olam[0]);
            }



            return discd_array(internal_state, RandomDistributions.rk_poisson, size, olam);
        }


        #endregion

        #region Power
        public double[] power(double oa, long size = 0)
        {
            return power(new double[] { oa }, size);
        }
        public double[] power(double[] oa, long size = 0)
        {
            ValidateSize(size);

            if (any_signbit(oa))
                throw new ArgumentOutOfRangeException("oa", "oa < 0");

            if (oa.Length == 1)
            {
                 return cont1_array_sc(internal_state, RandomDistributions.rk_power, size, oa[0]);
            }

            return cont1_array(internal_state, RandomDistributions.rk_power, size, oa);
        }
        #endregion

        #region rayleigh
        public double[] rayleigh(double oscale, long size = 0)
        {
            return rayleigh(new double[] { oscale }, size);
        }
        public double[] rayleigh(double[] oscale, long size = 0)
        {
            ValidateSize(size);

            if (any_signbit(oscale))
                throw new ArgumentOutOfRangeException("oscale", "oscale < 0");

            if (oscale.Length == 1)
            {
                 return cont1_array_sc(internal_state, RandomDistributions.rk_rayleigh, size, oscale[0]);
            }
    
            return cont1_array(internal_state, RandomDistributions.rk_rayleigh, size, oscale);
        }
#endregion

#region standard_cauchy

        public double standard_cauchy()
        {
            double[] rndArray = cont0_array(internal_state, RandomDistributions.rk_standard_cauchy, 0);
            return rndArray[0];
        }


        public double[] standard_cauchy(long size)
        {
            double[] rndArray = cont0_array(internal_state, RandomDistributions.rk_standard_cauchy, size);
            return rndArray;
        }

#endregion

#region standard_exponential

        public double standard_exponential()
        {
            double [] rndArray = cont0_array(internal_state, RandomDistributions.rk_standard_exponential, 0);
            return rndArray[0];
        }


        public double[] standard_exponential(long size)
        {
            double[] rndArray = cont0_array(internal_state, RandomDistributions.rk_standard_exponential, size);
            return rndArray;
        }

        #endregion

        #region standard_gamma

        public double[] standard_gamma(double oshape, long size = 0)
        {
            return standard_gamma(new double[] { oshape }, size);
        }

        public double[] standard_gamma(double[] oshape, long size = 0)
        {
            ValidateSize(size);

            if (any_signbit(oshape))
                throw new ArgumentOutOfRangeException("oshape", "oshape < 0");

            if (oshape.Length == 1)
            {
                return cont1_array_sc(internal_state, RandomDistributions.rk_standard_gamma, size, oshape[0]);
            }

            return cont1_array(internal_state, RandomDistributions.rk_standard_gamma, size, oshape);
        }

#endregion

#region standard_normal

        public double standard_normal()
        {
            double[] rndArray = cont0_array(internal_state, RandomDistributions.rk_gauss, 0);
            return rndArray[0];
        }

        public double [] standard_normal(long size)
        {
            double [] rndArray = cont0_array(internal_state, RandomDistributions.rk_gauss, size);
            return rndArray;
        }

        #endregion

        #region standard_t

        public double[] standard_t(double odf, long size = 0)
        {
            return standard_t(new double[] { odf }, size);
        }

        public double[] standard_t(double[] odf, long size = 0)
        {
            ValidateSize(size);

            if (any_less_equal(odf, 0.0))
                throw new ArgumentOutOfRangeException("odf", "odf < 0");

            if (odf.Length == 1)
            {
                 return cont1_array_sc(internal_state, RandomDistributions.rk_standard_t, size, odf[0]);
            }

            return cont1_array(internal_state, RandomDistributions.rk_standard_t, size, odf);
        }

        #endregion

        #region triangular
        public double[] triangular(double oleft, double omode, double oright, long size = 0)
        {
            return triangular(new double[] { oleft }, new double[] { omode }, new double[] { oright }, size);
        }
        public double[] triangular(double[] oleft, double omode, double oright, long size = 0)
        {
            return triangular(oleft, new double[] { omode }, new double[] { oright }, size);
        }
        public double[] triangular(double[] oleft, double[] omode, double oright, long size = 0)
        {
            return triangular(oleft, omode, new double[] { oright }, size);
        }
        public double[] triangular(double[] oleft, double[] omode, double[] oright, long size = 0)
        {
            ValidateSize(size);

            if (any_greater(oleft, omode))
                throw new ArgumentOutOfRangeException("left > mode");
            if (any_greater(omode, oright))
                throw new ArgumentOutOfRangeException("mode > right");
            if (any_equal(oleft, oright))
                throw new ArgumentOutOfRangeException("left == right");

            if (oleft.Length == 1 && omode.Length == 1 && oright.Length == 1)
            {
                 return cont3_array_sc(internal_state, RandomDistributions.rk_triangular, size, oleft[0], omode[0], oright[0]);
            }
                   
            return cont3_array(internal_state, RandomDistributions.rk_triangular, size, oleft, omode, oright);
        }

        #endregion

        #region uniform
        public double[] uniform(double low = 0.0, double high = 1.0, long size = 0)
        {
            ValidateSize(size);

            double[] lowarr = new double[] { low };
            double[] higharr = new double[] { high };

            return uniform(lowarr, higharr, size);
        }

        public double[] uniform(double[] olow, double[] ohigh, long size = 0)
        {
            ValidateSize(size);

            double[] odiff = subtract(ohigh, olow);
            if (any_isfinite(odiff))
                throw new ArgumentOutOfRangeException("Range exceeds valid bounds");

            if (olow.Length == 1 && ohigh.Length == 1)
            {
                return cont2_array_sc(internal_state, RandomDistributions.rk_uniform, size, olow[0], ohigh[0] - olow[0]);
            }

            return cont2_array(internal_state, RandomDistributions.rk_uniform, size, olow, odiff);
        }


        #endregion

        #region vonmises
        public double[] vonmises(double omu, double okappa, long size = 0)
        {
            return vonmises(new double[] { omu }, new double[] { okappa }, size);
        }
        public double[] vonmises(double[] omu, double okappa, long size = 0)
        {
            return vonmises(omu, new double[] { okappa }, size);
        }
        public double [] vonmises(double [] omu, double[] okappa, long size = 0)
        {
            if (any_less(okappa, 0.0))
                throw new ArgumentOutOfRangeException("okappa < 0");

            if (omu.Length == 1 && okappa.Length == 1)
            {
                 return cont2_array_sc(internal_state, RandomDistributions.rk_vonmises, size, omu[0], okappa[0]);
            }

            return cont2_array(internal_state, RandomDistributions.rk_vonmises, size, omu, okappa);
        }

        #endregion

        #region wald
        public double[] wald(double omean, double oscale, long size = 0)
        {
            return wald(new double[] { omean }, new double[] { oscale }, size);
        }
        public double[] wald(double[] omean, double oscale, long size = 0)
        {
            return wald(omean, new double[] { oscale }, size);
        }
        public double [] wald(double [] omean, double[] oscale, long size = 0)
        {
            if (any_less_equal(omean, 0.0))
                throw new ArgumentOutOfRangeException("omean <= 0");

            if (any_less_equal(oscale, 0.0))
                throw new ArgumentOutOfRangeException("oscale <= 0");

            if (omean.Length == 1 && oscale.Length == 1)
            {
                 return cont2_array_sc(internal_state, RandomDistributions.rk_wald, size, omean[0], oscale[0]);
            }

            return cont2_array(internal_state, RandomDistributions.rk_wald, size, omean, oscale);
        }

        #endregion

        #region weibull

        public double[] weibull(double oa, long size = 0)
        {
            return weibull(new double[] { oa }, size);
        }
        public double[] weibull(double [] oa, long size = 0)
        {
            if (any_signbit(oa))
                throw new ArgumentOutOfRangeException("oa <= 0");

            if (oa.Length == 1)
            {
                return cont1_array_sc(internal_state, RandomDistributions.rk_weibull, size, oa[0]);
            }

            return cont1_array(internal_state, RandomDistributions.rk_weibull, size, oa);
        }


        #endregion

        #region zipf

        public long[] zipf(double oa, long size = 0)
        {
            return zipf(new double[] { oa }, size);
        }

        public long[] zipf(double[] oa, long size = 0)
        {
            // use logic that ensures NaN is rejected.
            if (!any_greater(oa, 1.0))
                throw new ArgumentOutOfRangeException("'a' must contain valid floats > 1.0");

            if (oa.Length == 1)
            {
                 return discd_array_sc(internal_state, RandomDistributions.rk_zipf, size, oa[0]);
            }

            return discd_array(internal_state, RandomDistributions.rk_zipf, size, oa);
        }



#endregion

        
#region Python Version

        private double[] cont0_array(rk_state state, Func<rk_state, double> func, long size)
        {
            double[] array_data;
            long i;

            if (size == 0)
            {
                lock (rk_lock)
                {
                    double rv = func(state);
                    return new double[] { rv };
                }
            }
            else
            {
                array_data = new double[size];
                lock (rk_lock)
                {
                    for (i = 0; i < array_data.Length; i++)
                    {
                        array_data[i] = func(state);
                    }
                }
                return array_data;
            }

        }

        private double[] cont1_array(rk_state state, Func<rk_state, double, double> func, long size, double[] oa)
        {
            double[] array_data;
            double[] oa_data;

            if (size == 0)
            {
                array_data = new double[size];

                oa_data = oa;
                for (long i = 0; i < size; i++)
                {
                    array_data[i] = func(state, oa_data[i]);
                }
            }

            else
            {
                array_data = new double[size];

                oa_data = oa;
                if (size != array_data.Length)
                {
                    throw new ArgumentOutOfRangeException("size is not compatible with inputs");
                }

                for (long i = 0; i < size; i++)
                {
                    array_data[i] = func(state, oa_data[i]);
                }
            }

            return array_data;
        }




        private double[] cont1_array_sc(rk_state state, Func<rk_state, double, double> func, long size, double a)
        {
            if (size == 0)
            {
                double rv = func(state, a);
                return new double[] { rv };
            }
            else
            {
                var array_data = new double[size];
                lock (rk_lock)
                {
                    for (int i = 0; i < array_data.Length; i++)
                    {
                        array_data[i] = func(state, a);
                    }
                }
                return array_data;
            }
        }


        private double[] cont2_array_sc(rk_state state, Func<rk_state, double, double, double> func, long size, double a, double b)
        {
            if (size == 0)
            {
                double rv = func(state, a, b);
                return new double[] { rv };
            }
            else
            {
                var array_data = new double[size];
                lock (rk_lock)
                {
                    for (int i = 0; i < array_data.Length; i++)
                    {
                        array_data[i] = func(state, a, b);
                    }
                }
                return array_data;
            }

        }


        private double[] cont2_array(rk_state state, Func<rk_state, double, double, double> func, long size, double[] oa, double[] ob)
        {
            double[] array_data;
  
            if (size == 0)
            {
                array_data = new double[oa.Length];
                size = oa.Length;
            }
            else
            {
                if (size != oa.Length)
                {
                    throw new ArgumentException("size is not compatible with inputs");
                }

                array_data = new double[size];
            }


            int oa_index = 0;
            int ob_index = 0;
            for (int i = 0; i < size; i++)
            {
                array_data[i] = func(state, oa[oa_index++], ob[ob_index++]);
                if (oa_index >= oa.Length)
                    oa_index = 0;
                if (ob_index >= ob.Length)
                    ob_index = 0;
            }

            return array_data;

        }

        private double[] cont3_array_sc(rk_state state, Func<rk_state, double, double, double, double> func, long size, double a, double b, double c)
        {
            if (size == 0)
            {
                double rv = func(state, a, b, c);
                return new double[] { rv };
            }
            else
            {
                var array_data = new double[size];
                lock (rk_lock)
                {
                    for (int i = 0; i < array_data.Length; i++)
                    {
                        array_data[i] = func(state, a, b, c);
                    }
                }
                return array_data;
            }
        }

        private double[] cont3_array(rk_state state, Func<rk_state, double, double, double, double> func, long size, double[] oa, double[] ob, double[] oc)
        {
            double[] array_data;

            if (size == 0)
            {
                array_data = new double[oa.Length];
            }
            else
            {
                array_data = new double[size];
            }

            int oa_index = 0;
            int ob_index = 0;
            int oc_index = 0;
            for (int i = 0; i < array_data.Length; i++)
            {
                array_data[i] = func(state, oa[oa_index++], ob[ob_index++], oc[oc_index++]);
                if (oa_index >= oa.Length)
                    oa_index = 0;
                if (ob_index >= ob.Length)
                    ob_index = 0;
                if (oc_index >= oc.Length)
                    oc_index = 0;
            }

            return array_data;
        }


        private long[] discd_array(rk_state state, Func<rk_state, double, long> func, long size, double[] oa)
        {
            long[] array_data;
  

            if (size == 0)
            {
                array_data = new long[oa.Length];
            }
            else
            {
                array_data = new long[size];
            }

            int oa_index = 0;
            for (int i = 0; i < array_data.Length; i++)
            {
                array_data[i] = func(state, oa[oa_index++]);
                if (oa_index >= oa.Length)
                    oa_index = 0;
            }

            return array_data;

        }

        private long[] discd_array_sc(rk_state state, Func<rk_state, double, long> func, long size, double a)
        {
            if (size == 0)
            {
                var rv = func(state, a);
                return new long[] { rv };
            }

            var array_data = new long[size];
            var length = array_data.Length;
            for (int i = 0; i < length; i++)
            {
                array_data[i] = func(state, a);
            }

            return array_data;
        }

        private long[] discnp_array(rk_state state, Func<rk_state, long, double, long> func, long size, long[] on, double[] op)
        {
            long[] array_data;


            if (size == 0)
            {
                array_data = new long[on.Length];
            }
            else
            {
                array_data = new long[size];
            }

            int on_index = 0;
            int op_index = 0;
            for (int i = 0; i < array_data.Length; i++)
            {
                array_data[i] = func(state, on[on_index++], op[op_index++]);
                if (on_index >= on.Length)
                    on_index = 0;
                if (op_index >= op.Length)
                    op_index = 0;
            }

            return array_data;

        }

        private long[] discnp_array_sc(rk_state state, Func<rk_state, long, double, long> func, long size, long n, double p)
        {
            if (size == 0)
            {
                long rv = func(state, n, p);
                return new long[] { rv };
            }

            long[] array_data = new long[size];

            for (int i = 0; i < array_data.Length; i++)
            {
                array_data[i] = func(state, n, p);
            }

            return array_data;
        }

        private long[] discdd_array(rk_state state, Func<rk_state, double, double, long> func, long size, long[] on, double[] op)
        {
            long[] array_data;

            if (size == 0)
            {
                array_data = new long[on.Length];
            }
            else
            {
                array_data = new long[size];
            }

            int on_index = 0;
            int op_index = 0;
            for (int i = 0; i < array_data.Length; i++)
            {
                array_data[i] = func(state, on[on_index++], op[op_index++]);
                if (on_index >= on.Length)
                    on_index = 0;
                if (op_index >= op.Length)
                    op_index = 0;
            }

            return array_data;

        }

        private long [] discdd_array_sc(rk_state state, Func<rk_state, double, double, long> func, long size, long n, double p)
        {
            if (size == 0)
            {
                long rv = func(state, n, p);
                return new long[] { rv };
            }

            long[] array_data = new long[size];

            for (int i = 0; i < array_data.Length; i++)
            {
                array_data[i] = func(state, n, p);
            }

            return array_data;
        }


        private long[] discnmN_array(rk_state state, Func<rk_state, long, long, long, long> func, long size, long[] on, long[] om, long[] oN)
        {
            long[] array_data;

            if (size == 0)
            {
                array_data = new long[on.Length];
            }
            else
            {
                array_data = new long[size];
            }

            int on_index = 0;
            int om_index = 0;
            int oN_index = 0;
            for (int i = 0; i < array_data.Length; i++)
            {
                array_data[i] = func(state, on[on_index++], om[om_index++], oN[oN_index++]);
                if (on_index >= on.Length)
                    on_index = 0;
                if (om_index >= om.Length)
                    om_index = 0;
                if (oN_index >= oN.Length)
                    oN_index = 0;
            }

            return array_data;
        }

        private long[] discnmN_array_sc(rk_state state, Func<rk_state, long, long, long, long> func, long size, long n, long m, long N)
        {
            if (size == 0)
            {
                long rv = func(state, n, m, N);
                return new long[] { rv };
            }

            long[] array_data = new long[size];

            for (int i = 0; i < array_data.Length; i++)
            {
                array_data[i] = func(state, n, m, N);
            }

            return array_data;
        }

        private double kahan_sum(double[] darr, long n)
        {
            double c, y, t, sum;
            long i;
            sum = darr[0];
            c = 0.0;
            for (i = 0; i < n; i++)
            {
                y = darr[i] - c;
                t = sum + y;
                c = (t - sum) - y;
                sum = t;
            }

            return sum;
        }

   
        private bool any_less_equal(double[] array, double value)
        {
            foreach (double v in array)
            {
                if (v <= value)
                    return true;
            }
            return false;
        }

        private bool any_greater(double[] array, double value)
        {
            foreach (double v in array)
            {
                if (v > value)
                    return true;
            }
            return false;
        }

        private bool any_greater_equal(double[] array, double value)
        {
            foreach (double v in array)
            {
                if (v >= value)
                    return true;
            }
            return false;
        }

        private bool any_less(double[] array, double value)
        {
            foreach (double v in array)
            {
                if (v < value)
                    return true;
            }
            return false;
        }

        private bool any_less(long[] array, int value)
        {
            foreach (long v in array)
            {
                if (v < value)
                    return true;
            }
            return false;
        }

   
   

        private bool any_nan(double[] array)
        {
            foreach (double v in array)
            {
                if (double.IsNaN(v))
                    return true;
            }
            return false;
        }

        private bool any_signbit(double[] array)
        {
            foreach (double v in array)
            {
                if (Math.Sign(v) < 0)
                    return true;
            }
            return false;
        }

        private bool any_less(long[] arr1, long[] arr2)
        {
            int arr2_index = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] < arr2[arr2_index++])
                    return true;
                if (arr2_index >= arr2.Length)
                    arr2_index = 0;
            }

            return false;
        }

        private bool any_equal(double[] arr1, double[] arr2)
        {
            int arr2_index = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == arr2[arr2_index++])
                    return true;
                if (arr2_index >= arr2.Length)
                    arr2_index = 0;
            }
            return false;
        }

        private bool any_greater(double[] arr1, double[] arr2)
        {
            int arr2_index = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] > arr2[arr2_index++])
                    return true;
                if (arr2_index >= arr2.Length)
                    arr2_index = 0;
            }
            return false;
        }

        private long[] add(long[] arr1, long[] arr2)
        {
            long[] sum = new long[arr1.Length];

            int arr2_index = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                sum[i] = arr1[i] + arr2[arr2_index++];
                if (arr2_index >= arr2.Length)
                    arr2_index = 0;
            }

            return sum;
        }

        private double[] subtract(double[] arr1, double[] arr2)
        {
            double[] sub = new double[arr1.Length];

            int arr2_index = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                sub[i] = arr1[i] - arr2[arr2_index++];
                if (arr2_index >= arr2.Length)
                    arr2_index = 0;
            }

            return sub;
        }
        private bool any_isfinite(double[] array)
        {
            foreach (double v in array)
            {
                if (double.IsInfinity(v) || double.IsNaN(v))
                    return true;
            }
            return false;
        }


        #endregion
    }

}
