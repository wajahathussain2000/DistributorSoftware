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
using DistributionSoftware.Models;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SupplierBalanceReportForm : Form
    {
        private string connectionString;
        private ReportViewer reportViewer;

        public SupplierBalanceReportForm()
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
                MessageBox.Show($"Error initializing Supplier Balance Report Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string testQuery = "SELECT COUNT(*) FROM Suppliers WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(testQuery, conn))
                    {
                        int supplierCount = (int)cmd.ExecuteScalar();
                        if (supplierCount == 0)
                        {
                            MessageBox.Show("No active suppliers found in database. Please ensure supplier data is available.", "No Data Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
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
                reportViewer.Location = new Point(0, 120);
                reportViewer.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 120);
                reportViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                reportViewer.ProcessingMode = ProcessingMode.Local;
                reportViewer.BackColor = Color.White;
                reportViewer.BorderStyle = BorderStyle.None;
                reportViewer.Dock = DockStyle.None;
                reportViewer.ZoomMode = ZoomMode.PageWidth;
                reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer.ShowZoomControl = true;
                reportViewer.ShowPrintButton = true;
                reportViewer.ShowExportButton = true;
                reportViewer.ShowRefreshButton = true;
                reportViewer.ShowFindControls = true;
                reportViewer.ShowPageNavigationControls = true;
                reportViewer.ShowStopButton = false;
                reportViewer.ShowProgress = true;

                string reportPath = FindReportPath();
                if (!string.IsNullOrEmpty(reportPath))
                {
                    reportViewer.LocalReport.ReportPath = reportPath;
                }
                else
                {
                    MessageBox.Show("Could not find the RDLC report file. Please ensure SupplierBalanceReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Path.Combine(Application.StartupPath, "Reports", "SupplierBalanceReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "Reports", "SupplierBalanceReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "Reports", "SupplierBalanceReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "..", "Reports", "SupplierBalanceReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "Reports", "SupplierBalanceReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "Reports", "SupplierBalanceReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Reports", "SupplierBalanceReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Reports", "SupplierBalanceReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "..", "DistributionSoftware", "Reports", "SupplierBalanceReport.rdlc")
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
                if (reportViewer == null)
                {
                    MessageBox.Show("Report viewer is not initialized. Please restart the application.", "Report Setup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (string.IsNullOrEmpty(reportViewer.LocalReport.ReportPath))
                {
                    MessageBox.Show("Report path is not set. Please ensure the RDLC file is accessible.", "Report Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (!File.Exists(reportViewer.LocalReport.ReportPath))
                {
                    MessageBox.Show($"Report file not found at: {reportViewer.LocalReport.ReportPath}\n\nPlease ensure SupplierBalanceReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
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
                var pageSettings = reportViewer.LocalReport.GetDefaultPageSettings();
            }
            catch (Exception) { /* Log error, but don't throw */ }
        }

        private void LoadFilters()
        {
            try
            {
                LoadSuppliers();
                
                // Set default date range to include 2025 data
                dtpStartDate.Value = new DateTime(2025, 1, 1);
                dtpEndDate.Value = new DateTime(2025, 12, 31);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading filter data: {ex.Message}", "Data Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT SupplierId, SupplierCode, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    // Add "All Suppliers" option at the top
                    DataRow allRow = dt.NewRow();
                    allRow["SupplierId"] = -1;
                    allRow["SupplierCode"] = "ALL";
                    allRow["SupplierName"] = "All Suppliers";
                    dt.Rows.InsertAt(allRow, 0);
                    
                    cmbSupplier.DataSource = dt;
                    cmbSupplier.DisplayMember = "SupplierName";
                    cmbSupplier.ValueMember = "SupplierId";
                    cmbSupplier.SelectedIndex = 0; // Select "All Suppliers" by default
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                int? supplierId = cmbSupplier.SelectedValue != null && (int)cmbSupplier.SelectedValue != -1 ? (int?)cmbSupplier.SelectedValue : null;
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1); // End of day

                // Get report data
                DataTable reportData = GetSupplierBalanceReportData(supplierId, startDate, endDate);
                DataTable summaryData = GetSupplierBalanceSummaryData(supplierId, startDate, endDate);

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
                    noDataRow["SupplierId"] = 0;
                    noDataRow["SupplierCode"] = "N/A";
                    noDataRow["SupplierName"] = "N/A";
                    noDataRow["ContactPerson"] = "N/A";
                    noDataRow["Phone"] = "N/A";
                    noDataRow["Email"] = "N/A";
                    noDataRow["Address"] = "N/A";
                    noDataRow["City"] = "N/A";
                    noDataRow["State"] = "N/A";
                    noDataRow["Country"] = "N/A";
                    noDataRow["GST"] = "N/A";
                    noDataRow["PaymentTermsDays"] = DBNull.Value;
                    noDataRow["OpeningBalance"] = 0;
                    noDataRow["TotalPurchases"] = 0;
                    noDataRow["TotalPayments"] = 0;
                    noDataRow["ClosingBalance"] = 0;
                    noDataRow["TransactionCount"] = 0;
                    noDataRow["LastTransactionDate"] = DBNull.Value;
                    noDataRow["NetMovement"] = 0;
                    noDataRow["DaysSinceLastTransaction"] = DBNull.Value;
                    noDataRow["BalanceStatus"] = "N/A";
                    reportData.Rows.Add(noDataRow);
                }

                if (!ValidateReportSetup()) return;

                // Clear existing data sources
                reportViewer.LocalReport.DataSources.Clear();
                
                // Add main report data source
                ReportDataSource reportDataSource = new ReportDataSource("SupplierBalanceDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);

                // Add summary data source
                ReportDataSource summaryDataSource = new ReportDataSource("SupplierBalanceSummaryDataSet", summaryData);
                reportViewer.LocalReport.DataSources.Add(summaryDataSource);

                // Set report parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("ReportTitle", "Supplier Balance Report"),
                    new ReportParameter("DateRange", $"{startDate.ToString("dd-MM-yyyy")} to {endDate.ToString("dd-MM-yyyy")}"),
                    new ReportParameter("SupplierFilter", supplierId.HasValue ? (cmbSupplier?.Text ?? "Unknown Supplier") : "All Suppliers")
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
                string supplierFilterStr = supplierId.HasValue ? (cmbSupplier?.Text ?? "Unknown Supplier") : "All Suppliers";
                
                this.Text = $"Supplier Balance Report - {startDateStr} to {endDateStr} | {supplierFilterStr}";
                
                MessageBox.Show($"Report generated successfully!\n\nFound {reportData.Rows.Count} records matching your criteria.", "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private DataTable GetSupplierBalanceReportData(int? supplierId, DateTime startDate, DateTime endDate)
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
                    // Use the stored procedure for supplier balance report
                    SqlCommand cmd = new SqlCommand("sp_GetSupplierBalanceReport", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId.HasValue ? (object)supplierId.Value : DBNull.Value);
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                
                // If no data found, add a "No data found" row
                if (dt.Rows.Count == 0)
                {
                    dt = CreateEmptyDataTable();
                    DataRow noDataRow = dt.NewRow();
                    noDataRow["SupplierId"] = 0;
                    noDataRow["SupplierCode"] = "N/A";
                    noDataRow["SupplierName"] = "N/A";
                    noDataRow["ContactPerson"] = "N/A";
                    noDataRow["Phone"] = "N/A";
                    noDataRow["Email"] = "N/A";
                    noDataRow["Address"] = "N/A";
                    noDataRow["City"] = "N/A";
                    noDataRow["State"] = "N/A";
                    noDataRow["Country"] = "N/A";
                    noDataRow["GST"] = "N/A";
                    noDataRow["PaymentTermsDays"] = DBNull.Value;
                    noDataRow["OpeningBalance"] = 0;
                    noDataRow["TotalPurchases"] = 0;
                    noDataRow["TotalPayments"] = 0;
                    noDataRow["ClosingBalance"] = 0;
                    noDataRow["TransactionCount"] = 0;
                    noDataRow["LastTransactionDate"] = DBNull.Value;
                    noDataRow["NetMovement"] = 0;
                    noDataRow["DaysSinceLastTransaction"] = DBNull.Value;
                    noDataRow["BalanceStatus"] = "N/A";
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

        private DataTable GetSupplierBalanceSummaryData(int? supplierId, DateTime startDate, DateTime endDate)
        {
            DataTable dt = new DataTable();
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_GetSupplierBalanceReport", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId.HasValue ? (object)supplierId.Value : DBNull.Value);
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds); // Fill all result sets
                    
                    if (ds.Tables.Count > 1)
                    {
                        dt = ds.Tables[1]; // Summary is the second result set
                    }
                    else
                    {
                        dt = CreateEmptySummaryDataTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving summary data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return CreateEmptySummaryDataTable();
            }

            return dt;
        }

        private DataTable CreateEmptyDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SupplierId", typeof(int));
            dt.Columns.Add("SupplierCode", typeof(string));
            dt.Columns.Add("SupplierName", typeof(string));
            dt.Columns.Add("ContactPerson", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("State", typeof(string));
            dt.Columns.Add("Country", typeof(string));
            dt.Columns.Add("GST", typeof(string));
            dt.Columns.Add("PaymentTermsDays", typeof(int));
            dt.Columns.Add("OpeningBalance", typeof(decimal));
            dt.Columns.Add("TotalPurchases", typeof(decimal));
            dt.Columns.Add("TotalPayments", typeof(decimal));
            dt.Columns.Add("ClosingBalance", typeof(decimal));
            dt.Columns.Add("TransactionCount", typeof(int));
            dt.Columns.Add("LastTransactionDate", typeof(DateTime));
            dt.Columns.Add("NetMovement", typeof(decimal));
            dt.Columns.Add("DaysSinceLastTransaction", typeof(int));
            dt.Columns.Add("BalanceStatus", typeof(string));
            return dt;
        }

        private DataTable CreateEmptySummaryDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TotalSuppliers", typeof(int));
            dt.Columns.Add("TotalOpeningBalance", typeof(decimal));
            dt.Columns.Add("TotalPurchases", typeof(decimal));
            dt.Columns.Add("TotalPayments", typeof(decimal));
            dt.Columns.Add("TotalClosingBalance", typeof(decimal));
            dt.Columns.Add("TotalTransactions", typeof(int));
            
            DataRow row = dt.NewRow();
            row["TotalSuppliers"] = 0;
            row["TotalOpeningBalance"] = 0m;
            row["TotalPurchases"] = 0m;
            row["TotalPayments"] = 0m;
            row["TotalClosingBalance"] = 0m;
            row["TotalTransactions"] = 0;
            dt.Rows.Add(row);
            
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
                saveDialog.FileName = "SupplierBalanceReport_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType;
                    string encoding;
                    string extension;

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
            if (reportViewer != null)
            {
                int filtersPanelHeight = 120;
                reportViewer.Location = new Point(0, filtersPanelHeight);
                reportViewer.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - filtersPanelHeight);
                if (reportViewer.Size.Width < 400) reportViewer.Size = new Size(400, reportViewer.Size.Height);
                if (reportViewer.Size.Height < 300) reportViewer.Size = new Size(reportViewer.Size.Width, 300);
            }
        }

        private void ImproveReportPositioning()
        {
            try
            {
                if (reportViewer != null)
                {
                    reportViewer.ZoomMode = ZoomMode.PageWidth;
                    reportViewer.RefreshReport();
                }
            }
            catch (Exception) { /* Log error, but don't throw */ }
        }

        private bool TestReportDataAvailability()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string testQuery = "SELECT COUNT(*) FROM Suppliers WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(testQuery, conn))
                    {
                        int count = (int)cmd.ExecuteScalar();
                        if (count == 0) return false;
                    }
                    
                    string simpleTestQuery = "SELECT TOP 1 SupplierName FROM Suppliers WHERE IsActive = 1";
                    using (SqlCommand cmd = new SqlCommand(simpleTestQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception) { return false; }
        }
    }
}
