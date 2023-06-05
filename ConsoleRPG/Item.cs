using ConsoleRPG.Items.Enchants;
using Spectre.Console;

namespace ConsoleRPG
{
    abstract class Item
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
        public string UniqID { get; set; }

        public bool IsEquapable { get; set; }
        public bool IsEquiped { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }

        public int basicItemDamage = 1;
        public int basicItemArmor = 1;
        public int basicItemElementalResistance = 1;
        public int basicItemElementalDamage = 1;

        public Item(int level)
        {
            Level = level;
            UniqID = DateTime.Now.Ticks.ToString();
        }

        public Item AddEnchant(BaseEnchant enchant)
        {
            _enchants.Add(enchant);
            return this;
        }

        public Item AddComponent(Characteristics component)
        {
            _components.Add(component);
            return this;
        }

        public T GetComponent<T>()
        {
            return (T)_components.OfType<T>().FirstOrDefault();
        }

        public T GetEnchant<T>()
        {
            return (T)_enchants.OfType<T>().FirstOrDefault();
        }

        private List<Characteristics> _components = new List<Characteristics>();

        private List<BaseEnchant> _enchants = new List<BaseEnchant>();

        public void SetRarity(int RarityId)
        {
            switch (RarityId)
            {
                case 0: Rarity = "Обычное"; RarityColor = "white"; break;
                case 1: Rarity = "Необычное"; RarityColor = "lime"; break;
                case 2: Rarity = "Редкое"; RarityColor = "blue"; break;
                case 3: Rarity = "Эпическое"; RarityColor = "purple3"; break;
                case 4: Rarity = "Мифическое"; RarityColor = "darkred"; break;
                case 5: Rarity = "Легендарное"; RarityColor = "gold1"; break;
                case 6: Rarity = "Предмет босса"; RarityColor = "orangered1"; break;
                case 9: Rarity = "Валюта"; RarityColor = "gold1"; break;
            }
        }

