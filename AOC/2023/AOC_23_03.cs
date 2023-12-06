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
    static class AOC_23_03
    {
        private const string True = "T";
        private const string False = "F";
        private const string PotentialGear = "PG";

        private class Node
        {
            internal Node(string v, List<int> indexes)
            {
                value = v;
                this.indexes = indexes;
            }

            internal List<int> indexes;
            internal string value;
            internal bool used = false;
        }

        static int Index(int columns, int column, int row)
        {
            return column + columns * row;
        }


        private static string ValueAtPos(string line, int pos, int columns, int row, out List<int> indexes)
        {
            indexes = new List<int>();
            if (line[pos] == '.') { return False; }
            if (line[pos] == '*') { return PotentialGear; }
            if (Converter.KeepIntegersOnly(line[pos].ToString()) != "")
            {
                bool reachedPos = false;
                var ret = "";
                for(int i = 0; i < line.Length; i++) 
                {
                    if (i == pos) { reachedPos = true; }
                    var atPos = Converter.KeepIntegersOnly(line[i].ToString());
                    if (atPos != "")
                    {
                        ret += atPos;
                        indexes.Add(Index(columns,i,row));
                    }
                    else
                    {
                        if (reachedPos == true)
                        {
                            return ret; 
                        }
                        else { ret = ""; indexes = new List<int>(); ; }
                    }
                }
                return ret;
            }

            return True;
        }



        public static int Result_A()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty();
            int columns = lines[0].Length;
            int rows = lines.Count;

            var grid = new Grid<Node>(columns, rows);

            OutputHelper.Print(columns + ":" + rows);

            for (int r = 0; r < rows; r++)
            {
                var line = lines[r];
                for (int c = 0; c < columns; c++)
                {
                    grid[new Coord(c, r)] = new Node(ValueAtPos(line, c, columns, r, out var indexes), indexes);
                }
            }

            List<int> ints = new List<int>();
            for (int c = 0; c < grid.columns; c++)
            {
                for (int r = 0; r < grid.rows; r++)
                {
                    if (grid[new Coord(c, r)].value == True)
                    {
                        foreach (var i in grid.NeighbourIndexes(new Coord(c,r),false,true))
                        {
                            string integerS = Converter.KeepIntegersOnly(grid[i].value);
                            if (integerS.Length > 0)
                            {
                                if (grid[i].used == false)
                                {
                                    ints.Add(Converter.ToInt(integerS));

                                    foreach (int i2 in grid[i].indexes)
                                    {
                                        grid[i2].used = true;
                                    }
                                }
                            } 
                        } 
                    }
                }
            }

            return ints.Sum();
        }

        public static int Result_B()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty();
            int columns = lines[0].Length;
            int rows = lines.Count;

            OutputHelper.Print(columns + ":" + rows);

            var grid = new Grid<Node>(columns, rows);

            OutputHelper.Print(columns + ":" + rows);

            for (int r = 0; r < rows; r++)
            {
                var line = lines[r];
                for (int c = 0; c < columns; c++)
                {
                    grid[new Coord(c, r)] = new Node(ValueAtPos(line, c, columns, r, out var indexes), indexes);
                }
            }

            List<int> ints = new List<int>();
            for (int c = 0; c < grid.columns; c++)
            {
                for (int r = 0; r < grid.rows; r++)
                {
                    if (grid[new Coord(c, r)].value == PotentialGear)
                    {
                        int intNear = 0;
                        int ratio = 1;
                        foreach (var i in grid.NeighbourIndexes(new Coord(c, r), false, true))
                        {
                            string integerS = Converter.KeepIntegersOnly(grid[i].value);
                            if (integerS.Length > 0)
                            {
                                if (grid[i].used == false)
                                {
                                    intNear++;
                                    ratio *= Converter.ToInt(integerS);

                                    foreach (int i2 in grid[i].indexes)
                                    {
                                        grid[i2].used = true;
                                    }
                                }
                            }
                        }
                        if (intNear == 2) { ints.Add(ratio); }

                        foreach (var i in grid.NeighbourIndexes(new Coord(c, r), false, true))
                        {
                            string integerS = Converter.KeepIntegersOnly(grid[i].value);
                            if (integerS.Length > 0)
                            {
                                if (grid[i].used == true)
                                {
                                    foreach (int i2 in grid[i].indexes)
                                    {
                                        grid[i2].used = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ints.Sum();
        }
    }
}
