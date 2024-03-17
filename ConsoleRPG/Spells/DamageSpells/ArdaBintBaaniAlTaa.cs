
using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    [Serializable]
    internal class ArdaBintBaaniAlTaa : BaseSpell
    {
        public ArdaBintBaaniAlTaa()
        {
            Name = "[orangered1]Ар'да Бинт Баани Аль-таа[/]";
            ID = Enums.SpellIdentifierEnum.ArdaBintBaaniAlTaa;
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Fire, new SerializableRandom().Next(16,48)},
                { DamageTypes.Electric, new SerializableRandom().Next(16,48)},
            }));
            
            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new Burn(), 25 },
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
