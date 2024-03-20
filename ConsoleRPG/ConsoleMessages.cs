using Spectre.Console;

namespace ConsoleRPG
{
    internal static class ConsoleMessages
    {
        public static void SendMessage(object sender, UnitEventArgs e)
        {
            AnsiConsole.MarkupLine(e.Message);
        }

        public static void PrintMessage(string message, bool canSkip = true, int printSpeedMilliSeconds = 50)
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
                Thread.Sleep(printSpeedMilliSeconds);
            }

            Console.WriteLine();
        }

        public static string Choice(IEnumerable<string> values, string title = "[bold]Что будете делать?[/]")
        {
            var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title(title)
                        .PageSize(10)
                        .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                        .AddChoices(values));

            return choice;
        }
    }
}
