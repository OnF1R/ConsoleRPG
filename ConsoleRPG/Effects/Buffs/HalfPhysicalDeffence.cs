namespace ConsoleRPG.Effects.Debuffs
{
    internal class HalfPhysicalDeffence : BaseEffect
    {
        private int ArmorBuff;

        public HalfPhysicalDeffence(int duration = 2)
        {
            Name = "[bold]Частичная блокировка[/]";
            Duration = duration;
            CurrentDuration = Duration;
            EffectType = EffectType.Buff;

            AddComponent(new ArmorCharacteristic() { Armor = 25 });

            ArmorBuff = GetComponent<ArmorCharacteristic>().Armor;
        }

        protected override void StartEffect(Unit unit)
        {
            unit.GetComponent<ArmorCharacteristic>().Armor += ArmorBuff;
        }

        public override void ApplyEffect(Unit unit)
        {
            if (Duration == CurrentDuration)
                StartEffect(unit);

            CurrentDuration--;

            unit.ShowMessage(EffectDurationMessage());

            if (CurrentDuration <= 0)
                EndEffect(unit);
        }

        protected override void EndEffect(Unit unit)
        {
            unit.GetComponent<ArmorCharacteristic>().Armor -= ArmorBuff;
        }

        public override string DescriptionEffect()
        {
            string message = $"{Name}: увеличивает физическую броню на {ArmorBuff}";
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
    }
}
