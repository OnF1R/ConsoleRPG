
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.StacableItems
{
    internal class RainbowShard : StacableItem
    {
        public RainbowShard(int level = 1) : base(level)
        {
            Name = "[red]Р[/][orange1]а[/][yellow1]д[/][green1]у[/][deepskyblue1]ж[/][blue]н[/][purple]ы[/][red]й[/] осколок";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 1 });
            RarityId = 1;
            Level = 1;
            DropChance = 8f;
            Count = new Random().Next(1, 5);

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.RainbowShard;
            SetRarity(RarityId);
        }
    }
}
