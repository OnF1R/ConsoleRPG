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

        public static void UseSpell(BaseSpell spell, Unit DamageTaker, Unit DamageDealer)
        {
			Dictionary<DamageTypes, int> elemDamage = spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
			foreach (DamageTypes type in elemDamage.Keys)
			{
                DamageDealer.DealDamage(DamageTaker, elemDamage[type], type, spell);
			}
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
                        foreach (var entity in Player.GetDamageEntities())
                            Player.Attack(Enemy, entity);

                        Enemy.FightLogic(Player);
                        Player.EffectsUpdate();
                        Enemy.EffectsUpdate();
                        break;
                    case 2:
                        var spell = Player.ChooseSpell();
                        if (spell != null)
                        {
                            UseSpell(spell, Enemy, Player);
							Enemy.FightLogic(Player);
							Player.EffectsUpdate();
							Enemy.EffectsUpdate();
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

            if (Enemy.IsDead)
                EnemiesKillsCount++;
        }
    }
}
