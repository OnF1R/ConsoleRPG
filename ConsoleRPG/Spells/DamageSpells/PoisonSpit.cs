using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class PoisonSpit : Spell
    {
        public PoisonSpit()
        {
            Name = "[yellow4]Плевок[/]";
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Poison, new Random().Next(5, 17) },
            }));
        }
    }
}
