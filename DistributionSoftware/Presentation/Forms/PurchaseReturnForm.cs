using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;
using DistributionSoftware.Common;
using System.Diagnostics;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class PurchaseReturnForm : Form
    {
        private SqlConnection connection;
        private DataTable purchaseReturnItemsDataTable;
        private DataTable suppliersDataTable;
        private DataTable productsDataTable;
        private DataTable purchasesDataTable;
        private bool isEditMode = false;
        private int currentPurchaseReturnId = 0;
        private string currentStatus = "Draft";
        
        // Services
        private IPurchaseReturnService _purchaseReturnService;
        private IPurchaseReturnRepository _purchaseReturnRepository;
        private IPurchaseReturnItemRepository _purchaseReturnItemRepository;

        // Form Controls
        private TextBox txtReturnNumber;
        private TextBox txtBarcode;
        private ComboBox cmbSupplier;
        private ComboBox cmbReferencePurchase;
        private DateTimePicker dtpReturnDate;
        private TextBox txtTaxAdjust;
        private TextBox txtDiscountAdjust;
        private TextBox txtFreightAdjust;
        private TextBox txtNetReturnAmount;
        private TextBox txtReason;
        private ComboBox cmbStatus;
        private DataGridView dgvPurchaseReturnItems;
        private DataGridView dgvPurchaseReturnList;
        private PictureBox picBarcode;
        
        // Item controls
        private ComboBox cmbProduct;
        private TextBox txtQuantity;
        private TextBox txtUnitPrice;
        private TextBox txtLineTotal;
        private TextBox txtBatchNumber;
        private DateTimePicker dtpExpiryDate;
        private TextBox txtItemNotes;

        public PurchaseReturnForm()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Initializing form");
                InitializeComponent();
                InitializeConnection();
                InitializeServices();
                InitializeDataTable();
                LoadSuppliers();
                LoadProducts();
                LoadPurchases();
                GenerateReturnNumber();
                GenerateBarcode();
                LoadPurchaseReturnList();
                SetFormReadOnly(false);
                Debug.WriteLine("PurchaseReturnForm: Form initialized successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Initialization error - {ex.Message}");
                MessageBox.Show($"Error initializing Purchase Return Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeConnection()
        {
            try
            {
                string connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
                connection = new SqlConnection(connectionString);
                Debug.WriteLine("PurchaseReturnForm: Database connection initialized");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Connection error - {ex.Message}");
                MessageBox.Show($"Database connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeServices()
        {
            try
            {
                string connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
                _purchaseReturnRepository = new PurchaseReturnRepository(connectionString);
                _purchaseReturnItemRepository = new PurchaseReturnItemRepository(connectionString);
                _purchaseReturnService = new PurchaseReturnService(_purchaseReturnRepository, _purchaseReturnItemRepository);
                Debug.WriteLine("PurchaseReturnForm: Services initialized");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Services initialization error - {ex.Message}");
                throw;
            }
        }

        private void InitializeDataTable()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Initializing data table");
                purchaseReturnItemsDataTable = new DataTable();
                purchaseReturnItemsDataTable.Columns.Add("ProductId", typeof(int));
                purchaseReturnItemsDataTable.Columns.Add("ProductName", typeof(string));
                purchaseReturnItemsDataTable.Columns.Add("ProductCode", typeof(string));
                purchaseReturnItemsDataTable.Columns.Add("Quantity", typeof(decimal));
                purchaseReturnItemsDataTable.Columns.Add("UnitPrice", typeof(decimal));
                purchaseReturnItemsDataTable.Columns.Add("LineTotal", typeof(decimal));
                purchaseReturnItemsDataTable.Columns.Add("BatchNumber", typeof(string));
                purchaseReturnItemsDataTable.Columns.Add("ExpiryDate", typeof(DateTime));
                purchaseReturnItemsDataTable.Columns.Add("Notes", typeof(string));

                dgvPurchaseReturnItems.DataSource = purchaseReturnItemsDataTable;
                dgvPurchaseReturnItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPurchaseReturnItems.RowHeadersVisible = false;
                dgvPurchaseReturnItems.AllowUserToAddRows = false;
                dgvPurchaseReturnItems.AllowUserToDeleteRows = true;
                dgvPurchaseReturnItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPurchaseReturnItems.MultiSelect = false;
                dgvPurchaseReturnItems.ReadOnly = false;
                dgvPurchaseReturnItems.ScrollBars = ScrollBars.Vertical;

                // Configure columns
                dgvPurchaseReturnItems.Columns["ProductId"].Visible = false;
                dgvPurchaseReturnItems.Columns["ProductName"].HeaderText = "Product";
                dgvPurchaseReturnItems.Columns["ProductName"].ReadOnly = true;
                dgvPurchaseReturnItems.Columns["ProductCode"].HeaderText = "Code";
                dgvPurchaseReturnItems.Columns["ProductCode"].ReadOnly = true;
                dgvPurchaseReturnItems.Columns["Quantity"].HeaderText = "Quantity";
                dgvPurchaseReturnItems.Columns["UnitPrice"].HeaderText = "Unit Price";
                dgvPurchaseReturnItems.Columns["LineTotal"].HeaderText = "Line Total";
                dgvPurchaseReturnItems.Columns["LineTotal"].ReadOnly = true;
                dgvPurchaseReturnItems.Columns["BatchNumber"].HeaderText = "Batch No";
                dgvPurchaseReturnItems.Columns["ExpiryDate"].HeaderText = "Expiry Date";
                dgvPurchaseReturnItems.Columns["Notes"].HeaderText = "Notes";

                // Add event handlers
                dgvPurchaseReturnItems.CellValueChanged += DgvPurchaseReturnItems_CellValueChanged;
                dgvPurchaseReturnItems.CellClick += DgvPurchaseReturnItems_CellClick;
                
                Debug.WriteLine("PurchaseReturnForm: Data table initialized");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Data table initialization error - {ex.Message}");
                throw;
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Loading suppliers");
                string query = @"SELECT SupplierId, SupplierCode, SupplierName, ContactPerson, Email, Phone
                                FROM Suppliers
                                WHERE IsActive = 1 OR IsActive IS NULL
                                ORDER BY SupplierName";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    suppliersDataTable = dt;
                    
                    cmbSupplier.DataSource = dt;
                    cmbSupplier.DisplayMember = "SupplierName";
                    cmbSupplier.ValueMember = "SupplierId";
                    cmbSupplier.SelectedIndex = -1;
                }
                Debug.WriteLine($"PurchaseReturnForm: Loaded {suppliersDataTable.Rows.Count} suppliers");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Load suppliers error - {ex.Message}");
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Loading products");
                string query = @"
                    SELECT p.ProductId, p.ProductCode, p.ProductName, p.Description, 
                           p.UnitPrice, p.StockQuantity, p.Category, p.Barcode
                    FROM Products p
                    WHERE p.IsActive = 1
                    ORDER BY p.ProductName";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    productsDataTable = dt;
                    
                    cmbProduct.DataSource = dt;
                    cmbProduct.DisplayMember = "ProductName";
                    cmbProduct.ValueMember = "ProductId";
                    cmbProduct.SelectedIndex = -1;
                }
                Debug.WriteLine($"PurchaseReturnForm: Loaded {productsDataTable.Rows.Count} products");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Load products error - {ex.Message}");
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPurchases()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Loading purchases");
                string query = @"SELECT pi.PurchaseInvoiceId, pi.InvoiceNumber, s.SupplierName, pi.InvoiceDate, pi.TotalAmount
                                FROM PurchaseInvoices pi
                                INNER JOIN Suppliers s ON pi.SupplierId = s.SupplierId
                                WHERE pi.Status = 'Posted'
                                ORDER BY pi.InvoiceDate DESC";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    purchasesDataTable = dt;
                    
                    cmbReferencePurchase.DataSource = dt;
                    cmbReferencePurchase.DisplayMember = "InvoiceNumber";
                    cmbReferencePurchase.ValueMember = "PurchaseInvoiceId";
                    cmbReferencePurchase.SelectedIndex = -1;
                }
                Debug.WriteLine($"PurchaseReturnForm: Loaded {purchasesDataTable.Rows.Count} purchases");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Load purchases error - {ex.Message}");
                MessageBox.Show($"Error loading purchases: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GenerateReturnNumber()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Generating return number");
                var returnNumber = await _purchaseReturnService.GenerateNextReturnNumberAsync();
                txtReturnNumber.Text = returnNumber;
                Debug.WriteLine($"PurchaseReturnForm: Generated return number {returnNumber}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Generate return number error - {ex.Message}");
                // Fallback
                string today = DateTime.Now.ToString("yyyyMMdd");
                txtReturnNumber.Text = $"PR-{today}-00001";
            }
        }

        private async void GenerateBarcode()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Generating barcode");
                var barcode = await _purchaseReturnService.GenerateBarcodeAsync(txtReturnNumber.Text);
                txtBarcode.Text = barcode;
                GenerateBarcodeImage(barcode);
                Debug.WriteLine($"PurchaseReturnForm: Generated barcode {barcode}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Generate barcode error - {ex.Message}");
                txtBarcode.Text = txtReturnNumber.Text;
                GenerateBarcodeImage(txtReturnNumber.Text);
            }
        }

        private void GenerateBarcodeImage(string barcodeText)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnForm: Generating barcode image for {barcodeText}");
                Bitmap barcodeImage = new Bitmap(300, 80);
                using (Graphics g = Graphics.FromImage(barcodeImage))
                {
                    g.Clear(Color.White);
                    
                    // Draw barcode lines
                    Random rand = new Random();
                    int startX = 20;
                    int lineSpacing = 3;
                    
                    for (int i = 0; i < barcodeText.Length * 4; i++)
                    {
                        int height = rand.Next(20, 60);
                        int x = startX + i * lineSpacing;
                        g.DrawLine(Pens.Black, x, 10, x, 10 + height);
                    }
                }
                
                picBarcode.Image = barcodeImage;
                Debug.WriteLine("PurchaseReturnForm: Barcode image generated");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Barcode image generation error - {ex.Message}");
            }
        }

        private async void LoadPurchaseReturnList()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Loading purchase return list");
                var purchaseReturns = await _purchaseReturnService.GetAllPurchaseReturnsAsync();
                
                dgvPurchaseReturnList.DataSource = purchaseReturns.Select(pr => new
                {
                    pr.PurchaseReturnId,
                    pr.ReturnNumber,
                    pr.SupplierName,
                    ReferencePurchase = pr.ReferencePurchaseNumber,
                    pr.ReturnDate,
                    pr.NetReturnAmount,
                    pr.Status,
                    pr.CreatedDate
                }).ToList();
                
                Debug.WriteLine($"PurchaseReturnForm: Loaded {purchaseReturns.Count} purchase returns");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Load purchase return list error - {ex.Message}");
                MessageBox.Show($"Error loading purchase return list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbReferencePurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbReferencePurchase.SelectedValue != null)
                {
                    Debug.WriteLine("PurchaseReturnForm: Reference purchase selected");
                    DataRowView selectedRow = (DataRowView)cmbReferencePurchase.SelectedItem;
                    if (selectedRow != null)
                    {
                        // Auto-populate supplier
                        string supplierName = selectedRow["SupplierName"].ToString();
                        for (int i = 0; i < cmbSupplier.Items.Count; i++)
                        {
                            DataRowView supplierRow = (DataRowView)cmbSupplier.Items[i];
                            if (supplierRow["SupplierName"].ToString() == supplierName)
                            {
                                cmbSupplier.SelectedIndex = i;
                                break;
                            }
                        }
                        
                        // Load products from this purchase
                        LoadProductsFromPurchase(Convert.ToInt32(cmbReferencePurchase.SelectedValue));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Reference purchase selection error - {ex.Message}");
                MessageBox.Show($"Error loading purchase details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductsFromPurchase(int purchaseId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnForm: Loading products from purchase {purchaseId}");
                string query = @"
                    SELECT DISTINCT p.ProductId, p.ProductCode, p.ProductName, p.UnitPrice
                    FROM Products p
                    INNER JOIN PurchaseInvoiceItems pii ON p.ProductId = pii.ProductId
                    WHERE pii.PurchaseInvoiceId = @PurchaseId AND p.IsActive = 1
                    ORDER BY p.ProductName";
                
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PurchaseId", purchaseId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        
                        cmbProduct.DataSource = dt;
                        cmbProduct.DisplayMember = "ProductName";
                        cmbProduct.ValueMember = "ProductId";
                        cmbProduct.SelectedIndex = -1;
                    }
                }
                Debug.WriteLine($"PurchaseReturnForm: Loaded products from purchase {purchaseId}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Load products from purchase error - {ex.Message}");
                // Fallback to all products
                LoadProducts();
            }
        }

        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProduct.SelectedValue != null)
                {
                    Debug.WriteLine("PurchaseReturnForm: Product selected");
                    DataRowView selectedRow = (DataRowView)cmbProduct.SelectedItem;
                    if (selectedRow != null)
                    {
                        // Auto-populate unit price
                        if (selectedRow["UnitPrice"] != DBNull.Value)
                        {
                            txtUnitPrice.Text = selectedRow["UnitPrice"].ToString();
                        }
                        
                        // Auto-populate batch number if available
                        string batchNumber = selectedRow["ProductCode"]?.ToString();
                        if (!string.IsNullOrEmpty(batchNumber))
                        {
                            txtBatchNumber.Text = batchNumber;
                        }
                        
                        // Calculate line total
                        CalculateLineTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Product selection error - {ex.Message}");
                MessageBox.Show($"Error loading product details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateLineTotal()
        {
            try
            {
                if (decimal.TryParse(txtQuantity.Text, out decimal quantity) && 
                    decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice))
                {
                    decimal lineTotal = quantity * unitPrice;
                    txtLineTotal.Text = lineTotal.ToString("N2");
                }
                else
                {
                    txtLineTotal.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Calculate line total error - {ex.Message}");
                txtLineTotal.Text = "0.00";
            }
        }

        private void TxtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateLineTotal();
        }

        private void TxtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateLineTotal();
        }

        private void DgvPurchaseReturnItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dgvPurchaseReturnItems.Rows[e.RowIndex];
                
                if (row.IsNewRow) return;
                
                // Calculate line total when quantity or unit price changes
                if (e.ColumnIndex == 3 || e.ColumnIndex == 4) // Quantity or UnitPrice column
                {
                    object quantityObj = GetCellValue(row, "Quantity");
                    object unitPriceObj = GetCellValue(row, "UnitPrice");
                    
                    decimal quantity = quantityObj == null || quantityObj == DBNull.Value ? 0 : Convert.ToDecimal(quantityObj);
                    decimal unitPrice = unitPriceObj == null || unitPriceObj == DBNull.Value ? 0 : Convert.ToDecimal(unitPriceObj);
                    
                    decimal lineTotal = quantity * unitPrice;
                    row.Cells["LineTotal"].Value = lineTotal;
                    
                    // Recalculate net amount
                    CalculateNetReturnAmount();
                }
            }
        }

        private void DgvPurchaseReturnItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1) // Product column
            {
                // Show product selection dialog or dropdown
                LoadProducts();
            }
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Adding item");
                
                if (cmbProduct.SelectedValue == null)
                {
                    MessageBox.Show("Please select a product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    MessageBox.Show("Please enter quantity and unit price.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                decimal quantity = Convert.ToDecimal(txtQuantity.Text);
                decimal unitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                decimal lineTotal = quantity * unitPrice;
                
                DataRow newRow = purchaseReturnItemsDataTable.NewRow();
                newRow["ProductId"] = cmbProduct.SelectedValue;
                newRow["ProductName"] = cmbProduct.Text;
                newRow["ProductCode"] = ((DataRowView)cmbProduct.SelectedItem)["ProductCode"];
                newRow["Quantity"] = quantity;
                newRow["UnitPrice"] = unitPrice;
                newRow["LineTotal"] = lineTotal;
                newRow["BatchNumber"] = string.IsNullOrEmpty(txtBatchNumber.Text) ? (object)DBNull.Value : txtBatchNumber.Text;
                newRow["ExpiryDate"] = dtpExpiryDate.Value;
                newRow["Notes"] = string.IsNullOrEmpty(txtItemNotes.Text) ? (object)DBNull.Value : txtItemNotes.Text;
                
                purchaseReturnItemsDataTable.Rows.Add(newRow);
                
                // Clear item fields
                cmbProduct.SelectedIndex = -1;
                txtQuantity.Clear();
                txtUnitPrice.Clear();
                txtLineTotal.Clear();
                txtBatchNumber.Clear();
                dtpExpiryDate.Value = DateTime.Now.AddYears(1);
                txtItemNotes.Clear();
                
                // Recalculate net amount
                CalculateNetReturnAmount();
                
                Debug.WriteLine("PurchaseReturnForm: Item added successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Add item error - {ex.Message}");
                MessageBox.Show($"Error adding item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPurchaseReturnItems.SelectedRows.Count > 0)
                {
                    Debug.WriteLine("PurchaseReturnForm: Removing item");
                    dgvPurchaseReturnItems.Rows.RemoveAt(dgvPurchaseReturnItems.SelectedRows[0].Index);
                    CalculateNetReturnAmount();
                    Debug.WriteLine("PurchaseReturnForm: Item removed successfully");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Remove item error - {ex.Message}");
                MessageBox.Show($"Error removing item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateNetReturnAmount()
        {
            try
            {
                decimal subtotal = 0;
                foreach (DataRow row in purchaseReturnItemsDataTable.Rows)
                {
                    if (row["LineTotal"] != DBNull.Value)
                    {
                        subtotal += Convert.ToDecimal(row["LineTotal"]);
                    }
                }
                
                decimal taxAdjust = string.IsNullOrEmpty(txtTaxAdjust.Text) ? 0 : Convert.ToDecimal(txtTaxAdjust.Text);
                decimal discountAdjust = string.IsNullOrEmpty(txtDiscountAdjust.Text) ? 0 : Convert.ToDecimal(txtDiscountAdjust.Text);
                decimal freightAdjust = string.IsNullOrEmpty(txtFreightAdjust.Text) ? 0 : Convert.ToDecimal(txtFreightAdjust.Text);
                
                decimal netAmount = subtotal + taxAdjust - discountAdjust + freightAdjust;
                txtNetReturnAmount.Text = netAmount.ToString("N2");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Calculate net amount error - {ex.Message}");
                txtNetReturnAmount.Text = "0.00";
            }
        }

        private void TxtTaxAdjust_TextChanged(object sender, EventArgs e)
        {
            CalculateNetReturnAmount();
        }

        private void TxtDiscountAdjust_TextChanged(object sender, EventArgs e)
        {
            CalculateNetReturnAmount();
        }

        private void TxtFreightAdjust_TextChanged(object sender, EventArgs e)
        {
            CalculateNetReturnAmount();
        }

        private async void BtnSaveDraft_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Saving draft");
                
                if (ValidateForm())
                {
                    var purchaseReturn = CreatePurchaseReturnFromForm();
                    purchaseReturn.Status = "Draft";
                    
                    if (isEditMode)
                    {
                        await _purchaseReturnService.UpdatePurchaseReturnAsync(purchaseReturn);
                    }
                    else
                    {
                        await _purchaseReturnService.CreatePurchaseReturnAsync(purchaseReturn);
                    }
                    
                    MessageBox.Show("Purchase return saved as draft successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadPurchaseReturnList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Save draft error - {ex.Message}");
                MessageBox.Show($"Error saving draft: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnPost_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Posting purchase return");
                
                if (ValidateForm())
                {
                    var purchaseReturn = CreatePurchaseReturnFromForm();
                    purchaseReturn.Status = "Posted";
                    
                    if (isEditMode)
                    {
                        await _purchaseReturnService.UpdatePurchaseReturnAsync(purchaseReturn);
                        await _purchaseReturnService.PostPurchaseReturnAsync(purchaseReturn.PurchaseReturnId);
                    }
                    else
                    {
                        var purchaseReturnId = await _purchaseReturnService.CreatePurchaseReturnAsync(purchaseReturn);
                        await _purchaseReturnService.PostPurchaseReturnAsync(purchaseReturnId);
                    }
                    
                    MessageBox.Show("Purchase return posted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadPurchaseReturnList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Post error - {ex.Message}");
                MessageBox.Show($"Error posting purchase return: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private PurchaseReturn CreatePurchaseReturnFromForm()
        {
            var purchaseReturn = new PurchaseReturn
            {
                PurchaseReturnId = currentPurchaseReturnId,
                ReturnNumber = txtReturnNumber.Text,
                Barcode = txtBarcode.Text,
                SupplierId = cmbSupplier.SelectedValue != null ? Convert.ToInt32(cmbSupplier.SelectedValue) : 0,
                ReferencePurchaseId = cmbReferencePurchase.SelectedValue != null ? Convert.ToInt32(cmbReferencePurchase.SelectedValue) : (int?)null,
                ReturnDate = dtpReturnDate.Value,
                TaxAdjust = string.IsNullOrEmpty(txtTaxAdjust.Text) ? 0 : Convert.ToDecimal(txtTaxAdjust.Text),
                DiscountAdjust = string.IsNullOrEmpty(txtDiscountAdjust.Text) ? 0 : Convert.ToDecimal(txtDiscountAdjust.Text),
                FreightAdjust = string.IsNullOrEmpty(txtFreightAdjust.Text) ? 0 : Convert.ToDecimal(txtFreightAdjust.Text),
                NetReturnAmount = string.IsNullOrEmpty(txtNetReturnAmount.Text) ? 0 : Convert.ToDecimal(txtNetReturnAmount.Text),
                Reason = txtReason.Text,
                Status = currentStatus,
                CreatedBy = GetCurrentUser(),
                CreatedDate = DateTime.Now
            };
            
            // Add items
            purchaseReturn.Items = new List<PurchaseReturnItem>();
            foreach (DataRow row in purchaseReturnItemsDataTable.Rows)
            {
                if (row["ProductId"] != DBNull.Value)
                {
                    var item = new PurchaseReturnItem
                    {
                        ProductId = Convert.ToInt32(row["ProductId"]),
                        ProductName = row["ProductName"].ToString(),
                        ProductCode = row["ProductCode"].ToString(),
                        Quantity = Convert.ToDecimal(row["Quantity"]),
                        UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                        LineTotal = Convert.ToDecimal(row["LineTotal"]),
                        BatchNumber = row["BatchNumber"] == DBNull.Value ? null : row["BatchNumber"].ToString(),
                        ExpiryDate = row["ExpiryDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["ExpiryDate"]),
                        Notes = row["Notes"] == DBNull.Value ? null : row["Notes"].ToString()
                    };
                    purchaseReturn.Items.Add(item);
                }
            }
            
            return purchaseReturn;
        }

        private bool ValidateForm()
        {
            try
            {
                if (cmbSupplier.SelectedValue == null)
                {
                    MessageBox.Show("Please select a supplier.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                if (purchaseReturnItemsDataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Please add at least one item.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Validation error - {ex.Message}");
                MessageBox.Show($"Validation error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ClearForm()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnForm: Clearing form");
                
                txtReturnNumber.Clear();
                txtBarcode.Clear();
                cmbSupplier.SelectedIndex = -1;
                cmbReferencePurchase.SelectedIndex = -1;
                dtpReturnDate.Value = DateTime.Now;
                txtTaxAdjust.Clear();
                txtDiscountAdjust.Clear();
                txtFreightAdjust.Clear();
                txtNetReturnAmount.Clear();
                txtReason.Clear();
                cmbStatus.SelectedIndex = -1;
                
                purchaseReturnItemsDataTable.Clear();
                
                cmbProduct.SelectedIndex = -1;
                txtQuantity.Clear();
                txtUnitPrice.Clear();
                txtLineTotal.Clear();
                txtBatchNumber.Clear();
                dtpExpiryDate.Value = DateTime.Now.AddYears(1);
                txtItemNotes.Clear();
                
                picBarcode.Image = null;
                
                isEditMode = false;
                currentPurchaseReturnId = 0;
                currentStatus = "Draft";
                
                GenerateReturnNumber();
                GenerateBarcode();
                SetFormReadOnly(false);
                
                Debug.WriteLine("PurchaseReturnForm: Form cleared");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Clear form error - {ex.Message}");
            }
        }

        private void SetFormReadOnly(bool readOnly)
        {
            try
            {
                cmbSupplier.Enabled = !readOnly;
                cmbReferencePurchase.Enabled = !readOnly;
                dtpReturnDate.Enabled = !readOnly;
                txtTaxAdjust.ReadOnly = readOnly;
                txtDiscountAdjust.ReadOnly = readOnly;
                txtFreightAdjust.ReadOnly = readOnly;
                txtReason.ReadOnly = readOnly;
                
                cmbProduct.Enabled = !readOnly;
                txtQuantity.ReadOnly = readOnly;
                txtUnitPrice.ReadOnly = readOnly;
                txtBatchNumber.ReadOnly = readOnly;
                dtpExpiryDate.Enabled = !readOnly;
                txtItemNotes.ReadOnly = readOnly;
                dgvPurchaseReturnItems.ReadOnly = readOnly;
                
                // Button states will be handled by the form designer
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Set form read only error - {ex.Message}");
            }
        }

        private int GetCurrentUser()
        {
            try
            {
                // Get from user session or default to 1
                return UserSession.CurrentUser?.UserId ?? 1;
            }
            catch
            {
                return 1;
            }
        }

        private object GetCellValue(DataGridViewRow row, string columnName)
        {
            var value = row.Cells[columnName].Value;
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return 0;
            }
            return value;
        }

        private void DgvPurchaseReturnList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    Debug.WriteLine("PurchaseReturnForm: Purchase return selected from list");
                    
                    DataGridViewRow row = dgvPurchaseReturnList.Rows[e.RowIndex];
                    object purchaseReturnIdObj = GetCellValue(row, "PurchaseReturnId");
                    
                    currentPurchaseReturnId = purchaseReturnIdObj == null || purchaseReturnIdObj == DBNull.Value ? 0 : Convert.ToInt32(purchaseReturnIdObj);
                    
                    // Load purchase return details
                    LoadPurchaseReturnDetails(currentPurchaseReturnId);
                    
                    // Set form read-only if posted
                    SetFormReadOnly(currentStatus == "Posted");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Purchase return list selection error - {ex.Message}");
                MessageBox.Show($"Error loading purchase return details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadPurchaseReturnDetails(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnForm: Loading purchase return details for ID {purchaseReturnId}");
                
                var purchaseReturn = await _purchaseReturnService.GetPurchaseReturnByIdAsync(purchaseReturnId);
                if (purchaseReturn != null)
                {
                    txtReturnNumber.Text = purchaseReturn.ReturnNumber;
                    txtBarcode.Text = purchaseReturn.Barcode;
                    
                    // Set supplier
                    for (int i = 0; i < cmbSupplier.Items.Count; i++)
                    {
                        DataRowView supplierRow = (DataRowView)cmbSupplier.Items[i];
                        if (Convert.ToInt32(supplierRow["SupplierId"]) == purchaseReturn.SupplierId)
                        {
                            cmbSupplier.SelectedIndex = i;
                            break;
                        }
                    }
                    
                    // Set reference purchase
                    if (purchaseReturn.ReferencePurchaseId.HasValue)
                    {
                        for (int i = 0; i < cmbReferencePurchase.Items.Count; i++)
                        {
                            DataRowView purchaseRow = (DataRowView)cmbReferencePurchase.Items[i];
                            if (Convert.ToInt32(purchaseRow["PurchaseInvoiceId"]) == purchaseReturn.ReferencePurchaseId.Value)
                            {
                                cmbReferencePurchase.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                    
                    dtpReturnDate.Value = purchaseReturn.ReturnDate;
                    txtTaxAdjust.Text = purchaseReturn.TaxAdjust.ToString("N2");
                    txtDiscountAdjust.Text = purchaseReturn.DiscountAdjust.ToString("N2");
                    txtFreightAdjust.Text = purchaseReturn.FreightAdjust.ToString("N2");
                    txtNetReturnAmount.Text = purchaseReturn.NetReturnAmount.ToString("N2");
                    txtReason.Text = purchaseReturn.Reason ?? "";
                    currentStatus = purchaseReturn.Status;
                    
                    // Load items
                    purchaseReturnItemsDataTable.Clear();
                    foreach (var item in purchaseReturn.Items)
                    {
                        DataRow newRow = purchaseReturnItemsDataTable.NewRow();
                        newRow["ProductId"] = item.ProductId;
                        newRow["ProductName"] = item.ProductName;
                        newRow["ProductCode"] = item.ProductCode;
                        newRow["Quantity"] = item.Quantity;
                        newRow["UnitPrice"] = item.UnitPrice;
                        newRow["LineTotal"] = item.LineTotal;
                        newRow["BatchNumber"] = item.BatchNumber ?? (object)DBNull.Value;
                        newRow["ExpiryDate"] = item.ExpiryDate ?? (object)DBNull.Value;
                        newRow["Notes"] = item.Notes ?? (object)DBNull.Value;
                        
                        purchaseReturnItemsDataTable.Rows.Add(newRow);
                    }
                    
                    // Generate barcode image
                    GenerateBarcodeImage(purchaseReturn.Barcode);
                    
                    isEditMode = true;
                }
                
                Debug.WriteLine("PurchaseReturnForm: Purchase return details loaded");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnForm: Load purchase return details error - {ex.Message}");
                MessageBox.Show($"Error loading purchase return details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (currentPurchaseReturnId == 0)
            {
                MessageBox.Show("Please select a purchase return to print.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            MessageBox.Show("Print functionality will be implemented here.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
