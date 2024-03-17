using ConsoleRPG.Items.CraftRecipes;
using ConsoleRPG.Items.Enchants;
using ConsoleRPG.Items.Weapons;
using Newtonsoft.Json;
using Spectre.Console;
using System.Timers;

namespace ConsoleRPG.NotPlayableNPC
{
    [Serializable]
    internal class Merchant
    {
        public string Name { get; set; }

        private Dictionary<Item, int> sellingItems = new Dictionary<Item, int>();

        private Dictionary<BaseEnchant, int> sellingsEnchants = new Dictionary<BaseEnchant, int>();

        private Dictionary<BaseItemRecipe, int> sellingsRecipes = new Dictionary<BaseItemRecipe, int>();

        [NonSerialized] private System.Timers.Timer _timer = null;
        private int timerIntervalInSeconds; // Сохраняем интервал таймера

        private Dictionary<int, string> menuChoises = new Dictionary<int, string>();

        public Merchant(int timerIntervalInSeconds)
        {
            Name = "Торговец";

            //_timer = new Timer(TimerCallback, null, 0, 900000);

            this.timerIntervalInSeconds = timerIntervalInSeconds;
            // Инициализация таймера
            _timer = new System.Timers.Timer(this.timerIntervalInSeconds * 1000); // Преобразуем секунды в миллисекунды
            _timer.Elapsed += TimerElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true; // Запускаем таймер


            menuChoises = MenuChoises.MerchantChoises();

            foreach (BaseEnchant recipe in ExistableEnchants.enchants)
            {
                sellingsEnchants.Add(recipe, 100);
            }

            foreach (BaseItemRecipe recipe in ExistableCrafts.recipes)
            {
                sellingsRecipes.Add(recipe, 100);
            }

            AnsiConsole.MarkupLine("[bold]Магазин торговца обновился![/] " + DateTime.Now);
            UpdateItems();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            AnsiConsole.MarkupLine("[bold]Магазин торговца обновился![/] " + DateTime.Now);
            UpdateItems();
        }

        private void TimerCallback(object o)
        {
            AnsiConsole.MarkupLine("[bold]Магазин торговца обновился![/] " + DateTime.Now);
            UpdateItems();
        }

        public void UpdateItems()
        {
            List<Item> randomItems = new List<Item>() {
                new FireSword(new SerializableRandom().Next(1, 101)),
                new SteelAxe(new SerializableRandom().Next(1, 101)),
                new SteelDagger(new SerializableRandom().Next(1, 101)),
                new SteelSword(new SerializableRandom().Next(1, 101)) };

            sellingItems.Clear();

            foreach (Item item in randomItems)
                sellingItems.Add(item, item.GetComponent<ValueCharacteristic>().Cost * new SerializableRandom().Next(3, 8));
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
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(menuChoises.Values));


                Console.Clear();

