
using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class WaterBall : BaseSpell
    {
        public WaterBall()
        {
            Name = "[blue1]Водяной[/] шар";
			ID = Enums.SpellIdentifierEnum.Waterball;
			AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Water, new Random().Next(7,12)},
            }));
            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new Wet(), 15 },
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
