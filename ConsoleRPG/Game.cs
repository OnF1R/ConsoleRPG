using Spectre.Console;
using Console = Colorful.Console;
using Color = Spectre.Console.Color;
using ConsoleRPG.Quests;
using ConsoleRPG.Locations;
using ConsoleRPG.NotPlayableNPC;
using ConsoleRPG.Items.Weapons;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ConsoleRPG
{
    [Serializable]
    internal class Game
    {
        [JsonProperty]
        private static Dictionary<int, string> menuChoises = MenuChoises.MainMenuChoises();
        [JsonProperty]
        private static Dictionary<int, string> mainMenuChoises = MenuChoises.StartMenuChoises();
        [JsonProperty]
        private static Dictionary<Race, string> racesNames = new RacesNames().racesNames;

        [JsonProperty]
        private BaseLocation currentLocation = null;

        public bool isHardcore = false;
        [JsonProperty]
        public static int EnemiesKills = 0;

        [JsonProperty]
        private Player Player;
        [JsonProperty]
        private Merchant Merchant;
        [JsonProperty]
        private Recruiter Recruiter;


        public Enemy CurrentBoss = null;

        private static void SerializeObject<T>(string filename, T obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                formatter.Serialize(stream, obj);
            }
        }

        // Метод для десериализации объекта
        private static T DeserializeObject<T>(string filename)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
                return (T)formatter.Deserialize(stream);
            }
        }

        public Game()
        {
            menuChoises = MenuChoises.MainMenuChoises();
            mainMenuChoises = MenuChoises.StartMenuChoises();
            racesNames = new RacesNames().racesNames;
        }

        private void DeathLogo()
        {
            Console.Clear();
            AnsiConsole.Write(
            new FigletText("YOU DIED")
                .Centered()
                .Color(Color.Red1));

            Thread.Sleep(5000);
            Console.ReadKey();
        }

        public void MainMenu()
        {
            AnsiConsole.Write(
            new FigletText("Astral Game")
                .Centered()
                .Color(Color.DeepSkyBlue1));

            var choise = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(3)
                    //.MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                    .AddChoices(mainMenuChoises.Values));
            //Console.Clear();

            switch (mainMenuChoises.FirstOrDefault(x => x.Value == choise).Key)
            {
                case 1:
                    StartGame();
                    break;
                case 2:
                    LoadGame();
                    break;
                default:
                    break;
            }
        }

        private Player CreateHero()
        {
            return CreatePlayer(EnterName(), ChooseRace());
        }

        private string EnterName()
        {
            var playerName = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Введите имя персонажа:[/]")
                    .PromptStyle("bold"));

            return playerName;
        }

        private Race ChooseRace()
        {
            var raceChoise = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold]Выберите расу: [/]")
                .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                .AddChoices(racesNames.Values));

            AnsiConsole.MarkupLine(racesNames.FirstOrDefault(x => x.Value == raceChoise).Key.RaceInfo());

            var confirm = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold]Вы точно хотите выбрать эту расу? [/]")
                .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                .AddChoices("Да", "Нет"));

            switch (confirm)
            {
                case "Да":
                    Console.Clear();
                    return racesNames.FirstOrDefault(x => x.Value == raceChoise).Key;
                default:
                    Console.Clear();
                    return ChooseRace();
            }

        }

        private void ChooseMode()
        {
            var confirm = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold]Выберите режим игры[/]")
                .AddChoices("Нормальный", "[red]Хардкор (Одна жизнь)[/]"));

            if (confirm != "Нормальный")
            {
                isHardcore = true;
            }
        }

        private void StartGame()
        {
            ChooseMode();
            Player = CreateHero();
            Merchant = new Merchant(900000);
            Recruiter = new Recruiter();

            GameMenu(Player, Merchant, Recruiter);
        }

        public void LoadGame()
        {
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
            var rgFiles = directory.GetFiles("*.astralsave");
            Dictionary<int, string> saves = new Dictionary<int, string>();

            for (int i = 0; i < rgFiles.Length; i++)
            {
                saves.Add(i + 1, rgFiles[i].Name);
            }

            var choise = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Выберите сохранение.[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(saves.Values));


            Game savedGame = DeserializeObject<Game>(choise);
            savedGame.GameMenu(savedGame.Player, savedGame.Merchant, savedGame.Recruiter);
            Console.Clear();
        }

        private void SaveGame()
        {
            try
            {
                string formattedDate = $"{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}";
                SerializeObject($"{Player.Name}_{formattedDate}.astralsave", this);
                AnsiConsole.MarkupLine("Сохранение создано успешно.");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Ошибка, сохранение не создано.[/] {ex.Message}");
            }
        }

        private void GameMenu(Player player, Merchant merchant, Recruiter recruiter)
        {
            bool Loop = true;
            while (Loop)
            {
                if (player.IsDead)
                {
                    if (isHardcore)
                    {
                        Loop = false;
                        DeathLogo();
                        break;
                    }

                    player.Resurrection();
                }

                var choise = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Что будете делать?[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(menuChoises.Values));


                Console.Clear();

                switch (menuChoises.FirstOrDefault(x => x.Value == choise).Key)
                {
                    case 1:
                        if (currentLocation == null)
                        {
                            player.ShowMessage("[red]Вы сейчас не находитесь ни в какой локации, измените локацию.[/]");
                            break;
                        }

                        if (player.KillCountNumber() > 0 && player.KillCountNumber() % 50 == 0)
                        {
                            if (CurrentBoss == null || CurrentBoss.IsDead)
                            {
                                CurrentBoss = ExistableEnemies.GetRandomBoss(currentLocation.locationId, player.Level);
                            }

                            Fight.StartTeamFight(player, CurrentBoss, currentLocation);
                        }
                        else
                        {
                            Fight.StartTeamFight(player, ExistableEnemies.GetRandomEnemy(currentLocation.locationId, player.Level), currentLocation);
                        }

                        break;
                    case 2:
                        Adventure(player);
                        break;
                    case 3:
                        merchant.SellingMenu(player);
                        break;
                    case 4:
                        player.Inventory.ShowInventory();
                        break;
                    case 5:
                        player.Equipment.EquipmentMenu(player, player);
                        break;
                    case 6:
                        player.ShowCharacteristics();
                        break;
                    case 7:
                        Crafting.CraftingMenu(player);
                        break;
                    case 8:
                        var quest = player.QuestsInfo();
                        if (quest != null)
                        {
                            player.ShowMessage(quest.Description());
                        }
                        break;
                    case 9:
                        player.KillCountInfo();
                        break;
                    case 10:
                        player.TeamControl();
                        break;
                    case 11:
                        recruiter.SellingMenu(player);
                        break;
                    case 12:
                        SaveGame();
                        break;
                    default:
                        Console.WriteLine("Выберите существующий вариант.");
                        break;
                }
            }
        }

        private void Adventure(Player player)
        {
            if (player.KillCountNumber() > 0 && player.KillCountNumber() % 50 == 0)
            {
                player.ShowMessage("[red]Невозможно сменить локацию во время боя с боссом![/]");
                return;
            }

            if (currentLocation != null)
            {
                currentLocation.ExitLocation(player);

                foreach (var unit in player.Team)
                    currentLocation.ExitLocation(unit);

                var tempCurrentLocation = ExistableLocations.GetRandomLocation();
                while (currentLocation == tempCurrentLocation)
                {
                    tempCurrentLocation = ExistableLocations.GetRandomLocation();
                }

                currentLocation = tempCurrentLocation;
            }
            else
            {
                currentLocation = ExistableLocations.GetRandomLocation();
            }

            currentLocation.EnterLocation(player);

            foreach (var unit in player.Team)
                currentLocation.EnterLocation(unit);
        }

        private Player CreatePlayer(string Name, Race race)
        {
            Player Player = new();
            Player.Name = Name;
            Player.Level = 1;
            Player.MaxHealth = 100;
            Player.CurrentHealth = Player.MaxHealth;
            Player.CurrentExp = 0;
            Player.NextLevelExp = 100;
            Player.IsDead = false;
            //Player.Luck = 0;
            Player.MyRace = race;
            Player.AddQuest(new SmallKillingQuest());
            Player.Inventory.AddItem(new FireSword(1));
            return Player;
        }
    }
}
