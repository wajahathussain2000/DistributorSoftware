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

        public WarehouseForm()
        {
            InitializeComponent();
            InitializeConnection();
            LoadWarehouses();
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        private void LoadWarehouses()
        {
            try
            {
                DataGridView dgvWarehouses = (DataGridView)this.Controls.Find("dgvWarehouses", true)[0];
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT WarehouseId, WarehouseName, Location, Description, IsActive, CreatedDate FROM Warehouses ORDER BY WarehouseName";
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

        private void AddWarehouseBtn_Click(object sender, EventArgs e)
        {
            TextBox txtWarehouseName = (TextBox)this.Controls.Find("txtWarehouseName", true)[0];
            TextBox txtLocation = (TextBox)this.Controls.Find("txtLocation", true)[0];
            TextBox txtDescription = (TextBox)this.Controls.Find("txtDescription", true)[0];
            
            if (string.IsNullOrWhiteSpace(txtWarehouseName.Text))
            {
                MessageBox.Show("Please enter a warehouse name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Warehouses 
                        (WarehouseName, Location, Description, IsActive, CreatedDate)
                        VALUES (@Name, @Location, @Description, 1, GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtWarehouseName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Warehouse added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadWarehouses();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding warehouse: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WarehousesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Implementation for editing/deleting warehouses
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            AddWarehouseBtn_Click(sender, e);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // Delete functionality
            DataGridView dgvWarehouses = (DataGridView)this.Controls.Find("dgvWarehouses", true)[0];
            if (dgvWarehouses.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Are you sure you want to delete this warehouse?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Implement delete logic here
                    LoadWarehouses();
                }
            }
        }

        private void ClearForm()
        {
            TextBox txtWarehouseName = (TextBox)this.Controls.Find("txtWarehouseName", true)[0];
            TextBox txtLocation = (TextBox)this.Controls.Find("txtLocation", true)[0];
            TextBox txtDescription = (TextBox)this.Controls.Find("txtDescription", true)[0];
            
            txtWarehouseName.Clear();
            txtLocation.Clear();
            txtDescription.Clear();
        }
    }
}