namespace ConsoleRPG.Effects.Debuffs
{
	internal class Wet : BaseEffect
	{
		private int WaterResistanceDebuff;
		private int ElectricResistanceDebuff;
		private Dictionary<DamageTypes, string> Names;

		public Wet(int duration = 2)
		{
			Name = "[dodgerblue1]Промокание[/]";
			Duration = duration;
			CurrentDuration = Duration;
			EffectType = EffectType.Debuff;

			Names = new DamageTypesNames().Names;

			AddComponent(new ElementalResistanceCharacteristic
			{
				ElemResistance = new Dictionary<DamageTypes, int>()
					{
						{ DamageTypes.Water, -25},
						{ DamageTypes.Electric, -50},
					}
			});

			WaterResistanceDebuff = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == DamageTypes.Water).Value;
			ElectricResistanceDebuff = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == DamageTypes.Electric).Value;
		}

		protected override void StartEffect(Unit unit)
		{
			unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Water] += WaterResistanceDebuff;
			unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Electric] += ElectricResistanceDebuff;
		}

		public override void ApplyEffect(Unit unit)
		{
			if (Duration == CurrentDuration)
				StartEffect(unit);

			CurrentDuration--;

			unit.ShowMessage(EffectDurationMessage(unit.Name));

			if (CurrentDuration <= 0)
				EndEffect(unit);
		}

		protected override void EndEffect(Unit unit)
		{
			unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Water] -= WaterResistanceDebuff;
			unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Electric] -= ElectricResistanceDebuff;
		}

		public override string DescriptionEffect()
		{
			string message = $"{Name}: уменьшает сопротивление ({Names[DamageTypes.Water]}) на {WaterResistanceDebuff}% и ({Names[DamageTypes.Electric]}) на {ElectricResistanceDebuff}%";
			return message;
		}

		private string EffectMessage(Unit unit)
		{
			string message = $"{unit.Name} промок от эффекта {Name}";

			if (Duration > 0)
			{
				message += $", осталось ходов: {Duration}";
			}
			else
			{
				message += ", последний ход!";
			}

			return message;
		}

		public override BaseEffect Clone()
		{
			return new Wet();
		}
	}
}
