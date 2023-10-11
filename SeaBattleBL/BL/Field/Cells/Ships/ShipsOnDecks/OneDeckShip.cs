using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBL
{
    public class OneDeckShip : Ship
    {
        public OneDeckShip(params Deck[] decks)
        {
            _decks = new Deck[decks.Length];

            for (int i = 0; i < decks.Length; i++)
            {
                _decks[i] = decks[i];
            }

            _lenghtShip = decks.Length;
        }

        #region -==- Prop -==-

        /// <summary>
        /// Returns the x coordinate of the origin of the ship.
        /// </summary>
        public override int XFirst
        {
            get { return _decks[0].X; }
        }

        /// <summary>
        /// Returns the y coordinate of the origin of the ship.
        /// </summary>
        public override int YFirst
        {
            get { return _decks[0].Y; }
        }

        /// <summary>
        /// Returns the x coordinate of the end of the ship.
        /// </summary>
        public override int XLast
        {
            get { return _decks[0 + _lenghtShip - 1].X; }
        }

        /// <summary>
        /// Returns the y coordinate of the end of the ship.
        /// </summary>
        public override int YLast
        {
            get { return _decks[0 + _lenghtShip - 1].Y; }
        }

        #endregion

        /// <summary>
        /// Returns one of the ship's decks.
        /// </summary>
        /// <param name="index">Deck index in array.</param>
        /// <returns>Ship deck.</returns>
        public Deck this[int index]
        {
            get { return _decks[index]; }
        }

        /// <summary>
        /// Checking if the ship is alive.
        /// </summary>
        /// <returns>Does the ship return lives.</returns>
        public override bool IsAlive()
        {
            return _decks[0].State;
        }
    }
}
