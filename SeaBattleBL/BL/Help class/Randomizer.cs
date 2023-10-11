using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBL
{
    public class Randomizer
    {
        public static Random rnd = new Random();

        /// <summary>
        /// Generate random position for ship.
        /// </summary>
        /// <returns>Random ship position.</returns>
        public static int RandomizePositionShip()
        {
            return rnd.Next(0,10);
        }

        /// <summary>
        /// Generate random direction for ship.
        /// </summary>
        /// <returns>Random ship direction.</returns>
        public static int RandomizeDirectionShip()
        {
            return rnd.Next(0, 2);
        }

        /// <summary>
        /// Generate random fire queueu.
        /// </summary>
        /// <returns>Return random fire queue.</returns>
        public static int RandomizeQuequeFire()
        {
            return rnd.Next(0, 2);
        }
    }
}
