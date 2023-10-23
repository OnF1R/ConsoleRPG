using static ConsoleRPG.Enums.LocationIdentifierEnum;
using System.Xml.Linq;

namespace ConsoleRPG.Locations
{
	internal class MageTowerLocaiton : BaseLocation
	{
		public MageTowerLocaiton()
		{
			Name = "Башня магов";
			locationId = LocationIdentifier.MageTower;
			AddComponent(new MagicAmplificationCharacteristic() { Amplification = 75 });
		}

		public override Enemy GetRandomLocationEnemy(int playerLevel)
		{
			return ExistableEnemies.GetRandomEnemy(locationId, playerLevel);
		}

		public override void ApplyLocationEffect(Unit unit)
		{
			unit.GetComponent<MagicAmplificationCharacteristic>().Amplification += GetComponent<MagicAmplificationCharacteristic>().Amplification;
		}

		public override void RemoveLocationEffect(Unit unit) 
		{
			unit.GetComponent<MagicAmplificationCharacteristic>().Amplification -= GetComponent<MagicAmplificationCharacteristic>().Amplification;
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
			string description = "Башня магов"; // here
			return description;
		}

		public override string LocationEffects()
		{
			string effects = $"Пассивная аура башни магов увеличивает силу заклинаний существ внутри неё на 75%.";
			return effects;
		}
	}
}
