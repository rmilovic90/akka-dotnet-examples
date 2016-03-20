using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Akka.Actor;
using Akka.Util.Internal;
using SystemPerformanceMonitor.Actors;

namespace SystemPerformanceMonitor
{
    public partial class Main : Form
    {
        readonly AtomicCounter seriesCounter = new AtomicCounter(1);

        IActorRef chartActor;

        public Main()
        {
            InitializeComponent();
        }

        void OnLoad(object sender, EventArgs e)
        {
            chartActor = SystemPerformanceMonitorActorSystem.Instance.ActorOf(
                Props.Create(() => new ChartingActor(systemPerformanceChart)), nameof(ChartingActor));

            var series = ChartDataHelper.RandomSeries($"FakeSeries{ seriesCounter.GetAndIncrement() }");

            chartActor.Tell(new ChartingActor.InitializeChart(new Dictionary<string, Series>()
                    {
                        { series.Name, series }
                    }
                )
            );
        }

        void OnClosing(object sender, FormClosingEventArgs e)
        {
            chartActor.Tell(PoisonPill.Instance);

            SystemPerformanceMonitorActorSystem.Instance.Terminate();
        }
    }
}