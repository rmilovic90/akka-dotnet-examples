using System;
using Akka.Actor;

namespace WinTail
{
    class ConsoleWriterActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            string receivedMessage = message as string;

            if (string.IsNullOrWhiteSpace(receivedMessage))
            {
                ConsoleUtils.PrintColoredMessage("Please provide an input.\n", ConsoleColor.DarkYellow);

                return;
            }

            bool receivedMessageHasEvenNumberOfCharacters = receivedMessage.Length % 2 == 0;

            string resultMessage = "Your string had an " +
                (receivedMessageHasEvenNumberOfCharacters ? "even " : "odd ") +
                "number of characters.\n";
            ConsoleColor resultMessageColor = receivedMessageHasEvenNumberOfCharacters ?
                ConsoleColor.Red : ConsoleColor.Green;

            ConsoleUtils.PrintColoredMessage(resultMessage, resultMessageColor);
        }
    }
}