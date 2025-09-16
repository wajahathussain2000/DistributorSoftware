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
    public partial class InvoiceWiseReportForm : Form
    {
        private ReportViewer reportViewer;
        private TextBox txtInvoiceNumber;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnGenerateReport;
        private Button btnExportPDF;
        private Button btnClose;

        public InvoiceWiseReportForm()
        {
            InitializeComponent();
            InitializeFormControls();
            InitializeReportViewer();
            SetDefaultDates();
        }

        private void InitializeFormControls()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Invoice-wise Report";
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

            // Invoice Number filter
            Label lblInvoiceNumber = new Label();
            lblInvoiceNumber.Text = "Invoice Number:";
            lblInvoiceNumber.Location = new Point(20, 20);
            lblInvoiceNumber.Size = new Size(100, 20);
            lblInvoiceNumber.Font = new Font("Arial", 9, FontStyle.Bold);

            txtInvoiceNumber = new TextBox();
            txtInvoiceNumber.Location = new Point(20, 40);
            txtInvoiceNumber.Size = new Size(200, 25);
            txtInvoiceNumber.Font = new Font("Arial", 9);

            // Start Date filter
            Label lblStartDate = new Label();
            lblStartDate.Text = "Start Date:";
            lblStartDate.Location = new Point(240, 20);
            lblStartDate.Size = new Size(80, 20);
            lblStartDate.Font = new Font("Arial", 9, FontStyle.Bold);

            dtpStartDate = new DateTimePicker();
            dtpStartDate.Location = new Point(240, 40);
            dtpStartDate.Size = new Size(120, 25);
            dtpStartDate.Font = new Font("Arial", 9);
            dtpStartDate.Format = DateTimePickerFormat.Short;

            // End Date filter
            Label lblEndDate = new Label();
            lblEndDate.Text = "End Date:";
            lblEndDate.Location = new Point(380, 20);
            lblEndDate.Size = new Size(80, 20);
            lblEndDate.Font = new Font("Arial", 9, FontStyle.Bold);

            dtpEndDate = new DateTimePicker();
            dtpEndDate.Location = new Point(380, 40);
            dtpEndDate.Size = new Size(120, 25);
            dtpEndDate.Font = new Font("Arial", 9);
            dtpEndDate.Format = DateTimePickerFormat.Short;

            // Buttons
            btnGenerateReport = new Button();
            btnGenerateReport.Text = "Generate Report";
            btnGenerateReport.Location = new Point(520, 35);
            btnGenerateReport.Size = new Size(120, 35);
            btnGenerateReport.BackColor = Color.FromArgb(52, 152, 219);
            btnGenerateReport.ForeColor = Color.White;
            btnGenerateReport.FlatStyle = FlatStyle.Flat;
            btnGenerateReport.FlatAppearance.BorderSize = 0;
            btnGenerateReport.Font = new Font("Arial", 9, FontStyle.Bold);
            btnGenerateReport.Click += btnGenerateReport_Click;

            btnExportPDF = new Button();
            btnExportPDF.Text = "Export PDF";
            btnExportPDF.Location = new Point(650, 35);
            btnExportPDF.Size = new Size(100, 35);
            btnExportPDF.BackColor = Color.FromArgb(46, 204, 113);
            btnExportPDF.ForeColor = Color.White;
            btnExportPDF.FlatStyle = FlatStyle.Flat;
            btnExportPDF.FlatAppearance.BorderSize = 0;
            btnExportPDF.Font = new Font("Arial", 9, FontStyle.Bold);
            btnExportPDF.Click += btnExportPDF_Click;

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new Point(760, 35);
            btnClose.Size = new Size(80, 35);
            btnClose.BackColor = Color.FromArgb(231, 76, 60);
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Font = new Font("Arial", 9, FontStyle.Bold);
            btnClose.Click += btnClose_Click;

            // Add controls to filter panel
            filterPanel.Controls.AddRange(new Control[] {
                lblInvoiceNumber, txtInvoiceNumber,
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
                    MessageBox.Show("Could not find the RDLC report file. Please ensure InvoiceWiseReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Path.Combine(Application.StartupPath, "Reports", "InvoiceWiseReport.rdlc"),
                Path.Combine(Application.StartupPath, "bin", "Debug", "Reports", "InvoiceWiseReport.rdlc"),
                Path.Combine(Application.StartupPath, "bin", "Release", "Reports", "InvoiceWiseReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "Reports", "InvoiceWiseReport.rdlc")
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

                string invoiceNumber = string.IsNullOrWhiteSpace(txtInvoiceNumber.Text) ? null : txtInvoiceNumber.Text.Trim();

                DataTable reportData = GetInvoiceWiseReportData(dtpStartDate.Value, dtpEndDate.Value, invoiceNumber);

                if (reportData == null)
                {
                    reportData = CreateEmptyDataTable();
                }

                if (reportData.Rows.Count == 0)
                {
                    DataRow noDataRow = reportData.NewRow();
                    noDataRow["SalesInvoiceId"] = 0;
                    noDataRow["InvoiceNumber"] = "No data found";
                    noDataRow["InvoiceDate"] = DateTime.Now;
                    noDataRow["CustomerName"] = "N/A";
                    noDataRow["SalesmanName"] = "N/A";
                    noDataRow["TotalAmount"] = 0;
                    noDataRow["PaidAmount"] = 0;
                    noDataRow["BalanceAmount"] = 0;
                    noDataRow["Status"] = "N/A";
                    noDataRow["PaymentMode"] = "N/A";
                    reportData.Rows.Add(noDataRow);
                }

                reportViewer.LocalReport.DataSources.Clear();
                ReportDataSource reportDataSource = new ReportDataSource("InvoiceWiseDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("StartDate", dtpStartDate.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("EndDate", dtpEndDate.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("InvoiceFilter", string.IsNullOrEmpty(invoiceNumber) ? "All Invoices" : invoiceNumber)
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

        private DataTable GetInvoiceWiseReportData(DateTime startDate, DateTime endDate, string invoiceNumber)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetInvoiceWiseReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@InvoiceNumber", string.IsNullOrEmpty(invoiceNumber) ? (object)DBNull.Value : invoiceNumber);

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
            dataTable.Columns.Add("SalesInvoiceId", typeof(int));
            dataTable.Columns.Add("InvoiceNumber", typeof(string));
            dataTable.Columns.Add("InvoiceDate", typeof(DateTime));
            dataTable.Columns.Add("CustomerName", typeof(string));
            dataTable.Columns.Add("SalesmanName", typeof(string));
            dataTable.Columns.Add("TotalAmount", typeof(decimal));
            dataTable.Columns.Add("PaidAmount", typeof(decimal));
            dataTable.Columns.Add("BalanceAmount", typeof(decimal));
            dataTable.Columns.Add("Status", typeof(string));
            dataTable.Columns.Add("PaymentMode", typeof(string));
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
                saveFileDialog.FileName = $"InvoiceWiseReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

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
