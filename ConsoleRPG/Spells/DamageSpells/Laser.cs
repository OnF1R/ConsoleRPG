
using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    [Serializable]
    internal class Laser : BaseSpell
    {
        public Laser()
        {
            Name = "[blue1]Лазер[/]";
			ID = Enums.SpellIdentifierEnum.Laser;
			AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Fire, new SerializableRandom().Next(3,6)},
                { DamageTypes.Electric, new SerializableRandom().Next(3,6)},
                { DamageTypes.Arcane, new SerializableRandom().Next(3,6) },
            }));

            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new Burn(), 15 },
            }));
        }

		public override void Use(Unit caster, Unit target)
		{
			Dictionary<DamageTypes, int> elemDamage = GetComponent<ElementalDamageCharacteristic>().ElemDamage;
			foreach (DamageTypes type in elemDamage.Keys)
			{
				if (elemDamage[type] != 0)
					caster.DealDamage(target, elemDamage[type], type, this);
			}
		}
	}
}
