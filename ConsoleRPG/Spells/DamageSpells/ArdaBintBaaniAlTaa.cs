
namespace ConsoleRPG.Spells.DamageSpells
{
    internal class ArdaBintBaaniAlTaa : Spell
    {
        public ArdaBintBaaniAlTaa()
        {
            Name = "[orangered1]Ар'да Бинт Баани Аль-таа[/]";
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Fire, new Random().Next(16,48) },
                { DamageTypes.Electric, new Random().Next(16,48) },
            }));
        }
    }
}
