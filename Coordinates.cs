using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CramTetrisCalcUI
{
    public class Coordinates
    {
        /// <summary>
        /// Used for storing 3D coordinate data
        /// </summary>
        /// <param name="x">East/West</param>
        /// <param name="y">Up/Down</param>
        /// <param name="z">North/South</param>
        public Coordinates(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        /// <summary>
        /// Add x, y, and z values of two sets of coordinates
        /// </summary>
        /// <returns>Sum of two coordinates</returns>
        public static Coordinates AddCoordinates(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Coordinates North = new(0, 0, -1);
        public static Coordinates South = new(0, 0, 1);
        public static Coordinates East = new(1, 0, 0);
        public static Coordinates West = new(-1, 0, 0);
        public static Coordinates Up = new(0, 1, 0);
        public static Coordinates Down = new(0, -1, 0);
        public static Coordinates Self = new(0, 0, 0);

        public static Coordinates[] AllDirections =
        {
            North, South, East, West, Up, Down
        };
    }
}
