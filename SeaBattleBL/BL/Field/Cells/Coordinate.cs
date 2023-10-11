using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattleBL
{
    public struct Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int xInput, int yInput)
        {
            x = xInput;
            y = yInput;
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate coordinate &&
                   x == coordinate.x &&
                   y == coordinate.y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares two coordinates.
        /// </summary>
        /// <param name="a">First coordinate.</param>
        /// <param name="b">Second coordinate.</param>
        /// <returns>Are the coordinates the same.</returns>
        public static bool operator ==(Coordinate a, Coordinate b)
        {
            return a.x == b.x && a.y == b.y;
        }

        /// <summary>
        /// Compares two coordinates.
        /// </summary>
        /// <param name="a">First coordinate.</param>
        /// <param name="b">Second coordinate.</param>
        /// <returns>Are the coordinates the same.</returns>
        public static bool operator !=(Coordinate a, Coordinate b)
        {
            return !(a == b);
        }
    }
}