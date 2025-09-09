using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class StockAdjustmentForm : Form
    {
        private string connectionString;
        private int selectedProductId = 0;

        public StockAdjustmentForm()
        {
            InitializeComponent();
            InitializeConnection();
            LoadProducts();
            LoadAdjustments();
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        private void LoadProducts()
        {
            try
            {
                ComboBox cmbProduct = (ComboBox)this.Controls.Find("cmbProduct", true)[0];
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ProductId, ProductName, ProductCode FROM Products WHERE IsActive = 1 ORDER BY ProductName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cmbProduct.DataSource = dt;
                    cmbProduct.DisplayMember = "ProductName";
                    cmbProduct.ValueMember = "ProductId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAdjustments()
        {
            try
            {
                DataGridView dgvAdjustments = (DataGridView)this.Controls.Find("dgvAdjustments", true)[0];
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT sa.AdjustmentId, p.ProductName, sa.AdjustmentType, sa.Quantity, 
                               sa.Reason, sa.AdjustmentDate, sa.Notes
                        FROM StockAdjustments sa
                        INNER JOIN Products p ON sa.ProductId = p.ProductId
                        ORDER BY sa.AdjustmentDate DESC";
                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvAdjustments.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading adjustments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProductSelection_Changed(object sender, EventArgs e)
        {
            ComboBox cmbProduct = (ComboBox)sender;
            if (cmbProduct.SelectedValue != null)
            {
                selectedProductId = Convert.ToInt32(cmbProduct.SelectedValue);
                LoadProductStock();
            }
        }

        private void LoadProductStock()
        {
            try
            {
                Label lblCurrentStock = (Label)this.Controls.Find("lblCurrentStock", true)[0];
                
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ISNULL(SUM(Quantity), 0) as CurrentStock FROM StockMovements WHERE ProductId = @ProductId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductId", selectedProductId);
                    
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        lblCurrentStock.Text = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product stock: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PhysicalStock_TextChanged(object sender, EventArgs e)
        {
            CalculateDifference();
        }

        private void CalculateDifference()
        {
            try
            {
                TextBox txtPhysicalStock = (TextBox)this.Controls.Find("txtPhysicalStock", true)[0];
                Label lblCurrentStock = (Label)this.Controls.Find("lblCurrentStock", true)[0];
                Label lblDifference = (Label)this.Controls.Find("lblDifference", true)[0];
                
                if (int.TryParse(txtPhysicalStock.Text, out int physicalStock) && int.TryParse(lblCurrentStock.Text, out int currentStock))
                {
                    int difference = physicalStock - currentStock;
                    lblDifference.Text = difference.ToString();
                    
                    if (difference > 0)
                    {
                        lblDifference.ForeColor = Color.FromArgb(46, 204, 113); // Green for increase
                    }
                    else if (difference < 0)
                    {
                        lblDifference.ForeColor = Color.FromArgb(231, 76, 60); // Red for decrease
                    }
                    else
                    {
                        lblDifference.ForeColor = Color.Black; // Black for no change
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating difference: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            ComboBox cmbProduct = (ComboBox)this.Controls.Find("cmbProduct", true)[0];
            ComboBox cmbAdjustmentType = (ComboBox)this.Controls.Find("cmbAdjustmentType", true)[0];
            TextBox txtPhysicalStock = (TextBox)this.Controls.Find("txtPhysicalStock", true)[0];
            TextBox txtReason = (TextBox)this.Controls.Find("txtReason", true)[0];
            DateTimePicker dtpAdjustmentDate = (DateTimePicker)this.Controls.Find("dtpAdjustmentDate", true)[0];
            TextBox txtNotes = (TextBox)this.Controls.Find("txtNotes", true)[0];
            
            // Validation
            if (cmbProduct.SelectedValue == null)
            {
                MessageBox.Show("Please select a product.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhysicalStock.Text))
            {
                MessageBox.Show("Please enter physical stock quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Please enter a reason for the adjustment.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO StockAdjustments 
                        (ProductId, AdjustmentType, Quantity, Reason, AdjustmentDate, Notes, CreatedDate, CreatedBy)
                        VALUES (@ProductId, @AdjustmentType, @Quantity, @Reason, @AdjustmentDate, @Notes, GETDATE(), @CreatedBy)";
                    
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductId", selectedProductId);
                    cmd.Parameters.AddWithValue("@AdjustmentType", cmbAdjustmentType.Text);
                    cmd.Parameters.AddWithValue("@Quantity", int.Parse(txtPhysicalStock.Text));
                    cmd.Parameters.AddWithValue("@Reason", txtReason.Text);
                    cmd.Parameters.AddWithValue("@AdjustmentDate", dtpAdjustmentDate.Value);
                    cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                    cmd.Parameters.AddWithValue("@CreatedBy", GetCurrentUser());

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Stock adjustment saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadAdjustments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving adjustment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            ComboBox cmbProduct = (ComboBox)this.Controls.Find("cmbProduct", true)[0];
            ComboBox cmbAdjustmentType = (ComboBox)this.Controls.Find("cmbAdjustmentType", true)[0];
            TextBox txtPhysicalStock = (TextBox)this.Controls.Find("txtPhysicalStock", true)[0];
            TextBox txtReason = (TextBox)this.Controls.Find("txtReason", true)[0];
            TextBox txtNotes = (TextBox)this.Controls.Find("txtNotes", true)[0];
            DateTimePicker dtpAdjustmentDate = (DateTimePicker)this.Controls.Find("dtpAdjustmentDate", true)[0];
            Label lblCurrentStock = (Label)this.Controls.Find("lblCurrentStock", true)[0];
            Label lblDifference = (Label)this.Controls.Find("lblDifference", true)[0];
            
            cmbProduct.SelectedIndex = -1;
            cmbAdjustmentType.SelectedIndex = -1;
            txtPhysicalStock.Clear();
            txtReason.Clear();
            txtNotes.Clear();
            dtpAdjustmentDate.Value = DateTime.Today;
            lblCurrentStock.Text = "0";
            lblDifference.Text = "0";
            selectedProductId = 0;
        }

        private void TxtPhysicalStock_TextChanged(object sender, EventArgs e)
        {
            CalculateDifference();
        }

        private void AdjustmentsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Implementation for editing/deleting adjustments
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveBtn_Click(sender, e);
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearBtn_Click(sender, e);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // Delete adjustment logic
        }

        private string GetCurrentUser()
        {
            try
            {
                return UserSession.CurrentUser?.Username ?? "System";
            }
            catch
            {
                return "System";
            }
        }
    }
}