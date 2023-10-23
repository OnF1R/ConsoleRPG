using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DefenceSpells
{
	internal class Fossilization : BaseSpell
    {
        public Fossilization()
        {
            Name = "[bold]Окаменение[/]";

			ID = Enums.SpellIdentifierEnum.Fossilization;

            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
				{ new Effects.Buffs.Fossilization(), 100 },
            }));
        }

		public override void Use(Unit caster, Unit target)
		{
			foreach (var effect in GetEffects().Keys)
				target.AddBuff(effect.Clone());
		}
	}
}
