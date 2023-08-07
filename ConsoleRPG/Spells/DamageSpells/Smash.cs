using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class Smash : BaseSpell
    {
        public Smash(Unit unit)
        {
            Name = "[bold]Размозжение[/]";
            AddComponent(new PhysicalDamageCharacteristic 
            { 
                PhysicalDamage = (unit.GetPhysicalDamage() + unit.GetArmor()) * 2
            });
        }
    }
}
