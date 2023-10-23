using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class FrostBolt : BaseSpell
    {
        public FrostBolt()
        {
            Name = "[aqua]Ледяная[/] стрела";
			ID = Enums.SpellIdentifierEnum.Frostbolt;
			AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Frost, new Random().Next(12,17)},
            }));
            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new Frost(), 75 },
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
