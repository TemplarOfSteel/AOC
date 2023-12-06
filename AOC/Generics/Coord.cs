using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Generics
{
    public struct Coord
    {
        public int col;
        public int row;

        public Coord(int col, int row)
        {
            this.col = col;
            this.row = row;
        }

        public static Coord operator +(Coord a) => a;
        public static Coord operator -(Coord a) => new Coord(-a.col, -a.row);

        public static Coord operator +(Coord a, Coord b)
            => new Coord(a.col + b.col, a.row + b.row);

        public static Coord operator -(Coord a, Coord b)
            => a + (-b);

        public static bool operator ==(Coord a, Coord b)
        => a.col == b.col && a.row == b.row;

        public static bool operator !=(Coord a, Coord b)
        => !(a == b);

        public override bool Equals(object obj)
        {
            return obj is Coord coord &&
                   col == coord.col &&
                   row == coord.row;
        }

        public override int GetHashCode()
        {
            int hashCode = -1831622508;
            hashCode = hashCode * -1521134295 + col.GetHashCode();
            hashCode = hashCode * -1521134295 + row.GetHashCode();
            return hashCode;
        }

        public static Coord Normalized(Coord a)
        {
            return new Coord(Math.Clamp(a.col, -1, 1), Math.Clamp(a.row, -1, 1));
        }

        public override string ToString()
        {
            return "{" + col + ";" + row + "}";

        }
    }
}
