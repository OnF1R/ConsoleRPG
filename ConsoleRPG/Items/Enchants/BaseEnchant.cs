
using Spectre.Console;

namespace ConsoleRPG.Items.Enchants
{
    internal abstract class BaseEnchant
    {
        public string Name { get; set; }
        public int CurrentLevel;
        public int UpgradeLevels { get; private set; } = 100;
        public int Cost { get; set; }
        public int EnchantBoost { get; set; }
        
        public List<ItemType> EnchantPurpose { get; set; }

        public Dictionary<ItemIdentifier, int> NeededItems { get; set; }

        public double EnchantChance { get; set; }

        public BaseEnchant()
        {
            CurrentLevel = 1;
            Name = "[purple4]Зачарование[/] ";
            EnchantPurpose = new List<ItemType>();
            NeededItems = new Dictionary<ItemIdentifier, int>();
        }

        public void UpgradeSelf()
        {
            if (CurrentLevel < UpgradeLevels)
            {
                CurrentLevel++;
                LevelUp();
                AnsiConsole.MarkupLine($"'{Name}' улучшено, текущий уровень {CurrentLevel}");
            }
            else
            {
                AnsiConsole.MarkupLine($"'{Name}' максимального уровня!");
            }
        }

        public abstract string EnchantNeededItems();

        public virtual void EnchantItem(Item item, int luck) { }

        public abstract bool IsEnchantable(Item item);

        protected virtual void LevelUp() { }
    }
}
