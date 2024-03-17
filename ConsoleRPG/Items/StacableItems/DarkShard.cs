
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    [Serializable]
    internal class DarkShard : StacableItem
    {
        public DarkShard(int level = 1) : base(level)
        {
            Name = "[grey30]Тёмный[/] осколок";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 6 });
            RarityId = 3;
            Level = 1;
            DropChance = 3f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.DarkShard;
            SetRarity(RarityId);
        }
    }
}
