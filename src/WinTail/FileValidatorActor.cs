using System.IO;
using Akka.Actor;

namespace WinTail
{
    class FileValidatorActor : UntypedActor
    {
        readonly IActorRef consoleWriterActor;

        public FileValidatorActor(IActorRef consoleWriterActor)
        {
            this.consoleWriterActor = consoleWriterActor;
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

                    Context.ActorSelection($"akka://{ Program.ACTOR_SYSTEM_NAME }/user/{ nameof(TailCoordinatorActor) }")
                        .Tell(new TailCoordinatorActor.StartTail(filePath, consoleWriterActor));
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