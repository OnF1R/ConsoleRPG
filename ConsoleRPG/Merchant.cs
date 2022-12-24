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

        private static Dictionary<int, string> MenuChoises = new Dictionary<int, string>();

        public Merchant()
        {
            _timer = new Timer(TimerCallback, null, 0, 1800000);

            MenuChoises = GenerateMenuChoises();

            
        }

        private static Dictionary<int, string> GenerateMenuChoises()
        {
            MenuChoises.Add(1, "Посмотреть товары");
            MenuChoises.Add(2, "Купить товары");
            MenuChoises.Add(3, "Продать предметы");
            MenuChoises.Add(4, "Выйти");

            return MenuChoises;
        }

        private void TimerCallback(Object o)
        {
            AnsiConsole.MarkupLine("[bold]Магазин торговца обновился![/] " + DateTime.Now);
            UpdateItems();
        }

        public void UpdateItems()
        {
            List<Item> randomItems = new List<Item>() { new FireSword(), new SteelAxe(), new SteelDagger(), new SteelSword() };

            foreach (Item item in randomItems)
            {
                sellingItems.Add(item, item.GetComponent<Valuable>().Cost * new Random().Next(3, 8));
            }
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
                var choise = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Что будете делать?[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(MenuChoises.Values));


                Console.Clear();

                switch (MenuChoises.FirstOrDefault(x => x.Value == choise).Key)
                {
                    case 1:
                        ShowSellingItems();
                        break;
                    case 2:
                        SellingItems(player);
                        break;
                    case 3:
                        BoughtItems(player);
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
            int _cost = sellingItems.FirstOrDefault(x => x.Key == BoughtItem).Value;

            if (player.Inventory.IsEnoughCurrency(_cost))
            {

                if (ConfirmBuy(BoughtItem))
                {
                    player.Inventory.BuyItem(_cost, BoughtItem);
                    AnsiConsole.MarkupLine($"Вы купили {GetItemsAndNames().FirstOrDefault(x => x.Key == BoughtItem).Value} , спасибо за покупку!");
                    sellingItems.Remove(BoughtItem);
                }
                else
                {
                    AnsiConsole.MarkupLine("[red bold]Вы отказались от покупки[/]");
                }

            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Не достаточно золота![/]");
            }
        }

        public void SellItem(Player player, params Item[] sellingItem)
        {
            int _cost = 0;

            foreach (Item item in sellingItem)
            {
                _cost += item.GetComponent<Valuable>().Cost;
            }

            if (ConfirmSell(sellingItem))
            {
                foreach (Item item in sellingItem)
                {
                    player.Inventory.SellItem(item);
                    AnsiConsole.MarkupLine("Вы продали {0} за [gold1]{1} золота[/]", item.Name, item.GetComponent<Valuable>().Cost);
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red bold]Вы отказались от продажи[/]");
            }
        }

        public bool ConfirmBuy(Item item)
        {
            AnsiConsole.MarkupLine("Вы точно хотите приобрести {0} со следующими характеристиками: ", item.Name);
            item.ItemInfo(item);
            var confirm = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Вы хотите приобрести этот предмет?[/]")
                    .AddChoices("Да", "Нет"));

            switch (confirm)
            {
                case "Да":
                    return true;
                default:
                    return false;
            }
        }

        public bool ConfirmSell(params Item[] sellingItem)
        {
            AnsiConsole.MarkupLine("Вы точно хотите продать следующие предметы:");
            foreach (Item item in sellingItem)
            {
                AnsiConsole.MarkupLine(item.Name);
            }
            var confirm = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Вы точно хотите их продать[/]")
                    .AddChoices("Да", "Нет"));

            switch (confirm)
            {
                case "Да":
                    return true;
                default:
                    return false;
            }
        }

        public void SellingItems(Player player)
        {
            if (GetItemsAndNames().Count == 0)
            {
                AnsiConsole.MarkupLine("[bold]Ты уже все купил, жди пополнения![/]");
            }
            else
            {
                var bought = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("[bold]Только лучшие товары![/]")
                                    .MoreChoicesText("[bold]Пролистайте ниже, чтобы увидеть все доступные варианты[/]")
                                    .AddChoices(GetItemsAndNames().Values));


                BuyItem(player, GetItemsAndNames().FirstOrDefault(x => x.Value == bought).Key);
            }
        }

        public void BoughtItems(Player player)
        {
            List<string> itemsNames = new List<string>();
            List<Item> items = new List<Item>();

            player.Inventory.GetItemsAndCost(out itemsNames, out items);


            if (items.Count > 0)
            {
                var bought = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                        .Title("[bold]Скупка предметов![/]")
                        .MoreChoicesText("[bold]Пролистайте ниже, чтобы увидеть все доступные варианты[/]")
                        .InstructionsText(
                        "[bold](Нажмите [blue]<Пробел>[/] чтобы выбрать предмет, " +
                        "[green]<Enter>[/] чтобы продать выбранное)[/]")
                        .AddChoices(itemsNames));

                foreach (string item in bought)
                {
                    SellItem(player, items[itemsNames.IndexOf(item)]);
                }


            }
            else
            {
                AnsiConsole.MarkupLine("[red bold]Вам нечего продать[/]");
            }
        }
    }
}
