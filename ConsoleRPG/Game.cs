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

namespace ConsoleRPG
{
    class Game
    {
        private static Dictionary<int, string> MenuChoises = new Dictionary<int, string>();

        public Game()
        {
            MenuChoises = GenerateMenuChoises();
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

        public Enemy GetRandomEnemy()
        {
            Enemy[] enemies = new Enemy[] { new FireMage(), new Elemental(), new Ghoul(), new SteelKnight(), new ExplosiveBug() };
            Random rand = new Random();
            Enemy enemy = enemies[rand.Next(enemies.Length)];
            return enemy;
        }

        public Player CreateHero()
        {
            Console.Write("Создание персонажа, введите имя: ");
            return CreatePlayer(Console.ReadLine());
        }

        public void StartGame()
        {
            Player player = CreateHero();
            Merchant merchant = new Merchant();
            MainMenu(player, merchant);
        }

        public void MainMenu(Player Player, Merchant merchant)
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
                        .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
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
                        EquipmentMenu(Player);
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

        public void EquipmentMenu(Player Player)
        {
            bool Loop = true;
            while (Loop)
            {
                Console.WriteLine("1.Посмотреть экипировку\n2.Изменить экипировку\n3.Выйти");
                Console.Write("Выберите действие: ");
                switch (Convert.ToString(Console.ReadLine()))
                {
                    case "1":
                        Player.Equipment.ShowWearEquipment();
                        break;
                    case "2":
                        Player.Equipment.ChangeEquipmentMenu(Player.Inventory);
                        break;
                    default:
                        Loop = false;
                        break;
                }
            }
        }

        //public void Fight(Player Player, Enemy Enemy)
        //{
        //    AnsiConsole.MarkupLine("[red]На вас напал[/] [bold]{0}[/] (Уровень: {1})", Enemy.Name, Enemy.Level);
        //    while (!Player.IsDead && !Enemy.IsDead)
        //    {
        //        AnsiConsole.MarkupLine("1. Атаковать\n2. Сбежать");
        //        AnsiConsole.Markup("Выберите действие: ");
        //        switch(Convert.ToString(Console.ReadLine()))
        //        {
        //            case "1":
        //                Player.BasicAttack(Enemy);
        //                break;
        //            case "2":
        //                break;
        //            default:
        //                AnsiConsole.MarkupLine("Выберите существующие действие.");
        //                break;
        //        }
        //    }
        //}

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
