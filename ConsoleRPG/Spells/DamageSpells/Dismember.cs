
using ConsoleRPG.Effects;
using ConsoleRPG.Effects.Stun;

namespace ConsoleRPG.Spells.DamageSpells
{
    [Serializable]
    internal class Dismember : BaseSpell
    {
        public Dismember()
        {
            Name = "[bold red]Расчленение[/]";
            ID = Enums.SpellIdentifierEnum.Dismember;

            AddComponent(new PhysicalDamageCharacteristic() { PhysicalDamage = new SerializableRandom().Next(3, 8) });

            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new Stun(), 100 },
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
