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
    public partial class TaxSummaryReportForm : Form
    {
        private ReportViewer reportViewer;
        private ComboBox taxTypeComboBox;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;
        private Button generateReportButton;
        private Button exportPdfButton;
        private Button closeButton;
        private Label taxTypeLabel;
        private Label startDateLabel;
        private Label endDateLabel;

        public TaxSummaryReportForm()
        {
            InitializeComponent();
            InitializeFormControls();
            InitializeReportViewer();
            LoadTaxTypes();
            SetDefaultDates();
        }

        private void InitializeFormControls()
        {
            this.Text = "Tax Summary Report";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // Create filter panel
            Panel filterPanel = new Panel();
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Height = 100;
            filterPanel.BackColor = Color.LightGray;
            filterPanel.Padding = new Padding(10);

            // Tax Type filter
            taxTypeLabel = new Label();
            taxTypeLabel.Text = "Tax Type:";
            taxTypeLabel.Location = new Point(20, 20);
            taxTypeLabel.Size = new Size(80, 20);
            taxTypeLabel.Font = new Font("Arial", 9, FontStyle.Bold);

            taxTypeComboBox = new ComboBox();
            taxTypeComboBox.Location = new Point(100, 18);
            taxTypeComboBox.Size = new Size(150, 25);
            taxTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            taxTypeComboBox.Font = new Font("Arial", 9);

            // Start Date filter
            startDateLabel = new Label();
            startDateLabel.Text = "Start Date:";
            startDateLabel.Location = new Point(270, 20);
            startDateLabel.Size = new Size(80, 20);
            startDateLabel.Font = new Font("Arial", 9, FontStyle.Bold);

            startDatePicker = new DateTimePicker();
            startDatePicker.Location = new Point(350, 18);
            startDatePicker.Size = new Size(120, 25);
            startDatePicker.Format = DateTimePickerFormat.Short;
            startDatePicker.Font = new Font("Arial", 9);

            // End Date filter
            endDateLabel = new Label();
            endDateLabel.Text = "End Date:";
            endDateLabel.Location = new Point(480, 20);
            endDateLabel.Size = new Size(80, 20);
            endDateLabel.Font = new Font("Arial", 9, FontStyle.Bold);

            endDatePicker = new DateTimePicker();
            endDatePicker.Location = new Point(560, 18);
            endDatePicker.Size = new Size(120, 25);
            endDatePicker.Format = DateTimePickerFormat.Short;
            endDatePicker.Font = new Font("Arial", 9);

            // Buttons
            generateReportButton = new Button();
            generateReportButton.Text = "Generate Report";
            generateReportButton.Location = new Point(700, 15);
            generateReportButton.Size = new Size(120, 30);
            generateReportButton.BackColor = Color.DodgerBlue;
            generateReportButton.ForeColor = Color.White;
            generateReportButton.FlatStyle = FlatStyle.Flat;
            generateReportButton.Font = new Font("Arial", 9, FontStyle.Bold);
            generateReportButton.Click += GenerateReportButton_Click;

            exportPdfButton = new Button();
            exportPdfButton.Text = "Export PDF";
            exportPdfButton.Location = new Point(830, 15);
            exportPdfButton.Size = new Size(100, 30);
            exportPdfButton.BackColor = Color.Red;
            exportPdfButton.ForeColor = Color.White;
            exportPdfButton.FlatStyle = FlatStyle.Flat;
            exportPdfButton.Font = new Font("Arial", 9, FontStyle.Bold);
            exportPdfButton.Click += ExportPdfButton_Click;

            closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Location = new Point(940, 15);
            closeButton.Size = new Size(80, 30);
            closeButton.BackColor = Color.Gray;
            closeButton.ForeColor = Color.White;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Font = new Font("Arial", 9, FontStyle.Bold);
            closeButton.Click += CloseButton_Click;

            // Add controls to filter panel
            filterPanel.Controls.Add(taxTypeLabel);
            filterPanel.Controls.Add(taxTypeComboBox);
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

        private void LoadTaxTypes()
        {
            try
            {
                taxTypeComboBox.Items.Clear();
                taxTypeComboBox.Items.Add("All Tax Types");
                taxTypeComboBox.Items.Add("Sales Tax");
                taxTypeComboBox.Items.Add("Purchase Tax");
                taxTypeComboBox.SelectedIndex = 0; // Select "All Tax Types"
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tax types: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                string taxType = GetSelectedTaxType();

                // Validate dates
                if (startDate > endDate)
                {
                    MessageBox.Show("Start date cannot be greater than end date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get report data
                var reportData = GetTaxSummaryReportData(startDate, endDate, taxType);

                if (reportData == null || reportData.Count == 0)
                {
                    MessageBox.Show("No data found for the selected criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Set up report
                reportViewer.LocalReport.ReportPath = Path.Combine(Application.StartupPath, "Reports", "TaxSummaryReport.rdlc");
                reportViewer.LocalReport.DataSources.Clear();

                // Add data source
                ReportDataSource reportDataSource = new ReportDataSource("TaxSummaryDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                // Set parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("ReportTitle", "Tax Summary Report"),
                    new ReportParameter("CompanyName", "Distribution Software"),
                    new ReportParameter("ReportGeneratedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new ReportParameter("StartDate", startDate.ToString("yyyy-MM-dd")),
                    new ReportParameter("EndDate", endDate.ToString("yyyy-MM-dd")),
                    new ReportParameter("TaxTypeFilter", taxTypeComboBox.SelectedItem?.ToString() ?? "All Tax Types")
                };

                reportViewer.LocalReport.SetParameters(parameters);

                // Refresh report
                reportViewer.RefreshReport();

                MessageBox.Show($"Report generated successfully! Found {reportData.Count} tax invoices matching your criteria.", "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSelectedTaxType()
        {
            if (taxTypeComboBox.SelectedIndex == 0) // "All Tax Types"
                return null;

            return taxTypeComboBox.SelectedItem.ToString();
        }

        private List<TaxSummaryReportData> GetTaxSummaryReportData(DateTime startDate, DateTime endDate, string taxType)
        {
            List<TaxSummaryReportData> reportData = new List<TaxSummaryReportData>();

            try
            {
                string connectionString = ConfigurationManager.DistributionConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetTaxSummaryReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@TaxType", taxType ?? (object)DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TaxSummaryReportData data = new TaxSummaryReportData
                                {
                                    TaxType = reader["TaxType"]?.ToString() ?? "",
                                    InvoiceId = reader["InvoiceId"] != DBNull.Value ? Convert.ToInt32(reader["InvoiceId"]) : 0,
                                    InvoiceNumber = reader["InvoiceNumber"]?.ToString() ?? "",
                                    InvoiceDate = reader["InvoiceDate"] != DBNull.Value ? Convert.ToDateTime(reader["InvoiceDate"]) : DateTime.MinValue,
                                    CustomerName = reader["CustomerName"]?.ToString() ?? "",
                                    CustomerCode = reader["CustomerCode"]?.ToString() ?? "",
                                    TaxCategoryId = reader["TaxCategoryId"] != DBNull.Value ? Convert.ToInt32(reader["TaxCategoryId"]) : (int?)null,
                                    TaxCategoryName = reader["TaxCategoryName"]?.ToString() ?? "",
                                    TaxCategoryCode = reader["TaxCategoryCode"]?.ToString() ?? "",
                                    TaxableAmount = reader["TaxableAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TaxableAmount"]) : 0,
                                    TaxAmount = reader["TaxAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TaxAmount"]) : 0,
                                    TaxPercentage = reader["TaxPercentage"] != DBNull.Value ? Convert.ToDecimal(reader["TaxPercentage"]) : 0,
                                    TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalAmount"]) : 0,
                                    SubTotal = reader["SubTotal"] != DBNull.Value ? Convert.ToDecimal(reader["SubTotal"]) : 0,
                                    DiscountAmount = reader["DiscountAmount"] != DBNull.Value ? Convert.ToDecimal(reader["DiscountAmount"]) : 0,
                                    Status = reader["Status"]?.ToString() ?? "",
                                    Remarks = reader["Remarks"]?.ToString() ?? "",
                                    CreatedBy = reader["CreatedBy"] != DBNull.Value ? Convert.ToInt32(reader["CreatedBy"]) : (int?)null,
                                    CreatedDate = reader["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(reader["CreatedDate"]) : (DateTime?)null,
                                    ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : (int?)null,
                                    ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                                    TaxAccountId = reader["TaxAccountId"] != DBNull.Value ? Convert.ToInt32(reader["TaxAccountId"]) : (int?)null,
                                    StatusDescription = reader["StatusDescription"]?.ToString() ?? "",
                                    TaxAmountCategory = reader["TaxAmountCategory"]?.ToString() ?? "",
                                    TaxYear = reader["TaxYear"] != DBNull.Value ? Convert.ToInt32(reader["TaxYear"]) : 0,
                                    TaxMonth = reader["TaxMonth"] != DBNull.Value ? Convert.ToInt32(reader["TaxMonth"]) : 0,
                                    TaxMonthName = reader["TaxMonthName"]?.ToString() ?? "",
                                    TaxableStatus = reader["TaxableStatus"]?.ToString() ?? "",
                                    EffectiveTaxRate = reader["EffectiveTaxRate"] != DBNull.Value ? Convert.ToDecimal(reader["EffectiveTaxRate"]) : 0,
                                    DaysSinceInvoice = reader["DaysSinceInvoice"] != DBNull.Value ? Convert.ToInt32(reader["DaysSinceInvoice"]) : 0,
                                    DaysOverdue = reader["DaysOverdue"] != DBNull.Value ? Convert.ToInt32(reader["DaysOverdue"]) : (int?)null
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
                saveDialog.FileName = $"TaxSummaryReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

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
