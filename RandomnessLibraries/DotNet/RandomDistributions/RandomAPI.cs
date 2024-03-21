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

        private int double_divsize = GetDivSize(sizeof(double));
        private int long_divsize = GetDivSize(sizeof(long));

        private static int GetDivSize(int elsize)
        {
            switch (elsize)
            {
                //case 1: return 0;
                //case 2: return 1;
                //case 4: return 2;
                case 8: return 3;
                case 16: return 4;
                    //case 32: return 5;
                    //case 64: return 6;
                    //case 128: return 7;
                    //case 256: return 8;
                    //case 512: return 9;
                    //case 1024: return 10;
                    //case 2048: return 11;
                    //case 4096: return 12;
            }

            throw new Exception("Unexpected elsize in GetDivSize");
        }


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

        private void ValidateSize(Int64 size)
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

        public double[] rand(Int64 size)
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

        public double[] randn(Int64 size)
        {
            ValidateSize(size);

            return standard_normal(size);
        }

        #endregion

        #region randbool

        public bool[] randbool(Int64 low, UInt64? high, Int64 size)
        {
            ValidateSize(size);

            bool[] randomData = new bool[size];

            RandomDistributions.rk_random_bool(false, true, randomData.Length, randomData, internal_state);

            return randomData;
        }

        #endregion

        #region randint8

        public sbyte[] randint8(Int64 low, UInt64? high, Int64 size)
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

        public byte[] randuint8(Int64 low, UInt64? high, Int64 size)
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

        public Int16[] randint16(Int64 low, UInt64? high, Int64 size)
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

        public UInt16[] randuint16(Int64 low, UInt64? high, Int64 size)
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
 

        public Int32[] randint32(Int64 low, UInt64? high, Int64 size)
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

        public UInt32[] randuint32(Int64 low, UInt64? high, Int64 size)
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

        public Int64[] randint64(Int64 low, UInt64? high, Int64 size)
        {
            ValidateSize(size);

            if (low < System.Int64.MinValue)
                throw new ArgumentOutOfRangeException("low", string.Format("low is out of bounds for Int64"));
            if (high > System.Int64.MaxValue)
                throw new ArgumentOutOfRangeException("high", string.Format("high is out of bounds for Int64"));

            Int64[] randomData = new Int64[size];

            Int64 _low = Convert.ToInt64(low);
            Int64? _high = null;

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

        public UInt64[] _randuint64(Int64 low, UInt64? high, Int64 size)
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
        public Int32[] random_integers(Int64 low, UInt64? high = null, Int64 size = 0)
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

        public double [] random_sample(Int64 size)
        {
            double [] rndArray = cont0_array(internal_state, RandomDistributions.rk_double, size);
            return rndArray;
        }

        #endregion

#region choice
#if false
        public ndarray choice(Int64 a, shape size = null, bool replace = true, double[] p = null)
        {
            return choice((object)a, size, replace, p);
        }

        public ndarray choice(object a, shape size = null, bool replace = true, double[] p = null)
        {
            Int64 pop_size = 0;

            // Format and Verify input
            ndarray aa = np.array(a, copy: false);
            if (aa.ndim == 0)
            {
                if (aa.TypeNum != NPY_TYPES.NPY_INT64)
                {
                    throw new ValueError("a must be a 1-dimensional or an integer");
                }

                pop_size = (Int64)aa;
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

            Int64 _size = 0;
            shape shape = size;
            if (size != null)
            {
                _size = (Int64)np.prod(np.asanyarray(size.iDims));
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
            Int64 i;

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

        public double[] beta(double[] a, double[] b, Int64 size = 0)
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

        public Int64[] binomial(Int64[] on, double[] op, Int64 size = 0)
        {
            Int64 ln;
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
 

        public Int64[] negative_binomial(Int64[] on, double[] op, Int64 size = 0)
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

        public double[] chisquare(double []odf, Int64 size = 0)
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

        public double[] noncentral_chisquare(double[] odf, double[] ononc, Int64 size = 0)
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

        public double[] exponential(double scale = 1.0, Int64 size = 0)
        {
            return exponential(new double[] { scale }, size);
        }

        public double[] exponential(double[] oscale, Int64 size = 0)
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

        public double[] f(double[] odfnum, double[] odfden, Int64 size = 0)
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

        public double[] gamma(double[] oshape, double[] oscale, Int64 size = 0)
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

        public double[] geometric(double[] op, Int64 size = 0)
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

        public double[] gumbel(double[] oloc, double[] oscale = null, Int64 size = 0)
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

        public Int64[] hypergeometric(Int64[] ongood, Int64[] onbad, Int64[] onsample, Int64 size = 0)
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

        public double[] laplace(double []oloc, double[]oscale = null, Int64 size = 0)
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

        public double[] logistic(double[] oloc, double[] oscale = null, Int64 size = 0)
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

        public double[] lognormal(double[] omean, double[] osigma, Int64 size = 0)
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

        public long[] logseries(double[] op, Int64 size = 0)
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

        public long[] multinomial(int n, double[] pvals, Int64 size = 0)
        {
            Int64 d = pvals.Length;

            if (kahan_sum(pvals, d - 1) > (1.0 + 1e-12))
            {
                throw new ArgumentOutOfRangeException("sum(pvals[:-1]) > 1.0");
            }

            var multin = new long[size];
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
        public double[] noncentral_f(double[] odfnum, double[] odfden, double[] ononc, Int64 size = 0)
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

        public double[] normal(double[] oloc, double[] oscale, Int64 size = 0)
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

        public double[] pareto(double[] oa, Int64 size = 0)
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

        public long[] poisson(double[] olam, Int64 size = 0)
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
        public double[] power(double[] oa, Int64 size = 0)
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

        public double[] rayleigh(double[] oscale, Int64 size = 0)
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


        public double[] standard_cauchy(Int64 size)
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


        public double[] standard_exponential(Int64 size)
        {
            double[] rndArray = cont0_array(internal_state, RandomDistributions.rk_standard_exponential, size);
            return rndArray;
        }

#endregion

#region standard_gamma

        public double[] standard_gamma(double[] oshape, Int64 size = 0)
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

        public double [] standard_normal(Int64 size)
        {
            double [] rndArray = cont0_array(internal_state, RandomDistributions.rk_gauss, size);
            return rndArray;
        }

#endregion

#region standard_t

        public double[] standard_t(double[] odf, Int64 size = 0)
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

        public double[] triangular(double[] oleft, double[] omode, double[] oright, Int64 size = 0)
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
        public double[] uniform(double low = 0.0, double high = 1.0, Int64 size = 0)
        {
            ValidateSize(size);

            double[] lowarr = new double[] { low };
            double[] higharr = new double[] { high };

            return uniform(lowarr, higharr, size);
        }

        public double[] uniform(double[] olow, double[] ohigh, Int64 size = 0)
        {
            ValidateSize(size);

            double[] odiff = subtract(ohigh, olow);

             todo: make sure these two bunches of logic are equivalent
            if (all_isfinite(odiff))
                throw new ArgumentOutOfRangeException("Range exceeds valid bounds");

            if (!np.allb(np.isfinite(odiff)))
                throw new Exception("Range exceeds valid bounds");
             todo: make sure these two bunches of logic are equivalent

            if (olow.Length == 1 && ohigh.Length == 1)
            {
                return cont2_array_sc(internal_state, RandomDistributions.rk_uniform, size, olow[0], ohigh[0] - olow[0]);
            }

            return cont2_array(internal_state, RandomDistributions.rk_uniform, size, olow, odiff);
        }

   

        #endregion

        #region vonmises

        public ndarray vonmises(object mu, object kappa, shape newdims = null)
        {
            ndarray omu, okappa;
            double fmu, fkappa;
            npy_intp[] size = null;
            if (newdims != null)
                size = newdims.iDims;

            omu = asanyarray(mu).astype(np.Float64);
            okappa = asanyarray(kappa).astype(np.Float64);

            if (omu.size == 1 && okappa.size == 1)
            {
                fmu = (double)omu.GetItem(0);
                fkappa = (double)okappa.GetItem(0);

                if (fkappa < 0)
                    throw new ValueError("kappa < 0");

                return cont2_array_sc(internal_state, RandomDistributions.rk_vonmises, size, fmu, fkappa);
            }

            if (np.anyb(np.less(okappa, 0.0)))
            {
                throw new ValueError("kappa < 0");
            }
            return cont2_array(internal_state, RandomDistributions.rk_vonmises, size, omu, okappa);
        }

#endregion

#region wald

        public ndarray wald(object mean, object scale, shape newdims = null)
        {
            ndarray omean = np.asanyarray(mean).astype(np.Float64);
            ndarray oscale = np.asanyarray(scale).astype(np.Float64);
            npy_intp[] size = null;
            if (newdims != null)
                size = newdims.iDims;

            if (omean.size == 1 && oscale.size == 1)
            {
                double fmean = Convert.ToDouble(mean);
                double fscale = Convert.ToDouble(scale);

                if (fmean <= 0)
                    throw new ValueError("mean <= 0");
                if (fscale <= 0)
                    throw new ValueError("scale <= 0");

                return cont2_array_sc(internal_state, RandomDistributions.rk_wald, size, fmean, fscale);
            }

            if (np.anyb(np.less_equal(omean, 0.0)))
                throw new ValueError("mean <= 0.0");
            if (np.anyb(np.less_equal(oscale, 0.0)))
                throw new ValueError("scale <= 0.0");

            return cont2_array(internal_state, RandomDistributions.rk_wald, size, omean, oscale);
        }

#endregion

#region weibull

        public ndarray weibull(object a, shape newdims = null)
        {
            ndarray oa;
            double fa;
            npy_intp[] size = null;
            if (newdims != null)
                size = newdims.iDims;

            oa = asanyarray(a).astype(np.Float64);

            if (oa.size == 1)
            {
                fa = (double)oa.GetItem(0);
                if ((bool)np.signbit(fa).GetItem(0))
                    throw new ValueError("a < 0");

                return cont1_array_sc(internal_state, RandomDistributions.rk_weibull, size, fa);
            }


            if (np.anyb(np.signbit(oa)))
            {
                throw new ValueError("a < 0");
            }
            return cont1_array(internal_state, RandomDistributions.rk_weibull, size, oa);
        }


#endregion

#region zipf

        public ndarray zipf(object a, shape shape = null)
        {
            ndarray oa;
            double fa;
            npy_intp[] size = null;
            if (shape != null)
                size = shape.iDims;

            oa = asanyarray(a).astype(np.Float64);

            if (oa.size == 1)
            {
                fa = (double)oa.GetItem(0);

                // use logic that ensures NaN is rejected.
                if (!(fa > 1.0))
                {
                    throw new ValueError("'a' must be a valid float > 1.0");
                }
                return discd_array_sc(internal_state, RandomDistributions.rk_zipf, size, fa);
            }

            // use logic that ensures NaN is rejected.
            if (!np.allb(np.greater(oa, 1.0)))
                throw new ValueError("'a' must contain valid floats > 1.0");

            return discd_array(internal_state, RandomDistributions.rk_zipf, size, oa);
        }



#endregion


#region helper functions

        private npy_intp CountTotalElements(npy_intp[] dims)
        {
            npy_intp TotalElements = 1;
            for (int i = 0; i < dims.Length; i++)
            {
                TotalElements *= dims[i];
            }

            return TotalElements;
        }

        private shape ConvertToSingleElementIfNull(shape _shape)
        {
            if (_shape == null)
            {
                return new shape(1);
            }
            return _shape;
        }
        private npy_intp CountTotalElements(shape _shape)
        {
            npy_intp TotalElements = 1;
            for (int i = 0; i < _shape.iDims.Length; i++)
            {
                TotalElements *= _shape.iDims[i];
            }

            return TotalElements;
        }
#endregion

#region Python Version

        private double[] cont0_array(rk_state state, Func<rk_state, double> func, Int64 size)
        {
            double[] array_data;
            Int64 i;

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

        private double[] cont1_array(rk_state state, Func<rk_state, double, double> func, Int64 size, double[] oa)
        {
            double[] array_data;
            double[] oa_data;

            if (size == 0)
            {
                array_data = new double[size];

                oa_data = oa;
                for (Int64 i = 0; i < size; i++)
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

                for (Int64 i = 0; i < size; i++)
                {
                    array_data[i] = func(state, oa_data[i]);
                }
            }

            return array_data;
        }




        private double[] cont1_array_sc(rk_state state, Func<rk_state, double, double> func, Int64 size, double a)
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


        private double[] cont2_array_sc(rk_state state, Func<rk_state, double, double, double> func, Int64 size, double a, double b)
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


        private double[] cont2_array(rk_state state, Func<rk_state, double, double, double> func, Int64 size, double[] oa, double[] ob)
        {
            double[] array_data;

            if (oa.Length != ob.Length)
            {
                throw new ArgumentException("oa and ob must be the same length array");
            }

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
             
            for (int i = 0; i < size; i++)
            {
                array_data[i] = func(state, oa[i], ob[i]);
            }

            return array_data;

        }

        private double[] cont3_array_sc(rk_state state, Func<rk_state, double, double, double, double> func, Int64 size, double a, double b, double c)
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

        private ndarray cont3_array(rk_state state, Func<rk_state, double, double, double, double> func, npy_intp[] size, ndarray oa, ndarray ob, ndarray oc)
        {
            broadcast multi;
            ndarray array;
            double[] array_data;

            if (size == null)
            {
                multi = np.broadcast(oa, ob, oc);
                array = np.empty(multi.shape, dtype: np.Float64);
            }
            else
            {
                array = np.empty(new shape(size), dtype: np.Float64);
                multi = np.broadcast(oa, ob, oc, array);
                if (multi.shape != array.shape)
                {
                    throw new ValueError("size is not compatible with inputs");
                }
            }

            array_data = array.Array.data.datap as double[];

            VoidPtr vpoa = multi.IterData(0);
            VoidPtr vpob = multi.IterData(1);
            VoidPtr vpoc = multi.IterData(2);


            double[] oa_data = multi.IterData(0).datap as double[];
            double[] ob_data = multi.IterData(1).datap as double[];
            double[] oc_data = multi.IterData(2).datap as double[];

            for (int i = 0; i < multi.size; i++)
            {
                vpoa = multi.IterData(0);
                vpob = multi.IterData(1);
                vpoc = multi.IterData(2);

                array_data[i] = func(state, oa_data[vpoa.data_offset >> double_divsize],
                                            ob_data[vpob.data_offset >> double_divsize],
                                            oc_data[vpoc.data_offset >> double_divsize]);
                multi.IterNext();
            }

            return np.array(array_data);
        }


        private ndarray discd_array(rk_state state, Func<rk_state, double, long> func, npy_intp[] size, ndarray oa)
        {
            long[] array_data;
            ndarray array;
            npy_intp length;
            npy_intp i;
            broadcast multi;
            flatiter itera;

            if (size == null)
            {
                array_data = new long[CountTotalElements(oa.dims)];
                length = array_data.Length;
                double[] oa_data = oa.Array.data.datap as double[];

                itera = NpyCoreApi.IterNew(oa);

                foreach (var dd in itera)
                {
                    array_data[itera.Iter.index] = func(state, oa_data[itera.CurrentPtr.data_offset >> double_divsize]);
                }

                return np.array(array_data);
            }
            else
            {
                array = np.empty(new shape(size), np.Int64);
                array_data = array.Array.data.datap as long[];

                multi = np.broadcast(array, oa);
                if (multi.size != array.size)
                {
                    throw new ValueError("size is not compatible with inputs");
                }

                double[] oa_data = multi.IterData(1).datap as double[];
                for (i = 0; i < multi.size; i++)
                {
                    var vpoa = multi.IterData(1);
                    array_data[i] = func(state, oa_data[vpoa.data_offset >> double_divsize]);
                    multi.IterNext();
                }

                return array.reshape(size);
            }

        }

        private long[] discd_array_sc(rk_state state, Func<rk_state, double, long> func, Int64 size, double a)
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

        private ndarray discnp_array(rk_state state, Func<rk_state, long, double, long> func, npy_intp[] size, ndarray on, ndarray op)
        {
            broadcast multi;
            ndarray array;
            long[] array_data;

            if (size == null)
            {
                multi = np.broadcast(on, op);
                array = np.empty(multi.shape, dtype: np.Int64);
            }
            else
            {
                array = np.empty(new shape(size), dtype: np.Int64);
                multi = np.broadcast(on, op, array);
                if (multi.shape != array.shape)
                {
                    throw new ValueError("size is not compatible with inputs");
                }
            }

            array_data = array.AsInt64Array();

            VoidPtr vpon = multi.IterData(0);
            VoidPtr vpop = multi.IterData(1);


            long[] on_data = multi.IterData(0).datap as long[];
            double[] op_data = multi.IterData(1).datap as double[];

            for (int i = 0; i < multi.size; i++)
            {
                vpon = multi.IterData(0);
                vpop = multi.IterData(1);
                array_data[i] = func(state, on_data[vpon.data_offset >> long_divsize], op_data[vpop.data_offset >> double_divsize]);
                multi.IterNext();
            }

            return np.array(array_data);
        }

        private long[] discnp_array_sc(rk_state state, Func<rk_state, long, double, long> func, Int64 size, long n, double p)
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

        private ndarray discdd_array(rk_state state, Func<rk_state, double, double, long> func, npy_intp[] size, ndarray on, ndarray op)
        {
            broadcast multi;
            ndarray array;
            long[] array_data;

            if (size == null)
            {
                multi = np.broadcast(on, op);
                array = np.empty(multi.shape, dtype: np.Int64);
            }
            else
            {
                array = np.empty(new shape(size), dtype: np.Int64);
                multi = np.broadcast(on, op, array);
                if (multi.shape != array.shape)
                {
                    throw new ValueError("size is not compatible with inputs");
                }
            }

            array_data = array.AsInt64Array();

            VoidPtr vpon = multi.IterData(0);
            VoidPtr vpop = multi.IterData(1);


            long[] on_data = multi.IterData(0).datap as long[];
            double[] op_data = multi.IterData(1).datap as double[];

            for (int i = 0; i < multi.size; i++)
            {
                vpon = multi.IterData(0);
                vpop = multi.IterData(1);
                array_data[i] = func(state, on_data[vpon.data_offset >> long_divsize], op_data[vpop.data_offset >> double_divsize]);
                multi.IterNext();
            }

            return np.array(array_data);
        }

        private long [] discdd_array_sc(rk_state state, Func<rk_state, double, double, long> func, Int64 size, long n, double p)
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




        private ndarray discnmN_array(rk_state state, Func<rk_state, long, long, long, long> func, npy_intp[] size, ndarray on, ndarray om, ndarray oN)
        {
            broadcast multi;
            ndarray array;
            long[] array_data;

            if (size == null)
            {
                multi = np.broadcast(on, om, oN);
                array = np.empty(multi.shape, dtype: np.Int32);
            }
            else
            {
                array = np.empty(new shape(size), dtype: np.Int32);
                multi = np.broadcast(on, om, oN, array);
                if (multi.shape != array.shape)
                {
                    throw new ValueError("size is not compatible with inputs");
                }
            }

            array_data = array.AsInt64Array();

            VoidPtr vpon = multi.IterData(0);
            VoidPtr vpom = multi.IterData(1);
            VoidPtr vpoN = multi.IterData(2);


            long[] on_data = multi.IterData(0).datap as long[];
            long[] om_data = multi.IterData(1).datap as long[];
            long[] oN_data = multi.IterData(2).datap as long[];

            for (int i = 0; i < multi.size; i++)
            {
                vpon = multi.IterData(0);
                vpom = multi.IterData(1);
                vpoN = multi.IterData(2);

                array_data[i] = func(state, on_data[vpon.data_offset >> long_divsize],
                                            om_data[vpom.data_offset >> long_divsize],
                                            oN_data[vpoN.data_offset >> long_divsize]);
                multi.IterNext();
            }

            return np.array(array_data);
        }

        private long[] discnmN_array_sc(rk_state state, Func<rk_state, long, long, long, long> func, Int64 size, long n, long m, long N)
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

        private double kahan_sum(double[] darr, Int64 n)
        {
            double c, y, t, sum;
            Int64 i;
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

        private shape _shape_from_size(shape size, npy_intp d)
        {
            if (size == null)
            {
                return new shape(d);
            }

            npy_intp[] newdims = new npy_intp[size.iDims.Length + 1];
            Array.Copy(size.iDims, 0, newdims, 0, size.iDims.Length);
            newdims[size.iDims.Length] = d;

            return new shape(newdims);

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

        private bool any_less(long[] array, long[] value)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < value[i])
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


        private bool any_equal(double[] arr1, double[] arr2)
        {
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == arr2[i])
                    return true;
            }
            return false;
        }

        private bool any_greater(double[] arr1, double[] arr2)
        {
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] > arr2[i])
                    return true;
            }
            return false;
        }

        private long[] add(long[] arr1, long[] arr2)
        {
            long[] sum = new long[arr1.Length];

            for (int i = 0; i < arr1.Length; i++)
            {
                sum[i] = arr1[i] + arr2[i];
            }

            return sum;
        }

        private double[] subtract(double[] arr1, double[] arr2)
        {
            double[] sub = new double[arr1.Length];

            for (int i = 0; i < arr1.Length; i++)
            {
                sub[i] = arr1[i] - arr2[i];
            }

            return sub;
        }



        #endregion
    }

}
