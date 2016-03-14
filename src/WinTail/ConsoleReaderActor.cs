using System;
using Akka.Actor;

namespace WinTail
{
    class ConsoleReaderActor : UntypedActor
    {
        public const string EXIT_COMMAND = "exit";

        readonly IActorRef consoleWriterActor;

        public ConsoleReaderActor(IActorRef consoleWriterActor)
        {
            this.consoleWriterActor = consoleWriterActor;
        }

        protected override void OnReceive(object message)
        {
            string line = Console.ReadLine();

            if (IsExitCommand(line))
            {
                Context.System.Terminate();

                return;
            }

            consoleWriterActor.Tell(line);

            Self.Tell("continue");
        }

        bool IsExitCommand(string line) =>
            !string.IsNullOrWhiteSpace(line) &&
            string.Equals(line, EXIT_COMMAND, StringComparison.OrdinalIgnoreCase);
    }
}