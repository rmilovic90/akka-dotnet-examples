using System;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using Akka.Util;

namespace SystemPerformanceMonitor
{
    static class ChartDataHelper
    {
        public static Series RandomSeries(string name, SeriesChartType type = SeriesChartType.Line, int points = 100)
        {
            Series series = new Series(name) { ChartType = type };

            foreach (int point in Enumerable.Range(0, points))
            {
                double range = ThreadLocalRandom.Current.NextDouble();

                series.Points.Add(new DataPoint(point, 2.0 * Math.Sin(range) + Math.Sin(range / 4.5)));
            }

            series.BorderWidth = 3;

            return series;
        }
    }
}