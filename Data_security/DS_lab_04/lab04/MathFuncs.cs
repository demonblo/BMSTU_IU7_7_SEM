using System;

namespace lab04
{
    class MathFuncs
    {
        static Random rnd = new Random();

        public static bool IsSimple(UInt64 num) //Проверка числа на простоту
        {
            UInt64 temp = (UInt64)Math.Sqrt(num);
            UInt64 i = 2;

            while (i <= temp)
            {
                if (num % i == 0)
                    return false;
                i++;
            }
                
            return true;                
        }

        public static UInt64 fi(UInt64 p, UInt64 q) //Вычисление fi
        {
            return (p - 1) * (q - 1);
        }

        public static UInt64 GCD(UInt64 a, UInt64 b) //НОД
        {
            UInt64 temp;

            while (b != 0)
            {
                if (a < b)
                {
                    temp = a;
                    a = b;
                    b = temp;
                }
                
                temp = b;
                b = a - b;
                a = temp;
            }

            return a;
        }

        public static UInt64 GenerateUInt64(UInt64 start, UInt64 end) 
        {
            UInt64 res;
            byte[] temp = new byte[sizeof(UInt64)];

            rnd.NextBytes(temp);
            res = BitConverter.ToUInt64(temp, 0);
            res = res % (end - start) + start;

            return res;
        }
    }
}