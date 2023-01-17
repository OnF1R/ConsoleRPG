using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Spectre.Console;
using Console = Colorful.Console;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Items.Currencies;
using System.Reflection.Metadata.Ecma335;
using ConsoleRPG.Enemies;
using Color = Spectre.Console.Color;
using ConsoleRPG.Races;
using ConsoleRPG.Items.StacableItems;

namespace ConsoleRPG
{
    class Game
    { 
        private static Dictionary<int, string> MenuChoises;
        private static Dictionary<int, string> MainMenuChoises;
        private static Dictionary<Race, string> RacesNames = new RacesNames().racesNames;
        public bool isHardcore = false;

        public Game()
        {
            MenuChoises = new Dictionary<int, string>();
            MenuChoises = GenerateMenuChoises();
            MainMenuChoises = new Dictionary<int, string>();
            MainMenuChoises = GenerateMainMenuChoises();
        }

        private static Dictionary<int, string> GenerateMenuChoises()
        {
            MenuChoises.Add(1, "Отправиться в приключение");
            MenuChoises.Add(2, "Торговец");
            MenuChoises.Add(3, "Инвентарь");
            MenuChoises.Add(4, "Экипировка");
            MenuChoises.Add(5, "Характеристики");
            MenuChoises.Add(6, "Крафт");

            return MenuChoises;
        }

        private static Dictionary<int, string> GenerateMainMenuChoises()
        {
            MainMenuChoises.Add(1, "Новая игра");
            MainMenuChoises.Add(2, "Загрузить");
            MainMenuChoises.Add(3, "Выйти");

            return MainMenuChoises;
        }

        private void DeathLogo()
        {
            Console.Clear();
            AnsiConsole.Write(
            new FigletText("YOU DIED")
                .Centered()
                .Color(Color.Red1));
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
                    .AddChoices(MainMenuChoises.Values));
            //Console.Clear();

            switch (MainMenuChoises.FirstOrDefault(x => x.Value == choise).Key)
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

        private Enemy GetRandomEnemy()
        {
            Enemy[] enemies = new Enemy[] { new FireMage(), new Elemental(), new Ghoul(), new SteelKnight(), new ExplosiveBug() };
            Random rand = new Random();
            Enemy enemy = enemies[rand.Next(enemies.Length)];
            return enemy;
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
                .AddChoices(RacesNames.Values));

            AnsiConsole.MarkupLine(RacesNames.FirstOrDefault(x => x.Value == raceChoise).Key.RaceInfo());

            var confirm = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold]Вы точно хотите выбрать эту расу? [/]")
                .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                .AddChoices("Да", "Нет"));

            switch (confirm)
            {
                case "Да":
                    Console.Clear();
                    return RacesNames.FirstOrDefault(x => x.Value == raceChoise).Key;
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
            GameMenu(player, merchant);
        }

        private void GameMenu(Player Player, Merchant merchant)
        {
            bool Loop = true;
            while (Loop)
            {
                if (Player.IsDead)
                {
                    if (isHardcore)
                    {
                        Loop = false;
                        DeathLogo();
                        break;
                    }

                    Player.Resurrection();
                }

                var choise = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold]Что будете делать?[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(MenuChoises.Values));


                Console.Clear();

                switch (MenuChoises.FirstOrDefault(x => x.Value == choise).Key)
                {
                    case 1:
                        new Fight().StartFight(Player, GetRandomEnemy());
                        break;
                    case 2:
                        merchant.SellingMenu(Player);
                        break;
                    case 3:
                        Player.Inventory.ShowInventory();
                        break;
                    case 4:
                        Player.Equipment.EquipmentMenu(Player);
                        break;
                    case 5:
                        Player.ShowCharacteristics();
                        break;
                    case 6:
                        Craft craft = new(Player, new FireSword(), new Dictionary<Item, int>() { { new IronOre(), 2 } });
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
            Player.Race = race;
            return Player;
        }
    }
}
