using System;
namespace GoldRepl
{
    public class ConsoleUtils
    {
        public ConsoleUtils()
        {

        }

        public static void Write(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
