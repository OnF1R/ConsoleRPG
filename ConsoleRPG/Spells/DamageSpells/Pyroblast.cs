using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class Pyroblast : BaseSpell
    {
        public Pyroblast()
        {
            Name = "[orangered1]Огненная[/] глыба";
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Fire, new Random().Next(12, 21)},
            }));
            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new Burn(), 100 },
            }));
        }
    }
}
