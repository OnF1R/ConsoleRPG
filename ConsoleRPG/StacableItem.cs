using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    abstract internal class StacableItem : Item
    {
        public StacableItem()
        {
            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
        }
    }
}
