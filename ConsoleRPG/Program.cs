using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons;
using SixLabors.ImageSharp.Processing;
using Spectre.Console;
using System.Reflection;
using System.Security.Cryptography;

namespace ConsoleRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game Game = new Game();
            Game.MainMenu();
        }
    }
}   