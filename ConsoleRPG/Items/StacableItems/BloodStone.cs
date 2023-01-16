
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.StacableItems
{
    internal class BloodStone : StacableItem
    {
        public BloodStone()
        {
            Name = "[red]Кровавый[/] камень";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 3 });
            RarityId = 1;
            Level = 1;
            DropChance = 5f;
            Count = new Random().Next(1, 3);

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.BloodStone;
            SetRarity(RarityId);
        }
    }
}
