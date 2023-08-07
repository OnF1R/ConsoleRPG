
using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class WaterBall : BaseSpell
    {
        public WaterBall()
        {
            Name = "[blue1]Водяной[/] шар";
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Water, new Random().Next(7,12)},
            }));
            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new Wet(), 15 },
            }));
        }
    }
}
