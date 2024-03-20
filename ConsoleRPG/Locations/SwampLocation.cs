using ConsoleRPG.Enums;
using System.Xml.Linq;

namespace ConsoleRPG.Locations
{
    [Serializable]
    internal class SwampLocation : BaseLocation
	{
		public SwampLocation()
		{
			Name = "Болото";
			locationId = LocationIdentifier.Swamp;
			AddComponent(new ElementalResistanceCharacteristic()
			{
				ElemResistance = new Dictionary<DamageTypes, int>()
				{
					{ DamageTypes.Nature, -50 },
					{ DamageTypes.Earth, -50 },
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
			player.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Nature] +=
				GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Nature];

			player.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Earth] +=
				GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Earth];
		}

		public void RemoveLocationEffect(Player player)
		{
			player.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Nature] -=
				GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Nature];

			player.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Earth] -=
				GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Earth];
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
			string description = "Болото "; // here
			return description;
		}

		public override string LocationEffects()
		{
			string effects = $"Прибывая в болоте игрок получает -50% к сопротивлению [{new DamageTypesNames().Color[DamageTypes.Earth]}]земле[/] и -50% к сопротивлению [{new DamageTypesNames().Color[DamageTypes.Nature]}]природе[/].";
			return effects;
		}
	}
}
