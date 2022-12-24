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

namespace ConsoleRPG
{
    class Game
    {

        private static Dictionary<int, string> MenuChoises;
        private static Dictionary<int, string> MainMenuChoises;

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

        public Enemy GetRandomEnemy()
        {
            Enemy[] enemies = new Enemy[] { new FireMage(), new Elemental(), new Ghoul(), new SteelKnight(), new ExplosiveBug() };
            Random rand = new Random();
            Enemy enemy = enemies[rand.Next(enemies.Length)];
            return enemy;
        }

        public Player CreateHero()
        {
            var playerName = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold]Введите имя персонажа:[/]")
                    .PromptStyle("bold"));

            return CreatePlayer(playerName);
        }

        public void StartGame()
        {
            Player player = CreateHero();
            Merchant merchant = new Merchant();
            GameMenu(player, merchant);
        }

        public void GameMenu(Player Player, Merchant merchant)
        {
            bool Loop = true;
            while (Loop)
            {
                if (Player.IsDead)
                {
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
                        break;
                    case 6:
                        break;
                    default:
                        Console.WriteLine("Выберите существующий вариант.");
                        break;
                }
            }
        }

        public void Adventure(Player Player)
        {

        }

        public Player CreatePlayer(string Name)
        {
            Player Player = new Player();
            Player.Name = Name;
            Player.Level = 1;
            Player.Damage = 3;
            Player.MaxHealth = 100;
            Player.CurrentHealth = Player.MaxHealth;
            Player.CurrentExp = 0;
            Player.NextLevelExp = 100;
            Player.IsDead = false;
            Player.Luck = 0;
            return Player;
        }
    }
}
