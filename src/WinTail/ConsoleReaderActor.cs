using System;
using Akka.Actor;

namespace WinTail
{
    class ConsoleReaderActor : UntypedActor
    {
        public const string START_COMMAND = "start";
        public const string EXIT_COMMAND = "exit";

        readonly IActorRef validationActor;

        public ConsoleReaderActor(IActorRef validationActor)
        {
            this.validationActor = validationActor;
        }

        protected override void OnReceive(object message)
        {
            if (message.Equals(START_COMMAND))
            {
                PrintInstructions();
            }

            GetAndValidateInput();
        }

        private void PrintInstructions()
        {
            Console.WriteLine("Write whatever you want into the console!");
            Console.WriteLine("Some entries will pass validation, and some won't...\n\n");
            Console.WriteLine("Type 'exit' to quit this application at any time.\n");
        }

        private void GetAndValidateInput()
        {
            string message = Console.ReadLine();

            if (IsExitCommand(message))
            {
                Context.System.Terminate();

                return;
            }

            validationActor.Tell(message);
        }

        private bool IsExitCommand(string message) =>
            !string.IsNullOrWhiteSpace(message) &&
            message.Equals(EXIT_COMMAND, StringComparison.OrdinalIgnoreCase);
    }
}