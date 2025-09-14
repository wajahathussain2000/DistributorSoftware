using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class JournalVoucherForm : Form
    {
        private IJournalVoucherService _journalVoucherService;
        private IChartOfAccountService _chartOfAccountService;
        private JournalVoucher _currentVoucher;
        private bool _isEditMode;

        public JournalVoucherForm()
        {
            InitializeComponent();
            _journalVoucherService = new JournalVoucherService();
            _chartOfAccountService = new ChartOfAccountService();
            _currentVoucher = new JournalVoucher();
            _isEditMode = true; // Start in edit mode for better user experience

            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadChartOfAccounts();
            LoadJournalVouchers();
            SetDefaultValues();
            ClearDetails(); // Clear any existing details since we're starting fresh
            UpdateButtonState();
        }

        private void LoadChartOfAccounts()
        {
            try
            {
                var accounts = _chartOfAccountService.GetActiveChartOfAccounts();
                cmbAccount.DataSource = accounts;
                cmbAccount.DisplayMember = "AccountName";
                cmbAccount.ValueMember = "AccountId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadJournalVouchers()
        {
            try
            {
                var vouchers = _journalVoucherService.GetAllJournalVouchers();
                dgvJournalVouchers.DataSource = vouchers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading journal vouchers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultValues()
        {
            txtVoucherNumber.Text = _journalVoucherService.GenerateJournalVoucherNumber();
            dtpVoucherDate.Value = DateTime.Now;
            txtNarration.Text = "";
            txtReference.Text = "";
        }

        private void UpdateButtonState()
        {
            btnSave.Enabled = _isEditMode;
            btnCancel.Enabled = _isEditMode;
            btnNew.Enabled = !_isEditMode;
            btnEdit.Enabled = !_isEditMode && _currentVoucher != null;
            btnDelete.Enabled = !_isEditMode && _currentVoucher != null;
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            _currentVoucher = new JournalVoucher();
            _isEditMode = true;
            SetDefaultValues();
            ClearDetails();
            UpdateButtonState();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_currentVoucher == null)
            {
                MessageBox.Show("Please select a journal voucher to edit.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isEditMode = true;
            LoadVoucherToForm();
            UpdateButtonState();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_currentVoucher == null)
            {
                MessageBox.Show("Please select a journal voucher to delete.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete journal voucher '{_currentVoucher.VoucherNumber}'?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_journalVoucherService.DeleteJournalVoucher(_currentVoucher.VoucherId))
                    {
                        MessageBox.Show("Journal voucher deleted successfully.", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadJournalVouchers();
                        _currentVoucher = null;
                        UpdateButtonState();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete journal voucher.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting journal voucher: {ex.Message}", "Error", 
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

                UpdateVoucherFromForm();

                bool success;
                if (_currentVoucher.VoucherId == 0)
                {
                    var voucherId = _journalVoucherService.CreateJournalVoucher(_currentVoucher);
                    success = voucherId > 0;
                    if (success)
                    {
                        _currentVoucher.VoucherId = voucherId;
                    }
                }
                else
                {
                    success = _journalVoucherService.UpdateJournalVoucher(_currentVoucher);
                }

                if (success)
                {
                    MessageBox.Show("Journal voucher saved successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isEditMode = false;
                    LoadJournalVouchers();
                    UpdateButtonState();
                }
                else
                {
                    MessageBox.Show("Failed to save journal voucher.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving journal voucher: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            _isEditMode = false;
            _currentVoucher = null;
            ClearForm();
            UpdateButtonState();
        }

        private void BtnAddDetail_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
            {
                MessageBox.Show("Please start editing a journal voucher first.", "Not in Edit Mode", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbAccount.SelectedValue == null)
            {
                MessageBox.Show("Please select an account.", "No Account Selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numDebitAmount.Value <= 0 && numCreditAmount.Value <= 0)
            {
                MessageBox.Show("Please enter either debit or credit amount.", "No Amount", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numDebitAmount.Value > 0 && numCreditAmount.Value > 0)
            {
                MessageBox.Show("Please enter either debit OR credit amount, not both.", "Invalid Amount", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var detail = new JournalVoucherDetail
            {
                AccountId = (int)cmbAccount.SelectedValue,
                DebitAmount = numDebitAmount.Value,
                CreditAmount = numCreditAmount.Value,
                Narration = txtDetailNarration.Text
            };

            _currentVoucher.Details.Add(detail);
            RefreshDetailsGrid();
            ClearDetailForm();
            UpdateTotals();
        }

        private void BtnRemoveDetail_Click(object sender, EventArgs e)
        {
            if (dgvDetails.SelectedRows.Count > 0)
            {
                var index = dgvDetails.SelectedRows[0].Index;
                _currentVoucher.Details.RemoveAt(index);
                RefreshDetailsGrid();
                UpdateTotals();
            }
        }

        private void DgvJournalVouchers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvJournalVouchers.SelectedRows.Count > 0)
            {
                var voucher = (JournalVoucher)dgvJournalVouchers.SelectedRows[0].DataBoundItem;
                _currentVoucher = voucher;
                LoadVoucherToForm();
            }
            UpdateButtonState();
        }

        private void RefreshDetailsGrid()
        {
            dgvDetails.DataSource = null;
            dgvDetails.DataSource = _currentVoucher.Details;
        }

        private void ClearDetailForm()
        {
            numDebitAmount.Value = 0;
            numCreditAmount.Value = 0;
            txtDetailNarration.Text = "";
        }

        private void ClearForm()
        {
            txtVoucherNumber.Text = "";
            dtpVoucherDate.Value = DateTime.Now;
            txtNarration.Text = "";
            txtReference.Text = "";
            ClearDetails();
        }

        private void ClearDetails()
        {
            _currentVoucher.Details.Clear();
            RefreshDetailsGrid();
            UpdateTotals();
        }

        private void LoadVoucherToForm()
        {
            if (_currentVoucher != null)
            {
                txtVoucherNumber.Text = _currentVoucher.VoucherNumber;
                dtpVoucherDate.Value = _currentVoucher.VoucherDate;
                txtNarration.Text = _currentVoucher.Narration;
                txtReference.Text = _currentVoucher.Reference;
                RefreshDetailsGrid();
                UpdateTotals();
            }
        }

        private void UpdateVoucherFromForm()
        {
            _currentVoucher.VoucherNumber = txtVoucherNumber.Text;
            _currentVoucher.VoucherDate = dtpVoucherDate.Value;
            _currentVoucher.Narration = txtNarration.Text;
            _currentVoucher.Reference = txtReference.Text;
        }

        private void UpdateTotals()
        {
            _currentVoucher.TotalDebit = _currentVoucher.Details.Sum(d => d.DebitAmount);
            _currentVoucher.TotalCredit = _currentVoucher.Details.Sum(d => d.CreditAmount);
            
            lblTotalDebit.Text = $"Total Debit: {_currentVoucher.TotalDebit:C}";
            lblTotalCredit.Text = $"Total Credit: {_currentVoucher.TotalCredit:C}";
            lblDifference.Text = $"Difference: {_currentVoucher.BalanceDifference:C}";
            
            // Color coding for balance
            if (_currentVoucher.IsBalanced)
            {
                lblDifference.ForeColor = Color.Green;
            }
            else
            {
                lblDifference.ForeColor = Color.Red;
            }
        }

        private bool ValidateForm()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(txtVoucherNumber.Text))
                errors.Add("Voucher Number is required");

            if (string.IsNullOrWhiteSpace(txtNarration.Text))
                errors.Add("Narration is required");

            if (_currentVoucher.Details.Count == 0)
                errors.Add("At least one journal voucher detail is required");

            if (!_currentVoucher.IsBalanced)
                errors.Add($"Journal voucher is not balanced. Difference: {_currentVoucher.BalanceDifference:C}");

            if (errors.Any())
            {
                MessageBox.Show($"Please fix the following errors:\n\n{string.Join("\n", errors)}", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
