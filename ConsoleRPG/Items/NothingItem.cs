using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRPG.Items.ItemsComponents;

namespace ConsoleRPG.Items
{
    internal class NothingItem : Item
    {
        public NothingItem()
        {
            Name = "";
            Rarity = "Пустой слот";
            RarityColor = "white";
            AddComponent(new DamageType { Type = DamageTypes.Physical });
            AddComponent(new DamageColor { Color = "white" });
        }
    }
}
