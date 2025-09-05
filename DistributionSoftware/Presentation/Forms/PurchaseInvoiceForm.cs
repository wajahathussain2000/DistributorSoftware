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

namespace DistributionSoftware.Presentation.Forms
{
    public partial class PurchaseInvoiceForm : Form
    {
        private SqlConnection connection;
        private DataTable purchaseItemsDataTable;
        private bool isEditMode = false;
        private int currentPurchaseId = 0;
        private string currentStatus = "Draft";

        public PurchaseInvoiceForm()
        {
            InitializeComponent();
            InitializeConnection();
            InitializeDataTable();
            LoadSuppliers();
            LoadProducts();
            GeneratePurchaseNumber();
            GenerateBarcode();
            LoadPurchaseList();
            SetFormReadOnly(false);
        }

        private void InitializeConnection()
        {
            try
            {
                connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DistributionDB;Integrated Security=True");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeDataTable()
        {
            purchaseItemsDataTable = new DataTable();
            purchaseItemsDataTable.Columns.Add("ProductId", typeof(int));
            purchaseItemsDataTable.Columns.Add("ProductName", typeof(string));
            purchaseItemsDataTable.Columns.Add("Quantity", typeof(decimal));
            purchaseItemsDataTable.Columns.Add("UnitPrice", typeof(decimal));
            purchaseItemsDataTable.Columns.Add("LineTotal", typeof(decimal));
            purchaseItemsDataTable.Columns.Add("BatchNo", typeof(string));
            purchaseItemsDataTable.Columns.Add("ExpiryDate", typeof(DateTime));

            dgvPurchaseItems.DataSource = purchaseItemsDataTable;
            dgvPurchaseItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPurchaseItems.RowHeadersVisible = false;
            dgvPurchaseItems.AllowUserToAddRows = true;
            dgvPurchaseItems.AllowUserToDeleteRows = true;
            dgvPurchaseItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPurchaseItems.MultiSelect = false;
            dgvPurchaseItems.ReadOnly = false;
            dgvPurchaseItems.ScrollBars = ScrollBars.Vertical;

            // Configure columns
            dgvPurchaseItems.Columns["ProductId"].Visible = false;
            dgvPurchaseItems.Columns["ProductName"].HeaderText = "Product";
            dgvPurchaseItems.Columns["ProductName"].ReadOnly = true;
            dgvPurchaseItems.Columns["Quantity"].HeaderText = "Quantity";
            dgvPurchaseItems.Columns["UnitPrice"].HeaderText = "Unit Price";
            dgvPurchaseItems.Columns["LineTotal"].HeaderText = "Line Total";
            dgvPurchaseItems.Columns["LineTotal"].ReadOnly = true;
            dgvPurchaseItems.Columns["BatchNo"].HeaderText = "Batch No";
            dgvPurchaseItems.Columns["ExpiryDate"].HeaderText = "Expiry Date";

            // Add event handlers
            dgvPurchaseItems.CellValueChanged += DgvPurchaseItems_CellValueChanged;
            dgvPurchaseItems.CellClick += DgvPurchaseItems_CellClick;
        }

        private void LoadSuppliers()
        {
            try
            {
                string query = "SELECT SupplierId, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName";
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    cmbSupplier.DataSource = dt;
                    cmbSupplier.DisplayMember = "SupplierName";
                    cmbSupplier.ValueMember = "SupplierId";
                    cmbSupplier.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
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
                    
                    cmbProduct.DataSource = dt;
                    cmbProduct.DisplayMember = "ProductName";
                    cmbProduct.ValueMember = "ProductId";
                    cmbProduct.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProduct.SelectedValue != null)
                {
                    // Get the selected product's unit price and auto-populate it
                    DataRowView selectedRow = (DataRowView)cmbProduct.SelectedItem;
                    if (selectedRow != null)
                    {
                        decimal unitPrice = Convert.ToDecimal(selectedRow["UnitPrice"]);
                        txtUnitPrice.Text = unitPrice.ToString("0.00");
                        
                        // Also populate batch number if available
                        string batchNumber = selectedRow["Barcode"]?.ToString();
                        if (!string.IsNullOrEmpty(batchNumber))
                        {
                            txtBatchNo.Text = batchNumber;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GeneratePurchaseNumber()
        {
            try
            {
                string today = DateTime.Now.ToString("yyyyMMdd");
                string query = $"SELECT ISNULL(MAX(CAST(SUBSTRING(InvoiceNumber, 12, 5) AS INT)), 0) + 1 FROM PurchaseInvoices WHERE InvoiceNumber LIKE 'PI-{today}-%'";
                
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int nextNumber = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                    
                    txtPurchaseNo.Text = $"PI-{today}-{nextNumber:D5}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating purchase number: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPurchaseNo.Text = $"PI-{DateTime.Now:yyyyMMdd}-00001";
            }
        }

        private void GenerateBarcode()
        {
            try
            {
                string barcodeText = txtPurchaseNo.Text;
                txtBarcode.Text = barcodeText;
                
                // Generate barcode image
                GenerateBarcodeImage(barcodeText);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateBarcodeImage(string barcodeText)
        {
            try
            {
                // Create a larger barcode image without text
                Bitmap barcodeImage = new Bitmap(300, 80);
                using (Graphics g = Graphics.FromImage(barcodeImage))
                {
                    g.Clear(Color.White);
                    
                    // Draw barcode lines (no text, just the barcode pattern)
                    Random rand = new Random();
                    int lineWidth = 2;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPurchaseList()
        {
            try
            {
                string query = @"SELECT pi.PurchaseInvoiceId as PurchaseId, pi.InvoiceNumber as PurchaseNo, 
                                       s.SupplierName, pi.InvoiceNumber as InvoiceNo, pi.InvoiceDate, 
                                       pi.TotalAmount as NetAmount, pi.Status, pi.CreatedDate
                                FROM PurchaseInvoices pi
                                INNER JOIN Suppliers s ON pi.SupplierId = s.SupplierId
                                ORDER BY pi.CreatedDate DESC";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPurchases.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPurchaseItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dgvPurchaseItems.Rows[e.RowIndex];
                
                // Skip the empty "new row" at the bottom
                if (row.IsNewRow) return;
                
                // Calculate line total
                if (e.ColumnIndex == 2 || e.ColumnIndex == 3) // Quantity or UnitPrice column
                {
                    object quantityObj = GetCellValue(row, "Quantity");
                    object unitPriceObj = GetCellValue(row, "UnitPrice");
                    
                    decimal quantity = quantityObj == DBNull.Value ? 0 : Convert.ToDecimal(quantityObj);
                    decimal unitPrice = unitPriceObj == DBNull.Value ? 0 : Convert.ToDecimal(unitPriceObj);
                    
                    if (quantity > 0 && unitPrice > 0)
                    {
                        row.Cells["LineTotal"].Value = quantity * unitPrice;
                    }
                }
                
                CalculateTotals();
            }
        }

        private void DgvPurchaseItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1) // Product column
            {
                // Show product selection dialog or dropdown
                LoadProducts();
            }
        }

        private void CalculateTotals()
        {
            // Calculate subtotal from items (for reference)
            decimal itemsSubtotal = 0;
            
            foreach (DataGridViewRow row in dgvPurchaseItems.Rows)
            {
                // Skip the empty "new row" at the bottom
                if (row.IsNewRow) continue;
                
                object lineTotalObj = GetCellValue(row, "LineTotal");
                decimal lineTotal = lineTotalObj == DBNull.Value ? 0 : Convert.ToDecimal(lineTotalObj);
                
                itemsSubtotal += lineTotal;
            }
            
            // Get base amount and other values
            decimal baseAmount = 0;
            decimal taxAmount = 0;
            decimal discountAmount = 0;
            decimal freightCharges = 0;
            
            decimal.TryParse(txtNetAmount.Text, out baseAmount); // txtNetAmount is now Base Amount
            decimal.TryParse(txtTaxAmount.Text, out taxAmount);
            decimal.TryParse(txtDiscountAmount.Text, out discountAmount);
            decimal.TryParse(txtFreightCharges.Text, out freightCharges);
            
            // Calculate final total: Base Amount - Discount + Tax + Freight
            decimal finalTotal = baseAmount - discountAmount + taxAmount + freightCharges;
            txtSubTotal.Text = finalTotal.ToString("N2");
        }

        private void TxtBaseAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotals();
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
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
            
            DataRow newRow = purchaseItemsDataTable.NewRow();
            newRow["ProductId"] = cmbProduct.SelectedValue == null ? DBNull.Value : cmbProduct.SelectedValue;
            newRow["ProductName"] = cmbProduct.Text == null ? "" : cmbProduct.Text;
            newRow["Quantity"] = quantity;
            newRow["UnitPrice"] = unitPrice;
            newRow["LineTotal"] = lineTotal;
            newRow["BatchNo"] = string.IsNullOrEmpty(txtBatchNo.Text) ? (object)DBNull.Value : txtBatchNo.Text;
            newRow["ExpiryDate"] = dtpExpiryDate.Value;
            
            purchaseItemsDataTable.Rows.Add(newRow);
            
            // Clear item fields
            cmbProduct.SelectedIndex = -1;
            txtQuantity.Clear();
            txtUnitPrice.Clear();
            txtBatchNo.Clear();
            dtpExpiryDate.Value = DateTime.Now.AddYears(1);
            
            CalculateTotals();
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvPurchaseItems.SelectedRows.Count > 0)
            {
                dgvPurchaseItems.Rows.RemoveAt(dgvPurchaseItems.SelectedRows[0].Index);
                CalculateTotals();
            }
        }

        private void BtnSaveDraft_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SavePurchase("Draft");
            }
        }

        private void BtnPost_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SavePurchase("Posted");
            }
        }

        private void SavePurchase(string status)
        {
            try
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                
                try
                {
                    if (isEditMode)
                    {
                        // Update existing purchase
                        UpdatePurchase(transaction, status);
                    }
                    else
                    {
                        // Insert new purchase
                        InsertPurchase(transaction, status);
                    }
                    
                    transaction.Commit();
                    MessageBox.Show($"Purchase {status.ToLower()} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    ClearForm();
                    LoadPurchaseList();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving purchase: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void InsertPurchase(SqlTransaction transaction, string status)
        {
            // Insert Purchase header
            string purchaseQuery = @"INSERT INTO PurchaseInvoices (InvoiceNumber, SupplierId, InvoiceDate, 
                                                      BaseAmount, SubTotal, TaxAmount, DiscountAmount, FreightAmount, TotalAmount, 
                                                      PaidAmount, BalanceAmount, Status, Remarks, CreatedBy, CreatedDate)
                                                VALUES (@PurchaseNo, @SupplierId, @InvoiceDate, 
                                                        @BaseAmount, @SubTotal, @TaxAmount, @DiscountAmount, @FreightCharges, @TotalAmount, 
                                                        0, @TotalAmount, @Status, @Notes, @CreatedBy, @CreatedDate)";
            
            SqlCommand purchaseCmd = new SqlCommand(purchaseQuery, connection, transaction);
            purchaseCmd.Parameters.AddWithValue("@PurchaseNo", txtPurchaseNo.Text);
            purchaseCmd.Parameters.AddWithValue("@SupplierId", cmbSupplier.SelectedValue == null ? DBNull.Value : cmbSupplier.SelectedValue);
            purchaseCmd.Parameters.AddWithValue("@InvoiceDate", dtpInvoiceDate.Value);
            purchaseCmd.Parameters.AddWithValue("@BaseAmount", Convert.ToDecimal(txtNetAmount.Text)); // txtNetAmount is now Base Amount
            purchaseCmd.Parameters.AddWithValue("@SubTotal", Convert.ToDecimal(txtSubTotal.Text));
            purchaseCmd.Parameters.AddWithValue("@TaxAmount", Convert.ToDecimal(txtTaxAmount.Text));
            purchaseCmd.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtDiscountAmount.Text));
            purchaseCmd.Parameters.AddWithValue("@FreightCharges", Convert.ToDecimal(txtFreightCharges.Text));
            purchaseCmd.Parameters.AddWithValue("@TotalAmount", Convert.ToDecimal(txtSubTotal.Text)); // TotalAmount = SubTotal
            purchaseCmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
            purchaseCmd.Parameters.AddWithValue("@CreatedBy", GetCurrentUser());
            purchaseCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            purchaseCmd.Parameters.AddWithValue("@Status", status);
            
            purchaseCmd.ExecuteNonQuery();
            
            // Get the inserted PurchaseId
            string getPurchaseIdQuery = "SELECT SCOPE_IDENTITY()";
            SqlCommand getIdCmd = new SqlCommand(getPurchaseIdQuery, connection, transaction);
            int purchaseId = Convert.ToInt32(getIdCmd.ExecuteScalar());
            
            // Insert Purchase Items
            foreach (DataRow row in purchaseItemsDataTable.Rows)
            {
                // Skip empty rows (where ProductId is null or 0)
                if (row["ProductId"] == DBNull.Value || Convert.ToInt32(row["ProductId"]) == 0)
                    continue;
                
                string itemQuery = @"INSERT INTO PurchaseInvoiceDetails (PurchaseInvoiceId, ProductId, Quantity, UnitPrice, TaxAmount, DiscountAmount, TotalAmount, BatchNumber, ExpiryDate)
                                    VALUES (@PurchaseId, @ProductId, @Quantity, @UnitPrice, @TaxAmount, @DiscountAmount, @TotalAmount, @BatchNo, @ExpiryDate)";
                
                SqlCommand itemCmd = new SqlCommand(itemQuery, connection, transaction);
                itemCmd.Parameters.AddWithValue("@PurchaseId", purchaseId);
                itemCmd.Parameters.AddWithValue("@ProductId", row["ProductId"] ?? DBNull.Value);
                itemCmd.Parameters.AddWithValue("@Quantity", row["Quantity"] ?? DBNull.Value);
                itemCmd.Parameters.AddWithValue("@UnitPrice", row["UnitPrice"] ?? DBNull.Value);
                itemCmd.Parameters.AddWithValue("@TaxAmount", 0); // Default to 0 for line items
                itemCmd.Parameters.AddWithValue("@DiscountAmount", 0); // Default to 0 for line items
                itemCmd.Parameters.AddWithValue("@TotalAmount", row["LineTotal"] ?? DBNull.Value);
                itemCmd.Parameters.AddWithValue("@BatchNo", row["BatchNo"] ?? DBNull.Value);
                itemCmd.Parameters.AddWithValue("@ExpiryDate", row["ExpiryDate"] ?? DBNull.Value);
                
                itemCmd.ExecuteNonQuery();
            }
            
            // If Posted, add to supplier ledger
            if (status == "Posted")
            {
                AddToSupplierLedger(transaction, purchaseId, Convert.ToDecimal(txtNetAmount.Text));
            }
        }

        private void UpdatePurchase(SqlTransaction transaction, string status)
        {
            // Update Purchase header
            string purchaseQuery = @"UPDATE PurchaseInvoices SET SupplierId = @SupplierId, 
                                                      InvoiceDate = @InvoiceDate, BaseAmount = @BaseAmount, SubTotal = @SubTotal, TaxAmount = @TaxAmount, 
                                                      DiscountAmount = @DiscountAmount, FreightAmount = @FreightCharges, 
                                                      TotalAmount = @TotalAmount, BalanceAmount = @TotalAmount, Remarks = @Notes, 
                                                      ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate, Status = @Status
                                                WHERE PurchaseInvoiceId = @PurchaseId";
            
            SqlCommand purchaseCmd = new SqlCommand(purchaseQuery, connection, transaction);
            purchaseCmd.Parameters.AddWithValue("@PurchaseId", currentPurchaseId);
            purchaseCmd.Parameters.AddWithValue("@SupplierId", cmbSupplier.SelectedValue == null ? DBNull.Value : cmbSupplier.SelectedValue);
            purchaseCmd.Parameters.AddWithValue("@InvoiceDate", dtpInvoiceDate.Value);
            purchaseCmd.Parameters.AddWithValue("@BaseAmount", Convert.ToDecimal(txtNetAmount.Text)); // txtNetAmount is now Base Amount
            purchaseCmd.Parameters.AddWithValue("@SubTotal", Convert.ToDecimal(txtSubTotal.Text));
            purchaseCmd.Parameters.AddWithValue("@TaxAmount", Convert.ToDecimal(txtTaxAmount.Text));
            purchaseCmd.Parameters.AddWithValue("@DiscountAmount", Convert.ToDecimal(txtDiscountAmount.Text));
            purchaseCmd.Parameters.AddWithValue("@FreightCharges", Convert.ToDecimal(txtFreightCharges.Text));
            purchaseCmd.Parameters.AddWithValue("@TotalAmount", Convert.ToDecimal(txtSubTotal.Text)); // TotalAmount = SubTotal
            purchaseCmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
            purchaseCmd.Parameters.AddWithValue("@ModifiedBy", GetCurrentUser());
            purchaseCmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
            purchaseCmd.Parameters.AddWithValue("@Status", status);
            
            purchaseCmd.ExecuteNonQuery();
            
            // Delete existing items
            string deleteItemsQuery = "DELETE FROM PurchaseInvoiceDetails WHERE PurchaseInvoiceId = @PurchaseId";
            SqlCommand deleteCmd = new SqlCommand(deleteItemsQuery, connection, transaction);
            deleteCmd.Parameters.AddWithValue("@PurchaseId", currentPurchaseId);
            deleteCmd.ExecuteNonQuery();
            
            // Insert updated items
            foreach (DataRow row in purchaseItemsDataTable.Rows)
            {
                // Skip empty rows (where ProductId is null or 0)
                if (row["ProductId"] == DBNull.Value || Convert.ToInt32(row["ProductId"]) == 0)
                    continue;
                
                string itemQuery = @"INSERT INTO PurchaseInvoiceDetails (PurchaseInvoiceId, ProductId, Quantity, UnitPrice, TaxAmount, DiscountAmount, TotalAmount, BatchNumber, ExpiryDate)
                                    VALUES (@PurchaseId, @ProductId, @Quantity, @UnitPrice, @TaxAmount, @DiscountAmount, @TotalAmount, @BatchNo, @ExpiryDate)";
                
                SqlCommand itemCmd = new SqlCommand(itemQuery, connection, transaction);
                itemCmd.Parameters.AddWithValue("@PurchaseId", currentPurchaseId);
                itemCmd.Parameters.AddWithValue("@ProductId", row["ProductId"] ?? DBNull.Value);
                itemCmd.Parameters.AddWithValue("@Quantity", row["Quantity"] ?? DBNull.Value);
                itemCmd.Parameters.AddWithValue("@UnitPrice", row["UnitPrice"] ?? DBNull.Value);
                itemCmd.Parameters.AddWithValue("@TaxAmount", 0); // Default to 0 for line items
                itemCmd.Parameters.AddWithValue("@DiscountAmount", 0); // Default to 0 for line items
                itemCmd.Parameters.AddWithValue("@TotalAmount", row["LineTotal"] ?? DBNull.Value);
                itemCmd.Parameters.AddWithValue("@BatchNo", row["BatchNo"] ?? DBNull.Value);
                itemCmd.Parameters.AddWithValue("@ExpiryDate", row["ExpiryDate"] ?? DBNull.Value);
                
                itemCmd.ExecuteNonQuery();
            }
            
            // If Posted, add to supplier ledger
            if (status == "Posted")
            {
                AddToSupplierLedger(transaction, currentPurchaseId, Convert.ToDecimal(txtNetAmount.Text));
            }
        }

        private void AddToSupplierLedger(SqlTransaction transaction, int purchaseId, decimal amount)
        {
            string ledgerQuery = @"INSERT INTO SupplierTransactions (SupplierId, TransactionDate, TransactionType, 
                                              Description, DebitAmount, Balance, ReferenceNumber, CreatedDate, CreatedBy) 
                                              VALUES (@SupplierId, @TransactionDate, @TransactionType, @Description, 
                                              @DebitAmount, @Balance, @ReferenceNumber, @CreatedDate, @CreatedBy)";
            
            SqlCommand ledgerCmd = new SqlCommand(ledgerQuery, connection, transaction);
            ledgerCmd.Parameters.AddWithValue("@SupplierId", cmbSupplier.SelectedValue == null ? DBNull.Value : cmbSupplier.SelectedValue);
            ledgerCmd.Parameters.AddWithValue("@TransactionDate", dtpInvoiceDate.Value);
            ledgerCmd.Parameters.AddWithValue("@TransactionType", "Purchase");
            ledgerCmd.Parameters.AddWithValue("@Description", $"Purchase Invoice - {txtPurchaseNo.Text}");
            ledgerCmd.Parameters.AddWithValue("@DebitAmount", amount);
            ledgerCmd.Parameters.AddWithValue("@Balance", amount); // This should be calculated properly
            ledgerCmd.Parameters.AddWithValue("@ReferenceNumber", txtPurchaseNo.Text);
            ledgerCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            ledgerCmd.Parameters.AddWithValue("@CreatedBy", GetCurrentUser());
            
            ledgerCmd.ExecuteNonQuery();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (currentPurchaseId == 0)
            {
                MessageBox.Show("Please select a purchase to print.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // TODO: Implement print functionality
            MessageBox.Show("Print functionality will be implemented here.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtPurchaseNo.Clear();
            txtBarcode.Clear();
            cmbSupplier.SelectedIndex = -1;
            txtInvoiceNo.Clear();
            dtpInvoiceDate.Value = DateTime.Now;
            txtTaxAmount.Text = "0.00";
            txtDiscountAmount.Text = "0.00";
            txtFreightCharges.Text = "0.00";
            txtNetAmount.Text = "0.00";
            txtNotes.Clear();
            txtSubTotal.Text = "0.00";
            
            purchaseItemsDataTable.Clear();
            
            cmbProduct.SelectedIndex = -1;
            txtQuantity.Clear();
            txtUnitPrice.Clear();
            txtBatchNo.Clear();
            dtpExpiryDate.Value = DateTime.Now.AddYears(1);
            
            picBarcode.Image = null;
            
            isEditMode = false;
            currentPurchaseId = 0;
            currentStatus = "Draft";
            
            GeneratePurchaseNumber();
            GenerateBarcode();
            SetFormReadOnly(false);
        }

        private bool ValidateForm()
        {
            if (cmbSupplier.SelectedValue == null)
            {
                MessageBox.Show("Please select a supplier.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                MessageBox.Show("Please enter invoice number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            if (purchaseItemsDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one item.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            return true;
        }

        private void SetFormReadOnly(bool readOnly)
        {
            cmbSupplier.Enabled = !readOnly;
            txtInvoiceNo.ReadOnly = readOnly;
            dtpInvoiceDate.Enabled = !readOnly;
            txtTaxAmount.ReadOnly = readOnly;
            txtDiscountAmount.ReadOnly = readOnly;
            txtFreightCharges.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            
            cmbProduct.Enabled = !readOnly;
            txtQuantity.ReadOnly = readOnly;
            txtUnitPrice.ReadOnly = readOnly;
            txtBatchNo.ReadOnly = readOnly;
            dtpExpiryDate.Enabled = !readOnly;
            btnAddItem.Enabled = !readOnly;
            btnRemoveItem.Enabled = !readOnly;
            dgvPurchaseItems.ReadOnly = readOnly;
            
            btnSaveDraft.Enabled = !readOnly;
            btnPost.Enabled = !readOnly;
        }

        private int GetCurrentUser()
        {
            try
            {
                return 1; // Default user ID - implement actual user session
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
                return DBNull.Value;  // return as NULL to DB
            }
            return value;
        }


        private void DgvPurchases_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPurchases.Rows[e.RowIndex];
                object purchaseIdObj = GetCellValue(row, "PurchaseId");
                object statusObj = GetCellValue(row, "Status");
                
                currentPurchaseId = purchaseIdObj == DBNull.Value ? 0 : Convert.ToInt32(purchaseIdObj);
                currentStatus = statusObj == DBNull.Value ? "" : statusObj.ToString();
                
                // Load purchase details
                LoadPurchaseDetails(currentPurchaseId);
                
                // Set form read-only if posted
                SetFormReadOnly(currentStatus == "Posted");
            }
        }

        private void LoadPurchaseDetails(int purchaseId)
        {
            try
            {
                connection.Open();
                
                // Load purchase header
                string purchaseQuery = @"SELECT pi.*, s.SupplierName FROM PurchaseInvoices pi
                                        INNER JOIN Suppliers s ON pi.SupplierId = s.SupplierId
                                        WHERE pi.PurchaseInvoiceId = @PurchaseId";
                
                SqlCommand purchaseCmd = new SqlCommand(purchaseQuery, connection);
                purchaseCmd.Parameters.AddWithValue("@PurchaseId", purchaseId);
                
                using (SqlDataReader reader = purchaseCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtPurchaseNo.Text = reader["PurchaseNo"].ToString();
                        txtBarcode.Text = reader["PurchaseBarcode"].ToString();
                        cmbSupplier.Text = reader["SupplierName"].ToString();
                        txtInvoiceNo.Text = reader["InvoiceNo"].ToString();
                        dtpInvoiceDate.Value = Convert.ToDateTime(reader["InvoiceDate"]);
                        txtTaxAmount.Text = reader["TaxAmount"].ToString();
                        txtDiscountAmount.Text = reader["DiscountAmount"].ToString();
                        txtFreightCharges.Text = reader["FreightCharges"].ToString();
                        txtNetAmount.Text = reader["BaseAmount"].ToString(); // Load BaseAmount into txtNetAmount
                        txtNotes.Text = reader["Notes"].ToString();
                        
                        GenerateBarcodeImage(reader["PurchaseBarcode"].ToString());
                    }
                }
                
                // Load purchase items
                purchaseItemsDataTable.Clear();
                string itemsQuery = @"SELECT pid.*, p.ProductName FROM PurchaseInvoiceDetails pid
                                    INNER JOIN Products p ON pid.ProductId = p.ProductId
                                    WHERE pid.PurchaseInvoiceId = @PurchaseId";
                
                SqlCommand itemsCmd = new SqlCommand(itemsQuery, connection);
                itemsCmd.Parameters.AddWithValue("@PurchaseId", purchaseId);
                
                using (SqlDataReader reader = itemsCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataRow newRow = purchaseItemsDataTable.NewRow();
                        newRow["ProductId"] = reader["ProductId"] == DBNull.Value ? 0 : reader["ProductId"];
                        newRow["ProductName"] = reader["ProductName"] == DBNull.Value ? "" : reader["ProductName"].ToString();
                        newRow["Quantity"] = reader["Quantity"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["Quantity"]);
                        newRow["UnitPrice"] = reader["UnitPrice"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["UnitPrice"]);
                        newRow["LineTotal"] = (reader["Quantity"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["Quantity"])) * 
                                            (reader["UnitPrice"] == DBNull.Value ? 0m : Convert.ToDecimal(reader["UnitPrice"]));
                        newRow["BatchNo"] = reader["BatchNumber"] == DBNull.Value ? "" : reader["BatchNumber"].ToString();
                        newRow["ExpiryDate"] = reader["ExpiryDate"] == DBNull.Value ? DateTime.Now.AddYears(1) : Convert.ToDateTime(reader["ExpiryDate"]);
                        
                        purchaseItemsDataTable.Rows.Add(newRow);
                    }
                }
                
                CalculateTotals();
                isEditMode = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void TxtTaxAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotals();
        }

        private void TxtDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateTotals();
        }

        private void TxtFreightCharges_TextChanged(object sender, EventArgs e)
        {
            CalculateTotals();
        }
    }
}
