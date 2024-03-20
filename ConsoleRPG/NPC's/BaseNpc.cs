using ConsoleRPG.Enums;
using ConsoleRPG.Quests;
using Spectre.Console;

namespace ConsoleRPG.NPC_s
{
	[Serializable]
	abstract class BaseNpc : Enemy
	{
		protected BaseNpc(int level) : base(level)
		{
		}

		protected SerializableRandom random = new SerializableRandom();

		protected Dictionary<int, string> DialogueChoises = MenuChoises.StandartDialogueChoises();

		public NpcTypeEnum Type { get; set; }

		public List<string> GreetingPhrases { get; set; }

		public List<string> DialoguePhrases { get; set; }

		public List<Quest> Quests { get; set; }

		public virtual void Dialogue()
		{
			ConsoleMessages.PrintMessage(DialoguePhrases[random.Next(DialoguePhrases.Count)]);
		}

		public virtual void ActionMenu(Player player)
		{
			bool loop = true;

			while (loop || !IsDie())
			{
				var choise = AnsiConsole.Prompt(
					new SelectionPrompt<string>()
						.Title($"[bold]{Name}: {GreetingPhrases[random.Next(GreetingPhrases.Count)]}[/]")
						.MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
						.AddChoices(DialogueChoises.Values));

				Console.Clear();

				switch (DialogueChoises.FirstOrDefault(x => x.Value == choise).Key)
				{
					case 1:
						Dialogue();
						break;
					case 2:
						//Fight.StartFight(player, this, ref Game.EnemiesKills);
						break;
					default:
						loop = false;
						break;
				}
			}
		}
	}
}
