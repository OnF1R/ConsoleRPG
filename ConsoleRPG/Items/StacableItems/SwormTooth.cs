
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    internal class SwormTooth : StacableItem
    {
        public SwormTooth(int level = 1) : base(level)
        {
            Name = "Зуб червя";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 5 });
            RarityId = 2;
            Level = 1;
            DropChance = 3f;
            Count = new Random().Next(1, 3);

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.SwormTooth;
            SetRarity(RarityId);
        }
    }
}
