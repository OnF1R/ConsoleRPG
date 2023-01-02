using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    abstract class Characteristics { }

    class StrengthCharacteristic : Characteristics
    {
        public int HealthPerStrength= 20;
        public double StrengthPerLevel { get; set; }
        public double RealStrength { get; set; }
        public int Strength { get; set; }
    }

    class AgilityCharacteristic : Characteristics
    {
        public int DamagePerAgility = 1;
        public double AgilityPerLevel { get; set; }
        public double RealAgility { get; set; }
        public int Agility { get; set; }
    }

    class IntelligenceCharacteristic : Characteristics
    {
        public double MagicAmplificationPerIntelligence = 1;
        public double IntelligencePerLevel { get; set; }
        public double RealIntelligence { get; set; }
        public int Intelligence { get; set; }
    }

    class MagicAmplificationCharacteristic : Characteristics
    {
        public double AmplificationPerLevel { get; set; }
        public double Amplification { get; set; }
    }

    class PhysicalDamageCharacteristic : Characteristics
    {
        public double PhysicalDamagePerLevel { get; set; }
        public double RealPhysicalDamage { get; set; }
        public int PhysicalDamage { get; set; }
    }

    class LuckCharacteristic : Characteristics
    {
        public double LuckPerLevel { get; set; }
        public double RealLuck { get; set; }
        public int Luck { get; set; }
    }

    class ArmorCharacteristic : Characteristics
    {
        public double ArmorPerLevel { get; set; }
        public double RealArmor { get; set; }
        public int Armor { get; set; }
    }

    class EvasionCharacteristic : Characteristics
    {
        public double EvasionPerLevel { get; set; }
        public double EvasionChance { get; set; }
    }

    class MissCharacteristic : Characteristics
    {
        public double MissPerLevel { get; set; }
        public double MissChance { get; set; }
    }

    class CriticalChanceCharacteristic : Characteristics
    {
        public double CriticalChancePerLevel { get; set; }
        public double CriticalChance { get; set; }
    }

    class CriticalDamageCharacteristic : Characteristics
    {
        public double CriticalDamagePerLevel { get; set; }
        public double CriticalDamage { get; set; }
    }

    class ValueCharacteristic : Characteristics
    {
        public int Cost { get; set; }
    }

    class ElementalResistanceCharacteristic : Characteristics
    {
        public Dictionary<DamageTypes, int> ElemResistance = new();

        public ElementalResistanceCharacteristic(params Dictionary<DamageTypes, int>[] damageTypes)
        {
            foreach (var damageType in damageTypes)
            {
                foreach (DamageTypes type in damageType.Keys)
                {
                    ElemResistance.Add(type, damageType[type]);
                }
            }
        }
    }

    class ElementalDamageCharacteristic : Characteristics
    {
        public Dictionary<DamageTypes, int> ElemDamage = new();

        public ElementalDamageCharacteristic(params Dictionary<DamageTypes, int>[] damageTypes)
        {
            foreach (var damageType in damageTypes)
            {
                foreach (DamageTypes type in damageType.Keys)
                {
                    ElemDamage.Add(type, damageType[type]);
                }
            }
        }
    }
}

