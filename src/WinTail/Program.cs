using Akka.Actor;

namespace WinTail
{
    class Program
    {
        static ActorSystem myActorSystem;

        static void Main(string[] args)
        {
            myActorSystem = ActorSystem.Create("MyActorSystem");

            IActorRef consoleWriterActor = myActorSystem.ActorOf(
                Props.Create(() => new ConsoleWriterActor()), "consoleWriterActor");
            IActorRef consoleReaderActor = myActorSystem.ActorOf(
                Props.Create(() => new ConsoleReaderActor(consoleWriterActor)), "consoleReaderActor");

            consoleReaderActor.Tell(ConsoleReaderActor.START_COMMAND);

            myActorSystem.WhenTerminated.Wait();
        }
    }
}