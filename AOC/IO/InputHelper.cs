using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.IO
{
    public static class InputHelper
    {
        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static List<string> ReadAllLinesUntilEmpty()
        {
            var ret = new List<string>();
            string line;
            while((line = ReadLine() ) != null && line !="")
            {
                ret.Add(line);
            }
            return ret;
        }

        public static List<List<string>> ReadAllLinesSeperatedByEmptyLine()
        {
            List<List<string>> ret = new List<List<string>>();
            while(true)
            {
                var newList = ReadAllLinesUntilEmpty();
                if (newList == null || newList.Count() == 0) { break; }
                else { ret.Add(newList); }
            }
            return ret;
        }
    }
}
