using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using Akka.Actor;

namespace SystemPerformanceMonitor.Actors
{
    class ChartingActor : UntypedActor
    {
        readonly Chart chart;

        Dictionary<string, Series> seriesIndex;

        public ChartingActor(Chart chart) : this(chart, new Dictionary<string, Series>()) {}

        public ChartingActor(Chart chart, Dictionary<string, Series> seriesIndex)
        {
            this.chart = chart;
            this.seriesIndex = seriesIndex;
        }

        protected override void OnReceive(object message)
        {
            if (message is InitializeChart)
            {
                InitializeChart initializeChart = message as InitializeChart;

                HandleInitialize(initializeChart);
            }
        }

        void HandleInitialize(InitializeChart initializeChart)
        {
            if (initializeChart.InitialSeries != null)
            {
                seriesIndex = initializeChart.InitialSeries;
            }

            chart.Series.Clear();

            if (seriesIndex.Any())
            {
                foreach (var series in seriesIndex)
                {
                    series.Value.Name = series.Key;

                    chart.Series.Add(series.Value);
                }
            }
        }

        #region Messages

        public class InitializeChart
        {
            public InitializeChart(Dictionary<string, Series> initialSeries)
            {
                InitialSeries = initialSeries;
            }

            public Dictionary<string, Series> InitialSeries { get; }
        }

        #endregion
    }
}