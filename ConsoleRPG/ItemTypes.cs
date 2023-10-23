using ConsoleRPG.Enums;

namespace ConsoleRPG
{
    internal class ItemTypes
    {
        public Dictionary<ItemType, string> Names = new Dictionary<ItemType, string>();

        public ItemTypes()
        {
            Names.Add(ItemType.Sword , "Меч");
            Names.Add(ItemType.TwoHandedSword, "Двуручный меч");
            Names.Add(ItemType.Axe, "Топор");
            Names.Add(ItemType.TwoHandedAxe, "Двуручный топор");
            Names.Add(ItemType.Dagger, "Кинжал");
            Names.Add(ItemType.Shield, "Щит");
            Names.Add(ItemType.Staff, "Посох");
            Names.Add(ItemType.TwoHandenStaff, "Двуручный посох");
            Names.Add(ItemType.Bow, "Лук");


            Names.Add(ItemType.Helmet, "Шлем");
            Names.Add(ItemType.Chest, "Нагрудник");
            Names.Add(ItemType.Gloves, "Перчатки");
            Names.Add(ItemType.Leggs, "Поножи");
            Names.Add(ItemType.Boots, "Сапоги");
            Names.Add(ItemType.Ring, "Кольцо");
            Names.Add(ItemType.Cape, "Плащ");
            Names.Add(ItemType.Trinket, "Аксессуар");
        }
    }
}
