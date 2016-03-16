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
                Messages.InputError receivedMessage = message as Messages.InputError;

                PrintColoredMessage(receivedMessage.Reason, ConsoleColor.Red);
            }
            else if (message is Messages.InputSuccess)
            {
                Messages.InputSuccess receivedMessage = message as Messages.InputSuccess;

                PrintColoredMessage(receivedMessage.Reason, ConsoleColor.Green);
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