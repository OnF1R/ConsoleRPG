using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;
using Spectre.Console;

namespace ConsoleRPG
{
    abstract internal class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int RarityId { get; set; }
        public string Rarity { get; set; }
        public string RarityColor { get; set; }
        public int Level { get; set; }
        public double DropChance { get; set; }
        public bool IsStacable { get; set; }
        public ItemType Type { get; set; }
        public ItemIdentifier ID { get; set; }
        public bool IsEquapable { get; set; }
        public bool IsEquiped { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }

        public Item AddComponent(Characteristics component)
        {
            _components.Add(component);
            return this;
        }

        public T GetComponent<T>()
        {
            return (T)_components.OfType<T>().FirstOrDefault();
        }

        private List<Characteristics> _components = new List<Characteristics>();

        public void SetRarity(int RarityId)
        {
            switch (RarityId)
            {
                case 0: this.Rarity = "Обычное"; this.RarityColor = "white"; break;
                case 1: this.Rarity = "Необычное"; this.RarityColor = "lime"; break;
                case 2: this.Rarity = "Редкое"; this.RarityColor = "blue"; break;
                case 3: this.Rarity = "Эпическое"; this.RarityColor = "purple3"; break;
                case 4: this.Rarity = "Мифическое"; this.RarityColor = "darkred"; break;
                case 5: this.Rarity = "Легендарное"; this.RarityColor = "gold1"; break;
                case 9: this.Rarity = "Валюта"; this.RarityColor = "gold1"; break;
            }
        }

