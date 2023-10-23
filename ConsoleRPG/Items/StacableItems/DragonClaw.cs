
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    internal class DragonClaw : StacableItem
    {
        public DragonClaw(int level = 1) : base(level)
        {
            Name = "Коготь [bold]дракона[/]";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 6 });
            RarityId = 1;
            Level = 1;
            DropChance = 6f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.DragonClaw;
            SetRarity(RarityId);
        }
    }
}
