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

namespace DistributionSoftware.Presentation.Forms
{
    public partial class ProfitLossReportForm : Form
    {
        private string connectionString;
        private ReportViewer reportViewer;

        public ProfitLossReportForm()
        {
            InitializeComponent();
            
            try
            {
                InitializeConnection();
                InitializeReportViewer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Profit & Loss Report Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeConnection()
        {
            try
            {
                // Get connection string from configuration
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Database connection string is not configured.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing database connection: " + ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeReportViewer()
        {
            try
            {
                // Create ReportViewer control
                reportViewer = new ReportViewer();
                reportViewer.ProcessingMode = ProcessingMode.Local;
                
                string reportPath = FindReportPath();
                reportViewer.LocalReport.ReportPath = reportPath;
                
                // Configure ReportViewer properties
                reportViewer.ZoomMode = ZoomMode.PageWidth;
                reportViewer.ZoomPercent = 100;
                reportViewer.ShowToolBar = true;
                reportViewer.ShowExportButton = true;
                reportViewer.ShowPrintButton = true;
                reportViewer.ShowRefreshButton = true;
                reportViewer.ShowFindControls = true;
                reportViewer.ShowPageNavigationControls = true;
                reportViewer.ShowStopButton = true;
                reportViewer.ShowProgress = true;
                reportViewer.ShowBackButton = true;
                reportViewer.ShowDocumentMapButton = true;
                reportViewer.ShowParameterPrompts = false;
                reportViewer.ShowPromptAreaButton = false;
                reportViewer.ShowZoomControl = true;
                reportViewer.ShowCredentialPrompts = false;
                
                // Position the ReportViewer
                reportViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                reportViewer.Location = new Point(12, 80);
                reportViewer.Size = new Size(1200, 600);
                reportViewer.TabIndex = 0;
                
                if (!string.IsNullOrEmpty(reportViewer.LocalReport.ReportPath))
                {
                    this.Controls.Add(reportViewer);
                }
                else
                {
                    MessageBox.Show("Could not find the RDLC report file. Please ensure ProfitLossReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing report viewer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FindReportPath()
        {
            // Try multiple possible paths for the RDLC file
            string[] possiblePaths = {
                Path.Combine(Application.StartupPath, "Reports", "ProfitLossReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "Reports", "ProfitLossReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "Reports", "ProfitLossReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "..", "Reports", "ProfitLossReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "Reports", "ProfitLossReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "Reports", "ProfitLossReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Reports", "ProfitLossReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Reports", "ProfitLossReport.rdlc"),
                // Try absolute path from project structure
                Path.Combine(Application.StartupPath, "..", "..", "..", "DistributionSoftware", "Reports", "ProfitLossReport.rdlc")
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

        private void ProfitLossReportForm_Load(object sender, EventArgs e)
        {
            // Set default dates to current month
            DateTime today = DateTime.Today;
            dtpStartDate.Value = new DateTime(today.Year, today.Month, 1); // First day of month
            dtpEndDate.Value = today; // Today
            
            // Set form title
            this.Text = "Profit & Loss Report - " + dtpStartDate.Value.ToString("dd-MM-yyyy") + " to " + dtpEndDate.Value.ToString("dd-MM-yyyy");
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
                    MessageBox.Show($"Report file not found at: {reportViewer.LocalReport.ReportPath}\n\nPlease ensure ProfitLossReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating report setup: {ex.Message}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (dtpStartDate.Value == null || dtpEndDate.Value == null)
                {
                    MessageBox.Show("Please select both start and end dates for the Profit & Loss report.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
        
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1); // End of day
        
                // Get report data
                DataTable reportData = GetProfitLossReportData(startDate, endDate);
        
                if (reportData == null || reportData.Rows.Count == 0)
                {
                    MessageBox.Show("No data found for the selected date range. Please check your data or try a different date range.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
        
                // Validate report setup before proceeding
                if (!ValidateReportSetup())
                {
                    return;
                }
                
                // Clear existing data sources first
                reportViewer.LocalReport.DataSources.Clear();
        
                // Add data source
                ReportDataSource dataSource = new ReportDataSource("ProfitLossDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(dataSource);
        
                // Set report parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("StartDate", startDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("EndDate", endDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("ReportGeneratedDate", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")),
                    new ReportParameter("ReportTitle", "Profit & Loss Statement"),
                    new ReportParameter("CompanyName", "Distribution Software")
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
        
                // Update form title
                this.Text = "Profit & Loss Report - " + startDate.ToString("dd-MM-yyyy") + " to " + endDate.ToString("dd-MM-yyyy");
        
                // Show success message
                MessageBox.Show("Profit & Loss report generated successfully!\n\nFound " + reportData.Rows.Count + " accounts for " + startDate.ToString("dd-MM-yyyy") + " to " + endDate.ToString("dd-MM-yyyy"), 
                    "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Profit & Loss report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetProfitLossReportData(DateTime startDate, DateTime endDate)
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
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sp_GetProfitLossReport", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    
                    adapter.Fill(dataSet);

                    if (dataSet.Tables.Count > 0)
                    {
                        dt = dataSet.Tables[0].Copy();
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    dt = CreateEmptyDataTable();
                    DataRow noDataRow = dt.NewRow();
                    noDataRow["AccountId"] = 0;
                    noDataRow["AccountCode"] = "N/A";
                    noDataRow["AccountName"] = "No revenue or expense data found for the selected date range";
                    noDataRow["AccountType"] = "N/A";
                    noDataRow["AccountCategory"] = "N/A";
                    noDataRow["NormalBalance"] = "N/A";
                    noDataRow["AccountLevel"] = 0;
                    noDataRow["ParentAccountName"] = "N/A";
                    noDataRow["ParentAccountCode"] = "N/A";
                    noDataRow["PeriodAmount"] = 0;
                    noDataRow["TransactionCount"] = 0;
                    noDataRow["LastTransactionDate"] = DBNull.Value;
                    noDataRow["AccountStatus"] = "N/A";
                    noDataRow["AccountDescription"] = "N/A";
                    noDataRow["SectionType"] = "N/A";
                    noDataRow["DisplayOrder"] = 0;
                    noDataRow["RevenueAmount"] = 0;
                    noDataRow["ExpenseAmount"] = 0;
                    noDataRow["DaysSinceLastTransaction"] = 0;
                    noDataRow["ActivityLevel"] = "N/A";
                    dt.Rows.Add(noDataRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving Profit & Loss Statement data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return CreateEmptyDataTable();
            }
            return dt;
        }

        private DataTable CreateEmptyDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AccountId", typeof(int));
            dt.Columns.Add("AccountCode", typeof(string));
            dt.Columns.Add("AccountName", typeof(string));
            dt.Columns.Add("AccountType", typeof(string));
            dt.Columns.Add("AccountCategory", typeof(string));
            dt.Columns.Add("NormalBalance", typeof(string));
            dt.Columns.Add("AccountLevel", typeof(int));
            dt.Columns.Add("ParentAccountName", typeof(string));
            dt.Columns.Add("ParentAccountCode", typeof(string));
            dt.Columns.Add("PeriodAmount", typeof(decimal));
            dt.Columns.Add("TransactionCount", typeof(int));
            dt.Columns.Add("LastTransactionDate", typeof(DateTime));
            dt.Columns.Add("AccountStatus", typeof(string));
            dt.Columns.Add("AccountDescription", typeof(string));
            dt.Columns.Add("SectionType", typeof(string));
            dt.Columns.Add("DisplayOrder", typeof(int));
            dt.Columns.Add("RevenueAmount", typeof(decimal));
            dt.Columns.Add("ExpenseAmount", typeof(decimal));
            dt.Columns.Add("DaysSinceLastTransaction", typeof(int));
            dt.Columns.Add("ActivityLevel", typeof(string));
            return dt;
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate the report first before exporting.", "No Report Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                saveDialog.FileName = "ProfitLossReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] reportBytes = reportViewer.LocalReport.Render("PDF");
                    System.IO.File.WriteAllBytes(saveDialog.FileName, reportBytes);
                    MessageBox.Show("Report exported successfully to: " + saveDialog.FileName, "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            // Update form title when start date changes
            this.Text = "Profit & Loss Report - " + dtpStartDate.Value.ToString("dd-MM-yyyy") + " to " + dtpEndDate.Value.ToString("dd-MM-yyyy");
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            // Update form title when end date changes
            this.Text = "Profit & Loss Report - " + dtpStartDate.Value.ToString("dd-MM-yyyy") + " to " + dtpEndDate.Value.ToString("dd-MM-yyyy");
        }
    }
}


