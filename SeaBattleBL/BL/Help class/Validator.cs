using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeaBattleBL
{
    public class Validator
    {
        // TODO: 5 использование регулярных выражений.
        /// <summary>
        /// Checks the coordinate entered by the player.
        /// </summary>
        /// <param name="coordinate">The coordinate entered by the player.</param>
        /// <returns>Is the coordinate correct.</returns>
        public static bool CheckCoordinatePlayer(string coordinate)
        {
            bool enterCoordinate = false;

            Regex reg = new Regex("[A-Ja-j][1][0]|[A-Ja-j][1-9]$");

            MatchCollection match = reg.Matches(coordinate);

            if (coordinate.Length > 0 && coordinate.Length < 5 && match.Count == 1)
            {
                enterCoordinate = true;
            }

            return enterCoordinate;
        }
    }
}
