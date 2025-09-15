using System;
using System.Drawing;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class BankAccountForm : Form
    {
        private BankAccount _bankAccount;
        private bool _isEditMode;
        private IBankAccountService _bankAccountService;

        public BankAccount BankAccount => _bankAccount;

        public BankAccountForm(BankAccount bankAccount, bool isEditMode)
        {
            InitializeComponent();
            _bankAccount = bankAccount ?? new BankAccount();
            _isEditMode = isEditMode;
            var connectionString = Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            _bankAccountService = new BankAccountService(new BankAccountRepository(connectionString));
            
            InitializeForm();
        }

        private void InitializeForm()
        {
            if (_isEditMode && _bankAccount.BankAccountId > 0)
            {
                LoadBankAccountData();
                this.Text = "Edit Bank Account";
            }
            else
            {
                SetDefaultValues();
                this.Text = "New Bank Account";
            }
        }

        private void LoadBankAccountData()
        {
            txtBankName.Text = _bankAccount.BankName ?? string.Empty;
            txtAccountNumber.Text = _bankAccount.AccountNumber ?? string.Empty;
            txtAccountHolder.Text = _bankAccount.AccountHolderName ?? string.Empty;
            txtAccountType.Text = _bankAccount.AccountType ?? string.Empty;
            txtBranch.Text = _bankAccount.Branch ?? string.Empty;
            txtAddress.Text = _bankAccount.Address ?? string.Empty;
            txtPhone.Text = _bankAccount.Phone ?? string.Empty;
            txtEmail.Text = _bankAccount.Email ?? string.Empty;
            numCurrentBalance.Value = _bankAccount.CurrentBalance;
            chkIsActive.Checked = _bankAccount.IsActive;
        }

        private void SetDefaultValues()
        {
            txtBankName.Text = string.Empty;
            txtAccountNumber.Text = string.Empty;
            txtAccountHolder.Text = string.Empty;
            txtAccountType.Text = "CURRENT";
            txtBranch.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            numCurrentBalance.Value = 0;
            chkIsActive.Checked = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                UpdateBankAccountFromForm();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving bank account: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void UpdateBankAccountFromForm()
        {
            _bankAccount.BankName = txtBankName.Text.Trim();
            _bankAccount.AccountNumber = txtAccountNumber.Text.Trim();
            _bankAccount.AccountHolderName = txtAccountHolder.Text.Trim();
            _bankAccount.AccountType = txtAccountType.Text.Trim();
            _bankAccount.Branch = txtBranch.Text.Trim();
            _bankAccount.Address = txtAddress.Text.Trim();
            _bankAccount.Phone = txtPhone.Text.Trim();
            _bankAccount.Email = txtEmail.Text.Trim();
            _bankAccount.CurrentBalance = numCurrentBalance.Value;
            _bankAccount.IsActive = chkIsActive.Checked;
            _bankAccount.CreatedBy = 1; // Default user
        }

        private bool ValidateForm()
        {
            var errors = new System.Collections.Generic.List<string>();

            if (string.IsNullOrWhiteSpace(txtBankName.Text))
                errors.Add("Bank name is required");

            if (string.IsNullOrWhiteSpace(txtAccountNumber.Text))
                errors.Add("Account number is required");

            if (string.IsNullOrWhiteSpace(txtAccountHolder.Text))
                errors.Add("Account holder name is required");

            if (string.IsNullOrWhiteSpace(txtAccountType.Text))
                errors.Add("Account type is required");

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
