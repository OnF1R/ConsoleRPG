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
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Fire, new Random().Next(7,12) },
            }));
        }
    }
}
