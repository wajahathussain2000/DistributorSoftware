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
    public partial class GRNForm : Form
    {
        private SqlConnection connection;
        private DataTable grnItemsDataTable;
        private DataTable suppliersDataTable;
        private bool isEditMode = false;
        private int currentGRNId = 0;
        private string currentStatus = "Draft";

        public GRNForm()
        {
            InitializeComponent();
            InitializeConnection();
            InitializeDataTable();
            LoadSuppliers();
            LoadPostedPurchases();
            LoadProducts();
            GenerateGRNNumber();
            GenerateBarcode();
            LoadGRNList();
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
            grnItemsDataTable = new DataTable();
            grnItemsDataTable.Columns.Add("ProductId", typeof(int));
            grnItemsDataTable.Columns.Add("ProductName", typeof(string));
            grnItemsDataTable.Columns.Add("ReceivedQty", typeof(decimal));
            grnItemsDataTable.Columns.Add("AcceptedQty", typeof(decimal));
            grnItemsDataTable.Columns.Add("RejectedQty", typeof(decimal));
            grnItemsDataTable.Columns.Add("BatchNo", typeof(string));
            grnItemsDataTable.Columns.Add("ExpiryDate", typeof(DateTime));

            dgvGRNItems.DataSource = grnItemsDataTable;
            dgvGRNItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGRNItems.RowHeadersVisible = false;
            dgvGRNItems.AllowUserToAddRows = false;
            dgvGRNItems.AllowUserToDeleteRows = true;
            dgvGRNItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGRNItems.MultiSelect = false;
            dgvGRNItems.ReadOnly = false;
            dgvGRNItems.ScrollBars = ScrollBars.Vertical;

            // Configure columns
            dgvGRNItems.Columns["ProductId"].Visible = false;
            dgvGRNItems.Columns["ProductName"].HeaderText = "Product";
            dgvGRNItems.Columns["ProductName"].ReadOnly = true;
            dgvGRNItems.Columns["ReceivedQty"].HeaderText = "Received Qty";
            dgvGRNItems.Columns["AcceptedQty"].HeaderText = "Accepted Qty";
            dgvGRNItems.Columns["RejectedQty"].HeaderText = "Rejected Qty";
            dgvGRNItems.Columns["RejectedQty"].ReadOnly = true;
            dgvGRNItems.Columns["BatchNo"].HeaderText = "Batch No";
            dgvGRNItems.Columns["ExpiryDate"].HeaderText = "Expiry Date";

            // Add event handlers
            dgvGRNItems.CellValueChanged += DgvGRNItems_CellValueChanged;
            dgvGRNItems.CellClick += DgvGRNItems_CellClick;
        }

        private void LoadPostedPurchases()
        {
            try
            {
                // First try to load only Posted purchases
                string query = @"SELECT pi.PurchaseInvoiceId, pi.InvoiceNumber, s.SupplierName, pi.InvoiceDate, pi.TotalAmount
                                FROM PurchaseInvoices pi
                                INNER JOIN Suppliers s ON pi.SupplierId = s.SupplierId
                                WHERE pi.Status = 'Posted'
                                ORDER BY pi.InvoiceDate DESC";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    // If no posted purchases found, load all purchases
                    if (dt.Rows.Count == 0)
                    {
                        query = @"SELECT pi.PurchaseInvoiceId, pi.InvoiceNumber, s.SupplierName, pi.InvoiceDate, pi.TotalAmount
                                 FROM PurchaseInvoices pi
                                 INNER JOIN Suppliers s ON pi.SupplierId = s.SupplierId
                                 ORDER BY pi.InvoiceDate DESC";
                        
                        adapter.SelectCommand.CommandText = query;
                        dt.Clear();
                        adapter.Fill(dt);
                    }
                    
                    cmbPurchase.DataSource = dt;
                    cmbPurchase.DisplayMember = "InvoiceNumber";
                    cmbPurchase.ValueMember = "PurchaseInvoiceId";
                    cmbPurchase.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchases: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                string query = @"SELECT SupplierId, SupplierCode, SupplierName, ContactPerson, Email, Phone
                                FROM Suppliers
                                WHERE IsActive = 1 OR IsActive IS NULL
                                ORDER BY SupplierName";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    
                    // Store suppliers data for later use
                    suppliersDataTable = dt;
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

        private void CmbPurchase_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPurchase.SelectedValue != null)
                {
                    // Get the selected purchase's supplier and auto-populate it
                    DataRowView selectedRow = (DataRowView)cmbPurchase.SelectedItem;
                    if (selectedRow != null)
                    {
                        txtSupplier.Text = selectedRow["SupplierName"].ToString();
                        txtSupplier.Tag = selectedRow["PurchaseInvoiceId"]; // Store PurchaseInvoiceId for later use
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProduct.SelectedValue != null)
                {
                    // Get the selected product's details
                    DataRowView selectedRow = (DataRowView)cmbProduct.SelectedItem;
                    if (selectedRow != null)
                    {
                        // Auto-populate batch number if available
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

        private void GenerateGRNNumber()
        {
            try
            {
                string today = DateTime.Now.ToString("yyyyMMdd");
                
                // Get the next sequential number for today's date
                string query = $"SELECT ISNULL(MAX(CAST(SUBSTRING(GRNNo, 12, 5) AS INT)), 0) + 1 FROM GRN WHERE GRNNo LIKE 'GRN-{today}-%'";
                
                // Use a fresh connection to avoid connection state issues
                using (SqlConnection freshConnection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DistributionDB;Integrated Security=True"))
                using (SqlCommand cmd = new SqlCommand(query, freshConnection))
                {
                    freshConnection.Open();
                    int nextNumber = Convert.ToInt32(cmd.ExecuteScalar());
                    
                    // Generate sequential number with 5-digit padding
                    txtGRNNo.Text = $"GRN-{today}-{nextNumber:D5}";
                }
                
                // Update barcode to match GRN number
                GenerateBarcode();
            }
            catch
            {
                // Fallback to sequential number if database query fails
                string today = DateTime.Now.ToString("yyyyMMdd");
                txtGRNNo.Text = $"GRN-{today}-00001";
                
                // Update barcode to match GRN number
                GenerateBarcode();
            }
        }

        private void GenerateBarcode()
        {
            try
            {
                string barcodeText = txtGRNNo.Text;
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

        private void LoadGRNList()
        {
            try
            {
                string query = @"SELECT g.GRNId, g.GRNNo, s.SupplierName, p.InvoiceNumber as PurchaseNo, 
                                       g.GRNDate, g.Remarks, g.CreatedDate
                                FROM GRN g
                                INNER JOIN Suppliers s ON g.SupplierId = s.SupplierId
                                LEFT JOIN PurchaseInvoices p ON g.PurchaseId = p.PurchaseInvoiceId
                                ORDER BY g.CreatedDate DESC";
                
                // Use a fresh connection to avoid connection state issues
                using (SqlConnection freshConnection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DistributionDB;Integrated Security=True"))
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, freshConnection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvGRNList.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading GRN list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvGRNItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dgvGRNItems.Rows[e.RowIndex];
                
                // Skip the empty "new row" at the bottom
                if (row.IsNewRow) return;
                
                // Calculate rejected quantity
                if (e.ColumnIndex == 2 || e.ColumnIndex == 3) // ReceivedQty or AcceptedQty column
                {
                    object receivedQtyObj = GetCellValue(row, "ReceivedQty");
                    object acceptedQtyObj = GetCellValue(row, "AcceptedQty");
                    
                    decimal receivedQty = receivedQtyObj == null || receivedQtyObj == DBNull.Value ? 0 : Convert.ToDecimal(receivedQtyObj);
                    decimal acceptedQty = acceptedQtyObj == null || acceptedQtyObj == DBNull.Value ? 0 : Convert.ToDecimal(acceptedQtyObj);
                    
                    // Calculate rejected quantity = Received - Accepted
                    decimal rejectedQty = receivedQty - acceptedQty;
                    row.Cells["RejectedQty"].Value = rejectedQty;
                }
            }
        }

        private void DgvGRNItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1) // Product column
            {
                // Show product selection dialog or dropdown
                LoadProducts();
            }
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedValue == null)
            {
                MessageBox.Show("Please select a product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (string.IsNullOrEmpty(txtReceivedQty.Text) || string.IsNullOrEmpty(txtAcceptedQty.Text))
            {
                MessageBox.Show("Please enter received and accepted quantities.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            decimal receivedQty = string.IsNullOrEmpty(txtReceivedQty.Text) ? 0 : Convert.ToDecimal(txtReceivedQty.Text);
            decimal acceptedQty = string.IsNullOrEmpty(txtAcceptedQty.Text) ? 0 : Convert.ToDecimal(txtAcceptedQty.Text);
            decimal rejectedQty = receivedQty - acceptedQty;
            
            DataRow newRow = grnItemsDataTable.NewRow();
            newRow["ProductId"] = cmbProduct.SelectedValue == null ? DBNull.Value : cmbProduct.SelectedValue;
            newRow["ProductName"] = cmbProduct.Text == null ? "" : cmbProduct.Text;
            newRow["ReceivedQty"] = receivedQty;
            newRow["AcceptedQty"] = acceptedQty;
            newRow["RejectedQty"] = rejectedQty;
            newRow["BatchNo"] = string.IsNullOrEmpty(txtBatchNo.Text) ? (object)DBNull.Value : txtBatchNo.Text;
            newRow["ExpiryDate"] = dtpExpiryDate.Value;
            
            grnItemsDataTable.Rows.Add(newRow);
            
            // Clear item fields
            cmbProduct.SelectedIndex = -1;
            txtReceivedQty.Clear();
            txtAcceptedQty.Clear();
            txtBatchNo.Clear();
            dtpExpiryDate.Value = DateTime.Now.AddYears(1);
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvGRNItems.SelectedRows.Count > 0)
            {
                dgvGRNItems.Rows.RemoveAt(dgvGRNItems.SelectedRows[0].Index);
            }
        }

        private void BtnSaveDraft_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SaveGRN("Draft");
            }
        }

        private void BtnPost_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SaveGRN("Posted");
            }
        }

        private void SaveGRN(string status)
        {
            try
            {
                
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                
                try
                {
                    if (isEditMode)
                    {
                        // Update existing GRN
                        UpdateGRN(transaction, status);
                    }
                    else
                    {
                        // Insert new GRN
                        InsertGRN(transaction, status);
                    }
                    
                    transaction.Commit();
                    MessageBox.Show($"GRN {status.ToLower()} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    ClearForm();
                    LoadGRNList();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving GRN: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
            
        }

        private void InsertGRN(SqlTransaction transaction, string status)
        {
            try
            {
                
                // Get supplier ID
                int supplierId = GetSupplierIdFromPurchase();
                
                // Check if this supplier exists in the Suppliers table
                VerifySupplierExists(supplierId);
                
                // Insert GRN header
                string grnQuery = @"INSERT INTO GRN (GRNNo, GRNBarcode, PurchaseId, SupplierId, GRNDate, Remarks, CreatedBy, CreatedDate)
                                    VALUES (@GRNNo, @GRNBarcode, @PurchaseId, @SupplierId, @GRNDate, @Remarks, @CreatedBy, @CreatedDate)";
                
                SqlCommand grnCmd = new SqlCommand(grnQuery, connection, transaction);
                
                grnCmd.Parameters.AddWithValue("@GRNNo", txtGRNNo.Text);
                grnCmd.Parameters.AddWithValue("@GRNBarcode", txtBarcode.Text);
                grnCmd.Parameters.AddWithValue("@PurchaseId", DBNull.Value); // Set to NULL since Purchases table is empty
                grnCmd.Parameters.AddWithValue("@SupplierId", supplierId);
                grnCmd.Parameters.AddWithValue("@GRNDate", dtpGRNDate.Value);
                grnCmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                grnCmd.Parameters.AddWithValue("@CreatedBy", GetCurrentUser());
                grnCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                
                
                int rowsAffected = grnCmd.ExecuteNonQuery();
                
                if (rowsAffected == 0)
                {
                    throw new Exception("No rows were inserted into GRN table");
                }
            
                // Get the inserted GRNId
                string getGRNIdQuery = "SELECT @@IDENTITY";
                SqlCommand getIdCmd = new SqlCommand(getGRNIdQuery, connection, transaction);
                int grnId = 0;
                
                object result = getIdCmd.ExecuteScalar();
                
                if (result == null || result == DBNull.Value)
                {
                    // Try alternative approach - get the last inserted ID by querying the table
                    string altQuery = "SELECT TOP 1 GRNId FROM GRN WHERE GRNNo = @GRNNo ORDER BY GRNId DESC";
                    SqlCommand altCmd = new SqlCommand(altQuery, connection, transaction);
                    altCmd.Parameters.AddWithValue("@GRNNo", txtGRNNo.Text);
                    object altResult = altCmd.ExecuteScalar();
                    
                    if (altResult == null || altResult == DBNull.Value)
                    {
                        throw new Exception("Could not retrieve GRNId after successful INSERT");
                    }
                    
                    grnId = Convert.ToInt32(altResult);
                }
                else
                {
                    grnId = Convert.ToInt32(result);
                }
                
                // Insert GRN Items
                foreach (DataRow row in grnItemsDataTable.Rows)
                {
                    // Skip empty rows (where ProductId is null or 0)
                    if (row["ProductId"] == DBNull.Value || row["ProductId"] == null || 
                        (row["ProductId"] != DBNull.Value && Convert.ToInt32(row["ProductId"]) == 0))
                    {
                        continue;
                    }
                    
                    // Additional check: skip if all required fields are empty
                    if (string.IsNullOrEmpty(row["ProductName"]?.ToString()) && 
                        (row["ReceivedQty"] == DBNull.Value || Convert.ToDecimal(row["ReceivedQty"]) == 0))
                    {
                        continue;
                    }
                    
                    string itemQuery = @"INSERT INTO GRNItems (GRNId, ProductId, ReceivedQty, AcceptedQty, BatchNo, ExpiryDate)
                                        VALUES (@GRNId, @ProductId, @ReceivedQty, @AcceptedQty, @BatchNo, @ExpiryDate)";
                    
                    SqlCommand itemCmd = new SqlCommand(itemQuery, connection, transaction);
                    
                    itemCmd.Parameters.AddWithValue("@GRNId", grnId);
                    itemCmd.Parameters.AddWithValue("@ProductId", row["ProductId"] == DBNull.Value ? 0 : row["ProductId"]);
                    itemCmd.Parameters.AddWithValue("@ReceivedQty", row["ReceivedQty"] == DBNull.Value ? 0 : row["ReceivedQty"]);
                    itemCmd.Parameters.AddWithValue("@AcceptedQty", row["AcceptedQty"] == DBNull.Value ? 0 : row["AcceptedQty"]);
                    itemCmd.Parameters.AddWithValue("@BatchNo", row["BatchNo"] == DBNull.Value ? "" : row["BatchNo"]);
                    itemCmd.Parameters.AddWithValue("@ExpiryDate", row["ExpiryDate"] == DBNull.Value ? DateTime.Now.AddYears(1) : row["ExpiryDate"]);
                    
                    itemCmd.ExecuteNonQuery();
                }
                
                // If Posted, update inventory with Accepted Qty
                if (status == "Posted")
                {
                    UpdateInventory(transaction, grnId);
                }
            }
            catch
            {
                throw;
            }
        }

        private void UpdateGRN(SqlTransaction transaction, string status)
        {
            // Update GRN header
            string grnQuery = @"UPDATE GRN SET PurchaseId = @PurchaseId, SupplierId = @SupplierId, 
                                          GRNDate = @GRNDate, Remarks = @Remarks, 
                                          ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate
                                    WHERE GRNId = @GRNId";
            
            SqlCommand grnCmd = new SqlCommand(grnQuery, connection, transaction);
            grnCmd.Parameters.AddWithValue("@GRNId", currentGRNId);
            grnCmd.Parameters.AddWithValue("@PurchaseId", DBNull.Value); // Set to NULL since Purchases table is empty
            grnCmd.Parameters.AddWithValue("@SupplierId", GetSupplierIdFromPurchase());
            grnCmd.Parameters.AddWithValue("@GRNDate", dtpGRNDate.Value);
            grnCmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
            grnCmd.Parameters.AddWithValue("@ModifiedBy", GetCurrentUser());
            grnCmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
            
            grnCmd.ExecuteNonQuery();
            
            // Delete existing items
            string deleteItemsQuery = "DELETE FROM GRNItems WHERE GRNId = @GRNId";
            SqlCommand deleteCmd = new SqlCommand(deleteItemsQuery, connection, transaction);
            deleteCmd.Parameters.AddWithValue("@GRNId", currentGRNId);
            deleteCmd.ExecuteNonQuery();
            
            // Insert updated items
            foreach (DataRow row in grnItemsDataTable.Rows)
            {
                // Skip empty rows (where ProductId is null or 0)
                if (row["ProductId"] == DBNull.Value || row["ProductId"] == null || 
                    (row["ProductId"] != DBNull.Value && Convert.ToInt32(row["ProductId"]) == 0))
                    continue;
                
                // Additional check: skip if all required fields are empty
                if (string.IsNullOrEmpty(row["ProductName"]?.ToString()) && 
                    (row["ReceivedQty"] == DBNull.Value || Convert.ToDecimal(row["ReceivedQty"]) == 0))
                    continue;
                
                string itemQuery = @"INSERT INTO GRNItems (GRNId, ProductId, ReceivedQty, AcceptedQty, BatchNo, ExpiryDate)
                                    VALUES (@GRNId, @ProductId, @ReceivedQty, @AcceptedQty, @BatchNo, @ExpiryDate)";
                
                SqlCommand itemCmd = new SqlCommand(itemQuery, connection, transaction);
                itemCmd.Parameters.AddWithValue("@GRNId", currentGRNId);
                itemCmd.Parameters.AddWithValue("@ProductId", row["ProductId"] == DBNull.Value ? 0 : row["ProductId"]);
                itemCmd.Parameters.AddWithValue("@ReceivedQty", row["ReceivedQty"] == DBNull.Value ? 0 : row["ReceivedQty"]);
                itemCmd.Parameters.AddWithValue("@AcceptedQty", row["AcceptedQty"] == DBNull.Value ? 0 : row["AcceptedQty"]);
                itemCmd.Parameters.AddWithValue("@BatchNo", row["BatchNo"] == DBNull.Value ? "" : row["BatchNo"]);
                itemCmd.Parameters.AddWithValue("@ExpiryDate", row["ExpiryDate"] == DBNull.Value ? DateTime.Now.AddYears(1) : row["ExpiryDate"]);
                
                itemCmd.ExecuteNonQuery();
            }
            
            // If Posted, update inventory with Accepted Qty
            if (status == "Posted")
            {
                UpdateInventory(transaction, currentGRNId);
            }
        }

        private void UpdateInventory(SqlTransaction transaction, int grnId)
        {
            // Update inventory with accepted quantities
            string inventoryQuery = @"UPDATE p 
                                      SET p.StockQuantity = p.StockQuantity + gi.AcceptedQty
                                      FROM Products p
                                      INNER JOIN GRNItems gi ON p.ProductId = gi.ProductId
                                      WHERE gi.GRNId = @GRNId";
            
            SqlCommand inventoryCmd = new SqlCommand(inventoryQuery, connection, transaction);
            inventoryCmd.Parameters.AddWithValue("@GRNId", grnId);
            inventoryCmd.ExecuteNonQuery();
        }

        private int GetSupplierIdFromPurchase()
        {
            try
            {
                
                if (cmbPurchase.SelectedValue != null)
                {
                    string query = "SELECT SupplierId FROM PurchaseInvoices WHERE PurchaseInvoiceId = @PurchaseId";
                    
                    // Use a fresh connection to avoid transaction conflicts
                    using (SqlConnection freshConnection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DistributionDB;Integrated Security=True"))
                    {
                        freshConnection.Open();
                        using (SqlCommand cmd = new SqlCommand(query, freshConnection))
                        {
                            cmd.Parameters.AddWithValue("@PurchaseId", cmbPurchase.SelectedValue);
                            object result = cmd.ExecuteScalar();
                            
                            int supplierId = result != null ? Convert.ToInt32(result) : 0;
                            return supplierId;
                        }
                    }
                }
                else
                {
                }
            }
            catch (Exception)
            {
                return 0;
            }
            
            return 0;
        }

        private void VerifySupplierExists(int supplierId)
        {
            try
            {
                
                // Use a fresh connection to avoid transaction conflicts
                using (SqlConnection freshConnection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DistributionDB;Integrated Security=True"))
                {
                    freshConnection.Open();
                    string query = "SELECT SupplierId, SupplierName FROM Suppliers WHERE SupplierId = @SupplierId";
                    using (SqlCommand cmd = new SqlCommand(query, freshConnection))
                    {
                        cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                            }
                            else
                            {
                                
                                // List all available suppliers
                                reader.Close();
                                string allSuppliersQuery = "SELECT SupplierId, SupplierName FROM Suppliers";
                                using (SqlCommand allCmd = new SqlCommand(allSuppliersQuery, freshConnection))
                                {
                                    using (SqlDataReader allReader = allCmd.ExecuteReader())
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (currentGRNId == 0)
            {
                MessageBox.Show("Please select a GRN to print.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            MessageBox.Show("Print functionality will be implemented here.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtGRNNo.Clear();
            txtBarcode.Clear();
            cmbPurchase.SelectedIndex = -1;
            txtSupplier.Clear();
            dtpGRNDate.Value = DateTime.Now;
            txtRemarks.Clear();
            
            grnItemsDataTable.Clear();
            
            cmbProduct.SelectedIndex = -1;
            txtReceivedQty.Clear();
            txtAcceptedQty.Clear();
            txtBatchNo.Clear();
            dtpExpiryDate.Value = DateTime.Now.AddYears(1);
            
            picBarcode.Image = null;
            
            isEditMode = false;
            currentGRNId = 0;
            currentStatus = "Draft";
            
            GenerateGRNNumber();
            GenerateBarcode();
            SetFormReadOnly(false);
        }

        private bool ValidateForm()
        {
            if (cmbPurchase.SelectedValue == null)
            {
                MessageBox.Show("Please select a purchase.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            if (grnItemsDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one item.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            return true;
        }

        private void SetFormReadOnly(bool readOnly)
        {
            cmbPurchase.Enabled = !readOnly;
            txtSupplier.ReadOnly = readOnly;
            dtpGRNDate.Enabled = !readOnly;
            txtRemarks.ReadOnly = readOnly;
            
            cmbProduct.Enabled = !readOnly;
            txtReceivedQty.ReadOnly = readOnly;
            txtAcceptedQty.ReadOnly = readOnly;
            txtBatchNo.ReadOnly = readOnly;
            dtpExpiryDate.Enabled = !readOnly;
            btnAddItem.Enabled = !readOnly;
            btnRemoveItem.Enabled = !readOnly;
            dgvGRNItems.ReadOnly = readOnly;
            
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
                return 0;  // return 0 instead of NULL to DB
            }
            return value;
        }

        private object SafeGetValue(object value, object defaultValue)
        {
            if (value == null || value == DBNull.Value)
            {
                return defaultValue;
            }
            return value;
        }

        private void DgvGRNList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvGRNList.Rows[e.RowIndex];
                object grnIdObj = GetCellValue(row, "GRNId");
                
                currentGRNId = grnIdObj == null || grnIdObj == DBNull.Value ? 0 : Convert.ToInt32(grnIdObj);
                
                // Load GRN details
                LoadGRNDetails(currentGRNId);
                
                // Set form read-only if posted
                SetFormReadOnly(currentStatus == "Posted");
            }
        }

        private void LoadGRNDetails(int grnId)
        {
            try
            {
                // Use a fresh connection to avoid connection state issues
                using (SqlConnection freshConnection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=DistributionDB;Integrated Security=True"))
                {
                    freshConnection.Open();
                    
                    // Load GRN header
                    string grnQuery = @"SELECT g.*, s.SupplierName, p.InvoiceNumber as PurchaseNo FROM GRN g
                                        INNER JOIN Suppliers s ON g.SupplierId = s.SupplierId
                                        LEFT JOIN PurchaseInvoices p ON g.PurchaseId = p.PurchaseInvoiceId
                                        WHERE g.GRNId = @GRNId";
                    
                    SqlCommand grnCmd = new SqlCommand(grnQuery, freshConnection);
                    grnCmd.Parameters.AddWithValue("@GRNId", grnId);
                    
                    using (SqlDataReader reader = grnCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtGRNNo.Text = SafeGetValue(reader["GRNNo"], "").ToString();
                            txtBarcode.Text = SafeGetValue(reader["GRNBarcode"], "").ToString();
                            txtSupplier.Text = SafeGetValue(reader["SupplierName"], "").ToString();
                            dtpGRNDate.Value = Convert.ToDateTime(SafeGetValue(reader["GRNDate"], DateTime.Now));
                            txtRemarks.Text = SafeGetValue(reader["Remarks"], "").ToString();
                            
                            // Set purchase selection
                            if (reader["PurchaseNo"] != DBNull.Value)
                            {
                                string purchaseNo = reader["PurchaseNo"].ToString();
                                for (int i = 0; i < cmbPurchase.Items.Count; i++)
                                {
                                    DataRowView item = (DataRowView)cmbPurchase.Items[i];
                                    if (item["InvoiceNumber"].ToString() == purchaseNo)
                                    {
                                        cmbPurchase.SelectedIndex = i;
                                        break;
                                    }
                                }
                            }
                            
                            // Generate barcode image to match the GRN number
                            GenerateBarcodeImage(SafeGetValue(reader["GRNNo"], "").ToString());
                        }
                    }
                    
                    // Load GRN items
                    grnItemsDataTable.Clear();
                    string itemsQuery = @"SELECT gi.*, p.ProductName FROM GRNItems gi
                                        INNER JOIN Products p ON gi.ProductId = p.ProductId
                                        WHERE gi.GRNId = @GRNId";
                    
                    SqlCommand itemsCmd = new SqlCommand(itemsQuery, freshConnection);
                    itemsCmd.Parameters.AddWithValue("@GRNId", grnId);
                    
                    using (SqlDataReader reader = itemsCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataRow newRow = grnItemsDataTable.NewRow();
                            newRow["ProductId"] = SafeGetValue(reader["ProductId"], 0);
                            newRow["ProductName"] = SafeGetValue(reader["ProductName"], "").ToString();
                            newRow["ReceivedQty"] = Convert.ToDecimal(SafeGetValue(reader["ReceivedQty"], 0));
                            newRow["AcceptedQty"] = Convert.ToDecimal(SafeGetValue(reader["AcceptedQty"], 0));
                            newRow["RejectedQty"] = Convert.ToDecimal(SafeGetValue(reader["RejectedQty"], 0));
                            newRow["BatchNo"] = SafeGetValue(reader["BatchNo"], "").ToString();
                            newRow["ExpiryDate"] = Convert.ToDateTime(SafeGetValue(reader["ExpiryDate"], DateTime.Now.AddYears(1)));
                            
                            grnItemsDataTable.Rows.Add(newRow);
                        }
                    }
                    
                    isEditMode = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading GRN details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
