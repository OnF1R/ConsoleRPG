using Spectre.Console;

namespace ConsoleRPG
{
    internal class Fight
    {

        private static Dictionary<int, string> ActionMenu = MenuChoises.ActionMenuChoises();

        public Fight()
        {
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

        public static void StartFight(Player Player, Enemy Enemy, ref int EnemiesKillsCount)
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
                        Dictionary<DamageTypes, int> allDamage = Player.GetExistableTypeDamage();
                        Enemy.FightLogic(Player, allDamage);
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

            if (Enemy.IsDead)
                EnemiesKillsCount++;
        }
    }
}
