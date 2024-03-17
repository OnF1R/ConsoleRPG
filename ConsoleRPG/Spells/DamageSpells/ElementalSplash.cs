using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Spells.DamageSpells
{
    [Serializable]
    internal class ElementalSplash : BaseSpell
    {
        public ElementalSplash()
        {
			ID = Enums.SpellIdentifierEnum.ElementalSplash;
			DamageTypesNames damageTypesNames = new DamageTypesNames();
            DamageTypes damageType = damageTypesNames.GetRandomElementalDamageType();
            string damageTypeColor = damageTypesNames.Color[damageType];
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { damageType, new SerializableRandom().Next(7,12)},
            }));
            Name = $"[{damageTypeColor}]Элементальный[/] брызг";
            
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
