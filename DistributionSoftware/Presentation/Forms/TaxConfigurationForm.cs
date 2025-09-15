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
    public partial class TaxConfigurationForm : Form
    {
        private ITaxCategoryService _taxCategoryService;
        private ITaxRateService _taxRateService;
        private List<TaxCategory> _taxCategories;
        private List<TaxRate> _taxRates;
        private TaxCategory _currentCategory;
        private TaxRate _currentRate;
        private bool _isEditingCategory = false;
        private bool _isEditingRate = false;

        public TaxConfigurationForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeForm();
        }

        private void InitializeServices()
        {
            var connectionString = Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            var taxCategoryRepository = new TaxCategoryRepository(connectionString);
            var taxRateRepository = new TaxRateRepository(connectionString);
            
            _taxCategoryService = new TaxCategoryService(taxCategoryRepository);
            _taxRateService = new TaxRateService(taxRateRepository);
        }

        private void InitializeForm()
        {
            LoadTaxCategories();
            LoadTaxRates();
            SetupDataGrids();
            ClearCategoryForm();
            ClearRateForm();
        }

        private void SetupDataGrids()
        {
            SetupCategoriesGrid();
            SetupRatesGrid();
        }

        private void SetupCategoriesGrid()
        {
            dgvCategories.AutoGenerateColumns = false;
            dgvCategories.Columns.Clear();
            
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaxCategoryCode",
                HeaderText = "Code",
                DataPropertyName = "TaxCategoryCode",
                Width = 100
            });
            
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaxCategoryName",
                HeaderText = "Category Name",
                DataPropertyName = "TaxCategoryName",
                Width = 200
            });
            
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "Description",
                DataPropertyName = "Description",
                Width = 300
            });
            
            dgvCategories.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsActive",
                HeaderText = "Active",
                DataPropertyName = "IsActive",
                Width = 80
            });
            
            dgvCategories.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsSystemCategory",
                HeaderText = "System",
                DataPropertyName = "IsSystemCategory",
                Width = 80
            });
            
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedDate",
                HeaderText = "Created Date",
                DataPropertyName = "CreatedDate",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
        }

        private void SetupRatesGrid()
        {
            dgvRates.AutoGenerateColumns = false;
            dgvRates.Columns.Clear();
            
            dgvRates.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaxRateCode",
                HeaderText = "Code",
                DataPropertyName = "TaxRateCode",
                Width = 100
            });
            
            dgvRates.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaxRateName",
                HeaderText = "Rate Name",
                DataPropertyName = "TaxRateName",
                Width = 150
            });
            
            dgvRates.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaxPercentage",
                HeaderText = "Percentage",
                DataPropertyName = "TaxPercentage",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });
            
            dgvRates.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TaxCategoryName",
                HeaderText = "Category",
                DataPropertyName = "TaxCategoryName",
                Width = 120
            });
            
            dgvRates.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsActive",
                HeaderText = "Active",
                DataPropertyName = "IsActive",
                Width = 80
            });
            
            dgvRates.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsInclusive",
                HeaderText = "Inclusive",
                DataPropertyName = "IsInclusive",
                Width = 80
            });
            
            dgvRates.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EffectiveFrom",
                HeaderText = "From Date",
                DataPropertyName = "EffectiveFrom",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
            
            dgvRates.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EffectiveTo",
                HeaderText = "To Date",
                DataPropertyName = "EffectiveTo",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
        }

        private void LoadTaxCategories()
        {
            try
            {
                _taxCategories = _taxCategoryService.GetAllTaxCategories();
                dgvCategories.DataSource = _taxCategories;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tax categories: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTaxRates()
        {
            try
            {
                _taxRates = _taxRateService.GetAllTaxRates();
                // Load category names for display
                foreach (var rate in _taxRates)
                {
                    var category = _taxCategories?.FirstOrDefault(c => c.TaxCategoryId == rate.TaxCategoryId);
                    if (category != null)
                    {
                        rate.TaxCategoryName = category.TaxCategoryName;
                    }
                }
                dgvRates.DataSource = _taxRates;
                LoadCategoryComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tax rates: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategoryComboBox()
        {
            cmbRateCategory.DataSource = null;
            cmbRateCategory.Items.Clear();
            
            if (_taxCategories != null)
            {
                cmbRateCategory.DataSource = _taxCategories;
                cmbRateCategory.DisplayMember = "TaxCategoryName";
                cmbRateCategory.ValueMember = "TaxCategoryId";
            }
        }

        #region Category CRUD Operations

        private void BtnCategoryNew_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== Category New Clicked ===");
            ClearCategoryForm();
            _isEditingCategory = false;
            _currentCategory = null;
            
            // Ensure form is in a clean state
            txtCategoryCode.Text = "Auto-generated";
            txtCategoryCode.ReadOnly = true;
            txtCategoryCode.BackColor = Color.LightGray;
            txtCategoryName.Text = string.Empty;
            txtCategoryDescription.Text = string.Empty;
            chkCategoryIsActive.Checked = true;
            chkCategoryIsSystem.Checked = false;
            
            txtCategoryName.Focus();
            System.Diagnostics.Debug.WriteLine("Form cleared and ready for new entry");
        }

        private void BtnCategoryEdit_Click(object sender, EventArgs e)
        {
            if (_currentCategory == null)
            {
                MessageBox.Show("Please select a category to edit.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isEditingCategory = true;
            LoadCategoryIntoForm(_currentCategory);
        }

        private void BtnCategorySave_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== Category Save Clicked ===");
            
            if (!ValidateCategoryForm())
            {
                System.Diagnostics.Debug.WriteLine("Validation failed");
                return;
            }

            System.Diagnostics.Debug.WriteLine("Validation passed, proceeding with save");

            try
            {
                var category = GetCategoryFromForm();
                
                if (_isEditingCategory)
                {
                    System.Diagnostics.Debug.WriteLine("Updating existing category");
                    category.TaxCategoryId = _currentCategory.TaxCategoryId;
                    category.ModifiedDate = DateTime.Now;
                    category.ModifiedBy = 1; // TODO: Get from user session
                    category.ModifiedByName = "System User"; // TODO: Get from user session
                    
                    _taxCategoryService.UpdateTaxCategory(category);
                    MessageBox.Show("Tax category updated successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Creating new category");
                    _taxCategoryService.CreateTaxCategory(category);
                    MessageBox.Show("Tax category created successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadTaxCategories();
                LoadTaxRates(); // Refresh rates to update category names
                ClearCategoryForm();
                _isEditingCategory = false;
                _currentCategory = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in save: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error saving tax category: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCategoryDelete_Click(object sender, EventArgs e)
        {
            if (_currentCategory == null)
            {
                MessageBox.Show("Please select a category to delete.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentCategory.IsSystemCategory)
            {
                MessageBox.Show("System categories cannot be deleted.", "Cannot Delete", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete the category '{_currentCategory.TaxCategoryName}'?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _taxCategoryService.DeleteTaxCategory(_currentCategory.TaxCategoryId);
                    MessageBox.Show("Tax category deleted successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadTaxCategories();
                    LoadTaxRates();
                    ClearCategoryForm();
                    _currentCategory = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting tax category: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCategoryCancel_Click(object sender, EventArgs e)
        {
            ClearCategoryForm();
            _isEditingCategory = false;
            _currentCategory = null;
        }

        private void DgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCategories.SelectedRows.Count > 0)
            {
                var selectedCategory = dgvCategories.SelectedRows[0].DataBoundItem as TaxCategory;
                if (selectedCategory != null)
                {
                    _currentCategory = selectedCategory;
                    LoadCategoryIntoForm(selectedCategory);
                }
            }
        }

        private void LoadCategoryIntoForm(TaxCategory category)
        {
            txtCategoryCode.Text = category.TaxCategoryCode;
            txtCategoryCode.ReadOnly = false;
            txtCategoryCode.BackColor = Color.White;
            txtCategoryName.Text = category.TaxCategoryName;
            txtCategoryDescription.Text = category.Description;
            chkCategoryIsActive.Checked = category.IsActive;
            chkCategoryIsSystem.Checked = category.IsSystemCategory;
        }

        private TaxCategory GetCategoryFromForm()
        {
            var category = new TaxCategory
            {
                TaxCategoryCode = txtCategoryCode.Text?.Trim() == "Auto-generated" ? string.Empty : txtCategoryCode.Text?.Trim() ?? string.Empty,
                TaxCategoryName = txtCategoryName.Text?.Trim() ?? string.Empty,
                Description = txtCategoryDescription.Text?.Trim() ?? string.Empty,
                IsActive = chkCategoryIsActive.Checked,
                IsSystemCategory = chkCategoryIsSystem.Checked,
                CreatedDate = DateTime.Now,
                CreatedBy = 1, // TODO: Get from user session
                CreatedByName = "System User" // TODO: Get from user session
            };

            // Debug logging
            System.Diagnostics.Debug.WriteLine($"Creating TaxCategory:");
            System.Diagnostics.Debug.WriteLine($"  Code: '{category.TaxCategoryCode}' (auto-generated: {string.IsNullOrEmpty(category.TaxCategoryCode)})");
            System.Diagnostics.Debug.WriteLine($"  Name: '{category.TaxCategoryName}'");
            System.Diagnostics.Debug.WriteLine($"  Description: '{category.Description}'");
            System.Diagnostics.Debug.WriteLine($"  IsActive: {category.IsActive}");
            System.Diagnostics.Debug.WriteLine($"  IsSystemCategory: {category.IsSystemCategory}");

            return category;
        }

        private bool ValidateCategoryForm()
        {
            // Debug logging
            System.Diagnostics.Debug.WriteLine($"Validating Category Form:");
            System.Diagnostics.Debug.WriteLine($"  Code Text: '{txtCategoryCode.Text}'");
            System.Diagnostics.Debug.WriteLine($"  Name Text: '{txtCategoryName.Text}'");
            System.Diagnostics.Debug.WriteLine($"  Description Text: '{txtCategoryDescription.Text}'");

            // Category code is auto-generated, no validation needed

            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Category name is required.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return false;
            }

            // Category code is auto-generated, no additional validation needed

            if (txtCategoryName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Category name cannot be empty.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return false;
            }

            return true;
        }

        private void ClearCategoryForm()
        {
            System.Diagnostics.Debug.WriteLine("=== Clearing Category Form ===");
            
            txtCategoryCode.Text = "Auto-generated";
            txtCategoryCode.ReadOnly = true;
            txtCategoryCode.BackColor = Color.LightGray;
            txtCategoryName.Text = string.Empty;
            txtCategoryDescription.Text = string.Empty;
            chkCategoryIsActive.Checked = true;
            chkCategoryIsSystem.Checked = false;
            
            System.Diagnostics.Debug.WriteLine("Category form cleared");
        }

        #endregion

        #region Rate CRUD Operations

        private void BtnRateNew_Click(object sender, EventArgs e)
        {
            try
            {
                using (var modal = new TaxRateEditModal(_taxRateService, _taxCategoryService))
                {
                    modal.LoadTaxRate(null, false);
                    
                    if (modal.ShowDialog() == DialogResult.OK)
                    {
                        LoadTaxRates();
                        MessageBox.Show("Tax rate created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating tax rate: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRateEdit_Click(object sender, EventArgs e)
        {
            if (_currentRate == null)
            {
                MessageBox.Show("Please select a tax rate to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var modal = new TaxRateEditModal(_taxRateService, _taxCategoryService))
                {
                    modal.LoadTaxRate(_currentRate, true);
                    
                    if (modal.ShowDialog() == DialogResult.OK)
                    {
                        LoadTaxRates();
                        MessageBox.Show("Tax rate updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing tax rate: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRateSave_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== Rate Save Clicked ===");
            
            if (!ValidateRateForm())
            {
                System.Diagnostics.Debug.WriteLine("Rate validation failed");
                return;
            }

            System.Diagnostics.Debug.WriteLine("Rate validation passed, proceeding with save");

            try
            {
                var rate = GetRateFromForm();
                
                if (_isEditingRate)
                {
                    System.Diagnostics.Debug.WriteLine("Updating existing rate");
                    rate.TaxRateId = _currentRate.TaxRateId;
                    rate.ModifiedDate = DateTime.Now;
                    rate.ModifiedBy = 1; // TODO: Get from user session
                    rate.ModifiedByName = "System User"; // TODO: Get from user session
                    
                    _taxRateService.UpdateTaxRate(rate);
                    MessageBox.Show("Tax rate updated successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Creating new rate");
                    _taxRateService.CreateTaxRate(rate);
                    MessageBox.Show("Tax rate created successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadTaxRates();
                ClearRateForm();
                _isEditingRate = false;
                _currentRate = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in rate save: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error saving tax rate: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRateDelete_Click(object sender, EventArgs e)
        {
            if (_currentRate == null)
            {
                MessageBox.Show("Please select a rate to delete.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentRate.IsSystemRate)
            {
                MessageBox.Show("System rates cannot be deleted.", "Cannot Delete", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete the rate '{_currentRate.TaxRateName}'?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _taxRateService.DeleteTaxRate(_currentRate.TaxRateId);
                    MessageBox.Show("Tax rate deleted successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadTaxRates();
                    ClearRateForm();
                    _currentRate = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting tax rate: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRateCancel_Click(object sender, EventArgs e)
        {
            ClearRateForm();
            _isEditingRate = false;
            _currentRate = null;
        }

        private void DgvRates_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRates.SelectedRows.Count > 0)
            {
                var selectedRate = dgvRates.SelectedRows[0].DataBoundItem as TaxRate;
                if (selectedRate != null)
                {
                    _currentRate = selectedRate;
                    LoadRateIntoForm(selectedRate);
                }
            }
        }

        private void LoadRateIntoForm(TaxRate rate)
        {
            cmbRateCategory.SelectedValue = rate.TaxCategoryId;
            txtRateName.Text = rate.TaxRateName;
            txtRateCode.Text = rate.TaxRateCode;
            txtRateCode.ReadOnly = false;
            txtRateCode.BackColor = Color.White;
            txtRatePercentage.Text = rate.TaxPercentage.ToString("N2");
            txtRateDescription.Text = rate.Description;
            dtpRateEffectiveFrom.Value = rate.EffectiveFrom ?? DateTime.Now;
            dtpRateEffectiveTo.Value = rate.EffectiveTo ?? DateTime.Now.AddYears(1);
            chkRateIsActive.Checked = rate.IsActive;
            chkRateIsSystem.Checked = rate.IsSystemRate;
            chkRateIsCompound.Checked = rate.IsCompound;
            chkRateIsInclusive.Checked = rate.IsInclusive;
        }

        private TaxRate GetRateFromForm()
        {
            var rate = new TaxRate
            {
                TaxCategoryId = (int)cmbRateCategory.SelectedValue,
                TaxRateName = txtRateName.Text?.Trim() ?? string.Empty,
                TaxRateCode = txtRateCode.Text?.Trim() == "Auto-generated" ? string.Empty : txtRateCode.Text?.Trim() ?? string.Empty,
                TaxPercentage = decimal.TryParse(txtRatePercentage.Text, out decimal percentage) ? percentage : 0,
                Description = txtRateDescription.Text?.Trim() ?? string.Empty,
                EffectiveFrom = dtpRateEffectiveFrom.Value,
                EffectiveTo = dtpRateEffectiveTo.Checked ? dtpRateEffectiveTo.Value : (DateTime?)null,
                IsActive = chkRateIsActive.Checked,
                IsSystemRate = chkRateIsSystem.Checked,
                IsCompound = chkRateIsCompound.Checked,
                IsInclusive = chkRateIsInclusive.Checked,
                CreatedDate = DateTime.Now,
                CreatedBy = 1, // TODO: Get from user session
                CreatedByName = "System User" // TODO: Get from user session
            };

            // Debug logging
            System.Diagnostics.Debug.WriteLine($"Creating TaxRate:");
            System.Diagnostics.Debug.WriteLine($"  Code: '{rate.TaxRateCode}' (auto-generated: {string.IsNullOrEmpty(rate.TaxRateCode)})");
            System.Diagnostics.Debug.WriteLine($"  Name: '{rate.TaxRateName}'");
            System.Diagnostics.Debug.WriteLine($"  Percentage: {rate.TaxPercentage}");
            System.Diagnostics.Debug.WriteLine($"  CategoryId: {rate.TaxCategoryId}");
            System.Diagnostics.Debug.WriteLine($"  Description: '{rate.Description}'");

            return rate;
        }

        private bool ValidateRateForm()
        {
            // Debug logging
            System.Diagnostics.Debug.WriteLine($"Validating Rate Form:");
            System.Diagnostics.Debug.WriteLine($"  Category Selected: {cmbRateCategory.SelectedValue}");
            System.Diagnostics.Debug.WriteLine($"  Rate Name Text: '{txtRateName.Text}'");
            System.Diagnostics.Debug.WriteLine($"  Rate Code Text: '{txtRateCode.Text}'");
            System.Diagnostics.Debug.WriteLine($"  Rate Percentage Text: '{txtRatePercentage.Text}'");

            if (cmbRateCategory.SelectedValue == null)
            {
                MessageBox.Show("Please select a tax category.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRateCategory.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRateName.Text))
            {
                MessageBox.Show("Rate name is required.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRateName.Focus();
                return false;
            }

            // Rate code is auto-generated, no validation needed

            if (!decimal.TryParse(txtRatePercentage.Text, out decimal percentage) || percentage < 0)
            {
                MessageBox.Show("Please enter a valid tax percentage.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRatePercentage.Focus();
                return false;
            }

            if (percentage > 100)
            {
                MessageBox.Show("Tax percentage cannot exceed 100%.", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRatePercentage.Focus();
                return false;
            }

            return true;
        }

        private void ClearRateForm()
        {
            cmbRateCategory.SelectedIndex = -1;
            txtRateName.Clear();
            txtRateCode.Text = "Auto-generated";
            txtRateCode.ReadOnly = true;
            txtRateCode.BackColor = Color.LightGray;
            txtRatePercentage.Clear();
            txtRateDescription.Clear();
            dtpRateEffectiveFrom.Value = DateTime.Now;
            dtpRateEffectiveTo.Value = DateTime.Now.AddYears(1);
            chkRateIsActive.Checked = true;
            chkRateIsSystem.Checked = false;
            chkRateIsCompound.Checked = false;
            chkRateIsInclusive.Checked = false;
        }

        #endregion
    }
}