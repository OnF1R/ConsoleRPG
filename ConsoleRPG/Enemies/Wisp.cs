using ConsoleRPG.Enums;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Spells.DamageSpells;


namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class Wisp : Enemy
    {
        public Wisp(int playerLevel) : base(playerLevel)
        {
            SerializableRandom random = new SerializableRandom();
            //Equipment equipment = new Equipment();
            Name = "[lightgoldenrod1]Светлячок[/]";
            MaxHealth = random.Next(3, 6) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.Wisp;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, 2 + Level);
            MyRace = new Races.Elemental();

            DropList = new Item[]
            {
                new Gold(),
                new RainbowShard(),
                new RainbowStone(),
            };
        }

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {
                WillOWisp(unit);
            }
            else
            {
                Death(Player);
            }
        }

        public void WillOWisp(Unit unit)
        {
            BaseSpell Spell = new WillOWisp();
			Spell.Use(this, unit);
		}
    }
}
