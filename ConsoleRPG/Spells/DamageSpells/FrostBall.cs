using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class FrostBall : BaseSpell
    {
        public FrostBall()
        {
            Name = "[aqua]Ледяной[/] шар";
			ID = Enums.SpellIdentifierEnum.Frostball;
			AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Frost, new Random().Next(7,12)},
            }));
            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new Frost(), 35 },
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
