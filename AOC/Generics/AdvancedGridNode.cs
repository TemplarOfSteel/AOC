using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AOC.Generics
{
    internal class GridConnectionNode
    {
        public bool south;
        public bool east;
        public bool north;
        public bool west;


        public GridConnectionNode(bool south, bool east, bool north, bool west)
        {
            this.south = south;
            this.north = north;
            this.east = east;
            this.west = west;
        }

        public int Connections()
        {
            var ret = 0;
            ret += south ? 1 : 0;
            ret += east ? 1 : 0;
            ret += north ? 1 : 0;
            ret += west ? 1 : 0;
            return ret;
        }

        public static List<Coord> ConnectedNeighbourCoords(Grid<GridConnectionNode> grid, Coord coord, bool wrapEdges, bool includeDiagonals)
        {
            var ret = new List<Coord>();
            var nCoords = grid.NeighbourCoords(coord, wrapEdges, includeDiagonals);
            foreach (var nCoord in nCoords)
            {
                var dir = nCoord - coord;

                if ((dir.row == 1 && grid[nCoord].north && grid[coord].south) ||
                    (dir.row == -1 && grid[nCoord].south && grid[coord].north) ||
                    (dir.col == 1 && grid[nCoord].west && grid[coord].east) ||
                    (dir.col == -1 && grid[nCoord].east && grid[coord].west) )
                { ret.Add(nCoord); }
            }
            return ret;
        }

        public static List<Coord> ConnectedCoords(Grid<GridConnectionNode> grid, Coord coord, bool wrapEdges, bool includeDiagonals)
        {
            List<Coord> ret = new List<Coord>() { coord };
            List<Coord> toCheck = new List<Coord>() { coord };
            while (toCheck.Count()>0)
            {
                var lastIndex = toCheck.Count() - 1;
                ret.Add(toCheck[lastIndex]);
                var newNodes = ConnectedNeighbourCoords(grid, toCheck[lastIndex], wrapEdges, includeDiagonals).Where(n => !ret.Contains(n));
                toCheck.AddRange(newNodes);
                toCheck.RemoveAt(lastIndex);
            }
            return ret;
        }

        public static List<Coord> ConnectedCoordsInLoop(Grid<GridConnectionNode> grid, Coord coord, bool wrapEdges, bool includeDiagonals)
        {
            List<Coord> ret = new List<Coord>() { coord };
            var current = coord;
            var last = coord;
            while (true)
            {
                var temp = current;
                current = NextCoordInLoop(grid, current, last, wrapEdges, includeDiagonals);
                ret.Add(current);
                last = temp;
                if (current == coord) { break; }
            }
            return ret;
        }

        private static Coord NextCoordInLoop(Grid<GridConnectionNode> grid, Coord current, Coord last, bool wrapEdges, bool includeDiagonals)
        {
            var nCoords = grid.NeighbourCoords(current, wrapEdges, includeDiagonals);
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

        public static List<Coord> InsideLoopCoords(Grid<GridConnectionNode> grid, List<Coord> loopNodes)
        {
            var ret = new List<Coord>();

            foreach(var loopNode in loopNodes)
            {
                if(grid[loopNode].Connections()!=2)
                {
                    throw new Exception("Node was not part of a loop, should have exactly 2 connections, had "+ grid[loopNode].Connections()+".");
                }
            }

            for (int r = 0; r < grid.rows; r++)
            {
                bool inside = false;
                int up = 0;
                for (int c = 0; c < grid.columns; c++)
                {
                    var coord = new Coord(c, r);

                    if (loopNodes.Contains(coord))
                    {
                        if (grid[coord].north) { up++; }
                        if (grid[coord].south) { up--; }

                        if (up == 0)
                        {
                            inside = !inside;
                        }

                        if ((up + 2) % 2 == 0)
                        { up = 0; }
                    }
                    else
                    {
                        if (inside)
                        {
                            ret.Add(coord);
                        }
                    }
                }
            }

            return ret;
        }
    }
}
