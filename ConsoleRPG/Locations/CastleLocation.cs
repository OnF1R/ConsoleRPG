using ConsoleRPG.Enemies;
using ConsoleRPG.NPC_s;
using ConsoleRPG.Enums;

namespace ConsoleRPG.Locations
{
    [Serializable]
    internal class CastleLocation : BaseLocation
	{
		public CastleLocation()
		{
			Name = "Замок";
			locationId = LocationIdentifier.Castle;
			AddComponent(new ArmorCharacteristic() { Armor = 5 });
			AddComponent(new ParryCharacteristic() { ParryPercent = 25 });
		}

		public override Enemy GetRandomLocationEnemy(int playerLevel)
		{
			return ExistableEnemies.GetRandomEnemy(locationId, playerLevel);
		}

		public override void ApplyLocationEffect(Unit unit)
		{
			unit.GetComponent<ArmorCharacteristic>().Armor += GetComponent<ArmorCharacteristic>().Armor;
			unit.GetComponent<ParryCharacteristic>().ParryPercent += GetComponent<ParryCharacteristic>().ParryPercent;
		}

		public override void RemoveLocationEffect(Unit unit)
		{
			unit.GetComponent<ArmorCharacteristic>().Armor -= GetComponent<ArmorCharacteristic>().Armor;
			unit.GetComponent<ParryCharacteristic>().ParryPercent -= GetComponent<ParryCharacteristic>().ParryPercent;
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
			string description = "Замок, в прошлом место собрания элит за круглым столом для решения проблем континента, королевств и " +
				"государств, нынче разрушенное и грязное место, где обитают выжившие рыцари.";
			return description;
		}

		public override string LocationEffects()
		{
			string effects = "Прибывая в замке существа получают +5 к броне и +25% шанс парировать атаку.";
			return effects;
		}
	}
}
