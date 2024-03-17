
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    [Serializable]
    internal class GargoyleWing : StacableItem
    {
        public GargoyleWing(int level = 1) : base(level)
        {
            Name = "Крыло гаргульи";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 33 });
            RarityId = 4;
            Level = 1;
            DropChance = 1f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.GargoyleWing;
            SetRarity(RarityId);
        }
    }
}
