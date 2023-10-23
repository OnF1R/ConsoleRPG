
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    internal class DarkStone : StacableItem
    {
        public DarkStone(int level = 1) : base(level)
        {
            Name = "[grey30]Тёмный[/] камень";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 33 });
            RarityId = 3;
            Level = 1;
            DropChance = 3f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.DarkStone;
            SetRarity(RarityId);
        }
    }
}
