using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    abstract internal class Weapon : Item
    {
        public Weapon(int level) : base(level)
        {
            this.IsStacable = false;
            this.IsEquapable = true;
        }
    }
}
