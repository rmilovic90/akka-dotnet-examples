using System;
using Akka.Actor;

namespace WinTail
{
    class Program
    {
        static ActorSystem myActorSystem;

        static void Main(string[] args)
        {
            myActorSystem = ActorSystem.Create("MyActorSystem");

            PrintInstructions();

            IActorRef consoleWriterActor = myActorSystem.ActorOf(
                Props.Create(() => new ConsoleWriterActor()), "consoleWriterActor");
            IActorRef consoleReaderActor = myActorSystem.ActorOf(
                Props.Create(() => new ConsoleReaderActor(consoleWriterActor)), "consoleReaderActor");

            consoleReaderActor.Tell("start");

            myActorSystem.WhenTerminated.Wait();
        }

        static void PrintInstructions()
        {
            Console.WriteLine("Write whatever you want into the console!");
            Console.Write("Some lines will appear as ");
            ConsoleUtils.PrintColoredMessage("red ", ConsoleColor.DarkRed);
            Console.Write("and others will appear as ");
            ConsoleUtils.PrintColoredMessage("green!", ConsoleColor.Green);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Type 'exit' to quit this application at any time.\n");
        }
    }
}