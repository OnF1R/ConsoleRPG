using ConsoleRPG.Spells.SpellsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class NecroticExplosion : Spell
    {
        public NecroticExplosion()
        {
            Name = "[yellow4]ВЗРЫВ[/]";
            AddComponent(new SpellElementalDamage { Necrotic = 999 });
            AddComponent(new SpellDamageType { Type = DamageTypes.Necrotic });
        }
    }
}
