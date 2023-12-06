using AOC.IO;
using AOC.Convertion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace AOC._2023
{
    static class AOC_23_02
    {
        public static bool Valid(int r, int g, int b, string line)
        {
            //rgb is max
            foreach(var set in line.Split(':')[1].Split(';'))
            {
                var cubes = set.Split(',');
                int ri = 0;
                int gi = 0;
                int bi = 0;
                foreach(var c in cubes)
                {
                    ri += (NumberOfColor(c, "red"));
                    gi += (NumberOfColor(c, "green"));
                    bi += (NumberOfColor(c, "blue"));
                }
                if (ri > r || gi > g || bi > b) { return false; }
            }
            return true;
        }

        public static int MinCubesPow(string line)
        {
            int rm = 0;
            int gm = 0;
            int bm = 0;

            foreach(var set in line.Split(':')[1].Split(';'))
            {
                var cubes = set.Split(',');
                int ri = 0;
                int gi = 0;
                int bi = 0;
                foreach(var c in cubes)
                {
                    ri += NumberOfColor(c, "red");
                    gi += NumberOfColor(c, "green");
                    bi += NumberOfColor(c, "blue");
                }

                rm = Math.Max(rm, ri);
                gm = Math.Max(gm, gi);
                bm = Math.Max(bm, bi);
            }

            return Pow(rm,gm,bm);
        }

        public static int Pow(int r, int g, int b)
        {
            return r * g * b;
        }

        public static int NumberOfColor(string source, string color)
        {
            if (source.Contains(color)) { return Converter.ToInt( Converter.KeepIntegersOnly(source)); }
            return 0;
        }

        public static int GameID(string line)
        {
            return Converter.ToInt(Converter.KeepIntegersOnly(line.Split(':')[0]));
        }

        public static int Result_A()
        {
            var red = 12;
            var green = 13;
            var blue = 14;

            var lines = InputHelper.ReadAllLinesUntilEmpty();
            var validGames = lines.Select(l=>Valid(red,green,blue,l)? GameID (l): 0);

            return  validGames.Sum();
        }

        public static int Result_B()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty();
            var minCubesPerLineSquared = lines.Select(line => MinCubesPow(line));

            return minCubesPerLineSquared.Sum();
        }
    }
}
