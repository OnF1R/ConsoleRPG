using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleRPG.Items
{
    [Serializable]
    internal class NothingItem : Item
    {
        public NothingItem(int level = 0) : base(level)
        {
            Name = "";
            Rarity = "Пустой слот";
            RarityColor = "white";
        }
    }
}
