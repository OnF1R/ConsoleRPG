using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DefenceSpells
{
	internal class SmallPhysicalDefence : BaseSpell
    {
        public SmallPhysicalDefence()
        {
            Name = "[bold]Частичная блокировка[/]";

			ID = Enums.SpellIdentifierEnum.SmallPhysicalDefence;

            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
				{ new Effects.Buffs.SmallPhysicalDefence(), 100 },
            }));
        }

		public override void Use(Unit caster, Unit target)
		{
			foreach (var effect in GetEffects().Keys)
				target.AddBuff(effect.Clone());
		}
	}
}
