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
        public int Level { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Damage { get; set; }
        public int CurrentExp { get; set; }
        public int NextLevelExp { get; set; }

        public bool IsDead { get; set; }

        public int Luck { get; set; }
        public int Armor { get; set; }

        public double MissChance { get; set; }
        public double CritChance { get; set; }
        public double CritDamage { get; set; }

        public Array Stats { get; set; }

        public Equipment Equipment = new Equipment();

        public Inventory Inventory = new Inventory();

        public Player()
        {
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
