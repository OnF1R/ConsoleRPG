using ConsoleRPG.Enums;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;

namespace ConsoleRPG.Quests
{
	internal class SmallKillingQuest : Quest
	{
		Random random = new Random();

		List<string> questNames = new List<string>()
		{
			"Небольшое поручение",
			"Просьба друга",
			"Охота",
			"Помощь в маленьком деле",
			"Слабое звено",
		};

		Gold gold = new Gold();

		public SmallKillingQuest()
		{
			QuestCondition();
			Name = questNames[random.Next(questNames.Count)];
			QuestType = QuestType.Killing;
			Experience = new Random().Next(1 * GetComponent<QuestKillCondition>().RequiredKillCount, 4 * GetComponent<QuestKillCondition>().RequiredKillCount);
		}

		public override void GetReward(Player player)
		{
			gold.Count = new Random().Next(1, 6);

			List<Item> reward = new List<Item>()
			{
				new IronOre(),
				new IronIngot(),
				new RainbowShard(),
				new BloodStone(),
				new AdamantiteOre(),
			};

			player.Inventory.AddItem(gold);
			player.Inventory.AddItem(reward[random.Next(reward.Count)]);

			player.TakeExp(Experience);
		}

		public override bool IsCompleteQuest()
		{
			if (GetComponent<QuestKillCondition>().CurrentKillCount >= GetComponent<QuestKillCondition>().RequiredKillCount)
			{
				return true;
			}

			return false;
		}

		public override string Description()
		{
			var desription = $"Устранить {ExistableEnemies.EnemyNames[GetComponent<QuestKillCondition>().RequiredEnemyKillType]}: " +
				$"{GetComponent<QuestKillCondition>().CurrentKillCount}/{GetComponent<QuestKillCondition>().RequiredKillCount}";
			return desription;
		}

		public override void QuestCondition()
		{
			EnemyIdentifierEnum[] allEnemyIdentifier = Enum.GetValues(typeof(EnemyIdentifierEnum)).Cast<EnemyIdentifierEnum>().ToArray();
			EnemyIdentifierEnum enemyID = allEnemyIdentifier[random.Next(allEnemyIdentifier.Length)];

			int requiredKillCount = random.Next(4, 9);

			AddComponent(new QuestKillCondition() { RequiredKillCount = requiredKillCount, RequiredEnemyKillType = enemyID });
		}
	}
}
