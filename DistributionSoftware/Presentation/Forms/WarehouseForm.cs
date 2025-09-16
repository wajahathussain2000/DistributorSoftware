using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class WarehouseForm : Form
    {
        private string connectionString;
        private int selectedWarehouseId = -1;

        public WarehouseForm()
        {
            InitializeComponent();
            InitializeConnection();
            SetupDataGridView();
            
            // Load warehouses after form is fully shown and all controls are initialized
            this.Shown += (sender, e) => LoadWarehouses();
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        private void SetupDataGridView()
        {
            // Configure DataGridView styling
            dgvWarehouses.BackgroundColor = Color.White;
            dgvWarehouses.BorderStyle = BorderStyle.Fixed3D;
            dgvWarehouses.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvWarehouses.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvWarehouses.ColumnHeadersDefaultCellStyle.BackColor = Color.LightBlue;
            dgvWarehouses.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            dgvWarehouses.ColumnHeadersDefaultCellStyle.ForeColor = Color.DarkBlue;
            dgvWarehouses.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvWarehouses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvWarehouses.ColumnHeadersHeight = 35;
            dgvWarehouses.DefaultCellStyle.BackColor = Color.White;
            dgvWarehouses.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F);
            dgvWarehouses.DefaultCellStyle.ForeColor = Color.Black;
            dgvWarehouses.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvWarehouses.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvWarehouses.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dgvWarehouses.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvWarehouses.GridColor = Color.LightGray;
            dgvWarehouses.RowHeadersVisible = false;
            dgvWarehouses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWarehouses.MultiSelect = false;
            dgvWarehouses.ReadOnly = true;
            dgvWarehouses.AllowUserToAddRows = false;
            dgvWarehouses.AllowUserToDeleteRows = false;
        }

        private void LoadWarehouses()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query;
                    
                    // Default to showing only active warehouses
                    query = "SELECT WarehouseId, WarehouseName, Location, ContactPerson, ContactPhone, IsActive, CreatedDate FROM Warehouses WHERE IsActive = 1 ORDER BY WarehouseName";
                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvWarehouses.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWarehousesWithFilter()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query;
                    
                    // Check if chkShowInactive is initialized before accessing it
                    bool showInactive = chkShowInactive != null && chkShowInactive.Checked;
                    
                    if (showInactive)
                    {
                        // Show all warehouses (active and inactive)
                        query = "SELECT WarehouseId, WarehouseName, Location, ContactPerson, ContactPhone, IsActive, CreatedDate FROM Warehouses ORDER BY IsActive DESC, WarehouseName";
                    }
                    else
                    {
                        // Show only active warehouses
                        query = "SELECT WarehouseId, WarehouseName, Location, ContactPerson, ContactPhone, IsActive, CreatedDate FROM Warehouses WHERE IsActive = 1 ORDER BY WarehouseName";
                    }
                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvWarehouses.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading warehouses: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridViewColumns()
        {
            try
            {
                if (dgvWarehouses.Columns != null && dgvWarehouses.Columns.Count > 0)
                {
                    // Configure column headers
                    if (dgvWarehouses.Columns.Contains("WarehouseId"))
                        dgvWarehouses.Columns["WarehouseId"].HeaderText = "ID";
                    if (dgvWarehouses.Columns.Contains("WarehouseName"))
                        dgvWarehouses.Columns["WarehouseName"].HeaderText = "Warehouse Name";
                    if (dgvWarehouses.Columns.Contains("Location"))
                        dgvWarehouses.Columns["Location"].HeaderText = "Location";
                    if (dgvWarehouses.Columns.Contains("ContactPerson"))
                        dgvWarehouses.Columns["ContactPerson"].HeaderText = "Contact Person";
                    if (dgvWarehouses.Columns.Contains("ContactPhone"))
                        dgvWarehouses.Columns["ContactPhone"].HeaderText = "Contact Phone";
                    if (dgvWarehouses.Columns.Contains("IsActive"))
                        dgvWarehouses.Columns["IsActive"].HeaderText = "Active";
                    if (dgvWarehouses.Columns.Contains("CreatedDate"))
                        dgvWarehouses.Columns["CreatedDate"].HeaderText = "Created Date";
                    
                    // Hide WarehouseId column
                    if (dgvWarehouses.Columns.Contains("WarehouseId"))
                        dgvWarehouses.Columns["WarehouseId"].Visible = false;
                    
                    // Set column widths safely
                    if (dgvWarehouses.Columns.Contains("WarehouseName"))
                        dgvWarehouses.Columns["WarehouseName"].Width = 200;
                    if (dgvWarehouses.Columns.Contains("Location"))
                        dgvWarehouses.Columns["Location"].Width = 250;
                    if (dgvWarehouses.Columns.Contains("ContactPerson"))
                        dgvWarehouses.Columns["ContactPerson"].Width = 150;
                    if (dgvWarehouses.Columns.Contains("ContactPhone"))
                        dgvWarehouses.Columns["ContactPhone"].Width = 150;
                    if (dgvWarehouses.Columns.Contains("IsActive"))
                        dgvWarehouses.Columns["IsActive"].Width = 80;
                    if (dgvWarehouses.Columns.Contains("CreatedDate"))
                        dgvWarehouses.Columns["CreatedDate"].Width = 120;
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't show to user as it's not critical
                System.Diagnostics.Debug.WriteLine($"Error configuring DataGridView columns: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = @"
                            INSERT INTO Warehouses 
                            (WarehouseName, Location, ContactPerson, ContactPhone, IsActive, CreatedDate)
                            VALUES (@Name, @Location, @ContactPerson, @ContactPhone, @IsActive, GETDATE())";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Name", txtWarehouseName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactPhone", txtContactPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Warehouse added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                        LoadWarehousesWithFilter();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding warehouse: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedWarehouseId == -1)
            {
                MessageBox.Show("Please select a warehouse to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ValidateInput())
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = @"
                            UPDATE Warehouses 
                            SET WarehouseName = @Name, 
                                Location = @Location, 
                                ContactPerson = @ContactPerson, 
                                ContactPhone = @ContactPhone, 
                                IsActive = @IsActive
                            WHERE WarehouseId = @Id";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Id", selectedWarehouseId);
                        cmd.Parameters.AddWithValue("@Name", txtWarehouseName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactPhone", txtContactPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Warehouse updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            LoadWarehousesWithFilter();
                        }
                        else
                        {
                            MessageBox.Show("No warehouse was updated. Please try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating warehouse: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedWarehouseId == -1)
            {
                MessageBox.Show("Please select a warehouse to deactivate.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to deactivate this warehouse?\n\nNote: This will mark the warehouse as inactive instead of permanently deleting it to preserve data integrity.", "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE Warehouses SET IsActive = 0 WHERE WarehouseId = @Id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Id", selectedWarehouseId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Warehouse deactivated successfully!\n\nThe warehouse has been marked as inactive and is no longer available for new operations.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            LoadWarehousesWithFilter();
                        }
                        else
                        {
                            MessageBox.Show("No warehouse was deactivated. Please try again.", "Deactivation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deactivating warehouse: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvWarehouses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvWarehouses.Rows[e.RowIndex];
                selectedWarehouseId = Convert.ToInt32(row.Cells["WarehouseId"].Value);
                bool isActive = Convert.ToBoolean(row.Cells["IsActive"].Value);
                
                // Populate form fields with selected warehouse data
                txtWarehouseName.Text = row.Cells["WarehouseName"].Value.ToString();
                txtLocation.Text = row.Cells["Location"].Value.ToString();
                txtContactPerson.Text = row.Cells["ContactPerson"].Value.ToString();
                txtContactPhone.Text = row.Cells["ContactPhone"].Value.ToString();
                chkIsActive.Checked = isActive;
                
                btnAdd.Text = "➕ Add New";
                btnUpdate.Enabled = true;
                
                // Show/hide buttons based on warehouse status
                if (isActive)
                {
                    btnDelete.Enabled = true;
                    btnDelete.Text = "🚫 Deactivate";
                    btnReactivate.Enabled = false;
                    btnReactivate.Visible = false;
                }
                else
                {
                    btnDelete.Enabled = false;
                    btnDelete.Visible = false;
                    btnReactivate.Enabled = true;
                    btnReactivate.Visible = true;
                }
            }
        }

        private void dgvWarehouses_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Configure columns after data binding is complete
            ConfigureDataGridViewColumns();
        }

        private void chkShowInactive_CheckedChanged(object sender, EventArgs e)
        {
            // Reload warehouses when filter changes (only if form is fully loaded)
            if (this.IsHandleCreated)
            {
                LoadWarehousesWithFilter();
            }
        }

        private void btnReactivate_Click(object sender, EventArgs e)
        {
            if (selectedWarehouseId == -1)
            {
                MessageBox.Show("Please select an inactive warehouse to reactivate.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Are you sure you want to reactivate this warehouse?", "Confirm Reactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE Warehouses SET IsActive = 1 WHERE WarehouseId = @Id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Id", selectedWarehouseId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Warehouse reactivated successfully!\n\nThe warehouse is now active and available for operations.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            LoadWarehousesWithFilter();
                        }
                        else
                        {
                            MessageBox.Show("No warehouse was reactivated. Please try again.", "Reactivation Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reactivating warehouse: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtWarehouseName.Text))
            {
                MessageBox.Show("Please enter a warehouse name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWarehouseName.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtWarehouseName.Clear();
            txtLocation.Clear();
            txtContactPerson.Clear();
            txtContactPhone.Clear();
            chkIsActive.Checked = true;
            selectedWarehouseId = -1;
            btnAdd.Text = "➕ Add New";
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnDelete.Visible = true;
            btnDelete.Text = "🚫 Deactivate";
            btnReactivate.Enabled = false;
            btnReactivate.Visible = false;
            
            // Clear selection in DataGridView
            dgvWarehouses.ClearSelection();
        }

        // Legacy methods for backward compatibility
        private void AddWarehouseBtn_Click(object sender, EventArgs e)
        {
            btnAdd_Click(sender, e);
        }

        private void WarehousesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvWarehouses_CellClick(sender, e);
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            btnClose_Click(sender, e);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            btnAdd_Click(sender, e);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            btnClear_Click(sender, e);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            btnDelete_Click(sender, e);
        }
    }
}
