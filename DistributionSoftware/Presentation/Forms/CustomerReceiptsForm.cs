using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class CustomerReceiptsForm : Form
    {
        #region Private Fields
        
        private ICustomerReceiptService _customerReceiptService;
        private ICustomerRepository _customerRepository;
        private ISalesInvoiceRepository _salesInvoiceRepository;
        private CustomerReceipt _currentReceipt;
        private bool _isEditMode;
        private int _currentReceiptId;
        
        #endregion
        
        #region Constructor
        
        public CustomerReceiptsForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeForm();
            LoadData();
            SetDefaultValues();
            UpdateButtonState();
        }
        
        #endregion
        
        #region Initialization Methods
        
        private void InitializeServices()
        {
            try
            {
                _customerReceiptService = new CustomerReceiptService();
                _customerRepository = new CustomerRepository();
                _salesInvoiceRepository = new SalesInvoiceRepository();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing services: {ex.Message}", "Initialization Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void InitializeForm()
        {
            // Set form properties
            this.Text = "Customer Receipts Management";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1200, 800);
            this.MinimumSize = new Size(1000, 600);
            
            // Initialize current receipt
            _currentReceipt = new CustomerReceipt();
            _isEditMode = false;
            _currentReceiptId = 0;
        }
        
        private void LoadData()
        {
            LoadCustomers();
            LoadPaymentMethods();
            LoadReceiptsGrid();
        }
        
        private void SetDefaultValues()
        {
            // Set default receipt date
            dtpReceiptDate.Value = DateTime.Now;
            
            // Set default payment method
            cmbPaymentMethod.SelectedIndex = 0;
            
            // Generate receipt number
            GenerateReceiptNumber();
            
            // Set current user
            txtReceivedBy.Text = UserSession.GetDisplayName();
        }
        
        #endregion
        
        #region Data Loading Methods
        
        private void LoadCustomers()
        {
            try
            {
                var customers = _customerRepository.GetActiveCustomers();
                
                cmbCustomer.Items.Clear();
                cmbCustomer.Items.Add(new { Id = 0, Name = "Select Customer", Phone = "", Address = "" });
                
                foreach (var customer in customers)
                {
                    cmbCustomer.Items.Add(new { 
                        Id = customer.CustomerId, 
                        Name = customer.CustomerName, 
                        Phone = customer.Phone ?? customer.Mobile ?? "", 
                        Address = customer.Address ?? "" 
                    });
                }
                
                cmbCustomer.DisplayMember = "Name";
                cmbCustomer.ValueMember = "Id";
                cmbCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadPaymentMethods()
        {
            try
            {
                cmbPaymentMethod.Items.Clear();
                cmbPaymentMethod.Items.AddRange(new string[] {
                    "CASH",
                    "BANK_TRANSFER", 
                    "CHEQUE",
                    "CARD",
                    "EASYPAISA",
                    "JAZZCASH",
                    "OTHER"
                });
                
                cmbPaymentMethod.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payment methods: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadReceiptsGrid()
        {
            try
            {
                var receipts = _customerReceiptService.GetAllCustomerReceipts();
                
                dgvReceipts.DataSource = receipts.Select(r => new {
                    ReceiptId = r.ReceiptId,
                    ReceiptNumber = r.ReceiptNumber,
                    ReceiptDate = r.ReceiptDate.ToString("dd/MM/yyyy"),
                    CustomerName = r.CustomerName,
                    Amount = r.Amount.ToString("N2"),
                    PaymentMethod = r.PaymentMethod,
                    InvoiceReference = r.InvoiceReference ?? "",
                    Description = r.Description ?? "",
                    ReceivedBy = r.ReceivedBy,
                    Status = r.Status
                }).ToList();
                
                // Configure grid columns
                if (dgvReceipts.Columns.Count > 0)
                {
                    dgvReceipts.Columns["ReceiptId"].Visible = false;
                    dgvReceipts.Columns["ReceiptNumber"].HeaderText = "Receipt No.";
                    dgvReceipts.Columns["ReceiptDate"].HeaderText = "Date";
                    dgvReceipts.Columns["CustomerName"].HeaderText = "Customer";
                    dgvReceipts.Columns["Amount"].HeaderText = "Amount";
                    dgvReceipts.Columns["PaymentMethod"].HeaderText = "Payment Method";
                    dgvReceipts.Columns["InvoiceReference"].HeaderText = "Invoice Ref.";
                    dgvReceipts.Columns["Description"].HeaderText = "Description";
                    dgvReceipts.Columns["ReceivedBy"].HeaderText = "Received By";
                    dgvReceipts.Columns["Status"].HeaderText = "Status";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading receipts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
        
        #region Event Handlers
        
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;
                
            try
            {
                PopulateReceiptFromForm();
                
                bool success = false;
                if (_isEditMode)
                {
                    success = _customerReceiptService.UpdateCustomerReceipt(_currentReceipt);
                    if (success)
                        MessageBox.Show("Receipt updated successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int receiptId = _customerReceiptService.CreateCustomerReceipt(_currentReceipt);
                    if (receiptId > 0)
                    {
                        _currentReceiptId = receiptId;
                        _currentReceipt.ReceiptId = receiptId;
                        success = true; // Set success to true when receipt is created successfully
                        MessageBox.Show($"Receipt created successfully! Receipt ID: {receiptId}", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to create receipt.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                
                if (success || _currentReceiptId > 0)
                {
                    LoadReceiptsGrid();
                    ClearForm();
                    UpdateButtonState();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving receipt: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            UpdateButtonState();
        }
        
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_currentReceiptId <= 0)
            {
                MessageBox.Show("Please select a receipt to delete.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var result = MessageBox.Show("Are you sure you want to delete this receipt?", "Confirm Delete", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = _customerReceiptService.DeleteCustomerReceipt(_currentReceiptId);
                    if (success)
                    {
                        MessageBox.Show("Receipt deleted successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReceiptsGrid();
                        ClearForm();
                        UpdateButtonState();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete receipt.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting receipt: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (_currentReceiptId <= 0)
            {
                MessageBox.Show("Please select a receipt to print.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                var receipt = _customerReceiptService.GetCustomerReceiptById(_currentReceiptId);
                if (receipt != null)
                {
                    string receiptText = GenerateReceiptText(receipt);
                    
                    // Show receipt in a print preview dialog
                    var printForm = new Form
                    {
                        Text = $"Receipt - {receipt.ReceiptNumber}",
                        Size = new Size(500, 700),
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    
                    var textBox = new TextBox
                    {
                        Text = receiptText,
                        Multiline = true,
                        ReadOnly = true,
                        Font = new Font("Courier New", 10),
                        Dock = DockStyle.Fill,
                        ScrollBars = ScrollBars.Vertical
                    };
                    
                    printForm.Controls.Add(textBox);
                    printForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing receipt: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void CmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedItem != null)
            {
                var selectedCustomer = cmbCustomer.SelectedItem;
                var customerId = selectedCustomer.GetType().GetProperty("Id")?.GetValue(selectedCustomer);
                
                if (customerId != null && Convert.ToInt32(customerId) > 0)
                {
                    LoadCustomerInvoices(Convert.ToInt32(customerId));
                }
                else
                {
                    cmbInvoiceReference.Items.Clear();
                    cmbInvoiceReference.Items.Add("Select Invoice");
                    cmbInvoiceReference.SelectedIndex = 0;
                }
            }
        }
        
        private void DgvReceipts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceipts.SelectedRows.Count > 0)
            {
                var selectedRow = dgvReceipts.SelectedRows[0];
                if (selectedRow.Cells["ReceiptId"].Value != null)
                {
                    _currentReceiptId = Convert.ToInt32(selectedRow.Cells["ReceiptId"].Value);
                    LoadReceiptForEdit(_currentReceiptId);
                }
            }
        }
        
        #endregion
        
        #region Helper Methods
        
        private bool ValidateForm()
        {
            if (cmbCustomer.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCustomer.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtReceivedBy.Text))
            {
                MessageBox.Show("Please enter who received the payment.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReceivedBy.Focus();
                return false;
            }
            
            return true;
        }
        
        private void PopulateReceiptFromForm()
        {
            var selectedCustomer = cmbCustomer.SelectedItem;
            var customerId = selectedCustomer.GetType().GetProperty("Id")?.GetValue(selectedCustomer);
            var customerName = selectedCustomer.GetType().GetProperty("Name")?.GetValue(selectedCustomer);
            
            _currentReceipt.ReceiptId = _currentReceiptId;
            _currentReceipt.ReceiptNumber = txtReceiptNumber.Text;
            _currentReceipt.ReceiptDate = dtpReceiptDate.Value;
            _currentReceipt.CustomerId = Convert.ToInt32(customerId);
            _currentReceipt.CustomerName = customerName?.ToString();
            _currentReceipt.Amount = decimal.Parse(txtAmount.Text);
            _currentReceipt.PaymentMethod = cmbPaymentMethod.Text;
            _currentReceipt.InvoiceReference = cmbInvoiceReference.Text != "Select Invoice" ? cmbInvoiceReference.Text : null;
            _currentReceipt.Description = txtDescription.Text;
            _currentReceipt.ReceivedBy = txtReceivedBy.Text;
            _currentReceipt.Status = "COMPLETED";
            _currentReceipt.CreatedBy = UserSession.CurrentUserId;
            _currentReceipt.CreatedDate = DateTime.Now;
        }
        
        private void LoadReceiptForEdit(int receiptId)
        {
            try
            {
                var receipt = _customerReceiptService.GetCustomerReceiptById(receiptId);
                if (receipt != null)
                {
                    _currentReceipt = receipt;
                    _isEditMode = true;
                    
                    // Populate form fields
                    txtReceiptNumber.Text = receipt.ReceiptNumber;
                    dtpReceiptDate.Value = receipt.ReceiptDate;
                    
                    // Set customer
                    for (int i = 0; i < cmbCustomer.Items.Count; i++)
                    {
                        var item = cmbCustomer.Items[i];
                        var id = item.GetType().GetProperty("Id")?.GetValue(item);
                        if (id != null && Convert.ToInt32(id) == receipt.CustomerId)
                        {
                            cmbCustomer.SelectedIndex = i;
                            break;
                        }
                    }
                    
                    txtAmount.Text = receipt.Amount.ToString("N2");
                    cmbPaymentMethod.Text = receipt.PaymentMethod;
                    cmbInvoiceReference.Text = receipt.InvoiceReference ?? "Select Invoice";
                    txtDescription.Text = receipt.Description ?? "";
                    txtReceivedBy.Text = receipt.ReceivedBy;
                    
                    UpdateButtonState();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading receipt: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadCustomerInvoices(int customerId)
        {
            try
            {
                var invoices = _salesInvoiceRepository.GetSalesInvoicesByCustomer(customerId);
                
                cmbInvoiceReference.Items.Clear();
                cmbInvoiceReference.Items.Add("Select Invoice");
                
                foreach (var invoice in invoices.Where(i => i.Status == "PAID" || i.Status == "CONFIRMED"))
                {
                    cmbInvoiceReference.Items.Add($"{invoice.InvoiceNumber} - {invoice.TotalAmount:N2}");
                }
                
                cmbInvoiceReference.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer invoices: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void GenerateReceiptNumber()
        {
            try
            {
                string receiptNumber = _customerReceiptService.GenerateReceiptNumber();
                txtReceiptNumber.Text = receiptNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating receipt number: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ClearForm()
        {
            txtReceiptNumber.Text = "";
            dtpReceiptDate.Value = DateTime.Now;
            cmbCustomer.SelectedIndex = 0;
            txtAmount.Text = "";
            cmbPaymentMethod.SelectedIndex = 0;
            cmbInvoiceReference.Items.Clear();
            cmbInvoiceReference.Items.Add("Select Invoice");
            cmbInvoiceReference.SelectedIndex = 0;
            txtDescription.Text = "";
            txtReceivedBy.Text = UserSession.GetDisplayName();
            
            _currentReceipt = new CustomerReceipt();
            _isEditMode = false;
            _currentReceiptId = 0;
            
            GenerateReceiptNumber();
        }
        
        private void UpdateButtonState()
        {
            btnSave.Text = _isEditMode ? "Update Receipt" : "Save Receipt";
            btnDelete.Enabled = _isEditMode && _currentReceiptId > 0;
            btnPrint.Enabled = _isEditMode && _currentReceiptId > 0;
        }
        
        private string GenerateReceiptText(CustomerReceipt receipt)
        {
            var receiptText = new System.Text.StringBuilder();
            
            // Company header
            receiptText.AppendLine("           [COMPANY LOGO]");
            receiptText.AppendLine("        Your Company Name");
            receiptText.AppendLine("     123 Main Street, Karachi");
            receiptText.AppendLine("     Phone: +92-21-1234567");
            receiptText.AppendLine("     GST#: 1234567890123");
            receiptText.AppendLine("     NTN#: 1234567890123");
            receiptText.AppendLine("─────────────────────────────");
            
            // Receipt details
            receiptText.AppendLine($"Receipt No.: {receipt.ReceiptNumber}");
            receiptText.AppendLine($"Date: {receipt.ReceiptDate:dd/MM/yyyy}");
            receiptText.AppendLine($"Received From: {receipt.CustomerName}");
            receiptText.AppendLine($"Amount: {receipt.Amount:N2}");
            receiptText.AppendLine($"Payment Method: {receipt.PaymentMethod}");
            
            if (!string.IsNullOrEmpty(receipt.InvoiceReference))
            {
                receiptText.AppendLine($"For: Invoice #{receipt.InvoiceReference}");
            }
            
            if (!string.IsNullOrEmpty(receipt.Description))
            {
                receiptText.AppendLine($"Description: {receipt.Description}");
            }
            
            receiptText.AppendLine("─────────────────────────────");
            receiptText.AppendLine($"Received By: {receipt.ReceivedBy}");
            receiptText.AppendLine($"Status: {receipt.Status}");
            receiptText.AppendLine("─────────────────────────────");
            receiptText.AppendLine("");
            receiptText.AppendLine("Thank you for your payment!");
            receiptText.AppendLine("Visit us again soon");
            
            return receiptText.ToString();
        }
        
        #endregion
    }
}
