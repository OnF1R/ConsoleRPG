using ConsoleRPG.Items.Weapons;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    abstract internal class Enemy
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Damage { get; set; }
        public int Energy { get; set; }

        public bool IsDead { get; set; }

        public int Armor { get; set; }
        public double MissChance { get; set; }
        public double CritChance { get; set; }
        public double CritDamage { get; set; }

        public Item[] DropList { get; set; }

        public Race Race;

        public Equipment Equipment = new Equipment();

        public Enemy()
        {
            this.IsDead = false;
            this.CurrentHealth = this.MaxHealth;
        }

        abstract public void FightLogic(Fight CurrentFight, Player Player, Item PlayerWeapon, int TakedDamage);


        //public void BasicAttack(Player Player)
        //{
        //    int TempDamage = this.Damage;

        //    if (this.CritChance >= new Random().Next(1, 101))
        //    {
        //        TempDamage = this.Damage * (int)Math.Floor(this.Damage / (100 * this.CritDamage));
        //    }

        //    Player.TakeDamage(TempDamage);
        //}

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

        public void Death(Player Player)
        {
            this.IsDead = true;
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

            Equipment.TakeOffAllEquip();

            Player.TakeExp(this.GiveExp());
        }
    }
}
