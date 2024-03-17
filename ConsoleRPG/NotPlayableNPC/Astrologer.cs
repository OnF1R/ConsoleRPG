using Spectre.Console;

namespace ConsoleRPG.NotPlayableNPC
{
    [Serializable]
    internal class Astrologer
    {
        public string Name { get; set; }

        private static Dictionary<int, string> menuChoises = new Dictionary<int, string>();

        public Astrologer()
        {
            Name = "Торговец";

            menuChoises = MenuChoises.AstrologerChoises();
        }

        public void DialogueMenu(Player player)
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
                        //ShowSellingItems();
                        break;
                    case 2:
                        //SellingItems(player);
                        break;
                    case 3:
                        //BoughtItems(player);
                        break;
                    case 4:
                        //SellingRecipes(player);
                        break;
                    case 5:
                        //SellingEnchants(player);
                        break;
                    default:
                        loop = false;
                        break;
                }

            }
        }
    }
}
