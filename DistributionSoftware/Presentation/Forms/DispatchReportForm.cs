using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using DistributionSoftware.Models;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class DispatchReportForm : Form
    {
        private ComboBox cmbVehicle;
        private ComboBox cmbRoute;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnGenerateReport;
        private Button btnExportPDF;
        private ReportViewer reportViewer;

        public DispatchReportForm()
        {
            InitializeComponent();
            InitializeFormControls();
            LoadFilterData();
        }

        private void InitializeFormControls()
        {
            this.Text = "Dispatch Report";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Create filter panel
            Panel filterPanel = new Panel();
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Height = 120;
            filterPanel.BackColor = Color.White;
            filterPanel.BorderStyle = BorderStyle.FixedSingle;

            // Start Date filter
            Label lblStartDate = new Label();
            lblStartDate.Text = "Start Date:";
            lblStartDate.Location = new Point(20, 20);
            lblStartDate.Size = new Size(80, 20);
            lblStartDate.Font = new Font("Arial", 9, FontStyle.Bold);

            dtpStartDate = new DateTimePicker();
            dtpStartDate.Location = new Point(20, 40);
            dtpStartDate.Size = new Size(150, 25);
            dtpStartDate.Font = new Font("Arial", 9);
            dtpStartDate.Value = DateTime.Now.AddDays(-30);

            // End Date filter
            Label lblEndDate = new Label();
            lblEndDate.Text = "End Date:";
            lblEndDate.Location = new Point(190, 20);
            lblEndDate.Size = new Size(80, 20);
            lblEndDate.Font = new Font("Arial", 9, FontStyle.Bold);

            dtpEndDate = new DateTimePicker();
            dtpEndDate.Location = new Point(190, 40);
            dtpEndDate.Size = new Size(150, 25);
            dtpEndDate.Font = new Font("Arial", 9);
            dtpEndDate.Value = DateTime.Now;

            // Vehicle filter
            Label lblVehicle = new Label();
            lblVehicle.Text = "Vehicle:";
            lblVehicle.Location = new Point(360, 20);
            lblVehicle.Size = new Size(60, 20);
            lblVehicle.Font = new Font("Arial", 9, FontStyle.Bold);

            cmbVehicle = new ComboBox();
            cmbVehicle.Location = new Point(360, 40);
            cmbVehicle.Size = new Size(150, 25);
            cmbVehicle.Font = new Font("Arial", 9);
            cmbVehicle.DropDownStyle = ComboBoxStyle.DropDownList;

            // Route filter
            Label lblRoute = new Label();
            lblRoute.Text = "Route:";
            lblRoute.Location = new Point(530, 20);
            lblRoute.Size = new Size(50, 20);
            lblRoute.Font = new Font("Arial", 9, FontStyle.Bold);

            cmbRoute = new ComboBox();
            cmbRoute.Location = new Point(530, 40);
            cmbRoute.Size = new Size(150, 25);
            cmbRoute.Font = new Font("Arial", 9);
            cmbRoute.DropDownStyle = ComboBoxStyle.DropDownList;

            // Generate Report button
            btnGenerateReport = new Button();
            btnGenerateReport.Text = "Generate Report";
            btnGenerateReport.Location = new Point(700, 40);
            btnGenerateReport.Size = new Size(120, 30);
            btnGenerateReport.Font = new Font("Arial", 9, FontStyle.Bold);
            btnGenerateReport.BackColor = Color.FromArgb(52, 152, 219);
            btnGenerateReport.ForeColor = Color.White;
            btnGenerateReport.FlatStyle = FlatStyle.Flat;
            btnGenerateReport.Click += BtnGenerateReport_Click;

            // Export PDF button
            btnExportPDF = new Button();
            btnExportPDF.Text = "Export PDF";
            btnExportPDF.Location = new Point(830, 40);
            btnExportPDF.Size = new Size(100, 30);
            btnExportPDF.Font = new Font("Arial", 9, FontStyle.Bold);
            btnExportPDF.BackColor = Color.FromArgb(46, 204, 113);
            btnExportPDF.ForeColor = Color.White;
            btnExportPDF.FlatStyle = FlatStyle.Flat;
            btnExportPDF.Click += BtnExportPDF_Click;

            // Add controls to filter panel
            filterPanel.Controls.AddRange(new Control[] {
                lblStartDate, dtpStartDate, lblEndDate, dtpEndDate,
                lblVehicle, cmbVehicle, lblRoute, cmbRoute,
                btnGenerateReport, btnExportPDF
            });

            // Create report viewer
            reportViewer = new ReportViewer();
            reportViewer.Dock = DockStyle.Fill;
            reportViewer.LocalReport.ReportPath = "Reports/DispatchReport.rdlc";

            // Add panels to form
            this.Controls.Add(reportViewer);
            this.Controls.Add(filterPanel);
        }

        private void LoadFilterData()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DistributionConnection"]?.ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Load Vehicles
                    string vehicleQuery = "SELECT VehicleId, VehicleNo FROM VehicleMaster WHERE IsActive = 1 ORDER BY VehicleNo";
                    using (SqlCommand command = new SqlCommand(vehicleQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        cmbVehicle.Items.Add(new { Value = (int?)null, Text = "All Vehicles" });
                        while (reader.Read())
                        {
                            cmbVehicle.Items.Add(new { Value = reader.GetInt32(0), Text = reader.GetString(1) });
                        }
                    }
                    cmbVehicle.DisplayMember = "Text";
                    cmbVehicle.ValueMember = "Value";
                    cmbVehicle.SelectedIndex = 0;

                    // Load Routes
                    string routeQuery = "SELECT RouteId, RouteName FROM RouteMaster WHERE IsActive = 1 ORDER BY RouteName";
                    using (SqlCommand command = new SqlCommand(routeQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        cmbRoute.Items.Add(new { Value = (int?)null, Text = "All Routes" });
                        while (reader.Read())
                        {
                            cmbRoute.Items.Add(new { Value = reader.GetInt32(0), Text = reader.GetString(1) });
                        }
                    }
                    cmbRoute.DisplayMember = "Text";
                    cmbRoute.ValueMember = "Value";
                    cmbRoute.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading filter data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateReport()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DistributionConnection"]?.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "EXEC sp_GetDispatchReport @StartDate, @EndDate, @VehicleId, @RouteId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", dtpStartDate.Value.Date);
                    command.Parameters.AddWithValue("@EndDate", dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1));
                    command.Parameters.AddWithValue("@VehicleId", ((dynamic)cmbVehicle.SelectedItem).Value ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RouteId", ((dynamic)cmbRoute.SelectedItem).Value ?? (object)DBNull.Value);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);

                        if (dataSet.Tables.Count > 0)
                        {
                            reportViewer.LocalReport.DataSources.Clear();
                            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DispatchDataSet", dataSet.Tables[0]));

                            // Set report parameters
                            ReportParameter[] parameters = new ReportParameter[]
                            {
                                new ReportParameter("StartDate", dtpStartDate.Value.ToString("MM/dd/yyyy")),
                                new ReportParameter("EndDate", dtpEndDate.Value.ToString("MM/dd/yyyy")),
                                new ReportParameter("VehicleFilter", cmbVehicle.Text),
                                new ReportParameter("RouteFilter", cmbRoute.Text)
                            };
                            reportViewer.LocalReport.SetParameters(parameters);

                            reportViewer.RefreshReport();
                        }
                        else
                        {
                            MessageBox.Show("No data found for the selected criteria.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveDialog.FileName = $"DispatchReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] pdfBytes = reportViewer.LocalReport.Render("PDF");
                    System.IO.File.WriteAllBytes(saveDialog.FileName, pdfBytes);
                    MessageBox.Show("Report exported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
