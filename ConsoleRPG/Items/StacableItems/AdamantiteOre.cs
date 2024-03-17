
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    [Serializable]
    internal class AdamantiteOre : StacableItem
    {
        public AdamantiteOre(int level = 1) : base(level)
        {
            Name = "[fuchsia]Адамантитовая[/] руда";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 4 });
            RarityId = 2;
            Level = 1;
            DropChance = 5f;
            Count = new SerializableRandom().Next(1, 4);

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.AdamantiteOre;
            SetRarity(RarityId);
        }
    }
}
