
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    internal class RainbowStone : StacableItem
    {
        public RainbowStone(int level = 1) : base(level)
        {
            Name = "[red]Р[/][orange1]а[/][yellow1]д[/][green1]у[/][deepskyblue1]ж[/][blue]н[/][purple]ы[/][red]й[/] камень";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 7 });
            RarityId = 2;
            Level = 1;
            DropChance = 5f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.RainbowStone;
            SetRarity(RarityId);
        }
    }
}
