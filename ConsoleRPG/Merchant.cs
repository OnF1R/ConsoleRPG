using ConsoleRPG.Items.ItemsComponents;
using ConsoleRPG.Items.Weapons;
using Spectre.Console;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    internal class Merchant
    {
        public string Name { get; set; }

        public Dictionary<Item, int> sellingItems = new Dictionary<Item, int>();

        private Timer _timer = null;

        public Merchant()
        {
            _timer = new Timer(TimerCallback, null, 0, 1800000);

            sellingItems.Add(new FireSword(), 159);
            sellingItems.Add(new SteelAxe(), 55);
            sellingItems.Add(new SteelDagger(), 654);
            sellingItems.Add(new SteelSword(), 22);
            sellingItems.Add(new FireSword(), 159);
            sellingItems.Add(new SteelAxe(), 55);
            sellingItems.Add(new SteelDagger(), 654);
            sellingItems.Add(new SteelSword(), 22);
            sellingItems.Add(new FireSword(), 159);
            sellingItems.Add(new SteelAxe(), 55);
            sellingItems.Add(new SteelDagger(), 654);
            sellingItems.Add(new SteelSword(), 22);
            sellingItems.Add(new FireSword(), 159);
            sellingItems.Add(new SteelAxe(), 55);
            sellingItems.Add(new SteelDagger(), 654);
            sellingItems.Add(new SteelSword(), 22);

        }

        private static void TimerCallback(Object o)
        {
            //AnsiConsole.MarkupLine("[bold]Магазнин торговца обновился![/] " + DateTime.Now);
        }

        public void UpdateItems()
        {

        }

        public List<string> GetAllSellingItemsNames()
        {
            List<string> infoList = new List<string>();

            foreach (Item item in sellingItems.Keys)
            {
                infoList.Add(item.Name + " (" + sellingItems[item] + " [gold1]золота[/])");
            }

            return infoList;
        }

        public List<Item> GetAllSellingItems()
        {
            List<Item> itemsList = new List<Item>();

            foreach (Item item in sellingItems.Keys)
            {
                itemsList.Add(item);
            }

            return itemsList;
        }

        public Dictionary<Item, string> GetItemsAndNames()
        {
            Dictionary<Item, string> itemsAndNames = new Dictionary<Item, string>();

            List<string> names = GetAllSellingItemsNames();
            List<Item> items = GetAllSellingItems();

            for (int i = 0; i < items.Count; i++)
                itemsAndNames.Add(items[i], names[i]);

            return itemsAndNames;
        }

        public void SellingItems(Player player)
        {
            var bought = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Только лучшие товары![/]")
                    .MoreChoicesText("[bold]Пролистайте ниже чтобы увидеть все предложения![/]")
                    .AddChoices(GetItemsAndNames().Values));

            // Echo the fruit back to the terminal
            AnsiConsole.MarkupLine($"Вы купили {bought} , спасибо за покупку!");
            player.Inventory.AddItem(GetItemsAndNames().FirstOrDefault(x => x.Value == bought).Key);
        }
    }
}
