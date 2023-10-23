using ConsoleRPG.Enums;
using ConsoleRPG.Locations;
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

		private static Unit ChooseSpellTarget(params Unit[] availableTargets)
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

		}  // TODO: Сделать нормальное использование заклинаний для всех юнитов, использование на врага, себя и т.д.

		public static void StartFight(Player Player, Enemy Enemy, BaseLocation location, ref int EnemiesKillsCount)
		{
			bool GiveUp = false;
			location.EnterLocation(Enemy);
			AnsiConsole.MarkupLine("[red]На вас напал[/] [bold]{0}[/] (Уровень: {1})", Enemy.Name, Enemy.Level);
			while (!Player.IsDead && !Enemy.IsDead && !GiveUp)
			{
				if (Player.IsStunned())
				{
					Player.ShowMessage("[bold]Вы оглушены и не можете действовать.[/]");
					if (!Enemy.IsStunned()) Enemy.FightLogic(Player);
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

							if (!Enemy.IsStunned()) Enemy.FightLogic(Player);
							if (!Player.IsDie()) Player.EffectsUpdate();
							if (!Enemy.IsDie()) Enemy.EffectsUpdate();
							break;
						case 2:
							var spell = Player.ChooseSpell();
							if (spell != null)
							{
								var target = ChooseSpellTarget(Player, Enemy);

								if (target != null)
								{
									spell.Use(Player, target);
									if (!Enemy.IsStunned()) Enemy.FightLogic(Player);
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
			}


		}
	}
}
