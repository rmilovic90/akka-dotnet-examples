using System;
using Akka.Actor;

namespace WinTail
{
    class ConsoleWriterActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if (message is Messages.InputError)
            {
                Messages.InputError inputErrorMessage = message as Messages.InputError;

                PrintColoredMessage(inputErrorMessage.Reason, ConsoleColor.Red);
            }
            else if (message is Messages.InputSuccess)
            {
                Messages.InputSuccess inputSuccessMessage = message as Messages.InputSuccess;

                PrintColoredMessage(inputSuccessMessage.Reason, ConsoleColor.Green);
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        void PrintColoredMessage(string message, ConsoleColor messageColor)
        {
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}