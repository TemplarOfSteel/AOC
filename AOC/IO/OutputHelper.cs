using AOC.Convertion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.IO
{
    static class OutputHelper
    {
        public static void Print(int i)
        {
            Print(i.ToString());
        }

        public static void Print(string s)
        {
            Console.WriteLine(s);
        }

        public static void Print(IEnumerable<string> strings)
        {
            var list = strings.ToList();
            for (int i = 0; i< list.Count(); i++)
            {
                Console.WriteLine(i + ": " + list[i]);
            }
        }

        public static void Print(IEnumerable<IEnumerable<string>> strings)
        {
            Print(strings.Select(s => StringOperations.Combine(s, " | ")));
        }
    }
}
