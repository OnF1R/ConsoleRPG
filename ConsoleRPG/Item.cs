using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;
using ConsoleRPG.Items.ItemsComponents;
using Spectre.Console;
using ConsoleRPG.Spells.SpellsComponents;

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
        public bool IsEquapable { get; set; }
        public bool IsEquiped { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }

        public Item AddComponent(ItemComponent component)
        {
            _components.Add(component);
            return this;
        }

        public T GetComponent<T>()
        {
            return (T)_components.OfType<T>().FirstOrDefault();
        }

        public void _Get(Item item)
        {
            foreach (var component in _components)
            {
                Console.WriteLine(component.ToString());
            }

            foreach (DamageTypes Type in (DamageTypes[])Enum.GetValues(typeof(DamageTypes)))
            {
            }
        }

        private List<ItemComponent> _components = new List<ItemComponent>();

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
            DamageTypes DamageType = new DamageTypesNames().GetRandomElementalDamageType();

            if (DamageType == DamageTypes.Fire)
            {
                item.AddComponent(new ElementalResistance { Fire = resist });
            }
            else if (DamageType == DamageTypes.Electric)
            {
                item.AddComponent(new ElementalResistance { Electric = resist });
            }
            else if(DamageType == DamageTypes.Nature)
            {
                item.AddComponent(new ElementalResistance { Nature = resist });
            }
            else if(DamageType == DamageTypes.Frost)
            {
                item.AddComponent(new ElementalResistance { Frost = resist });
            }
            else if(DamageType == DamageTypes.Water)
            {
                item.AddComponent(new ElementalResistance { Water = resist });
            }
            else if(DamageType == DamageTypes.Earth)
            {
                item.AddComponent(new ElementalResistance { Earth = resist });
            }
            else if(DamageType == DamageTypes.Wind)
            {
                item.AddComponent(new ElementalResistance { Wind = resist });
            }
            else if(DamageType == DamageTypes.Dark)
            {
                item.AddComponent(new ElementalResistance { Holy = resist });
            }
            else if(DamageType == DamageTypes.Holy)
            {
                item.AddComponent(new ElementalResistance { Holy = resist });
            }
            else if(DamageType == DamageTypes.Poison)
            {
                item.AddComponent(new ElementalResistance { Poison = resist });
            }
            else if(DamageType == DamageTypes.Necrotic)
            {
                item.AddComponent(new ElementalResistance { Necrotic = resist });
            }
            else if(DamageType == DamageTypes.Psychic)
            {
                item.AddComponent(new ElementalResistance { Psychic = resist });
            }
            else if(DamageType == DamageTypes.Arcane)
            {
                item.AddComponent(new ElementalResistance { Arcane = resist });
            }
        }

        public void AddNeededElementalTypesDamage(Item item, int minDmg, int maxDmg)
        {
            if (item.GetComponent<DamageType>().Type == DamageTypes.Fire)
            {
                item.AddComponent(new ElementalDamage { Fire = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Electric)
            {
                item.AddComponent(new ElementalDamage { Electric = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Nature)
            {
                item.AddComponent(new ElementalDamage { Nature = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Frost)
            {
                item.AddComponent(new ElementalDamage { Frost = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Water)
            {
                item.AddComponent(new ElementalDamage { Water = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Earth)
            {
                item.AddComponent(new ElementalDamage { Earth = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Wind)
            {
                item.AddComponent(new ElementalDamage { Wind = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Dark)
            {
                item.AddComponent(new ElementalDamage { Dark = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Holy)
            {
                item.AddComponent(new ElementalDamage { Holy = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Poison)
            {
                item.AddComponent(new ElementalDamage { Poison = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Necrotic)
            {
                item.AddComponent(new ElementalDamage { Necrotic = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Psychic)
            {
                item.AddComponent(new ElementalDamage { Psychic = new Random().Next(minDmg, maxDmg) });
            }
            if (item.GetComponent<DamageType>().Type == DamageTypes.Arcane)
            {
                item.AddComponent(new ElementalDamage { Arcane = new Random().Next(minDmg, maxDmg) });
            }
        }

        public void ItemInfo(Item item)
        {
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
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Fire, new DamageTypesNames().Names[DamageTypes.Fire]);
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
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Electric, new DamageTypesNames().Names[DamageTypes.Electric]);
            }

            if (
                DamageType == DamageTypes.Nature
                || DamageType == DamageTypes.Flame
                || DamageType == DamageTypes.Load)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Nature, new DamageTypesNames().Names[DamageTypes.Nature]);
            }

            if (
                DamageType == DamageTypes.Frost
                || DamageType == DamageTypes.Melting
                || DamageType == DamageTypes.Icecharge)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Frost, new DamageTypesNames().Names[DamageTypes.Frost]);
            }

            if (
                DamageType == DamageTypes.Water
                || DamageType == DamageTypes.Fume
                || DamageType == DamageTypes.Shock)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Water, new DamageTypesNames().Names[DamageTypes.Water]);
            }

            if (
                DamageType == DamageTypes.Earth
                || DamageType == DamageTypes.Meteor
                || DamageType == DamageTypes.Stream)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Earth, new DamageTypesNames().Names[DamageTypes.Earth]);
            }

            if (
                DamageType == DamageTypes.Wind
                || DamageType == DamageTypes.Fireblast
                || DamageType == DamageTypes.Storm)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Wind, new DamageTypesNames().Names[DamageTypes.Wind]);
            }

            if (
                DamageType == DamageTypes.Dark
                || DamageType == DamageTypes.Chaos)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Dark, new DamageTypesNames().Names[DamageTypes.Dark]);
            }

            if (
                DamageType == DamageTypes.Holy)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Holy, new DamageTypesNames().Names[DamageTypes.Holy]);
            }

            if (
                DamageType == DamageTypes.Poison)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Poison, new DamageTypesNames().Names[DamageTypes.Poison]);
            }

            if (
                DamageType == DamageTypes.Necrotic)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Necrotic, new DamageTypesNames().Names[DamageTypes.Necrotic]);
            }

            if (
                DamageType == DamageTypes.Psychic)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Psychic, new DamageTypesNames().Names[DamageTypes.Psychic]);
            }

            if (
                DamageType == DamageTypes.Arcane)
            {
                if (item.GetComponent<ElementalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<ElementalDamage>().Arcane, new DamageTypesNames().Names[DamageTypes.Arcane]);
            }

            if (
                DamageType == DamageTypes.Physical
                || DamageType == DamageTypes.Burst)
            {

                if (item.GetComponent<PhysicalDamage>() != null)
                    AnsiConsole.Markup("+{0} урона ({1}) ", item.GetComponent<PhysicalDamage>().Physical, new DamageTypesNames().Names[DamageTypes.Physical]);
            }

            if (item.GetComponent<Defence>() != null)
            {
                AnsiConsole.Markup("+{0} к [bold]броне[/] ", item.GetComponent<Defence>().ArmorPoints);
            }

            if (item.GetComponent<ElementalResistance>() != null)
            {
                if (item.GetComponent<ElementalResistance>().Fire != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Fire > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ", 
                                item.GetComponent<ElementalResistance>().Fire, new DamageTypesNames().Names[DamageTypes.Fire]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ", 
                                item.GetComponent<ElementalResistance>().Fire, new DamageTypesNames().Names[DamageTypes.Fire]);
                        }
                    }
                }

                if (item.GetComponent<ElementalResistance>().Electric != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Electric > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Electric, new DamageTypesNames().Names[DamageTypes.Electric]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Electric, new DamageTypesNames().Names[DamageTypes.Electric]);
                        }
                    }
                }
                if (item.GetComponent<ElementalResistance>().Nature != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Nature > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Nature, new DamageTypesNames().Names[DamageTypes.Nature]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Nature, new DamageTypesNames().Names[DamageTypes.Nature]);
                        }
                    }
                }
                if (item.GetComponent<ElementalResistance>().Frost != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Frost > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Frost, new DamageTypesNames().Names[DamageTypes.Frost]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Frost, new DamageTypesNames().Names[DamageTypes.Frost]);
                        }
                    }
                }
                if (item.GetComponent<ElementalResistance>().Water != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Water > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Water, new DamageTypesNames().Names[DamageTypes.Water]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Water, new DamageTypesNames().Names[DamageTypes.Water]);
                        }
                    }
                }
                if (item.GetComponent<ElementalResistance>().Earth != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Earth > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Earth, new DamageTypesNames().Names[DamageTypes.Earth]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Earth, new DamageTypesNames().Names[DamageTypes.Earth]);
                        }
                    }
                }
                if (item.GetComponent<ElementalResistance>().Wind != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Wind > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Wind, new DamageTypesNames().Names[DamageTypes.Wind]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Wind, new DamageTypesNames().Names[DamageTypes.Wind]);
                        }
                    }
                }
                if (item.GetComponent<ElementalResistance>().Dark != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Dark > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Dark, new DamageTypesNames().Names[DamageTypes.Dark]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Dark, new DamageTypesNames().Names[DamageTypes.Dark]);
                        }
                    }
                }
                if (item.GetComponent<ElementalResistance>().Holy != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Holy > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Holy, new DamageTypesNames().Names[DamageTypes.Holy]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Holy, new DamageTypesNames().Names[DamageTypes.Holy]);
                        }
                    }
                }
                if (item.GetComponent<ElementalResistance>().Poison != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Poison > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Poison, new DamageTypesNames().Names[DamageTypes.Poison]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Poison, new DamageTypesNames().Names[DamageTypes.Poison]);
                        }
                    }
                }
                if (item.GetComponent<ElementalResistance>().Necrotic != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Necrotic > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Necrotic, new DamageTypesNames().Names[DamageTypes.Necrotic]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Necrotic, new DamageTypesNames().Names[DamageTypes.Necrotic]);
                        }
                    }
                }
                if (item.GetComponent<ElementalResistance>().Psychic != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Psychic > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Psychic, new DamageTypesNames().Names[DamageTypes.Psychic]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Psychic, new DamageTypesNames().Names[DamageTypes.Psychic]);
                        }
                    }
                }
                if (item.GetComponent<ElementalResistance>().Arcane != 0)
                {
                    if (item.GetComponent<ElementalResistance>().Arcane > 0)
                    {
                        {
                            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Arcane, new DamageTypesNames().Names[DamageTypes.Arcane]);
                        }
                    }
                    else
                    {
                        {
                            AnsiConsole.Markup("{0}% сопротивления к ({1}) ",
                                item.GetComponent<ElementalResistance>().Arcane, new DamageTypesNames().Names[DamageTypes.Arcane]);
                        }
                    }
                }
            }

            if (item.GetComponent<Criticals>() != null)
            {
                if (item.GetComponent<Criticals>().CritChance > 0)
                {
                    AnsiConsole.Markup("+{0} к шансу крит. удара", item.GetComponent<Criticals>().CritChance);
                }
                if (item.GetComponent<Criticals>().CritDamage > 0)
                {
                    AnsiConsole.Markup("+{0} к множителю крит. удара", item.GetComponent<Criticals>().CritDamage);
                }
            }

            Console.WriteLine();
        }
    }
}
