using AOC.IO;
using AOC.Convertion;
using System.Collections.Generic;
using System.Linq;


namespace AOC._2023
{
    public static class AOC_23_09
    {
        static int Next(List<int> series)
        {
            var diffs = new List<int>();
            for(int i = 1; i < series.Count; i++)
            {
                diffs.Add(series[i] - series[i-1]);
            }

            if(!series.Any(x => x != 0))
            {
                return 0;
            }
            else
            {
                return series.Last() + Next(diffs);
            }
        }

        static int Previous(List<int> series)
        {
            var diffs = new List<int>();
            for (int i = 1; i < series.Count; i++)
            {
                diffs.Add(series[i] - series[i - 1]);
            }

            if (!series.Any(x => x != 0))
            {
                return 0;
            }
            else
            {
                return series.First() - Previous(diffs);
            }
        }

        public static int Result_A()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty();
            var results = new List<int>();

            foreach(var line in lines)
            {
                results.Add(Next(Converter.ToInt(line.Split(' '))));
            }

            return results.Sum();
        }

        public static long Result_B()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty();
            var results = new List<int>();

            foreach (var line in lines)
            {
                results.Add(Previous(Converter.ToInt(line.Split(' '))));
            }

            return results.Sum();
        }
    }

}
