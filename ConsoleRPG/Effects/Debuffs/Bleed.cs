using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Effects.Debuffs
{
    [Serializable]
    internal class Bleed : BaseEffect, IDamageDealerEntity
    {
        private int BaseDamage;
        private Dictionary<DamageTypes, string> Names;

        public Bleed(int duration = 2)
        {
            Name = "[red]Кровотечение[/]";
            Duration = duration;
            CurrentDuration = Duration;
            EffectType = EffectType.Debuff;

            Names = new DamageTypesNames().Names;
            AddComponent(new ElementalDamageCharacteristic
            {
                ElemDamage = new Dictionary<DamageTypes, int>()
                    {
                        { DamageTypes.Physical, 4},
                    }
            });

            BaseDamage = GetComponent<ElementalDamageCharacteristic>().ElemDamage.FirstOrDefault(x => x.Key == DamageTypes.Physical).Value;
        }

        protected override void StartEffect(Unit unit)
        {
        }

        public override void ApplyEffect(Unit unit)
        {
            if (Duration == CurrentDuration)
                StartEffect(unit);

            CurrentDuration--;

            unit.DealDamage(unit, BaseDamage, DamageTypes.Physical, this);

            unit.ShowMessage(EffectDurationMessage(unit.Name));

            if (CurrentDuration <= 0)
                EndEffect(unit);
        }

        protected override void EndEffect(Unit unit)
        {
        }

        public override string DescriptionEffect()
        {
            string message = $"{Name}: Наносит {BaseDamage} урон от ({Names[DamageTypes.Physical]})";
            return message;
        }

		public override BaseEffect Clone()
		{
			return new Bleed();
		}
	}
}
