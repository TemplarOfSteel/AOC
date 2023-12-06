using AOC.IO;
using AOC.Convertion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data;

namespace AOC._2023
{
    static class AOC_23_03_old
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

        static List<int> Neighbours(int index, int columns, int rows)
        {
            var ret = new List<int>();

            int c = index % columns;
            int r = index / columns;

            if(!(r<0+1))
            {
                ret.Add(Index(columns, c, r - 1));

                if (!(c < 0 + 1))
                {
                    ret.Add(Index(columns, c - 1, r-1));
                }

                if (!(c >= columns - 1))
                {
                    ret.Add(Index(columns, c + 1, r-1));
                }
            }

            if (!(r >= rows-1))
            {
                ret.Add(Index(columns, c, r + 1));

                if (!(c < 0 + 1))
                {
                    ret.Add(Index(columns, c - 1, r + 1));
                }

                if (!(c >= columns - 1))
                {
                    ret.Add(Index(columns, c + 1, r + 1));
                }
            }

            if (!(c < 0 + 1))
            {
                ret.Add(Index(columns, c-1, r));
            }

            if (!(c >= columns - 1))
            {
                ret.Add(Index(columns, c+1, r));
            }

            return ret;
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

            OutputHelper.Print(columns + ":" + rows);

            var nodes = new List<Node>();
            for(int r = 0;r<rows;r++)
            {
                var line = lines[r];
                for(int c = 0; c < columns; c++)
                {
                    nodes.Add(new Node(ValueAtPos(line, c,columns,r, out var indexes),indexes));
                }
            }

            OutputHelper.Print("nodes: " + nodes.Count);

            List<int> ints = new List<int>();
            for(int n =0;n<nodes.Count();n++)
            {
                if (nodes[n].value==True)
                {
                    foreach (var i in Neighbours(n, columns, rows))
                    {
                        string integerS = Converter.KeepIntegersOnly(nodes[i].value);
                        if(integerS.Length>0)
                        {
                            if(nodes[i].used == false)
                            {
                                ints.Add(Converter.ToInt(integerS));

                                foreach(int i2 in nodes[i].indexes)
                                {
                                    nodes[i2].used = true;
                                }
                            }
                        }
                    }
                }
            }

            foreach(var i in ints)
            {
                Console.WriteLine(i);
            }

            return ints.Sum();
        }

        public static int Result_B()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty();
            int columns = lines[0].Length;
            int rows = lines.Count;

            OutputHelper.Print(columns + ":" + rows);

            var nodes = new List<Node>();
            for (int r = 0; r < rows; r++)
            {
                var line = lines[r];
                for (int c = 0; c < columns; c++)
                {
                    nodes.Add(new Node(ValueAtPos(line, c, columns, r, out var indexes), indexes));
                }
            }

            OutputHelper.Print("nodes: " + nodes.Count);

            List<int> ints = new List<int>();
            for (int n = 0; n < nodes.Count(); n++)
            {
                if (nodes[n].value == PotentialGear)
                {
                    int intNear = 0;
                    int ratio = 1;
                    foreach (var i in Neighbours(n, columns, rows))
                    {
                        string integerS = Converter.KeepIntegersOnly(nodes[i].value);
                        if (integerS.Length > 0)
                        {
                            if (nodes[i].used == false)
                            {
                                intNear++;
                                ratio *= Converter.ToInt(integerS);

                                foreach (int i2 in nodes[i].indexes)
                                {
                                    nodes[i2].used = true;
                                }
                            }
                        }
                    }
                    if (intNear == 2) { ints.Add(ratio); }

                    foreach (var i in Neighbours(n, columns, rows))
                    {
                        string integerS = Converter.KeepIntegersOnly(nodes[i].value);
                        if (integerS.Length > 0)
                        {
                            if (nodes[i].used == true)
                            {
                                foreach (int i2 in nodes[i].indexes)
                                {
                                    nodes[i2].used = false;
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
