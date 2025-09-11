using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class ExpenseCategoryMasterForm : Form
    {
        private IExpenseCategoryService _expenseCategoryService;
        private List<ExpenseCategory> _categories;
        private int _currentCategoryId = 0;
        private bool _isEditMode = false;

        public ExpenseCategoryMasterForm()
        {
            InitializeComponent();
            InitializeServices();
            LoadCategoriesGrid();
            InitializeStatusFilter();
            
            // Subscribe to form load event
            this.Load += ExpenseCategoryMasterForm_Load;
        }

        private void ExpenseCategoryMasterForm_Load(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void InitializeServices()
        {
            try
            {
                _expenseCategoryService = new ExpenseCategoryService();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing services: {ex.Message}", "Initialization Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeStatusFilter()
        {
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add("All");
            cmbStatusFilter.Items.Add("Active");
            cmbStatusFilter.Items.Add("Inactive");
            cmbStatusFilter.SelectedIndex = 0;

            // Set placeholder text for search box
            txtSearch.Text = "Search categories...";
            txtSearch.ForeColor = Color.Gray;
            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;

            // Add event handler for auto-generating category code
            txtCategoryName.TextChanged += TxtCategoryName_TextChanged;
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search categories...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search categories...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void LoadCategoriesGrid()
        {
            try
            {
                _categories = _expenseCategoryService.GetAllExpenseCategories();
                BindCategoriesGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindCategoriesGrid()
        {
            try
            {
                dgvCategories.DataSource = null;
                dgvCategories.Rows.Clear();
                dgvCategories.Columns.Clear();

                if (_categories == null || !_categories.Any())
                {
                    return;
                }

                // Create columns
                dgvCategories.Columns.Add("CategoryId", "ID");
                dgvCategories.Columns["CategoryId"].Visible = false;

                dgvCategories.Columns.Add("CategoryName", "Category Name");
                dgvCategories.Columns["CategoryName"].Width = 150;
                dgvCategories.Columns["CategoryName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dgvCategories.Columns.Add("CategoryCode", "Code");
                dgvCategories.Columns["CategoryCode"].Width = 80;
                dgvCategories.Columns["CategoryCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvCategories.Columns.Add("Description", "Description");
                dgvCategories.Columns["Description"].Width = 200;
                dgvCategories.Columns["Description"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dgvCategories.Columns.Add("Status", "Status");
                dgvCategories.Columns["Status"].Width = 80;
                dgvCategories.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvCategories.Columns.Add("CreatedDate", "Created Date");
                dgvCategories.Columns["CreatedDate"].Width = 120;
                dgvCategories.Columns["CreatedDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Add rows
                foreach (var category in _categories)
                {
                    dgvCategories.Rows.Add(
                        category.CategoryId,
                        category.CategoryName,
                        category.CategoryCode ?? "",
                        category.Description ?? "",
                        category.StatusText,
                        category.FormattedCreatedDate
                    );
                }

                // Style the grid
                StyleDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error binding categories grid: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StyleDataGrid()
        {
            dgvCategories.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgvCategories.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvCategories.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvCategories.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCategories.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvCategories.GridColor = Color.FromArgb(220, 220, 220);
            dgvCategories.BorderStyle = BorderStyle.FixedSingle;
            dgvCategories.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.MultiSelect = false;
            dgvCategories.ReadOnly = true;
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.RowHeadersVisible = false;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadCategoryToForm(ExpenseCategory category)
        {
            try
            {
                if (category == null) return;

                _currentCategoryId = category.CategoryId;
                _isEditMode = true;

                txtCategoryName.Text = category.CategoryName;
                txtCategoryCode.Text = category.CategoryCode ?? "";
                txtDescription.Text = category.Description ?? "";
                chkIsActive.Checked = category.IsActive;

                // Update button text
                btnSave.Text = "✏️ Update";
                btnSave.BackColor = Color.FromArgb(52, 152, 219); // Blue for edit
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading category to form: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ExpenseCategory GetCategoryFromForm()
        {
            var category = new ExpenseCategory
            {
                CategoryId = _currentCategoryId,
                CategoryName = txtCategoryName.Text.Trim(),
                CategoryCode = string.IsNullOrWhiteSpace(txtCategoryCode.Text) ? null : txtCategoryCode.Text.Trim(),
                Description = string.IsNullOrWhiteSpace(txtDescription.Text) ? null : txtDescription.Text.Trim(),
                IsActive = chkIsActive.Checked,
                CreatedBy = UserSession.CurrentUser?.UserId ?? 1,
                ModifiedBy = UserSession.CurrentUser?.UserId ?? 1
            };

            // Ensure category code is generated if not present
            if (string.IsNullOrWhiteSpace(category.CategoryCode) && !string.IsNullOrWhiteSpace(category.CategoryName))
            {
                try
                {
                    category.CategoryCode = _expenseCategoryService.GenerateCategoryCode(category.CategoryName);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error generating category code in GetCategoryFromForm: {ex.Message}");
                }
            }

            return category;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Category name is required.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return false;
            }

            if (txtCategoryName.Text.Length > 100)
            {
                MessageBox.Show("Category name cannot exceed 100 characters.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtCategoryCode.Text) && txtCategoryCode.Text.Length > 20)
            {
                MessageBox.Show("Category code cannot exceed 20 characters.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryCode.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtDescription.Text) && txtDescription.Text.Length > 255)
            {
                MessageBox.Show("Description cannot exceed 255 characters.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            _currentCategoryId = 0;
            _isEditMode = false;

            txtCategoryName.Clear();
            txtCategoryCode.Clear();
            txtDescription.Clear();
            chkIsActive.Checked = true;

            // Reset button
            btnSave.Text = "💾 Save";
            btnSave.BackColor = Color.FromArgb(46, 204, 113); // Green for save

            // Clear selection
            dgvCategories.ClearSelection();
        }

        private void FilterCategories()
        {
            try
            {
                // Reload all categories first
                var allCategories = _expenseCategoryService.GetAllExpenseCategories();
                var searchTerm = txtSearch.Text.Trim();
                
                // Ignore placeholder text
                if (searchTerm == "Search categories...")
                {
                    searchTerm = "";
                }
                
                var statusFilter = cmbStatusFilter.SelectedItem?.ToString();

                var filteredCategories = allCategories.AsEnumerable();

                // Apply search filter
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    filteredCategories = filteredCategories.Where(c => 
                        c.CategoryName.ToLower().Contains(searchTerm.ToLower()) ||
                        (c.CategoryCode != null && c.CategoryCode.ToLower().Contains(searchTerm.ToLower())) ||
                        (c.Description != null && c.Description.ToLower().Contains(searchTerm.ToLower())));
                }

                // Apply status filter
                if (statusFilter == "Active")
                {
                    filteredCategories = filteredCategories.Where(c => c.IsActive);
                }
                else if (statusFilter == "Inactive")
                {
                    filteredCategories = filteredCategories.Where(c => !c.IsActive);
                }

                _categories = filteredCategories.ToList();
                BindCategoriesGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering categories: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event Handlers
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                var category = GetCategoryFromForm();
                bool success;
                string message;

                if (_isEditMode)
                {
                    success = _expenseCategoryService.UpdateExpenseCategory(category);
                    message = success ? "Category updated successfully!" : "Failed to update category.";
                }
                else
                {
                    success = _expenseCategoryService.CreateExpenseCategory(category);
                    message = success ? "Category created successfully!" : "Failed to create category.";
                }

                if (success)
                {
                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCategoriesGrid();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving category: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (_currentCategoryId == 0)
                {
                    MessageBox.Show("Please select a category to delete.", "No Selection", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to delete this category?", 
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool success = _expenseCategoryService.DeleteExpenseCategory(_currentCategoryId);
                    string message = success ? "Category deleted successfully!" : "Failed to delete category.";

                    if (success)
                    {
                        MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCategoriesGrid();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting category: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count > 0)
            {
                var selectedRow = dgvCategories.SelectedRows[0];
                if (selectedRow.Cells["CategoryId"].Value != null)
                {
                    var categoryId = Convert.ToInt32(selectedRow.Cells["CategoryId"].Value);
                    var category = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
                    if (category != null)
                    {
                        LoadCategoryToForm(category);
                    }
                }
            }
        }

        private void DgvCategories_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dgvCategories.Rows[e.RowIndex];
                if (selectedRow.Cells["CategoryId"].Value != null)
                {
                    var categoryId = Convert.ToInt32(selectedRow.Cells["CategoryId"].Value);
                    var category = _categories.FirstOrDefault(c => c.CategoryId == categoryId);
                    if (category != null)
                    {
                        LoadCategoryToForm(category);
                    }
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterCategories();
        }

        private void CmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterCategories();
        }

        private void TxtCategoryName_TextChanged(object sender, EventArgs e)
        {
            // Auto-generate category code when category name changes
            if (!_isEditMode && !string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                try
                {
                    var generatedCode = _expenseCategoryService.GenerateCategoryCode(txtCategoryName.Text);
                    txtCategoryCode.Text = generatedCode;
                }
                catch (Exception ex)
                {
                    // If generation fails, clear the code field
                    txtCategoryCode.Text = "";
                    System.Diagnostics.Debug.WriteLine($"Error generating category code: {ex.Message}");
                }
            }
            else if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                txtCategoryCode.Text = "";
            }
        }
    }
}
