using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattleBL
{
    public class Deck : Cell
    {
        private bool _state;    // True - Dead, False - Alive

        private Ship _ownerShip;

        public Deck(Coordinate cordDeck, Ship owner)
        {
            _location = cordDeck;
            _state = false;
            _ownerShip = owner;
        }

        /// <summary>
        /// Returns the state of the deck(True - Dead, False - Alive).
        /// </summary>
        public bool State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }
    }
}