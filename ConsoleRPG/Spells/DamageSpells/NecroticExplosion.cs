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
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Necrotic, 999 },
            }));
        }
    }
}
