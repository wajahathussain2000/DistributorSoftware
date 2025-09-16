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
    public partial class PendingDeliveryReportForm : Form
    {
        private ComboBox cmbCustomer;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnGenerateReport;
        private Button btnExportPDF;
        private ReportViewer reportViewer;

        public PendingDeliveryReportForm()
        {
            InitializeComponent();
            InitializeFormControls();
            LoadFilterData();
        }

        private void InitializeFormControls()
        {
            this.Text = "Pending Delivery Report";
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

            // Customer filter
            Label lblCustomer = new Label();
            lblCustomer.Text = "Customer:";
            lblCustomer.Location = new Point(360, 20);
            lblCustomer.Size = new Size(70, 20);
            lblCustomer.Font = new Font("Arial", 9, FontStyle.Bold);

            cmbCustomer = new ComboBox();
            cmbCustomer.Location = new Point(360, 40);
            cmbCustomer.Size = new Size(200, 25);
            cmbCustomer.Font = new Font("Arial", 9);
            cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;

            // Generate Report button
            btnGenerateReport = new Button();
            btnGenerateReport.Text = "Generate Report";
            btnGenerateReport.Location = new Point(580, 40);
            btnGenerateReport.Size = new Size(120, 30);
            btnGenerateReport.Font = new Font("Arial", 9, FontStyle.Bold);
            btnGenerateReport.BackColor = Color.FromArgb(52, 152, 219);
            btnGenerateReport.ForeColor = Color.White;
            btnGenerateReport.FlatStyle = FlatStyle.Flat;
            btnGenerateReport.Click += BtnGenerateReport_Click;

            // Export PDF button
            btnExportPDF = new Button();
            btnExportPDF.Text = "Export PDF";
            btnExportPDF.Location = new Point(710, 40);
            btnExportPDF.Size = new Size(100, 30);
            btnExportPDF.Font = new Font("Arial", 9, FontStyle.Bold);
            btnExportPDF.BackColor = Color.FromArgb(46, 204, 113);
            btnExportPDF.ForeColor = Color.White;
            btnExportPDF.FlatStyle = FlatStyle.Flat;
            btnExportPDF.Click += BtnExportPDF_Click;

            // Add controls to filter panel
            filterPanel.Controls.AddRange(new Control[] {
                lblStartDate, dtpStartDate, lblEndDate, dtpEndDate,
                lblCustomer, cmbCustomer, btnGenerateReport, btnExportPDF
            });

            // Create report viewer
            reportViewer = new ReportViewer();
            reportViewer.Dock = DockStyle.Fill;
            reportViewer.LocalReport.ReportPath = "Reports/PendingDeliveryReport.rdlc";

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

                    // Load Customers
                    string customerQuery = "SELECT CustomerId, CustomerName FROM Customers ORDER BY CustomerName";
                    using (SqlCommand command = new SqlCommand(customerQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        cmbCustomer.Items.Add(new { Value = (int?)null, Text = "All Customers" });
                        while (reader.Read())
                        {
                            cmbCustomer.Items.Add(new { Value = reader.GetInt32(0), Text = reader.GetString(1) });
                        }
                    }
                    cmbCustomer.DisplayMember = "Text";
                    cmbCustomer.ValueMember = "Value";
                    cmbCustomer.SelectedIndex = 0;
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

                string query = "EXEC sp_GetPendingDeliveryReport @StartDate, @EndDate, @CustomerId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", dtpStartDate.Value.Date);
                    command.Parameters.AddWithValue("@EndDate", dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1));
                    command.Parameters.AddWithValue("@CustomerId", ((dynamic)cmbCustomer.SelectedItem).Value ?? (object)DBNull.Value);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet);

                        if (dataSet.Tables.Count > 0)
                        {
                            reportViewer.LocalReport.DataSources.Clear();
                            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("PendingDeliveryDataSet", dataSet.Tables[0]));

                            // Set report parameters
                            ReportParameter[] parameters = new ReportParameter[]
                            {
                                new ReportParameter("StartDate", dtpStartDate.Value.ToString("MM/dd/yyyy")),
                                new ReportParameter("EndDate", dtpEndDate.Value.ToString("MM/dd/yyyy")),
                                new ReportParameter("CustomerFilter", cmbCustomer.Text)
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
                saveDialog.FileName = $"PendingDeliveryReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

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
