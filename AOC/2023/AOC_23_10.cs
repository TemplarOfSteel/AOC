using AOC.IO;
using AOC.Convertion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using AOC.Generics;
using System.Collections;

namespace AOC._2023
{
    public static class AOC_23_10
    {
        private static bool South(char c)
        {
           return c == '|' || c == 'F' || c == '7';
        }

        private static bool East(char c)
        {
            return c == '-' || c == 'L' || c == 'F';
        }

        private static bool North(char c)
        {
            return c == '|' || c == 'J' || c == 'L';
        }

        private static bool West(char c)
        {
            return c == '-' || c == '7' || c == 'J';
        }

        public static int Result_A()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty().Select(l => l.ToCharArray()).ToList();
            var rows = lines.Count();
            var columns = lines[0].Count();

            var grid = new Grid<GridConnectionNode>(columns, rows);
            Coord startCoord = new Coord();
            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    grid[new Coord(c, r)] = new GridConnectionNode(South(lines[r][c]), East(lines[r][c]), North(lines[r][c]), West(lines[r][c]));

                    if (lines[r][c] == 'S')
                    {
                        startCoord = new Coord(c, r);
                    }
                }
            }

            foreach (var nCoord in grid.NeighbourCoords(startCoord, false, false))
            {
                var dir = nCoord - startCoord;
                if (dir.row == -1 && grid[nCoord].south) { grid[startCoord].north = true; }
                if (dir.row == 1 && grid[nCoord].north) { grid[startCoord].south = true; }

                if (dir.col == 1 && grid[nCoord].west) { grid[startCoord].east = true; }
                if (dir.col == -1 && grid[nCoord].east) { grid[startCoord].west = true; }
            }

            var timeStart = DateTime.Now;
            var count = GridConnectionNode.ConnectedCoordsInLoop(grid, startCoord, false, false).Count();
            var timeTotal = DateTime.Now - timeStart;
            OutputHelper.Print("Took seconds: " + timeTotal.TotalSeconds);
            return count / 2;
        }

        public static int Result_B()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty().Select(l => l.ToCharArray()).ToList();
            var rows = lines.Count();
            var columns = lines[0].Count();

            var grid = new Grid<GridConnectionNode>(columns, rows);
            Coord startCoord = new Coord();
            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    grid[new Coord(c, r)] = new GridConnectionNode(South(lines[r][c]), East(lines[r][c]), North(lines[r][c]), West(lines[r][c]));

                    if (lines[r][c] == 'S')
                    {
                        startCoord = new Coord(c, r);
                    }
                }
            }

            foreach (var nCoord in grid.NeighbourCoords(startCoord, false, false))
            {
                var dir = nCoord - startCoord;
                if (dir.row == -1 && grid[nCoord].south) { grid[startCoord].north = true; }
                if (dir.row == 1 && grid[nCoord].north) { grid[startCoord].south = true; }

                if (dir.col == 1 && grid[nCoord].west) { grid[startCoord].east = true; }
                if (dir.col == -1 && grid[nCoord].east) { grid[startCoord].west = true; }
            }

            var loopNodes = GridConnectionNode.ConnectedCoords(grid, startCoord, false, false);
            var found = GridConnectionNode.InsideLoopCoords(grid, loopNodes).Count();

            return found;
        }
    }

    public static class AOC_23_10_Old
    {
        private class Node
        {
            public bool south;
            public bool east;
            public bool north;
            public bool west;
            public bool start;
            public char c;
            public bool isLoop = false;

            public Node(char c)
            {
                south = c == '|' || c == 'F' || c == '7';
                east  = c == '-' || c == 'L' || c == 'F';
                north = c == '|' || c == 'J' || c == 'L';
                west  = c == '-' || c == '7' || c == 'J';
                start = c == 'S';
                this.c = c;
            }
        }

        private static Coord Next(Grid<Node> grid, Coord current, Coord last)
        {
            var nCoords = grid.NeighbourCoords(current, false, false);
            foreach (var nCoord in nCoords)
            {
                if (nCoord == last) { continue; }

                var dir = nCoord - current;

                if (dir.row == 1 && grid[nCoord].north && grid[current].south) { return nCoord; }
                if (dir.row == -1 && grid[nCoord].south && grid[current].north) { return nCoord; }

                if (dir.col == 1 && grid[nCoord].west && grid[current].east) { return nCoord; }
                if (dir.col == -1 && grid[nCoord].east && grid[current].west) { return nCoord; }
            }
            throw new Exception("No valid out");
        }

        public static int Result_A()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty().Select(l=>l.ToCharArray()).ToList();
            var rows = lines.Count();
            var columns = lines[0].Count();

            var grid = new Grid<Node>(columns, rows);
            Coord startCoord = new Coord();
            for(int c = 0; c<columns;c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    grid[new Coord(c, r)] = new Node(lines[r][c]);

                    if(lines[r][c]=='S')
                    {
                        startCoord = new Coord(c, r);
                        OutputHelper.Print("Start at " + c + ":" + r);
                    }
                }
            }

            foreach(var nCoord in grid.NeighbourCoords(startCoord, false, false))
            {
                var dir = nCoord - startCoord;
                if (dir.row == -1 && grid[nCoord].south) { grid[startCoord].north = true; }
                if (dir.row == 1 && grid[nCoord].north) { grid[startCoord].south = true; }

                if (dir.col == 1 && grid[nCoord].west) { grid[startCoord].east = true; }
                if (dir.col == -1 && grid[nCoord].east) { grid[startCoord].west = true; }
            }

            Coord current = startCoord;
            Coord last = startCoord;
            var timeStart = DateTime.Now;
            int loopIndex = 0;
            while (true)
            {
                var temp = current;
                current = Next(grid, current, last);
                last = temp;
                loopIndex++;
                if (grid[current].start) { break; }
            }

           
            var timeTotal = DateTime.Now - timeStart;
            OutputHelper.Print("Took seconds: " + timeTotal.TotalSeconds);

            return  (loopIndex+1)/2;
        }

        public static int Result_B()
        {
            var lines = InputHelper.ReadAllLinesUntilEmpty().Select(l => l.ToCharArray()).ToList();
            var rows = lines.Count();
            var columns = lines[0].Count();

            var grid = new Grid<Node>(columns, rows);
            Coord startCoord = new Coord();
            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    grid[new Coord(c, r)] = new Node(lines[r][c]);

                    if (lines[r][c] == 'S')
                    {
                        startCoord = new Coord(c, r);
                        OutputHelper.Print("Start at " + c + ":" + r);
                    }
                }
            }

            //90 129 (88 128)
            foreach (var nCoord in grid.NeighbourCoords(startCoord, false, false))
            {
                var dir = nCoord - startCoord;
                if (dir.row == -1 && grid[nCoord].south) { grid[startCoord].north = true; }
                if (dir.row == 1 && grid[nCoord].north) { grid[startCoord].south = true; }

                if (dir.col == 1 && grid[nCoord].west) { grid[startCoord].east = true; }
                if (dir.col == -1 && grid[nCoord].east) { grid[startCoord].west = true; }
            }

            Coord current = startCoord;
            Coord last = startCoord;
            int loopIndex = 0;
            OutputHelper.Print(grid[last].c + " > " + grid[current].c);
            while (true)
            {
                var temp = current;
                current = Next(grid, current, last);
                last = temp;
                OutputHelper.Print(grid[last].c + " > " + grid[current].c);
                loopIndex++;
                grid[current].isLoop = true;
                if (grid[current].start) { break; }
            }

            int found = 0;
            for (int r = 0; r < rows; r++)
            {
                bool inside = false;
                int up = 0;
                string toPrint = "";
                for (int c = 0; c < columns; c++)
                {
                    var coord = new Coord(c, r);

                    if (grid[coord].isLoop) 
                    {
                        toPrint += grid[coord].c;

                        if (grid[coord].north) { up++; }
                        if (grid[coord].south) { up--; }

                        if(up == 0)
                        {
                            inside = !inside;
                        }

                        if((up + 2) %2 == 0)
                        { up = 0; }
                    }
                    else 
                    {
                        if (inside)
                        { 
                            found++;
                            toPrint += 'X';
                        }
                        else
                        {
                            toPrint += 'O';
                        }
                    }
                }
                OutputHelper.Print(toPrint);
            }

            return found;
        }
    }
}