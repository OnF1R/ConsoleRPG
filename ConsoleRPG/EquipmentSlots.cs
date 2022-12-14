using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    internal class EquipmentSlots
    {
        public Dictionary<EquipmentSlot, string> Names = new Dictionary<EquipmentSlot, string>();

        public EquipmentSlots()
        {
            Names.Add(EquipmentSlot.LeftHand, "Левая рука");
            Names.Add(EquipmentSlot.RightHand, "Правая рука");
            Names.Add(EquipmentSlot.Helmet, "Шлем");
            Names.Add(EquipmentSlot.Chest, "Нагрудник");
            Names.Add(EquipmentSlot.Gloves, "Перчатки");
            Names.Add(EquipmentSlot.Leggs, "Поножи");
            Names.Add(EquipmentSlot.Boots, "Сапоги");
            Names.Add(EquipmentSlot.FirstRing, "Кольцо");
            Names.Add(EquipmentSlot.SecondRing, "Кольцо");
            Names.Add(EquipmentSlot.Cape, "Плащ");
            Names.Add(EquipmentSlot.Trinket, "Аксессуар");
        }
    }

    public enum EquipmentSlot
    {
        LeftHand,
        RightHand,
        Helmet,
        Chest,
        Gloves,
        Leggs,
        Boots,
        FirstRing,
        SecondRing,
        Cape,
        Trinket,
    }
}
