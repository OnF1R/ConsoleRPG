using ConsoleRPG.Spells.SpellsComponents;
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
            AddComponent(new SpellDamageType { Type = damageType });
            Name = $"[{damageTypeColor}]Элементальный[/] брызг";
            AddNeededElementalTypesDamage(this, 7, 12);
            
        }
    }
}
