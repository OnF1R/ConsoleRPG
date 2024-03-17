using ConsoleRPG.Enums;
using ConsoleRPG.Quests;
using Spectre.Console;

namespace ConsoleRPG
{
    [Serializable]
    abstract internal class Enemy : Unit
    {
        public Item[] DropList { get; set; }

        public int Energy { get; set; }

        public List<Enemy> Team { get; set; } = new List<Enemy>();

        public EnemyIdentifierEnum ID { get; set; }

        public bool IsBoss { get; set; } = false;

        public Enemy(int level)
        {
            Energy = 0;

            int maxLevel = level;

            if (maxLevel * 2 > 10)
            {
                maxLevel = 10;
            }

            Level = new SerializableRandom().Next(maxLevel, level + maxLevel);
        }

        public List<Enemy> GetTeamWithMe()
        {
            List<Enemy> list = new List<Enemy>();
            if (Team != null)
            {
                list.AddRange(Team);
            }
            list.Add(this);
            return list;
        }

        public Enemy GetRandomTeammate()
        {
            SerializableRandom rand = new SerializableRandom();
            return Team[rand.Next(Team.Count)];
        }

        public Enemy GetRandomTeammateWithMe()
        {
            var list = GetTeamWithMe();
            SerializableRandom rand = new SerializableRandom();
            return list[rand.Next(list.Count)];
        }

        abstract public void FightLogic(Player Player, Unit unit);

        public List<Item> DropLoot(params Item[] DropList)
        {
            List<Item> ListInventory = new List<Item> { };
            foreach (Item item in DropList)
            {
                if (item.DropChance >= new SerializableRandom().Next(0, 101))
                {
                    ListInventory.Add(item);
                }
            }
            return ListInventory;
        }

        public int GiveExp()
        {
            int exp = Level * new SerializableRandom().Next(1, 8);

            if (IsBoss)
                exp *= 4;

            return exp;
        }

        public override void Death(Player Player)
        {
            IsDead = true;

            List<Item> DroppedLoot = DropLoot(DropList);
            if (DroppedLoot != null)
            {
                foreach (Item DroppedItem in DroppedLoot)
                {
                    Player.Inventory.AddItem(DroppedItem);
                }
            }
            else
            {
                Console.WriteLine("К сожалению вы ничего не получили...");
            }

            Player.KillCountUpdate(ID);
            Player.KillingQuestUpdate(ID);

            if (Player.KillCountNumber() > 0 && Player.KillCountNumber() % 15 == 0)
                Player.AddQuest(new SmallKillingQuest());

            Equipment.TakeOffAllEquip(this);

            Player.TakeExp(GiveExp());

            foreach (var unit in Player.Team)
            {
                unit.TakeExp(GiveExp());
            }
        }
    }
}
