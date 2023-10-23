using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class VomitOdor : BaseSpell
    {
        public VomitOdor()
        {
            Name = "[yellow4]Рвотный запах[/]";
			ID = Enums.SpellIdentifierEnum.VomitOdor;
			AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Necrotic, new Random().Next(5,10)},
            }));
            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new Rotting(), 35 },
                { new Blind(), 75 },
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
