using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBL
{
    public class CellChangedEventArgs : EventArgs
    {
        public Coordinate Position
        {
            get; private set;
        }

        public Cell State
        {
            get; private set;
        }

        public CellChangedEventArgs(Coordinate position, Cell state)
        {
            Position = position;
            State = state;
        }
    }
}