                switch (menuChoises.FirstOrDefault(x => x.Value == choise).Key)
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
                    case 4:
                        SellingRecipes(player);
                        break;
                    case 5:
                        SellingEnchants(player);
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
            foreach (Item item in sellingItem)
            {
                if (ConfirmSell(item))
                {
                    player.Inventory.SellItem(item);
                    AnsiConsole.MarkupLine("Вы продали {0} за [gold1]{1} золота[/]", item.Name, item.GetComponent<ValueCharacteristic>().Cost);
                }
                else
                {
                    AnsiConsole.MarkupLine("[red bold]Вы отказались от продажи[/]");
                }
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

        public bool ConfirmBuy(BaseEnchant recipe)
        {
            AnsiConsole.MarkupLine("Вы точно хотите приобрести {0}?", recipe.Name);
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

        public bool ConfirmBuy(BaseItemRecipe recipe)
        {
            AnsiConsole.MarkupLine("Вы точно хотите приобрести {0}?", recipe.Name);
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
                AnsiConsole.MarkupLine("{0} за [gold1]{1} золота[/]", item.Name, item.GetComponent<ValueCharacteristic>().Cost);
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

        public void SellingRecipes(Player player)
        {
            if (sellingsRecipes.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold]Ты купил все доступные рецепты![/]");
            }
            else
            {
                IEnumerable<string> names =
                    from recipe in sellingsRecipes
                     where recipe.Value > 0
                     select recipe.Key.Name + " (" + recipe.Key.Cost * Math.Clamp(Math.Abs(101 - recipe.Value), 1, int.MaxValue) + " [gold1]золота[/])";

                Dictionary<BaseItemRecipe, string> keyValuePairs = new Dictionary<BaseItemRecipe, string>();
                List<BaseItemRecipe> keys = new List<BaseItemRecipe>();
                List<string> namesList = names.ToList();
                foreach (var recipe in sellingsRecipes.Keys)
                {
                    keys.Add(recipe);
                }

                for (int i = 0; i < sellingsRecipes.Count; i++)
                {
                    keyValuePairs.Add(sellingsRecipes.FirstOrDefault(x => x.Key == keys[i]).Key, namesList[i]);
                }

                var bought = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("[bold]Только лучшие товары![/]")
                                    .MoreChoicesText("[bold]Пролистайте ниже, чтобы увидеть все доступные варианты[/]")
                                    .AddChoices(names));


                BuyRecipe(player, keyValuePairs.FirstOrDefault(x => x.Value == bought).Key);
            }
        }

        public void SellingEnchants(Player player)
        {
            if (sellingsEnchants.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold]Ты купил все доступные зачарования![/]");
            }
            else
            {
                IEnumerable<string> names =
                    from recipe in sellingsEnchants
                     where recipe.Value > 0
                     select recipe.Key.Name + " (" + recipe.Key.Cost * Math.Clamp(Math.Abs(101 - recipe.Value), 1, int.MaxValue) + " [gold1]золота[/])";

                Dictionary<BaseEnchant, string> keyValuePairs = new Dictionary<BaseEnchant, string>();
                List<BaseEnchant> keys = new List<BaseEnchant>();
                List<string> namesList = names.ToList();
                foreach (var recipe in sellingsEnchants.Keys)
                {
                    keys.Add(recipe);
                }

                for (int i = 0; i < sellingsEnchants.Count; i++)
                {
                    keyValuePairs.Add(sellingsEnchants.FirstOrDefault(x => x.Key == keys[i]).Key, namesList[i]);
                }

                var bought = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("[bold]Только лучшие товары![/]")
                                    .MoreChoicesText("[bold]Пролистайте ниже, чтобы увидеть все доступные варианты[/]")
                                    .AddChoices(names));


                BuyEnchant(player, keyValuePairs.FirstOrDefault(x => x.Value == bought).Key);
            }
        }

        public void BuyRecipe(Player player, BaseItemRecipe recipe)
        {
            BaseItemRecipe sellingRecipe = sellingsRecipes.FirstOrDefault(x => x.Key == recipe).Key;
            int _count = sellingsRecipes[sellingRecipe];
            int _cost = recipe.Cost * Math.Abs(101 - _count);
            if (player.Inventory.IsEnoughCurrency(_cost))
            {

                if (ConfirmBuy(recipe))
                {
                    player.Inventory.BuyRecipe(_cost, recipe);
                    AnsiConsole.MarkupLine($"Вы купили {sellingRecipe.Name} , спасибо за покупку!");
                    if (_count > 1)
                    {
                        _count--;
                        sellingsRecipes[sellingRecipe] = _count;
                    }
                    else
                    {
                        sellingsRecipes.Remove(sellingRecipe);
                    }
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

        public void BuyEnchant(Player player, BaseEnchant enchant)
        {
            BaseEnchant sellingEnchant = sellingsEnchants.FirstOrDefault(x => x.Key == enchant).Key;
            int _count = sellingsEnchants[sellingEnchant];
            int _cost = enchant.Cost * Math.Abs(101 - _count);
            if (player.Inventory.IsEnoughCurrency(_cost))
            {

                if (ConfirmBuy(enchant))
                {
                    player.Inventory.BuyRecipe(_cost, enchant);
                    AnsiConsole.MarkupLine($"Вы купили {sellingEnchant.Name} , спасибо за покупку!");
                    if (_count > 1)
                    {
                        _count--;
                        sellingsEnchants[sellingEnchant] = _count;
                    }
                    else
                    {
                        sellingsEnchants.Remove(sellingEnchant);
                    }
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

        public void BoughtItems(Player player)
        {
            List<string> itemsNames = player.Inventory.GetNumerableItems();

            List<Item> items = player.Inventory.GetNotStacableItems();

            List<Item> sellingItems = new List<Item>();

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
                    sellingItems.Add(items[itemsNames.IndexOf(item)]);
                }

                SellItem(player, sellingItems.ToArray());
            }
            else
            {
                AnsiConsole.MarkupLine("[red bold]Вам нечего продать[/]");
            }
        }
    }
}
