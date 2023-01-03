using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    abstract class Unit
    {
        public string Name { get; set; }

        public int Level { get; set; }

        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }

        public bool IsDead { get; set; }

        public Race Race;

        public Unit AddComponent(Characteristics component)
        {
            _components.Add(component);
            return this;
        }

        public T GetComponent<T>()
        {
            return (T)_components.OfType<T>().FirstOrDefault();
        }

        private List<Characteristics> _components = new List<Characteristics>();

        public Equipment Equipment = new Equipment();

        public Inventory Inventory = new Inventory();

        public Unit()
        {
            AddComponent(new StrengthCharacteristic { RealStrength = 1, Strength = 1, StrengthPerLevel = 0 });
            AddComponent(new AgilityCharacteristic { RealAgility = 1, Agility = 1, AgilityPerLevel = 0 });
            AddComponent(new IntelligenceCharacteristic { RealIntelligence = 1, Intelligence = 1, IntelligencePerLevel = 0 });
            AddComponent(new ArmorCharacteristic { RealArmor = 0, Armor = 0, ArmorPerLevel = 0 });
            AddComponent(new PhysicalDamageCharacteristic { RealPhysicalDamage = 3, PhysicalDamage = 3, PhysicalDamagePerLevel = 0 });
            AddComponent(new CriticalChanceCharacteristic { CriticalChance = 0, CriticalChancePerLevel = 0 });
            AddComponent(new CriticalDamageCharacteristic { CriticalDamage = 0, CriticalDamagePerLevel = 0 });
            AddComponent(new EvasionCharacteristic { EvasionChance = 0, EvasionPerLevel = 0 });
            AddComponent(new MissCharacteristic { MissChance = 0, MissPerLevel = 0 });
            AddComponent(new LuckCharacteristic { RealLuck = 0, Luck = 0, LuckPerLevel = 0 });
            AddComponent(new MagicAmplificationCharacteristic { Amplification = 0, AmplificationPerLevel = 0 });

            Dictionary<DamageTypes, int> elemental = new Dictionary<DamageTypes, int>();
            Dictionary<DamageTypes, int> elemental2 = new Dictionary<DamageTypes, int>();

            foreach (DamageTypes type in new DamageTypesNames().ArrayBasicElementalDamageTypes)
            {
                elemental.Add(type, 0);
                elemental2.Add(type, 0);
            }

            AddComponent(new ElementalResistanceCharacteristic { ElemResistance = elemental });
            AddComponent(new ElementalDamageCharacteristic { ElemDamage = elemental2 });
        }

        public void Resurrection()
        {
            IsDead = false;
            AnsiConsole.MarkupLine(Name + " [lime]возроджён[/]");
            HealMaxHealth();
        }

        public void HealMaxHealth()
        {
            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(int TakedDamage, DamageTypes damageType)
        {
            Dictionary<DamageTypes, int> resistance = GetExistableTypeResistance();
            if (resistance.ContainsKey(damageType))
            {
                if (resistance[damageType] > 0)
                {
                    double tempBlockedDamage = TakedDamage * (resistance[damageType] / (double)100);
                    TakedDamage -= (int)tempBlockedDamage;
                }
            }

            CurrentHealth -= TakedDamage;

            if (CurrentHealth <= 0)
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}) и [red]умер[/]",
                Name, TakedDamage, new DamageTypesNames().Names[damageType]);
                Death();
            }
            else
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}), его здоровье [lime]{3}[/]",
                Name, TakedDamage, new DamageTypesNames().Names[damageType], CurrentHealth);
            }
        }

        public void TakeCriticalDamage(int TakedDamage, DamageTypes damageType)
        {
            Dictionary<DamageTypes, int> resistance = GetExistableTypeResistance();
            if (resistance.ContainsKey(damageType))
            {
                if (resistance[damageType] > 0)
                {
                    double tempBlockedDamage = TakedDamage * (resistance[damageType] / (double)100);
                    TakedDamage -= (int)tempBlockedDamage;
                }
            }

            TakedDamage = CalcPhysicalCritical(TakedDamage);

            CurrentHealth -= TakedDamage;

            if (CurrentHealth <= 0)
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} [red]критического урона[/] ({2}) и [red]умер[/]",
                Name, TakedDamage, new DamageTypesNames().Names[damageType]);
                Death();
            }
            else
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} [red]критического урона[/] ({2}), его здоровье [lime]{3}[/]",
                Name, TakedDamage, new DamageTypesNames().Names[damageType], CurrentHealth);
            }
        }

        public void UpgradeStats()
        {
            BaseStatsUpgrade();
            RaceUpgradeStats();
        }

        public void StrengthUpgrade()
        {
            GetComponent<StrengthCharacteristic>().RealStrength += Race.GetComponent<StrengthCharacteristic>().StrengthPerLevel;
            int tempRealSTR = (int)GetComponent<StrengthCharacteristic>().RealStrength;
            int tempSTR = GetComponent<StrengthCharacteristic>().Strength;
            int subtracts = tempRealSTR - tempSTR;
            MaxHealth += subtracts * GetComponent<StrengthCharacteristic>().HealthPerStrength;
            GetComponent<StrengthCharacteristic>().Strength = tempRealSTR;
        }

        public void AgilityUpgrade()
        {
            GetComponent<AgilityCharacteristic>().RealAgility += Race.GetComponent<AgilityCharacteristic>().AgilityPerLevel;
            int tempRealAGI = (int)GetComponent<AgilityCharacteristic>().RealAgility;
            int tempAGI = GetComponent<AgilityCharacteristic>().Agility;
            int subtracts = tempRealAGI - tempAGI;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += subtracts * GetComponent<AgilityCharacteristic>().DamagePerAgility;
            GetComponent<AgilityCharacteristic>().Agility = tempRealAGI;
        }

        public void IntelligenceUpdage()
        {
            GetComponent<IntelligenceCharacteristic>().RealIntelligence += Race.GetComponent<IntelligenceCharacteristic>().IntelligencePerLevel;
            int tempRealINT = (int)GetComponent<IntelligenceCharacteristic>().RealIntelligence;
            int tempINT = GetComponent<IntelligenceCharacteristic>().Intelligence;
            int subtracts = tempRealINT - tempINT;
            GetComponent<MagicAmplificationCharacteristic>().Amplification +=
                subtracts * GetComponent<IntelligenceCharacteristic>().MagicAmplificationPerIntelligence;
            GetComponent<IntelligenceCharacteristic>().Intelligence = tempRealINT;
        }

        public void BaseStatsUpgrade()
        {
            StrengthUpgrade();
            AgilityUpgrade();
            IntelligenceUpdage();
        }

        public void RaceUpgradeStats()
        {
            if (Race.GetComponent<ArmorCharacteristic>() != null)
            {
                GetComponent<ArmorCharacteristic>().RealArmor += Race.GetComponent<ArmorCharacteristic>().ArmorPerLevel;
                GetComponent<ArmorCharacteristic>().Armor = (int)GetComponent<ArmorCharacteristic>().RealArmor;
            }
            if (Race.GetComponent<PhysicalDamageCharacteristic>() != null)
            {
                GetComponent<PhysicalDamageCharacteristic>().RealPhysicalDamage += Race.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamagePerLevel;
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = (int)GetComponent<PhysicalDamageCharacteristic>().RealPhysicalDamage;
            }
            if (Race.GetComponent<LuckCharacteristic>() != null)
            {
                GetComponent<LuckCharacteristic>().RealLuck += Race.GetComponent<LuckCharacteristic>().LuckPerLevel;
                GetComponent<LuckCharacteristic>().Luck = (int)GetComponent<LuckCharacteristic>().RealLuck;
            }
            if (Race.GetComponent<MissCharacteristic>() != null)
            {
                GetComponent<MissCharacteristic>().MissChance += Race.GetComponent<MissCharacteristic>().MissPerLevel;
            }
            if (Race.GetComponent<EvasionCharacteristic>() != null)
            {
                GetComponent<EvasionCharacteristic>().EvasionChance += Race.GetComponent<EvasionCharacteristic>().EvasionPerLevel;
            }
            if (Race.GetComponent<CriticalChanceCharacteristic>() != null)
            {
                GetComponent<CriticalChanceCharacteristic>().CriticalChance += Race.GetComponent<CriticalChanceCharacteristic>().CriticalChancePerLevel;
            }
            if (Race.GetComponent<CriticalDamageCharacteristic>() != null)
            {
                GetComponent<CriticalDamageCharacteristic>().CriticalDamage += Race.GetComponent<CriticalDamageCharacteristic>().CriticalDamagePerLevel;
            }
            if (Race.GetComponent<MagicAmplificationCharacteristic>() != null)
            {
                GetComponent<MagicAmplificationCharacteristic>().Amplification += Race.GetComponent<MagicAmplificationCharacteristic>().AmplificationPerLevel;
            }
        }

        public Dictionary<DamageTypes, int> GetExistableTypeDamage()
        {
            Dictionary<DamageTypes, int> result = new Dictionary<DamageTypes, int>();
            Dictionary<DamageTypes, int> damageTypes = GetElementalDamage();
            result.Add(DamageTypes.Physical, GetPhysicalDamage());

            

            foreach (DamageTypes type in damageTypes.Keys)
            {
                result.Add(type, damageTypes[type]);
            }

            return result;
        }

        public Dictionary<DamageTypes, int> GetExistableTypeResistance()
        {
            Dictionary<DamageTypes, int> result = new Dictionary<DamageTypes, int>();

            result.Add(DamageTypes.Physical, GetArmor());

            result.Concat(GetElementalResistance());

            return result;
        }

        public int GetPhysicalDamage()
        {
            return GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
        }

        public int GetArmor()
        {
            return GetComponent<ArmorCharacteristic>().Armor;
        }

        public Dictionary<DamageTypes, int> GetElementalDamage()
        {
            Dictionary<DamageTypes, int> elementalDamage = new Dictionary<DamageTypes, int>();

            foreach (DamageTypes type in GetComponent<ElementalDamageCharacteristic>().ElemDamage.Keys)
            {
                int damage = GetComponent<ElementalDamageCharacteristic>().ElemDamage[type];
                if (damage > 0)
                {
                    elementalDamage.Add(type, damage);
                }
            }

            return elementalDamage;
        }

        public Dictionary<DamageTypes, int> GetElementalResistance()
        {
            Dictionary<DamageTypes, int> elementalResistance = new Dictionary<DamageTypes, int>();

            foreach (DamageTypes type in GetComponent<ElementalResistanceCharacteristic>().ElemResistance.Keys)
            {
                int resistance = GetComponent<ElementalResistanceCharacteristic>().ElemResistance[type];
                if (resistance != 0)
                {
                    elementalResistance.Add(type, resistance);
                }
            }

            return elementalResistance;
        }

        public double GetCriticalChance()
        {
            return GetComponent<CriticalChanceCharacteristic>().CriticalChance;
        }

        public double GetCriticalDamage()
        {
            return GetComponent<CriticalDamageCharacteristic>().CriticalDamage;
        }

        public bool IsCrit()
        {
            if (GetCriticalChance() >= new Random().Next(1, 101))
            {
                return true;
            }

            return false;
        }

        public int CalcPhysicalCritical(int damage)
        {
            return damage += (int)damage * ((int)GetCriticalDamage() / 100);
        }

        public Dictionary<DamageTypes, int> GetTypeResistance(params DamageTypes[] damageTypes)
        {
            Dictionary<DamageTypes, int> elementalResistance = new();

            foreach (DamageTypes damageType in damageTypes)
            {
                int resistance = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == damageType).Value;
                if (resistance > 0)
                {
                    elementalResistance.Add(damageType, resistance);
                }
            }

            return elementalResistance;
        }

        public void Death()
        {
            IsDead = true;
        }
    }
}
