using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    abstract class Characteristics { }

    class Stat_Strength : Characteristics
    {
        public double PerLevel { get; set; }
        public double RealSTR { get; set; }
        public int STR { get; set; }
    }

    class Stat_Agility : Characteristics
    {
        public double PerLevel { get; set; }
        public double RealAGI { get; set; }
        public int AGI { get; set; }
    }

    class Stat_Intelligence : Characteristics
    {
        public double PerLevel { get; set; }
        public double RealINT { get; set; }
        public int INT { get; set; }
    }

    class Stat_Damage : Characteristics
    {
        public double PerLevel { get; set; }
        public double RealDMG { get; set; }
        public int DMG { get; set; }
    }

    class Stat_Luck : Characteristics
    {
        public double PerLevel { get; set; }
        public double RealLCK { get; set; }
        public int LCK { get; set; }
    }

    class Stat_Armor : Characteristics
    {
        public double PerLevel { get; set; }
        public double RealARM { get; set; }
        public int ARM { get; set; }
    }

    class Stat_MissChance : Characteristics
    {
        public double PerLevel { get; set; }
        public double CHANCE { get; set; }
    }

    class Stat_Evasion : Characteristics
    {
        public double PerLevel { get; set; }
        public double CHANCE { get; set; }
    }

    class Stat_CritChance : Characteristics
    {
        public double PerLevel { get; set; }
        public double CHANCE { get; set; }
    }

    class Stat_CritDamage : Characteristics
    {
        public double PerLevel { get; set; }
        public double DMG { get; set; }
    }

    class Stat_ElementalResistance : Characteristics
    {
        public Dictionary<DamageTypes, int> RESISTANCE = new();

        public Stat_ElementalResistance(params Dictionary<DamageTypes, int>[] damageTypes)
        {
            foreach (var damageType in damageTypes)
            {
                foreach (DamageTypes type in damageType.Keys)
                {
                    RESISTANCE.Add(type, damageType[type]);
                }
            }
        }
    }

    class Stat_ElementalDamage : Characteristics
    {
        public Dictionary<DamageTypes, int> DAMAGE = new();

        public Stat_ElementalDamage(params Dictionary<DamageTypes, int>[] damageTypes)
        {
            foreach (var damageType in damageTypes)
            {
                foreach (DamageTypes type in damageType.Keys)
                {
                    DAMAGE.Add(type, damageType[type]);
                }
            }
        }
    }
}

