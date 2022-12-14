using SixLabors.ImageSharp.Processing;
using Spectre.Console;

namespace ConsoleRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game Game = new Game();
            Player Player = Game.CreateHero();
            Game.Adventure(Player);
        }
    }
}   