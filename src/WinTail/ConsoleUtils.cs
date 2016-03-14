using System;

namespace WinTail
{
    static class ConsoleUtils
    {
        public static void PrintColoredMessage(string message, ConsoleColor messageColor)
        {
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}