using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class Pyroblast : Spell
    {
        public Pyroblast()
        {
            Name = "[orangered1]Огненная[/] глыба";
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Fire, new Random().Next(12, 21) },
            }));
        }
    }
}
