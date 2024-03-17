using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Effects.Debuffs
{
    [Serializable]
    internal class Burn : BaseEffect, IDamageDealerEntity
	{
        private int BaseDamage;
        private int FireResistanceDebuff;
        private int WaterResistanceBuff;
        private Dictionary<DamageTypes, string> Names;

        public Burn(int duration = 2)
        {
            Name = "[orangered1]Поджог[/]";
            Duration = duration;
            CurrentDuration = Duration;
            EffectType = EffectType.Debuff;

            Names = new DamageTypesNames().Names;
            AddComponent(new ElementalDamageCharacteristic
            {
                ElemDamage = new Dictionary<DamageTypes, int>()
                    {
                        { DamageTypes.Fire, 2},
                    }
            });

            AddComponent(new ElementalResistanceCharacteristic
            {
                ElemResistance = new Dictionary<DamageTypes, int>()
                    {
                        { DamageTypes.Fire, -10},
                        { DamageTypes.Water, 20},
                    }
            });

            BaseDamage = GetComponent<ElementalDamageCharacteristic>().ElemDamage.FirstOrDefault(x => x.Key == DamageTypes.Fire).Value;
            WaterResistanceBuff = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == DamageTypes.Water).Value;
            FireResistanceDebuff = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == DamageTypes.Fire).Value;
        }

        protected override void StartEffect(Unit unit)
        {
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Fire] += FireResistanceDebuff;
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Water] += WaterResistanceBuff;
        }

        public override void ApplyEffect(Unit unit)
        {
            if (Duration == CurrentDuration)
                StartEffect(unit);

            CurrentDuration--;

            unit.DealDamage(unit, BaseDamage, DamageTypes.Fire, this);

            unit.ShowMessage(EffectDurationMessage(unit.Name));

            if (CurrentDuration <= 0)
                EndEffect(unit);
        }

        protected override void EndEffect(Unit unit)
        {
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Fire] -= FireResistanceDebuff;
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Water] -= WaterResistanceBuff;
        }

        public override string DescriptionEffect()
        {
            string message = $"{Name}: Наносит {BaseDamage} урон от ({Names[DamageTypes.Fire]}), " +
                $"также уменьнает сопротивление ({Names[DamageTypes.Fire]}) на {FireResistanceDebuff}% и увеличивает сопротивление ({Names[DamageTypes.Water]}) на {WaterResistanceBuff}%";
            return message;
        }

		public override BaseEffect Clone()
		{
			return new Burn();
		}
	}
}
