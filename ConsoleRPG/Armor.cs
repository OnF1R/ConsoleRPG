using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    abstract internal class Armor : Item
    {
        public Armor()
        {
            IsEquapable = true;
            IsStacable = false;
            IsEquiped = false;
        }
    }
}
