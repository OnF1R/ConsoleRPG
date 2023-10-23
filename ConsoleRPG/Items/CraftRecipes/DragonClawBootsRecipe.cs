
using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Boots;
using ConsoleRPG.Items.Enchants;
using Spectre.Console;

namespace ConsoleRPG.Items.CraftRecipes
{
    internal class DragonClawBootsRecipe : BaseItemRecipe
    {
        public DragonClawBootsRecipe()
        {
            Name += "[bold]Драконьи зубы[/]";
            Cost = 2;
            CraftChance = 15;

            NeededItems.Add(ItemIdentifier.DragonClaw, 2);
            NeededItems.Add(ItemIdentifier.RainbowShard, 2);
        }

        protected override void LevelUp()
        {
            if (CraftChance < 100 && CurrentLevel % 2 == 0)
            {
                CraftChance++;
                AnsiConsole.MarkupLine($"{Name}, улучшен шанс успешного зачарования, текущий шанс {CraftChance}");
            }

            if (CurrentLevel % 60 == 0)
            {
                NeededItems[ItemIdentifier.DragonClaw] -= 1;
                NeededItems[ItemIdentifier.RainbowShard] -= 1;
                AnsiConsole.MarkupLine($"{Name}, улучшен, теперь необходимо меньше ресурсов");
            }
        }

        public override string CraftNeededItems()
        {
            string info = "";

            foreach (var item in NeededItems)
            {
                info += $"{ItemID.Names[item.Key]}: {item.Value} шт.\n";
            }

            return info;
        }

        public override void GenerateItem(Player player)
        {
            CraftItem = new DragonClawBoots(player.Level);
        }
    }
}
