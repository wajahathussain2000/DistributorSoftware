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
    public partial class SalesRegisterReportForm : Form
    {
        private string connectionString;
        private ReportViewer reportViewer;

        public SalesRegisterReportForm()
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
                MessageBox.Show($"Error initializing Sales Register Report Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    
                    string testQuery = "SELECT COUNT(*) FROM SalesInvoices";
                    using (SqlCommand cmd = new SqlCommand(testQuery, conn))
                    {
                        int invoiceCount = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection test failed: {ex.Message}", "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeReportViewer()
        {
            try
            {
                reportViewer = new ReportViewer();
                reportViewer.Location = new Point(0, 140);
                reportViewer.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 140);
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
                    MessageBox.Show("Could not find the RDLC report file. Please ensure SalesRegisterReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Path.Combine(Application.StartupPath, "Reports", "SalesRegisterReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "Reports", "SalesRegisterReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "Reports", "SalesRegisterReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "..", "Reports", "SalesRegisterReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "Reports", "SalesRegisterReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "..", "DistributionSoftware", "Reports", "SalesRegisterReport.rdlc")
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

        private void LoadFilters()
        {
            try
            {
                LoadCustomers();
                LoadSalesmen();
                
                // Set default date range to current month
                dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpEndDate.Value = DateTime.Now;
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
                    
                    DataRow allRow = dt.NewRow();
                    allRow["CustomerId"] = -1;
                    allRow["CustomerCode"] = "ALL";
                    allRow["CustomerName"] = "All Customers";
                    dt.Rows.InsertAt(allRow, 0);
                    
                    cmbCustomer.DataSource = dt;
                    cmbCustomer.DisplayMember = "CustomerName";
                    cmbCustomer.ValueMember = "CustomerId";
                    cmbCustomer.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSalesmen()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT SalesmanId, SalesmanCode, SalesmanName FROM Salesman WHERE IsActive = 1 ORDER BY SalesmanName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    DataRow allRow = dt.NewRow();
                    allRow["SalesmanId"] = -1;
                    allRow["SalesmanCode"] = "ALL";
                    allRow["SalesmanName"] = "All Salesmen";
                    dt.Rows.InsertAt(allRow, 0);
                    
                    cmbSalesman.DataSource = dt;
                    cmbSalesman.DisplayMember = "SalesmanName";
                    cmbSalesman.ValueMember = "SalesmanId";
                    cmbSalesman.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading salesmen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                int? customerId = cmbCustomer.SelectedValue != null && (int)cmbCustomer.SelectedValue != -1 ? (int?)cmbCustomer.SelectedValue : null;
                int? salesmanId = cmbSalesman.SelectedValue != null && (int)cmbSalesman.SelectedValue != -1 ? (int?)cmbSalesman.SelectedValue : null;
                string invoiceNumber = string.IsNullOrWhiteSpace(txtInvoiceNumber.Text) ? null : txtInvoiceNumber.Text.Trim();

                DataTable reportData = GetSalesRegisterReportData(dtpStartDate.Value, dtpEndDate.Value, customerId, salesmanId, invoiceNumber);

                if (reportData == null)
                {
                    reportData = CreateEmptyDataTable();
                }

                if (reportData.Rows.Count == 0)
                {
                    DataRow noDataRow = reportData.NewRow();
                    noDataRow["SalesInvoiceId"] = 0;
                    noDataRow["InvoiceNumber"] = "N/A";
                    noDataRow["InvoiceDate"] = DateTime.Now;
                    noDataRow["CustomerName"] = "No data found";
                    noDataRow["SalesmanName"] = "N/A";
                    noDataRow["TotalAmount"] = 0;
                    noDataRow["Status"] = "N/A";
                    noDataRow["PaymentMode"] = "N/A";
                    reportData.Rows.Add(noDataRow);
                }

                reportViewer.LocalReport.DataSources.Clear();
                ReportDataSource reportDataSource = new ReportDataSource("SalesRegisterDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("StartDate", dtpStartDate.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("EndDate", dtpEndDate.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("CustomerFilter", customerId.HasValue ? (cmbCustomer?.Text ?? "Unknown Customer") : "All Customers"),
                    new ReportParameter("SalesmanFilter", salesmanId.HasValue ? (cmbSalesman?.Text ?? "Unknown Salesman") : "All Salesmen"),
                    new ReportParameter("InvoiceNumberFilter", string.IsNullOrEmpty(invoiceNumber) ? "All" : invoiceNumber)
                };
                
                try
                {
                    reportViewer.LocalReport.SetParameters(parameters);
                }
                catch (Exception paramEx)
                {
                    // Continue without parameters if they fail
                }
                
                reportViewer.RefreshReport();
                
                string customerFilterStr = customerId.HasValue ? (cmbCustomer?.Text ?? "Unknown Customer") : "All Customers";
                string salesmanFilterStr = salesmanId.HasValue ? (cmbSalesman?.Text ?? "Unknown Salesman") : "All Salesmen";
                
                this.Text = $"Sales Register Report | {customerFilterStr} | {salesmanFilterStr} | {dtpStartDate.Value:dd/MM/yyyy} - {dtpEndDate.Value:dd/MM/yyyy}";
                
                MessageBox.Show($"Report generated successfully!\n\nFound {reportData.Rows.Count} sales invoices matching your criteria.", "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Report Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetSalesRegisterReportData(DateTime startDate, DateTime endDate, int? customerId, int? salesmanId, string invoiceNumber)
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
                    SqlCommand cmd = new SqlCommand("sp_GetSalesRegisterReport", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId.HasValue ? (object)customerId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@SalesmanId", salesmanId.HasValue ? (object)salesmanId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@InvoiceNumber", string.IsNullOrEmpty(invoiceNumber) ? (object)DBNull.Value : invoiceNumber);
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
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
            dt.Columns.Add("SalesInvoiceId", typeof(int));
            dt.Columns.Add("InvoiceNumber", typeof(string));
            dt.Columns.Add("InvoiceDate", typeof(DateTime));
            dt.Columns.Add("CustomerName", typeof(string));
            dt.Columns.Add("SalesmanName", typeof(string));
            dt.Columns.Add("TotalAmount", typeof(decimal));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("PaymentMode", typeof(string));
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
                saveDialog.FileName = "SalesRegisterReport_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType;
                    string encoding;
                    string extension;

                    byte[] reportBytes = reportViewer.LocalReport.Render(
                        saveDialog.FileName.EndsWith(".pdf") ? "PDF" : 
                        saveDialog.FileName.EndsWith(".xls") ? "Excel" : "Word",
                        null, out mimeType, out encoding, out extension, out streamIds, out warnings);

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
                int filtersPanelHeight = 140;
                reportViewer.Location = new Point(0, filtersPanelHeight);
                reportViewer.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - filtersPanelHeight);
            }
        }
    }
}
