using System;
using Akka.Actor;

namespace WinTail
{
    class ConsoleReaderActor : UntypedActor
    {
        public const string START_COMMAND = "start";
        public const string EXIT_COMMAND = "exit";

        protected override void OnReceive(object message)
        {
            if (message.Equals(START_COMMAND))
            {
                PrintInstructions();
            }

            GetAndValidateInput();
        }

        void PrintInstructions()
        {
            Console.WriteLine("Please provide the URI of a log file on disk.\n");
        }

        void GetAndValidateInput()
        {
            string message = Console.ReadLine();

            if (IsExitCommand(message))
            {
                Context.System.Terminate();

                return;
            }

            Context.ActorSelection($"akka://{ Program.ACTOR_SYSTEM_NAME }/user/{ nameof(FileValidatorActor) }")
                .Tell(message);
        }

        bool IsExitCommand(string message) =>
            !string.IsNullOrWhiteSpace(message) &&
            string.Equals(message, EXIT_COMMAND, StringComparison.OrdinalIgnoreCase);
    }
}