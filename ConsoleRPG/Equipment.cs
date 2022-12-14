using ConsoleRPG.Items;
using ConsoleRPG.Items.ItemsComponents;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Spells.SpellsComponents;
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

        public Equipment()
        {
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

        //public int GetAllResistance(Item Weapon)
        //{
        //    int resistance = 0;

        //    foreach (Item item in Equip.Values)
        //    {
        //        foreach (DamageTypes type in Enum.GetValues(typeof(DamageTypes)))
        //        {
        //            if (item.GetComponent<ElementalResistance>().type != null)
        //            {

        //            }
        //        }
        //    }
        //}

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

        public void ChangeEquipment(Inventory inv, EquipmentSlot SlotName, params ItemType[] ItemType)
        {
            List<Item> EquipableItems = new List<Item> { };
            EquipableItems = inv.SortInventoryForEquip(ItemType);
            if (EquipableItems.Count > 0)
            {
                int number = 0;
                foreach (var Item in EquipableItems)
                {
                    number++;
                    AnsiConsole.Markup("{0}. {1} ([{2}]{3}[/]): ", number, Item.Name, Item.RarityColor, Item.Rarity);
                    Item.ItemInfo(Item);
                }
                Console.Write("Выберите предмет: ");
                var ChosedEquipment = Convert.ToString(Console.ReadLine());
                try
                {
                    int.TryParse(ChosedEquipment, out int result);
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
            Console.WriteLine("Какую экипировку хотите изменить?");
            Console.Write("Выберите действие: ");
            switch (Convert.ToString(Console.ReadLine()))
            {
                case "1":
                    ChangeEquipment(inv, EquipmentSlot.LeftHand, 
                        ItemType.Sword, ItemType.TwoHandedSword, ItemType.Axe, ItemType.TwoHandedAxe, ItemType.Bow, ItemType.Staff, ItemType.TwoHandenStaff, ItemType.Shield, ItemType.Dagger);
                    break;
                case "2":
                    ChangeEquipment(inv, EquipmentSlot.RightHand,
                        ItemType.Sword, ItemType.TwoHandedSword, ItemType.Axe, ItemType.TwoHandedAxe, ItemType.Bow, ItemType.Staff, ItemType.TwoHandenStaff, ItemType.Shield, ItemType.Dagger);
                    break;
                case "3":
                    ChangeEquipment(inv, EquipmentSlot.Helmet, ItemType.Helmet);
                    break;
                case "4":
                    ChangeEquipment(inv, EquipmentSlot.Chest, ItemType.Chest);
                    break;
                case "5":
                    ChangeEquipment(inv, EquipmentSlot.Gloves, ItemType.Gloves);
                    break;
                case "6":
                    ChangeEquipment(inv, EquipmentSlot.Leggs, ItemType.Leggs);
                    break;
                case "7":
                    ChangeEquipment(inv, EquipmentSlot.Boots, ItemType.Boots);
                    break;
                case "8":
                    ChangeEquipment(inv, EquipmentSlot.FirstRing, ItemType.Ring);
                    break;
                case "9":
                    ChangeEquipment(inv, EquipmentSlot.SecondRing, ItemType.Ring);
                    break;
                case "10":
                    ChangeEquipment(inv, EquipmentSlot.Cape, ItemType.Cape);
                    break;
                case "11":
                    ChangeEquipment(inv, EquipmentSlot.Trinket, ItemType.Trinket);
                    break;
                default:
                    break;
            }
        }

        public int GetTypeResistance(Item Weapon)
        {
            int Resistance = 0;


            DamageTypes DamageType = DamageTypes.Pure;

            if (Weapon.GetComponent<DamageType>() != null)
            {
                DamageType = Weapon.GetComponent<DamageType>().Type;
            }


            if (
                DamageType == DamageTypes.Fire
                || DamageType == DamageTypes.Overcharge
                || DamageType == DamageTypes.Flame
                || DamageType == DamageTypes.Melting
                || DamageType == DamageTypes.Fume
                || DamageType == DamageTypes.Meteor
                || DamageType == DamageTypes.Fireblast
                || DamageType == DamageTypes.Burst)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Fire;
                }
            }

            if (
                DamageType == DamageTypes.Electric
                || DamageType == DamageTypes.Overcharge
                || DamageType == DamageTypes.Load
                || DamageType == DamageTypes.Icecharge
                || DamageType == DamageTypes.Shock
                || DamageType == DamageTypes.Stream
                || DamageType == DamageTypes.Storm)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Electric;
                }
            }

            if (
                DamageType == DamageTypes.Nature
                || DamageType == DamageTypes.Flame
                || DamageType == DamageTypes.Load)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Nature;
                }
            }

            if (
                DamageType == DamageTypes.Frost
                || DamageType == DamageTypes.Melting
                || DamageType == DamageTypes.Icecharge)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Frost;
                }
            }

            if (
                DamageType == DamageTypes.Water
                || DamageType == DamageTypes.Fume
                || DamageType == DamageTypes.Shock)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Water;
                }
            }

            if (
                DamageType == DamageTypes.Earth
                || DamageType == DamageTypes.Meteor
                || DamageType == DamageTypes.Stream)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Earth;
                }
            }

            if (
                DamageType == DamageTypes.Wind
                || DamageType == DamageTypes.Fireblast
                || DamageType == DamageTypes.Storm)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Wind;
                }
            }

            if (
                DamageType == DamageTypes.Dark
                || DamageType == DamageTypes.Chaos)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Dark;
                }
            }

            if (
                DamageType == DamageTypes.Holy)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Holy;
                }
            }

            if (
                DamageType == DamageTypes.Poison)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Poison;
                }
            }

            if (
                DamageType == DamageTypes.Necrotic)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Necrotic;
                }
            }

            if (
                DamageType == DamageTypes.Psychic)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Psychic;
                }
            }

            if (
                DamageType == DamageTypes.Arcane)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Arcane;
                }
            }

            if (
                DamageType == DamageTypes.Physical
                || DamageType == DamageTypes.Burst)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<Defence>() != null)
                        Resistance += item.GetComponent<Defence>().ArmorPoints;
                }
            }

            return Resistance;
        }

        public int GetTypeResistance(Spell Spell)
        {
            int Resistance = 0;

            DamageTypes DamageType = DamageTypes.Pure;

            if (Spell.GetComponent<SpellDamageType>() != null)
            {
                DamageType = Spell.GetComponent<SpellDamageType>().Type;
            }

            if (
                DamageType == DamageTypes.Fire
                || DamageType == DamageTypes.Overcharge
                || DamageType == DamageTypes.Flame
                || DamageType == DamageTypes.Melting
                || DamageType == DamageTypes.Fume
                || DamageType == DamageTypes.Meteor
                || DamageType == DamageTypes.Fireblast
                || DamageType == DamageTypes.Burst)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Fire;
                }
            }

            if (
                DamageType == DamageTypes.Electric
                || DamageType == DamageTypes.Overcharge
                || DamageType == DamageTypes.Load
                || DamageType == DamageTypes.Icecharge
                || DamageType == DamageTypes.Shock
                || DamageType == DamageTypes.Stream
                || DamageType == DamageTypes.Storm)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Electric;
                }
            }

            if (
                DamageType == DamageTypes.Nature
                || DamageType == DamageTypes.Flame
                || DamageType == DamageTypes.Load)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Nature;
                }
            }

            if (
                DamageType == DamageTypes.Frost
                || DamageType == DamageTypes.Melting
                || DamageType == DamageTypes.Icecharge)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Frost;
                }
            }

            if (
                DamageType == DamageTypes.Water
                || DamageType == DamageTypes.Fume
                || DamageType == DamageTypes.Shock)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Water;
                }
            }

            if (
                DamageType == DamageTypes.Earth
                || DamageType == DamageTypes.Meteor
                || DamageType == DamageTypes.Stream)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Earth;
                }
            }

            if (
                DamageType == DamageTypes.Wind
                || DamageType == DamageTypes.Fireblast
                || DamageType == DamageTypes.Storm)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Wind;
                }
            }

            if (
                DamageType == DamageTypes.Dark
                || DamageType == DamageTypes.Chaos)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Dark;
                }
            }

            if (
                DamageType == DamageTypes.Holy)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Holy;
                }
            }

            if (
                DamageType == DamageTypes.Poison)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Poison;
                }
            }

            if (
                DamageType == DamageTypes.Necrotic)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Necrotic;
                }
            }

            if (
                DamageType == DamageTypes.Psychic)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Psychic;
                }
            }

            if (
                DamageType == DamageTypes.Arcane)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<ElementalResistance>() != null)
                        Resistance += item.GetComponent<ElementalResistance>().Arcane;
                }
            }

            if (
                DamageType == DamageTypes.Physical
                || DamageType == DamageTypes.Burst)
            {
                foreach (Item item in Equip.Values)
                {
                    if (item.GetComponent<Defence>() != null)
                        Resistance += item.GetComponent<Defence>().ArmorPoints;
                }
            }

            return Resistance;
        }

        public double GetCriticalChance()
        {
            double CriticalChance = 0;

            foreach (Item item in Equip.Values)
            {
                if (item.GetComponent<Criticals>() != null)
                {
                    CriticalChance += item.GetComponent<Criticals>().CritChance;
                }
            }

            return CriticalChance;
        }

        public double GetCriticalDamage()
        {
            double CriticalDamage = 0;

            foreach (Item item in Equip.Values)
            {
                if (item.GetComponent<Criticals>() != null)
                {
                    CriticalDamage += item.GetComponent<Criticals>().CritDamage;
                }
            }

            return CriticalDamage;
        }

        public int GetTypeDamage(Item item)
        {
            int Damage = 0;

            DamageTypes DamageType = DamageTypes.Abyss;

            if (item.GetComponent<DamageType>() != null)
            {
                DamageType = item.GetComponent<DamageType>().Type;
            }



            if (
                DamageType == DamageTypes.Fire
                || DamageType == DamageTypes.Overcharge
                || DamageType == DamageTypes.Flame
                || DamageType == DamageTypes.Melting
                || DamageType == DamageTypes.Fume
                || DamageType == DamageTypes.Meteor
                || DamageType == DamageTypes.Fireblast
                || DamageType == DamageTypes.Burst)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Fire;
            }

            if (
                DamageType == DamageTypes.Electric
                || DamageType == DamageTypes.Overcharge
                || DamageType == DamageTypes.Load
                || DamageType == DamageTypes.Icecharge
                || DamageType == DamageTypes.Shock
                || DamageType == DamageTypes.Stream
                || DamageType == DamageTypes.Storm)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Electric;
            }

            if (
                DamageType == DamageTypes.Nature
                || DamageType == DamageTypes.Flame
                || DamageType == DamageTypes.Load)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Nature;
            }

            if (
                DamageType == DamageTypes.Frost
                || DamageType == DamageTypes.Melting
                || DamageType == DamageTypes.Icecharge)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Frost;
            }

            if (
                DamageType == DamageTypes.Water
                || DamageType == DamageTypes.Fume
                || DamageType == DamageTypes.Shock)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Water;
            }

            if (
                DamageType == DamageTypes.Earth
                || DamageType == DamageTypes.Meteor
                || DamageType == DamageTypes.Stream)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Earth;
            }

            if (
                DamageType == DamageTypes.Wind
                || DamageType == DamageTypes.Fireblast
                || DamageType == DamageTypes.Storm)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Wind;
            }

            if (
                DamageType == DamageTypes.Dark
                || DamageType == DamageTypes.Chaos)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Dark;
            }

            if (
                DamageType == DamageTypes.Holy)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Holy;
            }

            if (
                DamageType == DamageTypes.Poison)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Poison;
            }

            if (
                DamageType == DamageTypes.Necrotic)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Necrotic;
            }

            if (
                DamageType == DamageTypes.Psychic)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Psychic;
            }

            if (
                DamageType == DamageTypes.Arcane)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    Damage += item.GetComponent<ElementalDamage>().Arcane;
            }

            if (
                DamageType == DamageTypes.Physical
                || DamageType == DamageTypes.Burst)
            {

                if (item.GetComponent<PhysicalDamage>() != null)
                    Damage += item.GetComponent<PhysicalDamage>().Physical;
            }

            return Damage;
        }

        public int GetTypeDamage(Spell spell)
        {
            int Damage = 0;

            DamageTypes DamageType = DamageTypes.Abyss;

            if (spell.GetComponent<SpellDamageType>() != null)
            {
                DamageType = spell.GetComponent<SpellDamageType>().Type;
            }



            if (
                DamageType == DamageTypes.Fire
                || DamageType == DamageTypes.Overcharge
                || DamageType == DamageTypes.Flame
                || DamageType == DamageTypes.Melting
                || DamageType == DamageTypes.Fume
                || DamageType == DamageTypes.Meteor
                || DamageType == DamageTypes.Fireblast
                || DamageType == DamageTypes.Burst)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Fire;
            }

            if (
                DamageType == DamageTypes.Electric
                || DamageType == DamageTypes.Overcharge
                || DamageType == DamageTypes.Load
                || DamageType == DamageTypes.Icecharge
                || DamageType == DamageTypes.Shock
                || DamageType == DamageTypes.Stream
                || DamageType == DamageTypes.Storm)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Electric;
            }

            if (
                DamageType == DamageTypes.Nature
                || DamageType == DamageTypes.Flame
                || DamageType == DamageTypes.Load)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Nature;
            }

            if (
                DamageType == DamageTypes.Frost
                || DamageType == DamageTypes.Melting
                || DamageType == DamageTypes.Icecharge)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Frost;
            }

            if (
                DamageType == DamageTypes.Water
                || DamageType == DamageTypes.Fume
                || DamageType == DamageTypes.Shock)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Water;
            }

            if (
                DamageType == DamageTypes.Earth
                || DamageType == DamageTypes.Meteor
                || DamageType == DamageTypes.Stream)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Earth;
            }

            if (
                DamageType == DamageTypes.Wind
                || DamageType == DamageTypes.Fireblast
                || DamageType == DamageTypes.Storm)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Wind;
            }

            if (
                DamageType == DamageTypes.Dark
                || DamageType == DamageTypes.Chaos)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Dark;
            }

            if (
                DamageType == DamageTypes.Holy)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Holy;
            }

            if (
                DamageType == DamageTypes.Poison)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Poison;
            }

            if (
                DamageType == DamageTypes.Necrotic)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Necrotic;
            }

            if (
                DamageType == DamageTypes.Psychic)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Psychic;
            }

            if (
                DamageType == DamageTypes.Arcane)
            {
                if (spell.GetComponent<SpellElementalDamage>() != null)
                    Damage += spell.GetComponent<SpellElementalDamage>().Arcane;
            }

            if (
                DamageType == DamageTypes.Physical
                || DamageType == DamageTypes.Burst)
            {

                if (spell.GetComponent<SpellPhysicalDamage>() != null)
                    Damage += spell.GetComponent<SpellPhysicalDamage>().Physical;
            }

            return Damage;
        }
    }
}
