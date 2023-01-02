using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleRPG.Items
{
    internal class NothingItem : Item
    {
        public NothingItem()
        {
            Name = "";
            Rarity = "Пустой слот";
            RarityColor = "white";
        }
    }
}
