using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class ElementalSplash : Spell
    {
        public ElementalSplash()
        {
            DamageTypesNames damageTypesNames = new DamageTypesNames();
            DamageTypes damageType = damageTypesNames.GetRandomElementalDamageType();
            string damageTypeColor = damageTypesNames.Color[damageType];
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { damageType, new Random().Next(7,12) },
            }));
            Name = $"[{damageTypeColor}]Элементальный[/] брызг";
            
        }
    }
}
