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
            IActorRef consoleWriterActor = myActorSystem.ActorOf(consoleWriterProps, "consoleWriterActor");

            Props validationActorProps = Props.Create(() => new ValidationActor(consoleWriterActor));
            IActorRef validationActor = myActorSystem.ActorOf(validationActorProps, "validationActor");

            Props consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
            IActorRef consoleReaderActor = myActorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");

            consoleReaderActor.Tell(ConsoleReaderActor.START_COMMAND);

            myActorSystem.WhenTerminated.Wait();
        }
    }
}