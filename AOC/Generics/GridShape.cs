using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Generics
{
    public struct GridShape
    {
        public GridShape(List<Coord> shape)
        {
            localCoordinates = shape;
        }

        public List<Coord> localCoordinates;

        public void Flip()
        {
            var temp = new List<Coord>();
            foreach (var coord in localCoordinates)
            {
                temp.Add(new Coord(coord.col, -coord.row));
            }
            localCoordinates = temp;
        }

        public void Rotate(int rotationsClockwise)
        {
            for (int i = 0; i < rotationsClockwise % 4; i++)
            {
                var temp = new List<Coord>();
                foreach (var coord in localCoordinates)
                {
                    temp.Add(new Coord(coord.row, -coord.col));
                }
                localCoordinates = temp;
            }
        }
    }
}
