using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class TaxReturnExportForm : Form
    {
        private ITaxCategoryService _taxCategoryService;
        private ITaxRateService _taxRateService;
        private ISalesInvoiceService _salesInvoiceService;
        private IPurchaseInvoiceService _purchaseInvoiceService;
        private List<TaxReturnData> _taxData;

        public TaxReturnExportForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeForm();
        }

        private void InitializeServices()
        {
            var connectionString = DistributionSoftware.Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            var taxCategoryRepository = new DistributionSoftware.DataAccess.TaxCategoryRepository(connectionString);
            var taxRateRepository = new DistributionSoftware.DataAccess.TaxRateRepository(connectionString);
            
            _taxCategoryService = new TaxCategoryService(taxCategoryRepository);
            _taxRateService = new TaxRateService(taxRateRepository);
            _salesInvoiceService = new SalesInvoiceService();
            _purchaseInvoiceService = new PurchaseInvoiceService();
        }

        private void InitializeForm()
        {
            LoadTaxTypes();
            SetupExportFormats();
            SetDefaultDates();
            SetupDataGridView();
        }

        private void LoadTaxTypes()
        {
            try
            {
                var taxCategories = _taxCategoryService.GetActiveTaxCategories();
                cmbTaxType.Items.Clear();
                cmbTaxType.Items.Add("All Tax Types");
                
                foreach (var category in taxCategories)
                {
                    cmbTaxType.Items.Add(category.TaxCategoryName);
                }
                
                cmbTaxType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tax types: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupExportFormats()
        {
            cmbExportFormat.Items.Clear();
            cmbExportFormat.Items.AddRange(new[] { 
                "CSV (Comma Separated)", 
                "Excel (XLSX)", 
                "XML", 
                "JSON", 
                "PDF Report",
                "Text (Tab Delimited)" 
            });
            cmbExportFormat.SelectedIndex = 0;
        }

        private void SetDefaultDates()
        {
            // Set default to current month
            var today = DateTime.Now;
            dtpStartDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpEndDate.Value = today;
        }

        private void SetupDataGridView()
        {
            dgvPreview.AutoGenerateColumns = false;
            dgvPreview.Columns.Clear();
        }

        private void LoadTaxData()
        {
            try
            {
                _taxData = new List<TaxReturnData>();
                
                // Get sales tax data
                var salesData = GetSalesTaxData();
                _taxData.AddRange(salesData);
                
                // Get purchase tax data
                var purchaseData = GetPurchaseTaxData();
                _taxData.AddRange(purchaseData);
                
                // Filter by tax type if selected
                if (cmbTaxType.SelectedIndex > 0)
                {
                    var selectedTaxType = cmbTaxType.Text;
                    _taxData = _taxData.Where(t => t.TaxType == selectedTaxType).ToList();
                }
                
                DisplayTaxData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tax data: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<TaxReturnData> GetSalesTaxData()
        {
            var salesData = new List<TaxReturnData>();
            
            try
            {
                var salesInvoices = _salesInvoiceService.GetSalesInvoicesByDateRange(
                    dtpStartDate.Value, dtpEndDate.Value);
                
                foreach (var invoice in salesInvoices)
                {
                    if (invoice.TaxAmount > 0)
                    {
                        salesData.Add(new TaxReturnData
                        {
                            TransactionType = "Sales",
                            TransactionNumber = invoice.InvoiceNumber,
                            TransactionDate = invoice.InvoiceDate,
                            CustomerSupplierName = invoice.CustomerName ?? "Unknown",
                            TaxableAmount = invoice.TaxableAmount,
                            TaxAmount = invoice.TaxAmount,
                            TaxPercentage = invoice.TaxPercentage,
                            TaxType = GetTaxTypeName(invoice.TaxCategoryId),
                            TotalAmount = invoice.TotalAmount,
                            Status = invoice.Status
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("TaxReturnExportForm.GetSalesTaxData", ex);
            }
            
            return salesData;
        }

        private List<TaxReturnData> GetPurchaseTaxData()
        {
            var purchaseData = new List<TaxReturnData>();
            
            try
            {
                var purchaseInvoices = _purchaseInvoiceService.GetPurchaseInvoicesByDateRange(
                    dtpStartDate.Value, dtpEndDate.Value);
                
                foreach (var invoice in purchaseInvoices)
                {
                    if (invoice.TaxAmount > 0)
                    {
                        purchaseData.Add(new TaxReturnData
                        {
                            TransactionType = "Purchase",
                            TransactionNumber = invoice.InvoiceNumber,
                            TransactionDate = invoice.InvoiceDate,
                            CustomerSupplierName = invoice.SupplierName ?? "Unknown",
                            TaxableAmount = invoice.TaxableAmount,
                            TaxAmount = invoice.TaxAmount,
                            TaxPercentage = invoice.TaxPercentage,
                            TaxType = GetTaxTypeName(invoice.TaxCategoryId),
                            TotalAmount = invoice.TotalAmount,
                            Status = invoice.Status
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("TaxReturnExportForm.GetPurchaseTaxData", ex);
            }
            
            return purchaseData;
        }

        private string GetTaxTypeName(int? taxCategoryId)
        {
            if (taxCategoryId == null) return "Unknown";
            
            try
            {
                var taxCategory = _taxCategoryService.GetTaxCategoryById(taxCategoryId.Value);
                return taxCategory?.TaxCategoryName ?? "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }

        private void DisplayTaxData()
        {
            dgvPreview.DataSource = _taxData;
            lblPreview.Text = $"Tax Data Preview ({_taxData.Count} records)";
        }

        private void ExportData()
        {
            if (_taxData == null || _taxData.Count == 0)
            {
                MessageBox.Show("No tax data to export. Please preview the data first.", "No Data", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var format = cmbExportFormat.Text;
                var fileName = GenerateFileName(format);
                
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.FileName = fileName;
                    saveDialog.Filter = GetFileFilter(format);
                    
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        ExportToFile(saveDialog.FileName, format);
                        MessageBox.Show($"Tax data exported successfully to:\n{saveDialog.FileName}", "Export Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data: {ex.Message}", "Export Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateFileName(string format)
        {
            var startDate = dtpStartDate.Value.ToString("yyyyMMdd");
            var endDate = dtpEndDate.Value.ToString("yyyyMMdd");
            var taxType = cmbTaxType.SelectedIndex > 0 ? cmbTaxType.Text.Replace(" ", "") : "All";
            
            var extension = GetFileExtension(format);
            return $"TaxReturn_{taxType}_{startDate}_{endDate}{extension}";
        }

        private string GetFileExtension(string format)
        {
            switch (format)
            {
                case "CSV (Comma Separated)": return ".csv";
                case "Excel (XLSX)": return ".xlsx";
                case "XML": return ".xml";
                case "JSON": return ".json";
                case "PDF Report": return ".pdf";
                case "Text (Tab Delimited)": return ".txt";
                default: return ".csv";
            }
        }

        private string GetFileFilter(string format)
        {
            switch (format)
            {
                case "CSV (Comma Separated)": return "CSV Files (*.csv)|*.csv";
                case "Excel (XLSX)": return "Excel Files (*.xlsx)|*.xlsx";
                case "XML": return "XML Files (*.xml)|*.xml";
                case "JSON": return "JSON Files (*.json)|*.json";
                case "PDF Report": return "PDF Files (*.pdf)|*.pdf";
                case "Text (Tab Delimited)": return "Text Files (*.txt)|*.txt";
                default: return "All Files (*.*)|*.*";
            }
        }

        private void ExportToFile(string filePath, string format)
        {
            switch (format)
            {
                case "CSV (Comma Separated)":
                    ExportToCsv(filePath);
                    break;
                case "Excel (XLSX)":
                    ExportToExcel(filePath);
                    break;
                case "XML":
                    ExportToXml(filePath);
                    break;
                case "JSON":
                    ExportToJson(filePath);
                    break;
                case "PDF Report":
                    ExportToPdf(filePath);
                    break;
                case "Text (Tab Delimited)":
                    ExportToText(filePath);
                    break;
            }
        }

        private void ExportToCsv(string filePath)
        {
            var csv = new StringBuilder();
            
            // Add headers
            csv.AppendLine("Transaction Type,Transaction Number,Transaction Date,Customer/Supplier,Taxable Amount,Tax Amount,Tax %,Tax Type,Total Amount,Status");
            
            // Add data
            foreach (var item in _taxData)
            {
                csv.AppendLine($"{item.TransactionType},{item.TransactionNumber},{item.TransactionDate:yyyy-MM-dd},{item.CustomerSupplierName},{item.TaxableAmount:F2},{item.TaxAmount:F2},{item.TaxPercentage:F2},{item.TaxType},{item.TotalAmount:F2},{item.Status}");
            }
            
            File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8);
        }

        private void ExportToExcel(string filePath)
        {
            // For Excel export, we'll use CSV format with .xlsx extension
            // In a real implementation, you would use a library like EPPlus or ClosedXML
            ExportToCsv(filePath.Replace(".xlsx", ".csv"));
        }

        private void ExportToXml(string filePath)
        {
            var xml = new StringBuilder();
            xml.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            xml.AppendLine("<TaxReturnData>");
            xml.AppendLine($"  <ExportInfo>");
            xml.AppendLine($"    <StartDate>{dtpStartDate.Value:yyyy-MM-dd}</StartDate>");
            xml.AppendLine($"    <EndDate>{dtpEndDate.Value:yyyy-MM-dd}</EndDate>");
            xml.AppendLine($"    <TaxType>{cmbTaxType.Text}</TaxType>");
            xml.AppendLine($"    <ExportDate>{DateTime.Now:yyyy-MM-dd HH:mm:ss}</ExportDate>");
            xml.AppendLine($"  </ExportInfo>");
            xml.AppendLine($"  <Transactions>");
            
            foreach (var item in _taxData)
            {
                xml.AppendLine($"    <Transaction>");
                xml.AppendLine($"      <TransactionType>{item.TransactionType}</TransactionType>");
                xml.AppendLine($"      <TransactionNumber>{item.TransactionNumber}</TransactionNumber>");
                xml.AppendLine($"      <TransactionDate>{item.TransactionDate:yyyy-MM-dd}</TransactionDate>");
                xml.AppendLine($"      <CustomerSupplierName>{item.CustomerSupplierName}</CustomerSupplierName>");
                xml.AppendLine($"      <TaxableAmount>{item.TaxableAmount:F2}</TaxableAmount>");
                xml.AppendLine($"      <TaxAmount>{item.TaxAmount:F2}</TaxAmount>");
                xml.AppendLine($"      <TaxPercentage>{item.TaxPercentage:F2}</TaxPercentage>");
                xml.AppendLine($"      <TaxType>{item.TaxType}</TaxType>");
                xml.AppendLine($"      <TotalAmount>{item.TotalAmount:F2}</TotalAmount>");
                xml.AppendLine($"      <Status>{item.Status}</Status>");
                xml.AppendLine($"    </Transaction>");
            }
            
            xml.AppendLine($"  </Transactions>");
            xml.AppendLine("</TaxReturnData>");
            
            File.WriteAllText(filePath, xml.ToString(), Encoding.UTF8);
        }

        private void ExportToJson(string filePath)
        {
            var json = new StringBuilder();
            json.AppendLine("{");
            json.AppendLine($"  \"exportInfo\": {{");
            json.AppendLine($"    \"startDate\": \"{dtpStartDate.Value:yyyy-MM-dd}\",");
            json.AppendLine($"    \"endDate\": \"{dtpEndDate.Value:yyyy-MM-dd}\",");
            json.AppendLine($"    \"taxType\": \"{cmbTaxType.Text}\",");
            json.AppendLine($"    \"exportDate\": \"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\"");
            json.AppendLine($"  }},");
            json.AppendLine($"  \"transactions\": [");
            
            for (int i = 0; i < _taxData.Count; i++)
            {
                var item = _taxData[i];
                json.AppendLine($"    {{");
                json.AppendLine($"      \"transactionType\": \"{item.TransactionType}\",");
                json.AppendLine($"      \"transactionNumber\": \"{item.TransactionNumber}\",");
                json.AppendLine($"      \"transactionDate\": \"{item.TransactionDate:yyyy-MM-dd}\",");
                json.AppendLine($"      \"customerSupplierName\": \"{item.CustomerSupplierName}\",");
                json.AppendLine($"      \"taxableAmount\": {item.TaxableAmount:F2},");
                json.AppendLine($"      \"taxAmount\": {item.TaxAmount:F2},");
                json.AppendLine($"      \"taxPercentage\": {item.TaxPercentage:F2},");
                json.AppendLine($"      \"taxType\": \"{item.TaxType}\",");
                json.AppendLine($"      \"totalAmount\": {item.TotalAmount:F2},");
                json.AppendLine($"      \"status\": \"{item.Status}\"");
                json.AppendLine($"    }}{(i < _taxData.Count - 1 ? "," : "")}");
            }
            
            json.AppendLine($"  ]");
            json.AppendLine("}");
            
            File.WriteAllText(filePath, json.ToString(), Encoding.UTF8);
        }

        private void ExportToPdf(string filePath)
        {
            // For PDF export, we'll create a simple text-based report
            // In a real implementation, you would use a library like iTextSharp or PDFSharp
            var report = new StringBuilder();
            report.AppendLine("TAX RETURN REPORT");
            report.AppendLine("=================");
            report.AppendLine();
            report.AppendLine($"Export Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            report.AppendLine($"Period: {dtpStartDate.Value:yyyy-MM-dd} to {dtpEndDate.Value:yyyy-MM-dd}");
            report.AppendLine($"Tax Type: {cmbTaxType.Text}");
            report.AppendLine();
            report.AppendLine("TRANSACTION SUMMARY");
            report.AppendLine("===================");
            
            var salesTotal = _taxData.Where(t => t.TransactionType == "Sales").Sum(t => t.TaxAmount);
            var purchaseTotal = _taxData.Where(t => t.TransactionType == "Purchase").Sum(t => t.TaxAmount);
            
            report.AppendLine($"Sales Tax Collected: {salesTotal:F2}");
            report.AppendLine($"Purchase Tax Paid: {purchaseTotal:F2}");
            report.AppendLine($"Net Tax Liability: {salesTotal - purchaseTotal:F2}");
            report.AppendLine();
            report.AppendLine("DETAILED TRANSACTIONS");
            report.AppendLine("=====================");
            
            foreach (var item in _taxData)
            {
                report.AppendLine($"{item.TransactionType} | {item.TransactionNumber} | {item.TransactionDate:yyyy-MM-dd} | {item.CustomerSupplierName} | {item.TaxAmount:F2}");
            }
            
            File.WriteAllText(filePath.Replace(".pdf", ".txt"), report.ToString(), Encoding.UTF8);
        }

        private void ExportToText(string filePath)
        {
            var text = new StringBuilder();
            
            // Add headers
            text.AppendLine("Transaction Type\tTransaction Number\tTransaction Date\tCustomer/Supplier\tTaxable Amount\tTax Amount\tTax %\tTax Type\tTotal Amount\tStatus");
            
            // Add data
            foreach (var item in _taxData)
            {
                text.AppendLine($"{item.TransactionType}\t{item.TransactionNumber}\t{item.TransactionDate:yyyy-MM-dd}\t{item.CustomerSupplierName}\t{item.TaxableAmount:F2}\t{item.TaxAmount:F2}\t{item.TaxPercentage:F2}\t{item.TaxType}\t{item.TotalAmount:F2}\t{item.Status}");
            }
            
            File.WriteAllText(filePath, text.ToString(), Encoding.UTF8);
        }

        #region Event Handlers

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnPreview_Click(object sender, EventArgs e)
        {
            LoadTaxData();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            ExportData();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            dgvPreview.DataSource = null;
            lblPreview.Text = "Tax Data Preview";
            _taxData = null;
        }

        #endregion
    }

    public class TaxReturnData
    {
        public string TransactionType { get; set; }
        public string TransactionNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CustomerSupplierName { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxPercentage { get; set; }
        public string TaxType { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}

