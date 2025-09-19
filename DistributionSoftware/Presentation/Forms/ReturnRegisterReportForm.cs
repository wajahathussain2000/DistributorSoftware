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
    public partial class ReturnRegisterReportForm : Form
    {
        private ReportViewer reportViewer;
        private TextBox invoiceNumberTextBox;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;
        private Button generateReportButton;
        private Button exportPdfButton;
        private Button closeButton;
        private Label invoiceNumberLabel;
        private Label startDateLabel;
        private Label endDateLabel;

        public ReturnRegisterReportForm()
        {
            InitializeComponent();
            InitializeFormControls();
            InitializeReportViewer();
            SetDefaultDates();
        }

        private void InitializeFormControls()
        {
            this.Text = "Return Register Report";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // Create filter panel
            Panel filterPanel = new Panel();
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Height = 100;
            filterPanel.BackColor = Color.LightGray;
            filterPanel.Padding = new Padding(10);

            // Invoice Number filter
            invoiceNumberLabel = new Label();
            invoiceNumberLabel.Text = "Invoice Number:";
            invoiceNumberLabel.Location = new Point(20, 20);
            invoiceNumberLabel.Size = new Size(100, 20);
            invoiceNumberLabel.Font = new Font("Arial", 9, FontStyle.Bold);

            invoiceNumberTextBox = new TextBox();
            invoiceNumberTextBox.Location = new Point(120, 18);
            invoiceNumberTextBox.Size = new Size(150, 25);
            invoiceNumberTextBox.Font = new Font("Arial", 9);
            // PlaceholderText is not available on older WinForms targets; omit for compatibility

            // Start Date filter
            startDateLabel = new Label();
            startDateLabel.Text = "Start Date:";
            startDateLabel.Location = new Point(290, 20);
            startDateLabel.Size = new Size(80, 20);
            startDateLabel.Font = new Font("Arial", 9, FontStyle.Bold);

            startDatePicker = new DateTimePicker();
            startDatePicker.Location = new Point(370, 18);
            startDatePicker.Size = new Size(120, 25);
            startDatePicker.Format = DateTimePickerFormat.Short;
            startDatePicker.Font = new Font("Arial", 9);

            // End Date filter
            endDateLabel = new Label();
            endDateLabel.Text = "End Date:";
            endDateLabel.Location = new Point(500, 20);
            endDateLabel.Size = new Size(80, 20);
            endDateLabel.Font = new Font("Arial", 9, FontStyle.Bold);

            endDatePicker = new DateTimePicker();
            endDatePicker.Location = new Point(580, 18);
            endDatePicker.Size = new Size(120, 25);
            endDatePicker.Format = DateTimePickerFormat.Short;
            endDatePicker.Font = new Font("Arial", 9);

            // Buttons
            generateReportButton = new Button();
            generateReportButton.Text = "Generate Report";
            generateReportButton.Location = new Point(720, 15);
            generateReportButton.Size = new Size(120, 30);
            generateReportButton.BackColor = Color.DodgerBlue;
            generateReportButton.ForeColor = Color.White;
            generateReportButton.FlatStyle = FlatStyle.Flat;
            generateReportButton.Font = new Font("Arial", 9, FontStyle.Bold);
            generateReportButton.Click += GenerateReportButton_Click;

            exportPdfButton = new Button();
            exportPdfButton.Text = "Export PDF";
            exportPdfButton.Location = new Point(850, 15);
            exportPdfButton.Size = new Size(100, 30);
            exportPdfButton.BackColor = Color.Red;
            exportPdfButton.ForeColor = Color.White;
            exportPdfButton.FlatStyle = FlatStyle.Flat;
            exportPdfButton.Font = new Font("Arial", 9, FontStyle.Bold);
            exportPdfButton.Click += ExportPdfButton_Click;

            closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Location = new Point(960, 15);
            closeButton.Size = new Size(80, 30);
            closeButton.BackColor = Color.Gray;
            closeButton.ForeColor = Color.White;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Font = new Font("Arial", 9, FontStyle.Bold);
            closeButton.Click += CloseButton_Click;

            // Add controls to filter panel
            filterPanel.Controls.Add(invoiceNumberLabel);
            filterPanel.Controls.Add(invoiceNumberTextBox);
            filterPanel.Controls.Add(startDateLabel);
            filterPanel.Controls.Add(startDatePicker);
            filterPanel.Controls.Add(endDateLabel);
            filterPanel.Controls.Add(endDatePicker);
            filterPanel.Controls.Add(generateReportButton);
            filterPanel.Controls.Add(exportPdfButton);
            filterPanel.Controls.Add(closeButton);

            this.Controls.Add(filterPanel);
        }

        private void InitializeReportViewer()
        {
            reportViewer = new ReportViewer();
            reportViewer.Dock = DockStyle.Fill;
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.ZoomMode = ZoomMode.PageWidth;
            reportViewer.ZoomPercent = 100;
            reportViewer.ShowExportButton = false;
            reportViewer.ShowPrintButton = true;
            reportViewer.ShowRefreshButton = true;
            reportViewer.ShowZoomControl = true;
            reportViewer.ShowFindControls = true;
            reportViewer.ShowPageNavigationControls = true;
            reportViewer.ShowToolBar = true;
            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowDocumentMapButton = false;
            reportViewer.ShowPromptAreaButton = false;
            reportViewer.ShowStopButton = false;
            reportViewer.ShowProgress = true;
            reportViewer.ShowBackButton = false;
            reportViewer.ShowCredentialPrompts = false;

            this.Controls.Add(reportViewer);
        }

        private void SetDefaultDates()
        {
            // Set default date range to current month
            DateTime today = DateTime.Today;
            startDatePicker.Value = new DateTime(today.Year, today.Month, 1);
            endDatePicker.Value = today;
        }

        private void GenerateReportButton_Click(object sender, EventArgs e)
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
            try
            {
                // Get filter values
                DateTime startDate = startDatePicker.Value.Date;
                DateTime endDate = endDatePicker.Value.Date;
                string invoiceNumber = string.IsNullOrWhiteSpace(invoiceNumberTextBox.Text) ? null : invoiceNumberTextBox.Text.Trim();

                // Validate dates
                if (startDate > endDate)
                {
                    MessageBox.Show("Start date cannot be greater than end date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get report data
                var reportData = GetReturnRegisterReportData(startDate, endDate, invoiceNumber);

                if (reportData == null || reportData.Count == 0)
                {
                    MessageBox.Show("No data found for the selected criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Set up report
                reportViewer.LocalReport.ReportPath = Path.Combine(Application.StartupPath, "Reports", "ReturnRegisterReport.rdlc");
                reportViewer.LocalReport.DataSources.Clear();

                // Add data source
                ReportDataSource reportDataSource = new ReportDataSource("ReturnRegisterDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                // Set parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("ReportTitle", "Return Register Report"),
                    new ReportParameter("CompanyName", "Distribution Software"),
                    new ReportParameter("ReportGeneratedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new ReportParameter("StartDate", startDate.ToString("yyyy-MM-dd")),
                    new ReportParameter("EndDate", endDate.ToString("yyyy-MM-dd")),
                    new ReportParameter("InvoiceNumberFilter", string.IsNullOrWhiteSpace(invoiceNumber) ? "All Invoices" : invoiceNumber)
                };

                reportViewer.LocalReport.SetParameters(parameters);

                // Refresh report
                reportViewer.RefreshReport();

                MessageBox.Show($"Report generated successfully! Found {reportData.Count} returns matching your criteria.", "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ReturnRegisterReportData> GetReturnRegisterReportData(DateTime startDate, DateTime endDate, string invoiceNumber)
        {
            List<ReturnRegisterReportData> reportData = new List<ReturnRegisterReportData>();

            try
            {
                string connectionString = ConfigurationManager.DistributionConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetReturnRegisterReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber ?? (object)DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReturnRegisterReportData data = new ReturnRegisterReportData
                                {
                                    ReturnType = reader["ReturnType"]?.ToString() ?? "",
                                    ReturnId = reader["ReturnId"] != DBNull.Value ? Convert.ToInt32(reader["ReturnId"]) : 0,
                                    ReturnNumber = reader["ReturnNumber"]?.ToString() ?? "",
                                    ReturnBarcode = reader["ReturnBarcode"]?.ToString() ?? "",
                                    ReturnDate = reader["ReturnDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReturnDate"]) : DateTime.MinValue,
                                    CustomerId = reader["CustomerId"] != DBNull.Value ? Convert.ToInt32(reader["CustomerId"]) : (int?)null,
                                    CustomerName = reader["CustomerName"]?.ToString() ?? "",
                                    CustomerCode = reader["CustomerCode"]?.ToString() ?? "",
                                    CustomerPhone = reader["CustomerPhone"]?.ToString() ?? "",
                                    CustomerAddress = reader["CustomerAddress"]?.ToString() ?? "",
                                    ReferenceSalesInvoiceId = reader["ReferenceSalesInvoiceId"] != DBNull.Value ? Convert.ToInt32(reader["ReferenceSalesInvoiceId"]) : (int?)null,
                                    ReferenceInvoiceNumber = reader["ReferenceInvoiceNumber"]?.ToString() ?? "",
                                    ReferenceInvoiceDate = reader["ReferenceInvoiceDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReferenceInvoiceDate"]) : (DateTime?)null,
                                    Reason = reader["Reason"]?.ToString() ?? "",
                                    SubTotal = reader["SubTotal"] != DBNull.Value ? Convert.ToDecimal(reader["SubTotal"]) : 0,
                                    TaxAmount = reader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TaxAmount"]) : 0,
                                    DiscountAmount = reader["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(reader["DiscountAmount"]) : 0,
                                    TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalAmount"]) : 0,
                                    Status = reader["Status"]?.ToString() ?? "",
                                    Remarks = reader["Remarks"]?.ToString() ?? "",
                                    CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : (int?)null,
                                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : (DateTime?)null,
                                    ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : (int?)null,
                                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                                    ProcessedDate = reader["ProcessedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ProcessedDate"]) : (DateTime?)null,
                                    ProcessedBy = reader["ProcessedBy"] != DBNull.Value ? Convert.ToInt32(reader["ProcessedBy"]) : (int?)null,
                                    ApprovedDate = reader["ApprovedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ApprovedDate"]) : (DateTime?)null,
                                    ApprovedBy = reader["ApprovedBy"] != DBNull.Value ? Convert.ToInt32(reader["ApprovedBy"]) : (int?)null,
                                    TaxCategoryId = reader["TaxCategoryId"] != DBNull.Value ? Convert.ToInt32(reader["TaxCategoryId"]) : (int?)null,
                                    StatusDescription = reader["StatusDescription"]?.ToString() ?? "",
                                    DaysSinceReturn = reader["DaysSinceReturn"] != DBNull.Value ? Convert.ToInt32(reader["DaysSinceReturn"]) : 0,
                                    DaysToApproval = reader["DaysToApproval"] != DBNull.Value ? Convert.ToInt32(reader["DaysToApproval"]) : (int?)null,
                                    ApprovalStatus = reader["ApprovalStatus"]?.ToString() ?? "",
                                    AmountCategory = reader["AmountCategory"]?.ToString() ?? "",
                                    ReturnYear = reader["ReturnYear"] != DBNull.Value ? Convert.ToInt32(reader["ReturnYear"]) : 0,
                                    ReturnMonth = reader["ReturnMonth"] != DBNull.Value ? Convert.ToInt32(reader["ReturnMonth"]) : 0,
                                    ReturnMonthName = reader["ReturnMonthName"]?.ToString() ?? "",
                                    ProcessStatus = reader["ProcessStatus"]?.ToString() ?? "",
                                    ProcessingDays = reader["ProcessingDays"] != DBNull.Value ? Convert.ToInt32(reader["ProcessingDays"]) : (int?)null,
                                    ApprovalDays = reader["ApprovalDays"] != DBNull.Value ? Convert.ToInt32(reader["ApprovalDays"]) : (int?)null
                                };

                                reportData.Add(data);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving report data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return reportData;
        }

        private void ExportPdfButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate a report first before exporting.", "No Report", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveDialog.FileName = $"ReturnRegisterReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] reportBytes = reportViewer.LocalReport.Render("PDF");
                    File.WriteAllBytes(saveDialog.FileName, reportBytes);
                    MessageBox.Show($"Report exported successfully to: {saveDialog.FileName}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
