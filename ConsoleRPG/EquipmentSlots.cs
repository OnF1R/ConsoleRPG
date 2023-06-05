
namespace ConsoleRPG
{
    public static class EquipmentSlots
    {
        public static Dictionary<EquipmentSlot, string> Names = new Dictionary<EquipmentSlot, string>()
        {
            { EquipmentSlot.LeftHand, "Левая рука" },
            { EquipmentSlot.RightHand, "Правая рука" },
            { EquipmentSlot.Helmet, "Шлем" },
            { EquipmentSlot.Chest, "Нагрудник" },
            { EquipmentSlot.Gloves, "Перчатки" },
            { EquipmentSlot.Leggs, "Поножи" },
            { EquipmentSlot.Boots, "Сапоги" },
            { EquipmentSlot.FirstRing, "Кольцо" },
            { EquipmentSlot.SecondRing, "Кольцо" },
            { EquipmentSlot.Cape, "Плащ" },
            { EquipmentSlot.Trinket, "Аксессуар" },
        };
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
