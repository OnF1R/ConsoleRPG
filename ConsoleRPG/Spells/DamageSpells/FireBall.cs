using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    [Serializable]
    internal class FireBall : BaseSpell
    {
        public FireBall()
        {
            Name = "[orangered1]Огненный[/] шар";
			ID = Enums.SpellIdentifierEnum.Fireball;
			AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Fire, new SerializableRandom().Next(7,12)},
            }));
            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new Burn(), 50 },
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
