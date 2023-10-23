
namespace ConsoleRPG.Spells.DamageSpells
{
    internal class WillOWisp : BaseSpell
    {
        public WillOWisp()
        {
            Name = "[bold]Блуждающий огонек[/]";
			ID = Enums.SpellIdentifierEnum.WillOWisp;
			AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Holy, new Random().Next(7,12)},
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
