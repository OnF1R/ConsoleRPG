using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class Smash : BaseSpell
    {
        public Smash(Unit unit)
        {
            Name = "[bold]Размозжение[/]";
			ID = Enums.SpellIdentifierEnum.Smash;
			AddComponent(new PhysicalDamageCharacteristic 
            { 
                PhysicalDamage = (unit.GetPhysicalDamage() + unit.GetArmor()) * 2
            });
        }

		public override void Use(Unit caster, Unit target)
		{
			int damage = GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
            caster.DealDamage(target, damage, DamageTypes.Physical, this);
		}
	}
}
