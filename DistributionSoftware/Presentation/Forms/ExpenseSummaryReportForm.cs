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
    public partial class ExpenseSummaryReportForm : Form
    {
        private ReportViewer reportViewer;
        private ComboBox categoryComboBox;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;
        private Button generateReportButton;
        private Button exportPdfButton;
        private Button closeButton;
        private Label categoryLabel;
        private Label startDateLabel;
        private Label endDateLabel;

        public ExpenseSummaryReportForm()
        {
            InitializeComponent();
            InitializeFormControls();
            InitializeReportViewer();
            LoadCategories();
            SetDefaultDates();
        }

        private void InitializeFormControls()
        {
            this.Text = "Expense Summary Report";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // Create filter panel
            Panel filterPanel = new Panel();
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Height = 100;
            filterPanel.BackColor = Color.LightGray;
            filterPanel.Padding = new Padding(10);

            // Category filter
            categoryLabel = new Label();
            categoryLabel.Text = "Category:";
            categoryLabel.Location = new Point(20, 20);
            categoryLabel.Size = new Size(80, 20);
            categoryLabel.Font = new Font("Arial", 9, FontStyle.Bold);

            categoryComboBox = new ComboBox();
            categoryComboBox.Location = new Point(100, 18);
            categoryComboBox.Size = new Size(200, 25);
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryComboBox.Font = new Font("Arial", 9);

            // Start Date filter
            startDateLabel = new Label();
            startDateLabel.Text = "Start Date:";
            startDateLabel.Location = new Point(320, 20);
            startDateLabel.Size = new Size(80, 20);
            startDateLabel.Font = new Font("Arial", 9, FontStyle.Bold);

            startDatePicker = new DateTimePicker();
            startDatePicker.Location = new Point(400, 18);
            startDatePicker.Size = new Size(150, 25);
            startDatePicker.Format = DateTimePickerFormat.Short;
            startDatePicker.Font = new Font("Arial", 9);

            // End Date filter
            endDateLabel = new Label();
            endDateLabel.Text = "End Date:";
            endDateLabel.Location = new Point(570, 20);
            endDateLabel.Size = new Size(80, 20);
            endDateLabel.Font = new Font("Arial", 9, FontStyle.Bold);

            endDatePicker = new DateTimePicker();
            endDatePicker.Location = new Point(650, 18);
            endDatePicker.Size = new Size(150, 25);
            endDatePicker.Format = DateTimePickerFormat.Short;
            endDatePicker.Font = new Font("Arial", 9);

            // Buttons
            generateReportButton = new Button();
            generateReportButton.Text = "Generate Report";
            generateReportButton.Location = new Point(820, 15);
            generateReportButton.Size = new Size(120, 30);
            generateReportButton.BackColor = Color.DodgerBlue;
            generateReportButton.ForeColor = Color.White;
            generateReportButton.FlatStyle = FlatStyle.Flat;
            generateReportButton.Font = new Font("Arial", 9, FontStyle.Bold);
            generateReportButton.Click += GenerateReportButton_Click;

            exportPdfButton = new Button();
            exportPdfButton.Text = "Export PDF";
            exportPdfButton.Location = new Point(950, 15);
            exportPdfButton.Size = new Size(100, 30);
            exportPdfButton.BackColor = Color.Red;
            exportPdfButton.ForeColor = Color.White;
            exportPdfButton.FlatStyle = FlatStyle.Flat;
            exportPdfButton.Font = new Font("Arial", 9, FontStyle.Bold);
            exportPdfButton.Click += ExportPdfButton_Click;

            closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Location = new Point(1060, 15);
            closeButton.Size = new Size(80, 30);
            closeButton.BackColor = Color.Gray;
            closeButton.ForeColor = Color.White;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Font = new Font("Arial", 9, FontStyle.Bold);
            closeButton.Click += CloseButton_Click;

            // Add controls to filter panel
            filterPanel.Controls.Add(categoryLabel);
            filterPanel.Controls.Add(categoryComboBox);
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

        private void LoadCategories()
        {
            try
            {
                categoryComboBox.Items.Clear();
                categoryComboBox.Items.Add("All Categories");

                string connectionString = ConfigurationManager.DistributionConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT CategoryId, CategoryName FROM ExpenseCategories WHERE IsActive = 1 ORDER BY CategoryName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string categoryName = reader["CategoryName"].ToString();
                                int categoryId = Convert.ToInt32(reader["CategoryId"]);
                                categoryComboBox.Items.Add($"{categoryName} (ID: {categoryId})");
                            }
                        }
                    }
                }

                categoryComboBox.SelectedIndex = 0; // Select "All Categories"
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int? categoryId = GetSelectedCategoryId();

                // Validate dates
                if (startDate > endDate)
                {
                    MessageBox.Show("Start date cannot be greater than end date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get report data
                var reportData = GetExpenseSummaryReportData(startDate, endDate, categoryId);

                if (reportData == null || reportData.Count == 0)
                {
                    MessageBox.Show("No data found for the selected criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Set up report
                reportViewer.LocalReport.ReportPath = Path.Combine(Application.StartupPath, "Reports", "ExpenseSummaryReport.rdlc");
                reportViewer.LocalReport.DataSources.Clear();

                // Add data source
                ReportDataSource reportDataSource = new ReportDataSource("ExpenseSummaryDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                // Set parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("ReportTitle", "Expense Summary Report"),
                    new ReportParameter("CompanyName", "Distribution Software"),
                    new ReportParameter("ReportGeneratedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    new ReportParameter("StartDate", startDate.ToString("yyyy-MM-dd")),
                    new ReportParameter("EndDate", endDate.ToString("yyyy-MM-dd")),
                    new ReportParameter("CategoryFilter", categoryComboBox.SelectedItem?.ToString() ?? "All Categories")
                };

                reportViewer.LocalReport.SetParameters(parameters);

                // Refresh report
                reportViewer.RefreshReport();

                MessageBox.Show($"Report generated successfully! Found {reportData.Count} categories with expenses.", "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? GetSelectedCategoryId()
        {
            if (categoryComboBox.SelectedIndex == 0) // "All Categories"
                return null;

            string selectedItem = categoryComboBox.SelectedItem.ToString();
            if (selectedItem.Contains("(ID:"))
            {
                string idPart = selectedItem.Substring(selectedItem.IndexOf("(ID:") + 4);
                idPart = idPart.Replace(")", "").Trim();
                if (int.TryParse(idPart, out int categoryId))
                    return categoryId;
            }

            return null;
        }

        private List<ExpenseSummaryReportData> GetExpenseSummaryReportData(DateTime startDate, DateTime endDate, int? categoryId)
        {
            List<ExpenseSummaryReportData> reportData = new List<ExpenseSummaryReportData>();

            try
            {
                string connectionString = ConfigurationManager.DistributionConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetExpenseSummaryReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@CategoryId", categoryId ?? (object)DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ExpenseSummaryReportData data = new ExpenseSummaryReportData
                                {
                                    CategoryId = reader["CategoryId"] != DBNull.Value ? Convert.ToInt32(reader["CategoryId"]) : 0,
                                    CategoryCode = reader["CategoryCode"]?.ToString() ?? "",
                                    CategoryName = reader["CategoryName"]?.ToString() ?? "",
                                    CategoryDescription = reader["CategoryDescription"]?.ToString() ?? "",
                                    TotalExpenses = reader["TotalExpenses"] != DBNull.Value ? Convert.ToInt32(reader["TotalExpenses"]) : 0,
                                    TotalUsers = reader["TotalUsers"] != DBNull.Value ? Convert.ToInt32(reader["TotalUsers"]) : 0,
                                    TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalAmount"]) : 0,
                                    AverageAmount = reader["AverageAmount"] != DBNull.Value ? Convert.ToDecimal(reader["AverageAmount"]) : 0,
                                    MinAmount = reader["MinAmount"] != DBNull.Value ? Convert.ToDecimal(reader["MinAmount"]) : 0,
                                    MaxAmount = reader["MaxAmount"] != DBNull.Value ? Convert.ToDecimal(reader["MaxAmount"]) : 0,
                                    PendingExpenses = reader["PendingExpenses"] != DBNull.Value ? Convert.ToInt32(reader["PendingExpenses"]) : 0,
                                    ApprovedExpenses = reader["ApprovedExpenses"] != DBNull.Value ? Convert.ToInt32(reader["ApprovedExpenses"]) : 0,
                                    RejectedExpenses = reader["RejectedExpenses"] != DBNull.Value ? Convert.ToInt32(reader["RejectedExpenses"]) : 0,
                                    PaidExpenses = reader["PaidExpenses"] != DBNull.Value ? Convert.ToInt32(reader["PaidExpenses"]) : 0,
                                    CancelledExpenses = reader["CancelledExpenses"] != DBNull.Value ? Convert.ToInt32(reader["CancelledExpenses"]) : 0,
                                    PendingAmount = reader["PendingAmount"] != DBNull.Value ? Convert.ToDecimal(reader["PendingAmount"]) : 0,
                                    ApprovedAmount = reader["ApprovedAmount"] != DBNull.Value ? Convert.ToDecimal(reader["ApprovedAmount"]) : 0,
                                    RejectedAmount = reader["RejectedAmount"] != DBNull.Value ? Convert.ToDecimal(reader["RejectedAmount"]) : 0,
                                    PaidAmount = reader["PaidAmount"] != DBNull.Value ? Convert.ToDecimal(reader["PaidAmount"]) : 0,
                                    CancelledAmount = reader["CancelledAmount"] != DBNull.Value ? Convert.ToDecimal(reader["CancelledAmount"]) : 0,
                                    CashExpenses = reader["CashExpenses"] != DBNull.Value ? Convert.ToInt32(reader["CashExpenses"]) : 0,
                                    CardExpenses = reader["CardExpenses"] != DBNull.Value ? Convert.ToInt32(reader["CardExpenses"]) : 0,
                                    BankTransferExpenses = reader["BankTransferExpenses"] != DBNull.Value ? Convert.ToInt32(reader["BankTransferExpenses"]) : 0,
                                    ChequeExpenses = reader["ChequeExpenses"] != DBNull.Value ? Convert.ToInt32(reader["ChequeExpenses"]) : 0,
                                    EasypaisaExpenses = reader["EasypaisaExpenses"] != DBNull.Value ? Convert.ToInt32(reader["EasypaisaExpenses"]) : 0,
                                    JazzcashExpenses = reader["JazzcashExpenses"] != DBNull.Value ? Convert.ToInt32(reader["JazzcashExpenses"]) : 0,
                                    TotalCashAmount = reader["TotalCashAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalCashAmount"]) : 0,
                                    TotalCardAmount = reader["TotalCardAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalCardAmount"]) : 0,
                                    TotalBankTransferAmount = reader["TotalBankTransferAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalBankTransferAmount"]) : 0,
                                    TotalChequeAmount = reader["TotalChequeAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalChequeAmount"]) : 0,
                                    TotalEasypaisaAmount = reader["TotalEasypaisaAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalEasypaisaAmount"]) : 0,
                                    TotalJazzcashAmount = reader["TotalJazzcashAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalJazzcashAmount"]) : 0,
                                    HighValueExpenses = reader["HighValueExpenses"] != DBNull.Value ? Convert.ToInt32(reader["HighValueExpenses"]) : 0,
                                    MediumValueExpenses = reader["MediumValueExpenses"] != DBNull.Value ? Convert.ToInt32(reader["MediumValueExpenses"]) : 0,
                                    LowValueExpenses = reader["LowValueExpenses"] != DBNull.Value ? Convert.ToInt32(reader["LowValueExpenses"]) : 0,
                                    VeryLowValueExpenses = reader["VeryLowValueExpenses"] != DBNull.Value ? Convert.ToInt32(reader["VeryLowValueExpenses"]) : 0,
                                    TotalHighValueAmount = reader["TotalHighValueAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalHighValueAmount"]) : 0,
                                    TotalMediumValueAmount = reader["TotalMediumValueAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalMediumValueAmount"]) : 0,
                                    TotalLowValueAmount = reader["TotalLowValueAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalLowValueAmount"]) : 0,
                                    TotalVeryLowValueAmount = reader["TotalVeryLowValueAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalVeryLowValueAmount"]) : 0,
                                    PendingPercentage = reader["PendingPercentage"] != DBNull.Value ? Convert.ToDecimal(reader["PendingPercentage"]) : 0,
                                    ApprovedPercentage = reader["ApprovedPercentage"] != DBNull.Value ? Convert.ToDecimal(reader["ApprovedPercentage"]) : 0,
                                    PaidPercentage = reader["PaidPercentage"] != DBNull.Value ? Convert.ToDecimal(reader["PaidPercentage"]) : 0,
                                    RejectedPercentage = reader["RejectedPercentage"] != DBNull.Value ? Convert.ToDecimal(reader["RejectedPercentage"]) : 0
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
                saveDialog.FileName = $"ExpenseSummaryReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

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
