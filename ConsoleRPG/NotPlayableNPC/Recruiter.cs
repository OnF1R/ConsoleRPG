using ConsoleRPG.Items.CraftRecipes;
using ConsoleRPG.Items.Enchants;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.PlayableUnits;
using Newtonsoft.Json;
using Spectre.Console;
using System.Text;
using System.Timers;

namespace ConsoleRPG.NotPlayableNPC
{
    [Serializable]
    internal class Recruiter
    {
        public string Name { get; set; }

        private Dictionary<PlayableUnit, int> recruits = new Dictionary<PlayableUnit, int>();

        [NonSerialized] private System.Timers.Timer _timer = null;

        private int timerIntervalInSeconds;

        private Dictionary<int, string> menuChoises = new Dictionary<int, string>();

        public Recruiter(int timerIntervalInSeconds)
        {
            Name = "Рекрутер";

            this.timerIntervalInSeconds = timerIntervalInSeconds;
            // Инициализация таймера
            _timer = new System.Timers.Timer(this.timerIntervalInSeconds * 1000); // Преобразуем секунды в миллисекунды
            _timer.Elapsed += TimerElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;

            menuChoises = MenuChoises.RecruiterChoises();

            AnsiConsole.MarkupLine("[bold]Доступные рекруты обновились![/] " + DateTime.Now);
            UpdateRecruits();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            AnsiConsole.MarkupLine("[bold]Доступные рекруты обновились![/] " + DateTime.Now);
            UpdateRecruits();
        }

        public void UpdateRecruits()
        {
            List<PlayableUnit> randomRecruits = new List<PlayableUnit>() {
                new MagePlayableUnit(),
                new MagePlayableUnit(),
                new MagePlayableUnit(),
                new MagePlayableUnit(),
            };

            recruits.Clear();

            foreach (PlayableUnit recruit in randomRecruits)
            {
                recruits.Add(recruit, 65);
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
                        ShowRecruits();
                        break;
                    case 2:
                        RoundUpRecruit(player);
                        break;
                    default:
                        loop = false;
                        break;
                }

            }
        }

        public void ShowRecruits()
        {
            foreach (var recruit in recruits)
            {
                AnsiConsole.MarkupLine(recruit.Key.Name + " ([gold1]" + recruit.Value + " золота[/])");
            }
        }

        public void RoundUpRecruit(Player player)
        {
            if (recruits.Count == 0)
            {
                AnsiConsole.MarkupLine("[bold]В данный момент нет доступных рекрутов, приходи позже.[/]");
            }
            else
            {
                List<string> recruitsNames = new List<string>();

                foreach (var item in recruits)
                    recruitsNames.Add(item.Key.Name);

                var recruit = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                    .Title("[bold]Только самые сильные рекруты![/]")
                                    .MoreChoicesText("[bold]Пролистайте ниже, чтобы увидеть все доступные варианты[/]")
                                    .AddChoices(recruitsNames));


                PayingForRecruit(player, recruits.Keys.FirstOrDefault(x => x.Name == recruit));
            }
        }

        public void PayingForRecruit(Player player, PlayableUnit recruit)
        {
            int _cost = recruits.FirstOrDefault(x => x.Key == recruit).Value;

            if (player.Inventory.IsEnoughCurrency(_cost))
            {

                if (ConfirmBuy(recruit))
                {
                    player.Inventory.RemoveCurrencies(_cost);
                    AnsiConsole.MarkupLine($"Вы завербовали {recruit.Name} , удачного совместного путешествия!");
                    player.AddToParty(recruit);
                    recruits.Remove(recruit);
                }
                else
                {
                    AnsiConsole.MarkupLine("[red bold]Вы отказались от вербовки[/]");
                }

            }
            else
            {
                AnsiConsole.MarkupLine("[bold red]Не достаточно золота![/]");
            }
        }

        public bool ConfirmBuy(PlayableUnit recruit)
        {
            AnsiConsole.MarkupLine("Вы выбрали {0}", recruit.Name);
            var confirm = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Вы точно хотите завербовать этого рекрута?[/]")
                    .AddChoices("Да", "Нет"));

            switch (confirm)
            {
                case "Да":
                    return true;
                default:
                    return false;
            }
        }
    }
}
