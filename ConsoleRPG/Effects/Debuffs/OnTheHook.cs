using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Effects.Debuffs
{
    [Serializable]
    internal class OnTheHook : BaseEffect, IDamageDealerEntity
    {
        private double EvasionDebuff;

        public OnTheHook(int duration = 2)
        {
            Name = "[bold]На крючке![/]";
            Duration = duration;
            CurrentDuration = Duration;
            EffectType = EffectType.Debuff;
            AddComponent(new EvasionCharacteristic() { EvasionChance = -50 });

            EvasionDebuff = GetComponent<EvasionCharacteristic>().EvasionChance;
        }

        protected override void StartEffect(Unit unit)
        {
            unit.GetComponent<EvasionCharacteristic>().EvasionChance += EvasionDebuff;
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
            unit.GetComponent<EvasionCharacteristic>().EvasionChance -= EvasionDebuff;
		}

		public override string DescriptionEffect()
        {
            string message = $"{Name}: Уменьшает шанс уклонения на {Math.Abs(EvasionDebuff)}%)";
            return message;
        }

		public override BaseEffect Clone()
		{
			return new OnTheHook();
		}
	}
}
