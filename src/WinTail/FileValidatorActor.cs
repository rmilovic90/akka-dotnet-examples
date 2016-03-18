using System.IO;
using Akka.Actor;

namespace WinTail
{
    class FileValidatorActor : UntypedActor
    {
        readonly IActorRef consoleWriterActor;
        readonly IActorRef tailCoordinatorActor;

        public FileValidatorActor(IActorRef consoleWriterActor, IActorRef tailCoordinatorActor)
        {
            this.consoleWriterActor = consoleWriterActor;
            this.tailCoordinatorActor = tailCoordinatorActor;
        }

        protected override void OnReceive(object message)
        {
            string filePath = message as string;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                consoleWriterActor.Tell(new Messages.NullInputError("Input was blank. Please try again.\n"));

                Sender.Tell(new Messages.ContinueProcessing());
            }
            else
            {
                if (IsFileUri(filePath))
                {
                    consoleWriterActor.Tell(new Messages.InputSuccess($"Starting processing for { filePath }"));

                    tailCoordinatorActor.Tell(new TailCoordinatorActor.StartTail(filePath, consoleWriterActor));
                }
                else
                {
                    consoleWriterActor.Tell(new Messages.ValidationError($"{ filePath } is not an existing URI on disk."));

                    Sender.Tell(new Messages.ContinueProcessing());
                }
            }
        }

        bool IsFileUri(string path) => File.Exists(path);
    }
}