﻿using Spectre.Console;
using Console = Colorful.Console;
using Color = Spectre.Console.Color;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG
{
    class Game
    {
        private static Dictionary<int, string> menuChoises;
        private static Dictionary<int, string> mainMenuChoises;
        private static Dictionary<Race, string> racesNames;
        public bool isHardcore = false;
        public int EnemiesKills = 0;

        public Enemy CurrentBoss = null;

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
            Player player = CreateHero();
            Merchant merchant = new Merchant();
            Crafting crafting = new Crafting();
            GameMenu(player, merchant, crafting);
        }

        private void GameMenu(Player player, Merchant merchant, Crafting crafting)
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
                        if (EnemiesKills > 0 && EnemiesKills % 50 == 0)
                        {

                            if (CurrentBoss == null || CurrentBoss.IsDead)
                            {
                                CurrentBoss = ExistableEnemies.GetRandomBoss(player.Level);
                            }

                            Fight.StartFight(player, CurrentBoss, ref EnemiesKills);
                        }
                        else
                        {
                            Fight.StartFight(player, ExistableEnemies.GetRandomEnemy(player.Level), ref EnemiesKills);
                        }
                        break;
                    case 2:
                        merchant.SellingMenu(player);
                        break;
                    case 3:
                        player.Inventory.ShowInventory();
                        break;
                    case 4:
                        player.Equipment.EquipmentMenu(player);
                        break;
                    case 5:
                        player.ShowCharacteristics();
                        break;
                    case 6:
                        crafting.CraftingMenu(player);
                        break;
                    default:
                        Console.WriteLine("Выберите существующий вариант.");
                        break;
                }
            }
        }

        private void Adventure(Player Player)
        {

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
            return Player;
        }
    }
}
