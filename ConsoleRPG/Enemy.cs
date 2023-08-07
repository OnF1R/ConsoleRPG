using Spectre.Console;

namespace ConsoleRPG
{
    abstract internal class Enemy : Unit
    {
        public Item[] DropList { get; set; }

        public int Energy { get; set; }

        public bool IsBoss { get; set; } = false;

        public Enemy(int level)
        {
            Energy = 0;

            int maxLevel = level;

            if (maxLevel * 2 > 10)
            {
                maxLevel = 10;
            }

            Level = new Random().Next(maxLevel, level + maxLevel);
        }

        abstract public void FightLogic(Player Player);

        public List<Item> DropLoot(params Item[] DropList)
        {
            List<Item> ListInventory = new List<Item> { };
            foreach (Item item in DropList)
            {
                if (item.DropChance >= new Random().Next(0, 101))
                {
                    ListInventory.Add(item);
                }
            }
            return ListInventory;
        }

        public int GiveExp()
        {
            int exp = Level * new Random().Next(1, 8);

            if (IsBoss)
                exp *= 4;

            return exp;
        }

        public void DeathDropLoot(Player Player)
        {
            List<Item> DroppedLoot = DropLoot(DropList);
            if (DroppedLoot != null)
            {
                foreach (Item DroppedItem in DroppedLoot)
                {
                    if (DroppedItem.IsStacable)
                    {
                        AnsiConsole.MarkupLine("Вы получили {0} ([{1}]{2})[/] x{3}", DroppedItem.Name, DroppedItem.RarityColor, DroppedItem.Rarity, DroppedItem.Count);
                    }
                    else
                    {
                        AnsiConsole.Markup("Вы получили {0} ([{1}]{2}[/]): ", DroppedItem.Name, DroppedItem.RarityColor, DroppedItem.Rarity);
                        DroppedItem.ItemInfo(DroppedItem);
                    }

                    Player.Inventory.AddItem(DroppedItem);
                }
            }
            else
            {
                Console.WriteLine("К сожалению вы ничего не получили...");
            }

            Equipment.TakeOffAllEquip(this);

            Player.TakeExp(GiveExp());
        }
    }
}
