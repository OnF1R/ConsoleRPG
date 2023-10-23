using ConsoleRPG.Interfaces;

namespace ConsoleRPG.Effects.Buffs
{
    internal class Fossilization : BaseEffect, IHealDealerEntity
    {
        private int ArmorBuff;
        private double EvasionDebuff;


		public Fossilization(int duration = 2)
        {
            Name = "[bold]Окаменение[/]";
            Duration = duration;
            CurrentDuration = Duration;
            EffectType = EffectType.Stun;

            AddComponent(new ArmorCharacteristic() { Armor = 65 });
            AddComponent(new EvasionCharacteristic() { EvasionChance = -25 });

            ArmorBuff = GetComponent<ArmorCharacteristic>().Armor;
            EvasionDebuff = GetComponent<EvasionCharacteristic>().EvasionChance;
        }

        protected override void StartEffect(Unit unit)
        {
            unit.GetComponent<ArmorCharacteristic>().Armor += ArmorBuff;
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
			unit.GetComponent<ArmorCharacteristic>().Armor -= ArmorBuff;
			unit.GetComponent<EvasionCharacteristic>().EvasionChance -= EvasionDebuff;
            unit.Heal((int)(unit.MaxHealth * 0.2), this);
		}

        public override string DescriptionEffect()
        {
            string message = $"{Name}: [bold]Оглушает[/], увеличивает физическую броню на {ArmorBuff}, но уменьшает шанс уклонения на {EvasionDebuff}%";
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
            return new Fossilization();
		}
	}
}
