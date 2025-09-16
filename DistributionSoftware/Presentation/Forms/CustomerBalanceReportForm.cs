using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WinForms;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class CustomerBalanceReportForm : Form
    {
        private string connectionString;
        private ReportViewer reportViewer;

        public CustomerBalanceReportForm()
        {
            InitializeComponent();
            
            try
            {
                InitializeConnection();
                InitializeReportViewer();
                LoadFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Customer Balance Report Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeConnection()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["DistributionConnection"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Database connection string not found. Please check your configuration.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Test the connection
                TestDatabaseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing database connection: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        
        private void TestDatabaseConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Test if we have data in key tables
                    string testQuery = "SELECT COUNT(*) FROM Customers WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(testQuery, conn))
                    {
                        int customerCount = (int)cmd.ExecuteScalar();
                        
                        if (customerCount == 0)
                        {
                            MessageBox.Show("No customers found in database. Please check the database setup.", "No Data Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    
                    // Test customer ledger data
                    string ledgerQuery = "SELECT COUNT(*) FROM CustomerLedger";
                    using (SqlCommand cmd = new SqlCommand(ledgerQuery, conn))
                    {
                        int ledgerCount = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection test failed: {ex.Message}\n\nPlease ensure:\n1. SQL Server is running\n2. Database 'DistributionDB' exists\n3. Connection string is correct", "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeReportViewer()
        {
            try
            {
                reportViewer = new ReportViewer();
                reportViewer.Location = new Point(0, 120); // Position below filters panel
                reportViewer.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 120);
                reportViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                reportViewer.ProcessingMode = ProcessingMode.Local;
                
                // Improve ReportViewer appearance and alignment
                reportViewer.BackColor = Color.White;
                reportViewer.BorderStyle = BorderStyle.None;
                reportViewer.Dock = DockStyle.None; // Use manual positioning instead of dock
                reportViewer.ZoomMode = ZoomMode.PageWidth; // Auto-fit to page width
                reportViewer.SetDisplayMode(DisplayMode.PrintLayout); // Use print layout for better landscape view
                reportViewer.ShowZoomControl = true;
                reportViewer.ShowPrintButton = true;
                reportViewer.ShowExportButton = true;
                reportViewer.ShowRefreshButton = true;
                reportViewer.ShowFindControls = true;
                reportViewer.ShowPageNavigationControls = true;
                reportViewer.ShowStopButton = false;
                reportViewer.ShowProgress = true;
                
                // Set the report path
                string reportPath = FindReportPath();
                if (!string.IsNullOrEmpty(reportPath))
                {
                    reportViewer.LocalReport.ReportPath = reportPath;
                }
                else
                {
                    MessageBox.Show("Could not find the RDLC report file. Please ensure CustomerBalanceReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            // Try multiple possible paths for the RDLC file
            string[] possiblePaths = {
                Path.Combine(Application.StartupPath, "Reports", "CustomerBalanceReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "Reports", "CustomerBalanceReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "Reports", "CustomerBalanceReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "..", "Reports", "CustomerBalanceReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "Reports", "CustomerBalanceReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "Reports", "CustomerBalanceReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Reports", "CustomerBalanceReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Reports", "CustomerBalanceReport.rdlc"),
                // Try absolute path from project structure
                Path.Combine(Application.StartupPath, "..", "..", "..", "DistributionSoftware", "Reports", "CustomerBalanceReport.rdlc")
            };
            
            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }
            
            return null;
        }
        
        private bool ValidateReportSetup()
        {
            try
            {
                // Check if ReportViewer is initialized
                if (reportViewer == null)
                {
                    MessageBox.Show("Report viewer is not initialized. Please restart the application.", "Report Setup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                // Check if report path is set
                if (string.IsNullOrEmpty(reportViewer.LocalReport.ReportPath))
                {
                    MessageBox.Show("Report path is not set. Please ensure the RDLC file is accessible.", "Report Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                // Check if report file exists
                if (!File.Exists(reportViewer.LocalReport.ReportPath))
                {
                    MessageBox.Show($"Report file not found at: {reportViewer.LocalReport.ReportPath}\n\nPlease ensure CustomerBalanceReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                // Check if report is properly loaded
                if (reportViewer.LocalReport.DataSources == null)
                {
                    MessageBox.Show("Report data sources are not initialized properly.", "Report Setup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                // Ensure report is properly initialized
                EnsureReportInitialized();
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating report setup: {ex.Message}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        
        private void EnsureReportInitialized()
        {
            try
            {
                // Force the report to initialize by calling GetDefaultPageSettings
                var pageSettings = reportViewer.LocalReport.GetDefaultPageSettings();
            }
            catch (Exception ex)
            {
                // Don't throw here, just log the error
            }
        }

        private void LoadFilters()
        {
            try
            {
                // Load Customers
                LoadCustomers();
                
                // Set default date range to include 2025 data
                dtpStartDate.Value = new DateTime(2025, 1, 1);
                dtpEndDate.Value = new DateTime(2025, 12, 31);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading filter data: {ex.Message}", "Data Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT CustomerId, CustomerCode, CustomerName FROM Customers WHERE IsActive = 1 ORDER BY CustomerName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    // Add "All Customers" option at the top
                    DataRow allRow = dt.NewRow();
                    allRow["CustomerId"] = -1;
                    allRow["CustomerCode"] = "ALL";
                    allRow["CustomerName"] = "All Customers";
                    dt.Rows.InsertAt(allRow, 0);
                    
                    cmbCustomer.DataSource = dt;
                    cmbCustomer.DisplayMember = "CustomerName";
                    cmbCustomer.ValueMember = "CustomerId";
                    cmbCustomer.SelectedIndex = 0; // Select "All Customers" by default
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                
                // Test database connection and data availability first
                if (!TestReportDataAvailability())
                {
                    MessageBox.Show("Database connection or data availability issues detected. Please check the database setup.", "Data Availability Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get filter values (ignore "All" options with ID = -1)
                int? customerId = cmbCustomer.SelectedValue != null && (int)cmbCustomer.SelectedValue != -1 ? (int?)cmbCustomer.SelectedValue : null;
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1); // End of day

                // Get report data
                DataTable reportData = GetCustomerBalanceReportData(customerId, startDate, endDate);

                // Always show the report, even if no data
                if (reportData == null)
                {
                    reportData = CreateEmptyDataTable();
                }

                // Ensure we always have data structure even if empty
                if (reportData.Rows.Count == 0)
                {
                    // Add a "No data found" row
                    DataRow noDataRow = reportData.NewRow();
                    noDataRow["CustomerId"] = 0;
                    noDataRow["CustomerCode"] = "N/A";
                    noDataRow["CustomerName"] = "N/A";
                    noDataRow["ContactName"] = "N/A";
                    noDataRow["Phone"] = "N/A";
                    noDataRow["Email"] = "N/A";
                    noDataRow["Address"] = "N/A";
                    noDataRow["City"] = "N/A";
                    noDataRow["State"] = "N/A";
                    noDataRow["Country"] = "N/A";
                    noDataRow["GSTNumber"] = "N/A";
                    noDataRow["CreditLimit"] = 0;
                    noDataRow["OutstandingBalance"] = 0;
                    noDataRow["PaymentTerms"] = "N/A";
                    noDataRow["CurrentBalance"] = 0;
                    noDataRow["PeriodSales"] = 0;
                    noDataRow["PeriodPayments"] = 0;
                    noDataRow["PeriodReturns"] = 0;
                    noDataRow["PeriodTransactionCount"] = 0;
                    noDataRow["TotalTransactionCount"] = 0;
                    noDataRow["OpeningBalance"] = 0;
                    noDataRow["ClosingBalance"] = 0;
                    noDataRow["CreditUtilizationPercent"] = 0;
                    noDataRow["BalanceStatus"] = "N/A";
                    noDataRow["CreditStatus"] = "N/A";
                    noDataRow["NetPeriodActivity"] = 0;
                    noDataRow["LastTransactionDate"] = DBNull.Value;
                    noDataRow["FirstTransactionDate"] = DBNull.Value;
                    reportData.Rows.Add(noDataRow);
                }

                // Validate report setup before proceeding
                if (!ValidateReportSetup())
                {
                    return;
                }
                
                // Clear existing data sources first
                reportViewer.LocalReport.DataSources.Clear();
                
                // Set up report data source first
                ReportDataSource reportDataSource = new ReportDataSource("CustomerBalanceDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                
                // Set report parameters AFTER data source is added
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("StartDate", startDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("EndDate", endDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("CustomerFilter", customerId.HasValue ? (cmbCustomer?.Text ?? "Unknown Customer") : "All Customers")
                };
                
                try
                {
                    reportViewer.LocalReport.SetParameters(parameters);
                }
                catch (Exception paramEx)
                {
                    // Continue without parameters if they fail
                }
                
                // Refresh the report
                reportViewer.RefreshReport();
                
                // Improve report positioning after refresh
                ImproveReportPositioning();
                
                // Update form title with filter information
                string startDateStr = startDate.ToString("dd-MM-yyyy");
                string endDateStr = endDate.ToString("dd-MM-yyyy");
                string customerFilterStr = customerId.HasValue ? (cmbCustomer?.Text ?? "Unknown Customer") : "All Customers";
                
                this.Text = $"Customer Balance Report - {startDateStr} to {endDateStr} | {customerFilterStr}";
                
                MessageBox.Show($"Report generated successfully!\n\nFound {reportData.Rows.Count} customers matching your criteria.", "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error generating report: {ex.Message}\n\n" +
                                   $"Technical Details:\n" +
                                   $"- Exception Type: {ex.GetType().Name}\n" +
                                   $"- Stack Trace: {ex.StackTrace}\n\n" +
                                   $"Please check:\n" +
                                   $"1. Database connection\n" +
                                   $"2. Data availability\n" +
                                   $"3. Filter selections\n" +
                                   $"4. RDLC file accessibility\n\n" +
                                   $"Contact administrator if problem persists.";
                
                MessageBox.Show(errorMessage, "Report Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetCustomerBalanceReportData(int? customerId, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            
            try
            {
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Database connection is not available.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return CreateEmptyDataTable();
                }
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Use the stored procedure for customer balance report
                    SqlCommand cmd = new SqlCommand("sp_GetCustomerBalanceReport", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId.HasValue ? (object)customerId.Value : DBNull.Value);
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                
                // If no data found, add a "No data found" row
                if (dt.Rows.Count == 0)
                {
                    dt = CreateEmptyDataTable();
                    DataRow noDataRow = dt.NewRow();
                    noDataRow["CustomerId"] = 0;
                    noDataRow["CustomerCode"] = "N/A";
                    noDataRow["CustomerName"] = "N/A";
                    noDataRow["ContactName"] = "N/A";
                    noDataRow["Phone"] = "N/A";
                    noDataRow["Email"] = "N/A";
                    noDataRow["Address"] = "N/A";
                    noDataRow["City"] = "N/A";
                    noDataRow["State"] = "N/A";
                    noDataRow["Country"] = "N/A";
                    noDataRow["GSTNumber"] = "N/A";
                    noDataRow["CreditLimit"] = 0;
                    noDataRow["OutstandingBalance"] = 0;
                    noDataRow["PaymentTerms"] = "N/A";
                    noDataRow["CurrentBalance"] = 0;
                    noDataRow["PeriodSales"] = 0;
                    noDataRow["PeriodPayments"] = 0;
                    noDataRow["PeriodReturns"] = 0;
                    noDataRow["PeriodTransactionCount"] = 0;
                    noDataRow["TotalTransactionCount"] = 0;
                    noDataRow["OpeningBalance"] = 0;
                    noDataRow["ClosingBalance"] = 0;
                    noDataRow["CreditUtilizationPercent"] = 0;
                    noDataRow["BalanceStatus"] = "N/A";
                    noDataRow["CreditStatus"] = "N/A";
                    noDataRow["NetPeriodActivity"] = 0;
                    noDataRow["LastTransactionDate"] = DBNull.Value;
                    noDataRow["FirstTransactionDate"] = DBNull.Value;
                    dt.Rows.Add(noDataRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving report data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return CreateEmptyDataTable();
            }

            return dt;
        }
        
        private DataTable CreateEmptyDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerId", typeof(int));
            dt.Columns.Add("CustomerCode", typeof(string));
            dt.Columns.Add("CustomerName", typeof(string));
            dt.Columns.Add("ContactName", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("State", typeof(string));
            dt.Columns.Add("Country", typeof(string));
            dt.Columns.Add("GSTNumber", typeof(string));
            dt.Columns.Add("CreditLimit", typeof(decimal));
            dt.Columns.Add("OutstandingBalance", typeof(decimal));
            dt.Columns.Add("PaymentTerms", typeof(string));
            dt.Columns.Add("CurrentBalance", typeof(decimal));
            dt.Columns.Add("PeriodSales", typeof(decimal));
            dt.Columns.Add("PeriodPayments", typeof(decimal));
            dt.Columns.Add("PeriodReturns", typeof(decimal));
            dt.Columns.Add("PeriodTransactionCount", typeof(int));
            dt.Columns.Add("TotalTransactionCount", typeof(int));
            dt.Columns.Add("OpeningBalance", typeof(decimal));
            dt.Columns.Add("ClosingBalance", typeof(decimal));
            dt.Columns.Add("CreditUtilizationPercent", typeof(decimal));
            dt.Columns.Add("BalanceStatus", typeof(string));
            dt.Columns.Add("CreditStatus", typeof(string));
            dt.Columns.Add("NetPeriodActivity", typeof(decimal));
            dt.Columns.Add("LastTransactionDate", typeof(DateTime));
            dt.Columns.Add("FirstTransactionDate", typeof(DateTime));
            return dt;
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer == null || reportViewer.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate a report first before exporting.", "No Report Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF files (*.pdf)|*.pdf|Excel files (*.xls)|*.xls|Word files (*.doc)|*.doc";
                saveDialog.FileName = "CustomerBalanceReport_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Export using ReportViewer's built-in export functionality
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType;
                    string encoding;
                    string extension;

                    // Set device info for landscape orientation
                    string deviceInfo = @"<DeviceInfo>
                        <OutputFormat>PDF</OutputFormat>
                        <PageWidth>14in</PageWidth>
                        <PageHeight>8.5in</PageHeight>
                        <MarginTop>0.5in</MarginTop>
                        <MarginLeft>0.5in</MarginLeft>
                        <MarginRight>0.5in</MarginRight>
                        <MarginBottom>0.5in</MarginBottom>
                    </DeviceInfo>";

                    byte[] reportBytes = reportViewer.LocalReport.Render(
                        saveDialog.FileName.EndsWith(".pdf") ? "PDF" : 
                        saveDialog.FileName.EndsWith(".xls") ? "Excel" : "Word",
                        deviceInfo, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    System.IO.File.WriteAllBytes(saveDialog.FileName, reportBytes);
                    MessageBox.Show("Report exported successfully to " + saveDialog.FileName, "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting report: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            // Adjust ReportViewer size and position when form is resized
            if (reportViewer != null)
            {
                // Ensure ReportViewer stays below the filters panel (120px height)
                int filtersPanelHeight = 120;
                reportViewer.Location = new Point(0, filtersPanelHeight);
                reportViewer.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - filtersPanelHeight);
                
                // Ensure minimum size
                if (reportViewer.Size.Width < 400)
                    reportViewer.Size = new Size(400, reportViewer.Size.Height);
                if (reportViewer.Size.Height < 300)
                    reportViewer.Size = new Size(reportViewer.Size.Width, 300);
            }
        }
        
        private void ImproveReportPositioning()
        {
            try
            {
                if (reportViewer != null)
                {
                    // Set optimal zoom mode for better alignment
                    reportViewer.ZoomMode = ZoomMode.PageWidth;
                    
                    // Ensure the report is properly centered
                    reportViewer.RefreshReport();
                }
            }
            catch (Exception ex)
            {
            }
        }
        
        private bool TestReportDataAvailability()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    
                    // Test if Customers table exists and has data
                    string testQuery = "SELECT COUNT(*) FROM Customers WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(testQuery, conn))
                    {
                        int count = (int)cmd.ExecuteScalar();
                        
                        if (count == 0)
                        {
                            return false;
                        }
                    }
                    
                    // Test if we can execute a simple query
                    string simpleTestQuery = "SELECT TOP 1 CustomerCode, CustomerName FROM Customers WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(simpleTestQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return true;
                            }
                        }
                    }
                }
                
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
