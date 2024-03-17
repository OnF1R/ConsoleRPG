using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DefenceSpells
{
    [Serializable]
    internal class IceBlock : BaseSpell
    {
        public IceBlock()
        {
            Name = "[aqua]Ледяная[/] глыба";

			ID = Enums.SpellIdentifierEnum.Iceblock;

            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
				{ new Effects.Buffs.IceBlock(), 100 },
            }));
        }

		public override void Use(Unit caster, Unit target)
		{
			foreach (var effect in GetEffects().Keys)
				target.AddBuff(effect.Clone());
		}
	}
}
