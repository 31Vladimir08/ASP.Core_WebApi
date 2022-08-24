using System;
using System.Collections.Generic;

namespace Fibonacci
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var d = GetFibonacci(4, 10);

            foreach (var item in d)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Расчет фибоначчи
        /// </summary>
        /// <param name="x">число</param>
        /// <param name="n">кол-во чисел для вывода</param>
        /// <returns></returns>
        public static List<long> GetFibonacci(long x, int n)
        {
            var result = new List<long>();
            int a = 0;
            int b = 1;

            while (n > 0)
            {
                int temp = a;
                a = b;
                b = temp + b;

                if (a <= x)
                    continue;
                result.Add(a);
                n--;
            }
            return result;
        }
    }
}
