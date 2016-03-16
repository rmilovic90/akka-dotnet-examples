using Akka.Actor;

namespace WinTail
{
    class ValidationActor : UntypedActor
    {
        readonly IActorRef consoleWriterActor;

        public ValidationActor(IActorRef consoleWriterActor)
        {
            this.consoleWriterActor = consoleWriterActor;
        }

        protected override void OnReceive(object message)
        {
            string receivedMessage = message as string;

            if (string.IsNullOrWhiteSpace(receivedMessage))
            {
                consoleWriterActor.Tell(new Messages.NullInputError("No input received."));
            }
            else
            {
                if (IsValid(receivedMessage))
                {
                    consoleWriterActor.Tell(new Messages.InputSuccess("Thank you! Message was valid."));
                }
                else
                {
                    consoleWriterActor.Tell(new Messages.ValidationError("Invalid: input had odd number of characters."));
                }
            }

            Sender.Tell(new Messages.ContinueProcessing());
        }

        static bool IsValid(string message) =>
            message.Length % 2 == 0;
    }
}