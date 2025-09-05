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

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Stock Adjustment - Distribution Software";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new Size(1200, 800);

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Label headerLabel = new Label
            {
                Text = "📊 Stock Adjustment",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button closeBtn = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Location = new Point(1150, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            closeBtn.Click += (s, e) => this.Close();

            headerPanel.Controls.Add(headerLabel);
            headerPanel.Controls.Add(closeBtn);

            // Main Content Panel
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Product Selection Group
            GroupBox productGroup = new GroupBox
            {
                Text = "📦 Product Selection",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1200, 120),
                Location = new Point(20, 20)
            };

            Label productLabel = new Label { Text = "🏷️ Select Product:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            ComboBox cmbProduct = new ComboBox { Name = "cmbProduct", Location = new Point(20, 55), Size = new Size(1150, 30), Font = new Font("Segoe UI", 11), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbProduct.SelectedIndexChanged += ProductSelection_Changed;

            productGroup.Controls.AddRange(new Control[] { productLabel, cmbProduct });

            // Stock Information Group
            GroupBox stockGroup = new GroupBox
            {
                Text = "📊 Stock Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1200, 250),
                Location = new Point(20, 160)
            };

            Label currentStockLabel = new Label { Text = "💾 Current System Stock:", Location = new Point(20, 40), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtCurrentStock = new TextBox { Name = "txtCurrentStock", Location = new Point(20, 65), Size = new Size(250, 30), Font = new Font("Segoe UI", 11), ReadOnly = true, BackColor = Color.FromArgb(240, 240, 240) };

            Label physicalStockLabel = new Label { Text = "🔍 Physical Stock Count:", Location = new Point(290, 40), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtPhysicalStock = new TextBox { Name = "txtPhysicalStock", Location = new Point(290, 65), Size = new Size(250, 30), Font = new Font("Segoe UI", 11) };
            txtPhysicalStock.TextChanged += PhysicalStock_TextChanged;

            Label differenceLabel = new Label { Text = "📈 Difference:", Location = new Point(560, 40), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtDifference = new TextBox { Name = "txtDifference", Location = new Point(560, 65), Size = new Size(250, 30), Font = new Font("Segoe UI", 11), ReadOnly = true, BackColor = Color.FromArgb(240, 240, 240) };

            Label adjustmentTypeLabel = new Label { Text = "🔄 Adjustment Type:", Location = new Point(830, 40), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtAdjustmentType = new TextBox { Name = "txtAdjustmentType", Location = new Point(830, 65), Size = new Size(250, 30), Font = new Font("Segoe UI", 11), ReadOnly = true, BackColor = Color.FromArgb(240, 240, 240) };

            Label reasonLabel = new Label { Text = "📝 Reason for Adjustment:", Location = new Point(20, 120), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtReason = new TextBox { Name = "txtReason", Location = new Point(20, 145), Size = new Size(1150, 60), Font = new Font("Segoe UI", 11), Multiline = true };

            stockGroup.Controls.AddRange(new Control[] { 
                currentStockLabel, txtCurrentStock, 
                physicalStockLabel, txtPhysicalStock, 
                differenceLabel, txtDifference, 
                adjustmentTypeLabel, txtAdjustmentType, 
                reasonLabel, txtReason 
            });

            // Action Buttons Group
            GroupBox actionGroup = new GroupBox
            {
                Text = "⚡ Actions",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1200, 100),
                Location = new Point(20, 430)
            };

            Button saveBtn = new Button
            {
                Text = "💾 Save Adjustment",
                Size = new Size(150, 40),
                Location = new Point(20, 30),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            saveBtn.Click += SaveBtn_Click;

            Button clearBtn = new Button
            {
                Text = "🗑️ Clear Form",
                Size = new Size(150, 40),
                Location = new Point(190, 30),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };
            clearBtn.Click += ClearBtn_Click;

            actionGroup.Controls.AddRange(new Control[] { saveBtn, clearBtn });

            // Adjustments History Group
            GroupBox historyGroup = new GroupBox
            {
                Text = "📋 Stock Adjustments History",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1420, 350),
                Location = new Point(20, 550)
            };

            DataGridView adjustmentsGrid = new DataGridView
            {
                Name = "dgvAdjustments",
                Location = new Point(20, 35),
                Size = new Size(1380, 300),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                GridColor = Color.FromArgb(189, 195, 199),
                Font = new Font("Segoe UI", 9),
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 8, FontStyle.Bold), BackColor = Color.FromArgb(52, 73, 94), ForeColor = Color.White },
                ColumnHeadersHeight = 35,
                RowHeadersVisible = false
            };

            historyGroup.Controls.Add(adjustmentsGrid);

            // Add all controls to main panel
            mainPanel.Controls.AddRange(new Control[] { 
                productGroup, stockGroup, actionGroup, historyGroup 
            });

            // Add controls to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(mainPanel);

            this.ResumeLayout(false);
        }

        private void LoadProducts()
        {
            try
            {
                ComboBox cmbProduct = (ComboBox)this.Controls.Find("cmbProduct", true)[0];
                cmbProduct.Items.Clear();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ProductId, ProductName, StockQuantity FROM Products WHERE IsActive = 1 ORDER BY ProductName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string productInfo = $"{reader["ProductName"]} (ID: {reader["ProductId"]})";
                        cmbProduct.Items.Add(productInfo);
                    }
                }

                if (cmbProduct.Items.Count > 0)
                    cmbProduct.SelectedIndex = 0;
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
                        SELECT 
                            sa.AdjustmentId,
                            p.ProductName,
                            sa.SystemQuantity,
                            sa.PhysicalQuantity,
                            sa.Difference,
                            sa.Reason,
                            sa.AdjustmentType,
                            sa.CreatedBy,
                            sa.CreatedDate
                        FROM StockAdjustments sa
                        INNER JOIN Products p ON sa.ProductId = p.ProductId
                        ORDER BY sa.CreatedDate DESC";

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
            if (cmbProduct.SelectedIndex >= 0)
            {
                string selectedItem = cmbProduct.SelectedItem.ToString();
                // Extract ProductId from "ProductName (ID: X)" format
                int startIndex = selectedItem.IndexOf("(ID: ") + 4;
                int endIndex = selectedItem.IndexOf(")");
                if (startIndex > 3 && endIndex > startIndex)
                {
                    selectedProductId = int.Parse(selectedItem.Substring(startIndex, endIndex - startIndex));
                    LoadProductStock();
                }
            }
        }

        private void LoadProductStock()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT StockQuantity FROM Products WHERE ProductId = @ProductId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductId", selectedProductId);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        TextBox txtCurrentStock = (TextBox)this.Controls.Find("txtCurrentStock", true)[0];
                        txtCurrentStock.Text = result.ToString();
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
                TextBox txtCurrentStock = (TextBox)this.Controls.Find("txtCurrentStock", true)[0];
                TextBox txtPhysicalStock = (TextBox)this.Controls.Find("txtPhysicalStock", true)[0];
                TextBox txtDifference = (TextBox)this.Controls.Find("txtDifference", true)[0];
                TextBox txtAdjustmentType = (TextBox)this.Controls.Find("txtAdjustmentType", true)[0];

                if (int.TryParse(txtCurrentStock.Text, out int currentStock) && 
                    int.TryParse(txtPhysicalStock.Text, out int physicalStock))
                {
                    int difference = physicalStock - currentStock;
                    txtDifference.Text = difference.ToString();

                    if (difference > 0)
                    {
                        txtAdjustmentType.Text = "Increase";
                        txtAdjustmentType.BackColor = Color.FromArgb(46, 204, 113);
                        txtAdjustmentType.ForeColor = Color.White;
                    }
                    else if (difference < 0)
                    {
                        txtAdjustmentType.Text = "Decrease";
                        txtAdjustmentType.BackColor = Color.FromArgb(231, 76, 60);
                        txtAdjustmentType.ForeColor = Color.White;
                    }
                    else
                    {
                        txtAdjustmentType.Text = "No Change";
                        txtAdjustmentType.BackColor = Color.FromArgb(240, 240, 240);
                        txtAdjustmentType.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle calculation errors silently
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            // Validation
            if (selectedProductId == 0)
            {
                MessageBox.Show("Please select a product.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TextBox txtPhysicalStock = (TextBox)this.Controls.Find("txtPhysicalStock", true)[0];
            TextBox txtReason = (TextBox)this.Controls.Find("txtReason", true)[0];

            if (string.IsNullOrWhiteSpace(txtPhysicalStock.Text))
            {
                MessageBox.Show("Please enter the physical stock count.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Please provide a reason for the adjustment.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtPhysicalStock.Text, out int physicalStock))
            {
                MessageBox.Show("Please enter a valid number for physical stock.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Get current stock
                            string getStockQuery = "SELECT StockQuantity FROM Products WHERE ProductId = @ProductId";
                            SqlCommand getStockCmd = new SqlCommand(getStockQuery, conn, transaction);
                            getStockCmd.Parameters.AddWithValue("@ProductId", selectedProductId);
                            int currentStock = Convert.ToInt32(getStockCmd.ExecuteScalar());

                            int difference = physicalStock - currentStock;
                            string adjustmentType = difference > 0 ? "Increase" : "Decrease";

                            // Insert into StockAdjustments
                            string insertQuery = @"
                                INSERT INTO StockAdjustments 
                                (ProductId, SystemQuantity, PhysicalQuantity, Difference, Reason, AdjustmentType, CreatedBy, CreatedDate)
                                VALUES (@ProductId, @SystemQuantity, @PhysicalQuantity, @Difference, @Reason, @AdjustmentType, @CreatedBy, @CreatedDate)";

                            SqlCommand insertCmd = new SqlCommand(insertQuery, conn, transaction);
                            insertCmd.Parameters.AddWithValue("@ProductId", selectedProductId);
                            insertCmd.Parameters.AddWithValue("@SystemQuantity", currentStock);
                            insertCmd.Parameters.AddWithValue("@PhysicalQuantity", physicalStock);
                            insertCmd.Parameters.AddWithValue("@Difference", difference);
                            insertCmd.Parameters.AddWithValue("@Reason", txtReason.Text.Trim());
                            insertCmd.Parameters.AddWithValue("@AdjustmentType", adjustmentType);
                            insertCmd.Parameters.AddWithValue("@CreatedBy", "Admin"); // TODO: Get from user session
                            insertCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                            insertCmd.ExecuteNonQuery();

                            // Update Products table
                            string updateQuery = "UPDATE Products SET StockQuantity = @StockQuantity WHERE ProductId = @ProductId";
                            SqlCommand updateCmd = new SqlCommand(updateQuery, conn, transaction);
                            updateCmd.Parameters.AddWithValue("@StockQuantity", physicalStock);
                            updateCmd.Parameters.AddWithValue("@ProductId", selectedProductId);

                            updateCmd.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Stock adjustment saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            LoadAdjustments();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
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
            TextBox txtPhysicalStock = (TextBox)this.Controls.Find("txtPhysicalStock", true)[0];
            TextBox txtReason = (TextBox)this.Controls.Find("txtReason", true)[0];
            TextBox txtDifference = (TextBox)this.Controls.Find("txtDifference", true)[0];
            TextBox txtAdjustmentType = (TextBox)this.Controls.Find("txtAdjustmentType", true)[0];

            txtPhysicalStock.Clear();
            txtReason.Clear();
            txtDifference.Clear();
            txtAdjustmentType.Clear();
            txtAdjustmentType.BackColor = Color.FromArgb(240, 240, 240);
            txtAdjustmentType.ForeColor = Color.Black;
        }
    }
}

