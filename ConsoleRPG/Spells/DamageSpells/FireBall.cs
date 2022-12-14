using ConsoleRPG.Spells.SpellsComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class FireBall : Spell
    {
        public FireBall()
        {
            Name = "[orangered1]Огненный[/] шар";
            AddComponent(new SpellElementalDamage { Fire = new Random().Next(7,12) });
            AddComponent(new SpellDamageType { Type = DamageTypes.Fire });
        }
    }
}
