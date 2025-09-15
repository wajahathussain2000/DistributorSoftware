using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Business;
using DistributionSoftware.Models;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class TaxRateEditModal : Form
    {
        private readonly ITaxRateService _taxRateService;
        private readonly ITaxCategoryService _taxCategoryService;
        private TaxRate _currentRate;
        private bool _isEditMode;

        public TaxRateEditModal(ITaxRateService taxRateService, ITaxCategoryService taxCategoryService)
        {
            _taxRateService = taxRateService;
            _taxCategoryService = taxCategoryService;
            InitializeComponent();
            ApplyProfessionalStyling();
        }

        public TaxRate TaxRate { get; private set; }

        public void LoadTaxRate(TaxRate taxRate, bool isEditMode = true)
        {
            _currentRate = taxRate;
            _isEditMode = isEditMode;

            if (taxRate != null)
            {
                LoadTaxCategories();
                PopulateForm(taxRate);
            }
            else
            {
                LoadTaxCategories();
                ClearForm();
            }

            UpdateFormTitle();
        }

        private void LoadTaxCategories()
        {
            try
            {
                var categories = _taxCategoryService.GetAllTaxCategories();
                cmbCategory.DataSource = categories;
                cmbCategory.DisplayMember = "TaxCategoryName";
                cmbCategory.ValueMember = "TaxCategoryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tax categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateForm(TaxRate taxRate)
        {
            try
            {
                // Select the category
                if (taxRate.TaxCategoryId > 0)
                {
                    cmbCategory.SelectedValue = taxRate.TaxCategoryId;
                }

                txtName.Text = taxRate.TaxRateName ?? string.Empty;
                txtCode.Text = taxRate.TaxRateCode ?? string.Empty;
                txtPercentage.Text = taxRate.TaxPercentage.ToString("F2");
                txtDescription.Text = taxRate.Description ?? string.Empty;
                
                dtpEffectiveFrom.Value = taxRate.EffectiveFrom.HasValue ? taxRate.EffectiveFrom.Value : DateTime.Now;
                dtpEffectiveTo.Value = taxRate.EffectiveTo.HasValue ? taxRate.EffectiveTo.Value : DateTime.Now.AddYears(1);
                
                chkIsActive.Checked = taxRate.IsActive;
                chkIsSystem.Checked = taxRate.IsSystemRate;
                chkIsCompound.Checked = taxRate.IsCompound;
                chkIsInclusive.Checked = taxRate.IsInclusive;

                // Make code field editable for existing rates
                txtCode.ReadOnly = false;
                txtCode.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            cmbCategory.SelectedIndex = -1;
            txtName.Clear();
            txtCode.Text = "Auto-generated";
            txtCode.ReadOnly = true;
            txtCode.BackColor = Color.LightGray;
            txtPercentage.Clear();
            txtDescription.Clear();
            dtpEffectiveFrom.Value = DateTime.Now;
            dtpEffectiveTo.Value = DateTime.Now.AddYears(1);
            chkIsActive.Checked = true;
            chkIsSystem.Checked = false;
            chkIsCompound.Checked = false;
            chkIsInclusive.Checked = false;
        }

        private void UpdateFormTitle()
        {
            if (_isEditMode)
            {
                this.Text = $"✏️ Edit Tax Rate - {(_currentRate?.TaxRateName ?? "New Rate")}";
                lblTitle.Text = $"✏️ Edit Tax Rate";
            }
            else
            {
                this.Text = "➕ Add New Tax Rate";
                lblTitle.Text = "➕ Add New Tax Rate";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                var taxRate = GetTaxRateFromForm();

                if (_isEditMode && _currentRate != null)
                {
                    taxRate.TaxRateId = _currentRate.TaxRateId;
                    
                    // Debug: Log the tax rate data being updated
                    System.Diagnostics.Debug.WriteLine($"=== Updating Tax Rate ===");
                    System.Diagnostics.Debug.WriteLine($"TaxRateId: {taxRate.TaxRateId}");
                    System.Diagnostics.Debug.WriteLine($"TaxRateName: {taxRate.TaxRateName}");
                    System.Diagnostics.Debug.WriteLine($"TaxRateCode: {taxRate.TaxRateCode}");
                    System.Diagnostics.Debug.WriteLine($"TaxPercentage: {taxRate.TaxPercentage}");
                    System.Diagnostics.Debug.WriteLine($"TaxCategoryId: {taxRate.TaxCategoryId}");
                    System.Diagnostics.Debug.WriteLine($"EffectiveFrom: {taxRate.EffectiveFrom}");
                    System.Diagnostics.Debug.WriteLine($"EffectiveTo: {taxRate.EffectiveTo}");
                    System.Diagnostics.Debug.WriteLine($"IsActive: {taxRate.IsActive}");
                    
                    _taxRateService.UpdateTaxRate(taxRate);
                    MessageBox.Show("Tax rate updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _taxRateService.CreateTaxRate(taxRate);
                    MessageBox.Show("Tax rate created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.TaxRate = taxRate;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving tax rate: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private TaxRate GetTaxRateFromForm()
        {
            var taxRate = new TaxRate
            {
                TaxCategoryId = cmbCategory.SelectedValue != null ? (int)cmbCategory.SelectedValue : 0,
                TaxRateName = txtName.Text?.Trim() ?? string.Empty,
                TaxRateCode = txtCode.Text?.Trim() == "Auto-generated" ? string.Empty : txtCode.Text?.Trim() ?? string.Empty,
                TaxPercentage = decimal.TryParse(txtPercentage.Text, out decimal percentage) ? percentage : 0,
                Description = txtDescription.Text?.Trim(),
                EffectiveFrom = dtpEffectiveFrom.Value,
                EffectiveTo = dtpEffectiveTo.Value,
                IsActive = chkIsActive.Checked,
                IsSystemRate = chkIsSystem.Checked,
                IsCompound = chkIsCompound.Checked,
                IsInclusive = chkIsInclusive.Checked,
                CreatedDate = _currentRate?.CreatedDate ?? DateTime.Now,
                CreatedBy = _currentRate?.CreatedBy ?? 0
            };

            return taxRate;
        }

        private bool ValidateForm()
        {
            var errors = new System.Collections.Generic.List<string>();

            if (cmbCategory.SelectedIndex == -1)
                errors.Add("Please select a tax category");

            if (string.IsNullOrWhiteSpace(txtName.Text))
                errors.Add("Tax rate name is required");

            if (string.IsNullOrWhiteSpace(txtPercentage.Text) || !decimal.TryParse(txtPercentage.Text, out decimal percentage))
                errors.Add("Valid tax percentage is required");
            else if (percentage < 0 || percentage > 100)
                errors.Add("Tax percentage must be between 0 and 100");

            if (dtpEffectiveFrom.Value > dtpEffectiveTo.Value)
                errors.Add("Effective from date cannot be after effective to date");

            if (errors.Any())
            {
                MessageBox.Show($"Please fix the following errors:\n\n• {string.Join("\n• ", errors)}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ApplyProfessionalStyling()
        {
            // Form styling
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Header panel styling
            pnlHeader.BackColor = Color.FromArgb(0, 123, 255);
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);

            // Form panel styling
            pnlForm.BackColor = Color.White;
            pnlForm.BorderStyle = BorderStyle.FixedSingle;

            // Button styling
            btnSave.BackColor = Color.FromArgb(0, 123, 255);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.ForeColor = Color.White;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.ForeColor = Color.White;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Text box styling
            foreach (Control control in GetAllControls(pnlForm))
            {
                if (control is TextBox textBox)
                {
                    textBox.Font = new Font("Segoe UI", 9F);
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (control is ComboBox comboBox)
                {
                    comboBox.Font = new Font("Segoe UI", 9F);
                    comboBox.FlatStyle = FlatStyle.Flat;
                }
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.Font = new Font("Segoe UI", 9F);
                }
                else if (control is CheckBox checkBox)
                {
                    checkBox.Font = new Font("Segoe UI", 9F);
                }
                else if (control is Label label)
                {
                    label.Font = new Font("Segoe UI", 9F);
                }
            }
        }

        private System.Collections.Generic.IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                yield return control;
                foreach (Control child in GetAllControls(control))
                {
                    yield return child;
                }
            }
        }
    }
}
