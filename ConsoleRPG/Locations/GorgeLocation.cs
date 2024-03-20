using ConsoleRPG.Enums;

namespace ConsoleRPG.Locations
{
    [Serializable]
    internal class GorgeLocation : BaseLocation
	{
		public GorgeLocation()
		{
			Name = "Ущелье";
			locationId = LocationIdentifier.Gorge;
			AddComponent(new ElementalResistanceCharacteristic()
			{
				ElemResistance = new Dictionary<DamageTypes, int>()
				{
					{ DamageTypes.Dark, -150 },
				}
			});
		}

		public override Enemy GetRandomLocationEnemy(int playerLevel)
		{
			return ExistableEnemies.GetRandomEnemy(locationId, playerLevel);
		}

		public override void ApplyLocationEffect(Unit unit) { }

		public override void RemoveLocationEffect(Unit unit) { }

		public override void EnterLocation(Unit unit)
		{
			ApplyLocationEffect(unit);
		}

		public void ApplyLocationEffect(Player player)
		{
			player.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Dark] +=
				GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Dark];
		}

		public void RemoveLocationEffect(Player player)
		{
			player.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Dark] -=
				GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Dark];
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
			string description = "Ущелье "; // here
			return description;
		}

		public override string LocationEffects()
		{
			string effects = $"В ущелье темно и страшно, игрок получает -150% к сопротивлению [{new DamageTypesNames().Color[DamageTypes.Dark]}]тьме[/].";
			return effects;
		}
	}
}
