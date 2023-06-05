
namespace ConsoleRPG.Items.StacableItems
{
    internal class AdamantiteIngot : StacableItem
    {
        public AdamantiteIngot(int level = 1) : base(level)
        {
            Name = "[fuchsia]Адамантитовый[/] слиток";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 15 });
            RarityId = 2;
            Level = 1;
            DropChance = 3f;
            Count = 1;

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.AdamantiteIngot;
            SetRarity(RarityId);
        }
    }
}
