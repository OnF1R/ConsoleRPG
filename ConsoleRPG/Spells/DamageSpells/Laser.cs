
namespace ConsoleRPG.Spells.DamageSpells
{
    internal class Laser : Spell
    {
        public Laser()
        {
            Name = "[blue1]Лазер[/]";
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Fire, new Random().Next(3,6) },
                { DamageTypes.Electric, new Random().Next(3,6) },
                { DamageTypes.Arcane, new Random().Next(3,6) },
            }));
        }
    }
}
