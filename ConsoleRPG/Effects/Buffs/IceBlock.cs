using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Effects.Buffs
{
    internal class IceBlock : BaseEffect, IHealDealerEntity
    {
        private int ArmorBuff;
        private int FrostResistanceBuff;
        private int WaterResistanceBuff;
        private int FireResistanceDebuff;
		private Dictionary<DamageTypes, string> Names;

		public IceBlock(int duration = 2)
        {
            Name = "[aqua]Ледяная[/] глыба";
            Duration = duration;
            CurrentDuration = Duration;
            EffectType = EffectType.Stun;
			Names = new DamageTypesNames().Names;
			AddComponent(new ArmorCharacteristic() { Armor = 65 });

			AddComponent(new ElementalResistanceCharacteristic
			{
				ElemResistance = new Dictionary<DamageTypes, int>()
					{
						{ DamageTypes.Frost, 80},
						{ DamageTypes.Water, 40},
						{ DamageTypes.Fire, -80},
					}
			});
			ArmorBuff = GetComponent<ArmorCharacteristic>().Armor;

			FrostResistanceBuff = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == DamageTypes.Frost).Value;
			WaterResistanceBuff = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == DamageTypes.Water).Value;
			FireResistanceDebuff = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == DamageTypes.Fire).Value;
		}

        protected override void StartEffect(Unit unit)
        {
            unit.GetComponent<ArmorCharacteristic>().Armor += ArmorBuff;
			unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Frost] += FrostResistanceBuff;
			unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Water] += WaterResistanceBuff;
			unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Fire] += FireResistanceDebuff;

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
			unit.GetComponent<ArmorCharacteristic>().Armor -= ArmorBuff;
			unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Frost] -= FrostResistanceBuff;
			unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Water] -= WaterResistanceBuff;
			unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Fire] -= FireResistanceDebuff;
			unit.Heal((int)(unit.MaxHealth * 0.2), this);
		}

        public override string DescriptionEffect()
        {
            string message = $"{Name}: [bold]Оглушает[/], увеличивает физическую броню на {ArmorBuff}, сопротивление ({Names[DamageTypes.Frost]}) на {FrostResistanceBuff} и ({Names[DamageTypes.Water]}) на {WaterResistanceBuff}, но уменьшает сопротивление ({Names[DamageTypes.Fire]}) на {FireResistanceDebuff}%";
            return message;
        }

        private string EffectMessage(Unit unit)
        {
            string message = $"{unit.Name} получил эффект {Name} ";
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
			return new IceBlock();
		}
	}
}
