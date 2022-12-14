using ConsoleRPG.Items.ItemsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.StacableItems
{
    internal class IronIngot : StacableItem
    {
        public IronIngot()
        {
            Name = "[grey]Железный[/] слиток";
            Type = ItemType.Stacable;
            AddComponent(new Valuable { Cost = 2 });
            RarityId = 0;
            Level = 1;
            DropChance = 0f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
