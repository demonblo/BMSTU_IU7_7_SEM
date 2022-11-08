using System;
using System.Numerics;

namespace lab04
{
    class RSA
    {
        private UInt64 _startKey = (UInt64)Math.Pow(2, 31);
        private UInt64 _endKey = (UInt64)Math.Pow(2, 32) - 1;
        public UInt64 privateKey = 0;

        public UInt64 publicKey;
        public UInt64 n;

        public RSA()
        {
            UInt64 p = GetSimpleKey(_startKey, _endKey);
            UInt64 q = GetSimpleKey(_startKey, _endKey);
            n = p * q;
            UInt64 fi = MathFuncs.fi(p, q);

            while (privateKey == 0)
            {
                publicKey = GetPublicKey(fi);
                privateKey = GetPrivateKey(publicKey, fi);
            }

            Console.WriteLine("n = {0}  privateKey = {1}  publicKey = {2}", n, privateKey, publicKey);
        }

        static UInt64 GetSimpleKey(UInt64 start, UInt64 end)
        {
            UInt64 num;

            do
            {
                num = MathFuncs.GenerateUInt64(start, end);
            } while (!MathFuncs.IsSimple(num));

            return num;
        }

        static UInt64 GetPublicKey(UInt64 fi, UInt64 left = 1)
        {
            UInt64 e, res;

            do
            {
                e = MathFuncs.GenerateUInt64(left + 1, fi);
                res = MathFuncs.GCD(e, fi);
            } while (res != 1);

            return e;            
        }

        static UInt64 GetPrivateKey(UInt64 a, UInt64 b)
        {
            BigInteger res;

            (_, res, _) = _getPrivateKey(a, b);

            if (res < 0)
                return 0;

            return (UInt64)res;
        }

        // ax + by = нод(a, b)

        static (BigInteger, BigInteger, BigInteger) _getPrivateKey(BigInteger a, BigInteger b)
        {
            BigInteger s, s1, t, t1, gcd;

            if (b.IsZero)
                return (a, 1, 0);

            (gcd, s1, t1) = _getPrivateKey(b, a % b);

            s = t1;
            t = s1 - (a / b) * t1;

            return (gcd, s, t);
        }
    }
}