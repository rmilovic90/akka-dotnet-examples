using Akka.Actor;

namespace SystemPerformanceMonitor
{
    class SystemPerformanceMonitorActorSystem
    {
        const string ACTOR_SYSTEM_NAME = nameof(SystemPerformanceMonitorActorSystem);

        static readonly ActorSystem actorSystem = ActorSystem.Create(ACTOR_SYSTEM_NAME);

        public static ActorSystem Instance => actorSystem;
    }
}