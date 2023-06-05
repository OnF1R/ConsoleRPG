
namespace ConsoleRPG.Spells.DamageSpells
{
    internal class WaterBall : Spell
    {
        public WaterBall()
        {
            Name = "[blue1]Водяной[/] шар";
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Water, new Random().Next(7,12) },
            }));
        }
    }
}
