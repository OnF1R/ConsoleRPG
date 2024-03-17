
using ConsoleRPG.Enums;

namespace ConsoleRPG.Items.StacableItems
{
    [Serializable]
    internal class EnchantedIceShard : StacableItem
    {
        public EnchantedIceShard(int level = 1) : base(level)
        {
            Name = "[purple4 slowblink]Зачарованный[/] [aqua]ледяной[/] осколок";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 18 });
            RarityId = 3;
            Level = 1;
            DropChance = 2f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.EnchantedIceShard;
            SetRarity(RarityId);
        }
    }
}
