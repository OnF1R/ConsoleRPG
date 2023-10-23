
using ConsoleRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.StacableItems
{
    internal class BloodStone : StacableItem
    {
        public BloodStone(int level = 1) : base(level)
        {
            Name = "[red]Кровавый[/] камень";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 3 });
            RarityId = 1;
            Level = 1;
            DropChance = 8f;
            Count = new Random().Next(1, 3);

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.BloodStone;
            SetRarity(RarityId);
        }
    }
}