        public void AddRandomElementalResist(Item item, int resist)
        {
            DamageTypes damageType = new DamageTypesNames().GetRandomElementalDamageType();

            if (item.GetComponent<ElementalResistanceCharacteristic>() != null && item.GetComponent<ElementalResistanceCharacteristic>().ElemResistance.ContainsKey(damageType))
            {
                item.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[damageType] += resist;
            }
            else
            {
                item.AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { damageType, resist },
                }));
            }
        }

        public void AddNeededElementalResistance(Item item, DamageTypes damageType, int minDmg, int maxDmg)
        {
            if (item.GetComponent<ElementalResistanceCharacteristic>() != null && item.GetComponent<ElementalResistanceCharacteristic>().ElemResistance.ContainsKey(damageType))
            {
                item.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[damageType] += new Random().Next(minDmg, maxDmg);
            }
            else
            {
                item.AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { damageType, new Random().Next(minDmg,maxDmg) },
                }));
            }
        }

        public void AddNeededElementalTypesDamage(Item item, DamageTypes damageType, int minDmg, int maxDmg)
        {
            if (item.GetComponent<ElementalDamageCharacteristic>() != null && item.GetComponent<ElementalDamageCharacteristic>().ElemDamage.ContainsKey(damageType))
            {
                item.GetComponent<ElementalDamageCharacteristic>().ElemDamage[damageType] += new Random().Next(minDmg, maxDmg);
            }
            else
            {
                item.AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { damageType, new Random().Next(minDmg,maxDmg) },
                }));
            }
        }

        public void ItemInfo(Item item)
        {
            AnsiConsole.MarkupLine(ItemInfoString(item));

            //Dictionary<DamageTypes, string> damageTypes = new DamageTypesNames().Names;

            //if (item.GetComponent<PhysicalDamageCharacteristic>() != null)
            //{
            //    if (item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage >= 0)
            //    {
            //        AnsiConsole.Markup("+{0} к [bold]физическому урону[/] ", item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0} к [bold]физическому урону[/] ", item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage);
            //    }
            //}

            //if (item.GetComponent<ArmorCharacteristic>() != null)
            //{
            //    if (item.GetComponent<ArmorCharacteristic>().Armor >= 0)
            //    {
            //        AnsiConsole.Markup("+{0} к [bold]броне[/] ", item.GetComponent<ArmorCharacteristic>().Armor);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0} к [bold]броне[/] ", item.GetComponent<ArmorCharacteristic>().Armor);
            //    }
            //}

            //if (item.GetComponent<ElementalDamageCharacteristic>() != null)
            //{
            //    Dictionary<DamageTypes, int> elementalDamage = item.GetComponent<ElementalDamageCharacteristic>().ElemDamage;

            //    foreach (DamageTypes type in elementalDamage.Keys)
            //    {
            //        if (elementalDamage.FirstOrDefault(x => x.Key == type).Value >= 0)
            //        {
            //            AnsiConsole.Markup("+{0} к урону ({1}) ", elementalDamage.FirstOrDefault(x => x.Key == type).Value, damageTypes[type]);
            //        }
            //        else
            //        {
            //            AnsiConsole.Markup("{0} к урону ({1}) ", elementalDamage.FirstOrDefault(x => x.Key == type).Value, damageTypes[type]);
            //        }
            //    }
            //}

            //if (item.GetComponent<ElementalResistanceCharacteristic>() != null)
            //{
            //    Dictionary<DamageTypes, int> elementalResistance = item.GetComponent<ElementalResistanceCharacteristic>().ElemResistance;

            //    foreach (DamageTypes type in elementalResistance.Keys)
            //    {
            //        if (elementalResistance.FirstOrDefault(x => x.Key == type).Value >= 0)
            //        {
            //            AnsiConsole.Markup("+{0}% сопротивления к ({1}) ", elementalResistance.FirstOrDefault(x => x.Key == type).Value, damageTypes[type]);
            //        }
            //        else
            //        {
            //            AnsiConsole.Markup("{0}% сопротивления к ({1}) ", elementalResistance.FirstOrDefault(x => x.Key == type).Value, damageTypes[type]);
            //        }
            //    }
            //}

            //if (item.GetComponent<CriticalChanceCharacteristic>() != null)
            //{
            //    if (item.GetComponent<CriticalChanceCharacteristic>().CriticalChance >= 0)
            //    {
            //        AnsiConsole.Markup("+{0} к шансу крит. удара ", item.GetComponent<CriticalChanceCharacteristic>().CriticalChance);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0} к шансу крит. удара ", item.GetComponent<CriticalChanceCharacteristic>().CriticalChance);
            //    }
            //}

            //if (item.GetComponent<CriticalDamageCharacteristic>() != null)
            //{
            //    if (item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage > 0)
            //    {
            //        AnsiConsole.Markup("+{0} к множителю крит. удара ", item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0} к множителю крит. удара ", item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage);
            //    }
            //}

            //if (item.GetComponent<CriticalDamageCharacteristic>() != null)
            //{
            //    if (item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage > 0)
            //    {
            //        AnsiConsole.Markup("+{0} к множителю крит. удара ", item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0} к множителю крит. удара ", item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage);
            //    }
            //}

            //if (item.GetComponent<StrengthCharacteristic>() != null)
            //{
            //    if (item.GetComponent<StrengthCharacteristic>().Strength > 0)
            //    {
            //        AnsiConsole.Markup("+{0} к силе ", item.GetComponent<StrengthCharacteristic>().Strength);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0} к силе ", item.GetComponent<StrengthCharacteristic>().Strength);
            //    }
            //}

            //if (item.GetComponent<AgilityCharacteristic>() != null)
            //{
            //    if (item.GetComponent<AgilityCharacteristic>().Agility > 0)
            //    {
            //        AnsiConsole.Markup("+{0} к ловкости ", item.GetComponent<AgilityCharacteristic>().Agility);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0} к ловкости ", item.GetComponent<AgilityCharacteristic>().Agility);
            //    }
            //}

            //if (item.GetComponent<IntelligenceCharacteristic>() != null)
            //{
            //    if (item.GetComponent<IntelligenceCharacteristic>().Intelligence > 0)
            //    {
            //        AnsiConsole.Markup("+{0} к интелекту ", item.GetComponent<IntelligenceCharacteristic>().Intelligence);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0} к интелекту ", item.GetComponent<IntelligenceCharacteristic>().Intelligence);
            //    }
            //}

            //if (item.GetComponent<ExperienceBooster>() != null)
            //{
            //    if (item.GetComponent<ExperienceBooster>().PercentBoost > 0)
            //    {
            //        AnsiConsole.Markup("+{0}% к опыту ", item.GetComponent<ExperienceBooster>().PercentBoost);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0}% к опыту ", item.GetComponent<ExperienceBooster>().PercentBoost);
            //    }
            //}

            //if (item.GetComponent<SpikeCharacteristic>() != null)
            //{
            //    if (item.GetComponent<SpikeCharacteristic>().SpikeDamage > 0)
            //    {

            //        AnsiConsole.Markup("+{0} к урону от шипов ", item.GetComponent<SpikeCharacteristic>().SpikeDamage);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0}% к урону от шипов ", item.GetComponent<SpikeCharacteristic>().SpikeDamage);
            //    }
            //}

            //if (item.GetComponent<HealAmplificationCharacteristic>() != null)
            //{
            //    if (item.GetComponent<HealAmplificationCharacteristic>().Amplification > 0)
            //    {

            //        AnsiConsole.Markup("+{0}% к усилению [lime]исцеления[/] ", item.GetComponent<HealAmplificationCharacteristic>().Amplification);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0}% к усилению [lime]исцеления[/] ", item.GetComponent<HealAmplificationCharacteristic>().Amplification);
            //    }
            //}

            //if (item.GetComponent<VampirismCharacteristic>() != null)
            //{
            //    if (item.GetComponent<VampirismCharacteristic>().VampirismPercent > 0)
            //    {

            //        AnsiConsole.Markup("+{0}% вампиризма ", item.GetComponent<VampirismCharacteristic>().VampirismPercent);
            //    }
            //    else
            //    {
            //        AnsiConsole.Markup("{0}% вампиризма ", item.GetComponent<VampirismCharacteristic>().VampirismPercent);
            //    }
            //}

            //Console.WriteLine();
        }

        public string ItemInfoString(Item item)
        {
            string itemInfo = "";

            Dictionary<DamageTypes, string> damageTypes = new DamageTypesNames().Names;

            if (item.GetComponent<PhysicalDamageCharacteristic>() != null)
            {
                if (item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage >= 0)
                {
                    itemInfo += ($"+{item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage} к [bold]физическому урону[/] ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage} к [bold]физическому урону[/] ");
                }
            }

            if (item.GetComponent<ArmorCharacteristic>() != null)
            {
                if (item.GetComponent<ArmorCharacteristic>().Armor >= 0)
                {
                    itemInfo += ($"+{item.GetComponent<ArmorCharacteristic>().Armor} к [bold]броне[/] ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<ArmorCharacteristic>().Armor} к [bold]броне[/] ");
                }
            }

            if (item.GetComponent<MissCharacteristic>() != null)
            {
                if (item.GetComponent<MissCharacteristic>().MissChance > 0)
                {
                    itemInfo += ($"+{item.GetComponent<MissCharacteristic>().MissChance}% к уклонению ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<MissCharacteristic>().MissChance}% к уклонению ");
                }
            }

            if (item.GetComponent<ElementalDamageCharacteristic>() != null)
            {
                Dictionary<DamageTypes, int> elementalDamage = item.GetComponent<ElementalDamageCharacteristic>().ElemDamage;

                foreach (DamageTypes type in elementalDamage.Keys)
                {
                    if (elementalDamage.FirstOrDefault(x => x.Key == type).Value >= 0)
                    {
                        itemInfo += ($"+{elementalDamage.FirstOrDefault(x => x.Key == type).Value} к урону ({damageTypes[type]}) ");
                    }
                    else
                    {
                        itemInfo += ($"{elementalDamage.FirstOrDefault(x => x.Key == type).Value} к урону ({damageTypes[type]}) ");
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
                        itemInfo += ($"+{elementalResistance.FirstOrDefault(x => x.Key == type).Value}% сопротивления к ({damageTypes[type]}) ");
                    }
                    else
                    {
                        itemInfo += ($"{elementalResistance.FirstOrDefault(x => x.Key == type).Value}% сопротивления к ({damageTypes[type]}) ");
                    }
                }
            }

            if (item.GetComponent<CriticalChanceCharacteristic>() != null)
            {
                if (item.GetComponent<CriticalChanceCharacteristic>().CriticalChance >= 0)
                {
                    itemInfo += ($"+{item.GetComponent<CriticalChanceCharacteristic>().CriticalChance} к шансу крит. удара ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<CriticalChanceCharacteristic>().CriticalChance} к шансу крит. удара ");
                }
            }

            if (item.GetComponent<CriticalDamageCharacteristic>() != null)
            {
                if (item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage > 0)
                {
                    itemInfo += ($"+{item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage} к множителю крит. удара ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<CriticalDamageCharacteristic>().CriticalDamage} к множителю крит. удара ");
                }
            }

            if (item.GetComponent<StrengthCharacteristic>() != null)
            {
                if (item.GetComponent<StrengthCharacteristic>().Strength > 0)
                {
                    itemInfo += ($"+{item.GetComponent<StrengthCharacteristic>().Strength} к силе ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<StrengthCharacteristic>().Strength} к силе ");
                }
            }

            if (item.GetComponent<AgilityCharacteristic>() != null)
            {
                if (item.GetComponent<AgilityCharacteristic>().Agility > 0)
                {
                    itemInfo += ($"+{item.GetComponent<AgilityCharacteristic>().Agility} к ловкости ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<AgilityCharacteristic>().Agility} к ловкости ");
                }
            }

            if (item.GetComponent<IntelligenceCharacteristic>() != null)
            {
                if (item.GetComponent<IntelligenceCharacteristic>().Intelligence > 0)
                {
                    itemInfo += ($"+{item.GetComponent<IntelligenceCharacteristic>().Intelligence} к интеллекту ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<IntelligenceCharacteristic>().Intelligence} к интеллекту ");
                }
            }

            if (item.GetComponent<ExperienceBooster>() != null)
            {
                if (item.GetComponent<ExperienceBooster>().PercentBoost > 0)
                {
                    itemInfo += ($"+{item.GetComponent<ExperienceBooster>().PercentBoost}% к опыту ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<ExperienceBooster>().PercentBoost}% к опыту ");
                }
            }

            if (item.GetComponent<SpikeCharacteristic>() != null)
            {
                if (item.GetComponent<SpikeCharacteristic>().SpikeDamage > 0)
                {
                    itemInfo += ($"+{item.GetComponent<SpikeCharacteristic>().SpikeDamage} к урону от шипов ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<SpikeCharacteristic>().SpikeDamage} к урону от шипов ");
                }
            }

            if (item.GetComponent<HealAmplificationCharacteristic>() != null)
            {
                if (item.GetComponent<HealAmplificationCharacteristic>().Amplification > 0)
                {
                    itemInfo += ($"+{item.GetComponent<HealAmplificationCharacteristic>().Amplification}% к усилению [lime]исцеления[/] ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<HealAmplificationCharacteristic>().Amplification}% к усилению [lime]исцеления[/] ");
                }
            }

            if (item.GetComponent<VampirismCharacteristic>() != null)
            {
                if (item.GetComponent<VampirismCharacteristic>().VampirismPercent > 0)
                {
                    itemInfo += ($"+{item.GetComponent<VampirismCharacteristic>().VampirismPercent}% вампиризма ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<VampirismCharacteristic>().VampirismPercent}% вампиризма ");
                }
            }

            if (item.GetComponent<ParryCharacteristic>() != null)
            {
                if (item.GetComponent<ParryCharacteristic>().ParryPercent > 0)
                {
                    itemInfo += ($"+{item.GetComponent<ParryCharacteristic>().ParryPercent}% парировать атаку ");
                }
                else
                {
                    itemInfo += ($"{item.GetComponent<ParryCharacteristic>().ParryPercent}% парировать атаку ");
                }
            }

            return itemInfo;
        }
    }
}
