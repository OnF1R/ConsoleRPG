
using ConsoleRPG.Enums;
using ConsoleRPG.Items.CraftRecipes;
using ConsoleRPG.Items.Enchants;
using Spectre.Console;

namespace ConsoleRPG
{
    internal class Crafting
    {
        private Dictionary<int, string> menuChoises;

        public Crafting()
        {
            menuChoises = MenuChoises.CraftMenuChoises();
        }

        public void CraftingMenu(Player player)
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
                        Craft.StartCraftItem(player);
                        break;
                    case 2:
                        StartEnchantItem(player);
                        break;
                    case 3:
                        //StartCreateSpell(player);
                        break;
                    default:
                        loop = false;
                        break;
                }

            }
        }

        public void StartCreateSpell(Player player)
        {
			List<BaseItemRecipe> recipes = player.Inventory.ListRecipes;
			Dictionary<BaseItemRecipe, string> keyValuePairs = new Dictionary<BaseItemRecipe, string>();

			foreach (BaseItemRecipe recipe in recipes)
			{
				keyValuePairs.Add(recipe, recipe.Name + " (Уровень: " + recipe.CurrentLevel + ")");
			}

			if (recipes.Count > 0)
			{
				var chosedEnchant = AnsiConsole.Prompt(
					new SelectionPrompt<string>()
						.Title("[bold]Выберите рецепт[/]")
						.PageSize(10)
						.MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
						.AddChoices(keyValuePairs.Values));


				try
				{
					BaseItemRecipe recipe = keyValuePairs.FirstOrDefault(x => x.Value == chosedEnchant).Key;
					//ConfirmUseCraft(player, recipe);
				}
				catch
				{
					AnsiConsole.MarkupLine("Ошибка в Craft.cs StarttCraftItem(), пожалуйста сообщите Олежику :)" +
						", вообще сюда не должно было никак попасть, но на то и существуют баги");
				}

			}
			else
			{
				AnsiConsole.MarkupLine("Нет рецептов");
			}
		}

        public void StartEnchantItem(Player player)
        {
            List<BaseEnchant> enchants = player.Inventory.ListEnchants;
            Dictionary<BaseEnchant, string> keyValuePairs = new Dictionary<BaseEnchant, string>();

            foreach (BaseEnchant enchant in enchants)
            {
                keyValuePairs.Add(enchant, enchant.Name + " (Уровень: " + enchant.CurrentLevel + ")");
            }

            if (enchants.Count > 0)
            {
                var chosedEnchant = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Выберите зачарование[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(keyValuePairs.Values));


                try
                {
                    BaseEnchant enchant = keyValuePairs.FirstOrDefault(x => x.Value == chosedEnchant).Key;
                    ConfirmUseEnchant(player, enchant);
                }
                catch
                {
                    AnsiConsole.MarkupLine("Ошибка в Crafting.cs StartEnchantItem(), пожалуйста сообщите Олежику :)" +
                        ", вообще сюда не должно было никак попасть, но на то и существуют баги");
                }

            }
            else
            {
                AnsiConsole.MarkupLine("Нет зачарований");
            }
        }

        public void ConfirmUseEnchant(Player player, BaseEnchant enchant)
        {
            AnsiConsole.MarkupLine($"Для использования {enchant.Name} необходимы следующие предметы: ");
            AnsiConsole.MarkupLine(enchant.EnchantNeededItems());

            if (IsEnoughtItemsForEnchant(player, enchant))
            {
                var confirm = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"[bold]Вы точно хотите использовать {enchant.Name} с шансом {enchant.EnchantChance}% + {player.GetLuck()}% от удачи? ({enchant.EnchantChance + player.GetLuck()})[/]")
                        .AddChoices("Да", "Нет"));

                switch (confirm)
                {
                    case "Да":
                        ChooseItemForEnchant(player, enchant, enchant.EnchantPurpose.ToArray());
                        break;
                    default:
                        break;
                }
            }
            else
            {
                AnsiConsole.MarkupLine($"Недостаточно предметов для использования {enchant.Name}!");
            }
        }

        public bool IsEnoughtItemsForEnchant(Player player, BaseEnchant enchant)
        {
            int hit = 0;
            int needHits = enchant.NeededItems.Count;

            foreach (var item in enchant.NeededItems.Keys)
            {
                if (player.Inventory.IsHaveStacableItemByItemID(item, enchant.NeededItems.FirstOrDefault(x => x.Key == item).Value))
                {
                    hit++;
                }
            }

            if (hit == needHits)
            {
                return true;
            }

            return false;
        }

        public void ChooseItemForEnchant(Player player, BaseEnchant enchant, params ItemType[] ItemType)
        {
            List<Item> enchantableItems = new List<Item>();
            enchantableItems = player.Inventory.SortInventoryForEquip(ItemType);
            Dictionary<int, string> itemsWithStats = new Dictionary<int, string>();

            if (enchantableItems.Count > 0)
            {
                for (int i = 0; i < enchantableItems.Count; i++)
                {
                    itemsWithStats.Add(i + 1, $"{i + 1}:{enchantableItems[i].Name} ([{enchantableItems[i].RarityColor}]{enchantableItems[i].Rarity}[/]): {enchantableItems[i].ItemInfoString(enchantableItems[i])}");
                }

                var chosedEquipment = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Выберите предмет для зачарования[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(itemsWithStats.Values));

                try
                {
                    int result = itemsWithStats.FirstOrDefault(x => x.Value == chosedEquipment).Key;
                    switch (result >= 1 && result <= enchantableItems.Count)
                    {
                        case true:
                            EnchantItem(player, enchantableItems[--result], enchant);
                            break;
                        default:
                            Console.WriteLine("Не правильно выбран предмет");
                            break;
                    }
                }
                catch
                {
                    AnsiConsole.MarkupLine("Ошибка в Crafting.cs ChooseItemForEnchant(), пожалуйста сообщите Олежику :)" +
                        ", вообще сюда не должно было никак попасть, но на то и существуют баги");
                }

            }
            else
            {
                Console.WriteLine("Нет подходящей экипировки");
            }
        }

        public void EnchantItem(Player player, Item item, BaseEnchant enchant)
        {
            AnsiConsole.MarkupLine("Вы точно хотите зачаровать следующий предмет?");
            AnsiConsole.MarkupLine($"{item.Name}: {item.ItemInfoString(item)}");

            var confirm = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Вы точно хотите его зачаровать[/]")
                    .AddChoices("Да", "Нет"));

            switch (confirm)
            {
                case "Да":
                    if (enchant.IsEnchantable(item))
                    {
                        foreach (var _item in enchant.NeededItems)
                        {
                            player.Inventory.RemoveItem(player.Inventory.SearchStacableItemByItemId(_item.Key), _item.Value);
                        }

                        enchant.EnchantItem(item, player.GetLuck());
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold]Предмет уже имеет это зачарование![/]");
                    }
                    break;
                default:
                    break;
            }
        }

        private void ExistableCrafts()
        {

        }
    }
}
