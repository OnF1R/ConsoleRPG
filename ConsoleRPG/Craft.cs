using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    internal class Craft
    {
        public Craft(Player player, Item craftingItem, Dictionary<Item, int> neededItems)
        {
            if (CraftItem(player.Inventory, craftingItem, neededItems))
            {
                AnsiConsole.MarkupLine($"Вы успешно создали {craftingItem.Name}: {craftingItem.ItemInfoString(craftingItem)}");
                player.Inventory.AddItem(craftingItem);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Не достаточно предметов для создания![/]");
            }
        }

        private bool CraftItem(Inventory inventory, Item craftingItem, Dictionary<Item, int> neededItems)
        {
            foreach (Item item in neededItems.Keys)
            {
                if (item.IsStacable)
                {
                    if (inventory.IsHave(item, neededItems[item]))
                    {
                        inventory.RemoveItem(item, neededItems[item]);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (inventory.IsHave(item))
                    {
                        inventory.RemoveItem(item);
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
