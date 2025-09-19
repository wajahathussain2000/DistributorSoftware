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
    public partial class ExpenseReportForm : Form
    {
        private ReportViewer reportViewer;
        private ComboBox cmbCategory;
        private ComboBox cmbUser;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnGenerateReport;
        private Button btnExportPDF;
        private Button btnClose;
        private string connectionString;

        public ExpenseReportForm()
        {
            InitializeComponent();
            InitializeFormControls();
            InitializeReportViewer();
            LoadCategories();
            LoadUsers();
            SetDefaultDates();
        }

        private void InitializeFormControls()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Expense Report";
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

            // Category filter
            Label lblCategory = new Label();
            lblCategory.Text = "Category:";
            lblCategory.Location = new Point(10, 15);
            lblCategory.Size = new Size(80, 20);
            lblCategory.Font = new Font("Arial", 9, FontStyle.Bold);

            cmbCategory = new ComboBox();
            cmbCategory.Location = new Point(95, 12);
            cmbCategory.Size = new Size(200, 25);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Font = new Font("Arial", 9);

            // User filter
            Label lblUser = new Label();
            lblUser.Text = "User:";
            lblUser.Location = new Point(310, 15);
            lblUser.Size = new Size(50, 20);
            lblUser.Font = new Font("Arial", 9, FontStyle.Bold);

            cmbUser = new ComboBox();
            cmbUser.Location = new Point(365, 12);
            cmbUser.Size = new Size(200, 25);
            cmbUser.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUser.Font = new Font("Arial", 9);

            // Start Date filter
            Label lblStartDate = new Label();
            lblStartDate.Text = "Start Date:";
            lblStartDate.Location = new Point(10, 50);
            lblStartDate.Size = new Size(80, 20);
            lblStartDate.Font = new Font("Arial", 9, FontStyle.Bold);

            dtpStartDate = new DateTimePicker();
            dtpStartDate.Location = new Point(95, 47);
            dtpStartDate.Size = new Size(150, 25);
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Font = new Font("Arial", 9);

            // End Date filter
            Label lblEndDate = new Label();
            lblEndDate.Text = "End Date:";
            lblEndDate.Location = new Point(260, 50);
            lblEndDate.Size = new Size(70, 20);
            lblEndDate.Font = new Font("Arial", 9, FontStyle.Bold);

            dtpEndDate = new DateTimePicker();
            dtpEndDate.Location = new Point(335, 47);
            dtpEndDate.Size = new Size(150, 25);
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Font = new Font("Arial", 9);

            // Buttons
            btnGenerateReport = new Button();
            btnGenerateReport.Text = "Generate Report";
            btnGenerateReport.Location = new Point(500, 45);
            btnGenerateReport.Size = new Size(120, 30);
            btnGenerateReport.BackColor = Color.FromArgb(0, 120, 215);
            btnGenerateReport.ForeColor = Color.White;
            btnGenerateReport.FlatStyle = FlatStyle.Flat;
            btnGenerateReport.Font = new Font("Arial", 9, FontStyle.Bold);
            btnGenerateReport.Click += BtnGenerateReport_Click;

            btnExportPDF = new Button();
            btnExportPDF.Text = "Export PDF";
            btnExportPDF.Location = new Point(630, 45);
            btnExportPDF.Size = new Size(100, 30);
            btnExportPDF.BackColor = Color.FromArgb(220, 53, 69);
            btnExportPDF.ForeColor = Color.White;
            btnExportPDF.FlatStyle = FlatStyle.Flat;
            btnExportPDF.Font = new Font("Arial", 9, FontStyle.Bold);
            btnExportPDF.Click += BtnExportPDF_Click;

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new Point(740, 45);
            btnClose.Size = new Size(80, 30);
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Arial", 9, FontStyle.Bold);
            btnClose.Click += BtnClose_Click;

            // Add controls to filter panel
            filterPanel.Controls.Add(lblCategory);
            filterPanel.Controls.Add(cmbCategory);
            filterPanel.Controls.Add(lblUser);
            filterPanel.Controls.Add(cmbUser);
            filterPanel.Controls.Add(lblStartDate);
            filterPanel.Controls.Add(dtpStartDate);
            filterPanel.Controls.Add(lblEndDate);
            filterPanel.Controls.Add(dtpEndDate);
            filterPanel.Controls.Add(btnGenerateReport);
            filterPanel.Controls.Add(btnExportPDF);
            filterPanel.Controls.Add(btnClose);

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
                
                // Improve ReportViewer appearance and alignment
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
                
                // Set the report path
                string reportPath = FindReportPath();
                if (!string.IsNullOrEmpty(reportPath))
                {
                    reportViewer.LocalReport.ReportPath = reportPath;
                }
                else
                {
                    MessageBox.Show("Could not find the RDLC report file. Please ensure ExpenseReport.rdlc exists in the Reports folder.", "Report File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Path.Combine(Application.StartupPath, "Reports", "ExpenseReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "Reports", "ExpenseReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "Reports", "ExpenseReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "..", "Reports", "ExpenseReport.rdlc"),
                Path.Combine(Directory.GetCurrentDirectory(), "Reports", "ExpenseReport.rdlc"),
                Path.Combine(Application.StartupPath, "..", "..", "..", "DistributionSoftware", "Reports", "ExpenseReport.rdlc")
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

        private void LoadCategories()
        {
            try
            {
                connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DistributionConnection"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
                }

                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Database connection string not found. Please check your configuration.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT CategoryId, CategoryCode, CategoryName FROM ExpenseCategories WHERE IsActive = 1 ORDER BY CategoryName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    DataRow allRow = dt.NewRow();
                    allRow["CategoryId"] = -1;
                    allRow["CategoryCode"] = "ALL";
                    allRow["CategoryName"] = "All Categories";
                    dt.Rows.InsertAt(allRow, 0);
                    
                    cmbCategory.DataSource = dt;
                    cmbCategory.DisplayMember = "CategoryName";
                    cmbCategory.ValueMember = "CategoryId";
                    cmbCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT UserId, FirstName + ' ' + LastName AS UserName FROM Users WHERE IsActive = 1 ORDER BY FirstName, LastName";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    DataRow allRow = dt.NewRow();
                    allRow["UserId"] = -1;
                    allRow["UserName"] = "All Users";
                    dt.Rows.InsertAt(allRow, 0);
                    
                    cmbUser.DataSource = dt;
                    cmbUser.DisplayMember = "UserName";
                    cmbUser.ValueMember = "UserId";
                    cmbUser.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultDates()
        {
            // Set default date range to current month
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = DateTime.Now;
        }

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer == null)
                {
                    MessageBox.Show("Report viewer is not initialized properly.", "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int? categoryId = cmbCategory.SelectedValue != null && (int)cmbCategory.SelectedValue != -1 ? (int?)cmbCategory.SelectedValue : null;
                int? userId = cmbUser.SelectedValue != null && (int)cmbUser.SelectedValue != -1 ? (int?)cmbUser.SelectedValue : null;

                DataTable reportData = GetExpenseReportData(dtpStartDate.Value, dtpEndDate.Value, categoryId, userId);

                if (reportData == null)
                {
                    reportData = CreateEmptyDataTable();
                }

                if (reportData.Rows.Count == 0)
                {
                    DataRow noDataRow = reportData.NewRow();
                    noDataRow["ExpenseId"] = 0;
                    noDataRow["ExpenseNumber"] = "N/A";
                    noDataRow["ExpenseDate"] = DateTime.Now;
                    noDataRow["Description"] = "No data found";
                    noDataRow["Amount"] = 0;
                    noDataRow["CategoryName"] = "N/A";
                    noDataRow["UserName"] = "N/A";
                    noDataRow["PaymentMethodDescription"] = "N/A";
                    noDataRow["StatusDescription"] = "N/A";
                    noDataRow["VendorName"] = "N/A";
                    reportData.Rows.Add(noDataRow);
                }

                reportViewer.LocalReport.DataSources.Clear();
                ReportDataSource reportDataSource = new ReportDataSource("ExpenseDataSet", reportData);
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("StartDate", dtpStartDate.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("EndDate", dtpEndDate.Value.ToString("dd/MM/yyyy")),
                    new ReportParameter("CategoryFilter", categoryId.HasValue ? (cmbCategory?.Text ?? "Unknown Category") : "All Categories"),
                    new ReportParameter("UserFilter", userId.HasValue ? (cmbUser?.Text ?? "Unknown User") : "All Users"),
                    new ReportParameter("ReportGeneratedDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm")),
                    new ReportParameter("ReportTitle", "Expense Report"),
                    new ReportParameter("CompanyName", "Distribution Company")
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
                
                string categoryFilterStr = categoryId.HasValue ? (cmbCategory?.Text ?? "Unknown Category") : "All Categories";
                string userFilterStr = userId.HasValue ? (cmbUser?.Text ?? "Unknown User") : "All Users";
                
                this.Text = $"Expense Report | {categoryFilterStr} | {userFilterStr} | {dtpStartDate.Value:dd/MM/yyyy} - {dtpEndDate.Value:dd/MM/yyyy}";
                
                MessageBox.Show($"Report generated successfully!\n\nFound {reportData.Rows.Count} expenses matching your criteria.", "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Report Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetExpenseReportData(DateTime startDate, DateTime endDate, int? categoryId, int? userId)
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
                    SqlCommand cmd = new SqlCommand("sp_GetExpenseReport", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId.HasValue ? (object)categoryId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserId", userId.HasValue ? (object)userId.Value : DBNull.Value);
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving expense data: {ex.Message}", "Data Retrieval Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return CreateEmptyDataTable();
            }
            
            return dt;
        }

        private DataTable CreateEmptyDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ExpenseId", typeof(int));
            dt.Columns.Add("ExpenseNumber", typeof(string));
            dt.Columns.Add("ExpenseDate", typeof(DateTime));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("CategoryName", typeof(string));
            dt.Columns.Add("UserName", typeof(string));
            dt.Columns.Add("PaymentMethodDescription", typeof(string));
            dt.Columns.Add("StatusDescription", typeof(string));
            dt.Columns.Add("VendorName", typeof(string));
            dt.Columns.Add("ReferenceNumber", typeof(string));
            dt.Columns.Add("ReceiptNumber", typeof(string));
            dt.Columns.Add("ApprovedByName", typeof(string));
            dt.Columns.Add("ApprovedDate", typeof(DateTime));
            dt.Columns.Add("Remarks", typeof(string));
            dt.Columns.Add("AmountCategory", typeof(string));
            dt.Columns.Add("DaysSinceExpense", typeof(int));
            dt.Columns.Add("ExpenseMonthName", typeof(string));
            return dt;
        }

        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate the report first before exporting.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveDialog.FileName = $"ExpenseReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                saveDialog.Title = "Export Expense Report to PDF";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] reportBytes = reportViewer.LocalReport.Render("PDF");
                    File.WriteAllBytes(saveDialog.FileName, reportBytes);
                    MessageBox.Show($"Report exported successfully to:\n{saveDialog.FileName}", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExpenseReportForm_Load(object sender, EventArgs e)
        {
            // Set form title with current date range
            this.Text = $"Expense Report - {dtpStartDate.Value:dd/MM/yyyy} to {dtpEndDate.Value:dd/MM/yyyy}";
        }
    }
}
