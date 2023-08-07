
using ConsoleRPG.Effects.Debuffs;
using ConsoleRPG.Effects;

namespace ConsoleRPG.Spells.DamageSpells
{
    internal class ArdaBintBaaniAlTaa : BaseSpell
    {
        public ArdaBintBaaniAlTaa()
        {
            Name = "[orangered1]Ар'да Бинт Баани Аль-таа[/]";
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Fire, new Random().Next(16,48)},
                { DamageTypes.Electric, new Random().Next(16,48)},
            }));
            AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
            {
                { new Burn(), 25 },
            }));
        }
    }
}
