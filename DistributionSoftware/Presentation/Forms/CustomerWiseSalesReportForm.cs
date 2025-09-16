using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class CustomerWiseSalesReportForm : Form
    {
        private ReportViewer reportViewer;
        private ComboBox cmbCustomer;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnGenerateReport;
        private Button btnExportPDF;
        private Button btnClose;

        public CustomerWiseSalesReportForm()
        {
            InitializeComponent();
            InitializeFormControls();
            InitializeReportViewer();
            LoadCustomers();
            SetDefaultDates();
        }

        private void InitializeFormControls()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Customer-wise Sales Report";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 600);
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Create filter panel
            Panel filterPanel = new Panel();
            filterPanel.Location = new Point(10, 10);
            filterPanel.Size = new Size(this.ClientSize.Width - 20, 140);
            filterPanel.BackColor = Color.White;
            filterPanel.BorderStyle = BorderStyle.FixedSingle;

            // Customer filter
            Label lblCustomer = new Label();
            lblCustomer.Text = "Customer:";
            lblCustomer.Location = new Point(20, 20);
            lblCustomer.Size = new Size(80, 20);
            lblCustomer.Font = new Font("Arial", 9, FontStyle.Bold);

            cmbCustomer = new ComboBox();
            cmbCustomer.Location = new Point(20, 40);
            cmbCustomer.Size = new Size(300, 25);
            cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCustomer.Font = new Font("Arial", 9);

            // Start Date filter
            Label lblStartDate = new Label();
            lblStartDate.Text = "Start Date:";
            lblStartDate.Location = new Point(340, 20);
            lblStartDate.Size = new Size(80, 20);
            lblStartDate.Font = new Font("Arial", 9, FontStyle.Bold);

            dtpStartDate = new DateTimePicker();
            dtpStartDate.Location = new Point(340, 40);
            dtpStartDate.Size = new Size(120, 25);
            dtpStartDate.Font = new Font("Arial", 9);
            dtpStartDate.Format = DateTimePickerFormat.Short;

            // End Date filter
            Label lblEndDate = new Label();
            lblEndDate.Text = "End Date:";
            lblEndDate.Location = new Point(480, 20);
            lblEndDate.Size = new Size(80, 20);
            lblEndDate.Font = new Font("Arial", 9, FontStyle.Bold);

            dtpEndDate = new DateTimePicker();
            dtpEndDate.Location = new Point(480, 40);
            dtpEndDate.Size = new Size(120, 25);
            dtpEndDate.Font = new Font("Arial", 9);
            dtpEndDate.Format = DateTimePickerFormat.Short;

            // Buttons
            btnGenerateReport = new Button();
            btnGenerateReport.Text = "Generate Report";
            btnGenerateReport.Location = new Point(620, 35);
            btnGenerateReport.Size = new Size(120, 35);
            btnGenerateReport.BackColor = Color.FromArgb(52, 152, 219);
            btnGenerateReport.ForeColor = Color.White;
            btnGenerateReport.FlatStyle = FlatStyle.Flat;
            btnGenerateReport.FlatAppearance.BorderSize = 0;
            btnGenerateReport.Font = new Font("Arial", 9, FontStyle.Bold);
            btnGenerateReport.Click += btnGenerateReport_Click;

            btnExportPDF = new Button();
            btnExportPDF.Text = "Export PDF";
            btnExportPDF.Location = new Point(750, 35);
            btnExportPDF.Size = new Size(100, 35);
            btnExportPDF.BackColor = Color.FromArgb(46, 204, 113);
            btnExportPDF.ForeColor = Color.White;
            btnExportPDF.FlatStyle = FlatStyle.Flat;
            btnExportPDF.FlatAppearance.BorderSize = 0;
            btnExportPDF.Font = new Font("Arial", 9, FontStyle.Bold);
            btnExportPDF.Click += btnExportPDF_Click;

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new Point(860, 35);
            btnClose.Size = new Size(80, 35);
            btnClose.BackColor = Color.FromArgb(231, 76, 60);
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Font = new Font("Arial", 9, FontStyle.Bold);
            btnClose.Click += btnClose_Click;

            // Add controls to filter panel
            filterPanel.Controls.AddRange(new Control[] {
                lblCustomer, cmbCustomer,
                lblStartDate, dtpStartDate,
                lblEndDate, dtpEndDate,
                btnGenerateReport, btnExportPDF, btnClose
            });

            // Add filter panel to form
            this.Controls.Add(filterPanel);

            this.ResumeLayout(false);
        }

        private void InitializeReportViewer()
        {
            try
            {
                reportViewer = new ReportViewer();
                reportViewer.Location = new Point(0, 160);
                reportViewer.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 160);
                reportViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.BackColor = Color.White;
                reportViewer.BorderStyle = BorderStyle.None;
                reportViewer.ZoomMode = ZoomMode.PageWidth;
                reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                
                string reportPath = FindReportPath();
                if (!string.IsNullOrEmpty(reportPath))
                {
                    reportViewer.LocalReport.ReportPath = reportPath;
                }
                else
                {
                    MessageBox.Show("Could not find the RDLC report file. Please ensure CustomerWiseSalesReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                this.Controls.Add(reportViewer);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing ReportViewer: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FindReportPath()
        {
            string[] possiblePaths = {
                Path.Combine(Application.StartupPath, "Reports", "CustomerWiseSalesReport.rdlc"),
                Path.Combine(Application.StartupPath, "bin", "Debug", "Reports", "CustomerWiseSalesReport.rdlc"),
                Path.Combine(Application.StartupPath, "bin", "Release", "Reports", "CustomerWiseSalesReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "Reports", "CustomerWiseSalesReport.rdlc")
            };

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return string.Empty;
        }

        private void LoadCustomers()
        {
            try
            {
                string connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT CustomerId, CustomerName FROM Customers WHERE IsActive = 1 ORDER BY CustomerName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            cmbCustomer.Items.Add(new { CustomerId = -1, CustomerName = "All Customers" });
                            while (reader.Read())
                            {
                                cmbCustomer.Items.Add(new { CustomerId = reader.GetInt32(0), CustomerName = reader.GetString(1) });
                            }
                        }
                    }
                }
                cmbCustomer.DisplayMember = "CustomerName";
                cmbCustomer.ValueMember = "CustomerId";
                cmbCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultDates()
        {
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpEndDate.Value = DateTime.Now;
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer == null)
                {
                    MessageBox.Show("Report viewer is not initialized properly.", "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int? customerId = cmbCustomer.SelectedValue != null && (int)cmbCustomer.SelectedValue != -1 ? (int?)cmbCustomer.SelectedValue : null;

                DataTable reportData = GetCustomerWiseSalesReportData(dtpStartDate.Value, dtpEndDate.Value, customerId);

                if (reportData == null)
                {
                    reportData = CreateEmptyDataTable();
                }

                if (reportData.Rows.Count == 0)
                {
                    DataRow noDataRow = reportData.NewRow();
                    noDataRow["CustomerId"] = 0;
                    noDataRow["CustomerCode"] = "N/A";
                    noDataRow["CustomerName"] = "No data found";
                    noDataRow["Phone"] = "N/A";
                    noDataRow["City"] = "N/A";
                    noDataRow["TotalInvoicesCount"] = 0;
                    noDataRow["TotalSalesAmount"] = 0;
                    noDataRow["TotalPaidAmount"] = 0;
                    noDataRow["TotalBalanceAmount"] = 0;
                    noDataRow["OverdueAmount"] = 0;
                    noDataRow["CustomerValueCategory"] = "N/A";
                    noDataRow["PaymentBehaviorCategory"] = "N/A";
                    reportData.Rows.Add(noDataRow);
                }

                reportViewer.LocalReport.DataSources.Clear();
                ReportDataSource reportDataSource = new ReportDataSource("CustomerWiseSalesDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("StartDate", dtpStartDate.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("EndDate", dtpEndDate.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("CustomerFilter", customerId.HasValue ? (cmbCustomer?.Text ?? "Unknown Customer") : "All Customers")
                };
                
                try
                {
                    reportViewer.LocalReport.SetParameters(parameters);
                }
                catch (Exception paramEx)
                {
                    MessageBox.Show($"Error setting report parameters: {paramEx.Message}", "Parameter Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Report Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetCustomerWiseSalesReportData(DateTime startDate, DateTime endDate, int? customerId)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCustomerWiseSalesReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@CustomerId", customerId.HasValue ? (object)customerId.Value : DBNull.Value);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving report data: {ex.Message}", "Data Retrieval Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return dataTable;
        }

        private DataTable CreateEmptyDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("CustomerId", typeof(int));
            dataTable.Columns.Add("CustomerCode", typeof(string));
            dataTable.Columns.Add("CustomerName", typeof(string));
            dataTable.Columns.Add("Phone", typeof(string));
            dataTable.Columns.Add("City", typeof(string));
            dataTable.Columns.Add("TotalInvoicesCount", typeof(int));
            dataTable.Columns.Add("TotalSalesAmount", typeof(decimal));
            dataTable.Columns.Add("TotalPaidAmount", typeof(decimal));
            dataTable.Columns.Add("TotalBalanceAmount", typeof(decimal));
            dataTable.Columns.Add("OverdueAmount", typeof(decimal));
            dataTable.Columns.Add("CustomerValueCategory", typeof(string));
            dataTable.Columns.Add("PaymentBehaviorCategory", typeof(string));
            return dataTable;
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate the report first before exporting.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.FileName = $"CustomerWiseSalesReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType;
                    string encoding;
                    string fileNameExtension;

                    byte[] reportBytes = reportViewer.LocalReport.Render(
                        "PDF", null, out mimeType, out encoding, out fileNameExtension, out streamIds, out warnings);

                    File.WriteAllBytes(saveFileDialog.FileName, reportBytes);
                    MessageBox.Show($"Report exported successfully to: {saveFileDialog.FileName}", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
