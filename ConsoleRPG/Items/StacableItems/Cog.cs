
namespace ConsoleRPG.Items.StacableItems
{
    internal class Cog : StacableItem
    {
        public Cog(int level = 1) : base(level)
        {
            Name = "Шестеренка";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 2 });
            RarityId = 1;
            Level = 1;
            DropChance = 11f;
            Count = new Random().Next(1, 3);

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.Cog;
            SetRarity(RarityId);
        }
    }
}
