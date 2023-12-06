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

namespace AOC._2023
{
    static class AOC_23_04
    {      
        static int PointsOnCard(IEnumerable<int> winningNUmbers, IEnumerable<int> yourNumbers)
        {
            int matches = MatchesOnCard(winningNUmbers, yourNumbers);

            if (matches == 0) { return 0; }

            return (int)(1 * Math.Pow(2, matches - 1));
        }

        static int MatchesOnCard(IEnumerable<int> winningNUmbers, IEnumerable<int> yourNumbers)
        {
            int matches = 0;

            foreach (int i in yourNumbers)
            {
                if (winningNUmbers.Contains(i)) { matches++; }
            }

            return matches;
        }

        public static int Result_A()
        {
            List<List<List<int>>> linesSplit = InputHelper.ReadAllLinesUntilEmpty()
                .Select(l => l.Split('|'))
                .Select(l => new List<List<int>> () 
                { 
                    l[0].Split(":")[1].Split(' ').Where(s=>s!="").Select(s=>Converter.ToInt(s)).ToList(),
                    l[1].Split(' ').Where(s=>s!="").Select(s => Converter.ToInt(s)).ToList()
                }).ToList();

            var points = 0;
            for(int i = 0; i<linesSplit.Count();i++)
            {
                points += PointsOnCard(linesSplit[i][0], linesSplit[i][1]);
            }

            return points;
        }

        public static int Result_B()
        {
            List<List<List<int>>> linesSplit = InputHelper.ReadAllLinesUntilEmpty()
                .Select(l => l.Split('|'))
                .Select(l => new List<List<int>>()
                {
                    l[0].Split(":")[1].Split(' ').Where(s=>s!="").Select(s=>Converter.ToInt(s)).ToList(),
                    l[1].Split(' ').Where(s=>s!="").Select(s => Converter.ToInt(s)).ToList()
                }).ToList();

            List<int> cardsInEnd = new List<int>();
            for (int i = 0; i < linesSplit.Count(); i++)
            {
                cardsInEnd.Add(1);
            }
            
            var points = 0;
            for (int i = 0; i < linesSplit.Count(); i++)
            {
                int matches = MatchesOnCard(linesSplit[i][0], linesSplit[i][1]);
                int copies = cardsInEnd[i];
                for(int j = 1;j<= matches; j++)
                {
                    if (i + j >= linesSplit.Count()) { break; }
                    cardsInEnd[i + j] += copies;
                }
            }

            return cardsInEnd.Sum();
        }
    }
}
