using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;
using Spectre.Console;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Items.ItemsComponents;
using ConsoleRPG.Spells.SpellsComponents;

namespace ConsoleRPG
{
    internal class Player
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Level { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int CurrentExp { get; set; }
        public int NextLevelExp { get; set; }

        public bool IsDead { get; set; }


        //public Player AddComponent(Characteristics component)
        //{
        //    _components.Add(component);
        //    return this;
        //}

        //public T GetComponent<T>()
        //{
        //    return (T)_components.OfType<T>().FirstOrDefault();
        //}

        //private List<Characteristics> _components = new List<Characteristics>();

        public Race Race;

        public Player AddComponent(Characteristics component)
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

        public Player()
        {
            AddComponent(new Stat_Strength { RealSTR = 1, STR = 1 });
            AddComponent(new Stat_Agility { RealAGI = 1, AGI = 1 });
            AddComponent(new Stat_Intelligence { RealINT = 1, INT = 1 });
            AddComponent(new Stat_Armor { RealARM = 0, ARM = 0 });
            AddComponent(new Stat_Damage { RealDMG = 3, DMG = 3 });
            AddComponent(new Stat_CritChance { CHANCE = 0 });
            AddComponent(new Stat_CritDamage { DMG = 0 });
            AddComponent(new Stat_Evasion { CHANCE = 0 });
            AddComponent(new Stat_MissChance { CHANCE = 0 });
            AddComponent(new Stat_Luck { RealLCK = 0, LCK = 0 });
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

        public void LevelUp()
        {
            Level += 1;
            AnsiConsole.MarkupLine("[bold]{0}[/] повысил уровень, текущий уровень [bold]{1}[/]", Name, Level);
            NextLevelExp = (int)Math.Floor(NextLevelExp * 1.3);
            UpgradeStats();
        }

        public void UpgradeStats()
        {
            GetComponent<Stat_Strength>().RealSTR += Race.GetComponent<Stat_Strength>().PerLevel;
            GetComponent<Stat_Strength>().STR = (int)GetComponent<Stat_Strength>().RealSTR;
            GetComponent<Stat_Agility>().RealAGI += Race.GetComponent<Stat_Agility>().PerLevel;
            GetComponent<Stat_Agility>().AGI = (int)GetComponent<Stat_Agility>().RealAGI;
            GetComponent<Stat_Intelligence>().RealINT += Race.GetComponent<Stat_Intelligence>().PerLevel;
            GetComponent<Stat_Intelligence>().INT = (int)GetComponent<Stat_Intelligence>().RealINT;

            if (Race.GetComponent<Stat_Armor>() != null)
            {
                GetComponent<Stat_Armor>().RealARM += Race.GetComponent<Stat_Armor>().PerLevel;
                GetComponent<Stat_Armor>().ARM = (int)GetComponent<Stat_Armor>().RealARM;
            }
            if (Race.GetComponent<Stat_Damage>() != null) 
            { 
                GetComponent<Stat_Damage>().RealDMG += Race.GetComponent<Stat_Damage>().PerLevel;
                GetComponent<Stat_Damage>().DMG = (int)GetComponent<Stat_Damage>().RealDMG;
            }
            if (Race.GetComponent<Stat_Luck>() != null)
            {
                GetComponent<Stat_Luck>().RealLCK += Race.GetComponent<Stat_Luck>().PerLevel;
                GetComponent<Stat_Luck>().LCK = (int)GetComponent<Stat_Luck>().RealLCK;
            }
            if (Race.GetComponent<Stat_MissChance>() != null) 
            { 
                GetComponent<Stat_MissChance>().CHANCE += Race.GetComponent<Stat_MissChance>().PerLevel;
            }
            if (Race.GetComponent<Stat_Evasion>() != null) 
            { 
                GetComponent<Stat_Evasion>().CHANCE += Race.GetComponent<Stat_Evasion>().PerLevel;
            }            
            if (Race.GetComponent<Stat_CritDamage>() != null) 
            { 
                GetComponent<Stat_CritDamage>().DMG += Race.GetComponent<Stat_CritDamage>().PerLevel;
            }
            if (Race.GetComponent<Stat_CritChance>() != null) 
            { 
                GetComponent<Stat_CritChance>().CHANCE += Race.GetComponent<Stat_CritChance>().PerLevel;
            }
        }

        public void ShowCharacteristics()
        {
            var table = new Table();
            table.AddColumn(new TableColumn("[bold]Характеристики[/]"));
            //table.AddColumn(new TableColumn("[bold]Характеристики[/]").Centered());
            if (GetComponent<Stat_Strength>() != null) { table.AddRow($"Сила: {GetComponent<Stat_Strength>().STR}"); }
            if (GetComponent<Stat_Agility>() != null) { table.AddRow($"Ловкость: {GetComponent<Stat_Agility>().AGI}"); }
            if (GetComponent<Stat_Intelligence>() != null) { table.AddRow($"Интеллект: {GetComponent<Stat_Intelligence>().INT}"); }
            if (GetComponent<Stat_Armor>() != null) { table.AddRow($"Броня: {GetComponent<Stat_Armor>().ARM}"); }
            if (GetComponent<Stat_Damage>() != null) { table.AddRow($"Урон: {GetComponent<Stat_Damage>().DMG}"); }
            if (GetComponent<Stat_MissChance>() != null) { table.AddRow($"Шанс промаха: {GetComponent<Stat_MissChance>().CHANCE}%"); }
            if (GetComponent<Stat_Evasion>() != null) { table.AddRow($"Шанс уклонения: {GetComponent<Stat_Evasion>().CHANCE}%"); }
            if (GetComponent<Stat_Luck>() != null) { table.AddRow($"Удача: {GetComponent<Stat_Luck>().LCK}"); }
            if (GetComponent<Stat_CritDamage>() != null) { table.AddRow($"Крит. урон: {GetComponent<Stat_CritDamage>().DMG}%"); }
            if (GetComponent<Stat_CritChance>() != null) { table.AddRow($"Крит. шанс: {GetComponent<Stat_CritChance>().CHANCE}%"); }
            AnsiConsole.Write(table);

            //if (GetComponent<Stat_Strength>() != null) { AnsiConsole.MarkupLine("Сила: {0}", GetComponent<Stat_Strength>().STR); }
            //if (GetComponent<Stat_Agility>() != null) { AnsiConsole.MarkupLine("Ловкость: {0}", GetComponent<Stat_Agility>().AGI); }
            //if (GetComponent<Stat_Intelligence>() != null) { AnsiConsole.MarkupLine("Интеллект: {0}", GetComponent<Stat_Intelligence>().INT); }
            //if (GetComponent<Stat_Armor>() != null) { AnsiConsole.MarkupLine("Броня: {0}", GetComponent<Stat_Armor>().ARM); }
            //if (GetComponent<Stat_Damage>() != null) { AnsiConsole.MarkupLine("Урон: {0}", GetComponent<Stat_Damage>().DMG); }
            //if (GetComponent<Stat_MissChance>() != null) { AnsiConsole.MarkupLine("Шанс промаха: {0}", GetComponent<Stat_MissChance>().CHANCE); }
            //if (GetComponent<Stat_Evasion>() != null) { AnsiConsole.MarkupLine("Шанс уклонения: {0}", GetComponent<Stat_Evasion>().CHANCE); }
            //if (GetComponent<Stat_Luck>() != null) { AnsiConsole.MarkupLine("Удача: {0}", GetComponent<Stat_Luck>().LCK); }
            //if (GetComponent<Stat_CritDamage>() != null) { AnsiConsole.MarkupLine("Крит. урон : {0}", GetComponent<Stat_CritDamage>().DMG); }
            //if (GetComponent<Stat_CritChance>() != null) { AnsiConsole.MarkupLine("Крит. шанс : {0}", GetComponent<Stat_CritChance>().CHANCE); }
        }

        public void TakeExp(int Exp)
        {
            CurrentExp += Exp;
            if (CurrentExp >= NextLevelExp)
            {
                CurrentExp -= NextLevelExp;
                LevelUp();
            }
            else
            {
                AnsiConsole.MarkupLine("{0} получил {1} опыта", Name, Exp);
            }
        }

        public void Death()
        {
            IsDead = true;
        }

    }
}
