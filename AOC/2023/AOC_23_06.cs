using AOC.IO;
using AOC.Convertion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data;
using AOC.Generics;
using System.Reflection.Metadata.Ecma335;
using System.Reflection;
using AOC.Comparison.LookUpTable;

namespace AOC._2023
{
    static class AOC_23_06
    {
        public static long Result_A()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty();
            var timeList = Converter.ToInt(lines[0].Split(' ').Where(s => s != "").Skip(1));
            var distanceList = Converter.ToInt( lines[1].Split(' ').Where(s => s != "").Skip(1));

            var mul = 1;
            for(int i = 0;i<timeList.Count(); i++)
            {
                var count = 0;
                for(int t = 0;t <= timeList[i];t++)
                {
                    var len = t * (timeList[i] - t);
                    if (len > distanceList[i]) { count++; }
                }
                mul *= count;
            }


            return mul;
        }

        public static long Result_B()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty();
            var time = Converter.ToLong(Converter.ToString( lines[0].Split(' ').Where(s => s != "").Skip(1)));
            var distance = Converter.ToLong(Converter.ToString(lines[1].Split(' ').Where(s => s != "").Skip(1)));

            long count = 0;
            for (int t = 0; t <= time; t++)
            {
                var len = t * (time - t);
                if (len > distance) { count++; }
            }

            return count;
        }
    }
}
