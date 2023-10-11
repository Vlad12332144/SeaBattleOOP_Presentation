using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattleBL.BL.Interfaces
{
    public interface IFieldViev
    {
        int CountRow
        {
            get;
        }

        int CountColumn
        {
            get;
        }

        Cell this[int indexRow, int indexColumn]
        {
            get;
        }
    }
}
