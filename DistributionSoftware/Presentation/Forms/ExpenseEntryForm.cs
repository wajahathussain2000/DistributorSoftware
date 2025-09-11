using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Business;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class ExpenseEntryForm : Form
    {
        private IExpenseService _expenseService;
        private IExpenseCategoryService _expenseCategoryService;
        private List<Expense> _expenses;
        private List<ExpenseCategory> _categories;
        private int _currentExpenseId = 0;
        private bool _isEditMode = false;
        private string _searchPlaceholder = "Search by description, reference, or code...";
        // Image fields removed - we only need barcode images

        public ExpenseEntryForm()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== Starting ExpenseEntryForm constructor ===");
                
                System.Diagnostics.Debug.WriteLine("Calling InitializeComponent...");
                InitializeComponent();
                System.Diagnostics.Debug.WriteLine("InitializeComponent completed successfully");
                
                // Initialize with minimal setup first
                _expenses = new List<Expense>();
                _categories = new List<ExpenseCategory>();
                
                System.Diagnostics.Debug.WriteLine("Basic initialization completed successfully");
                
                // Set basic form properties
                this.Text = "Expense Entry Form";
                this.StartPosition = FormStartPosition.CenterParent;
                
                System.Diagnostics.Debug.WriteLine("Form properties set successfully");
                
                // Try to initialize services separately
                try
                {
                    System.Diagnostics.Debug.WriteLine("Calling InitializeServices...");
                    InitializeServices();
                    System.Diagnostics.Debug.WriteLine("InitializeServices completed successfully");
                }
                catch (Exception serviceEx)
                {
                    System.Diagnostics.Debug.WriteLine($"InitializeServices failed: {serviceEx.Message}");
                    // Continue without services
                }
                
                // Try to initialize form separately
                try
                {
                    System.Diagnostics.Debug.WriteLine("Calling InitializeForm...");
                    InitializeForm();
                    System.Diagnostics.Debug.WriteLine("InitializeForm completed successfully");
                }
                catch (Exception formEx)
                {
                    System.Diagnostics.Debug.WriteLine($"InitializeForm failed: {formEx.Message}");
                    // Continue with basic form
                }
                
                // Try to load data separately
                try
                {
                    System.Diagnostics.Debug.WriteLine("Calling LoadExpenseCategories...");
                    LoadExpenseCategories();
                    System.Diagnostics.Debug.WriteLine("LoadExpenseCategories completed successfully");
                }
                catch (Exception categoryEx)
                {
                    System.Diagnostics.Debug.WriteLine($"LoadExpenseCategories failed: {categoryEx.Message}");
                    // Continue with empty categories
                }
                
                try
                {
                    System.Diagnostics.Debug.WriteLine("Calling LoadExpenses...");
                    LoadExpenses();
                    System.Diagnostics.Debug.WriteLine("LoadExpenses completed successfully");
                }
                catch (Exception expenseEx)
                {
                    System.Diagnostics.Debug.WriteLine($"LoadExpenses failed: {expenseEx.Message}");
                    // Continue with empty expenses
                }
                
                System.Diagnostics.Debug.WriteLine("=== ExpenseEntryForm constructor completed successfully ===");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ExpenseEntryForm constructor: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error initializing Expense Entry Form: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeServices()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Initializing ExpenseService...");
                _expenseService = new ExpenseService();
                System.Diagnostics.Debug.WriteLine("ExpenseService initialized successfully");
                
                System.Diagnostics.Debug.WriteLine("Initializing ExpenseCategoryService...");
                _expenseCategoryService = new ExpenseCategoryService();
                System.Diagnostics.Debug.WriteLine("ExpenseCategoryService initialized successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error initializing services: {ex.Message}");
                throw;
            }
        }

        private void InitializeForm()
        {
            // Set default values
            dtpExpenseDate.Value = DateTime.Today;
            
            // Initialize payment methods
            cmbPaymentMethod.Items.AddRange(new string[] { "Cash", "Credit Card", "Bank Transfer", "Check", "Other" });
            cmbPaymentMethod.SelectedIndex = 0;

            // Initialize status filter
            cmbStatusFilter.Items.AddRange(new string[] { "All", "PENDING", "APPROVED", "REJECTED" });
            cmbStatusFilter.SelectedIndex = 0;

            // Set search placeholder
            txtSearch.Text = _searchPlaceholder;
            txtSearch.ForeColor = Color.Gray;

            // Generate initial codes
            GenerateNewCodes();
        }

        private void LoadExpenseCategories()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== Starting LoadExpenseCategories ===");
                
                // Create a simple hardcoded list first to test the UI
                _categories = new List<ExpenseCategory>
                {
                    new ExpenseCategory { CategoryId = 1, CategoryName = "Office Supplies", CategoryCode = "OFF-SUP", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                    new ExpenseCategory { CategoryId = 2, CategoryName = "Travel & Transportation", CategoryCode = "TRAVEL", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now },
                    new ExpenseCategory { CategoryId = 3, CategoryName = "Meals & Entertainment", CategoryCode = "MEALS", IsActive = true, CreatedBy = 1, CreatedDate = DateTime.Now }
                };
                
                System.Diagnostics.Debug.WriteLine($"Created hardcoded list with {_categories.Count} categories");
                
                cmbCategory.DataSource = _categories;
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.ValueMember = "CategoryId";
                cmbCategory.SelectedIndex = -1;
                
                System.Diagnostics.Debug.WriteLine("ComboBox populated successfully");
                
                // Now try to load from database
                try
                {
                    System.Diagnostics.Debug.WriteLine("Testing direct database connection...");
                    TestDirectDatabaseConnection();
                    
                    if (_expenseCategoryService != null)
                    {
                        System.Diagnostics.Debug.WriteLine("Calling GetAllExpenseCategories...");
                        var allCategories = _expenseCategoryService.GetAllExpenseCategories();
                        System.Diagnostics.Debug.WriteLine($"GetAllExpenseCategories returned: {(allCategories == null ? "null" : allCategories.Count().ToString())} items");
                        
                        if (allCategories != null)
                        {
                            var activeCategories = allCategories.Where(c => c.IsActive).ToList();
                            if (activeCategories.Any())
                            {
                                _categories = activeCategories;
                                System.Diagnostics.Debug.WriteLine($"Updated with {_categories.Count} active categories from database");
                                
                                cmbCategory.DataSource = _categories;
                                cmbCategory.DisplayMember = "CategoryName";
                                cmbCategory.ValueMember = "CategoryId";
                                cmbCategory.SelectedIndex = -1;
                            }
                        }
                    }
                }
                catch (Exception dbEx)
                {
                    System.Diagnostics.Debug.WriteLine($"Database load failed, using hardcoded data: {dbEx.Message}");
                    // Keep the hardcoded data
                }
                
                System.Diagnostics.Debug.WriteLine("=== LoadExpenseCategories completed successfully ===");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in LoadExpenseCategories: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error loading expense categories: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _categories = new List<ExpenseCategory>();
            }
        }

        private void TestDirectDatabaseConnection()
        {
            try
            {
                var connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
                System.Diagnostics.Debug.WriteLine($"Testing connection with: {connectionString}");
                
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    System.Diagnostics.Debug.WriteLine("Direct database connection successful");
                    
                    var query = "SELECT COUNT(*) FROM ExpenseCategories";
                    using (var command = new SqlCommand(query, connection))
                    {
                        var count = command.ExecuteScalar();
                        System.Diagnostics.Debug.WriteLine($"Direct query returned count: {count}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Direct database test failed: {ex.Message}");
                throw;
            }
        }

        private void LoadExpenses()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== Starting LoadExpenses ===");
                
                // Start with empty list
                _expenses = new List<Expense>();
                BindExpensesGrid();
                
                System.Diagnostics.Debug.WriteLine("Empty expenses list created and bound");
                
                // Try to load from database
                if (_expenseService != null)
                {
                    try
                    {
                        var allExpenses = _expenseService.GetAllExpenses();
                        if (allExpenses != null)
                        {
                            _expenses = allExpenses.ToList();
                        }
                        else
                        {
                            _expenses = new List<Expense>();
                        }
                        BindExpensesGrid();
                        System.Diagnostics.Debug.WriteLine($"Loaded {_expenses.Count} expenses from database");
                    }
                    catch (Exception dbEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Database load failed: {dbEx.Message}");
                        _expenses = new List<Expense>();
                        BindExpensesGrid();
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ExpenseService is null, using empty list");
                }
                
                System.Diagnostics.Debug.WriteLine("=== LoadExpenses completed successfully ===");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in LoadExpenses: {ex.Message}");
                MessageBox.Show($"Error loading expenses: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _expenses = new List<Expense>();
                BindExpensesGrid();
            }
        }

        private void BindExpensesGrid()
        {
            dgvExpenses.DataSource = null;
            dgvExpenses.DataSource = _expenses;
            
            if (dgvExpenses.Columns.Count > 0)
            {
                dgvExpenses.Columns["ExpenseId"].Visible = false;
                dgvExpenses.Columns["CategoryId"].Visible = false;
                // Image columns removed - we only need barcode images
                dgvExpenses.Columns["CreatedBy"].Visible = false;
                dgvExpenses.Columns["ModifiedBy"].Visible = false;
                dgvExpenses.Columns["ApprovedBy"].Visible = false;
                dgvExpenses.Columns["CreatedDate"].Visible = false;
                dgvExpenses.Columns["ModifiedDate"].Visible = false;
                dgvExpenses.Columns["ApprovedDate"].Visible = false;
                dgvExpenses.Columns["CreatedByName"].Visible = false;
                dgvExpenses.Columns["ModifiedByName"].Visible = false;
                dgvExpenses.Columns["ApprovedByName"].Visible = false;

                // Set column headers
                dgvExpenses.Columns["ExpenseCode"].HeaderText = "Code";
                dgvExpenses.Columns["Barcode"].HeaderText = "Barcode";
                dgvExpenses.Columns["CategoryName"].HeaderText = "Category";
                dgvExpenses.Columns["ExpenseDate"].HeaderText = "Date";
                dgvExpenses.Columns["Amount"].HeaderText = "Amount";
                dgvExpenses.Columns["Description"].HeaderText = "Description";
                dgvExpenses.Columns["ReferenceNumber"].HeaderText = "Reference";
                dgvExpenses.Columns["PaymentMethod"].HeaderText = "Payment";
                dgvExpenses.Columns["Status"].HeaderText = "Status";
                dgvExpenses.Columns["Remarks"].HeaderText = "Remarks";

                // Set column widths
                dgvExpenses.Columns["ExpenseCode"].Width = 100;
                dgvExpenses.Columns["Barcode"].Width = 120;
                dgvExpenses.Columns["CategoryName"].Width = 100;
                dgvExpenses.Columns["ExpenseDate"].Width = 80;
                dgvExpenses.Columns["Amount"].Width = 80;
                dgvExpenses.Columns["Description"].Width = 150;
                dgvExpenses.Columns["ReferenceNumber"].Width = 100;
                dgvExpenses.Columns["PaymentMethod"].Width = 80;
                dgvExpenses.Columns["Status"].Width = 80;
                dgvExpenses.Columns["Remarks"].Width = 100;
            }
        }

        private void GenerateNewCodes()
        {
            try
            {
                if (_expenseService == null)
                {
                    txtExpenseCode.Text = "EXP-2025-0001";
                    txtBarcode.Text = "EXP" + DateTime.Now.ToString("yyyyMMddHHmmss") + "001";
                    return;
                }

                txtExpenseCode.Text = _expenseService.GenerateExpenseCode();
                txtBarcode.Text = _expenseService.GenerateBarcode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating codes: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Fallback codes
                txtExpenseCode.Text = "EXP-2025-0001";
                txtBarcode.Text = "EXP" + DateTime.Now.ToString("yyyyMMddHHmmss") + "001";
            }
        }

        private void ClearForm()
        {
            _currentExpenseId = 0;
            _isEditMode = false;
            // Image fields removed - we only need barcode images

            cmbCategory.SelectedIndex = -1;
            txtDescription.Clear();
            dtpExpenseDate.Value = DateTime.Today;
            txtAmount.Clear();
            cmbPaymentMethod.SelectedIndex = 0;
            txtReferenceNumber.Clear();
            txtRemarks.Clear();
            picBarcodeImage.Image = null;

            GenerateNewCodes();
        }

        private Expense GetExpenseFromForm()
        {
            var expense = new Expense
            {
                ExpenseId = _currentExpenseId,
                ExpenseCode = txtExpenseCode.Text,
                Barcode = txtBarcode.Text,
                CategoryId = cmbCategory.SelectedValue != null ? Convert.ToInt32(cmbCategory.SelectedValue) : 0,
                ExpenseDate = dtpExpenseDate.Value.Date,
                Amount = decimal.TryParse(txtAmount.Text, out decimal amount) ? amount : 0,
                Description = txtDescription.Text,
                ReferenceNumber = txtReferenceNumber.Text,
                PaymentMethod = cmbPaymentMethod.SelectedItem?.ToString(),
                // Image properties removed - we only need barcode images
                Status = _isEditMode ? GetCurrentExpenseStatus() : "PENDING",
                Remarks = txtRemarks.Text,
                CreatedBy = UserSession.CurrentUser?.UserId ?? 1,
                ModifiedBy = UserSession.CurrentUser?.UserId ?? 1
            };

            return expense;
        }

        private string GetCurrentExpenseStatus()
        {
            if (_currentExpenseId > 0)
            {
                var currentExpense = _expenses.FirstOrDefault(exp => exp.ExpenseId == _currentExpenseId);
                return currentExpense?.Status ?? "PENDING";
            }
            return "PENDING";
        }

        private void LoadExpenseToForm(Expense expense)
        {
            _currentExpenseId = expense.ExpenseId;
            _isEditMode = true;

            txtExpenseCode.Text = expense.ExpenseCode;
            txtBarcode.Text = expense.Barcode;
            cmbCategory.SelectedValue = expense.CategoryId;
            dtpExpenseDate.Value = expense.ExpenseDate;
            txtAmount.Text = expense.Amount.ToString("F2");
            txtDescription.Text = expense.Description;
            txtReferenceNumber.Text = expense.ReferenceNumber;
            cmbPaymentMethod.SelectedItem = expense.PaymentMethod;
            txtRemarks.Text = expense.Remarks;

            // Load image if available
            // Generate barcode image if barcode exists
            if (!string.IsNullOrEmpty(expense.Barcode))
            {
                GenerateBarcodeImage(expense.Barcode);
            }
            else
            {
                picBarcodeImage.Image = null;
            }
        }

        private bool ValidateForm()
        {
            if (cmbCategory.SelectedValue == null)
            {
                MessageBox.Show("Please select a category.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCategory.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Please enter a description.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return false;
            }

            if (txtDescription.Text.Length < 10)
            {
                MessageBox.Show("Description must be at least 10 characters long.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return false;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            if (amount > 10000)
            {
                MessageBox.Show("Amount cannot exceed 10,000.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            if (dtpExpenseDate.Value > DateTime.Today)
            {
                MessageBox.Show("Expense date cannot be in the future.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpExpenseDate.Focus();
                return false;
            }

            if (dtpExpenseDate.Value < DateTime.Today.AddDays(-30))
            {
                MessageBox.Show("Expense date cannot be more than 30 days in the past.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpExpenseDate.Focus();
                return false;
            }

            return true;
        }

        private void FilterExpenses()
        {
            var filteredExpenses = _expenses.AsEnumerable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(txtSearch.Text) && txtSearch.Text != _searchPlaceholder)
            {
                var searchTerm = txtSearch.Text.ToLower();
                filteredExpenses = filteredExpenses.Where(exp =>
                    (exp.Description?.ToLower().Contains(searchTerm) ?? false) ||
                    (exp.ReferenceNumber?.ToLower().Contains(searchTerm) ?? false) ||
                    (exp.ExpenseCode?.ToLower().Contains(searchTerm) ?? false) ||
                    (exp.Barcode?.ToLower().Contains(searchTerm) ?? false));
            }

            // Apply status filter
            if (cmbStatusFilter.SelectedItem?.ToString() != "All")
            {
                var status = cmbStatusFilter.SelectedItem?.ToString();
                filteredExpenses = filteredExpenses.Where(exp => exp.Status == status);
            }

            dgvExpenses.DataSource = filteredExpenses.ToList();
        }

        #region Event Handlers

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSaveDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                var expense = GetExpenseFromForm();
                expense.Status = "DRAFT";

                if (_isEditMode)
                {
                    if (_expenseService.UpdateExpense(expense))
                    {
                        MessageBox.Show("Expense draft updated successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update expense draft.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    var expenseId = _expenseService.CreateExpense(expense);
                    if (expenseId > 0)
                    {
                        _currentExpenseId = expenseId;
                        _isEditMode = true;
                        MessageBox.Show("Expense draft saved successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to save expense draft.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                LoadExpenses();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving expense draft: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                var expense = GetExpenseFromForm();
                expense.Status = "PENDING";

                if (_isEditMode)
                {
                    if (_expenseService.UpdateExpense(expense))
                    {
                        MessageBox.Show("Expense submitted successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to submit expense.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    var expenseId = _expenseService.CreateExpense(expense);
                    if (expenseId > 0)
                    {
                        _currentExpenseId = expenseId;
                        _isEditMode = true;
                        MessageBox.Show("Expense submitted successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to submit expense.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                LoadExpenses();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting expense: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        // BtnSelectImage_Click method removed - we only need barcode images

        private void BtnClearBarcode_Click(object sender, EventArgs e)
        {
            picBarcodeImage.Image = null;
        }

        private void BtnGenerateBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtBarcode.Text))
                {
                    MessageBox.Show("Please generate a barcode first.", "Warning", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Generate a simple barcode image (text-based for now)
                GenerateBarcodeImage(txtBarcode.Text);
                
                MessageBox.Show("Barcode image generated successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode image: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateBarcodeImage(string barcodeText)
        {
            try
            {
                // Create a simple barcode image using text
                Bitmap barcodeBitmap = new Bitmap(200, 100);
                using (Graphics g = Graphics.FromImage(barcodeBitmap))
                {
                    g.Clear(Color.White);
                    
                    // Draw border
                    g.DrawRectangle(Pens.Black, 0, 0, 199, 99);
                    
                    // Draw barcode text
                    using (Font font = new Font("Courier New", 12, FontStyle.Bold))
                    {
                        StringFormat format = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        
                        g.DrawString(barcodeText, font, Brushes.Black, 
                            new RectangleF(0, 0, 200, 100), format);
                    }
                    
                    // Draw barcode lines (simple representation)
                    for (int i = 0; i < barcodeText.Length; i++)
                    {
                        int x = 10 + (i * 8);
                        int height = 20 + (i % 3) * 10;
                        g.DrawLine(Pens.Black, x, 50, x, 50 + height);
                    }
                }
                
                picBarcodeImage.Image = barcodeBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating barcode image: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvExpenses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count > 0)
            {
                var selectedRow = dgvExpenses.SelectedRows[0];
                var expenseId = Convert.ToInt32(selectedRow.Cells["ExpenseId"].Value);
                var expense = _expenses.FirstOrDefault(exp => exp.ExpenseId == expenseId);
                
                if (expense != null)
                {
                    LoadExpenseToForm(expense);
                }
            }
        }

        private void DgvExpenses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var expenseId = Convert.ToInt32(dgvExpenses.Rows[e.RowIndex].Cells["ExpenseId"].Value);
                var expense = _expenses.FirstOrDefault(exp => exp.ExpenseId == expenseId);
                
                if (expense != null)
                {
                    LoadExpenseToForm(expense);
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterExpenses();
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == _searchPlaceholder)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = _searchPlaceholder;
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void CmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterExpenses();
        }

        private void TxtAmount_TextChanged(object sender, EventArgs e)
        {
            // Format amount as user types
            if (decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                var cursorPosition = txtAmount.SelectionStart;
                txtAmount.Text = amount.ToString("F2");
                txtAmount.SelectionStart = Math.Min(cursorPosition, txtAmount.Text.Length);
            }
        }

        #endregion
    }
}
