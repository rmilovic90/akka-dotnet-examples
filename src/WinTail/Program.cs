using Akka.Actor;

namespace WinTail
{
    class Program
    {
        static void Main(string[] args)
        {
            MyActorSystem.ActorReferences.ConsoleReader.Tell(ConsoleReaderActor.START_COMMAND);

            MyActorSystem.Instance.WhenTerminated.Wait();
        }
    }
}