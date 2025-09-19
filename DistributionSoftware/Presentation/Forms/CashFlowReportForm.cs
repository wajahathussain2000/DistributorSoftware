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
    public partial class CashFlowReportForm : Form
    {
        private string connectionString;
        private ReportViewer reportViewer;

        public CashFlowReportForm()
        {
            InitializeComponent();
            
            try
            {
                InitializeConnection();
                InitializeReportViewer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Cash Flow Report Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Could not find the RDLC report file. Please ensure CashFlowReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Path.Combine(Application.StartupPath, "Reports", "CashFlowReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "Reports", "CashFlowReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "Reports", "CashFlowReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "..", "Reports", "CashFlowReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "Reports", "CashFlowReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "Reports", "CashFlowReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Reports", "CashFlowReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Reports", "CashFlowReport.rdlc"),
                // Try absolute path from project structure
                Path.Combine(Application.StartupPath, "..", "..", "..", "DistributionSoftware", "Reports", "CashFlowReport.rdlc")
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

        private void CashFlowReportForm_Load(object sender, EventArgs e)
        {
            // Set default dates like other reports
            DateTime today = DateTime.Today;
            dtpStartDate.Value = new DateTime(today.Year, today.Month, 1); // First day of month
            dtpEndDate.Value = today; // Today
            
            // Set form title
            this.Text = "Cash Flow Report - " + dtpStartDate.Value.ToString("dd-MM-yyyy") + " to " + dtpEndDate.Value.ToString("dd-MM-yyyy");
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
                    MessageBox.Show($"Report file not found at: {reportViewer.LocalReport.ReportPath}\n\nPlease ensure CashFlowReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Please select both start and end dates for the Cash Flow Report.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
        
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1); // End of day
        
                // Get report data
                DataTable reportData = GetCashFlowReportData(startDate, endDate);
        
                if (reportData == null || reportData.Rows.Count == 0)
                {
                    MessageBox.Show("No data found for the selected date range. Please check your data or try different dates.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
        
                // Validate report setup before proceeding
                if (!ValidateReportSetup())
                {
                    return;
                }
                
                // Clear existing data sources first
                reportViewer.LocalReport.DataSources.Clear();
        
                // Set report parameters FIRST (before adding data source)
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("StartDate", startDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("EndDate", endDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("ReportGeneratedDate", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")),
                    new ReportParameter("ReportTitle", "Cash Flow Report"),
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
        
                // Add data source AFTER setting parameters
                ReportDataSource dataSource = new ReportDataSource("CashFlowDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(dataSource);
        
                // Refresh the report
                reportViewer.RefreshReport();
        
                // Update form title
                this.Text = "Cash Flow Report - " + startDate.ToString("dd-MM-yyyy") + " to " + endDate.ToString("dd-MM-yyyy");
        
                // Show success message
                MessageBox.Show("Cash Flow report generated successfully!\n\nFound " + reportData.Rows.Count + " cash accounts from " + startDate.ToString("dd-MM-yyyy") + " to " + endDate.ToString("dd-MM-yyyy"), 
                    "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating Cash Flow report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetCashFlowReportData(DateTime startDate, DateTime endDate)
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

                    // Use the stored procedure for cash flow report
                    SqlCommand cmd = new SqlCommand("sp_GetCashFlowReport", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();
                    
                    adapter.Fill(dataSet);

                    // Use only the first result set (main cash flow data)
                    if (dataSet.Tables.Count > 0)
                    {
                        dt = dataSet.Tables[0].Copy();
                    }
                }

                // If no data found, add a "No data found" row
                if (dt.Rows.Count == 0)
                {
                    dt = CreateEmptyDataTable();
                    DataRow noDataRow = dt.NewRow();
                    noDataRow["AccountId"] = 0;
                    noDataRow["AccountCode"] = "N/A";
                    noDataRow["AccountName"] = "No cash flow data found for the selected date range";
                    noDataRow["AccountType"] = "N/A";
                    noDataRow["AccountCategory"] = "N/A";
                    noDataRow["NormalBalance"] = "N/A";
                    noDataRow["OperatingAmount"] = 0;
                    noDataRow["InvestingAmount"] = 0;
                    noDataRow["FinancingAmount"] = 0;
                    noDataRow["TotalAmount"] = 0;
                    noDataRow["AccountLevel"] = 0;
                    noDataRow["ParentAccountName"] = "N/A";
                    noDataRow["ParentAccountCode"] = "N/A";
                    noDataRow["SortOrder"] = 0;
                    dt.Rows.Add(noDataRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving Cash Flow report data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dt.Columns.Add("OperatingAmount", typeof(decimal));
            dt.Columns.Add("InvestingAmount", typeof(decimal));
            dt.Columns.Add("FinancingAmount", typeof(decimal));
            dt.Columns.Add("TotalAmount", typeof(decimal));
            dt.Columns.Add("AccountLevel", typeof(int));
            dt.Columns.Add("ParentAccountName", typeof(string));
            dt.Columns.Add("ParentAccountCode", typeof(string));
            dt.Columns.Add("SortOrder", typeof(int));
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
                saveDialog.FileName = "CashFlowReport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf";

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
            this.Text = "Cash Flow Report - " + dtpStartDate.Value.ToString("dd-MM-yyyy") + " to " + dtpEndDate.Value.ToString("dd-MM-yyyy");
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            // Update form title when end date changes
            this.Text = "Cash Flow Report - " + dtpStartDate.Value.ToString("dd-MM-yyyy") + " to " + dtpEndDate.Value.ToString("dd-MM-yyyy");
        }
    }
}
