using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Races;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    abstract internal class Enemy : Unit
    {
        public Item[] DropList { get; set; }
        public int Energy { get; set; }
        public Enemy()
        {
            Energy = 0;
            Race = new Orc();
        }

        abstract public void FightLogic(Player Player, Dictionary<DamageTypes, int> TakedDamage);

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
            return this.Level * new Random().Next(1, 8);
        }

        public void Attack(Player Player)
        {
            Dictionary<DamageTypes, int> damage = GetExistableTypeDamage();
            foreach (DamageTypes type in damage.Keys)
            {
                if (type == DamageTypes.Physical && IsCrit())
                {
                    damage[type] = CalcPhysicalCritical(damage[type]);
                    Player.TakeCriticalDamage(damage[type], type);
                }
                else
                {
                    Player.TakeDamage(damage[type], type);
                }
            }
        }

        public void DeathDropLoot(Player Player)
        {
            List<Item> DroppedLoot = DropLoot(this.DropList);
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

            Player.TakeExp(this.GiveExp());
        }
    }
}
