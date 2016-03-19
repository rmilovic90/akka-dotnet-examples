using Akka.Actor;

namespace WinTail
{
    static class MyActorSystem
    {
        const string ACTOR_SYSTEM_NAME = "MyActorSystem";

        static readonly ActorSystem actorSystem = ActorSystem.Create(ACTOR_SYSTEM_NAME);

        public static ActorSystem Instance => actorSystem;

        public static class ActorReferences
        {
            static readonly IActorRef consoleWriterActor =
                actorSystem.ActorOf(Props.Create<ConsoleWriterActor>(), nameof(ConsoleWriterActor));
            static readonly IActorRef tailCoordinatorActor =
                actorSystem.ActorOf(Props.Create<TailCoordinatorActor>(), nameof(TailCoordinatorActor));
            static readonly IActorRef fileValidatorActor =
                actorSystem.ActorOf(Props.Create(() => new FileValidatorActor(consoleWriterActor)), nameof(FileValidatorActor));
            static readonly IActorRef consoleReaderActor =
                actorSystem.ActorOf(Props.Create<ConsoleReaderActor>(), nameof(ConsoleReaderActor));

            public static IActorRef ConsoleReader => consoleReaderActor;
        }

        public static class ActorSelections
        {
            static readonly string ROOT_ACTOR_PATH = $"akka://{ ACTOR_SYSTEM_NAME }/user/";

            public static ActorSelection FileValidator =>
                actorSystem.ActorSelection(ROOT_ACTOR_PATH + nameof(FileValidatorActor));
            public static ActorSelection TailCoordinator =>
                actorSystem.ActorSelection(ROOT_ACTOR_PATH + nameof(TailCoordinatorActor));
        }
    }
}