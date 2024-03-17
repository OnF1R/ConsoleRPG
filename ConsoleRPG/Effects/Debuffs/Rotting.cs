namespace ConsoleRPG.Effects.Debuffs
{
    [Serializable]
    internal class Rotting : BaseEffect
    {
        private int BaseDamage;
        private int NecroticResistanceDebuff;
        private Dictionary<DamageTypes, string> Names;

        public Rotting(int duration = 2)
        {
            Name = "[yellow4]Гниение[/]";
            Duration = duration;
            CurrentDuration = Duration;
            EffectType = EffectType.Debuff;

            Names = new DamageTypesNames().Names;
            AddComponent(new ElementalDamageCharacteristic
            {
                ElemDamage = new Dictionary<DamageTypes, int>()
                    {
                        { DamageTypes.Necrotic, 3},
                    }
            });

            AddComponent(new ElementalResistanceCharacteristic
            {
                ElemResistance = new Dictionary<DamageTypes, int>()
                    {
                        { DamageTypes.Necrotic, -30},
                    }
            });

            BaseDamage = GetComponent<ElementalDamageCharacteristic>().ElemDamage.FirstOrDefault(x => x.Key == DamageTypes.Necrotic).Value;
			NecroticResistanceDebuff = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == DamageTypes.Necrotic).Value;
        }

        protected override void StartEffect(Unit unit)
        {
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Necrotic] += NecroticResistanceDebuff;
        }

        public override void ApplyEffect(Unit unit)
        {
            if (Duration == CurrentDuration)
                StartEffect(unit);

            CurrentDuration--;

            unit.DealDamage(unit, BaseDamage, DamageTypes.Necrotic, this);

            unit.ShowMessage(EffectDurationMessage(unit.Name));

            if (CurrentDuration <= 0)
                EndEffect(unit);
        }

        protected override void EndEffect(Unit unit)
        {
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Necrotic] -= NecroticResistanceDebuff;
        }

        public override string DescriptionEffect()
        {
            string message = $"{Name}: Наносит {BaseDamage} урон от ({Names[DamageTypes.Necrotic]}), " +
                $"также уменьнает сопротивление ({Names[DamageTypes.Necrotic]}) на {NecroticResistanceDebuff}%";
            return message;
        }

		public override BaseEffect Clone()
		{
			return new Rotting();
		}
	}
}
