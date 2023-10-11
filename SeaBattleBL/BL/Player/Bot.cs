using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattleBL
{
    public class Bot : Player
    {
        /// <summary>
        /// Generates a random coordinate for the bot.
        /// </summary>
        /// <returns>Random coordinate for the bot.</returns>
        public static Coordinate GetCoordinate()
        {
            int x = Randomizer.RandomizePositionShip();
            int y = Randomizer.RandomizePositionShip();

            return new Coordinate(x, y);
        }
    }
}