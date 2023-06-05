
using Spectre.Console;

namespace ConsoleRPG.Items.CraftRecipes
{
    internal abstract class BaseRecipe
    {
        public string Name { get; set; }
        public int CurrentLevel;
        public int UpgradeLevels { get; private set; } = 100;
        public int Cost { get; set; }

        public Item CraftItem { get; set; }

        public Dictionary<ItemIdentifier, int> NeededItems { get; set; }

        public double CraftChance { get; set; }

        public BaseRecipe()
        {
            CurrentLevel = 1;
            Name = "[orange3]Рецепт[/] ";
            NeededItems = new Dictionary<ItemIdentifier, int>();
        }

        public void UpgradeSelf()
        {
            if (CurrentLevel < UpgradeLevels)
            {
                CurrentLevel++;
                LevelUp();
                AnsiConsole.MarkupLine($"Рецепт '{Name}' улучшен, текущий уровень {CurrentLevel}");
            }
            else
            {
                AnsiConsole.MarkupLine($"Рецепт '{Name}' максимального уровня!");
            }
        }

        public abstract string CraftNeededItems();

        public virtual void CraftingItem(Item item, int luck) { }

        protected virtual void LevelUp() { }

        public abstract void GenerateItem(Player player);
    }
}
