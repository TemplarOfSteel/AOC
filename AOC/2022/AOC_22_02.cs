using AOC.Convertion;
using AOC.IO;
using AOC.SimpleGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2022
{
    internal class AOC_22_02
    {
        private static Dictionary<char, int> PointsPerChoice = new Dictionary<char, int>
        {
            {'R',1 },
            {'P',2 },
            {'S',3 }
        };

        private static Dictionary<char, int> PointsPerResult = new Dictionary<char, int>
        {
            {'W',6 },
            {'T',3 },
            {'L',0 }
        };

        private static Dictionary<char, char> MappingsA = new Dictionary<char, char>
        {
            {'A','R' },
            {'B','P' },
            {'C','S' },
            {'X','R' },
            {'Y','P' },
            {'Z','S' }
        };

        private static Dictionary<char, char> MappingsB = new Dictionary<char, char>
        {
            {'A','R' },
            {'B','P' },
            {'C','S' },
            {'X','L' },
            {'Y','T' },
            {'Z','W' }
        };

        public static int Result_A()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty();
            var stratPerLine = lines.Select(line => StringOperations.Replace(line, MappingsA).Replace(" ",""));
            var stratPerLineAsList = stratPerLine.Select(line => new List<char>() { line.First(), line.Last() });
            var pointsPerLine = stratPerLineAsList.Select(l => PointsPerResult[RockPaperScissor.Result(l[1], l[0])] + PointsPerChoice[l[1]]);

            return pointsPerLine.Sum();
        }

        public static int Result_B()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty();
            var stratPerLine = lines.Select(line => StringOperations.Replace(line, MappingsB).Replace(" ", ""));
            var stratPerLineAsList = stratPerLine.Select(line => new List<char>() { line.First(), RockPaperScissor.ChoiceSelf(line.First(), line.Last())});
            var pointsPerLine = stratPerLineAsList.Select(l => PointsPerResult[RockPaperScissor.Result(l[1], l[0])] + PointsPerChoice[l[1]]);

            return pointsPerLine.Sum();
        }
    }
}
