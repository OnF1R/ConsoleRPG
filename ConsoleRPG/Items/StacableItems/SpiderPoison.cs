
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    internal class SpiderPoison : StacableItem
    {
        public SpiderPoison(int level = 1) : base(level)
        {
            Name = "Паучий яд";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 12 });
            RarityId = 3;
            Level = 1;
            DropChance = 2f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.SpiderPoison;
            SetRarity(RarityId);
        }
    }
}
