using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattleBL
{
    public class Shoot : Cell
    {
        public Shoot(Coordinate cordDeck)
        {
            _location = cordDeck;
        }
    }
}