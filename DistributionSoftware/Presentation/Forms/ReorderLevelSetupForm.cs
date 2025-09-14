using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class ReorderLevelSetupForm : Form
    {
        #region Private Fields
        
        private IReorderLevelService _reorderLevelService;
        private IProductService _productService;
        private ReorderLevel _currentReorderLevel;
        private bool _isEditMode = false;
        private List<ReorderLevel> _reorderLevels;
        private List<Product> _products;
        
        #endregion
        
        #region Constructor
        
        public ReorderLevelSetupForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeForm();
        }
        
        #endregion
        
        #region Initialization
        
        private void InitializeServices()
        {
            _reorderLevelService = new ReorderLevelService();
            _productService = new ProductService();
        }
        
        private void InitializeForm()
        {
            LoadProducts();
            LoadReorderLevels();
            SetDefaultValues();
            UpdateButtonState();
        }
        
        #endregion
        
        #region Data Loading
        
        private void LoadProducts()
        {
            try
            {
                _products = _productService.GetAllProducts();
                cmbProduct.DataSource = _products;
                cmbProduct.DisplayMember = "ProductName";
                cmbProduct.ValueMember = "ProductId";
                cmbProduct.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadReorderLevels()
        {
            try
            {
                _reorderLevels = _reorderLevelService.GetAllReorderLevels();
                
                // Check if DataGridView is valid
                if (dgvReorderLevels == null || dgvReorderLevels.IsDisposed)
                    return;
                
                // Clear existing data source first
                dgvReorderLevels.DataSource = null;
                dgvReorderLevels.Columns.Clear();
                
                // Set the data source
                dgvReorderLevels.DataSource = _reorderLevels;
                
                // Column updates will be handled by the DataBindingComplete event
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reorder levels: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void UpdateGridColumns()
        {
            try
            {
                // Check if DataGridView is in a valid state
                if (dgvReorderLevels == null || dgvReorderLevels.IsDisposed || dgvReorderLevels.Columns == null)
                    return;
                
                // Check if DataGridView is in a valid state for column operations
                if (!dgvReorderLevels.IsHandleCreated || dgvReorderLevels.InvokeRequired)
                    return;
                
                // Wait a bit for the DataGridView to finish binding
                System.Threading.Thread.Sleep(50);
                
                if (dgvReorderLevels.Columns.Count == 0)
                    return;

                // Hide system columns
                var columnsToHide = new[] { "ReorderLevelId", "ProductId", "CreatedBy", "CreatedByName", 
                                          "ModifiedBy", "ModifiedByName", "CreatedDate", "ModifiedDate", "LastAlertDate" };
                
                foreach (var columnName in columnsToHide)
                {
                    if (dgvReorderLevels.Columns.Contains(columnName))
                    {
                        var column = dgvReorderLevels.Columns[columnName];
                        if (column != null)
                        {
                            try
                            {
                                column.Visible = false;
                            }
                            catch (Exception ex)
                            {
                                DebugHelper.WriteException($"Error hiding column {columnName}", ex);
                            }
                        }
                    }
                }
                
                // Set column headers and widths
                var columnSettings = new Dictionary<string, (string HeaderText, int Width)>
                {
                    { "Product", ("Product", 200) },
                    { "MinimumLevel", ("Min Level", 80) },
                    { "MaximumLevel", ("Max Level", 80) },
                    { "ReorderQuantity", ("Reorder Qty", 80) },
                    { "IsActive", ("Active", 60) },
                    { "AlertEnabled", ("Alerts", 60) }
                };
                
                foreach (var setting in columnSettings)
                {
                    if (dgvReorderLevels.Columns.Contains(setting.Key))
                    {
                        var column = dgvReorderLevels.Columns[setting.Key];
                        if (column != null)
                        {
                            try
                            {
                                column.HeaderText = setting.Value.HeaderText;
                            }
                            catch (Exception ex)
                            {
                                DebugHelper.WriteException($"Error setting HeaderText for column {setting.Key}", ex);
                            }
                            
                            try
                            {
                                // Additional safety check before setting width
                                if (column.Width != setting.Value.Width)
                                {
                                    column.Width = setting.Value.Width;
                                }
                            }
                            catch (Exception ex)
                            {
                                DebugHelper.WriteException($"Error setting Width for column {setting.Key}", ex);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReorderLevelSetupForm.UpdateGridColumns", ex);
                // Don't show error to user as this is just column formatting
            }
        }
        
        #endregion
        
        #region Form Management
        
        private void SetDefaultValues()
        {
            txtMinimumLevel.Text = "0";
            txtMaximumLevel.Text = "0";
            txtReorderQuantity.Text = "0";
            chkIsActive.Checked = true;
            chkAlertEnabled.Checked = true;
        }
        
        private void ClearForm()
        {
            cmbProduct.SelectedIndex = -1;
            txtMinimumLevel.Text = "0";
            txtMaximumLevel.Text = "0";
            txtReorderQuantity.Text = "0";
            chkIsActive.Checked = true;
            chkAlertEnabled.Checked = true;
            _currentReorderLevel = null;
            _isEditMode = false;
            UpdateButtonState();
        }
        
        private void UpdateButtonState()
        {
            btnSave.Enabled = _isEditMode;
            btnCancel.Enabled = _isEditMode;
            btnNew.Enabled = !_isEditMode;
            btnEdit.Enabled = !_isEditMode && _currentReorderLevel != null;
            btnDelete.Enabled = !_isEditMode && _currentReorderLevel != null;
        }
        
        #endregion
        
        #region Event Handlers
        
        private void BtnNew_Click(object sender, EventArgs e)
        {
            _currentReorderLevel = new ReorderLevel();
            _isEditMode = true;
            SetDefaultValues();
            UpdateButtonState();
            cmbProduct.Focus();
        }
        
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_currentReorderLevel == null)
            {
                MessageBox.Show("Please select a reorder level to edit.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            _isEditMode = true;
            LoadReorderLevelToForm();
            UpdateButtonState();
        }
        
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_currentReorderLevel == null)
            {
                MessageBox.Show("Please select a reorder level to delete.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var result = MessageBox.Show($"Are you sure you want to delete the reorder level for {_currentReorderLevel.Product?.ProductName}?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = _reorderLevelService.DeleteReorderLevel(_currentReorderLevel.ReorderLevelId);
                    if (success)
                    {
                        MessageBox.Show("Reorder level deleted successfully.", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReorderLevels();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete reorder level.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting reorder level: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                {
                    return;
                }
                
                UpdateReorderLevelFromForm();
                
                bool success;
                if (_currentReorderLevel.ReorderLevelId == 0)
                {
                    var reorderLevelId = _reorderLevelService.CreateReorderLevel(_currentReorderLevel);
                    success = reorderLevelId > 0;
                    if (success)
                    {
                        _currentReorderLevel.ReorderLevelId = reorderLevelId;
                    }
                }
                else
                {
                    success = _reorderLevelService.UpdateReorderLevel(_currentReorderLevel);
                }
                
                if (success)
                {
                    MessageBox.Show("Reorder level saved successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isEditMode = false;
                    LoadReorderLevels();
                    UpdateButtonState();
                }
                else
                {
                    MessageBox.Show("Failed to save reorder level.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving reorder level: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            _isEditMode = false;
            ClearForm();
        }
        
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void DgvReorderLevels_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReorderLevels.SelectedRows.Count > 0)
            {
                var selectedRow = dgvReorderLevels.SelectedRows[0];
                var reorderLevelId = Convert.ToInt32(selectedRow.Cells["ReorderLevelId"].Value);
                _currentReorderLevel = _reorderLevels.FirstOrDefault(rl => rl.ReorderLevelId == reorderLevelId);
                LoadReorderLevelToForm();
                UpdateButtonState();
            }
        }
        
        private void DgvReorderLevels_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // This event fires after data binding is complete, making it safe to modify columns
            try
            {
                UpdateGridColumns();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("Error in DataBindingComplete event", ex);
            }
        }
        
        private void CmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedValue != null && _isEditMode)
            {
                var productId = (int)cmbProduct.SelectedValue;
                var product = _products.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    // Show current stock information
                    lblCurrentStock.Text = $"Current Stock: {product.StockQuantity}";
                    lblCurrentStock.Visible = true;
                    
                    // Auto-suggest reorder levels based on current stock
                    if (_currentReorderLevel.ReorderLevelId == 0) // New record
                    {
                        txtMinimumLevel.Text = Math.Max(0, product.StockQuantity * 0.2m).ToString("0");
                        txtMaximumLevel.Text = (product.StockQuantity * 2).ToString("0");
                        txtReorderQuantity.Text = product.StockQuantity.ToString("0");
                    }
                }
            }
        }
        
        #endregion
        
        #region Helper Methods
        
        private void LoadReorderLevelToForm()
        {
            if (_currentReorderLevel != null)
            {
                cmbProduct.SelectedValue = _currentReorderLevel.ProductId;
                txtMinimumLevel.Text = _currentReorderLevel.MinimumLevel.ToString("0");
                txtMaximumLevel.Text = _currentReorderLevel.MaximumLevel.ToString("0");
                txtReorderQuantity.Text = _currentReorderLevel.ReorderQuantity.ToString("0");
                chkIsActive.Checked = _currentReorderLevel.IsActive;
                chkAlertEnabled.Checked = _currentReorderLevel.AlertEnabled;
                
                // Show current stock
                if (_currentReorderLevel.Product != null)
                {
                    lblCurrentStock.Text = $"Current Stock: {_currentReorderLevel.Product.StockQuantity}";
                    lblCurrentStock.Visible = true;
                }
            }
        }
        
        private void UpdateReorderLevelFromForm()
        {
            if (_currentReorderLevel != null)
            {
                _currentReorderLevel.ProductId = (int)cmbProduct.SelectedValue;
                _currentReorderLevel.MinimumLevel = Convert.ToDecimal(txtMinimumLevel.Text);
                _currentReorderLevel.MaximumLevel = Convert.ToDecimal(txtMaximumLevel.Text);
                _currentReorderLevel.ReorderQuantity = Convert.ToDecimal(txtReorderQuantity.Text);
                _currentReorderLevel.IsActive = chkIsActive.Checked;
                _currentReorderLevel.AlertEnabled = chkAlertEnabled.Checked;
            }
        }
        
        private bool ValidateForm()
        {
            var errors = new List<string>();
            
            if (cmbProduct.SelectedValue == null)
                errors.Add("Product is required");
            
            if (!decimal.TryParse(txtMinimumLevel.Text, out decimal minLevel) || minLevel < 0)
                errors.Add("Minimum level must be a valid non-negative number");
            
            if (!decimal.TryParse(txtMaximumLevel.Text, out decimal maxLevel) || maxLevel < 0)
                errors.Add("Maximum level must be a valid non-negative number");
            
            if (!decimal.TryParse(txtReorderQuantity.Text, out decimal reorderQty) || reorderQty < 0)
                errors.Add("Reorder quantity must be a valid non-negative number");
            
            if (minLevel > maxLevel)
                errors.Add("Minimum level cannot be greater than maximum level");
            
            if (errors.Any())
            {
                MessageBox.Show($"Please fix the following errors:\n\n{string.Join("\n", errors)}", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            
            return true;
        }
        
        #endregion
    }
}
