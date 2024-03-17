
using ConsoleRPG.Effects;
using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Interfaces;

namespace ConsoleRPG
{
    [Serializable]
    abstract class Characteristics { }

    [Serializable]
    class StrengthCharacteristic : Characteristics
    {
        public int HealthPerStrength = 20;
        public double StrengthPerLevel { get; set; }
        public double RealStrength { get; set; }
        public int Strength { get; set; }
        public int ItemsStrength { get; set; }
    }

    [Serializable]
    class AgilityCharacteristic : Characteristics
    {
        public int DamagePerAgility = 1;
        public double AgilityPerLevel { get; set; }
        public double RealAgility { get; set; }
        public int Agility { get; set; }
        public int ItemsAgility { get; set; }
    }
    [Serializable]
    class IntelligenceCharacteristic : Characteristics
    {
        public double MagicAmplificationPerIntelligence = 1;
        public double IntelligencePerLevel { get; set; }
        public double RealIntelligence { get; set; }
        public int Intelligence { get; set; }
        public int ItemsIntelligence { get; set; }
    }
    [Serializable]
    class HealAmplificationCharacteristic : Characteristics
    {
        public double AmplificationPerLevel { get; set; }
        public double Amplification { get; set; }
    }
    [Serializable]
    class ManaRegenerationAmplificationCharacteristic : Characteristics
    {
        public int AmplificationPerLevel { get; set; }
        public int Amplification { get; set; }
    }
    [Serializable]
    class MagicAmplificationCharacteristic : Characteristics
    {
        public double AmplificationPerLevel { get; set; }
        public double Amplification { get; set; }
    }
    [Serializable]
    class PhysicalDamageCharacteristic : Characteristics
    {
        public double PhysicalDamagePerLevel { get; set; }
        public double RealPhysicalDamage { get; set; }
        public int PhysicalDamage { get; set; }
    }
    [Serializable]
    class LuckCharacteristic : Characteristics
    {
        public double LuckPerLevel { get; set; }
        public double RealLuck { get; set; }
        public int Luck { get; set; }
    }
    [Serializable]
    class ArmorCharacteristic : Characteristics
    {
        public double ArmorPerLevel { get; set; }
        public double RealArmor { get; set; }
        public int Armor { get; set; }
    }
    [Serializable]
    class EvasionCharacteristic : Characteristics
    {
        public double EvasionPerLevel { get; set; }
        public double EvasionChance { get; set; }
    }
    [Serializable]
    class MissCharacteristic : Characteristics
    {
        public double MissPerLevel { get; set; }
        public double MissChance { get; set; }
    }
    [Serializable]
    class CriticalChanceCharacteristic : Characteristics
    {
        public double CriticalChancePerLevel { get; set; }
        public double CriticalChance { get; set; }
    }
    [Serializable]
    class CriticalDamageCharacteristic : Characteristics
    {
        public double CriticalDamagePerLevel { get; set; }
        public double CriticalDamage { get; set; }
    }
    [Serializable]
    class ValueCharacteristic : Characteristics
    {
        public int Cost { get; set; }
    }
    [Serializable]
    class ExperienceBooster : Characteristics
    {
        public double PercentBoost { get; set; }
        public double PercentBoostPerLevel { get; set; }
    }
    [Serializable]
    class SpikeCharacteristic : Characteristics, IDamageDealerEntity
    {
        public int SpikeDamage { get; set; }

        public string GetName()
        {
            return "Шипы";
        }
    }
    [Serializable]
    class VampirismCharacteristic : Characteristics, IHealDealerEntity
    {
        public double VampirismPercent { get; set; }
        public double VampirismPercentPerLevel { get; set; }

        public string GetName()
        {
            return "Вампиризм";
        }
    }
    [Serializable]
    class ParryCharacteristic : Characteristics
    {
        public double ParryPercent { get; set; }
        public double ParryPercentPerLevel { get; set; }
    }
    [Serializable]
    class ElementalResistanceCharacteristic : Characteristics
    {
        public Dictionary<DamageTypes, int> ElemResistance = new();

        public ElementalResistanceCharacteristic(params Dictionary<DamageTypes, int>[] damageTypes)
        {
			foreach (DamageTypes type in new DamageTypesNames().ArrayBasicElementalDamageTypes)
			{
				ElemResistance.Add(type, 0);
			}

			foreach (var damageType in damageTypes)
			{
				foreach (DamageTypes type in damageType.Keys)
				{
                    ElemResistance[type] = damageType[type];
				}
			}
		}
    }
    [Serializable]
    class ElementalDamageCharacteristic : Characteristics
    {
        public Dictionary<DamageTypes, int> ElemDamage = new();

        public ElementalDamageCharacteristic(params Dictionary<DamageTypes, int>[] damageTypes)
        {
			foreach (DamageTypes type in new DamageTypesNames().ArrayBasicElementalDamageTypes)
			{
				ElemDamage.Add(type, 0);
			}

			foreach (var damageType in damageTypes)
            {
                foreach (DamageTypes type in damageType.Keys)
                {
                    ElemDamage[type] =  damageType[type];
                }
            }
        }
    }
    [Serializable]
    class StatusEffectsCharacteristic : Characteristics
    {
        public Dictionary<BaseEffect, double> Effects = new();

        public StatusEffectsCharacteristic(params Dictionary<BaseEffect, double>[] effects)
        {
            foreach (var damageType in effects)
            {
                foreach (BaseEffect effect in damageType.Keys)
                {
                    Effects.Add(effect, damageType[effect]);
                }
            }
        }
    }
    [Serializable]
    class StatusEffectsImmunityCharacteristic : Characteristics
    {
        public List<BaseEffect> ImmunityEffects = new List<BaseEffect>();

        public StatusEffectsImmunityCharacteristic(params BaseEffect[] effects) 
        {
            foreach (var effect in effects)
                ImmunityEffects.Add(effect);
        }
    }
}

