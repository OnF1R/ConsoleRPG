using ConsoleRPG.Items.Currencies;
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

            sellingItems.Add(new FireSword(), 1);
            sellingItems.Add(new SteelAxe(), 55);
            sellingItems.Add(new SteelDagger(), 654);
            sellingItems.Add(new SteelSword(), 22);
        }

        private static void TimerCallback(Object o)
        {
            AnsiConsole.MarkupLine("[bold]Магазин торговца обновился![/] " + DateTime.Now);
        }

        public void UpdateItems()
        {

        }
        
        public void ShowSellingItems()
        {
            foreach (string name in GetItemsAndNames().Values)
            {
                AnsiConsole.Markup(name + ": ");
                Item item = GetItemsAndNames().FirstOrDefault(x => x.Value == name).Key;
                item.ItemInfo(item);
            }
        }

        public void SellingMenu(Player player)
        {
            bool loop = true;
            while (loop)
            {
                AnsiConsole.MarkupLine("[bold]Что будете делать?[/]");
                AnsiConsole.MarkupLine("1. Посмотреть товары.");
                AnsiConsole.MarkupLine("2. Купить товары.");
                AnsiConsole.MarkupLine("3. Выйти.");
                AnsiConsole.Markup("Выберите действие: ");

                switch (Convert.ToString(Console.ReadLine()))
                {
                    case "1":
                        ShowSellingItems();
                        break;
                    case "2":
                        //Merchant merchant = new Merchant();
                        SellingItems(player);
                        break;
                    default:
                        loop = false;
                        break;
                }
            }
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

        public void BuyItem(Player player, Item BoughtItem)
        {
            int cost = sellingItems.FirstOrDefault(x => x.Key == BoughtItem).Value;

            if (player.Inventory.IsEnoughCurrency(cost))
            {
                player.Inventory.AddItem(BoughtItem);
                AnsiConsole.MarkupLine($"Вы купили {GetItemsAndNames().FirstOrDefault(x => x.Key == BoughtItem).Value} , спасибо за покупку!");
            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Не достаточно золота![/]");
            }
        }

        public void SellingItems(Player player)
        {
            var bought = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Только лучшие товары![/]")
                    .MoreChoicesText("[bold]Пролистайте ниже чтобы увидеть все предложения![/]")
                    .AddChoices(GetItemsAndNames().Values));

            // Echo the fruit back to the terminal
            BuyItem(player, GetItemsAndNames().FirstOrDefault(x => x.Value == bought).Key);
            
        }
    }
}
