using ConsoleRPG.Items.ItemsComponents;
using ConsoleRPG.Spells.SpellsComponents;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG
{
    internal class Fight
    {

        private Dictionary<int, string> ActionMenu = new Dictionary<int, string>();

        public Fight()
        {
            ActionMenu = GenerateActionMenu();
            //TakeDamage(Enemy, Player, Damage, Weapon);

            //if (IsCrit(Enemy.Equipment.GetCriticalChance()))
            //{
            //    Damage = CalcCritDamage(Damage, Enemy.Equipment.GetCriticalDamage());
            //    Damage = GetDamageAfterResist(Damage, Player.Equipment, Weapon);
            //    TakeCritDamage(Player, Damage, Weapon);
            //}
            //else
            //{
            //    Damage = GetDamageAfterResist(Damage, Player.Equipment, Weapon);
            //    TakeDamage(Player, Damage, Weapon);
            //}
        }

        private Dictionary<int, string> GenerateActionMenu()
        {
            ActionMenu.Add(1, "Атаковать");
            ActionMenu.Add(2, "Сбежать");

            return ActionMenu;
        }

        public void StartFight(Player Player, Enemy Enemy)
        {
            bool GiveUp = false;
            AnsiConsole.MarkupLine("[red]На вас напал[/] [bold]{0}[/] (Уровень: {1})", Enemy.Name, Enemy.Level);
            while (!Player.IsDead && !Enemy.IsDead && !GiveUp)
            {

                var choise = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Что будете делать?[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(ActionMenu.Values));

                switch (ActionMenu.FirstOrDefault(x => x.Value == choise).Key)
                {
                    case 1:
                        Item LeftHand = Player.Equipment.Equip[EquipmentSlot.LeftHand];
                        Item RightHand = Player.Equipment.Equip[EquipmentSlot.RightHand];

                        int Damage = GetDamage(Player, LeftHand);

                        Enemy.FightLogic(this, Player, LeftHand, Damage);
                        break;
                    case 2:
                        GiveUp = true;
                        AnsiConsole.MarkupLine("Вы успешно сбежали");
                        break;
                    default:
                        AnsiConsole.MarkupLine("Выберите существующие действие.");
                        break;
                }
            }
        }

        public int GetDamageAfterResist(int Damage, Equipment Equip, Item Weapon)
        {

            return Damage * (Equip.GetTypeResistance(Weapon) / 100);
        }

        public int GetDamageAfterResist(int Damage, Equipment Equip, Spell Spell)
        {
            return Damage * (Equip.GetTypeResistance(Spell) / 100);
        }

        public bool IsCrit(double CritChance)
        {
            if (CritChance >= new Random().Next(1, 101))
            {
                return true;
            }

            return false;
        }

        public int CalcCritDamage(int Damage, double CritDamage)
        {
            Damage += (int)Damage * ((int)CritDamage / 100); 

            return Damage;
        }

        public int GetDamage(Player Player, Item Weapon)
        {
            int Damage = Player.Damage;

            Damage += Player.Equipment.GetTypeDamage(Weapon);

            return Damage;
        }

        public int GetDamage(Enemy Enemy, Item Weapon)
        {
            int Damage = Enemy.Damage;

            Damage += Enemy.Equipment.GetTypeDamage(Weapon);

            return Damage;
        }

        public int GetDamage(Enemy Enemy, Spell Spell)
        {
            int Damage = Enemy.Damage;

            Damage += Enemy.Equipment.GetTypeDamage(Spell);

            return Damage;
        }

        public void TakeDamage(Player Player, int TakedDamage, Item Weapon)
        {
            TakedDamage -= GetDamageAfterResist(TakedDamage, Player.Equipment, Weapon);

            Player.CurrentHealth -= TakedDamage;

            if (Player.CurrentHealth <= 0)
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}({3})) и [red]умер[/]",
                Player.Name, TakedDamage,Weapon.Name, new DamageTypesNames().Names[Weapon.GetComponent<DamageType>().Type]);
                Player.Death();
            }
            else
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}({3})), его здоровье [lime]{4}[/]",
                Player.Name, TakedDamage, Weapon.Name, new DamageTypesNames().Names[Weapon.GetComponent<DamageType>().Type], Player.CurrentHealth);
            }
        }

        public void TakeDamage(Player Player, int TakedDamage, Spell Spell)
        {
            TakedDamage -= GetDamageAfterResist(TakedDamage, Player.Equipment, Spell);

            Player.CurrentHealth -= TakedDamage;

            if (Player.CurrentHealth <= 0)
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}({3})) и [red]умер[/]",
                Player.Name, TakedDamage, Spell.Name, new DamageTypesNames().Names[Spell.GetComponent<SpellDamageType>().Type]);
                Player.Death();
            }
            else
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}({3})), его здоровье [lime]{4}[/]",
                Player.Name, TakedDamage,
                Spell.Name, new DamageTypesNames().Names[Spell.GetComponent<SpellDamageType>().Type], Player.CurrentHealth);
            }
        }

        public void TakeDamage(Enemy Enemy, Player Player, int TakedDamage, Item Weapon)
        {
            TakedDamage -= GetDamageAfterResist(TakedDamage, Enemy.Equipment, Weapon);

            Enemy.CurrentHealth -= TakedDamage;

            if (Enemy.CurrentHealth <= 0)
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}({3})) и [red]умер[/]",
                Enemy.Name, TakedDamage, Weapon.Name, new DamageTypesNames().Names[Weapon.GetComponent<DamageType>().Type]);
                Enemy.Death(Player);
            }
            else
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}({3})), его здоровье [lime]{4}[/]",
                Enemy.Name, TakedDamage,
                Weapon.Name,  new DamageTypesNames().Names[Weapon.GetComponent<DamageType>().Type], Enemy.CurrentHealth);
            }
        }

        public void TakeDamage(Enemy Enemy, Player Player, int TakedDamage, Spell Spell)
        {
            TakedDamage -= GetDamageAfterResist(TakedDamage, Enemy.Equipment, Spell);

            Enemy.CurrentHealth -= TakedDamage;

            if (Enemy.CurrentHealth <= 0)
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}({3})) и [red]умер[/]",
                Enemy.Name, TakedDamage,
                Spell.Name, new DamageTypesNames().Names[Spell.GetComponent<SpellDamageType>().Type]);
                Enemy.Death(Player);
            }
            else
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил {1} урона ({2}({3})), его здоровье [lime]{4}[/]",
                Enemy.Name, TakedDamage,
                Spell.Name, new DamageTypesNames().Names[Spell.GetComponent<SpellDamageType>().Type], Enemy.CurrentHealth);
            }
        }

        public void TakeCritDamage(Player Player, int TakedDamage, Spell Spell)
        {
            TakedDamage = CalcCritDamage(TakedDamage, Spell.GetComponent<Criticals>().CritDamage);

            TakedDamage -= GetDamageAfterResist(TakedDamage, Player.Equipment, Spell);

            Player.CurrentHealth -= TakedDamage;

            if (Player.CurrentHealth <= 0)
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил [red]критический удар[/] {1} урона ({2}({3})) и [red]умер[/]",
                Player.Name, TakedDamage,
                Spell.Name, new DamageTypesNames().Names[Spell.GetComponent<SpellDamageType>().Type]);
                Player.Death();
            }
            else
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил [red]критический удар[/] {1} урона ({2}({3})), его здоровье [lime]{4}[/]",
                Player.Name, TakedDamage,
                Spell.Name, new DamageTypesNames().Names[Spell.GetComponent<SpellDamageType>().Type], Player.CurrentHealth);
            }
        }

        public void TakeCritDamage(Enemy Enemy, Player Player, int TakedDamage, Spell Spell)
        {
            TakedDamage = CalcCritDamage(TakedDamage, Spell.GetComponent<Criticals>().CritDamage);

            TakedDamage -= GetDamageAfterResist(TakedDamage, Enemy.Equipment, Spell);

            Enemy.CurrentHealth -= TakedDamage;

            if (Player.CurrentHealth <= 0)
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил [red]критический удар[/] {1} урона ({2}({3})) и [red]умер[/]",
                Enemy.Name, TakedDamage,
                Spell.Name, new DamageTypesNames().Names[Spell.GetComponent<SpellDamageType>().Type]);
                Enemy.Death(Player);
            }
            else
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил [red]критический удар[/] {1} урона ({2}({3})), его здоровье [lime]{4}[/]",
                Enemy.Name, TakedDamage,
                Spell.Name, new DamageTypesNames().Names[Spell.GetComponent<SpellDamageType>().Type], Enemy.CurrentHealth);
            }
        }

        public void TakeCritDamage(Player Player, int TakedDamage, Item Weapon)
        {
            TakedDamage = CalcCritDamage(TakedDamage, Player.Equipment.GetCriticalDamage());

            Player.CurrentHealth -= TakedDamage;

            if (Player.CurrentHealth <= 0)
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил [red]критический удар[/] {1} урона ({2}({3})) и [red]умер[/]",
                Player.Name, TakedDamage, 
                Weapon.Name,  new DamageTypesNames().Names[Weapon.GetComponent<DamageType>().Type]);
                Player.Death();
            }
            else
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил [red]критический удар[/] {1} урона ({2}({3})), его здоровье [lime]{4}[/]",
                Player.Name, TakedDamage, 
                Weapon.Name,  new DamageTypesNames().Names[Weapon.GetComponent<DamageType>().Type], Player.CurrentHealth);
            }
        }

        public void TakeCritDamage(Enemy Enemy, Player Player, int TakedDamage, Item Weapon)
        {
            TakedDamage = CalcCritDamage(TakedDamage, Enemy.Equipment.GetCriticalDamage());

            Enemy.CurrentHealth -= TakedDamage;

            if (Enemy.CurrentHealth <= 0)
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил [red]критический удар[/] {1} урона ({2}({3})) и [red]умер[/]",
                Enemy.Name, TakedDamage, 
                Weapon.Name,  new DamageTypesNames().Names[Weapon.GetComponent<DamageType>().Type]);
                Enemy.Death(Player);
            }
            else
            {
                AnsiConsole.MarkupLine("[bold]{0}[/] получил [red]критический удар[/] {1} урона ({2}({3})), его здоровье [lime]{4}[/]",
                Enemy.Name, TakedDamage, 
                Weapon.Name,  new DamageTypesNames().Names[Weapon.GetComponent<DamageType>().Type], Enemy.CurrentHealth);
            }
        }

    }
}
