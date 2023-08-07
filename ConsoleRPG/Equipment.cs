using ConsoleRPG.Items;
using Spectre.Console;


namespace ConsoleRPG
{
    internal class Equipment
    {
        public Dictionary<EquipmentSlot, Item> Equip = new Dictionary<EquipmentSlot, Item>();

        private static Dictionary<int, string> menuChoises = new Dictionary<int, string>();
        private static Dictionary<int, string> changeSlotsChoises = new Dictionary<int, string>();

        public Equipment()
        {
            menuChoises = MenuChoises.EquipmentChoises();
            changeSlotsChoises = MenuChoises.EquipmentSlotsChoises();

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

        public void ShowWearEquipment()
        {
            foreach (var Equip in Equip)
            {
                AnsiConsole.Markup("{0}: {1} ([{2}]{3}[/]): "
                    , EquipmentSlots.Names[Equip.Key], Equip.Value.Name, Equip.Value.RarityColor, Equip.Value.Rarity);
                Equip.Value.ItemInfo(Equip.Value);
            }
        }

        public Item[] GetAllEquipmentWithStatusEffects()
        {
            List<Item> items = new List<Item>();
            foreach (var item in Equip.Values)
            {
                if (item.GetComponent<StatusEffectsCharacteristic>() != null)
                {
                    items.Add(item);
                }
            }
            return items.ToArray();
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

        public void TakeOffEquip(Unit unit, EquipmentSlot SlotName)
        {
            Equip[SlotName].IsEquiped = false;
            UpdateStatsWhenTakeOff(unit, Equip[SlotName]);
            Equip[SlotName] = new NothingItem();
        }

        public void UpdateStatsWhenWear(Unit unit, Item item)
        {
            Dictionary<DamageTypes, string> damageTypes = new DamageTypesNames().Names;

            if (item.GetComponent<PhysicalDamageCharacteristic>() != null)
            {
                unit.GetComponent<PhysicalDamageCharacteristic>().RealPhysicalDamage
                    += item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
                unit.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage
                    += item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
            }

            if (item.GetComponent<ArmorCharacteristic>() != null)
            {
                unit.GetComponent<ArmorCharacteristic>().RealArmor += item.GetComponent<ArmorCharacteristic>().Armor;
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

            if (item.GetComponent<MissCharacteristic>() != null)
            {
                unit.GetComponent<MissCharacteristic>().MissChance += item.GetComponent<MissCharacteristic>().MissChance;
            }

            if (item.GetComponent<EvasionCharacteristic>() != null)
            {
                unit.GetComponent<EvasionCharacteristic>().EvasionChance += item.GetComponent<EvasionCharacteristic>().EvasionChance;
            }

            if (item.GetComponent<CriticalChanceCharacteristic>() != null)
            {
                unit.GetComponent<CriticalChanceCharacteristic>().CriticalChance += item.GetComponent<CriticalChanceCharacteristic>().CriticalChance;
            }

            if (item.GetComponent<CriticalDamageCharacteristic>() != null)
            {
                unit.GetComponent<CriticalDamageCharacteristic>().CriticalDamage += item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage;
            }

            if (item.GetComponent<StrengthCharacteristic>() != null)
            {
                unit.GetComponent<StrengthCharacteristic>().ItemsStrength += item.GetComponent<StrengthCharacteristic>().Strength;
            }

            if (item.GetComponent<AgilityCharacteristic>() != null)
            {
                unit.GetComponent<AgilityCharacteristic>().ItemsAgility += item.GetComponent<AgilityCharacteristic>().Agility;
            }

            if (item.GetComponent<IntelligenceCharacteristic>() != null)
            {
                unit.GetComponent<IntelligenceCharacteristic>().ItemsIntelligence += item.GetComponent<IntelligenceCharacteristic>().Intelligence;
            }

            if (item.GetComponent<ExperienceBooster>() != null)
            {
                unit.GetComponent<ExperienceBooster>().PercentBoost += item.GetComponent<ExperienceBooster>().PercentBoost;
            }

            if (item.GetComponent<SpikeCharacteristic>() != null)
            {
                unit.GetComponent<SpikeCharacteristic>().SpikeDamage += item.GetComponent<SpikeCharacteristic>().SpikeDamage;
            }

            if (item.GetComponent<VampirismCharacteristic>() != null)
            {
                unit.GetComponent<VampirismCharacteristic>().VampirismPercent += item.GetComponent<VampirismCharacteristic>().VampirismPercent;
            }

            if (item.GetComponent<ParryCharacteristic>() != null)
            {
                unit.GetComponent<ParryCharacteristic>().ParryPercent += item.GetComponent<ParryCharacteristic>().ParryPercent;
            }
        }

        public void UpdateStatsWhenTakeOff(Unit unit, Item item)
        {
            Dictionary<DamageTypes, string> damageTypes = new DamageTypesNames().Names;

            if (item.GetComponent<PhysicalDamageCharacteristic>() != null)
            {
                unit.GetComponent<PhysicalDamageCharacteristic>().RealPhysicalDamage -=
                    item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
                unit.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage -=
                    item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
            }

            if (item.GetComponent<ArmorCharacteristic>() != null)
            {
                unit.GetComponent<ArmorCharacteristic>().RealArmor -= item.GetComponent<ArmorCharacteristic>().Armor;
                unit.GetComponent<ArmorCharacteristic>().Armor -= item.GetComponent<ArmorCharacteristic>().Armor;
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

            if (item.GetComponent<MissCharacteristic>() != null)
            {
                if (item.GetComponent<MissCharacteristic>().MissChance >= 0)
                {
                    unit.GetComponent<MissCharacteristic>().MissChance -= item.GetComponent<MissCharacteristic>().MissChance;
                }
                else
                {
                    unit.GetComponent<MissCharacteristic>().MissChance += item.GetComponent<MissCharacteristic>().MissChance;
                }
            }

            if (item.GetComponent<EvasionCharacteristic>() != null)
            {
                if (item.GetComponent<EvasionCharacteristic>().EvasionChance >= 0)
                {
                    unit.GetComponent<EvasionCharacteristic>().EvasionChance -= item.GetComponent<EvasionCharacteristic>().EvasionChance;
                }
                else
                {
                    unit.GetComponent<EvasionCharacteristic>().EvasionChance += item.GetComponent<EvasionCharacteristic>().EvasionChance;
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

            if (item.GetComponent<StrengthCharacteristic>() != null)
            {
                unit.GetComponent<StrengthCharacteristic>().ItemsStrength -= item.GetComponent<StrengthCharacteristic>().Strength;
            }

            if (item.GetComponent<AgilityCharacteristic>() != null)
            {
                unit.GetComponent<AgilityCharacteristic>().ItemsAgility -= item.GetComponent<AgilityCharacteristic>().Agility;
            }

            if (item.GetComponent<IntelligenceCharacteristic>() != null)
            {
                unit.GetComponent<IntelligenceCharacteristic>().ItemsIntelligence -= item.GetComponent<IntelligenceCharacteristic>().Intelligence;
            }

            if (item.GetComponent<ExperienceBooster>() != null)
            {
                unit.GetComponent<ExperienceBooster>().PercentBoost -= item.GetComponent<ExperienceBooster>().PercentBoost;
            }

            if (item.GetComponent<SpikeCharacteristic>() != null)
            {
                unit.GetComponent<SpikeCharacteristic>().SpikeDamage -= item.GetComponent<SpikeCharacteristic>().SpikeDamage;
            }

            if (item.GetComponent<VampirismCharacteristic>() != null)
            {
                unit.GetComponent<VampirismCharacteristic>().VampirismPercent -= item.GetComponent<VampirismCharacteristic>().VampirismPercent;
            }

            if (item.GetComponent<ParryCharacteristic>() != null)
            {
                unit.GetComponent<ParryCharacteristic>().ParryPercent -= item.GetComponent<ParryCharacteristic>().ParryPercent;
            }
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
                        .AddChoices(menuChoises.Values));


                Console.Clear();

                switch (menuChoises.FirstOrDefault(x => x.Value == choise).Key)
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
                        .AddChoices(changeSlotsChoises.Values));


            Console.Clear();

            switch (changeSlotsChoises.FirstOrDefault(x => x.Value == choise).Key)
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
