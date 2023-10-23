using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class SpiderBite : BaseSpell
    {
        public SpiderBite()
        {
            Name = "[bold]Паучий[/] укус";
			ID = Enums.SpellIdentifierEnum.SpiderBite;
			AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Poison, new Random().Next(6,15)},
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
