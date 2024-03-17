
using ConsoleRPG.Effects;
using ConsoleRPG.Effects.Debuffs;

namespace ConsoleRPG.Spells.DamageSpells
{
    [Serializable]
    internal class Hook : BaseSpell
    {
        public Hook()
        {
            Name = "[bold]Крюк[/]";
            ID = Enums.SpellIdentifierEnum.Hook;

            AddComponent(new PhysicalDamageCharacteristic()
            {
                PhysicalDamage = new SerializableRandom().Next(12, 17),
            });

            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new OnTheHook(3), 100 },
                { new Bleed(), 50 },
            }));
        }

        public override void Use(Unit caster, Unit target)
        {
            int damage = GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage;
            caster.DealDamage(target, damage, DamageTypes.Physical, this);
        }
    }
}
