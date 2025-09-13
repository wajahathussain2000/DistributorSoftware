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
            // Initialize current receipt
            _currentReceipt = new CustomerReceipt();
            _isEditMode = false;
            _currentReceiptId = 0;
            
            // Don't call ClearForm() here - it will be called after data is loaded
        }
        
        private void LoadData()
        {
            LoadCustomers();
            LoadPaymentMethods();
            LoadReceiptsGrid();
            
            // Clear form after data is loaded to start empty
            ClearForm();
            
            // Show payment details for the selected payment method after clearing
            ShowPaymentDetails();
            
            // Ensure action buttons are visible (they're now always visible in new layout)
            EnsureActionButtonsVisible();
        }
        
        private void SetDefaultValues()
        {
            // Set default receipt date
            dtpReceiptDate.Value = DateTime.Now;
            
            // Set default payment method only if ComboBox has items
            if (cmbPaymentMethod.Items.Count > 0)
                cmbPaymentMethod.SelectedIndex = 0;
            
            // Generate receipt number
            GenerateReceiptNumber();
            
            // Set current user
            txtReceivedBy.Text = UserSession.GetDisplayName();
        }
        
        private void EnsureActionButtonsVisible()
        {
            try
            {
                // Action buttons are now always visible in the new layout
                // No need for scrolling since they're docked at the bottom
                this.actionsGroup.BringToFront();
            }
            catch (Exception ex)
            {
                // If bringing to front fails, just continue
                System.Diagnostics.Debug.WriteLine($"Bring to front failed: {ex.Message}");
            }
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
                
                // Load card types
                cmbCardType.Items.Clear();
                cmbCardType.Items.AddRange(new string[] {
                    "Visa",
                    "MasterCard",
                    "American Express",
                    "Discover",
                    "Other"
                });
                
                cmbPaymentMethod.SelectedIndex = 0;
                
            // Add event handler for payment method changes
            cmbPaymentMethod.SelectedIndexChanged += CmbPaymentMethod_SelectedIndexChanged;
            
            // Add event handler for DataGridView data binding complete
            dgvReceipts.DataBindingComplete += DgvReceipts_DataBindingComplete;
            
            // Add event handler for customer selection to generate receipt number
            cmbCustomer.SelectedIndexChanged += CmbCustomer_SelectedIndexChanged;
            
            // Add keyboard shortcut to scroll to action buttons (Ctrl+End)
            this.KeyPreview = true;
            this.KeyDown += CustomerReceiptsForm_KeyDown;
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
                
                // Configure grid columns after data binding with a small delay
                // to ensure DataGridView is fully initialized
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 100; // 100ms delay
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    timer.Dispose();
                    DebugDataGridViewColumns();
                    ConfigureReceiptsGridColumns();
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading receipts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void DebugDataGridViewColumns()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"DataGridView Columns Count: {dgvReceipts.Columns.Count}");
                for (int i = 0; i < dgvReceipts.Columns.Count; i++)
                {
                    var column = dgvReceipts.Columns[i];
                    System.Diagnostics.Debug.WriteLine($"Column {i}: Name='{column.Name}', HeaderText='{column.HeaderText}', DataPropertyName='{column.DataPropertyName}'");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Debug error: {ex.Message}");
            }
        }
        
        private void ConfigureReceiptsGridColumns()
        {
            try
            {
                if (dgvReceipts.Columns.Count >= 10) // Ensure we have all expected columns
                {
                    // Configure columns by index to avoid name mismatch issues
                    // Column 0: ReceiptId - Hide this column
                    if (dgvReceipts.Columns[0] != null)
                    {
                        dgvReceipts.Columns[0].Visible = false;
                    }
                    
                    // Column 1: ReceiptNumber
                    if (dgvReceipts.Columns[1] != null)
                    {
                        dgvReceipts.Columns[1].HeaderText = "Receipt No.";
                        dgvReceipts.Columns[1].Width = 100;
                        dgvReceipts.Columns[1].DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                    }
                    
                    // Column 2: ReceiptDate
                    if (dgvReceipts.Columns[2] != null)
                    {
                        dgvReceipts.Columns[2].HeaderText = "Date";
                        dgvReceipts.Columns[2].Width = 80;
                        dgvReceipts.Columns[2].DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                    }
                    
                    // Column 3: CustomerName
                    if (dgvReceipts.Columns[3] != null)
                    {
                        dgvReceipts.Columns[3].HeaderText = "Customer";
                        dgvReceipts.Columns[3].Width = 150;
                        dgvReceipts.Columns[3].DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                    }
                    
                    // Column 4: Amount
                    if (dgvReceipts.Columns[4] != null)
                    {
                        dgvReceipts.Columns[4].HeaderText = "Amount";
                        dgvReceipts.Columns[4].Width = 100;
                        dgvReceipts.Columns[4].DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                        dgvReceipts.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    
                    // Column 5: PaymentMethod
                    if (dgvReceipts.Columns[5] != null)
                    {
                        dgvReceipts.Columns[5].HeaderText = "Payment Method";
                        dgvReceipts.Columns[5].Width = 150;
                        dgvReceipts.Columns[5].DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                    }
                    
                    // Column 6: InvoiceReference
                    if (dgvReceipts.Columns[6] != null)
                    {
                        dgvReceipts.Columns[6].HeaderText = "Invoice Ref.";
                        dgvReceipts.Columns[6].Width = 120;
                        dgvReceipts.Columns[6].DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                    }
                    
                    // Column 7: Description
                    if (dgvReceipts.Columns[7] != null)
                    {
                        dgvReceipts.Columns[7].HeaderText = "Description";
                        dgvReceipts.Columns[7].Width = 180;
                        dgvReceipts.Columns[7].DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                    }
                    
                    // Column 8: ReceivedBy
                    if (dgvReceipts.Columns[8] != null)
                    {
                        dgvReceipts.Columns[8].HeaderText = "Received By";
                        dgvReceipts.Columns[8].Width = 100;
                        dgvReceipts.Columns[8].DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                    }
                    
                    // Column 9: Status
                    if (dgvReceipts.Columns[9] != null)
                    {
                        dgvReceipts.Columns[9].HeaderText = "Status";
                        dgvReceipts.Columns[9].Width = 80;
                        dgvReceipts.Columns[9].DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                    }
                }
                
                // Apply styling to the DataGridView
                ApplyDataGridViewStyling();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configuring grid columns: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ApplyDataGridViewStyling()
        {
            try
            {
                // Header styling
                dgvReceipts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
                dgvReceipts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvReceipts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvReceipts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                // Row styling
                dgvReceipts.DefaultCellStyle.BackColor = Color.White;
                dgvReceipts.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
                dgvReceipts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
                dgvReceipts.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvReceipts.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
                
                // Alternating row colors
                dgvReceipts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
                dgvReceipts.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
                dgvReceipts.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
                
                // Grid lines
                dgvReceipts.GridColor = Color.FromArgb(230, 230, 230);
                dgvReceipts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying DataGridView styling: {ex.Message}");
            }
        }
        
        private void DgvReceipts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                // This event fires after data binding is complete
                // It's the safest time to configure columns
                DebugDataGridViewColumns();
                ConfigureReceiptsGridColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in DataBindingComplete: {ex.Message}", "Error", 
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
            try
            {
                if (cmbCustomer.SelectedItem != null)
                {
                    var selectedCustomer = cmbCustomer.SelectedItem;
                    var customerId = selectedCustomer.GetType().GetProperty("Id")?.GetValue(selectedCustomer);
                    
                    if (customerId != null && Convert.ToInt32(customerId) > 0)
                    {
                        // Generate receipt number when user selects a customer (starts creating receipt)
                        if (string.IsNullOrEmpty(txtReceiptNumber.Text))
                        {
                            GenerateReceiptNumber();
                        }
                        
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling customer selection: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
            // Validate payment method specific fields
            if (!ValidatePaymentMethodDetails())
            {
                return false;
            }
            
            return true;
        }
        
        private bool ValidatePaymentMethodDetails()
        {
            if (cmbPaymentMethod.SelectedItem == null)
            {
                MessageBox.Show("Please select a payment method.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPaymentMethod.Focus();
                return false;
            }
            
            string selectedMethod = cmbPaymentMethod.SelectedItem.ToString();
            
            switch (selectedMethod)
            {
                case "BANK_TRANSFER":
                    return ValidateBankTransferDetails();
                case "CHEQUE":
                    return ValidateChequeDetails();
                case "CARD":
                    return ValidateCardDetails();
                case "EASYPAISA":
                case "JAZZCASH":
                    return ValidateMobileWalletDetails();
                case "OTHER":
                    return ValidateOtherPaymentDetails();
                case "CASH":
                default:
                    return true; // No additional validation needed for cash
            }
        }
        
        private bool ValidateBankTransferDetails()
        {
            if (string.IsNullOrWhiteSpace(txtBankName.Text))
            {
                MessageBox.Show("Please enter the bank name for bank transfer.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBankName.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtAccountNumber.Text))
            {
                MessageBox.Show("Please enter the account number for bank transfer.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountNumber.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtTransactionId.Text))
            {
                MessageBox.Show("Please enter the transaction ID for bank transfer.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTransactionId.Focus();
                return false;
            }
            
            return true;
        }
        
        private bool ValidateChequeDetails()
        {
            if (string.IsNullOrWhiteSpace(txtBankName.Text))
            {
                MessageBox.Show("Please enter the bank name for cheque payment.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBankName.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtChequeNumber.Text))
            {
                MessageBox.Show("Please enter the cheque number.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChequeNumber.Focus();
                return false;
            }
            
            return true;
        }
        
        private bool ValidateCardDetails()
        {
            if (string.IsNullOrWhiteSpace(txtCardNumber.Text))
            {
                MessageBox.Show("Please enter the card number.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCardNumber.Focus();
                return false;
            }
            
            if (cmbCardType.SelectedIndex < 0)
            {
                MessageBox.Show("Please select the card type.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCardType.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtTransactionId.Text))
            {
                MessageBox.Show("Please enter the transaction ID for card payment.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTransactionId.Focus();
                return false;
            }
            
            return true;
        }
        
        private bool ValidateMobileWalletDetails()
        {
            if (string.IsNullOrWhiteSpace(txtMobileNumber.Text))
            {
                MessageBox.Show("Please enter the mobile number for mobile wallet payment.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMobileNumber.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtTransactionId.Text))
            {
                MessageBox.Show("Please enter the transaction ID for mobile wallet payment.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTransactionId.Focus();
                return false;
            }
            
            return true;
        }
        
        private bool ValidateOtherPaymentDetails()
        {
            if (string.IsNullOrWhiteSpace(txtPaymentReference.Text))
            {
                MessageBox.Show("Please enter a payment reference for other payment method.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPaymentReference.Focus();
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
            
            // Populate payment method specific details
            _currentReceipt.BankName = txtBankName.Text;
            _currentReceipt.AccountNumber = txtAccountNumber.Text;
            _currentReceipt.ChequeNumber = txtChequeNumber.Text;
            _currentReceipt.ChequeDate = dtpChequeDate.Visible ? dtpChequeDate.Value : (DateTime?)null;
            _currentReceipt.TransactionId = txtTransactionId.Text;
            _currentReceipt.CardNumber = txtCardNumber.Text;
            _currentReceipt.CardType = cmbCardType.Text;
            _currentReceipt.MobileNumber = txtMobileNumber.Text;
            _currentReceipt.PaymentReference = txtPaymentReference.Text;
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
                    
                    // Populate payment detail fields
                    txtBankName.Text = receipt.BankName ?? "";
                    txtAccountNumber.Text = receipt.AccountNumber ?? "";
                    txtChequeNumber.Text = receipt.ChequeNumber ?? "";
                    dtpChequeDate.Value = receipt.ChequeDate ?? DateTime.Now;
                    txtTransactionId.Text = receipt.TransactionId ?? "";
                    txtCardNumber.Text = receipt.CardNumber ?? "";
                    cmbCardType.Text = receipt.CardType ?? "";
                    txtMobileNumber.Text = receipt.MobileNumber ?? "";
                    txtPaymentReference.Text = receipt.PaymentReference ?? "";
                    
                    // Show payment details for the selected payment method
                    ShowPaymentDetails();
                    
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
            
            // Only set SelectedIndex if ComboBox has items
            if (cmbCustomer.Items.Count > 0)
                cmbCustomer.SelectedIndex = 0;
            
            txtAmount.Text = "";
            
            // Only set SelectedIndex if ComboBox has items
            if (cmbPaymentMethod.Items.Count > 0)
                cmbPaymentMethod.SelectedIndex = 0;
            
            cmbInvoiceReference.Items.Clear();
            cmbInvoiceReference.Items.Add("Select Invoice");
            cmbInvoiceReference.SelectedIndex = 0;
            txtDescription.Text = "";
            txtReceivedBy.Text = UserSession.GetDisplayName();
            
            // Clear payment detail fields
            txtBankName.Text = "";
            txtAccountNumber.Text = "";
            txtChequeNumber.Text = "";
            dtpChequeDate.Value = DateTime.Now;
            txtTransactionId.Text = "";
            txtCardNumber.Text = "";
            cmbCardType.SelectedIndex = -1;
            txtMobileNumber.Text = "";
            txtPaymentReference.Text = "";
            
            // Hide payment details group
            paymentDetailsGroup.Visible = false;
            
            _currentReceipt = new CustomerReceipt();
            _isEditMode = false;
            _currentReceiptId = 0;
            
            // Generate receipt number only when user starts creating a new receipt
            // Don't auto-generate on form load to keep form empty
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
            
            // Add payment method specific details
            AddPaymentMethodDetails(receiptText, receipt);
            
            receiptText.AppendLine("─────────────────────────────");
            receiptText.AppendLine($"Received By: {receipt.ReceivedBy}");
            receiptText.AppendLine($"Status: {receipt.Status}");
            receiptText.AppendLine("─────────────────────────────");
            receiptText.AppendLine("");
            receiptText.AppendLine("Thank you for your payment!");
            receiptText.AppendLine("Visit us again soon");
            
            return receiptText.ToString();
        }
        
        private void AddPaymentMethodDetails(System.Text.StringBuilder receiptText, CustomerReceipt receipt)
        {
            switch (receipt.PaymentMethod)
            {
                case "BANK_TRANSFER":
                    if (!string.IsNullOrEmpty(receipt.BankName))
                        receiptText.AppendLine($"Bank: {receipt.BankName}");
                    if (!string.IsNullOrEmpty(receipt.AccountNumber))
                        receiptText.AppendLine($"Account No: {receipt.AccountNumber}");
                    if (!string.IsNullOrEmpty(receipt.TransactionId))
                        receiptText.AppendLine($"Transaction ID: {receipt.TransactionId}");
                    break;
                    
                case "CHEQUE":
                    if (!string.IsNullOrEmpty(receipt.BankName))
                        receiptText.AppendLine($"Bank: {receipt.BankName}");
                    if (!string.IsNullOrEmpty(receipt.ChequeNumber))
                        receiptText.AppendLine($"Cheque No: {receipt.ChequeNumber}");
                    if (receipt.ChequeDate.HasValue)
                        receiptText.AppendLine($"Cheque Date: {receipt.ChequeDate.Value:dd/MM/yyyy}");
                    break;
                    
                case "CARD":
                    if (!string.IsNullOrEmpty(receipt.CardNumber))
                        receiptText.AppendLine($"Card No: {MaskCardNumber(receipt.CardNumber)}");
                    if (!string.IsNullOrEmpty(receipt.CardType))
                        receiptText.AppendLine($"Card Type: {receipt.CardType}");
                    if (!string.IsNullOrEmpty(receipt.TransactionId))
                        receiptText.AppendLine($"Transaction ID: {receipt.TransactionId}");
                    break;
                    
                case "EASYPAISA":
                case "JAZZCASH":
                    if (!string.IsNullOrEmpty(receipt.MobileNumber))
                        receiptText.AppendLine($"Mobile No: {receipt.MobileNumber}");
                    if (!string.IsNullOrEmpty(receipt.TransactionId))
                        receiptText.AppendLine($"Transaction ID: {receipt.TransactionId}");
                    if (!string.IsNullOrEmpty(receipt.PaymentReference))
                        receiptText.AppendLine($"Reference: {receipt.PaymentReference}");
                    break;
                    
                case "OTHER":
                    if (!string.IsNullOrEmpty(receipt.PaymentReference))
                        receiptText.AppendLine($"Reference: {receipt.PaymentReference}");
                    break;
                    
                case "CASH":
                default:
                    // No additional details needed for cash
                    break;
            }
        }
        
        private string MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 8)
                return cardNumber;
                
            // Show first 4 and last 4 digits, mask the middle
            return $"{cardNumber.Substring(0, 4)}****{cardNumber.Substring(cardNumber.Length - 4)}";
        }
        
        private void CustomerReceiptsForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // Ctrl+End to focus on action buttons (no scrolling needed in new layout)
                if (e.Control && e.KeyCode == Keys.End)
                {
                    btnSave.Focus();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"KeyDown error: {ex.Message}");
            }
        }
        
        #endregion
        
        #region Payment Method Event Handlers
        
        private void CmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ShowPaymentDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating payment details: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ShowPaymentDetails()
        {
            // Hide all payment detail fields first
            HideAllPaymentDetails();
            
            if (cmbPaymentMethod.SelectedItem == null)
            {
                paymentDetailsGroup.Visible = false;
                return;
            }
            
            string selectedMethod = cmbPaymentMethod.SelectedItem.ToString();
            
            switch (selectedMethod)
            {
                case "BANK_TRANSFER":
                    ShowBankTransferDetails();
                    break;
                case "CHEQUE":
                    ShowChequeDetails();
                    break;
                case "CARD":
                    ShowCardDetails();
                    break;
                case "EASYPAISA":
                case "JAZZCASH":
                    ShowMobileWalletDetails();
                    break;
                case "OTHER":
                    ShowOtherPaymentDetails();
                    break;
                case "CASH":
                default:
                    paymentDetailsGroup.Visible = false;
                    break;
            }
        }
        
        private void HideAllPaymentDetails()
        {
            // Reset payment details group
            paymentDetailsGroup.Text = "💳 Payment Details";
            paymentDetailsGroup.ForeColor = Color.FromArgb(52, 152, 219); // Default blue color
            
            // Hide all labels and controls
            lblBankName.Visible = false;
            txtBankName.Visible = false;
            txtBankName.Enabled = false;
            
            lblAccountNumber.Visible = false;
            txtAccountNumber.Visible = false;
            txtAccountNumber.Enabled = false;
            
            lblChequeNumber.Visible = false;
            txtChequeNumber.Visible = false;
            txtChequeNumber.Enabled = false;
            
            lblChequeDate.Visible = false;
            dtpChequeDate.Visible = false;
            dtpChequeDate.Enabled = false;
            
            lblTransactionId.Visible = false;
            txtTransactionId.Visible = false;
            txtTransactionId.Enabled = false;
            
            lblCardNumber.Visible = false;
            txtCardNumber.Visible = false;
            txtCardNumber.Enabled = false;
            
            lblCardType.Visible = false;
            cmbCardType.Visible = false;
            cmbCardType.Enabled = false;
            
            lblMobileNumber.Visible = false;
            txtMobileNumber.Visible = false;
            txtMobileNumber.Enabled = false;
            
            lblPaymentReference.Visible = false;
            txtPaymentReference.Visible = false;
            txtPaymentReference.Enabled = false;
        }
        
        private void ShowBankTransferDetails()
        {
            paymentDetailsGroup.Visible = true;
            paymentDetailsGroup.Text = "💳 Bank Transfer Details";
            paymentDetailsGroup.ForeColor = Color.FromArgb(46, 204, 113); // Green color for bank transfer
            
            lblBankName.Visible = true;
            txtBankName.Visible = true;
            txtBankName.Enabled = true;
            txtBankName.BackColor = Color.White;
            
            lblAccountNumber.Visible = true;
            txtAccountNumber.Visible = true;
            txtAccountNumber.Enabled = true;
            txtAccountNumber.BackColor = Color.White;
            
            lblTransactionId.Visible = true;
            txtTransactionId.Visible = true;
            txtTransactionId.Enabled = true;
            txtTransactionId.BackColor = Color.White;
            
            // Hide other payment method fields
            lblChequeNumber.Visible = false;
            txtChequeNumber.Visible = false;
            lblChequeDate.Visible = false;
            dtpChequeDate.Visible = false;
            lblCardNumber.Visible = false;
            txtCardNumber.Visible = false;
            lblCardType.Visible = false;
            cmbCardType.Visible = false;
            lblMobileNumber.Visible = false;
            txtMobileNumber.Visible = false;
            lblPaymentReference.Visible = false;
            txtPaymentReference.Visible = false;
        }
        
        private void ShowChequeDetails()
        {
            paymentDetailsGroup.Visible = true;
            paymentDetailsGroup.Text = "💳 Cheque Payment Details";
            paymentDetailsGroup.ForeColor = Color.FromArgb(231, 76, 60); // Red color for cheque
            
            lblBankName.Visible = true;
            txtBankName.Visible = true;
            txtBankName.Enabled = true;
            txtBankName.BackColor = Color.White;
            
            lblChequeNumber.Visible = true;
            txtChequeNumber.Visible = true;
            txtChequeNumber.Enabled = true;
            txtChequeNumber.BackColor = Color.White;
            
            lblChequeDate.Visible = true;
            dtpChequeDate.Visible = true;
            dtpChequeDate.Enabled = true;
            dtpChequeDate.BackColor = Color.White;
            
            // Hide other payment method fields
            lblAccountNumber.Visible = false;
            txtAccountNumber.Visible = false;
            lblTransactionId.Visible = false;
            txtTransactionId.Visible = false;
            lblCardNumber.Visible = false;
            txtCardNumber.Visible = false;
            lblCardType.Visible = false;
            cmbCardType.Visible = false;
            lblMobileNumber.Visible = false;
            txtMobileNumber.Visible = false;
            lblPaymentReference.Visible = false;
            txtPaymentReference.Visible = false;
        }
        
        private void ShowCardDetails()
        {
            paymentDetailsGroup.Visible = true;
            lblCardNumber.Visible = true;
            txtCardNumber.Visible = true;
            lblCardType.Visible = true;
            cmbCardType.Visible = true;
            lblTransactionId.Visible = true;
            txtTransactionId.Visible = true;
        }
        
        private void ShowMobileWalletDetails()
        {
            paymentDetailsGroup.Visible = true;
            lblMobileNumber.Visible = true;
            txtMobileNumber.Visible = true;
            lblTransactionId.Visible = true;
            txtTransactionId.Visible = true;
            lblPaymentReference.Visible = true;
            txtPaymentReference.Visible = true;
        }
        
        private void ShowOtherPaymentDetails()
        {
            paymentDetailsGroup.Visible = true;
            lblPaymentReference.Visible = true;
            txtPaymentReference.Visible = true;
        }
        
        #endregion
    }
}
