using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    [Flags]
    public enum ItemType
    {
        Sword = 1 << 0,
        TwoHandedSword = 1 << 1,
        Axe = 1 << 2,
        TwoHandedAxe = 1 << 3,
        Dagger = 1 << 4,
        Shield = 1 << 5,
        Staff = 1 << 6,
        TwoHandenStaff = 1 << 7,
        Bow = 1 << 8,

        Damaging = Sword | TwoHandedSword | Axe | TwoHandedAxe | Dagger | Staff | TwoHandenStaff | Bow,

        Helmet = 1 << 9,
        Chest = 1 << 10,
        Gloves = 1 << 11,
        Leggs = 1 << 12,
        Boots = 1 << 13,
        Ring = 1 << 14,
        Cape = 1 << 15,
        Trinket = 1 << 16,

        Armor = Helmet | Chest | Gloves | Leggs | Boots | Ring | Cape | Trinket,

        Currency = 1 << 17,
        Stacable = 1 << 18,
        
    }
}
