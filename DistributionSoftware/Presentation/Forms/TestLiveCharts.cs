using System;
using System.Windows.Forms;
// using LiveCharts;
// using LiveCharts.WinForms;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class TestLiveCharts : Form
    {
        public TestLiveCharts()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // 
            // TestLiveCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "TestLiveCharts";
            this.Text = "Test LiveCharts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            
            this.ResumeLayout(false);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // LiveCharts testing temporarily disabled
            // try
            // {
            //     // Create a simple line chart
            //     var chart = new CartesianChart
            //     {
            //         Dock = DockStyle.Fill,
            //         BackColor = System.Drawing.Color.White
            //     };

            //     var series = new LiveCharts.Wpf.LineSeries
            //     {
            //         Title = "Test Data",
            //         Values = new ChartValues<double> { 1, 2, 3, 4, 5 },
            //         PointGeometry = LiveCharts.Wpf.DefaultGeometries.Circle,
            //         PointGeometrySize = 12,
            //         LineSmoothness = 0.5,
            //         Fill = System.Windows.Media.Brushes.Transparent,
            //         Stroke = System.Windows.Media.Brushes.DodgerBlue,
            //         StrokeThickness = 3
            //     };

            //     chart.Series = new SeriesCollection { series };
            //     chart.AxisX[0].Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
                
            //     // Add title label
            //     var titleLabel = new Label
            //     {
            //         Text = "LiveCharts Test",
            //         Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
            //         AutoSize = true,
            //         Location = new System.Drawing.Point(10, 10)
            //     };

            //     this.Controls.Add(titleLabel);
            //     this.Controls.Add(chart);

            //     // Show success message
            //     MessageBox.Show("LiveCharts is working perfectly! 🎉\n\nNow let's test the full dashboard.", 
            //         "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //     // Test the full dashboard after a delay
            //     Timer timer = new Timer();
            //     timer.Interval = 2000;
            //     timer.Tick += (s, args) =>
            //     {
            //         timer.Stop();
            //         var adminDashboard = new AdminDashboardRedesigned();
            //         adminDashboard.Show();
            //         this.Close();
            //     };
            //     timer.Start();
            // }
            // catch (Exception ex)
            // {
            //     MessageBox.Show($"Error testing LiveCharts: {ex.Message}\n\nPlease make sure LiveCharts.WinForms NuGet package is installed.", 
            //         "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // }
        }
    }
}
