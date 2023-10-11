using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBL
{
    public class HitingTheSamePointExeption : Exception
    {
        public HitingTheSamePointExeption()
        {

        }

        public HitingTheSamePointExeption(string message) : base(message)
        {

        }

        public HitingTheSamePointExeption(string message, Exception innerException)
                : base(message, innerException)
        {

        }
    }
}
