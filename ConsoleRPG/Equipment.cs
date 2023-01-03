using ConsoleRPG.Items;
using ConsoleRPG.Items.Weapons;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG
{
    internal class Equipment
    {
        public Dictionary<EquipmentSlot, Item> Equip = new Dictionary<EquipmentSlot, Item>();
        public EquipmentSlots EquipmentSlotsNames = new EquipmentSlots();

        private static Dictionary<int, string> MenuChoises;
        private static Dictionary<int, string> ChangeSlotsChoises;

        public Equipment()
        {
            MenuChoises = new Dictionary<int, string>(); MenuChoises = GenerateMenuChoises();
            ChangeSlotsChoises = new Dictionary<int, string>(); ChangeSlotsChoises = GenerageChangeSlotsChoises();

            Equip.Add(EquipmentSlot.LeftHand, new NothingItem());
            Equip.Add(EquipmentSlot.RightHand, new NothingItem());
            Equip.Add(EquipmentSlot.Helmet, new NothingItem());
            Equip.Add(EquipmentSlot.Chest, new NothingItem());
            Equip.Add(EquipmentSlot.Gloves, new NothingItem());
            Equip.Add(EquipmentSlot.Leggs, new NothingItem());
            Equip.Add(EquipmentSlot.Boots, new NothingItem());
            Equip.Add(EquipmentSlot.FirstRing, new NothingItem());
            Equip.Add(EquipmentSlot.SecondRing, new NothingItem());
            Equip.Add(EquipmentSlot.Cape, new NothingItem());
            Equip.Add(EquipmentSlot.Trinket, new NothingItem());
        }

        private static Dictionary<int, string> GenerateMenuChoises()
        {
            MenuChoises.Add(1, "Посмотреть экипировку");
            MenuChoises.Add(2, "Изменить экипировку");
            MenuChoises.Add(3, "Выйти");

            return MenuChoises;
        }

        private static Dictionary<int, string> GenerageChangeSlotsChoises()
        {
            int index = 0;
            EquipmentSlots equipmentSlotsNames = new EquipmentSlots();

            foreach (EquipmentSlot slot in Enum.GetValues(typeof(EquipmentSlot)))
            {
                index++;
                ChangeSlotsChoises.Add(index, equipmentSlotsNames.Names[slot]);
            }

            return ChangeSlotsChoises;
        }

        public void ShowWearEquipment()
        {
            foreach (var Equip in Equip)
            {
                AnsiConsole.Markup("{0}: {1} ([{2}]{3}[/]): "
                    , EquipmentSlotsNames.Names[Equip.Key], Equip.Value.Name, Equip.Value.RarityColor, Equip.Value.Rarity);
                Equip.Value.ItemInfo(Equip.Value);
            }
        }

        public void WearEquip(Unit unit, Item Item, EquipmentSlot SlotName)
        {
            if (Equip.ContainsKey(SlotName))
            {
                Equip.TryGetValue(SlotName, out var equip);
                if (equip != null) TakeOffEquip(unit, SlotName);
                Item.IsEquiped = true;
                Equip[SlotName] = Item;
                UpdateStatsWhenWear(unit, Item);
            }
        }

        public void UpdateStatsWhenWear(Unit unit, Item item)
        {
            Dictionary<DamageTypes, string> damageTypes = new DamageTypesNames().Names;

            if (item.GetComponent<PhysicalDamageCharacteristic>() != null)
            {
                unit.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
            }

            if (item.GetComponent<ArmorCharacteristic>() != null)
            {

                unit.GetComponent<ArmorCharacteristic>().Armor += item.GetComponent<ArmorCharacteristic>().Armor;

            }

            if (item.GetComponent<ElementalDamageCharacteristic>() != null)
            {
                Dictionary<DamageTypes, int> elementalDamage = item.GetComponent<ElementalDamageCharacteristic>().ElemDamage;

                foreach (DamageTypes type in elementalDamage.Keys)
                {
                    unit.GetComponent<ElementalDamageCharacteristic>().ElemDamage[type] += elementalDamage.FirstOrDefault(x => x.Key == type).Value;
                }
            }

            if (item.GetComponent<ElementalResistanceCharacteristic>() != null)
            {
                Dictionary<DamageTypes, int> elementalResistance = item.GetComponent<ElementalResistanceCharacteristic>().ElemResistance;

                foreach (DamageTypes type in elementalResistance.Keys)
                {

                    unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[type]
                        += elementalResistance.FirstOrDefault(x => x.Key == type).Value;
                }
            }

            if (item.GetComponent<CriticalChanceCharacteristic>() != null)
            {
                unit.GetComponent<CriticalChanceCharacteristic>().CriticalChance += item.GetComponent<CriticalChanceCharacteristic>().CriticalChance;
            }

            if (item.GetComponent<CriticalDamageCharacteristic>() != null)
            {
                unit.GetComponent<CriticalDamageCharacteristic>().CriticalDamage += item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage;
            }
        }

        public void UpdateStatsWhenTakeOff(Unit unit, Item item)
        {
            Dictionary<DamageTypes, string> damageTypes = new DamageTypesNames().Names;

            if (item.GetComponent<PhysicalDamageCharacteristic>() != null)
            {
                if (item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage >= 0)
                {
                    unit.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage -= item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
                }
                else
                {
                    unit.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
                }
            }

            if (item.GetComponent<ArmorCharacteristic>() != null)
            {
                if (item.GetComponent<ArmorCharacteristic>().Armor >= 0)
                {
                    unit.GetComponent<ArmorCharacteristic>().Armor -= item.GetComponent<ArmorCharacteristic>().Armor;
                }
                else
                {
                    unit.GetComponent<ArmorCharacteristic>().Armor += item.GetComponent<ArmorCharacteristic>().Armor;
                }
            }

            if (item.GetComponent<ElementalDamageCharacteristic>() != null)
            {
                Dictionary<DamageTypes, int> elementalDamage = item.GetComponent<ElementalDamageCharacteristic>().ElemDamage;

                foreach (DamageTypes type in elementalDamage.Keys)
                {
                    if (elementalDamage.FirstOrDefault(x => x.Key == type).Value >= 0)
                    {
                        unit.GetComponent<ElementalDamageCharacteristic>().ElemDamage[type] -= elementalDamage.FirstOrDefault(x => x.Key == type).Value;
                    }
                    else
                    {
                        unit.GetComponent<ElementalDamageCharacteristic>().ElemDamage[type] += elementalDamage.FirstOrDefault(x => x.Key == type).Value;
                    }
                }
            }

            if (item.GetComponent<ElementalResistanceCharacteristic>() != null)
            {
                Dictionary<DamageTypes, int> elementalResistance = item.GetComponent<ElementalResistanceCharacteristic>().ElemResistance;

                foreach (DamageTypes type in elementalResistance.Keys)
                {
                    if (elementalResistance.FirstOrDefault(x => x.Key == type).Value >= 0)
                    {
                        unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[type]
                            -= elementalResistance.FirstOrDefault(x => x.Key == type).Value;
                    }
                    else
                    {
                        unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[type]
                            += elementalResistance.FirstOrDefault(x => x.Key == type).Value;
                    }
                }
            }

            if (item.GetComponent<CriticalChanceCharacteristic>() != null)
            {
                if (item.GetComponent<CriticalChanceCharacteristic>().CriticalChance >= 0)
                {
                    unit.GetComponent<CriticalChanceCharacteristic>().CriticalChance -= item.GetComponent<CriticalChanceCharacteristic>().CriticalChance;
                }
                else
                {
                    unit.GetComponent<CriticalChanceCharacteristic>().CriticalChance += item.GetComponent<CriticalChanceCharacteristic>().CriticalChance;
                }
            }

            if (item.GetComponent<CriticalDamageCharacteristic>() != null)
            {
                if (item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage > 0)
                {
                    unit.GetComponent<CriticalDamageCharacteristic>().CriticalDamage -= item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage;
                }
                else
                {
                    unit.GetComponent<CriticalDamageCharacteristic>().CriticalDamage += item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage;
                }
            }
        }

        public void TakeOffEquip(Unit unit, EquipmentSlot SlotName)
        {
            Equip[SlotName].IsEquiped = false;
            UpdateStatsWhenTakeOff(unit, Equip[SlotName]);
            Equip[SlotName] = new NothingItem();
        }

        public void TakeOffAllEquip(Unit unit)
        {
            if (Equip != null)
            {
                foreach (EquipmentSlot Slot in (EquipmentSlot[])Enum.GetValues(typeof(EquipmentSlot)))
                {
                    TakeOffEquip(unit, Slot);
                }
            }
        }

        public void EquipmentMenu(Player Player)
        {
            bool Loop = true;
            while (Loop)
            {
                var choise = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Что будете делать?[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(MenuChoises.Values));


                Console.Clear();

                switch (MenuChoises.FirstOrDefault(x => x.Value == choise).Key)
                {
                    case 1:
                        ShowWearEquipment();
                        break;
                    case 2:
                        ChangeEquipmentMenu(Player);
                        break;
                    default:
                        Loop = false;
                        break;
                }
            }
        }

        public void ChangeEquipment(Unit unit, EquipmentSlot SlotName, params ItemType[] ItemType)
        {
            List<Item> EquipableItems = new List<Item>();
            EquipableItems = unit.Inventory.SortInventoryForEquip(ItemType);
            Dictionary<int, string> itemsWithStats = new Dictionary<int, string>();

            if (EquipableItems.Count > 0)
            {
                for (int i = 0; i < EquipableItems.Count; i++)
                {
                    itemsWithStats.Add(i + 1, $"{EquipableItems[i].Name} ([{EquipableItems[i].RarityColor}]{EquipableItems[i].Rarity}[/]): {EquipableItems[i].ItemInfoString(EquipableItems[i])}");
                }

                var chosedEquipment = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Выберите предмет[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(itemsWithStats.Values));

                try
                {
                    int result = itemsWithStats.FirstOrDefault(x => x.Value == chosedEquipment).Key;
                    switch (result >= 1 && result <= EquipableItems.Count)
                    {
                        case true:
                            WearEquip(unit, EquipableItems[--result], SlotName);
                            break;
                        default:
                            Console.WriteLine("Не правильно выбран предмет");
                            break;
                    }
                }
                catch
                {
                    AnsiConsole.MarkupLine("Введите [red]число!!![/]");
                }

            }
            else
            {
                Console.WriteLine("Нет подходящей экипировки");
            }

        }

        public void ChangeEquipmentMenu(Unit unit)
        {
            ShowWearEquipment();
            var choise = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Какую экипировку хотите изменить?[/]")
                        .PageSize(11)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(ChangeSlotsChoises.Values));


            Console.Clear();

            switch (ChangeSlotsChoises.FirstOrDefault(x => x.Value == choise).Key)
            {
                case 1:
                    ChangeEquipment(unit, EquipmentSlot.LeftHand,
                        ItemType.Sword, ItemType.TwoHandedSword, ItemType.Axe, ItemType.TwoHandedAxe, ItemType.Bow, ItemType.Staff, ItemType.TwoHandenStaff, ItemType.Shield, ItemType.Dagger);
                    break;
                case 2:
                    ChangeEquipment(unit, EquipmentSlot.RightHand,
                        ItemType.Sword, ItemType.TwoHandedSword, ItemType.Axe, ItemType.TwoHandedAxe, ItemType.Bow, ItemType.Staff, ItemType.TwoHandenStaff, ItemType.Shield, ItemType.Dagger);
                    break;
                case 3:
                    ChangeEquipment(unit, EquipmentSlot.Helmet, ItemType.Helmet);
                    break;
                case 4:
                    ChangeEquipment(unit, EquipmentSlot.Chest, ItemType.Chest);
                    break;
                case 5:
                    ChangeEquipment(unit, EquipmentSlot.Gloves, ItemType.Gloves);
                    break;
                case 6:
                    ChangeEquipment(unit, EquipmentSlot.Leggs, ItemType.Leggs);
                    break;
                case 7:
                    ChangeEquipment(unit, EquipmentSlot.Boots, ItemType.Boots);
                    break;
                case 8:
                    ChangeEquipment(unit, EquipmentSlot.FirstRing, ItemType.Ring);
                    break;
                case 9:
                    ChangeEquipment(unit, EquipmentSlot.SecondRing, ItemType.Ring);
                    break;
                case 10:
                    ChangeEquipment(unit, EquipmentSlot.Cape, ItemType.Cape);
                    break;
                case 11:
                    ChangeEquipment(unit, EquipmentSlot.Trinket, ItemType.Trinket);
                    break;
                default:
                    break;
            }
        }
    }
}
