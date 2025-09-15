using System;
using System.Drawing;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class ReconciliationForm : Form
    {
        private BankReconciliation _reconciliation;
        private bool _isEditMode;
        private IBankReconciliationService _bankReconciliationService;
        private IBankAccountService _bankAccountService;

        public BankReconciliation Reconciliation => _reconciliation;

        public ReconciliationForm(BankReconciliation reconciliation, bool isEditMode)
        {
            InitializeComponent();
            _reconciliation = reconciliation ?? new BankReconciliation();
            _isEditMode = isEditMode;
            var connectionString = Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            _bankReconciliationService = new BankReconciliationService(new BankReconciliationRepository(connectionString));
            _bankAccountService = new BankAccountService(new BankAccountRepository(connectionString));
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadBankAccounts();
            
            if (_isEditMode && _reconciliation.ReconciliationId > 0)
            {
                LoadReconciliationData();
                this.Text = "Edit Reconciliation";
            }
            else
            {
                SetDefaultValues();
                this.Text = "New Reconciliation";
            }
        }

        private void LoadBankAccounts()
        {
            try
            {
                var accounts = _bankAccountService.GetActiveBankAccounts();
                cmbBankAccount.DataSource = accounts;
                cmbBankAccount.DisplayMember = "BankName";
                cmbBankAccount.ValueMember = "BankAccountId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bank accounts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadReconciliationData()
        {
            cmbBankAccount.SelectedValue = _reconciliation.BankAccountId;
            dtpReconciliationDate.Value = _reconciliation.ReconciliationDate;
            numStatementBalance.Value = _reconciliation.StatementBalance;
            numBookBalance.Value = _reconciliation.BookBalance;
            numDifference.Value = _reconciliation.Difference;
            cmbStatus.Text = _reconciliation.Status ?? string.Empty;
            txtNotes.Text = _reconciliation.Notes ?? string.Empty;
            chkIsCompleted.Checked = _reconciliation.IsCompleted;
        }

        private void SetDefaultValues()
        {
            dtpReconciliationDate.Value = DateTime.Now;
            numStatementBalance.Value = 0;
            numBookBalance.Value = 0;
            numDifference.Value = 0;
            cmbStatus.Text = "PENDING";
            txtNotes.Text = string.Empty;
            chkIsCompleted.Checked = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                UpdateReconciliationFromForm();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving reconciliation: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void UpdateReconciliationFromForm()
        {
            _reconciliation.BankAccountId = (int)cmbBankAccount.SelectedValue;
            _reconciliation.ReconciliationDate = dtpReconciliationDate.Value;
            _reconciliation.StatementBalance = numStatementBalance.Value;
            _reconciliation.BookBalance = numBookBalance.Value;
            _reconciliation.Difference = numDifference.Value;
            _reconciliation.Status = cmbStatus.Text.Trim();
            _reconciliation.Notes = txtNotes.Text.Trim();
            _reconciliation.IsCompleted = chkIsCompleted.Checked;
            _reconciliation.CreatedBy = 1; // Default user
        }

        private bool ValidateForm()
        {
            var errors = new System.Collections.Generic.List<string>();

            if (cmbBankAccount.SelectedValue == null)
                errors.Add("Bank account is required");

            if (string.IsNullOrWhiteSpace(cmbStatus.Text))
                errors.Add("Status is required");

            if (errors.Count > 0)
            {
                MessageBox.Show($"Please fix the following errors:\n\n{string.Join("\n", errors)}", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void NumStatementBalance_ValueChanged(object sender, EventArgs e)
        {
            CalculateDifference();
        }

        private void NumBookBalance_ValueChanged(object sender, EventArgs e)
        {
            CalculateDifference();
        }

        private void CalculateDifference()
        {
            numDifference.Value = numStatementBalance.Value - numBookBalance.Value;
        }
    }
}
