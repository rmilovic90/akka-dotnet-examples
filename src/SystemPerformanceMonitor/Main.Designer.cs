namespace SystemPerformanceMonitor
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private System.Windows.Forms.DataVisualization.Charting.Chart systemPerformanceChart;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            System.Windows.Forms.DataVisualization.Charting.ChartArea systemPerformanceChartArea = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend systemPerformanceChartLegend = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series systemPerformanceChartSeries = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.systemPerformanceChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize) (this.systemPerformanceChart)).BeginInit();
            this.SuspendLayout();

            //
            // System Performance Chart
            //

            systemPerformanceChartArea.Name = "SystemPerformanceChartArea";
            this.systemPerformanceChart.ChartAreas.Add(systemPerformanceChartArea);
            this.systemPerformanceChart.Dock = System.Windows.Forms.DockStyle.Fill;
            systemPerformanceChartLegend.Name = "SystemPerformanceChartLegend";
            this.systemPerformanceChart.Legends.Add(systemPerformanceChartLegend);
            this.systemPerformanceChart.Location = new System.Drawing.Point(0, 0);
            this.systemPerformanceChart.Name = "SystemPerformanceChart";
            systemPerformanceChartSeries.ChartArea = "SystemPerformanceChartArea";
            systemPerformanceChartSeries.Legend = "SystemPerformanceChartLegend";
            systemPerformanceChartSeries.Name = "SystemPerformanceChartSeries";
            this.systemPerformanceChart.Series.Add(systemPerformanceChartSeries);
            this.systemPerformanceChart.Size = new System.Drawing.Size(684, 446);
            this.systemPerformanceChart.TabIndex = 0;
            this.systemPerformanceChart.Text = "System Performance Chart";

            //
            // Main
            //

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 446);
            this.Controls.Add(this.systemPerformanceChart);
            this.Name = "Main";
            this.Text = "System Metrics";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            ((System.ComponentModel.ISupportInitialize) (this.systemPerformanceChart)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}