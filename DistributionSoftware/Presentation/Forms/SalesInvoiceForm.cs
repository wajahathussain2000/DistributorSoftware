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
    public partial class SalesInvoiceForm : Form
    {
        private ISalesInvoiceService _salesInvoiceService;
        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;
        private SalesInvoice _currentInvoice;
        private bool _isInvoiceSaved;
        private bool _isPrintMode;

        public SalesInvoiceForm()
        {
            InitializeComponent();
            _salesInvoiceService = new SalesInvoiceService();
            _productRepository = new ProductRepository();
            _customerRepository = new CustomerRepository();
            _currentInvoice = new SalesInvoice();
            _isInvoiceSaved = false;
            _isPrintMode = false;
            
            // Initialize invoice
            InitializeInvoice();
            
            // Load data
            LoadCustomers();
            LoadProducts();
            LoadPaymentModes();
            
            // Set default values
            SetDefaultValues();
            
            // Update button state
            UpdateButtonState();
            
            // Add event handlers for real-time calculations
            //txtPaidAmount.TextChanged += TxtPaidAmount_TextChanged;
        }


        private void InitializeInvoice()
        {
            _currentInvoice = new SalesInvoice
            {
                InvoiceDate = DateTime.Now,
                TransactionTime = DateTime.Now,
                Status = "DRAFT",
                PaymentMode = "CASH",
                CreatedBy = (UserSession.CurrentUser?.UserId ?? 1) > 0 ? (UserSession.CurrentUser?.UserId ?? 1) : 1,
                CreatedByUser = UserSession.CurrentUser?.FirstName + " " + UserSession.CurrentUser?.LastName ?? "System"
            };
        }

        private void LoadCustomers()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading customers from database...");
                // Load customers from database
                var customers = _customerRepository.GetActiveCustomers();
                System.Diagnostics.Debug.WriteLine($"Found {customers.Count} customers");
                
                cmbCustomer.Items.Clear();
                
                // Add Walk-in Customer as default
                cmbCustomer.Items.Add(new { Id = 0, Name = "Walk-in Customer", Phone = "", Address = "" });
                
                // Add real customers from database
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
                
                System.Diagnostics.Debug.WriteLine($"Successfully loaded {cmbCustomer.Items.Count} customers to dropdown");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading customers: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Fallback to sample data if database fails
                cmbCustomer.Items.Clear();
                cmbCustomer.Items.Add(new { Id = 0, Name = "Walk-in Customer", Phone = "", Address = "" });
                cmbCustomer.Items.Add(new { Id = 1, Name = "ABC Company Ltd.", Phone = "+92-21-1234567", Address = "123 Main Street, Karachi" });
                cmbCustomer.Items.Add(new { Id = 2, Name = "XYZ Trading Co.", Phone = "+92-21-7654321", Address = "456 Business Avenue, Lahore" });
                cmbCustomer.Items.Add(new { Id = 3, Name = "DEF Enterprises", Phone = "+92-21-9876543", Address = "789 Commercial Road, Islamabad" });
                
                cmbCustomer.DisplayMember = "Name";
                cmbCustomer.ValueMember = "Id";
                cmbCustomer.SelectedIndex = 0;
            }
        }

        private void LoadProducts()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading products from database...");
                // Load products from database with stock information
                var products = _productRepository.GetActiveProducts();
                System.Diagnostics.Debug.WriteLine($"Found {products.Count} products");
                
                cmbProduct.Items.Clear();
                
                // Add real products from database
                foreach (var product in products)
                {
                    var availableStock = product.Quantity - product.ReservedQuantity;
                    cmbProduct.Items.Add(new { 
                        Id = product.ProductId, 
                        Code = product.ProductCode ?? "", 
                        Name = product.ProductName ?? "", 
                        Price = product.SalePrice > 0 ? product.SalePrice : product.PurchasePrice,
                        Stock = availableStock,
                        FullProduct = product
                    });
                }
                
                cmbProduct.DisplayMember = "Name";
                cmbProduct.ValueMember = "Id";
                
                System.Diagnostics.Debug.WriteLine($"Successfully loaded {cmbProduct.Items.Count} products to dropdown");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading products: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Fallback to sample data if database fails
                cmbProduct.Items.Clear();
                cmbProduct.Items.Add(new { Id = 1, Code = "P001", Name = "Product A", Price = 1000m, Stock = 100 });
                cmbProduct.Items.Add(new { Id = 2, Code = "P002", Name = "Product B", Price = 2000m, Stock = 50 });
                cmbProduct.Items.Add(new { Id = 3, Code = "P003", Name = "Product C", Price = 1500m, Stock = 75 });
                cmbProduct.Items.Add(new { Id = 4, Code = "P004", Name = "Product D", Price = 3000m, Stock = 25 });
                
                cmbProduct.DisplayMember = "Name";
                cmbProduct.ValueMember = "Id";
            }
        }

        private void LoadPaymentModes()
        {
            //cmbPaymentMode.Items.Clear();
            //cmbPaymentMode.Items.Add("CASH");
            //cmbPaymentMode.Items.Add("CARD");
            //cmbPaymentMode.Items.Add("EASYPAISA");
            //cmbPaymentMode.Items.Add("JAZZCASH");
            //cmbPaymentMode.Items.Add("BANK_TRANSFER");
            //cmbPaymentMode.Items.Add("CHEQUE");
            //cmbPaymentMode.SelectedIndex = 0; // Default to CASH
        }

        private void SetDefaultValues()
        {
            dtpInvoiceDate.Value = DateTime.Now;
            txtInvoiceNumber.Text = _salesInvoiceService.GenerateInvoiceNumber();
            txtCashier.Text = _currentInvoice.CreatedByUser;
            txtTransactionTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void UpdateButtonState()
        {
            if (_isInvoiceSaved && !_isPrintMode)
            {
                btnSave.Text = "Print Invoice";
                btnSave.BackColor = Color.FromArgb(0, 120, 215); // Blue
                btnSave.Click -= BtnSave_Click;
                btnSave.Click += BtnPrint_Click;
            }
            else if (_isPrintMode)
            {
                btnSave.Text = "New Invoice";
                btnSave.BackColor = Color.FromArgb(40, 167, 69); // Green
                btnSave.Click -= BtnPrint_Click;
                btnSave.Click += BtnNewInvoice_Click;
            }
            else
            {
                btnSave.Text = "Create Invoice";
                btnSave.BackColor = Color.FromArgb(40, 167, 69); // Green
                btnSave.Click -= BtnNewInvoice_Click;
                btnSave.Click += BtnSave_Click;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Prevent double-click/double-save
                if (_isInvoiceSaved)
                {
                    return;
                }

                // Disable button temporarily to prevent double-clicks
                btnSave.Enabled = false;
                btnSave.Text = "Creating...";

                // Validate form
                if (!ValidateForm())
                {
                    btnSave.Enabled = true;
                    btnSave.Text = "Create Invoice";
                    return;
                }

                // Update invoice from form
                UpdateInvoiceFromForm();

                // Calculate totals
                _salesInvoiceService.CalculateInvoiceTotals(_currentInvoice);

                // Save invoice
                var invoiceId = _salesInvoiceService.CreateSalesInvoice(_currentInvoice);
                
                // Debug: Show invoice creation result
                MessageBox.Show($"Invoice Creation Debug:\nReturned Invoice ID: {invoiceId}\nInvoice Number: {_currentInvoice.InvoiceNumber}", 
                    "Invoice Creation Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if (invoiceId > 0)
                {
                    _currentInvoice.SalesInvoiceId = invoiceId;
                    _isInvoiceSaved = true;
                    _isPrintMode = false;
                    
                    // Display barcode image
                    DisplayBarcodeImage(_currentInvoice.BarcodeImage, _currentInvoice.Barcode);
                    
                    MessageBox.Show($"Invoice created successfully!\nInvoice Number: {_currentInvoice.InvoiceNumber}\nStatus: DRAFT - Ready for Payment", 
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    UpdateButtonState();
                    UpdateTotalsDisplay();
                }
                else
                {
                    MessageBox.Show("Failed to save invoice. Please try again.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSave.Enabled = true;
                    btnSave.Text = "Create Invoice";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving invoice: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = true;
                btnSave.Text = "Create Invoice";
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                // Generate thermal receipt
                var receipt = _salesInvoiceService.GenerateThermalReceipt(_currentInvoice);
                
                // Print receipt (implement thermal printer integration)
                PrintThermalReceipt(receipt);
                
                // Update print status
                _salesInvoiceService.UpdatePrintStatus(_currentInvoice.SalesInvoiceId, true);
                
                _isPrintMode = true;
                UpdateButtonState();
                
                MessageBox.Show("Invoice printed successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing invoice: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNewInvoice_Click(object sender, EventArgs e)
        {
            // Reset form for new invoice
            ResetForm();
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbProduct.SelectedItem == null)
                {
                    MessageBox.Show("Please select a product.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtQuantity.Text) || !decimal.TryParse(txtQuantity.Text, out decimal quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedProduct = (dynamic)cmbProduct.SelectedItem;
                var unitPrice = decimal.TryParse(txtUnitPrice.Text, out decimal price) ? price : selectedProduct.Price;

                // Check stock availability
                if (quantity > selectedProduct.Stock)
                {
                    MessageBox.Show($"Insufficient stock. Available: {selectedProduct.Stock}", "Stock Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Create invoice detail
                var detail = new SalesInvoiceDetail
                {
                    ProductId = selectedProduct.Id,
                    ProductCode = selectedProduct.Code ?? "",
                    ProductName = selectedProduct.Name ?? "",
                    ProductDescription = selectedProduct.FullProduct?.ProductDescription ?? "",
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    TaxPercentage = 17, // Default GST rate
                    LineTotal = quantity * unitPrice
                };

                // Calculate line totals
                detail.TaxableAmount = detail.LineTotal;
                detail.TaxAmount = detail.TaxableAmount * (detail.TaxPercentage / 100);
                detail.LineTotal = detail.TaxableAmount + detail.TaxAmount;
                detail.TotalAmount = detail.LineTotal; // Set TotalAmount for database (same as LineTotal)

                // Add to invoice
                _currentInvoice.Items.Add(detail);

                // Refresh grid
                RefreshItemsGrid();
                
                // Calculate totals
                _salesInvoiceService.CalculateInvoiceTotals(_currentInvoice);
                UpdateTotalsDisplay();

                // Clear product selection
                cmbProduct.SelectedIndex = -1;
                txtQuantity.Clear();
                txtUnitPrice.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvItems.SelectedRows.Count > 0)
                {
                    var selectedIndex = dgvItems.SelectedRows[0].Index;
                    if (selectedIndex >= 0 && selectedIndex < _currentInvoice.Items.Count)
                    {
                        _currentInvoice.Items.RemoveAt(selectedIndex);
                        RefreshItemsGrid();
                        
                        // Recalculate totals
                        _salesInvoiceService.CalculateInvoiceTotals(_currentInvoice);
                        UpdateTotalsDisplay();
                    }
                }
                else
                {
                    MessageBox.Show("Please select an item to remove.", "Selection Required", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing item: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Payment functionality removed - payment section was removed from form
        /*
        private void BtnProcessPayment_Click(object sender, EventArgs e)
        {
            // Payment processing code commented out
        }
        */

        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem != null)
            {
                var selectedProduct = (dynamic)cmbProduct.SelectedItem;
                txtUnitPrice.Text = selectedProduct.Price.ToString("N2");
                
                // Show real-time stock information
                var availableStock = selectedProduct.Stock;
                lblStock.Text = $"Stock: {availableStock:N0}";
                
                // Color code the stock level
                if (availableStock <= 0)
                {
                    lblStock.ForeColor = Color.Red;
                    lblStock.Text += " (OUT OF STOCK)";
                }
                else if (availableStock <= 10)
                {
                    lblStock.ForeColor = Color.Orange;
                    lblStock.Text += " (LOW STOCK)";
                }
                else
                {
                    lblStock.ForeColor = Color.Green;
                }
                
                // Auto-focus on quantity field for faster entry
                txtQuantity.Focus();
            }
        }

        private void CmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedItem != null)
            {
                var selectedCustomer = (dynamic)cmbCustomer.SelectedItem;
                txtCustomerName.Text = selectedCustomer.Name;
                txtCustomerPhone.Text = selectedCustomer.Phone;
                txtCustomerAddress.Text = selectedCustomer.Address;
            }
        }

        private void RefreshItemsGrid()
        {
            dgvItems.Rows.Clear();
            
            foreach (var item in _currentInvoice.Items)
            {
                dgvItems.Rows.Add(
                    item.ProductCode,
                    item.ProductName,
                    item.Quantity,
                    item.UnitPrice.ToString("N2"),
                    item.DiscountAmount.ToString("N2"),
                    item.TaxAmount.ToString("N2"),
                    item.LineTotal.ToString("N2")
                );
            }
        }

        // Payment functionality removed - payment section was removed from form
        /*
        private void TxtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            // Update change amount in real-time when paid amount changes
            if (decimal.TryParse(txtPaidAmount.Text, out decimal paidAmount))
            {
                _currentInvoice.ChangeAmount = paidAmount - _currentInvoice.TotalAmount;
                txtChangeAmount.Text = _currentInvoice.ChangeAmount.ToString("N2");
            }
            else
            {
                txtChangeAmount.Text = "0.00";
            }
        }
        */

        private void UpdateTotalsDisplay()
        {
            txtSubtotal.Text = _currentInvoice.Subtotal.ToString("N2");
            txtDiscountAmount.Text = _currentInvoice.DiscountAmount.ToString("N2");
            txtTaxableAmount.Text = _currentInvoice.TaxableAmount.ToString("N2");
            txtTaxAmount.Text = _currentInvoice.TaxAmount.ToString("N2");
            txtTotalAmount.Text = _currentInvoice.TotalAmount.ToString("N2");
            // Payment controls removed
            // txtPaidAmount.Text = _currentInvoice.PaidAmount.ToString("N2");
            
            // Calculate change amount in real-time - commented out
            /*
            decimal paidAmount = 0;
            if (decimal.TryParse(txtPaidAmount.Text, out paidAmount))
            {
                _currentInvoice.ChangeAmount = paidAmount - _currentInvoice.TotalAmount;
                txtChangeAmount.Text = _currentInvoice.ChangeAmount.ToString("N2");
            }
            else
            {
                txtChangeAmount.Text = "0.00";
            }
            */
        }

        private void UpdateInvoiceFromForm()
        {
            if (cmbCustomer.SelectedItem != null)
            {
                var selectedCustomer = (dynamic)cmbCustomer.SelectedItem;
                _currentInvoice.CustomerId = selectedCustomer.Id;
                _currentInvoice.CustomerName = selectedCustomer.Name;
                _currentInvoice.CustomerPhone = selectedCustomer.Phone;
                _currentInvoice.CustomerAddress = selectedCustomer.Address;
            }
            else
            {
                // Default to Walk-in Customer if none selected
                _currentInvoice.CustomerId = 0;
                _currentInvoice.CustomerName = "Walk-in Customer";
                _currentInvoice.CustomerPhone = "";
                _currentInvoice.CustomerAddress = "";
            }

            _currentInvoice.InvoiceDate = dtpInvoiceDate.Value;
            // Payment controls removed
            // _currentInvoice.PaymentMode = cmbPaymentMode.Text;
            _currentInvoice.Remarks = txtRemarks.Text;
            
            // Update paid amount from form - commented out
            /*
            if (decimal.TryParse(txtPaidAmount.Text, out decimal paidAmount))
            {
                _currentInvoice.PaidAmount = paidAmount;
                _currentInvoice.ChangeAmount = paidAmount - _currentInvoice.TotalAmount;
            }
            */
        }

        private bool ValidateForm()
        {
            if (cmbCustomer.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_currentInvoice.Items == null || !_currentInvoice.Items.Any())
            {
                MessageBox.Show("Please add at least one item to the invoice.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ResetForm()
        {
            // Reset invoice
            InitializeInvoice();
            
            // Clear form
            cmbCustomer.SelectedIndex = 0;
            txtInvoiceNumber.Text = _salesInvoiceService.GenerateInvoiceNumber();
            dtpInvoiceDate.Value = DateTime.Now;
            txtCashier.Text = _currentInvoice.CreatedByUser;
            txtTransactionTime.Text = DateTime.Now.ToString("HH:mm:ss");
            // Payment controls removed
            // cmbPaymentMode.SelectedIndex = 0;
            txtRemarks.Clear();
            // txtPaidAmount.Clear();
            
            // Clear items
            dgvItems.Rows.Clear();
            
            // Clear totals
            txtSubtotal.Clear();
            txtDiscountAmount.Clear();
            txtTaxableAmount.Clear();
            txtTaxAmount.Clear();
            txtTotalAmount.Clear();
            // txtChangeAmount.Clear(); // Payment control removed
            
            // Reset flags
            _isInvoiceSaved = false;
            _isPrintMode = false;
            
            // Clear barcode display
            DisplayBarcodeImage(null, "");
            
            // Update button state
            UpdateButtonState();
        }

        private void DisplayBarcodeImage(byte[] barcodeImageData, string barcodeText)
        {
            try
            {
                if (barcodeImageData != null && barcodeImageData.Length > 0)
                {
                    using (var stream = new System.IO.MemoryStream(barcodeImageData))
                    {
                        var image = Image.FromStream(stream);
                        picBarcode.Image = new Bitmap(image);
                    }
                    lblBarcode.Text = $"Barcode: {barcodeText}";
                }
                else
                {
                    picBarcode.Image = null;
                    lblBarcode.Text = "No Barcode";
                }
            }
            catch (Exception ex)
            {
                picBarcode.Image = null;
                lblBarcode.Text = "Barcode Error";
            }
        }

        private void PrintThermalReceipt(string receipt)
        {
            // Implement thermal printer integration
            // For now, show receipt in a message box
            MessageBox.Show($"Thermal Receipt:\n\n{receipt}", "Receipt Preview", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SalesInvoiceForm_Load(object sender, EventArgs e)
        {
            // Form loaded event
        }

        private void SalesInvoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isInvoiceSaved && !_isPrintMode)
            {
                var result = MessageBox.Show("Invoice is saved but not printed. Do you want to print it now?", 
                    "Print Invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    BtnPrint_Click(sender, e);
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Direct Payment Processing Methods

        private bool ProcessPaymentDirect(SalesPayment payment)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(Common.ConfigurationManager.DistributionConnectionString))
                {
                    connection.Open();
                    
                    // First, verify the invoice exists
                    var checkQuery = "SELECT COUNT(*) FROM SalesInvoices WHERE SalesInvoiceId = @SalesInvoiceId";
                    using (var checkCommand = new System.Data.SqlClient.SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@SalesInvoiceId", payment.SalesInvoiceId);
                        int invoiceExists = Convert.ToInt32(checkCommand.ExecuteScalar());
                        
                        if (invoiceExists == 0)
                        {
                            MessageBox.Show($"Invoice with ID {payment.SalesInvoiceId} does not exist in database. Please save the invoice first.", 
                                "Invoice Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    
                    // Simple, direct payment insertion
                    var query = @"
                        INSERT INTO SalesPayments (
                            SalesInvoiceId, PaymentMode, Amount, PaymentDate, Status, 
                            CreatedBy, CreatedDate, Remarks
                        ) VALUES (
                            @SalesInvoiceId, @PaymentMode, @Amount, @PaymentDate, @Status, 
                            @CreatedBy, @CreatedDate, @Remarks
                        )";

                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SalesInvoiceId", payment.SalesInvoiceId);
                        command.Parameters.AddWithValue("@PaymentMode", payment.PaymentMode ?? "CASH");
                        command.Parameters.AddWithValue("@Amount", payment.Amount);
                        command.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Status", "COMPLETED");
                        command.Parameters.AddWithValue("@CreatedBy", payment.CreatedBy);
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        command.Parameters.AddWithValue("@Remarks", payment.Remarks ?? "");

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Direct Payment Error: {ex.Message}", "Payment Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void UpdateInvoiceStatusDirect(string status)
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(Common.ConfigurationManager.DistributionConnectionString))
                {
                    connection.Open();
                    
                    var query = @"
                        UPDATE SalesInvoices 
                        SET Status = @Status, PaidAmount = @PaidAmount, ChangeAmount = @ChangeAmount
                        WHERE SalesInvoiceId = @SalesInvoiceId";

                    using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", status);
                        command.Parameters.AddWithValue("@PaidAmount", _currentInvoice.PaidAmount);
                        command.Parameters.AddWithValue("@ChangeAmount", _currentInvoice.ChangeAmount);
                        command.Parameters.AddWithValue("@SalesInvoiceId", _currentInvoice.SalesInvoiceId);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update Status Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessStockReductionDirect()
        {
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(Common.ConfigurationManager.DistributionConnectionString))
                {
                    connection.Open();
                    
                    foreach (var item in _currentInvoice.Items)
                    {
                        var query = @"
                            UPDATE Products 
                            SET StockQuantity = StockQuantity - @Quantity,
                                ReservedQuantity = ReservedQuantity - @Quantity
                            WHERE ProductId = @ProductId";

                        using (var command = new System.Data.SqlClient.SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Quantity", item.Quantity);
                            command.Parameters.AddWithValue("@ProductId", item.ProductId);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Stock Reduction Error: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
