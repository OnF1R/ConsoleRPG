
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    internal class DesertRune : StacableItem
    {
        public DesertRune(int level = 1) : base(level)
        {
            Name = "[orange1]Пустынная[/] руна";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 7 });
            RarityId = 2;
            Level = 1;
            DropChance = 3f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.DesertRune;
            SetRarity(RarityId);
        }
    }
}
