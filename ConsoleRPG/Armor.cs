﻿using ConsoleRPG.Items.Gems;

namespace ConsoleRPG
{
    [Serializable]
    abstract internal class Armor : Item
    {
        //public virtual Gem[] Gems { get; set; }

        public Armor(int level) : base(level)
        {
            IsEquapable = true;
            IsStacable = false;
            IsEquiped = false;
        }
    }
}
