
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    internal class GargoyleClaw : StacableItem
    {
        public GargoyleClaw(int level = 1) : base(level)
        {
            Name = "Коготь гаргульи";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 11 });
            RarityId = 3;
            Level = 1;
            DropChance = 3f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.GargoyleClaw;
            SetRarity(RarityId);
        }
    }
}
