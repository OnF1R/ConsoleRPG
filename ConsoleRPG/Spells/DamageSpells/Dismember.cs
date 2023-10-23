
using ConsoleRPG.Effects;
using ConsoleRPG.Effects.Stun;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class Dismember : BaseSpell
    {
        public Dismember()
        {
            Name = "[bold red]Расчленение[/]";
			ID = Enums.SpellIdentifierEnum.Dismember;

			AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
			{
				{ DamageTypes.Physical, new Random().Next(3,8)},
			}));

			AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
			{
				{ new Effects.Stun.Dismember(), 100 },
			}));
		}

		public override void Use(Unit caster, Unit target)
		{
			int damage = GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
			caster.DealDamage(target, damage, DamageTypes.Physical, this);

			caster.Heal(damage, this);
		}
	}
}
