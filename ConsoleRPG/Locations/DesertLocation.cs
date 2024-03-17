using static ConsoleRPG.Enums.LocationIdentifierEnum;

namespace ConsoleRPG.Locations
{
    [Serializable]
    internal class DesertLocation : BaseLocation
	{
		public DesertLocation()
		{
			Name = "[orange1]Пустыня[/]";
			locationId = LocationIdentifier.Desert;
			AddComponent(new MissCharacteristic() { MissChance = 15 });
			AddComponent(new EvasionCharacteristic() { EvasionChance = 15 });
		}

		public override Enemy GetRandomLocationEnemy(int playerLevel)
		{
			return ExistableEnemies.GetRandomEnemy(locationId, playerLevel);
		}

		public override void ApplyLocationEffect(Unit unit)
		{
			unit.GetComponent<MissCharacteristic>().MissChance += GetComponent<MissCharacteristic>().MissChance;
			unit.GetComponent<EvasionCharacteristic>().EvasionChance += GetComponent<EvasionCharacteristic>().EvasionChance;
		}

		public override void RemoveLocationEffect(Unit unit)
		{
			unit.GetComponent<MissCharacteristic>().MissChance -= GetComponent<MissCharacteristic>().MissChance;
			unit.GetComponent<EvasionCharacteristic>().EvasionChance -= GetComponent<EvasionCharacteristic>().EvasionChance;
		}

		public override void EnterLocation(Unit unit)
		{
			ApplyLocationEffect(unit);
		}

		public override void ExitLocation(Unit unit)
		{
			RemoveLocationEffect(unit);
		}

		public override void EnterLocation(Player player)
		{
			//ConsoleMessages.Message(LocationDesription());
			player.ShowMessage(LocationEffects());
			ApplyLocationEffect(player);
			player.ShowMessage($"{player.Name} прибыл в локацию [bold]{Name}[/]");
		}

		public override void ExitLocation(Player player)
		{
			RemoveLocationEffect(player);
			player.ShowMessage($"{player.Name} покинул локацию {Name}.");
		}

		public override string LocationDesription()
		{
			string description = "В пустыне обитают восточные бандиты, потерявшие свой дом, а также жуткие пустынные насекомые и млекопитающие";
			return description;
		}

		public override string LocationEffects()
		{
			string effects = "Прибывая в пустыне существа получают 15% к промаху и 15% к уклонению.";
			return effects;
		}
	}
}
