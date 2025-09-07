using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class CustomerCategoryForm : Form
    {
        private SqlConnection connection;
        private string connectionString;
        private int currentCategoryId = 0;
        private Button btnSave;
        private DataGridView dgvCategories;
        private bool isEditMode = false;

        public CustomerCategoryForm()
        {
            InitializeComponent();
            InitializeForm();
            InitializeConnection();
            LoadCategoriesGrid();
            
            // Subscribe to form load event to clear form after all controls are initialized
            this.Load += CustomerCategoryForm_Load;
        }

        private void CustomerCategoryForm_Load(object sender, EventArgs e)
        {
            // Clear form after all controls are fully initialized
            System.Diagnostics.Debug.WriteLine("Customer Category Form Load event fired - clearing form now");
            ClearForm();
        }

        private void InitializeConnection()
        {
            connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
        }

        private void InitializeForm()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Customer Category Master - Distribution Software";
            this.Size = new Size(1200, 700);
            this.MinimumSize = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 73, 94),
                Padding = new Padding(20, 0, 20, 0)
            };

            Label titleLabel = new Label
            {
                Text = "🏷️ Customer Category Master",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button closeBtn = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.Width - 80, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            closeBtn.Click += (s, e) => this.Close();

            headerPanel.Controls.AddRange(new Control[] { titleLabel, closeBtn });

            // Content Panel
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true
            };

            // Category Information Group
            GroupBox categoryGroup = new GroupBox
            {
                Text = "📋 Category Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(750, 200),
                Location = new Point(20, 100),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            // Category Name
            Label nameLabel = new Label { Text = "🏷️ Category Name:", Location = new Point(20, 30), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtCategoryName = new TextBox { Name = "txtCategoryName", Location = new Point(200, 28), Size = new Size(300, 25), Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            // Description
            Label descLabel = new Label { Text = "📝 Description:", Location = new Point(20, 70), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            TextBox txtDescription = new TextBox { Name = "txtDescription", Location = new Point(200, 68), Size = new Size(500, 100), Multiline = true, ScrollBars = ScrollBars.Vertical, Font = new Font("Segoe UI", 9, FontStyle.Regular) };

            categoryGroup.Controls.AddRange(new Control[] {
                nameLabel, txtCategoryName, descLabel, txtDescription
            });

            // Categories List Group (Right side)
            GroupBox categoryListGroup = new GroupBox
            {
                Text = "📋 Categories List",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(400, 400),
                Location = new Point(790, 100),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                BackColor = Color.FromArgb(240, 248, 255) // Light blue background
            };

            dgvCategories = new DataGridView
            {
                Name = "dgvCategories",
                Location = new Point(20, 30),
                Size = new Size(360, 350),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                ScrollBars = ScrollBars.Both,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                GridColor = Color.FromArgb(189, 195, 199),
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ColumnHeadersHeight = 30,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    SelectionBackColor = Color.FromArgb(52, 152, 219),
                    SelectionForeColor = Color.White
                }
            };
            dgvCategories.SelectionChanged += DgvCategories_SelectionChanged;

            categoryListGroup.Controls.Add(dgvCategories);

            // Actions Group
            GroupBox actionsGroup = new GroupBox
            {
                Text = "⚡ Actions",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(750, 80),
                Location = new Point(20, 320),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            btnSave = new Button
            {
                Text = "💾 Save",
                Location = new Point(20, 30),
                Size = new Size(100, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;

            Button btnClear = new Button
            {
                Text = "🗑️ Clear",
                Location = new Point(140, 30),
                Size = new Size(100, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(241, 196, 15),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnClear.Click += BtnClear_Click;

            Button btnDelete = new Button
            {
                Text = "🗑️ Delete",
                Location = new Point(260, 30),
                Size = new Size(100, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnDelete.Click += BtnDelete_Click;

            actionsGroup.Controls.AddRange(new Control[] { btnSave, btnClear, btnDelete });

            contentPanel.Controls.AddRange(new Control[] {
                categoryGroup, categoryListGroup, actionsGroup
            });

            this.Controls.Add(headerPanel);
            this.Controls.Add(contentPanel);

            this.ResumeLayout(false);
        }

        private void LoadCategoriesGrid()
        {
            try
            {
                if (dgvCategories == null) return;

                string query = @"
                    SELECT CategoryId, CategoryName, Description, IsActive, CreatedDate
                    FROM CustomerCategories 
                    WHERE IsActive = 1 
                    ORDER BY CategoryName";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Temporarily disable selection change event to prevent auto-population
                            dgvCategories.SelectionChanged -= DgvCategories_SelectionChanged;
                            
                            dgvCategories.DataSource = dataTable;
                            
                            // Set proper column headers
                            if (dgvCategories.Columns.Count > 0)
                            {
                                dgvCategories.Columns["CategoryId"].HeaderText = "ID";
                                dgvCategories.Columns["CategoryName"].HeaderText = "Category Name";
                                dgvCategories.Columns["Description"].HeaderText = "Description";
                                dgvCategories.Columns["IsActive"].Visible = false;
                                dgvCategories.Columns["CreatedDate"].Visible = false;
                            }
                            
                            dgvCategories.AutoResizeColumns();
                            
                            // Clear any selection to prevent auto-population
                            dgvCategories.ClearSelection();
                            
                            // Re-enable selection change event
                            dgvCategories.SelectionChanged += DgvCategories_SelectionChanged;
                            
                            // Debug: Show category count in form title
                            this.Text = $"Customer Category Master - Distribution Software ({dataTable.Rows.Count} categories)";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgvCategories = sender as DataGridView;
                if (dgvCategories.SelectedRows.Count > 0)
                {
                    DataRowView row = dgvCategories.SelectedRows[0].DataBoundItem as DataRowView;
                    if (row != null)
                    {
                        LoadCategoryToForm(row.Row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategoryToForm(DataRow row)
        {
            try
            {
                // Debug: Log the row data
                System.Diagnostics.Debug.WriteLine("Loading category data:");
                foreach (DataColumn column in row.Table.Columns)
                {
                    System.Diagnostics.Debug.WriteLine($"  {column.ColumnName}: {row[column.ColumnName]} (Type: {row[column.ColumnName].GetType()})");
                }

                currentCategoryId = Convert.ToInt32(row["CategoryId"]);

                TextBox txtCategoryName = FindControlRecursive(this, "txtCategoryName") as TextBox;
                TextBox txtDescription = FindControlRecursive(this, "txtDescription") as TextBox;

                // Safe string assignments with DBNull handling
                txtCategoryName.Text = row["CategoryName"] == DBNull.Value ? "" : row["CategoryName"].ToString();
                txtDescription.Text = row["Description"] == DBNull.Value ? "" : row["Description"].ToString();

                // Debug: Log loaded field values
                System.Diagnostics.Debug.WriteLine($"Loaded category data - Name: {txtCategoryName.Text}, Description: {txtDescription.Text}");

                // Set edit mode and update button text
                isEditMode = true;
                if (btnSave != null)
                {
                    btnSave.Text = "✏️ Edit";
                    btnSave.BackColor = Color.FromArgb(52, 152, 219); // Blue color for edit
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading category data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    if (currentCategoryId == 0)
                    {
                        InsertCategory();
                    }
                    else
                    {
                        UpdateCategory();
                    }
                    LoadCategoriesGrid();
                    ClearForm();
                    string message = isEditMode ? "Category updated successfully!" : "Category saved successfully!";
                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentCategoryId == 0)
                {
                    MessageBox.Show("Please select a category to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    DeleteCategory();
                    LoadCategoriesGrid();
                    ClearForm();
                    MessageBox.Show("Category deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            TextBox txtCategoryName = FindControlRecursive(this, "txtCategoryName") as TextBox;

            if (string.IsNullOrWhiteSpace(txtCategoryName?.Text))
            {
                MessageBox.Show("Category Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName?.Focus();
                return false;
            }

            return true;
        }

        private void InsertCategory()
        {
            string query = @"
                INSERT INTO CustomerCategories (CategoryName, Description, IsActive, CreatedDate)
                VALUES (@CategoryName, @Description, 1, GETDATE())";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    SetParameters(command);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void UpdateCategory()
        {
            string query = @"
                UPDATE CustomerCategories SET 
                    CategoryName = @CategoryName,
                    Description = @Description
                WHERE CategoryId = @CategoryId";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    SetParameters(command);
                    command.Parameters.AddWithValue("@CategoryId", currentCategoryId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void DeleteCategory()
        {
            string query = "UPDATE CustomerCategories SET IsActive = 0 WHERE CategoryId = @CategoryId";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", currentCategoryId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void SetParameters(SqlCommand command)
        {
            TextBox txtCategoryName = FindControlRecursive(this, "txtCategoryName") as TextBox;
            TextBox txtDescription = FindControlRecursive(this, "txtDescription") as TextBox;

            command.Parameters.AddWithValue("@CategoryName", txtCategoryName?.Text ?? "");
            command.Parameters.AddWithValue("@Description", txtDescription?.Text ?? "");
        }

        private void ClearForm()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("ClearForm() method called");
                currentCategoryId = 0;

                TextBox txtCategoryName = FindControlRecursive(this, "txtCategoryName") as TextBox;
                TextBox txtDescription = FindControlRecursive(this, "txtDescription") as TextBox;

                txtCategoryName.Clear();
                txtDescription.Clear();

                // Reset to save mode
                isEditMode = false;
                if (btnSave != null)
                {
                    btnSave.Text = "💾 Save";
                    btnSave.BackColor = Color.FromArgb(46, 204, 113); // Green color for save
                }

                // Debug: Verify form is cleared
                System.Diagnostics.Debug.WriteLine("Form cleared successfully - all fields should be empty");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Control FindControlRecursive(Control parent, string name)
        {
            if (parent.Name == name)
                return parent;

            foreach (Control control in parent.Controls)
            {
                Control found = FindControlRecursive(control, name);
                if (found != null)
                    return found;
            }
            return null;
        }
    }
}
