using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattleBL
{
    public abstract class Ship
    {
        protected Deck[] _decks;

        protected int _lenghtShip;

        /// <summary>
        /// Add Ship.
        /// </summary>
        /// <param name="decks">Transfer of decks of an added ship</param>
        /// <returns>A ship that will later be listed in the massif</returns>
        public static Ship CreateShip(params Deck[] decks)
        {
            Ship ship = null;

            switch (decks.Length)
            {
                case 1:
                    ship = new OneDeckShip(decks);

                    break;
                case 2:
                    ship = new TwoDeckShip(decks);

                    break;
                case 3:
                    ship = new ThreeDeckShip(decks);

                    break;
                case 4:
                    ship = new FourDeckShip(decks);

                    break;
            }

            return ship;
        }

        #region Prop

        // TODO: 3 использование абстрактного класса и полиморфное поведение.
        /// <summary>
        /// Returns the x coordinate of the origin of the ship.
        /// </summary>
        public abstract int XFirst { get; }

        /// <summary>
        /// Returns the y coordinate of the origin of the ship.
        /// </summary>
        public abstract int YFirst { get; }

        /// <summary>
        /// Returns the x coordinate of the end of the ship.
        /// </summary>
        public abstract int XLast { get; }

        /// <summary>
        /// Returns the y coordinate of the end of the ship.
        /// </summary>
        public abstract int YLast { get; }

        #endregion

        #region Check Ship

        // TODO: 3 + использование абстрактного метода.
        /// <summary>
        /// Checking if the ship is alive.
        /// </summary>
        /// <returns>Does the ship return lives.</returns>
        public abstract bool IsAlive();

        #endregion
    }
}