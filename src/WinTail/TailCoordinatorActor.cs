using System;
using Akka.Actor;

namespace WinTail
{
    class TailCoordinatorActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if (message is StartTail)
            {
                StartTail startTailMessage = message as StartTail;

                Context.ActorOf(Props.Create(() =>
                    new TailActor(startTailMessage.ReporterActor, startTailMessage.FilePath)));
            }
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                10,
                TimeSpan.FromSeconds(30),
                exception => {
                    if (exception is ArithmeticException)
                    {
                        return Directive.Resume;
                    }
                    else if (exception is NotSupportedException)
                    {
                        return Directive.Stop;
                    }
                    else
                    {
                        return Directive.Restart;
                    }
                }
            );
        }

        #region Message Types

        public class StartTail
        {
            public StartTail(string filePath, IActorRef reporterActor)
            {
                FilePath = filePath;
                ReporterActor = reporterActor;
            }

            public string FilePath { get; }
            public IActorRef ReporterActor { get; }
        }

        #endregion
    }
}