using System;
using System.Diagnostics;

namespace AOC2020
{
    public static class HelperFunctions
    {
        public static void Log(string s)
        {
            if (Debugger.IsAttached) Debug.WriteLine(s);
            Console.WriteLine(s);
        }

        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        public static long LCM(long a, long b) => a / GCD(a, b) * b;

    }
}
