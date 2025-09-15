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
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SupplierLedgerReportForm : Form
    {
        private SupplierReportRepository supplierReportRepository;

        public SupplierLedgerReportForm()
        {
            InitializeComponent();
            InitializeRepository();
            LoadInitialData();
        }

        private void InitializeRepository()
        {
            try
            {
                supplierReportRepository = new SupplierReportRepository();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing repository: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInitialData()
        {
            try
            {
                // Set default dates
                dtpStartDate.Value = DateTime.Now.AddMonths(-1);
                dtpEndDate.Value = DateTime.Now;

                // Load suppliers
                LoadSuppliers();

                // Load transaction types
                LoadTransactionTypes();

                // Set default filters
                cmbSupplierFilter.SelectedIndex = 0; // "All Suppliers"
                cmbTransactionTypeFilter.SelectedIndex = 0; // "All Types"
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                var suppliers = supplierReportRepository.GetActiveSuppliers();
                
                cmbSupplierFilter.Items.Clear();
                cmbSupplierFilter.Items.Add(new { Text = "All Suppliers", Value = (int?)null });
                
                foreach (var supplier in suppliers)
                {
                    cmbSupplierFilter.Items.Add(new { Text = $"{supplier.SupplierCode} - {supplier.SupplierName}", Value = (int?)supplier.SupplierId });
                }
                
                cmbSupplierFilter.DisplayMember = "Text";
                cmbSupplierFilter.ValueMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTransactionTypes()
        {
            try
            {
                cmbTransactionTypeFilter.Items.Clear();
                cmbTransactionTypeFilter.Items.AddRange(new string[] { "All Types", "Purchase", "Payment", "Return", "Debit Note", "Credit Note", "Adjustment" });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading transaction types: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
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
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date;
                int? supplierId = null;
                string transactionType = null;

                if (cmbSupplierFilter.SelectedItem != null)
                {
                    var selectedSupplier = cmbSupplierFilter.SelectedItem.GetType().GetProperty("Value").GetValue(cmbSupplierFilter.SelectedItem);
                    if (selectedSupplier != null)
                        supplierId = (int?)selectedSupplier;
                }

                if (cmbTransactionTypeFilter.SelectedItem != null && cmbTransactionTypeFilter.SelectedItem.ToString() != "All Types")
                {
                    transactionType = cmbTransactionTypeFilter.SelectedItem.ToString();
                }

                // Get data from repository
                var ledgerData = GetSupplierLedgerData(supplierId, startDate, endDate, transactionType);

                // Set up report data source
                ReportDataSource reportDataSource = new ReportDataSource("SupplierLedgerDataSet", ledgerData);

                // Clear existing data sources
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                // Set report parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("StartDate", startDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("EndDate", endDate.ToString("dd-MM-yyyy")),
                    new ReportParameter("SupplierFilter", supplierId.HasValue ? (cmbSupplierFilter?.Text ?? "Unknown Supplier") : "All Suppliers"),
                    new ReportParameter("TransactionTypeFilter", transactionType ?? "All Types")
                };

                reportViewer1.LocalReport.SetParameters(parameters);

                // Refresh the report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetSupplierLedgerData(int? supplierId, DateTime startDate, DateTime endDate, string transactionType)
        {
            DataTable dt = new DataTable();
            
            try
            {
                // Get data from repository
                var reportData = supplierReportRepository.GetSupplierLedgerReportData(supplierId, startDate, endDate, transactionType);

                // Create DataTable structure
                dt.Columns.Add("TransactionId", typeof(int));
                dt.Columns.Add("TransactionDate", typeof(DateTime));
                dt.Columns.Add("TransactionType", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Amount", typeof(decimal));
                dt.Columns.Add("RunningBalance", typeof(decimal));
                dt.Columns.Add("ReferenceNumber", typeof(string));
                dt.Columns.Add("DebitAmount", typeof(decimal));
                dt.Columns.Add("CreditAmount", typeof(decimal));
                dt.Columns.Add("SupplierId", typeof(int));
                dt.Columns.Add("SupplierCode", typeof(string));
                dt.Columns.Add("SupplierName", typeof(string));
                dt.Columns.Add("ContactPerson", typeof(string));
                dt.Columns.Add("Phone", typeof(string));
                dt.Columns.Add("Email", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("State", typeof(string));
                dt.Columns.Add("ReferenceDocument", typeof(string));
                dt.Columns.Add("TransactionCategory", typeof(string));
                dt.Columns.Add("DaysSinceTransaction", typeof(int));
                dt.Columns.Add("TransactionStatus", typeof(string));

                // Add data rows
                foreach (var item in reportData)
                {
                    DataRow row = dt.NewRow();
                    row["TransactionId"] = item.TransactionId;
                    row["TransactionDate"] = item.TransactionDate;
                    row["TransactionType"] = item.TransactionType;
                    row["Description"] = item.Description;
                    row["Amount"] = item.Amount;
                    row["RunningBalance"] = item.RunningBalance;
                    row["ReferenceNumber"] = item.ReferenceNumber;
                    row["DebitAmount"] = item.DebitAmount;
                    row["CreditAmount"] = item.CreditAmount;
                    row["SupplierId"] = item.SupplierId;
                    row["SupplierCode"] = item.SupplierCode;
                    row["SupplierName"] = item.SupplierName;
                    row["ContactPerson"] = item.ContactPerson;
                    row["Phone"] = item.Phone;
                    row["Email"] = item.Email;
                    row["City"] = item.City;
                    row["State"] = item.State;
                    row["ReferenceDocument"] = item.ReferenceDocument;
                    row["TransactionCategory"] = item.TransactionCategory;
                    row["DaysSinceTransaction"] = item.DaysSinceTransaction;
                    row["TransactionStatus"] = item.TransactionStatus;
                    dt.Rows.Add(row);
                }

                // If no data found, add a "No data found" row
                if (dt.Rows.Count == 0)
                {
                    DataRow noDataRow = dt.NewRow();
                    noDataRow["TransactionId"] = 0;
                    noDataRow["TransactionDate"] = DateTime.Now;
                    noDataRow["TransactionType"] = "N/A";
                    noDataRow["Description"] = "No transactions found for the selected criteria";
                    noDataRow["Amount"] = 0;
                    noDataRow["RunningBalance"] = 0;
                    noDataRow["ReferenceNumber"] = "N/A";
                    noDataRow["DebitAmount"] = 0;
                    noDataRow["CreditAmount"] = 0;
                    noDataRow["SupplierId"] = 0;
                    noDataRow["SupplierCode"] = "N/A";
                    noDataRow["SupplierName"] = "N/A";
                    noDataRow["ContactPerson"] = "N/A";
                    noDataRow["Phone"] = "N/A";
                    noDataRow["Email"] = "N/A";
                    noDataRow["City"] = "N/A";
                    noDataRow["State"] = "N/A";
                    noDataRow["ReferenceDocument"] = "N/A";
                    noDataRow["TransactionCategory"] = "N/A";
                    noDataRow["DaysSinceTransaction"] = 0;
                    noDataRow["TransactionStatus"] = "N/A";
                    dt.Rows.Add(noDataRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving supplier ledger data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return CreateEmptyDataTable();
            }

            return dt;
        }

        private DataTable CreateEmptyDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TransactionId", typeof(int));
            dt.Columns.Add("TransactionDate", typeof(DateTime));
            dt.Columns.Add("TransactionType", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Amount", typeof(decimal));
            dt.Columns.Add("RunningBalance", typeof(decimal));
            dt.Columns.Add("ReferenceNumber", typeof(string));
            dt.Columns.Add("DebitAmount", typeof(decimal));
            dt.Columns.Add("CreditAmount", typeof(decimal));
            dt.Columns.Add("SupplierId", typeof(int));
            dt.Columns.Add("SupplierCode", typeof(string));
            dt.Columns.Add("SupplierName", typeof(string));
            dt.Columns.Add("ContactPerson", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("State", typeof(string));
            dt.Columns.Add("ReferenceDocument", typeof(string));
            dt.Columns.Add("TransactionCategory", typeof(string));
            dt.Columns.Add("DaysSinceTransaction", typeof(int));
            dt.Columns.Add("TransactionStatus", typeof(string));
            return dt;
        }

        private void btnExportToPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer1.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate the report first before exporting.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveDialog.FileName = $"SupplierLedgerReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] pdfBytes = reportViewer1.LocalReport.Render("PDF");
                    File.WriteAllBytes(saveDialog.FileName, pdfBytes);
                    MessageBox.Show($"Report exported successfully to: {saveDialog.FileName}", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to PDF: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer1.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Please generate the report first before exporting.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.FileName = $"SupplierLedgerReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    byte[] excelBytes = reportViewer1.LocalReport.Render("EXCELOPENXML");
                    File.WriteAllBytes(saveDialog.FileName, excelBytes);
                    MessageBox.Show($"Report exported successfully to: {saveDialog.FileName}", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SupplierLedgerReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Set up the report viewer
                reportViewer1.LocalReport.ReportPath = Path.Combine(Application.StartupPath, "Reports", "SupplierLedgerReport.rdlc");
                reportViewer1.LocalReport.EnableExternalImages = true;
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report viewer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
