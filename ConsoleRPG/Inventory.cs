using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Console = Colorful.Console;
using Spectre.Console;

namespace ConsoleRPG
{
    internal class Inventory
    {
        public List<Item> ListInventory { get; set; }

        public Inventory()
        {
            ListInventory = new List<Item>();
        }

        public void ShowInventory()
        {
            ListInventory = SortInventory();

            if (ListInventory.Count > 0)
            {
                for (int i = 0; i < ListInventory.Count; i++)
                {
                    int Number = i + 1;
                    if (ListInventory[i].IsStacable)
                    {
                        AnsiConsole.MarkupLine("{0}. {1} ([{2}]{3}[/]) x{4}",
                            Number,
                            ListInventory[i].Name,
                            ListInventory[i].RarityColor,
                            ListInventory[i].Rarity,
                            ListInventory[i].Count
                            );
                        continue;
                    }

                    if (ListInventory[i].IsEquiped)
                    {
                        AnsiConsole.Markup("{0}. {1} ([{2}]{3}[/]) [bold]Экипировано[/]: ",
                            Number,
                            ListInventory[i].Name,
                            ListInventory[i].RarityColor,
                            ListInventory[i].Rarity
                            );
                        ListInventory[i].ItemInfo(ListInventory[i]);
                        continue;
                    }

                    AnsiConsole.Markup("{0}. {1} ([{2}]{3}[/]): ",
                            Number,
                            ListInventory[i].Name,
                            ListInventory[i].RarityColor,
                            ListInventory[i].Rarity
                            );
                    ListInventory[i].ItemInfo(ListInventory[i]);
                }
            }
            else
            {
                AnsiConsole.MarkupLine("Инвентарь пуст");
            }
        }

        public void GetEquipableItem(string SlotName)
        {

        }

        public List<Item> SortInventory()
        {
            List<Item> StacableItems = new List<Item> { };
            List<Item> NotStacableItems = new List<Item> { };

            foreach (var item in ListInventory)
            {
                if (item.IsStacable)
                {
                    StacableItems.Add(item);
                }
                else
                {
                    NotStacableItems.Add(item);
                }
            }

            List<Item> SortedInvenoty = new List<Item> { };
            SortedInvenoty = SortedInvenoty.Concat(StacableItems).Concat(NotStacableItems).ToList();

            return SortedInvenoty;
        }

        public List<Item> SortInventoryForType(params ItemType[] Type)
        {
            List<Item> SortedInventory = new List<Item> { };

            foreach (var item in ListInventory)
            {
                foreach (var type in Type)
                {
                    if (item.Type == type)
                    {
                        SortedInventory.Add(item);
                    }
                }
            }

            return SortedInventory;

        }

        public void AddItem(Item Item)
        {
            if (Item.IsStacable)
            {
                List<string> ItemsNames = new List<string>();
                if (ListInventory != null)
                {
                    foreach (Item AlreadyInInventory in ListInventory)
                    {
                        ItemsNames.Add(AlreadyInInventory.Name);
                    }

                    if (ItemsNames.Contains(Item.Name))
                    {
                        ListInventory.Find(Needle => Needle.Name == Item.Name).Count += Item.Count;
                    }
                    else
                    {
                        ListInventory.Add(Item);
                    }
                }

            }
            else
            {
                ListInventory.Add(Item);
            }
        }
    }
}
