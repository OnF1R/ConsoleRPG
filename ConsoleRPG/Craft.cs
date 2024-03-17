using ConsoleRPG.Items.CraftRecipes;
using Spectre.Console;

namespace ConsoleRPG
{
    [Serializable]
    internal static class Craft
    {
        private static void CraftItem(Player player, BaseItemRecipe recipe, int chance)
        {
            if (HaveItems(player, recipe))
            {
                foreach (var _item in recipe.NeededItems)
                {
                    player.Inventory.RemoveItem(player.Inventory.SearchStacableItemByItemId(_item.Key), _item.Value);
                }

                if (chance >= new SerializableRandom().Next(1,101))
                {
					recipe.GenerateItem(player);
					Item item = recipe.CraftItem;
					AnsiConsole.MarkupLine($"Вы успешно создали {item.Name}: {item.ItemInfoString(item)}");
					player.Inventory.AddItem(item);
				}
                else
                {
					AnsiConsole.MarkupLine("[red]Неудача[/], попробуйте ещё раз :(");
				}
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
                AnsiConsole.MarkupLine("Нет рецептов");
            }
        }

        private static void ConfirmUseCraft(Player player, BaseItemRecipe recipe)
        {
            AnsiConsole.MarkupLine($"Для использования {recipe.Name} необходимы следующие предметы: ");
            AnsiConsole.MarkupLine(recipe.CraftNeededItems());

            if (HaveItems(player, recipe))
            {
                int finalChance = (int)(recipe.CraftChance + player.GetLuck());


				var confirm = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title($"[bold]Вы точно хотите использовать {recipe.Name} с шансом {recipe.CraftChance}% + {player.GetLuck()}% от удачи? ({finalChance})[/]")
                        .AddChoices("Да", "Нет"));

                switch (confirm)
                {
                    case "Да":
                        CraftItem(player, recipe, finalChance);
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

        private static bool HaveItems(Player player, BaseItemRecipe recipe)
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
