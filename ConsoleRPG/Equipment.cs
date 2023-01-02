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

        public void WearEquip(Item Item, EquipmentSlot SlotName)
        {
            if (Equip.ContainsKey(SlotName))
            {
                Equip.TryGetValue(SlotName, out var equip);
                if (equip != null) equip.IsEquiped = false;
                Item.IsEquiped = true;
                Equip[SlotName] = Item;
            }
        }

        public void TakeOffEquip(EquipmentSlot SlotName)
        {
            Equip[SlotName].IsEquiped = false;
            Equip[SlotName] = new NothingItem();
        }

        public void TakeOffAllEquip()
        {
            if (Equip != null)
            {
                foreach (EquipmentSlot Slot in (EquipmentSlot[])Enum.GetValues(typeof(EquipmentSlot)))
                {
                    TakeOffEquip(Slot);
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
                        ChangeEquipmentMenu(Player.Inventory);
                        break;
                    default:
                        Loop = false;
                        break;
                }
            }
        }

        public void ChangeEquipment(Inventory inv, EquipmentSlot SlotName, params ItemType[] ItemType)
        {
            List<Item> EquipableItems = new List<Item>();
            EquipableItems = inv.SortInventoryForEquip(ItemType);
            Dictionary<int, string> itemsWithStats = new Dictionary<int, string>();



            if (EquipableItems.Count > 0)
            {
                for (int i = 0; i < EquipableItems.Count; i++)
                {
                    itemsWithStats.Add(i+1, $"{EquipableItems[i].Name} [{EquipableItems[i].RarityColor}]{EquipableItems[i].Rarity}[/] {EquipableItems[i].ItemInfoString(EquipableItems[i])}");
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
                            WearEquip(EquipableItems[--result], SlotName);
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

        public void ChangeEquipmentMenu(Inventory inv)
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
                    ChangeEquipment(inv, EquipmentSlot.LeftHand,
                        ItemType.Sword, ItemType.TwoHandedSword, ItemType.Axe, ItemType.TwoHandedAxe, ItemType.Bow, ItemType.Staff, ItemType.TwoHandenStaff, ItemType.Shield, ItemType.Dagger);
                    break;
                case 2:
                    ChangeEquipment(inv, EquipmentSlot.RightHand,
                        ItemType.Sword, ItemType.TwoHandedSword, ItemType.Axe, ItemType.TwoHandedAxe, ItemType.Bow, ItemType.Staff, ItemType.TwoHandenStaff, ItemType.Shield, ItemType.Dagger);
                    break;
                case 3:
                    ChangeEquipment(inv, EquipmentSlot.Helmet, ItemType.Helmet);
                    break;
                case 4:
                    ChangeEquipment(inv, EquipmentSlot.Chest, ItemType.Chest);
                    break;
                case 5:
                    ChangeEquipment(inv, EquipmentSlot.Gloves, ItemType.Gloves);
                    break;
                case 6:
                    ChangeEquipment(inv, EquipmentSlot.Leggs, ItemType.Leggs);
                    break;
                case 7:
                    ChangeEquipment(inv, EquipmentSlot.Boots, ItemType.Boots);
                    break;
                case 8:
                    ChangeEquipment(inv, EquipmentSlot.FirstRing, ItemType.Ring);
                    break;
                case 9:
                    ChangeEquipment(inv, EquipmentSlot.SecondRing, ItemType.Ring);
                    break;
                case 10:
                    ChangeEquipment(inv, EquipmentSlot.Cape, ItemType.Cape);
                    break;
                case 11:
                    ChangeEquipment(inv, EquipmentSlot.Trinket, ItemType.Trinket);
                    break;
                default:
                    break;
            }
        }
    }
}
