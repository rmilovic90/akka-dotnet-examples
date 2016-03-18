using Akka.Actor;

namespace WinTail
{
    class Program
    {
        static ActorSystem myActorSystem;

        static void Main(string[] args)
        {
            myActorSystem = ActorSystem.Create("MyActorSystem");

            Props consoleWriterProps = Props.Create<ConsoleWriterActor>();
            IActorRef consoleWriterActor = myActorSystem.ActorOf(consoleWriterProps, nameof(ConsoleWriterActor));

            Props tailCoordinatorProps = Props.Create<TailCoordinatorActor>();
            IActorRef tailCoordinatorActor = myActorSystem.ActorOf(tailCoordinatorProps, nameof(TailCoordinatorActor));

            Props fileValidatorActorProps = Props.Create(() => new FileValidatorActor(consoleWriterActor, tailCoordinatorActor));
            IActorRef fileValidatorActor = myActorSystem.ActorOf(fileValidatorActorProps, nameof(FileValidatorActor));

            Props consoleReaderProps = Props.Create(() => new ConsoleReaderActor(fileValidatorActor));
            IActorRef consoleReaderActor = myActorSystem.ActorOf(consoleReaderProps, nameof(ConsoleReaderActor));

            consoleReaderActor.Tell(ConsoleReaderActor.START_COMMAND);

            myActorSystem.WhenTerminated.Wait();
        }
    }
}