using Ink.Runtime;
using Spectre.Console;

namespace ConsoleRPG
{
	internal static class ConsoleMessages
	{
        public static void Message(string message, bool canSkip = true)
		{
            //char[] chars = message.ToCharArray();

            for (int i = 0; i < message.Length; i++)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter && canSkip)
                {
                    AnsiConsole.Write(message[i..]);
                    break;
                }

                AnsiConsole.Write(message[i]);
                Thread.Sleep(50);
            }

			Console.WriteLine();
        }
	}
}
