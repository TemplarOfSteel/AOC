using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC.Convertion
{
    public static class Converter
    {
        private static readonly Regex rxNonDigits = new Regex(@"[^\d]+");

        public static Dictionary<string, int> Integers = new Dictionary<string, int>
        {
            { "one", 1},
            { "two", 2},
            { "three", 3},
            { "four", 4},
            { "five", 5},
            { "six", 6},
            { "seven", 7},
            { "eight", 8},
            { "nine",   9},
            { "zero",  0}
        };

        public static int ToInt(string s)
        {
            foreach(var kvp in Integers)
            {
                s = s.Replace(kvp.Key, kvp.Value.ToString());
            }
            
            return int.Parse(s);
        }

        public static long ToLong(string s)
        {
            foreach (var kvp in Integers)
            {
                s = s.Replace(kvp.Key, kvp.Value.ToString());
            }

            return long.Parse(s);
        }

        public static List<int> ToInt(IEnumerable<string> e)
        {
            var ret = new List<int>();
            foreach(var s in e)
            {
                ret.Add(ToInt(s));
            }
            return ret;
        }

        public static List<long> ToLong(IEnumerable<string> e)
        {
            var ret = new List<long>();
            foreach (var s in e)
            {
                ret.Add(ToLong(s));
            }
            return ret;
        }

        public static string KeepIntegersOnly(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            string cleaned = rxNonDigits.Replace(s, "");
            return cleaned;
        }

        public static List<string> KeepIntegersOnly(IEnumerable<string> e)
        {
            var ret = new List<string>();
            foreach (var s in e)
            {
                ret.Add(KeepIntegersOnly(s));
            }
            return ret;
        }

        public static string ToString(IEnumerable <string> toCombine)
        {
            var ret = "";
            foreach(var s in toCombine)
            {
                ret += s;
            }
            return ret;
        }
    }
}
