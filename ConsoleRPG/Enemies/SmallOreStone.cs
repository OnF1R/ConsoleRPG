using ConsoleRPG.Items.StacableItems;

namespace ConsoleRPG.Enemies
{
    internal class SmallOreStone : Enemy
    {
        public SmallOreStone(int playerLevel) : base(playerLevel)
        {
            Random random = new Random();
            //Equipment equipment = new Equipment();
            Name = "[bold]Малый рудный камень[/]";
            MaxHealth = random.Next(30, 46) * Level;
            GetComponent<ArmorCharacteristic>().Armor += 50;
            CurrentHealth = MaxHealth;
            MyRace = new Races.Elemental();

            DropList = new Item[]
            {
                new RainbowShard(),
                new RainbowStone(),
                new IronOre(),
                new AdamantiteOre(),
            };
        }

        public override void FightLogic(Player Player, Dictionary<DamageTypes, int> TakedDamage)
        {
            foreach (DamageTypes type in TakedDamage.Keys)
            {
                if (!IsDead)
                    TakeDamage(Player, TakedDamage[type], type);
            }
            if (!IsDead)
            {
                Player.AfterAttackBehaviour(this);

                AfterAttackBehaviour(Player);
            }
            else
            {
                DeathDropLoot(Player);
            }
        }
    }
}
