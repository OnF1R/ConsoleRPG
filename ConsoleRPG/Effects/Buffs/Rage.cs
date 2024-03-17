namespace ConsoleRPG.Effects.Buffs
{
    [Serializable]
    internal class Rage : BaseEffect
    {
        private double CriticalDamageBuff;
        private double CriticalChanceBuff;

        public Rage(int duration = 2)
        {
            Name = "[red]Ярость[/]";
            Duration = duration;
            CurrentDuration = Duration;
            EffectType = EffectType.Buff;

            AddComponent(new CriticalChanceCharacteristic() { CriticalChance = 25 });
            AddComponent(new CriticalDamageCharacteristic() { CriticalDamage = 50 });

            CriticalDamageBuff = GetComponent<CriticalDamageCharacteristic>().CriticalDamage;
            CriticalChanceBuff = GetComponent<CriticalChanceCharacteristic>().CriticalChance;
        }

        protected override void StartEffect(Unit unit)
        {
            unit.GetComponent<CriticalDamageCharacteristic>().CriticalDamage += CriticalDamageBuff;
            unit.GetComponent<CriticalChanceCharacteristic>().CriticalChance += CriticalChanceBuff;
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
            unit.GetComponent<CriticalDamageCharacteristic>().CriticalDamage -= CriticalDamageBuff;
            unit.GetComponent<CriticalChanceCharacteristic>().CriticalChance -= CriticalChanceBuff;
        }

        public override string DescriptionEffect()
        {
            string message = $"{Name}: увеличивает шанс критического удара на {CriticalChanceBuff}% и критический урон на {CriticalDamageBuff}%";
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
			return new Rage();
		}
	}
}
