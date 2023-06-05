
namespace ConsoleRPG.Items.StacableItems
{
    internal class OceanRune : StacableItem
    {
        public OceanRune(int level = 1) : base(level)
        {
            Name = "[bold]Руна[/] [dodgerblue1]Океана[/]";
            Type = ItemType.Stacable;
            AddComponent(new ValueCharacteristic { Cost = 2 });
            RarityId = 1;
            Level = 1;
            DropChance = 6f;
            Count = new Random().Next(1, 3);

            IsStacable = true;
            IsEquapable = false;
            IsEquiped = false;
            ID = ItemIdentifier.OceanRune;
            SetRarity(RarityId);
        }
    }
}
