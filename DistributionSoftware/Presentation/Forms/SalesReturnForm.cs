using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SalesReturnForm : Form
    {
        private ISalesReturnService _salesReturnService;
        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;
        private ISalesInvoiceRepository _salesInvoiceRepository;
        private SalesReturn _currentReturn;
        private bool _isReturnSaved;
        private List<SalesReturnItem> _returnItems;
        private SalesInvoice _selectedInvoice;

        public SalesReturnForm()
        {
            try
            {
                InitializeComponent();
                _salesReturnService = new SalesReturnService();
                _productRepository = new ProductRepository();
                _customerRepository = new CustomerRepository();
                _salesInvoiceRepository = new SalesInvoiceRepository();
                _currentReturn = new SalesReturn();
                _isReturnSaved = false;
                _returnItems = new List<SalesReturnItem>();
                _selectedInvoice = null;
                
                // Initialize return
                InitializeReturn();
                
                // Load data
                LoadCustomers();
                LoadProducts();
                LoadInvoices();
                LoadReturnReasons();
                
                // Set default values
                SetDefaultValues();
                
                // Update button state
                UpdateButtonState();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Sales Return Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void InitializeReturn()
        {
            _currentReturn.ReturnDate = DateTime.Now;
            _currentReturn.Status = "PENDING";
            _currentReturn.SalesReturnItems = new List<SalesReturnItem>();
            
            // Generate barcode automatically
            try
            {
                _currentReturn.ReturnBarcode = _salesReturnService.GenerateReturnBarcode();
                txtReturnBarcode.Text = _currentReturn.ReturnBarcode;
                GenerateBarcodeImage(_currentReturn.ReturnBarcode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerRepository.GetActiveCustomers();
                cmbCustomer.DataSource = customers;
                cmbCustomer.DisplayMember = "ContactName";
                cmbCustomer.ValueMember = "CustomerId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                var products = _productRepository.GetActiveProducts();
                // Product controls removed - returns are from existing invoices
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoices()
        {
            try
            {
                var invoices = _salesInvoiceRepository.GetAllSalesInvoices();
                cmbInvoiceNumber.DataSource = invoices;
                cmbInvoiceNumber.DisplayMember = "InvoiceNumber";
                cmbInvoiceNumber.ValueMember = "SalesInvoiceId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadReturnReasons()
        {
            try
            {
                cmbReturnReason.Items.Clear();
                cmbReturnReason.Items.AddRange(new string[] {
                    "Damaged Product",
                    "Wrong Item",
                    "Customer Return",
                    "Defective Item",
                    "Size/Color Issue",
                    "Quality Issue",
                    "Late Delivery",
                    "Other"
                });
                cmbReturnReason.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading return reasons: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultValues()
        {
            // Set current date and time
            dtpReturnDate.Value = DateTime.Now;
            txtTransactionTime.Text = DateTime.Now.ToString("HH:mm:ss");
            
            // Set cashier name
            txtCashier.Text = UserSession.GetDisplayName();
            
            // Generate return number
            GenerateReturnNumber();
            
            // Clear all fields
            ClearFields();
        }

        private void GenerateReturnNumber()
        {
            try
            {
                // Generate a simple return number based on current date and time
                var returnNumber = $"SR{DateTime.Now:yyyyMMddHHmmss}";
                txtReturnNumber.Text = returnNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating return number: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            cmbCustomer.SelectedIndex = -1;
            txtCustomerName.Clear();
            txtCustomerPhone.Clear();
            txtCustomerAddress.Clear();
            
            // Product controls removed - returns are from existing invoices
            
            txtRemarks.Clear();
            cmbReturnReason.SelectedIndex = -1;
            
            _returnItems.Clear();
            RefreshItemsGrid();
            CalculateTotals();
        }

        private void UpdateButtonState()
        {
            var selectedItems = GetSelectedItems();
            btnSave.Enabled = selectedItems.Count > 0 && cmbCustomer.SelectedItem != null;
            // Add item button removed - returns are from existing invoices
        }

        private void RefreshItemsGrid()
        {
            try
            {
                if (dgvItems == null || _returnItems == null)
                    return;
                    
                dgvItems.Rows.Clear();
                
                foreach (var item in _returnItems)
                {
                    var row = new object[]
                    {
                        true, // Checkbox - selected by default
                        item.Product?.ProductCode ?? "",
                        item.Product?.ProductName ?? "",
                        item.Quantity.ToString("N2"),
                        item.UnitPrice.ToString("N2"),
                        item.DiscountAmount.ToString("N2"),
                        item.TaxAmount.ToString("N2"),
                        item.TotalAmount.ToString("N2")
                    };
                    
                    dgvItems.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing items grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateTotals()
        {
            try
            {
                var selectedItems = GetSelectedItems();
                decimal subtotal = selectedItems.Sum(item => item.Quantity * item.UnitPrice);
                decimal discountAmount = selectedItems.Sum(item => item.DiscountAmount);
                decimal taxAmount = selectedItems.Sum(item => item.TaxAmount);
                decimal totalAmount = subtotal + taxAmount - discountAmount;
                
                txtSubtotal.Text = subtotal.ToString("N2");
                txtDiscountAmount.Text = discountAmount.ToString("N2");
                txtTaxableAmount.Text = subtotal.ToString("N2");
                txtTaxAmount.Text = taxAmount.ToString("N2");
                txtTotalAmount.Text = totalAmount.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating totals: {ex.Message}", "Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<SalesReturnItem> GetSelectedItems()
        {
            try
            {
                var selectedItems = new List<SalesReturnItem>();
                
                if (dgvItems?.Rows == null || _returnItems == null)
                    return selectedItems;
                
                for (int i = 0; i < dgvItems.Rows.Count; i++)
                {
                    if (dgvItems.Rows[i].Cells[0].Value != null)
                    {
                        var isSelected = Convert.ToBoolean(dgvItems.Rows[i].Cells[0].Value);
                        if (isSelected && i < _returnItems.Count)
                        {
                            selectedItems.Add(_returnItems[i]);
                        }
                    }
                }
                
                return selectedItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting selected items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<SalesReturnItem>();
            }
        }

        private void SalesReturnForm_Load(object sender, EventArgs e)
        {
            // Form loaded successfully
        }

        private void BtnGenerateBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                var barcode = _salesReturnService.GenerateReturnBarcode();
                txtReturnBarcode.Text = barcode;
                _currentReturn.ReturnBarcode = barcode;
                GenerateBarcodeImage(barcode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateBarcodeImage(string barcode)
        {
            try
            {
                // Create a Code 128 barcode image
                var bitmap = new Bitmap(300, 100);
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.White);
                    
                    // Generate Code 128 barcode pattern
                    var barcodePattern = GenerateCode128Pattern(barcode);
                    var barWidth = 2;
                    var x = 10;
                    var barHeight = 80;
                    
                    // Draw barcode bars
                    for (int i = 0; i < barcodePattern.Length; i++)
                    {
                        if (barcodePattern[i] == '1')
                        {
                            graphics.FillRectangle(Brushes.Black, x, 10, barWidth, barHeight);
                        }
                        x += barWidth;
                    }
                    
                    // Draw quiet zones (start and end)
                    graphics.FillRectangle(Brushes.White, 0, 0, 10, 100);
                    graphics.FillRectangle(Brushes.White, x, 0, 10, 100);
                }
                
                picBarcode.Image = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateCode128Pattern(string data)
        {
            // Simplified Code 128 pattern generation
            // This creates a basic scannable barcode pattern
            var pattern = "11010010000"; // Start pattern
            
            foreach (char c in data)
            {
                var ascii = (int)c;
                // Convert ASCII to binary pattern (simplified)
                var binary = Convert.ToString(ascii, 2).PadLeft(8, '0');
                pattern += binary;
            }
            
            pattern += "11000101000"; // Stop pattern
            
            return pattern;
        }

        private void ShowOriginalInvoiceDetails()
        {
            try
            {
                if (_selectedInvoice != null)
                {
                    txtOriginalInvoiceNumber.Text = _selectedInvoice.InvoiceNumber;
                    txtOriginalInvoiceDate.Text = _selectedInvoice.InvoiceDate.ToString("yyyy-MM-dd");
                    txtOriginalInvoiceTotal.Text = _selectedInvoice.TotalAmount.ToString("N2");
                    txtOriginalInvoiceStatus.Text = _selectedInvoice.Status;
                    
                    // Show the panel and bring it to front
                    pnlOriginalInvoiceDetails.Visible = true;
                    pnlOriginalInvoiceDetails.BringToFront();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing original invoice details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SalesReturnForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isReturnSaved && _returnItems.Count > 0)
            {
                var result = MessageBox.Show("You have unsaved changes. Do you want to save before closing?", 
                    "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    BtnSave_Click(sender, e);
                    if (!_isReturnSaved)
                    {
                        e.Cancel = true;
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void CmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedItem != null)
            {
                var selectedCustomer = (Customer)cmbCustomer.SelectedItem;
                txtCustomerName.Text = selectedCustomer.ContactName;
                txtCustomerPhone.Text = selectedCustomer.Phone ?? "";
                txtCustomerAddress.Text = selectedCustomer.Address ?? "";
            }
            else
            {
                txtCustomerName.Clear();
                txtCustomerPhone.Clear();
                txtCustomerAddress.Clear();
            }
            
            UpdateButtonState();
        }




        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0)
            {
                var selectedIndex = dgvItems.SelectedRows[0].Index;
                if (selectedIndex >= 0 && selectedIndex < _returnItems.Count)
                {
                    _returnItems.RemoveAt(selectedIndex);
                    RefreshItemsGrid();
                    CalculateTotals();
                    UpdateButtonState();
                }
            }
            else
            {
                MessageBox.Show("Please select an item to remove.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Handle checkbox changes
                if (e.ColumnIndex == 0 && e.RowIndex >= 0) // Checkbox column
                {
                    CalculateTotals();
                    UpdateButtonState();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling cell click: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItems = GetSelectedItems();
                if (selectedItems.Count == 0)
                {
                    MessageBox.Show("Please select at least one item to return.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbCustomer.SelectedItem == null)
                {
                    MessageBox.Show("Please select a customer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Prepare sales return data
                _currentReturn.CustomerId = ((Customer)cmbCustomer.SelectedItem).CustomerId;
                _currentReturn.ReturnDate = dtpReturnDate.Value.Date;
                _currentReturn.ReturnNumber = txtReturnNumber.Text;
                _currentReturn.Reason = cmbReturnReason.SelectedItem?.ToString() ?? "";
                _currentReturn.Remarks = txtRemarks.Text;
                _currentReturn.ReferenceSalesInvoiceId = _selectedInvoice?.SalesInvoiceId;
                // Only include selected items
                var returnItems = GetSelectedItems();
                _currentReturn.SalesReturnItems = returnItems;

                // Calculate totals from selected items only
                _currentReturn.SubTotal = returnItems.Sum(item => item.Quantity * item.UnitPrice);
                _currentReturn.TaxAmount = returnItems.Sum(item => item.TaxAmount);
                _currentReturn.DiscountAmount = returnItems.Sum(item => item.DiscountAmount);
                _currentReturn.TotalAmount = _currentReturn.SubTotal + _currentReturn.TaxAmount - _currentReturn.DiscountAmount;

                // Save the sales return
                var returnId = _salesReturnService.CreateSalesReturn(_currentReturn);
                
                if (returnId > 0)
                {
                    // Update stock for returned items
                    var userId = UserSession.CurrentUserId > 0 ? UserSession.CurrentUserId : 1;
                    var stockUpdated = _salesReturnService.UpdateStockForSalesReturn(returnId, userId);
                    
                    _isReturnSaved = true;
                    var message = stockUpdated ? 
                        $"Sales return saved successfully with ID: {returnId}. Stock has been updated." :
                        $"Sales return saved successfully with ID: {returnId}. Warning: Stock update failed.";
                    
                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, 
                        stockUpdated ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                    
                    // Clear form for new return
                    ClearFields();
                    GenerateReturnNumber();
                }
                else
                {
                    MessageBox.Show("Failed to save sales return. Return ID was 0 or negative.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving sales return: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (!_isReturnSaved && _returnItems.Count > 0)
            {
                var result = MessageBox.Show("You have unsaved changes. Do you want to save before canceling?", 
                    "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    BtnSave_Click(sender, e);
                    if (_isReturnSaved)
                    {
                        this.Close();
                    }
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void BtnLoadInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbInvoiceNumber.SelectedItem == null)
                {
                    MessageBox.Show("Please select an invoice to load.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _selectedInvoice = (SalesInvoice)cmbInvoiceNumber.SelectedItem;
                
                // Load invoice details
                _selectedInvoice.Items = _salesInvoiceRepository.GetSalesInvoiceDetails(_selectedInvoice.SalesInvoiceId);
                
                // Auto-fill customer information
                var customer = _customerRepository.GetCustomerById(_selectedInvoice.CustomerId);
                if (customer != null)
                {
                    cmbCustomer.SelectedValue = customer.CustomerId;
                    txtCustomerName.Text = customer.ContactName;
                    txtCustomerPhone.Text = customer.Phone ?? "";
                    txtCustomerAddress.Text = customer.Address ?? "";
                }

                // Show original invoice details
                ShowOriginalInvoiceDetails();

                // Auto-fill invoice items
                LoadInvoiceItems();

                MessageBox.Show($"Invoice {_selectedInvoice.InvoiceNumber} loaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoiceItems()
        {
            try
            {
                if (_selectedInvoice?.Items != null)
                {
                    _returnItems.Clear();
                    
                    foreach (var detail in _selectedInvoice.Items)
                    {
                        var returnItem = new SalesReturnItem
                        {
                            ProductId = detail.ProductId,
                            OriginalInvoiceItemId = detail.DetailId,
                            Quantity = detail.Quantity,
                            UnitPrice = detail.UnitPrice,
                            TaxPercentage = detail.TaxPercentage,
                            TaxAmount = detail.TaxAmount,
                            DiscountPercentage = detail.DiscountPercentage,
                            DiscountAmount = detail.DiscountAmount,
                            TotalAmount = detail.TotalAmount,
                            Remarks = $"Return from Invoice {_selectedInvoice.InvoiceNumber}",
                            StockUpdated = false
                        };
                        
                        _returnItems.Add(returnItem);
                    }
                    
                    RefreshItemsGrid();
                    CalculateTotals();
                    UpdateButtonState();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoice items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


