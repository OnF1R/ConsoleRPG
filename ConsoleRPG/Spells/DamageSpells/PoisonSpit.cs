using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class PoisonSpit : BaseSpell
    {
        public PoisonSpit()
        {
            Name = "[yellow4]Плевок[/]";
			ID = Enums.SpellIdentifierEnum.PoisonSpit;
			AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Poison, new Random().Next(5, 17)},
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
