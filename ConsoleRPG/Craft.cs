using ConsoleRPG.Items.CraftRecipes;
using Spectre.Console;

namespace ConsoleRPG
{
    internal static class Craft
    {
        private static void CraftItem(Player player, BaseRecipe recipe)
        {
            if (HaveItems(player, recipe))
            {
                foreach (var _item in recipe.NeededItems)
                {
                    player.Inventory.RemoveItem(player.Inventory.SearchStacableItemByItemId(_item.Key), _item.Value);
                }

                recipe.GenerateItem(player);
                Item item = recipe.CraftItem;
                AnsiConsole.MarkupLine($"Вы успешно создали {item.Name}: {item.ItemInfoString(item)}");
                player.Inventory.AddItem(item);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Не достаточно предметов для создания![/]");
            }
        }

        private static void RemoveNeededItems(Inventory inventory, Dictionary<Item, int> neededItems)
        {
            foreach (Item item in neededItems.Keys)
            {
                if (item.IsStacable)
                {
                    inventory.RemoveItem(item, neededItems[item]);
                }
                else
                {
                    inventory.RemoveItem(item);
                }
            }
        }

        public static void StartCraftItem(Player player)
        {
            List<BaseRecipe> recipes = player.Inventory.ListRecipes;
            Dictionary<BaseRecipe, string> keyValuePairs = new Dictionary<BaseRecipe, string>();

            foreach (BaseRecipe recipe in recipes)
            {
                keyValuePairs.Add(recipe, recipe.Name + " (Уровень: " + recipe.CurrentLevel + ")");
            }

            if (recipes.Count > 0)
            {
                var chosedEnchant = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Выберите зачарование[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(keyValuePairs.Values));


                try
                {
                    BaseRecipe recipe = keyValuePairs.FirstOrDefault(x => x.Value == chosedEnchant).Key;
                    ConfirmUseCraft(player, recipe);
                }
                catch
                {
                    AnsiConsole.MarkupLine("Ошибка в Craft.cs StarttCraftItem(), пожалуйста сообщите Олежику :)" +
                        ", вообще сюда не должно было никак попасть, но на то и существуют баги");
                }

            }
            else
            {
                AnsiConsole.MarkupLine("Нет зачарований");
            }
        }

        private static void ConfirmUseCraft(Player player, BaseRecipe recipe)
        {
            AnsiConsole.MarkupLine($"Для использования {recipe.Name} необходимы следующие предметы: ");
            AnsiConsole.MarkupLine(recipe.CraftNeededItems());

            if (HaveItems(player, recipe))
            {
                var confirm = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"[bold]Вы точно хотите использовать {recipe.Name} с шансом {recipe.CraftChance}% + {player.GetLuck()}% от удачи? ({recipe.CraftChance + player.GetLuck()})[/]")
                        .AddChoices("Да", "Нет"));

                switch (confirm)
                {
                    case "Да":
                        CraftItem(player, recipe);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                AnsiConsole.MarkupLine($"Недостаточно предметов для использования {recipe.Name}!");
            }
        }

        private static bool HaveItems(Player player, BaseRecipe recipe)
        {
            int hit = 0;
            int needHits = recipe.NeededItems.Count;

            foreach (var item in recipe.NeededItems.Keys)
            {
                if (player.Inventory.IsHaveStacableItemByItemID(item, recipe.NeededItems.FirstOrDefault(x => x.Key == item).Value))
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

        private static bool HaveItems(Inventory inventory, Item craftingItem, Dictionary<Item, int> neededItems)
        {
            foreach (Item item in neededItems.Keys)
            {
                if (item.IsStacable)
                {
                    if (!inventory.IsHave(item, neededItems[item]))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!inventory.IsHave(item))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
