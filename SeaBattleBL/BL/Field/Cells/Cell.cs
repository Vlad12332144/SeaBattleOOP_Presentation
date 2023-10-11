using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattleBL
{
    public abstract class Cell
    {
        protected Coordinate _location;

        // TODO: 1 base class
        /// <summary>
        /// Return X coordinate Cell.
        /// </summary>
        public int X
        {
            get
            {
                return _location.x;
            }
        }

        /// <summary>
        /// Return Y coordinate Cell.
        /// </summary>
        public int Y
        {
            get
            {
                return _location.y;
            }
        }
    }
}