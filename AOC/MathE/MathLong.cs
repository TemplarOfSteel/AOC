using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.MathE
{
    internal static class MathLong
    {
        internal static long LeastCommonMultiple(long a, long b)
        {
            if (a == b) { return a; }
            long num1;
            long num2;
            if (a > b)
            {
                num1 = a;
                num2 = b;
            }
            else
            {
                num1 = b;
                num2 = a;
            }

            for (long i = 1; i < num2; i++)
            {
                if ((i * num1) % num2 == 0)
                {
                    return i * num1;
                }
            }
            return num2 * num1;
        }

        internal static long LeastCommonMultiple(params long[] numbers)
        {
            long ret = 1;
            foreach(var number in numbers)
            {
                ret = LeastCommonMultiple(ret, number);
            }
            return ret;
        }

        internal static long GreatestCommonDivisor(long a, long b)
        {
            if (a == b) { return a; }
            return a * b / LeastCommonMultiple(a, b);
        }

        internal static long GreatestCommonDivisor(params long[] numbers)
        {
            long ret = numbers[0];
            foreach (var number in numbers)
            {
                ret = GreatestCommonDivisor(ret, number);
            }
            return ret;
        }
    }
}
