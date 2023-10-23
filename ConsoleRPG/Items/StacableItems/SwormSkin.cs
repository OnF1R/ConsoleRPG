
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    internal class SwormSkin : StacableItem
    {
        public SwormSkin(int level = 1) : base(level)
        {
            Name = "Шкура червя";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 13 });
            RarityId = 3;
            Level = 1;
            DropChance = 3f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.SwormSkin;
            SetRarity(RarityId);
        }
    }
}
