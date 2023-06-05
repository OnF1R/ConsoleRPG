using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    abstract internal class StacableItem : Item
    {
        public StacableItem(int level = 1) : base(level = 1)
        {
            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
        }
    }
}
