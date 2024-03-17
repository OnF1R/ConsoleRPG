using ConsoleRPG.Enums;

namespace ConsoleRPG.Quests
{
    [Serializable]
    abstract class QuestConditions { };
   
	[Serializable]
    class QuestKillCondition : QuestConditions
	{
		public int RequiredKillCount { get; set; }
		public int CurrentKillCount { get; set; } = 0;
		public EnemyIdentifierEnum RequiredEnemyKillType { get; set; }
	}

    [Serializable]

    class QuestCollectItemCondition : QuestConditions
	{
		public Dictionary<ItemIdentifier, int> ItemsCountCollect { get; set; }
	}
}
