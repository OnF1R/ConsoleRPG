using ConsoleRPG.Enums;

namespace ConsoleRPG.Quests
{
	abstract class QuestConditions { };

	class QuestKillCondition : QuestConditions
	{
		public int RequiredKillCount { get; set; }
		public int CurrentKillCount { get; set; } = 0;
		public EnemyIdentifierEnum RequiredEnemyKillType { get; set; }
	}

	class QuestCollectItemCondition : QuestConditions
	{
		public Dictionary<ItemIdentifier, int> ItemsCountCollect { get; set; }
	}
}
