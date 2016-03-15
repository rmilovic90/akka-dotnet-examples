using System;
using Akka.Actor;

namespace WinTail
{
    class ConsoleReaderActor : UntypedActor
    {
        public const string START_COMMAND = "start";
        public const string EXIT_COMMAND = "exit";

        readonly IActorRef consoleWriterActor;

        public ConsoleReaderActor(IActorRef consoleWriterActor)
        {
            this.consoleWriterActor = consoleWriterActor;
        }

        protected override void OnReceive(object message)
        {
            if (message.Equals(START_COMMAND))
            {
                PrintInstructions();
            }
            else if (message is Messages.InputError)
            {
                consoleWriterActor.Tell(message as Messages.InputError);
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

            if (string.IsNullOrWhiteSpace(message))
            {
                Self.Tell(new Messages.NullInputError("No input received."));
            }
            else if (message.Equals(EXIT_COMMAND, StringComparison.OrdinalIgnoreCase))
            {
                Context.System.Terminate();
            }
            else
            {
                if (isValid(message))
                {
                    consoleWriterActor.Tell(new Messages.InputSuccess("Thank you! Message was valid."));

                    Self.Tell(new Messages.ContinueProcessing());
                }
                else
                {
                    Self.Tell(new Messages.ValidationError("Invalid: input had odd number of characters."));
                }
            }
        }

        private bool isValid(string message) => message.Length % 2 == 0;
    }
}