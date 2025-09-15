using System;
using System.Drawing;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class BankStatementForm : Form
    {
        private BankStatement _bankStatement;
        private bool _isEditMode;
        private IBankStatementService _bankStatementService;
        private IBankAccountService _bankAccountService;

        public BankStatement BankStatement => _bankStatement;

        public BankStatementForm(BankStatement bankStatement, bool isEditMode)
        {
            InitializeComponent();
            _bankStatement = bankStatement ?? new BankStatement();
            _isEditMode = isEditMode;
            var connectionString = Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            _bankStatementService = new BankStatementService(new BankStatementRepository(connectionString));
            _bankAccountService = new BankAccountService(new BankAccountRepository(connectionString));
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadBankAccounts();
            
            if (_isEditMode && _bankStatement.StatementId > 0)
            {
                LoadBankStatementData();
                this.Text = "Edit Bank Statement";
            }
            else
            {
                SetDefaultValues();
                this.Text = "New Bank Statement";
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

        private void LoadBankStatementData()
        {
            cmbBankAccount.SelectedValue = _bankStatement.BankAccountId;
            dtpTransactionDate.Value = _bankStatement.TransactionDate;
            txtDescription.Text = _bankStatement.Description ?? string.Empty;
            txtReferenceNumber.Text = _bankStatement.ReferenceNumber ?? string.Empty;
            cmbTransactionType.Text = _bankStatement.TransactionType ?? string.Empty;
            numAmount.Value = _bankStatement.Amount;
            numBalance.Value = _bankStatement.Balance;
            chkIsReconciled.Checked = _bankStatement.IsReconciled;
        }

        private void SetDefaultValues()
        {
            dtpTransactionDate.Value = DateTime.Now;
            txtDescription.Text = string.Empty;
            txtReferenceNumber.Text = string.Empty;
            cmbTransactionType.Text = "CREDIT";
            numAmount.Value = 0;
            numBalance.Value = 0;
            chkIsReconciled.Checked = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                UpdateBankStatementFromForm();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving bank statement: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void UpdateBankStatementFromForm()
        {
            _bankStatement.BankAccountId = (int)cmbBankAccount.SelectedValue;
            _bankStatement.TransactionDate = dtpTransactionDate.Value;
            _bankStatement.Description = txtDescription.Text.Trim();
            _bankStatement.ReferenceNumber = txtReferenceNumber.Text.Trim();
            _bankStatement.TransactionType = cmbTransactionType.Text.Trim();
            _bankStatement.Amount = numAmount.Value;
            _bankStatement.Balance = numBalance.Value;
            _bankStatement.IsReconciled = chkIsReconciled.Checked;
            _bankStatement.CreatedBy = 1; // Default user
        }

        private bool ValidateForm()
        {
            var errors = new System.Collections.Generic.List<string>();

            if (cmbBankAccount.SelectedValue == null)
                errors.Add("Bank account is required");

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
                errors.Add("Description is required");

            if (string.IsNullOrWhiteSpace(cmbTransactionType.Text))
                errors.Add("Transaction type is required");

            if (numAmount.Value <= 0)
                errors.Add("Amount must be greater than zero");

            if (cmbTransactionType.Text != "DEBIT" && cmbTransactionType.Text != "CREDIT")
                errors.Add("Transaction type must be DEBIT or CREDIT");

            if (errors.Count > 0)
            {
                MessageBox.Show($"Please fix the following errors:\n\n{string.Join("\n", errors)}", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
