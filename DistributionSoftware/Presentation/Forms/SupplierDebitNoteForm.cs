using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SupplierDebitNoteForm : Form
    {
        #region Private Fields
        
        private readonly ISupplierDebitNoteService _supplierDebitNoteService;
        private readonly IProductRepository _productRepository;
        private SqlConnection _connection;
        private string _connectionString;
        
        private SupplierDebitNote _currentDebitNote;
        private List<SupplierDebitNoteItem> _debitNoteItems;
        private bool _isEditMode;
        private bool _isLoading;
        
        #endregion
        
        #region Constructor
        
        public SupplierDebitNoteForm()
        {
            InitializeComponent();
            
            // Initialize services
            _supplierDebitNoteService = new SupplierDebitNoteService();
            _productRepository = new ProductRepository();
            
            // Initialize connection
            InitializeConnection();
            
            _debitNoteItems = new List<SupplierDebitNoteItem>();
            _isEditMode = false;
            _isLoading = false;
            
            InitializeForm();
        }
        
        public SupplierDebitNoteForm(int debitNoteId) : this()
        {
            _isEditMode = true;
            LoadDebitNote(debitNoteId);
        }
        
        #endregion
        
        #region Form Initialization
        
        private void InitializeForm()
        {
            // Set form properties
            this.Text = _isEditMode ? "Edit Supplier Debit Note" : "New Supplier Debit Note";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new Size(1000, 700);
            this.MinimumSize = new Size(1000, 700);
            
            // Initialize controls
            InitializeControls();
            LoadInitialData();
            SetupEventHandlers();
            
            // Set default values
            SetDefaultValues();
        }
        
        private void InitializeControls()
        {
            // Set up DataGridView
            dgvItems.AutoGenerateColumns = false;
            dgvItems.AllowUserToAddRows = false;
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItems.MultiSelect = false;
            
            // Set up ComboBoxes
            cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReason.DropDownStyle = ComboBoxStyle.DropDownList;
            
            // Set up DateTimePicker
            dtpDebitDate.Format = DateTimePickerFormat.Short;
            dtpDebitDate.Value = DateTime.Now;
            
            // Set up TextBoxes
            txtDebitNoteNo.ReadOnly = true;
            txtDebitNoteBarcode.ReadOnly = true;
            txtSubTotal.ReadOnly = true;
            txtTaxAmount.ReadOnly = true;
            txtTotalAmount.ReadOnly = true;
            
            // Set up Buttons
            btnAddItem.Enabled = true;
            btnRemoveItem.Enabled = false;
            btnSave.Enabled = true;
            btnApprove.Enabled = false;
            btnReject.Enabled = false;
            btnPrint.Enabled = false;
        }
        
        private void LoadInitialData()
        {
            try
            {
                _isLoading = true;
                
                // Load suppliers
                LoadSuppliers();
                
                // Load status options
                LoadStatusOptions();
                
                // Load reason options
                LoadReasonOptions();
                
                // Load reference data
                LoadReferenceData();
                
                // Generate new debit note number
                if (!_isEditMode)
                {
                    txtDebitNoteNo.Text = _supplierDebitNoteService.GenerateDebitNoteNumber();
                    txtDebitNoteBarcode.Text = _supplierDebitNoteService.GenerateDebitNoteBarcode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }
        
        private void InitializeConnection()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DistributionConnection"]?.ConnectionString;
            if (string.IsNullOrEmpty(_connectionString))
            {
                MessageBox.Show("Database connection string not found.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _connection = new SqlConnection(_connectionString);
        }
        
        private void LoadSuppliers()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = "SELECT SupplierId, SupplierName, SupplierCode FROM Suppliers ORDER BY SupplierName";
                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        var suppliers = new List<dynamic>();
                        while (reader.Read())
                        {
                            suppliers.Add(new { 
                                SupplierId = reader.GetInt32(reader.GetOrdinal("SupplierId")),
                                SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
                                SupplierCode = reader.GetString(reader.GetOrdinal("SupplierCode"))
                            });
                        }
                        
                        if (suppliers.Count > 0)
                        {
                            cmbSupplier.DataSource = suppliers;
                            cmbSupplier.DisplayMember = "SupplierName";
                            cmbSupplier.ValueMember = "SupplierId";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadStatusOptions()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new string[] { "DRAFT", "PENDING", "APPROVED", "REJECTED", "CANCELLED" });
        }
        
        private void LoadReasonOptions()
        {
            cmbReason.Items.Clear();
            cmbReason.Items.AddRange(new string[] 
            {
                "Damaged Goods Return",
                "Price Adjustment",
                "Quality Issues",
                "Damages in Transit",
                "Short Deliveries",
                "Service Charges",
                "Wrong Item Delivered",
                "Expired Goods",
                "Other"
            });
        }
        
        private void LoadReferenceData()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Load recent purchase invoices for the selected supplier
                    try
                    {
                        var invoiceQuery = "SELECT TOP 50 PurchaseInvoiceId as InvoiceId, InvoiceNumber as InvoiceNo, TotalAmount as InvoiceAmount FROM PurchaseInvoices WHERE SupplierId = @SupplierId ORDER BY CreatedDate DESC";
                        using (var command = new SqlCommand(invoiceQuery, connection))
                        {
                            command.Parameters.AddWithValue("@SupplierId", cmbSupplier.SelectedValue ?? (object)DBNull.Value);
                            using (var reader = command.ExecuteReader())
                            {
                                var dataTable = new DataTable();
                                dataTable.Load(reader);
                                
                                if (dataTable.Rows.Count > 0)
                                {
                                    cmbOriginalInvoice.DataSource = dataTable;
                                    cmbOriginalInvoice.DisplayMember = "InvoiceNo";
                                    cmbOriginalInvoice.ValueMember = "InvoiceId";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Table might not exist yet, show empty dropdown
                        cmbOriginalInvoice.DataSource = new DataTable();
                    }
                    
                    // Load recent purchases
                    try
                    {
                        var purchaseQuery = "SELECT TOP 50 PurchaseInvoiceId as PurchaseId, InvoiceNumber as PurchaseNo FROM PurchaseInvoices ORDER BY CreatedDate DESC";
                        using (var command = new SqlCommand(purchaseQuery, connection))
                        using (var reader = command.ExecuteReader())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            
                            if (dataTable.Rows.Count > 0)
                            {
                                cmbReferencePurchase.DataSource = dataTable;
                                cmbReferencePurchase.DisplayMember = "PurchaseNo";
                                cmbReferencePurchase.ValueMember = "PurchaseId";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Table might not exist yet, show empty dropdown
                        cmbReferencePurchase.DataSource = new DataTable();
                    }
                    
                    // Load recent GRNs
                    try
                    {
                        var grnQuery = "SELECT TOP 50 GRNId, GRNNumber as GRNNo FROM GRN ORDER BY CreatedDate DESC";
                        using (var command = new SqlCommand(grnQuery, connection))
                        using (var reader = command.ExecuteReader())
                        {
                            var dataTable = new DataTable();
                            dataTable.Load(reader);
                            
                            if (dataTable.Rows.Count > 0)
                            {
                                cmbReferenceGRN.DataSource = dataTable;
                                cmbReferenceGRN.DisplayMember = "GRNNo";
                                cmbReferenceGRN.ValueMember = "GRNId";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Table might not exist yet, show empty dropdown
                        cmbReferenceGRN.DataSource = new DataTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reference data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void SetDefaultValues()
        {
            if (!_isEditMode)
            {
                cmbStatus.SelectedItem = "DRAFT";
                cmbReason.SelectedItem = "Damaged Goods Return";
                dtpDebitDate.Value = DateTime.Now;
            }
        }
        
        #endregion
        
        #region Event Handlers
        
        private void SetupEventHandlers()
        {
            // Form events
            this.Load += SupplierDebitNoteForm_Load;
            this.FormClosing += SupplierDebitNoteForm_FormClosing;
            
            // Button events
            btnAddItem.Click += BtnAddItem_Click;
            btnRemoveItem.Click += BtnRemoveItem_Click;
            btnSave.Click += BtnSave_Click;
            btnApprove.Click += BtnApprove_Click;
            btnReject.Click += BtnReject_Click;
            btnPrint.Click += BtnPrint_Click;
            btnCancel.Click += BtnCancel_Click;
            
            // DataGridView events
            dgvItems.SelectionChanged += DgvItems_SelectionChanged;
            dgvItems.CellEndEdit += DgvItems_CellEndEdit;
            
            // ComboBox events
            cmbSupplier.SelectedIndexChanged += CmbSupplier_SelectedIndexChanged;
            cmbStatus.SelectedIndexChanged += CmbStatus_SelectedIndexChanged;
            cmbOriginalInvoice.SelectedIndexChanged += cmbOriginalInvoice_SelectedIndexChanged;
            cmbReferencePurchase.SelectedIndexChanged += CmbReferencePurchase_SelectedIndexChanged;
            cmbReferenceGRN.SelectedIndexChanged += CmbReferenceGRN_SelectedIndexChanged;
        }
        
        private void SupplierDebitNoteForm_Load(object sender, EventArgs e)
        {
            RefreshItemsGrid();
        }
        
        private void SupplierDebitNoteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (HasUnsavedChanges())
            {
                var result = MessageBox.Show("You have unsaved changes. Do you want to save before closing?", 
                    "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    SaveDebitNote();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
        
        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            AddNewItem();
        }
        
        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            RemoveSelectedItem();
        }
        
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveDebitNote();
        }
        
        private void BtnApprove_Click(object sender, EventArgs e)
        {
            ApproveDebitNote();
        }
        
        private void BtnReject_Click(object sender, EventArgs e)
        {
            RejectDebitNote();
        }
        
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrintDebitNote();
        }
        
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void DgvItems_SelectionChanged(object sender, EventArgs e)
        {
            btnRemoveItem.Enabled = dgvItems.SelectedRows.Count > 0;
        }
        
        private void DgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < _debitNoteItems.Count)
            {
                var item = _debitNoteItems[e.RowIndex];
                var cell = dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex];
                
                // Update item based on column
                switch (e.ColumnIndex)
                {
                    case 2: // Quantity
                        if (decimal.TryParse(cell.Value?.ToString(), out decimal quantity))
                        {
                            item.Quantity = quantity;
                            RecalculateItemTotal(item);
                        }
                        break;
                    case 3: // Unit Price
                        if (decimal.TryParse(cell.Value?.ToString(), out decimal unitPrice))
                        {
                            item.UnitPrice = unitPrice;
                            RecalculateItemTotal(item);
                        }
                        break;
                    case 4: // Tax Percentage
                        if (decimal.TryParse(cell.Value?.ToString(), out decimal taxPercentage))
                        {
                            item.TaxPercentage = taxPercentage;
                            RecalculateItemTotal(item);
                        }
                        break;
                    case 6: // Discount Percentage
                        if (decimal.TryParse(cell.Value?.ToString(), out decimal discountPercentage))
                        {
                            item.DiscountPercentage = discountPercentage;
                            RecalculateItemTotal(item);
                        }
                        break;
                    case 8: // Reason
                        item.Reason = cell.Value?.ToString();
                        break;
                }
                
                RefreshItemsGrid();
                CalculateTotals();
            }
        }
        
        private void CmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isLoading && cmbSupplier.SelectedValue != null)
            {
                LoadSupplierDetails();
                LoadReferenceData(); // Reload invoices for the selected supplier
            }
        }
        
        private void CmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }
        
        private void CmbReferencePurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbReferencePurchase.SelectedValue != null && !_isLoading)
                {
                    // Get purchase ID
                    int purchaseId;
                    if (cmbReferencePurchase.SelectedValue is DataRowView rowView)
                    {
                        purchaseId = Convert.ToInt32(rowView["PurchaseId"]);
                    }
                    else
                    {
                        purchaseId = Convert.ToInt32(cmbReferencePurchase.SelectedValue);
                    }
                    
                    // Auto-populate from purchase
                    AutoPopulateFromPurchase(purchaseId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void CmbReferenceGRN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbReferenceGRN.SelectedValue != null && !_isLoading)
                {
                    // Get GRN ID
                    int grnId;
                    if (cmbReferenceGRN.SelectedValue is DataRowView rowView)
                    {
                        grnId = Convert.ToInt32(rowView["GRNId"]);
                    }
                    else
                    {
                        grnId = Convert.ToInt32(cmbReferenceGRN.SelectedValue);
                    }
                    
                    // Auto-populate from GRN
                    AutoPopulateFromGRN(grnId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading GRN details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
        
        #region Data Loading
        
        private void LoadDebitNote(int debitNoteId)
        {
            try
            {
                _currentDebitNote = _supplierDebitNoteService.GetSupplierDebitNoteById(debitNoteId);
                if (_currentDebitNote != null)
                {
                    _debitNoteItems = _currentDebitNote.Items.ToList();
                    PopulateForm();
                }
                else
                {
                    MessageBox.Show("Debit note not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading debit note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void PopulateForm()
        {
            try
            {
                _isLoading = true;
                
                // Populate basic fields
                txtDebitNoteNo.Text = _currentDebitNote.DebitNoteNo;
                txtDebitNoteBarcode.Text = _currentDebitNote.DebitNoteBarcode;
                cmbSupplier.SelectedValue = _currentDebitNote.SupplierId;
                dtpDebitDate.Value = _currentDebitNote.DebitDate;
                cmbReason.Text = _currentDebitNote.Reason;
                cmbStatus.SelectedItem = _currentDebitNote.Status;
                txtRemarks.Text = _currentDebitNote.Remarks;
                
                // Populate reference fields
                if (_currentDebitNote.ReferencePurchaseId.HasValue)
                {
                    cmbReferencePurchase.SelectedValue = _currentDebitNote.ReferencePurchaseId.Value;
                }
                if (_currentDebitNote.ReferenceGRNId.HasValue)
                {
                    cmbReferenceGRN.SelectedValue = _currentDebitNote.ReferenceGRNId.Value;
                }
                
                // Populate totals
                txtSubTotal.Text = _currentDebitNote.SubTotal.ToString("N2");
                txtTaxAmount.Text = _currentDebitNote.TaxAmount.ToString("N2");
                txtTotalAmount.Text = _currentDebitNote.TotalAmount.ToString("N2");
                
                // Refresh items grid
                RefreshItemsGrid();
                
                // Update button states
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }
        
        private void LoadSupplierDetails()
        {
            try
            {
                if (cmbSupplier.SelectedValue != null)
                {
                    int supplierId;
                    if (cmbSupplier.SelectedValue is DataRowView rowView)
                    {
                        supplierId = Convert.ToInt32(rowView["SupplierId"]);
                    }
                    else
                    {
                        supplierId = Convert.ToInt32(cmbSupplier.SelectedValue);
                    }
                    // Update supplier details in UI if needed
                    // This could include loading supplier's recent purchases, GRNs, etc.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
        
        #region Item Management
        
        private void AddNewItem()
        {
            try
            {
                var item = new SupplierDebitNoteItem
                {
                    ProductId = 0,
                    ProductCode = "",
                    ProductName = "",
                    Quantity = 1,
                    UnitPrice = 0,
                    TaxPercentage = 17, // Default GST rate
                    TaxAmount = 0,
                    DiscountPercentage = 0,
                    DiscountAmount = 0,
                    TotalAmount = 0,
                    Reason = ""
                };
                
                _debitNoteItems.Add(item);
                RefreshItemsGrid();
                
                // Select the new row
                if (dgvItems.Rows.Count > 0)
                {
                    dgvItems.Rows[dgvItems.Rows.Count - 1].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void RemoveSelectedItem()
        {
            try
            {
                if (dgvItems.SelectedRows.Count > 0)
                {
                    var selectedIndex = dgvItems.SelectedRows[0].Index;
                    if (selectedIndex >= 0 && selectedIndex < _debitNoteItems.Count)
                    {
                        _debitNoteItems.RemoveAt(selectedIndex);
                        RefreshItemsGrid();
                        CalculateTotals();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void RecalculateItemTotal(SupplierDebitNoteItem item)
        {
            // Calculate line total
            var lineTotal = item.Quantity * item.UnitPrice;
            
            // Calculate discount
            item.DiscountAmount = lineTotal * (item.DiscountPercentage / 100);
            
            // Calculate taxable amount
            var taxableAmount = lineTotal - item.DiscountAmount;
            
            // Calculate tax
            item.TaxAmount = taxableAmount * (item.TaxPercentage / 100);
            
            // Calculate total
            item.TotalAmount = taxableAmount + item.TaxAmount;
        }
        
        private void RefreshItemsGrid()
        {
            try
            {
                dgvItems.DataSource = null;
                dgvItems.DataSource = _debitNoteItems;
                
                // Configure columns if not already done
                if (dgvItems.Columns.Count == 0)
                {
                    ConfigureItemsGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing items grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ConfigureItemsGrid()
        {
            // Clear existing columns
            dgvItems.Columns.Clear();
            
            // Add columns
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductCode",
                HeaderText = "Product Code",
                DataPropertyName = "ProductCode",
                Width = 100
            });
            
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                Width = 200
            });
            
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "Quantity",
                DataPropertyName = "Quantity",
                Width = 80
            });
            
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnitPrice",
                HeaderText = "Unit Price",
                DataPropertyName = "UnitPrice",
                Width = 100
            });
            
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaxPercentage",
                HeaderText = "Tax %",
                DataPropertyName = "TaxPercentage",
                Width = 60
            });
            
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaxAmount",
                HeaderText = "Tax Amount",
                DataPropertyName = "TaxAmount",
                Width = 100,
                ReadOnly = true
            });
            
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiscountPercentage",
                HeaderText = "Disc %",
                DataPropertyName = "DiscountPercentage",
                Width = 60
            });
            
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiscountAmount",
                HeaderText = "Discount",
                DataPropertyName = "DiscountAmount",
                Width = 100,
                ReadOnly = true
            });
            
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalAmount",
                HeaderText = "Total",
                DataPropertyName = "TotalAmount",
                Width = 100,
                ReadOnly = true
            });
            
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Reason",
                HeaderText = "Reason",
                DataPropertyName = "Reason",
                Width = 150
            });
        }
        
        #endregion
        
        #region Calculations
        
        private void CalculateTotals()
        {
            try
            {
                if (_currentDebitNote == null)
                {
                    _currentDebitNote = new SupplierDebitNote();
                }
                
                _currentDebitNote.Items = _debitNoteItems;
                _supplierDebitNoteService.CalculateDebitNoteTotals(_currentDebitNote);
                
                // Update UI
                txtSubTotal.Text = _currentDebitNote.SubTotal.ToString("N2");
                txtTaxAmount.Text = _currentDebitNote.TaxAmount.ToString("N2");
                txtTotalAmount.Text = _currentDebitNote.TotalAmount.ToString("N2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating totals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
        
        #region Save Operations
        
        private void SaveDebitNote()
        {
            try
            {
                if (!ValidateForm())
                {
                    return;
                }
                
                // Prepare debit note
                PrepareDebitNote();
                
                // Save debit note
                if (_isEditMode)
                {
                    _supplierDebitNoteService.UpdateSupplierDebitNote(_currentDebitNote);
                    MessageBox.Show("Debit note updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var debitNoteId = _supplierDebitNoteService.CreateSupplierDebitNote(_currentDebitNote);
                    _currentDebitNote.DebitNoteId = debitNoteId;
                    _isEditMode = true;
                    MessageBox.Show("Debit note created successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                // Update button states
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving debit note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void PrepareDebitNote()
        {
            if (_currentDebitNote == null)
            {
                _currentDebitNote = new SupplierDebitNote();
            }
            
            // Basic information
            _currentDebitNote.DebitNoteNo = txtDebitNoteNo.Text;
            _currentDebitNote.DebitNoteBarcode = txtDebitNoteBarcode.Text;
            if (cmbSupplier.SelectedValue is DataRowView supplierRowView)
            {
                _currentDebitNote.SupplierId = Convert.ToInt32(supplierRowView["SupplierId"]);
            }
            else
            {
                _currentDebitNote.SupplierId = Convert.ToInt32(cmbSupplier.SelectedValue);
            }
            _currentDebitNote.DebitDate = dtpDebitDate.Value;
            _currentDebitNote.Reason = cmbReason.Text;
            _currentDebitNote.Status = cmbStatus.SelectedItem?.ToString() ?? "DRAFT";
            _currentDebitNote.Remarks = txtRemarks.Text;
            
            // Reference information
            if (cmbReferencePurchase.SelectedValue is DataRowView purchaseRowView)
            {
                _currentDebitNote.ReferencePurchaseId = Convert.ToInt32(purchaseRowView["PurchaseId"]);
            }
            else
            {
                _currentDebitNote.ReferencePurchaseId = cmbReferencePurchase.SelectedValue as int?;
            }
            
            if (cmbReferenceGRN.SelectedValue is DataRowView grnRowView)
            {
                _currentDebitNote.ReferenceGRNId = Convert.ToInt32(grnRowView["GRNId"]);
            }
            else
            {
                _currentDebitNote.ReferenceGRNId = cmbReferenceGRN.SelectedValue as int?;
            }
            
            // Items
            _currentDebitNote.Items = _debitNoteItems;
            
            // Calculate totals
            _supplierDebitNoteService.CalculateDebitNoteTotals(_currentDebitNote);
            
            // Set audit fields
            if (!_isEditMode)
            {
                _currentDebitNote.CreatedBy = UserSession.CurrentUserId;
                _currentDebitNote.CreatedDate = DateTime.Now;
            }
            else
            {
                _currentDebitNote.ModifiedBy = UserSession.CurrentUserId;
                _currentDebitNote.ModifiedDate = DateTime.Now;
            }
        }
        
        private bool ValidateForm()
        {
            // Validate supplier
            if (cmbSupplier.SelectedValue == null)
            {
                MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSupplier.Focus();
                return false;
            }
            
            // Validate reason
            if (string.IsNullOrEmpty(cmbReason.Text))
            {
                MessageBox.Show("Please select a reason.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbReason.Focus();
                return false;
            }
            
            // Validate items
            if (_debitNoteItems == null || !_debitNoteItems.Any())
            {
                MessageBox.Show("Please select a Purchase Invoice to auto-populate items.\n\nItems will be automatically loaded from the selected invoice.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            // Validate each item
            foreach (var item in _debitNoteItems)
            {
                if (item.ProductId <= 0)
                {
                    MessageBox.Show("Please select a product for all items.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                if (item.Quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity for all items.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                if (item.UnitPrice < 0)
                {
                    MessageBox.Show("Please enter a valid unit price for all items.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            
            return true;
        }
        
        #endregion
        
        #region Approval Operations
        
        private void ApproveDebitNote()
        {
            try
            {
                if (_currentDebitNote == null || _currentDebitNote.DebitNoteId <= 0)
                {
                    MessageBox.Show("Please save the debit note first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                var result = MessageBox.Show("Are you sure you want to approve this debit note?", 
                    "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    var approved = _supplierDebitNoteService.ApproveDebitNote(_currentDebitNote.DebitNoteId, UserSession.CurrentUserId);
                    if (approved)
                    {
                        MessageBox.Show("Debit note approved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDebitNote(_currentDebitNote.DebitNoteId); // Reload to get updated status
                    }
                    else
                    {
                        MessageBox.Show("Failed to approve debit note.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving debit note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void RejectDebitNote()
        {
            try
            {
                if (_currentDebitNote == null || _currentDebitNote.DebitNoteId <= 0)
                {
                    MessageBox.Show("Please save the debit note first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                var rejectionReason = ShowInputDialog("Please enter the reason for rejection:", "Rejection Reason");
                
                if (!string.IsNullOrEmpty(rejectionReason))
                {
                    var rejected = _supplierDebitNoteService.RejectDebitNote(_currentDebitNote.DebitNoteId, UserSession.CurrentUserId, rejectionReason);
                    if (rejected)
                    {
                        MessageBox.Show("Debit note rejected successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDebitNote(_currentDebitNote.DebitNoteId); // Reload to get updated status
                    }
                    else
                    {
                        MessageBox.Show("Failed to reject debit note.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rejecting debit note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
        
        #region Print Operations
        
        private void PrintDebitNote()
        {
            try
            {
                if (_currentDebitNote == null || _currentDebitNote.DebitNoteId <= 0)
                {
                    MessageBox.Show("Please save the debit note first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                var printout = _supplierDebitNoteService.GenerateDebitNotePrintout(_currentDebitNote);
                
                // Show print preview or send to printer
                var printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    // Print logic here
                    MessageBox.Show("Print functionality will be implemented.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing debit note: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        #endregion
        
        #region Helper Methods
        
        private void UpdateButtonStates()
        {
            var status = cmbStatus.SelectedItem?.ToString() ?? "DRAFT";
            
            btnSave.Enabled = status == "DRAFT" || status == "PENDING";
            btnApprove.Enabled = status == "PENDING";
            btnReject.Enabled = status == "PENDING";
            btnPrint.Enabled = status == "APPROVED";
        }
        
        private bool HasUnsavedChanges()
        {
            // Check if there are unsaved changes
            // This is a simplified implementation
            return _debitNoteItems.Any() && (_currentDebitNote == null || _currentDebitNote.DebitNoteId <= 0);
        }
        
        private string ShowInputDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };
            
            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 350 };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 350 };
            Button confirmation = new Button() { Text = "OK", Left = 200, Width = 80, Top = 80, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Cancel", Left = 290, Width = 80, Top = 80, DialogResult = DialogResult.Cancel };
            
            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };
            
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;
            
            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
        
        private void cmbOriginalInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbOriginalInvoice.SelectedValue != null && !_isLoading)
                {
                    // Get invoice ID
                    int invoiceId;
                    if (cmbOriginalInvoice.SelectedValue is DataRowView rowView)
                    {
                        invoiceId = Convert.ToInt32(rowView["InvoiceId"]);
                    }
                    else
                    {
                        invoiceId = Convert.ToInt32(cmbOriginalInvoice.SelectedValue);
                    }
                    
                    // Auto-populate all related data from the invoice
                    AutoPopulateFromInvoice(invoiceId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoice details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AutoPopulateFromInvoice(int invoiceId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Get comprehensive purchase invoice details including supplier info
                    var invoiceQuery = @"
                        SELECT 
                            pi.PurchaseInvoiceId,
                            pi.InvoiceNumber,
                            pi.CreatedDate as InvoiceDate,
                            pi.TotalAmount,
                            pi.SupplierId,
                            s.SupplierName,
                            s.ContactPerson,
                            s.Phone,
                            s.Email,
                            s.Address
                        FROM PurchaseInvoices pi
                        LEFT JOIN Suppliers s ON pi.SupplierId = s.SupplierId
                        WHERE pi.PurchaseInvoiceId = @InvoiceId";
                    
                    using (var command = new SqlCommand(invoiceQuery, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceId", invoiceId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Set loading flag to prevent recursive events
                                _isLoading = true;
                                
                                try
                                {
                                    // Auto-populate invoice amount
                                    txtOriginalInvoiceAmount.Text = Convert.ToDecimal(reader["TotalAmount"]).ToString("N2");
                                    
                                    // Auto-populate supplier information
                                    int supplierId = Convert.ToInt32(reader["SupplierId"]);
                                    string supplierName = reader["SupplierName"]?.ToString();
                                    
                                    // Set supplier in dropdown
                                    if (!string.IsNullOrEmpty(supplierName))
                                    {
                                        // Find and select the supplier in the dropdown
                                        for (int i = 0; i < cmbSupplier.Items.Count; i++)
                                        {
                                            if (cmbSupplier.Items[i] is DataRowView supplierRow && 
                                                Convert.ToInt32(supplierRow["SupplierId"]) == supplierId)
                                            {
                                                cmbSupplier.SelectedIndex = i;
                                                break;
                                            }
                                        }
                                    }
                                    
                                    // Auto-populate debit date with invoice date
                                    if (reader["InvoiceDate"] != DBNull.Value)
                                    {
                                        dtpDebitDate.Value = Convert.ToDateTime(reader["InvoiceDate"]);
                                    }
                                    
                                    // Auto-populate debit note number with reference to invoice
                                    if (string.IsNullOrEmpty(txtDebitNoteNo.Text))
                                    {
                                        string invoiceNo = reader["InvoiceNumber"]?.ToString();
                                        txtDebitNoteNo.Text = $"DN-{invoiceNo}-{DateTime.Now:yyyyMMdd}";
                                    }
                                    
                                    // Auto-populate barcode with reference to invoice
                                    if (string.IsNullOrEmpty(txtDebitNoteBarcode.Text))
                                    {
                                        string invoiceNo = reader["InvoiceNumber"]?.ToString();
                                        txtDebitNoteBarcode.Text = $"DN{invoiceNo}{DateTime.Now:yyyyMMddHHmm}";
                                    }
                                    
                                    // Load related purchases and GRNs for this supplier
                                    LoadRelatedPurchasesAndGRNs(supplierId);
                                    
                                    // Auto-populate items from the purchase invoice
                                    LoadInvoiceItems(invoiceId);
                                    
                                    // Show success message
                                    MessageBox.Show($"Auto-populated from Invoice: {reader["InvoiceNumber"]}\n" +
                                                  $"Supplier: {supplierName}\n" +
                                                  $"Amount: {Convert.ToDecimal(reader["TotalAmount"]):N2}\n" +
                                                  $"Items: {_debitNoteItems.Count} products loaded\n\n" +
                                                  $"Note: Product availability has been checked and any discontinued products are marked in the reason field.", 
                                                  "Smart Auto-Population", 
                                                  MessageBoxButtons.OK, 
                                                  MessageBoxIcon.Information);
                                }
                                finally
                                {
                                    _isLoading = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error auto-populating from invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadInvoiceItems(int invoiceId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Clear existing items
                    _debitNoteItems.Clear();
                    
                    // Get all items from the purchase invoice with product availability check
                    var itemsQuery = @"
                        SELECT 
                            pid.ProductId,
                            p.ProductCode,
                            p.ProductName,
                            pid.Quantity,
                            pid.UnitPrice,
                            17.0 as TaxPercentage,
                            pid.TaxAmount,
                            0.0 as DiscountPercentage,
                            pid.DiscountAmount,
                            pid.TotalAmount,
                            CASE 
                                WHEN p.IsActive = 1 THEN 'Available'
                                ELSE 'Discontinued'
                            END as ProductStatus
                        FROM PurchaseInvoiceDetails pid
                        INNER JOIN Products p ON pid.ProductId = p.ProductId
                        WHERE pid.PurchaseInvoiceId = @InvoiceId
                        ORDER BY p.ProductName";
                    
                    using (var command = new SqlCommand(itemsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceId", invoiceId);
                        using (var reader = command.ExecuteReader())
                        {
                            int availableCount = 0;
                            int discontinuedCount = 0;
                            
                            while (reader.Read())
                            {
                                string productStatus = reader["ProductStatus"]?.ToString() ?? "Unknown";
                                string reason = "Auto-populated from Purchase Invoice";
                                
                                // Add status information to reason if product is discontinued
                                if (productStatus == "Discontinued")
                                {
                                    reason += " (Product Discontinued)";
                                    discontinuedCount++;
                                }
                                else
                                {
                                    availableCount++;
                                }
                                
                                var item = new SupplierDebitNoteItem
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    ProductCode = reader["ProductCode"]?.ToString() ?? "",
                                    ProductName = reader["ProductName"]?.ToString() ?? "",
                                    Quantity = Convert.ToDecimal(reader["Quantity"]),
                                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                    TaxPercentage = Convert.ToDecimal(reader["TaxPercentage"]),
                                    TaxAmount = Convert.ToDecimal(reader["TaxAmount"]),
                                    DiscountPercentage = Convert.ToDecimal(reader["DiscountPercentage"]),
                                    DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                    Reason = reason
                                };
                                
                                _debitNoteItems.Add(item);
                            }
                            
                            // Show warning if any products are discontinued
                            if (discontinuedCount > 0)
                            {
                                MessageBox.Show($"Warning: {discontinuedCount} discontinued product(s) found in this invoice.\n" +
                                              $"Available products: {availableCount}\n" +
                                              $"Discontinued products: {discontinuedCount}\n\n" +
                                              $"Please review the items before proceeding.", 
                                              "Product Status Warning", 
                                              MessageBoxButtons.OK, 
                                              MessageBoxIcon.Warning);
                            }
                        }
                    }
                    
                    // Refresh the items grid
                    RefreshItemsGrid();
                    CalculateTotals();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoice items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadRelatedPurchasesAndGRNs(int supplierId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Load recent purchases for this supplier
                    var purchaseQuery = @"
                        SELECT TOP 20 
                            PurchaseInvoiceId as PurchaseId, 
                            InvoiceNumber as PurchaseNo, 
                            TotalAmount as PurchaseAmount,
                            CreatedDate as PurchaseDate
                        FROM PurchaseInvoices 
                        WHERE SupplierId = @SupplierId 
                        ORDER BY CreatedDate DESC";
                    
                    using (var command = new SqlCommand(purchaseQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
                        using (var reader = command.ExecuteReader())
                        {
                            var purchaseTable = new DataTable();
                            purchaseTable.Load(reader);
                            
                            if (purchaseTable.Rows.Count > 0)
                            {
                                cmbReferencePurchase.DataSource = purchaseTable;
                                cmbReferencePurchase.DisplayMember = "PurchaseNo";
                                cmbReferencePurchase.ValueMember = "PurchaseId";
                            }
                        }
                    }
                    
                    // Load recent GRNs for this supplier
                    var grnQuery = @"
                        SELECT TOP 20 
                            GRNId, 
                            GRNNumber as GRNNo, 
                            TotalAmount as GRNAmount,
                            CreatedDate as GRNDate
                        FROM GRN 
                        WHERE SupplierId = @SupplierId 
                        ORDER BY CreatedDate DESC";
                    
                    using (var command = new SqlCommand(grnQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
                        using (var reader = command.ExecuteReader())
                        {
                            var grnTable = new DataTable();
                            grnTable.Load(reader);
                            
                            if (grnTable.Rows.Count > 0)
                            {
                                cmbReferenceGRN.DataSource = grnTable;
                                cmbReferenceGRN.DisplayMember = "GRNNo";
                                cmbReferenceGRN.ValueMember = "GRNId";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Silently handle errors - related data is optional
                System.Diagnostics.Debug.WriteLine($"Error loading related data: {ex.Message}");
            }
        }
        
        private void AutoPopulateFromPurchase(int purchaseId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Get comprehensive purchase details including supplier info
                    var purchaseQuery = @"
                        SELECT 
                            pi.PurchaseInvoiceId,
                            pi.InvoiceNumber,
                            pi.CreatedDate as PurchaseDate,
                            pi.TotalAmount,
                            pi.SupplierId,
                            s.SupplierName,
                            s.ContactPerson,
                            s.Phone,
                            s.Email,
                            s.Address
                        FROM PurchaseInvoices pi
                        LEFT JOIN Suppliers s ON pi.SupplierId = s.SupplierId
                        WHERE pi.PurchaseInvoiceId = @PurchaseId";
                    
                    using (var command = new SqlCommand(purchaseQuery, connection))
                    {
                        command.Parameters.AddWithValue("@PurchaseId", purchaseId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _isLoading = true;
                                
                                try
                                {
                                    // Auto-populate supplier information
                                    int supplierId = Convert.ToInt32(reader["SupplierId"]);
                                    string supplierName = reader["SupplierName"]?.ToString();
                                    
                                    // Set supplier in dropdown
                                    if (!string.IsNullOrEmpty(supplierName))
                                    {
                                        for (int i = 0; i < cmbSupplier.Items.Count; i++)
                                        {
                                            if (cmbSupplier.Items[i] is DataRowView supplierRow && 
                                                Convert.ToInt32(supplierRow["SupplierId"]) == supplierId)
                                            {
                                                cmbSupplier.SelectedIndex = i;
                                                break;
                                            }
                                        }
                                    }
                                    
                                    // Auto-populate debit date with purchase date
                                    if (reader["PurchaseDate"] != DBNull.Value)
                                    {
                                        dtpDebitDate.Value = Convert.ToDateTime(reader["PurchaseDate"]);
                                    }
                                    
                                    // Auto-populate debit note number with reference to purchase
                                    if (string.IsNullOrEmpty(txtDebitNoteNo.Text))
                                    {
                                        string purchaseNo = reader["InvoiceNumber"]?.ToString();
                                        txtDebitNoteNo.Text = $"DN-{purchaseNo}-{DateTime.Now:yyyyMMdd}";
                                    }
                                    
                                    // Auto-populate barcode with reference to purchase
                                    if (string.IsNullOrEmpty(txtDebitNoteBarcode.Text))
                                    {
                                        string purchaseNo = reader["InvoiceNumber"]?.ToString();
                                        txtDebitNoteBarcode.Text = $"DN{purchaseNo}{DateTime.Now:yyyyMMddHHmm}";
                                    }
                                    
                                    // Load related invoices and GRNs for this supplier
                                    LoadRelatedInvoicesAndGRNs(supplierId);
                                    
                                    MessageBox.Show($"Auto-populated from Purchase: {reader["InvoiceNumber"]}\n" +
                                                  $"Supplier: {supplierName}\n" +
                                                  $"Amount: {Convert.ToDecimal(reader["TotalAmount"]):N2}", 
                                                  "Smart Auto-Population", 
                                                  MessageBoxButtons.OK, 
                                                  MessageBoxIcon.Information);
                                }
                                finally
                                {
                                    _isLoading = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error auto-populating from purchase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void AutoPopulateFromGRN(int grnId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Get comprehensive GRN details including supplier info
                    var grnQuery = @"
                        SELECT 
                            g.GRNId,
                            g.GRNNumber,
                            g.CreatedDate as GRNDate,
                            g.TotalAmount,
                            g.SupplierId,
                            s.SupplierName,
                            s.ContactPerson,
                            s.Phone,
                            s.Email,
                            s.Address
                        FROM GRN g
                        LEFT JOIN Suppliers s ON g.SupplierId = s.SupplierId
                        WHERE g.GRNId = @GRNId";
                    
                    using (var command = new SqlCommand(grnQuery, connection))
                    {
                        command.Parameters.AddWithValue("@GRNId", grnId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _isLoading = true;
                                
                                try
                                {
                                    // Auto-populate supplier information
                                    int supplierId = Convert.ToInt32(reader["SupplierId"]);
                                    string supplierName = reader["SupplierName"]?.ToString();
                                    
                                    // Set supplier in dropdown
                                    if (!string.IsNullOrEmpty(supplierName))
                                    {
                                        for (int i = 0; i < cmbSupplier.Items.Count; i++)
                                        {
                                            if (cmbSupplier.Items[i] is DataRowView supplierRow && 
                                                Convert.ToInt32(supplierRow["SupplierId"]) == supplierId)
                                            {
                                                cmbSupplier.SelectedIndex = i;
                                                break;
                                            }
                                        }
                                    }
                                    
                                    // Auto-populate debit date with GRN date
                                    if (reader["GRNDate"] != DBNull.Value)
                                    {
                                        dtpDebitDate.Value = Convert.ToDateTime(reader["GRNDate"]);
                                    }
                                    
                                    // Auto-populate debit note number with reference to GRN
                                    if (string.IsNullOrEmpty(txtDebitNoteNo.Text))
                                    {
                                        string grnNo = reader["GRNNumber"]?.ToString();
                                        txtDebitNoteNo.Text = $"DN-{grnNo}-{DateTime.Now:yyyyMMdd}";
                                    }
                                    
                                    // Auto-populate barcode with reference to GRN
                                    if (string.IsNullOrEmpty(txtDebitNoteBarcode.Text))
                                    {
                                        string grnNo = reader["GRNNumber"]?.ToString();
                                        txtDebitNoteBarcode.Text = $"DN{grnNo}{DateTime.Now:yyyyMMddHHmm}";
                                    }
                                    
                                    // Load related invoices and purchases for this supplier
                                    LoadRelatedInvoicesAndPurchases(supplierId);
                                    
                                    MessageBox.Show($"Auto-populated from GRN: {reader["GRNNumber"]}\n" +
                                                  $"Supplier: {supplierName}\n" +
                                                  $"Amount: {Convert.ToDecimal(reader["TotalAmount"]):N2}", 
                                                  "Smart Auto-Population", 
                                                  MessageBoxButtons.OK, 
                                                  MessageBoxIcon.Information);
                                }
                                finally
                                {
                                    _isLoading = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error auto-populating from GRN: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadRelatedInvoicesAndGRNs(int supplierId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Load recent purchase invoices for this supplier
                    var invoiceQuery = @"
                        SELECT TOP 20 
                            PurchaseInvoiceId as InvoiceId, 
                            InvoiceNumber as InvoiceNo, 
                            TotalAmount as InvoiceAmount,
                            CreatedDate as InvoiceDate
                        FROM PurchaseInvoices 
                        WHERE SupplierId = @SupplierId 
                        ORDER BY CreatedDate DESC";
                    
                    using (var command = new SqlCommand(invoiceQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
                        using (var reader = command.ExecuteReader())
                        {
                            var invoiceTable = new DataTable();
                            invoiceTable.Load(reader);
                            
                            if (invoiceTable.Rows.Count > 0)
                            {
                                cmbOriginalInvoice.DataSource = invoiceTable;
                                cmbOriginalInvoice.DisplayMember = "InvoiceNo";
                                cmbOriginalInvoice.ValueMember = "InvoiceId";
                            }
                        }
                    }
                    
                    // Load recent GRNs for this supplier
                    var grnQuery = @"
                        SELECT TOP 20 
                            GRNId, 
                            GRNNumber as GRNNo, 
                            TotalAmount as GRNAmount,
                            CreatedDate as GRNDate
                        FROM GRN 
                        WHERE SupplierId = @SupplierId 
                        ORDER BY CreatedDate DESC";
                    
                    using (var command = new SqlCommand(grnQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
                        using (var reader = command.ExecuteReader())
                        {
                            var grnTable = new DataTable();
                            grnTable.Load(reader);
                            
                            if (grnTable.Rows.Count > 0)
                            {
                                cmbReferenceGRN.DataSource = grnTable;
                                cmbReferenceGRN.DisplayMember = "GRNNo";
                                cmbReferenceGRN.ValueMember = "GRNId";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading related data: {ex.Message}");
            }
        }
        
        private void LoadRelatedInvoicesAndPurchases(int supplierId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Load recent purchase invoices for this supplier
                    var invoiceQuery = @"
                        SELECT TOP 20 
                            PurchaseInvoiceId as InvoiceId, 
                            InvoiceNumber as InvoiceNo, 
                            TotalAmount as InvoiceAmount,
                            CreatedDate as InvoiceDate
                        FROM PurchaseInvoices 
                        WHERE SupplierId = @SupplierId 
                        ORDER BY CreatedDate DESC";
                    
                    using (var command = new SqlCommand(invoiceQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
                        using (var reader = command.ExecuteReader())
                        {
                            var invoiceTable = new DataTable();
                            invoiceTable.Load(reader);
                            
                            if (invoiceTable.Rows.Count > 0)
                            {
                                cmbOriginalInvoice.DataSource = invoiceTable;
                                cmbOriginalInvoice.DisplayMember = "InvoiceNo";
                                cmbOriginalInvoice.ValueMember = "InvoiceId";
                            }
                        }
                    }
                    
                    // Load recent purchases for this supplier
                    var purchaseQuery = @"
                        SELECT TOP 20 
                            PurchaseInvoiceId as PurchaseId, 
                            InvoiceNumber as PurchaseNo, 
                            TotalAmount as PurchaseAmount,
                            CreatedDate as PurchaseDate
                        FROM PurchaseInvoices 
                        WHERE SupplierId = @SupplierId 
                        ORDER BY CreatedDate DESC";
                    
                    using (var command = new SqlCommand(purchaseQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
                        using (var reader = command.ExecuteReader())
                        {
                            var purchaseTable = new DataTable();
                            purchaseTable.Load(reader);
                            
                            if (purchaseTable.Rows.Count > 0)
                            {
                                cmbReferencePurchase.DataSource = purchaseTable;
                                cmbReferencePurchase.DisplayMember = "PurchaseNo";
                                cmbReferencePurchase.ValueMember = "PurchaseId";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading related data: {ex.Message}");
            }
        }

        #endregion
    }
}
