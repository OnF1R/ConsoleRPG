using Spectre.Console;

namespace ConsoleRPG
{
	internal class ConsoleMessages
	{
		public static void Message(string message)
		{
			char[] chars = message.ToCharArray();

			foreach (char c in chars)
			{
				AnsiConsole.Write(c);
				Thread.Sleep(50);
			}

			Console.WriteLine();
		}
	}
}
