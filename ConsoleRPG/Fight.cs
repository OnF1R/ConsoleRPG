using ConsoleRPG.Enums;
using ConsoleRPG.Locations;
using ConsoleRPG.Quests;
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

        private static void CheckStunned(params Unit[] units)
        {
            foreach (var unit in units)
            {
                if (unit.IsStunned())
                {
                    unit.ShowMessage($"[bold]{unit.Name} оглушен и не может действовать.[/]");
                    //if (!unit.IsDie()) unit.EffectsUpdate();
                }
            }
        }

        private static void EnemyTeamFightLogic(List<Enemy> enemies, List<Unit> playerWithTeam, Player player)
        {
            var rand = new SerializableRandom();

            foreach (var enemy in enemies.Where(x => x.IsStunned() == false))
            {
                enemy.FightLogic(player, playerWithTeam[rand.Next(playerWithTeam.Count)]);

            }
        }

        private static void EffectsUpdateTurn(List<Enemy> enemies, List<Unit> playerWithTeam)
        {
            foreach (var unit in playerWithTeam)
            {
                if (!unit.IsDie())
                    unit.EffectsUpdate();
            }

            foreach (var unit in enemies)
            {
                if (!unit.IsDie())
                    unit.EffectsUpdate();
            }
        }

        private static Unit ChooseTarget(params Unit[] availableTargets)
        {
            Dictionary<Unit, string> choosesList = new Dictionary<Unit, string>();
            List<string> targetsNames = new List<string>();
            for (int i = 0; i < availableTargets.Length; i++)
            {
                choosesList.Add(availableTargets[i], $"{i + 1}. {availableTargets[i].Name}");
                targetsNames.Add($"{i + 1}. {availableTargets[i].Name}");
            }

            targetsNames.Add("[red]Отмена[/]");

            var choise = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Что будете делать?[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(targetsNames));

            return choosesList.FirstOrDefault(x => x.Value == choise).Key;

        }

        public static void StartFight(Player Player, Enemy Enemy, BaseLocation location)
        {
            bool GiveUp = false;
            location.EnterLocation(Enemy);
            AnsiConsole.MarkupLine("[red]На вас напал[/] [bold]{0}[/] (Уровень: {1})", Enemy.Name, Enemy.Level);
            while (!Player.IsDead && !Enemy.IsDead && !GiveUp)
            {
                if (Player.IsStunned())
                {
                    Player.ShowMessage("[bold]Вы оглушены и не можете действовать.[/]");
                    if (!Enemy.IsStunned()) Enemy.FightLogic(Player, Enemy);
                    if (!Player.IsDie()) Player.EffectsUpdate();
                    if (!Enemy.IsDie()) Enemy.EffectsUpdate();
                }
                else
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
                            foreach (var entity in Player.GetDamageEntities())
                                Player.Attack(Enemy, entity);

                            if (!Enemy.IsStunned()) Enemy.FightLogic(Player, Player);
                            if (!Player.IsDie()) Player.EffectsUpdate();
                            if (!Enemy.IsDie()) Enemy.EffectsUpdate();
                            break;
                        case 2:
                            var spell = Player.ChooseSpell();
                            if (spell != null)
                            {
                                var target = ChooseTarget(Player, Enemy);

                                if (target != null)
                                {
                                    spell.Use(Player, target);
                                    if (!Enemy.IsStunned()) Enemy.FightLogic(Player, Player);
                                    if (!Player.IsDie()) Player.EffectsUpdate();
                                    if (!Enemy.IsDie()) Enemy.EffectsUpdate();
                                }
                            }
                            break;
                        case 3:
                            GiveUp = true;
                            AnsiConsole.MarkupLine("Вы успешно сбежали");
                            break;
                        default:
                            AnsiConsole.MarkupLine("Выберите существующие действие.");
                            break;
                    }
                }
            }

            if (Enemy.IsDead)
            {
                Player.KillCountUpdate(Enemy.ID);
                Player.KillingQuestUpdate(Enemy.ID);

                if (Player.KillCountNumber() > 0 && Player.KillCountNumber() % 15 == 0)
                    Player.AddQuest(new SmallKillingQuest());
            }
        }

        public static void StartTeamFight(Player Player, Enemy Enemy, BaseLocation location)
        {
            
                Enemy.Team.Add(ExistableEnemies.GetRandomEnemy(location.locationId, Player.Level));

            bool GiveUp = false;
            List<Enemy> enemies = Enemy.GetTeamWithMe();
            List<PlayableUnit> playerTeam = Player.Team;

            foreach (var unit in enemies)
            {
                location.EnterLocation(unit);
            }

            if (enemies.Count > 1)
            {
                AnsiConsole.MarkupLine("[red]На вас напала группа врагов:[/]");
                foreach (var unit in enemies)
                {
                    AnsiConsole.MarkupLine($"[bold]{unit.Name}[/] (Уровень: {unit.Level})");
                }
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]На вас напал[/] [bold]{Enemy.Name}[/] (Уровень: {Enemy.Level})");
            }

            while (!Player.IsDead && !GiveUp)
            {
                bool turnEnd = false;
                int teamTurnCount = 0;

                CheckStunned(Player);
                CheckStunned(playerTeam.ToArray());
                CheckStunned(enemies.ToArray());

                if (enemies.Count > 0)
                    enemies.RemoveAll(x => x.IsDead == true);
                else
                    break;

                if (playerTeam.Count > 0)
                    playerTeam.RemoveAll(x => x.IsDead == true);

                foreach (var unit in Player.GetTeamWithMe().Where(x => x.IsStunned() == false))
                {
                    turnEnd = false;

                    if (enemies.Count <= 0)
                        break;

                    while (!turnEnd)
                    {
                        if (GiveUp)
                            break;

                        var choise = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title($"[bold]Ход {unit.Name} \n Что будете делать?[/]")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                            .AddChoices(ActionMenu.Values));

                        switch (ActionMenu.FirstOrDefault(x => x.Value == choise).Key)
                        {
                            case 1:
                                if (enemies.Count > 0)
                                {
                                    var target = ChooseTarget(enemies.ToArray());

                                    if (target != null)
                                    {
                                        foreach (var entity in unit.GetDamageEntities())
                                            unit.Attack(target, entity);
                                        teamTurnCount++;
                                        turnEnd = true;
                                    }
                                }
                                break;
                            case 2:
                                var spell = unit.ChooseSpell();
                                if (spell != null)
                                {
                                    List<Unit> units = new();
                                    units.Add(Player);
                                    units.AddRange(playerTeam);
                                    units.AddRange(enemies);

                                    var target = ChooseTarget(units.ToArray());

                                    if (target != null)
                                    {
                                        turnEnd = true;
                                        spell.Use(unit, target);
                                        teamTurnCount++;
                                    }
                                }
                                break;
                            case 3:
                                if (teamTurnCount > 0)
                                {
                                    AnsiConsole.MarkupLine("[red]Вы не можете сбежать, так как сделали ход одним из участников команды![/]");
                                }
                                else
                                {
                                    GiveUp = true;
                                    AnsiConsole.MarkupLine("[lime]Вы успешно сбежали[/]");
                                }
                                break;
                            default:
                                AnsiConsole.MarkupLine("Выберите существующие действие.");
                                break;
                        }

                        if (enemies.Count > 0)
                        {
                            foreach (var enemy in enemies)
                            {
                                if (enemy.IsDead == true)
                                    enemy.Death(Player);
                            }

                            enemies.RemoveAll(x => x.IsDead == true);
                        }

                        else
                            break;

                        if (playerTeam.Count > 0)
                        {
                            foreach (var teammate in playerTeam)
                            {
                                if (teammate.IsDead == true)
                                    teammate.Death();
                            }

                            playerTeam.RemoveAll(x => x.IsDead == true);
                        }

                    }
                }

                if (turnEnd)
                {
                    EnemyTeamFightLogic(enemies, Player.GetTeamWithMe(), Player);
                    EffectsUpdateTurn(enemies, Player.GetTeamWithMe());
                    turnEnd = false;
                    teamTurnCount = 0;
                }
            }



            foreach (var unit in enemies)
                location.ExitLocation(unit);
        }

    }
}