        public void AddRandomElementalResist(Item item, int resist)
        {
            DamageTypes damageType = new DamageTypesNames().GetRandomElementalDamageType();

            item.AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { damageType, resist },
            }));
        }

        public void AddNeededElementalTypesDamage(Item item, DamageTypes damageType, int minDmg, int maxDmg)
        {
            item.AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { damageType, new Random().Next(minDmg,maxDmg) },
            }));
        }

        public void ItemInfo(Item item)
        {
            Dictionary<DamageTypes, string> damageTypes = new DamageTypesNames().Names;

            if (item.GetComponent<PhysicalDamageCharacteristic>() != null)
            {
                if (item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage >= 0)
                {
                    AnsiConsole.Markup("+{0} к [bold]физическому урону[/] ", item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage);
                }
                else
                {
                    AnsiConsole.Markup("{0} к [bold]физическому урону[/] ", item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage);
                }
            }

            if (item.GetComponent<ArmorCharacteristic>() != null)
            {
                if (item.GetComponent<ArmorCharacteristic>().Armor >= 0)
                {
                    AnsiConsole.Markup("+{0} к [bold]броне[/] ", item.GetComponent<ArmorCharacteristic>().Armor);
                }
                else
                {
                    AnsiConsole.Markup("{0} к [bold]броне[/] ", item.GetComponent<ArmorCharacteristic>().Armor);
                }
            }

            if (item.GetComponent<ElementalDamageCharacteristic>() != null)
            {
                Dictionary<DamageTypes, int> elementalDamage = item.GetComponent<ElementalDamageCharacteristic>().ElemDamage;

                foreach (DamageTypes type in elementalDamage.Keys)
                {
                    if (elementalDamage.FirstOrDefault(x => x.Key == type).Value >= 0)
                    {
                        AnsiConsole.Markup("+{0} к урону ({1}) ", elementalDamage.FirstOrDefault(x => x.Key == type).Value, damageTypes[type]);
                    }
                    else
                    {
                        AnsiConsole.Markup("{0} к урону ({1}) ", elementalDamage.FirstOrDefault(x => x.Key == type).Value, damageTypes[type]);
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
                        AnsiConsole.Markup("+{0}% сопротивления к ({1}) ", elementalResistance.FirstOrDefault(x => x.Key == type).Value, damageTypes[type]);
                    }
                    else
                    {
                        AnsiConsole.Markup("{0}% сопротивления к ({1}) ", elementalResistance.FirstOrDefault(x => x.Key == type).Value, damageTypes[type]);
                    }
                }
            }

            if (item.GetComponent<CriticalChanceCharacteristic>() != null)
            {
                if (item.GetComponent<CriticalChanceCharacteristic>().CriticalChance >= 0)
                {
                    AnsiConsole.Markup("+{0} к шансу крит. удара ", item.GetComponent<CriticalChanceCharacteristic>().CriticalChance);
                }
                else
                {
                    AnsiConsole.Markup("{0} к шансу крит. удара ", item.GetComponent<CriticalChanceCharacteristic>().CriticalChance);
                }
            }

            if (item.GetComponent<CriticalDamageCharacteristic>() != null)
            {
                if (item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage > 0)
                {
                    AnsiConsole.Markup("+{0} к множителю крит. удара ", item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage);
                }
                else
                {
                    AnsiConsole.Markup("{0} к множителю крит. удара ", item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage);
                }
            }

            Console.WriteLine();
        }

        public string ItemInfoString(Item item)
        {
            string itemInfo = "";

            Dictionary<DamageTypes, string> damageTypes = new DamageTypesNames().Names;

            if (item.GetComponent<PhysicalDamageCharacteristic>() != null)
            {
                if (item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage >= 0)
                {
                    itemInfo= itemInfo + ($"+{item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage} к [bold]физическому урону[/] ");
                }
                else
                {
                    itemInfo = itemInfo + ($"{item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage} к [bold]физическому урону[/] ");
                }
            }

            if (item.GetComponent<ArmorCharacteristic>() != null)
            {
                if (item.GetComponent<ArmorCharacteristic>().Armor >= 0)
                {
                    itemInfo = itemInfo + ($"+{item.GetComponent<ArmorCharacteristic>().Armor} к [bold]броне[/] ");
                }
                else
                {
                    itemInfo = itemInfo + ($"{item.GetComponent<ArmorCharacteristic>().Armor} к [bold]броне[/] ");
                }
            }

            if (item.GetComponent<ElementalDamageCharacteristic>() != null)
            {
                Dictionary<DamageTypes, int> elementalDamage = item.GetComponent<ElementalDamageCharacteristic>().ElemDamage;

                foreach (DamageTypes type in elementalDamage.Keys)
                {
                    if (elementalDamage.FirstOrDefault(x => x.Key == type).Value >= 0)
                    {
                        itemInfo = itemInfo + ($"+{elementalDamage.FirstOrDefault(x => x.Key == type).Value} к урону ({damageTypes[type]}) ");
                    }
                    else
                    {
                        itemInfo = itemInfo + ($"{elementalDamage.FirstOrDefault(x => x.Key == type).Value} к урону ({damageTypes[type]}) ");
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
                        itemInfo = itemInfo + ($"+{elementalResistance.FirstOrDefault(x => x.Key == type).Value}% сопротивления к ({damageTypes[type]}) ");
                    }
                    else
                    {
                        itemInfo = itemInfo + ($"{elementalResistance.FirstOrDefault(x => x.Key == type).Value}% сопротивления к ({damageTypes[type]}) ");
                    }
                }
            }

            if (item.GetComponent<CriticalChanceCharacteristic>() != null)
            {
                if (item.GetComponent<CriticalChanceCharacteristic>().CriticalChance >= 0)
                {
                    itemInfo = itemInfo + ($"+{item.GetComponent<CriticalChanceCharacteristic>().CriticalChance} к шансу крит. удара ");
                }
                else
                {
                    itemInfo = itemInfo + ($"{item.GetComponent<CriticalChanceCharacteristic>().CriticalChance} к шансу крит. удара ");
                }
            }

            if (item.GetComponent<CriticalDamageCharacteristic>() != null)
            {
                if (item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage > 0)
                {
                    itemInfo = itemInfo + ($"+{item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage} к множителю крит. удара ");
                }
                else
                {
                    itemInfo = itemInfo + ($"{item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage} к множителю крит. удара ");
                }
            }

            return itemInfo;
        }
    }
}
