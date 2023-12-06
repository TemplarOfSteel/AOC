using AOC.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOC.Convertion;

namespace AOC._2022
{
    internal class AOC_22_01
    {
        public static int Result_A()
        {
            List<List<string>> perElf = InputHelper.ReadAllLinesSeperatedByEmptyLine();
            List<int> caloriesPerElf = perElf.Select(list => Converter.ToInt(list).Sum()).ToList();
            caloriesPerElf.Sort();
            int highest = caloriesPerElf.Last();
            return highest;
        }

        public static int Result_B()
        {
            List<List<string>> perElf = InputHelper.ReadAllLinesSeperatedByEmptyLine();
            List<int> caloriesPerElf = perElf.Select(list => Converter.ToInt(list).Sum()).ToList();
            caloriesPerElf.Sort();
            int c = caloriesPerElf.Count();
            int highest = caloriesPerElf[c-1] + caloriesPerElf[c - 2] + caloriesPerElf[c - 3];
            return highest;
        }
    } 
}
