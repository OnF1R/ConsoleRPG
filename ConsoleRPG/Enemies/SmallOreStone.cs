using ConsoleRPG.Enums;
using ConsoleRPG.Items.StacableItems;

namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class SmallOreStone : Enemy
    {
        public SmallOreStone(int playerLevel) : base(playerLevel)
        {
            SerializableRandom random = new SerializableRandom();
            //Equipment equipment = new Equipment();
            Name = "[bold]Малый рудный камень[/]";
            MaxHealth = random.Next(30, 46) * Level;
            GetComponent<ArmorCharacteristic>().Armor += 50;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.SmallOreStone;
			MyRace = new Races.Elemental();

            DropList = new Item[]
            {
                new RainbowShard(),
                new RainbowStone(),
                new IronOre(),
                new AdamantiteOre(),
            };
        }

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {

            }
            else
            {
                Death(Player);
            }
        }
    }
}
