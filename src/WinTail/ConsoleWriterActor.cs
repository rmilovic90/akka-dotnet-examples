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

                ConsoleUtils.PrintColoredMessage(receivedMessage.Reason, ConsoleColor.Red);
            }
            else if (message is Messages.InputSuccess)
            {
                Messages.InputSuccess receivedMessage = message as Messages.InputSuccess;

                ConsoleUtils.PrintColoredMessage(receivedMessage.Reason, ConsoleColor.Green);
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}