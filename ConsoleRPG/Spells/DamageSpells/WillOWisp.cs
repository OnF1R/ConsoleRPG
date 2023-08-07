
namespace ConsoleRPG.Spells.DamageSpells
{
    internal class WillOWisp : BaseSpell
    {
        public WillOWisp()
        {
            Name = "[bold]Блуждающий огонек[/]";
            AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
            {
                { DamageTypes.Holy, new Random().Next(7,12)},
            }));
        }
    }
}
