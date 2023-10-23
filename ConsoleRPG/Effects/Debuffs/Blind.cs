namespace ConsoleRPG.Effects.Debuffs
{
    internal class Blind : BaseEffect
    {
        private double MissChance;
        public Blind(int duration = 2)
        {
            Name = "[grey]Слепота[/]";
            Duration = duration;
            CurrentDuration = Duration;
            EffectType = EffectType.Debuff;

            AddComponent(new MissCharacteristic() { MissChance = 50} );

			MissChance = GetComponent<MissCharacteristic>().MissChance;
		}

        protected override void StartEffect(Unit unit)
        {
            unit.GetComponent<MissCharacteristic>().MissChance += MissChance;
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
			unit.GetComponent<MissCharacteristic>().MissChance -= MissChance;
		}

		public override string DescriptionEffect()
        {
            string message = $"{Name}: Увеличивает шанс промаха на {MissChance}%";
            return message;
        }

		public override BaseEffect Clone()
		{
			return new Blind();
		}
	}
}
