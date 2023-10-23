
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    internal class SpiderLeg : StacableItem
    {
        public SpiderLeg(int level = 1) : base(level)
        {
            Name = "Паучья лапа";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 3 });
            RarityId = 2;
            Level = 1;
            DropChance = 4f;
            Count = new Random().Next(1, 3);

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.SpiderLeg;
            SetRarity(RarityId);
        }
    }
}
