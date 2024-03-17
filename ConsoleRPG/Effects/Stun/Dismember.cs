namespace ConsoleRPG.Effects.Stun
{
    [Serializable]
    internal class Dismember : BaseEffect
    {
        public Dismember(int duration = 2)
        {
            Name = "[bold]Расчленение[/]";
            Duration = duration;
            CurrentDuration = Duration;
            EffectType = EffectType.Stun;
		}

        protected override void StartEffect(Unit unit)
        {
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
		}

		public override string DescriptionEffect()
        {
            string message = $"{Name}: Оглушает цель.";
            return message;
        }

		public override BaseEffect Clone()
		{
			return new Dismember();
		}
	}
}
