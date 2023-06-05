
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.StacableItems
{
    internal class IronIngot : StacableItem
    {
        public IronIngot(int level = 1) : base(level)
        {
            Name = "[grey]Железный[/] слиток";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 2 });
            RarityId = 0;
            Level = 1;
            DropChance = 0f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.IronIngot;
            SetRarity(RarityId);
        }
    }
}
