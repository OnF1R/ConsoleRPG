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

        public int MaxMana { get; set; }
        public int CurrentMana { get; set; }

        public bool IsDead { get; set; }

        public Race MyRace;

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
            AddComponent(new ExperienceBooster { PercentBoost = 0, PercentBoostPerLevel = 0 });
            AddComponent(new SpikeCharacteristic { SpikeDamage = 0 });
            AddComponent(new VampirismCharacteristic { VampirismPercent = 0, VampirismPercentPerLevel = 0 });
            AddComponent(new HealAmplificationCharacteristic { Amplification = 0, AmplificationPerLevel = 0 });
            AddComponent(new ParryCharacteristic { ParryPercent = 0, ParryPercentPerLevel = 0 });


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
            HealMaxMana();
        }

        public void Heal(int health)
        {
            health = HealWithAmplification(health);

            if (CurrentHealth + health > MaxHealth)
            {
                HealMaxHealth();
                AnsiConsole.MarkupLine($"{Name} [lime]исцелился[/] на {health}, его [lime]здоровье[/] {CurrentHealth}");
            }
            else
            {
                CurrentHealth += health;
                AnsiConsole.MarkupLine($"{Name} [lime]исцелился[/] на {health}, его [lime]здоровье[/] {CurrentHealth}");
                if (CurrentHealth <= 0)
                {
                    AnsiConsole.MarkupLine($"{Name} [red]умер[/] от лечения :)");
                    Death();
                }
            }
        }

        public void HealMaxHealth()
        {
            CurrentHealth = MaxHealth;
        }

        public void HealMaxMana()
        {
            CurrentMana = MaxMana;
        }

        public void TakeDamage(Unit damageDealer, int takedDamage, DamageTypes damageType)
        {
            BeforeTakeDamageBehaviour(this, ref takedDamage, damageType);

            if (takedDamage > 0)
            {
                CurrentHealth -= takedDamage;

                if (CurrentHealth <= 0)
                {
                    AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}) и [red]умер[/]",
                    Name, takedDamage, new DamageTypesNames().Names[damageType]);
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}), его здоровье [lime]{3}[/]",
                    Name, takedDamage, new DamageTypesNames().Names[damageType], CurrentHealth);

                }

                AfterTakeDamageBehaviour(damageDealer, damageType);
            }
        }

        public void TakeCriticalDamage(Unit damageDealer, int takedDamage, DamageTypes damageType)
        {
            BeforeTakeDamageBehaviour(this, ref takedDamage, damageType);

            if (takedDamage > 0)
            {
                takedDamage = CalcPhysicalCritical(takedDamage);

                CurrentHealth -= takedDamage;

                if (CurrentHealth <= 0)
                {
                    AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} [red]критического урона[/] ({2}) и [red]умер[/]",
                    Name, takedDamage, new DamageTypesNames().Names[damageType]);
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} [red]критического урона[/] ({2}), его здоровье [lime]{3}[/]",
                    Name, takedDamage, new DamageTypesNames().Names[damageType], CurrentHealth);
                }

                AfterTakeDamageBehaviour(damageDealer, damageType);
            }
        }

        public void BeforeAttackBehaviour(Unit unit)
        {

        }

        public void AfterAttackBehaviour(Unit unit)
        {
            TakeVampirism();
        }

        public void BeforeTakeDamageBehaviour(Unit damageDealer, ref int takedDamage, DamageTypes damageType)
        {
            if (damageType == DamageTypes.Physical && CheckMiss())
            {
                takedDamage = 0;
                return;
            }

            if (damageType == DamageTypes.Physical && CheckParry())
            {
                takedDamage = 0;
                return;
            }

            takedDamage = CheckResistance(takedDamage, damageType);
        }

        public void AfterTakeDamageBehaviour(Unit damageDealer, DamageTypes damageType)
        {
            if (CurrentHealth <= 0)
            {
                Death();
            }
            else
            {
                if (damageType == DamageTypes.Physical && !IsDead)
                {
                    TakeSpikeDamage(damageDealer);
                }
            }
        }

        public bool CheckMiss()
        {
            int miss = GetMissChance();

            if (new Random().Next(1, 101) < miss)
            {
                AnsiConsole.MarkupLine($"{Name} [bold]уклонился[/] от атаки!");
                return true;
            }

            return false;
        }

        public bool CheckParry()
        {
            int parry = GetParryChance();

            if (new Random().Next(1, 101) < parry)
            {
                AnsiConsole.MarkupLine($"{Name} [bold]парировал[/] атаку!");
                return true;
            }

            return false;
        }

        public int CheckResistance(int takedDamage, DamageTypes damageType)
        {
            Dictionary<DamageTypes, int> resistance = GetExistableTypeResistance();
            if (resistance.ContainsKey(damageType))
            {
                if (resistance[damageType] > 0)
                {
                    double tempBlockedDamage = takedDamage * (resistance[damageType] / (double)100);
                    takedDamage -= (int)tempBlockedDamage;
                }
            }

            return takedDamage;
        }

        public void TakeSpikeDamage(Unit damageDealer)
        {
            int spikeDamage = SpikeDamage();

            if (spikeDamage > 0)
            {
                damageDealer.CurrentHealth -= spikeDamage;
                if (damageDealer.CurrentHealth > 0)
                {
                    AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона от шипов, его здоровье [lime]{2}[/]",
                    damageDealer.Name, spikeDamage, damageDealer.CurrentHealth);
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона от шипов и [red]умер[/]",
                    damageDealer.Name, spikeDamage, damageDealer.CurrentHealth);
                    Death();
                }
            }
        }

        public void TakeVampirism()
        {
            double vampirism = GetComponent<VampirismCharacteristic>().VampirismPercent;

            int damage = GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;

            int percentConverter = 100;

            int heal = (int)(damage * (vampirism / percentConverter));

            if (heal > 0)
            {
                Heal(heal);
            }
        }

        public void UpgradeStats()
        {
            BaseStatsUpgrade();
            RaceUpgradeStats();
        }

        public void BaseStatsUpgrade()
        {
            StrengthUpgrade();
            AgilityUpgrade();
            IntelligenceUpdage();
        }

        public void StrengthUpgrade()
        {
            GetComponent<StrengthCharacteristic>().RealStrength += MyRace.GetComponent<StrengthCharacteristic>().StrengthPerLevel;
            int tempRealSTR = (int)GetComponent<StrengthCharacteristic>().RealStrength;
            int tempSTR = GetComponent<StrengthCharacteristic>().Strength;
            int subtracts = tempRealSTR - tempSTR;
            MaxHealth += subtracts * GetComponent<StrengthCharacteristic>().HealthPerStrength;
            GetComponent<StrengthCharacteristic>().Strength = tempRealSTR;
        }

        public void AgilityUpgrade()
        {
            GetComponent<AgilityCharacteristic>().RealAgility += MyRace.GetComponent<AgilityCharacteristic>().AgilityPerLevel;
            int tempRealAGI = (int)GetComponent<AgilityCharacteristic>().RealAgility;
            int tempAGI = GetComponent<AgilityCharacteristic>().Agility;
            int subtracts = tempRealAGI - tempAGI;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += subtracts * GetComponent<AgilityCharacteristic>().DamagePerAgility;
            GetComponent<AgilityCharacteristic>().Agility = tempRealAGI;
        }

        public void IntelligenceUpdage()
        {
            GetComponent<IntelligenceCharacteristic>().RealIntelligence += MyRace.GetComponent<IntelligenceCharacteristic>().IntelligencePerLevel;
            int tempRealINT = (int)GetComponent<IntelligenceCharacteristic>().RealIntelligence;
            int tempINT = GetComponent<IntelligenceCharacteristic>().Intelligence;
            int subtracts = tempRealINT - tempINT;
            GetComponent<MagicAmplificationCharacteristic>().Amplification +=
                subtracts * GetComponent<IntelligenceCharacteristic>().MagicAmplificationPerIntelligence;
            GetComponent<IntelligenceCharacteristic>().Intelligence = tempRealINT;
        }

        public void RaceUpgradeStats()
        {
            if (MyRace.GetComponent<ArmorCharacteristic>() != null)
            {
                GetComponent<ArmorCharacteristic>().RealArmor += MyRace.GetComponent<ArmorCharacteristic>().ArmorPerLevel;
                int tempRealArmor = (int)GetComponent<ArmorCharacteristic>().RealArmor;
                int tempArmor = GetComponent<ArmorCharacteristic>().Armor;
                int subtracts = tempRealArmor - tempArmor;
                GetComponent<ArmorCharacteristic>().Armor += subtracts;
            }
            if (MyRace.GetComponent<PhysicalDamageCharacteristic>() != null)
            {
                GetComponent<PhysicalDamageCharacteristic>().RealPhysicalDamage += MyRace.GetComponent<PhysicalDamageCharacteristic>().PhysicalDamagePerLevel;
                int tempRealDamage = (int)GetComponent<PhysicalDamageCharacteristic>().RealPhysicalDamage;
                int tempDamage = GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
                int subtracts = tempRealDamage - tempDamage;
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += subtracts;
            }
            if (MyRace.GetComponent<LuckCharacteristic>() != null)
            {
                GetComponent<LuckCharacteristic>().RealLuck += MyRace.GetComponent<LuckCharacteristic>().LuckPerLevel;
                int tempRealLuck = (int)GetComponent<LuckCharacteristic>().RealLuck;
                int tempLuck = GetComponent<LuckCharacteristic>().Luck;
                int subtracts = tempRealLuck - tempLuck;
                GetComponent<LuckCharacteristic>().Luck += (int)GetComponent<LuckCharacteristic>().RealLuck;
            }
            if (MyRace.GetComponent<MissCharacteristic>() != null)
            {
                GetComponent<MissCharacteristic>().MissChance += MyRace.GetComponent<MissCharacteristic>().MissPerLevel;
            }
            if (MyRace.GetComponent<EvasionCharacteristic>() != null)
            {
                GetComponent<EvasionCharacteristic>().EvasionChance += MyRace.GetComponent<EvasionCharacteristic>().EvasionPerLevel;
            }
            if (MyRace.GetComponent<CriticalChanceCharacteristic>() != null)
            {
                GetComponent<CriticalChanceCharacteristic>().CriticalChance += MyRace.GetComponent<CriticalChanceCharacteristic>().CriticalChancePerLevel;
            }
            if (MyRace.GetComponent<CriticalDamageCharacteristic>() != null)
            {
                GetComponent<CriticalDamageCharacteristic>().CriticalDamage += MyRace.GetComponent<CriticalDamageCharacteristic>().CriticalDamagePerLevel;
            }
            if (MyRace.GetComponent<MagicAmplificationCharacteristic>() != null)
            {
                GetComponent<MagicAmplificationCharacteristic>().Amplification += MyRace.GetComponent<MagicAmplificationCharacteristic>().AmplificationPerLevel;
            }
            if (MyRace.GetComponent<ExperienceBooster>() != null)
            {
                GetComponent<ExperienceBooster>().PercentBoost += MyRace.GetComponent<ExperienceBooster>().PercentBoostPerLevel;
            }
            if (MyRace.GetComponent<HealAmplificationCharacteristic>() != null)
            {
                GetComponent<HealAmplificationCharacteristic>().Amplification += MyRace.GetComponent<HealAmplificationCharacteristic>().AmplificationPerLevel;
            }
            if (MyRace.GetComponent<VampirismCharacteristic>() != null)
            {
                GetComponent<VampirismCharacteristic>().VampirismPercent += MyRace.GetComponent<VampirismCharacteristic>().VampirismPercentPerLevel;
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

        public int GetLuck()
        {
            return GetComponent<LuckCharacteristic>().Luck;
        }

        public int GetMissChance()
        {
            return (int)GetComponent<MissCharacteristic>().MissChance;
        }

        public int GetParryChance()
        {
            return (int)GetComponent<ParryCharacteristic>().ParryPercent;
        }

        public bool IsCrit()
        {
            if (GetCriticalChance() >= new Random().Next(1, 101))
            {
                return true;
            }

            return false;
        }

        public double ExperienceBoost()
        {
            return GetComponent<ExperienceBooster>().PercentBoost;
        }

        public double HealAmplification()
        {
            return GetComponent<HealAmplificationCharacteristic>().Amplification;
        }

        public double VampirismPercent()
        {
            return GetComponent<VampirismCharacteristic>().VampirismPercent;
        }

        public int HealWithAmplification(int heal)
        {
            int healWithBoost = heal;
            int percentConvert = 100;
            int healAmplification = (int)(HealAmplification() / percentConvert);

            if (healAmplification > 0)
            {
                healWithBoost += healAmplification;
                AnsiConsole.MarkupLine($"[lime]Исцеление[/] усилено на [bold]{healAmplification}[/]");
            }

            return healWithBoost;
        }

        public int CalcPhysicalCritical(int damage)
        {
            int percentConvert = 100;
            return damage += (int)damage * ((int)GetCriticalDamage() / percentConvert);
        }

        public int SpikeDamage()
        {
            return GetComponent<SpikeCharacteristic>().SpikeDamage;
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
