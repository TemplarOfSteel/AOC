using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Generics
{
    public struct Grid<T>
    {
        public T[] nodes;
        public int columns;
        public int rows;

        public Grid(int columns, int rows)
        {
            nodes = new T[columns * rows];
            this.columns = columns;
            this.rows = rows;
        }

        public T this[int key]
        {
            get => nodes[key];
            set => nodes[key] = value;
        }

        public T this[Coord coord]
        {
            get => nodes[Index(coord)];
            set => nodes[Index(coord)] = value;
        }

        public int Index(int column, int row)
        {
            return column + row * columns;
        }

        public int Index(Coord coord)
        {
            return coord.col + coord.row * columns;
        }

        public int Column(int index)
        {
            return index % columns;
        }

        public int Row(int index)
        {
            return index / columns;
        }

        public Coord Coord(int index)
        {
            return new Coord(Column(index), Row(index));
        }

        public int IndexWrapped(int column, int row)
        {
            int c = (column + columns) % columns;
            int r = (row + rows) % rows;
            return c + r * columns;
        }

        public bool ValidCoordinate(int column, int row)
        {
            return column >= 0 && row >= 0 && column < columns && row < rows;
        }

        public bool ValidCoordinate(Coord coord)
        {
            return coord.col >= 0 && coord.row >= 0 && coord.col < columns && coord.row < rows;
        }

        public bool ValidShape(Coord origin, GridShape shape)
        {
            foreach (var coord in shape.localCoordinates)
            {
                if (!ValidCoordinate(coord + origin))
                { return false; }
            }
            return true;
        }

        public List<int> ShapeIndexes(Coord origin, GridShape shape)
        {
            var ret = new List<int>();
            foreach (var coord in shape.localCoordinates)
            {
                ret.Add(Index(origin + coord));
            }
            return ret;
        }

        public List<T> ShapeObjects(Coord origin, GridShape shape)
        {
            var ret = new List<T>();
            foreach (var index in ShapeIndexes(origin, shape))
            {
                ret.Add(nodes[index]);
            }
            return ret;
        }

        public List<int> NeighbourIndexes(Coord coord, bool wrapEdges, bool includeDiagonals)
        {
            List<int> ret = new List<int>();

            if (wrapEdges)
            {
                ret.Add(IndexWrapped(coord.col - 1, coord.row));
                ret.Add(IndexWrapped(coord.col + 1, coord.row));
                ret.Add(IndexWrapped(coord.col, coord.row - 1));
                ret.Add(IndexWrapped(coord.col, coord.row + 1));

                if (includeDiagonals)
                {
                    ret.Add(IndexWrapped(coord.col - 1, coord.row - 1));
                    ret.Add(IndexWrapped(coord.col + 1, coord.row - 1));
                    ret.Add(IndexWrapped(coord.col - 1, coord.row + 1));
                    ret.Add(IndexWrapped(coord.col + 1, coord.row + 1));
                }
            }
            else
            {
                if (ValidCoordinate(coord.col - 1, coord.row)) { ret.Add(Index(coord.col - 1, coord.row)); }
                if (ValidCoordinate(coord.col + 1, coord.row)) { ret.Add(Index(coord.col + 1, coord.row)); }
                if (ValidCoordinate(coord.col, coord.row - 1)) { ret.Add(Index(coord.col, coord.row - 1)); }
                if (ValidCoordinate(coord.col, coord.row + 1)) { ret.Add(Index(coord.col, coord.row + 1)); }

                if (includeDiagonals)
                {
                    if (ValidCoordinate(coord.col - 1, coord.row - 1)) { ret.Add(Index(coord.col - 1, coord.row - 1)); }
                    if (ValidCoordinate(coord.col + 1, coord.row - 1)) { ret.Add(Index(coord.col + 1, coord.row - 1)); }
                    if (ValidCoordinate(coord.col - 1, coord.row + 1)) { ret.Add(Index(coord.col - 1, coord.row + 1)); }
                    if (ValidCoordinate(coord.col + 1, coord.row + 1)) { ret.Add(Index(coord.col + 1, coord.row + 1)); }
                }
            }

            return ret;
        }

        public List<Coord> NeighbourCoords(Coord coord, bool wrapEdges, bool includeDiagonals)
        {
            List<int> indexes = NeighbourIndexes(coord, wrapEdges, includeDiagonals);
            List<Coord> ret = new List<Coord>();
            for (int i = 0; i < indexes.Count; i++)
            {
                ret.Add(Coord(indexes[i]));
            }
            return ret;
        }

        public List<T> Neighbours(Coord coord, bool wrapEdges, bool includeDiagonals)
        {
            List<int> indexes = NeighbourIndexes(coord, wrapEdges, includeDiagonals);
            List<T> ret = new List<T>();
            for (int i = 0; i < indexes.Count; i++)
            {
                ret.Add(nodes[indexes[i]]);
            }
            return ret;
        }

        public List<Coord> AdjacentCoords(Coord origin, GridShape shape)
        {
            var ret = new List<Coord>();
            foreach (Coord localCoord in shape.localCoordinates)
            {
                ret.AddRange(NeighbourCoords(localCoord + origin, false, false));
            }
            ret = ret.Distinct().Except(shape.localCoordinates).ToList();
            return ret;
        }

        public List<T> Adjacent(Coord origin, GridShape shape)
        {
            var ret = new List<T>();
            foreach (Coord c in AdjacentCoords(origin, shape))
            {
                ret.Add(this[c]);
            }
            return ret;
        }

        public bool RotatedIndexes(List<int> indexes, int columnRotateAround, int rowotateAround, int rotationsClockwise, bool flip, out List<int> rotated)
        {
            rotated = new List<int>();
            foreach (var index in indexes)
            {
                int c = Column(index);
                int r = Row(index);

                c = c + (columnRotateAround - c) * 2; //FLIP

                for (int i = 0; i < rotationsClockwise % 4; i++)
                {
                    int temp = c;
                    c = r;
                    r = -temp;
                }

                if (ValidCoordinate(c, r))
                {
                    rotated.Add(Index(c, r));
                }
                else
                {
                    rotated = null;
                    return false;
                }
            }

            return true;
        }


    }
}
