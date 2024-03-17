using ConsoleRPG.Enums;

namespace ConsoleRPG.Quests
{
    [Serializable]
    abstract class Quest
	{
		public string Name { get; set; }

		public QuestType QuestType { get; set; }

		public int Experience { get; set; }

		public List<Item> Reward { get; set; }

		public bool IsComplete { get; set; } = false;

		public abstract void QuestCondition();

		public abstract void GetReward(Player player);

		public abstract bool IsCompleteQuest();

		public abstract string Description();

		public void CompleteQuest(Player player)
		{
			player.ShowMessage($"Задание {Name} [lime]выполнено[/]");
			IsComplete = true;
			GetReward(player);
		}

		public Quest AddComponent(QuestConditions component)
		{
			_components.Add(component);
			return this;
		}

		public T GetComponent<T>()
		{
			return (T)_components.OfType<T>().FirstOrDefault();
		}

		private List<QuestConditions> _components = new List<QuestConditions>();
	}
}
