using Spectre.Console;
using Console = Colorful.Console;
using Color = Spectre.Console.Color;
using ConsoleRPG.Quests;
using ConsoleRPG.Locations;
using ConsoleRPG.NotPlayableNPC;
using ConsoleRPG.Items.Weapons;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleRPG
{
    [Serializable]
    internal class Game
    {
        private static Dictionary<int, string> menuChoises = MenuChoises.MainMenuChoises();
        private static Dictionary<int, string> mainMenuChoises = MenuChoises.StartMenuChoises();
        private static Dictionary<Race, string> racesNames = new RacesNames().racesNames;
        private BaseLocation currentLocation = null;
        public bool isHardcore = false;
        public static int EnemiesKills = 0;

        private Player Player;

        private Merchant Merchant;

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
                new TextPrompt<string>("[red](Во избежание проблем, не используйте следующие символы:'/','\\','|','*','?',':','\"','<','>' и квадратные скобки)[/] \n" +
                "[bold]Введите имя персонажа:[/] ")
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
            Recruiter = new Recruiter(900000);

            GameMenu(Player, Merchant, Recruiter);
        }

        public void LoadGame()
        {
            FileInfo[] saveFiles;
            DirectoryInfo directoryInfo;

            if (Directory.Exists($"{Directory.GetCurrentDirectory()}\\Saves\\"))
            {
                directoryInfo = new DirectoryInfo($"{Directory.GetCurrentDirectory()}\\Saves\\");
                saveFiles = directoryInfo.GetFiles("*.astralsave");
            }
            else
            {
                Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\Saves\\");
                directoryInfo = new DirectoryInfo($"{Directory.GetCurrentDirectory()}\\Saves\\");
                saveFiles = directoryInfo.GetFiles("*.astralsave");
            }
            
            Dictionary<int, string> saves = new Dictionary<int, string>();

            for (int i = 0; i < saveFiles.Length; i++)
            {
                saves.Add(i + 1, saveFiles[i].Name);
            }

            if (saves.Count > 0)
            {
                var choise = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Выберите сохранение.[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(saves.Values));

                Game savedGame = DeserializeObject<Game>($"{directoryInfo}\\{choise}");
                savedGame.GameMenu(savedGame.Player, savedGame.Merchant, savedGame.Recruiter);
                Console.Clear();
            } 
            else
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[red]Сохранения не найдены![/]");
                MainMenu();
            }
        }

        private void SaveGame()
        {
            try
            {
                DirectoryInfo directoryInfo;

                if (Directory.Exists($"{Directory.GetCurrentDirectory()}\\Saves\\"))
                {
                    directoryInfo = new DirectoryInfo($"{Directory.GetCurrentDirectory()}\\Saves\\");
                }
                else
                {
                    Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\Saves\\");
                    directoryInfo = new DirectoryInfo($"{Directory.GetCurrentDirectory()}\\Saves\\");
                }

                string formattedDate = $"{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}";
                SerializeObject($"{directoryInfo}\\{Player.Name}_{formattedDate}.astralsave", this);
                AnsiConsole.MarkupLine("Сохранение создано успешно.");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Ошибка, сохранение не создано.[/] {ex.Message}");
            }
        }

        private void GameMenu(Player player, Merchant merchant, Recruiter recruiter)
        {
            
            if (player.IsUnitStateChangingEventIsNull())
                player.UnitStateChanging += ConsoleMessages.SendMessage;

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

                var choise = ConsoleMessages.Choice(menuChoises.Values);

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
                            if (CurrentBoss.IsUnitStateChangingEventIsNull())
                                CurrentBoss.UnitStateChanging += ConsoleMessages.SendMessage;


                            Fight.StartTeamFight(player, CurrentBoss, currentLocation);
                        }
                        else
                        {
                            var enemy = ExistableEnemies.GetRandomEnemy(currentLocation.locationId, player.Level);

                            if (enemy.IsUnitStateChangingEventIsNull())
                                enemy.UnitStateChanging += ConsoleMessages.SendMessage;

                            Fight.StartTeamFight(player, enemy, currentLocation);
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
                        AnsiConsole.MarkupLine("[red]Выберите существующий вариант![/]");
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

        private Player CreatePlayer(string name, Race race)
        {
            Player Player = new Player(name, race);
            //Player.AddQuest(new SmallKillingQuest());
            //Player.Inventory.AddItem(new FireSword(1));
            return Player;
        }
    }
}
