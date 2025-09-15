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
    public partial class BankReconciliationForm : Form
    {
        private IBankAccountService _bankAccountService;
        private IBankStatementService _bankStatementService;
        private IBankReconciliationService _bankReconciliationService;
        private IJournalVoucherService _journalVoucherService;
        private List<BankAccount> _bankAccounts;
        private List<BankStatement> _bankStatements;
        private List<BankReconciliation> _reconciliations;
        private List<JournalVoucher> _journalVouchers;

        // Current selected items for CRUD operations
        private BankAccount _currentBankAccount;
        private BankStatement _currentBankStatement;
        private BankReconciliation _currentReconciliation;
        private bool _isEditMode;

        public BankReconciliationForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeForm();
        }

        private void InitializeServices()
        {
            var connectionString = Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            var bankAccountRepository = new BankAccountRepository(connectionString);
            var bankStatementRepository = new BankStatementRepository(connectionString);
            var bankReconciliationRepository = new BankReconciliationRepository(connectionString);
            var journalVoucherRepository = new JournalVoucherRepository(connectionString);
            
            _bankAccountService = new BankAccountService(bankAccountRepository);
            _bankStatementService = new BankStatementService(bankStatementRepository);
            _bankReconciliationService = new BankReconciliationService(bankReconciliationRepository);
            _journalVoucherService = new JournalVoucherService();
        }

        private void InitializeForm()
        {
            LoadBankAccounts();
            LoadBankStatements();
            LoadJournalVouchers();
            LoadReconciliations();
        }

        private void LoadBankAccounts()
        {
            try
            {
                _bankAccounts = _bankAccountService.GetAllBankAccounts();
                BindBankAccountsToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bank accounts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindBankAccountsToGrid()
        {
            try
            {
                // Check if DataGridView exists
                if (dgvBankAccounts == null)
                {
                    MessageBox.Show("Bank accounts DataGridView is not initialized.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if bank accounts list exists
                if (_bankAccounts == null)
                {
                    _bankAccounts = new List<BankAccount>();
                }

                dgvBankAccounts.DataSource = null;
                dgvBankAccounts.DataSource = _bankAccounts;
                
                // Configure columns
                if (dgvBankAccounts.Columns.Count > 0)
                {
                    // Hide ID columns
                    if (dgvBankAccounts.Columns["BankAccountId"] != null)
                        dgvBankAccounts.Columns["BankAccountId"].Visible = false;
                    
                    // Set header texts
                    if (dgvBankAccounts.Columns["AccountHolderName"] != null)
                        dgvBankAccounts.Columns["AccountHolderName"].HeaderText = "Account Holder";
                    if (dgvBankAccounts.Columns["BankName"] != null)
                        dgvBankAccounts.Columns["BankName"].HeaderText = "Bank Name";
                    if (dgvBankAccounts.Columns["AccountNumber"] != null)
                        dgvBankAccounts.Columns["AccountNumber"].HeaderText = "Account Number";
                    if (dgvBankAccounts.Columns["AccountType"] != null)
                        dgvBankAccounts.Columns["AccountType"].HeaderText = "Account Type";
                    if (dgvBankAccounts.Columns["Branch"] != null)
                        dgvBankAccounts.Columns["Branch"].HeaderText = "Branch";
                    if (dgvBankAccounts.Columns["CurrentBalance"] != null)
                    {
                        dgvBankAccounts.Columns["CurrentBalance"].HeaderText = "Current Balance";
                        dgvBankAccounts.Columns["CurrentBalance"].DefaultCellStyle.Format = "N2";
                    }
                    if (dgvBankAccounts.Columns["IsActive"] != null)
                        dgvBankAccounts.Columns["IsActive"].HeaderText = "Active";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error binding bank accounts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBankStatements()
        {
            try
            {
                _bankStatements = _bankStatementService.GetAllBankStatements();
                BindBankStatementsToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bank statements: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindBankStatementsToGrid()
        {
            try
            {
                dgvBankStatements.DataSource = null;
                dgvBankStatements.DataSource = _bankStatements;
                
                // Configure columns
                if (dgvBankStatements.Columns.Count > 0)
                {
                    dgvBankStatements.Columns["BankStatementId"].Visible = false;
                    dgvBankStatements.Columns["BankAccountId"].Visible = false;
                    dgvBankStatements.Columns["TransactionDate"].HeaderText = "Transaction Date";
                    dgvBankStatements.Columns["TransactionDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvBankStatements.Columns["Description"].HeaderText = "Description";
                    dgvBankStatements.Columns["Amount"].HeaderText = "Amount";
                    dgvBankStatements.Columns["Amount"].DefaultCellStyle.Format = "N2";
                    dgvBankStatements.Columns["Balance"].HeaderText = "Balance";
                    dgvBankStatements.Columns["Balance"].DefaultCellStyle.Format = "N2";
                    dgvBankStatements.Columns["TransactionType"].HeaderText = "Type";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error binding bank statements: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadJournalVouchers()
        {
            try
            {
                _journalVouchers = _journalVoucherService.GetAllJournalVouchers();
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading journal vouchers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadReconciliations()
        {
            try
            {
                _reconciliations = _bankReconciliationService.GetAllReconciliations();
                BindReconciliationsToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reconciliations: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindReconciliationsToGrid()
        {
            try
            {
                dgvReconciliation.DataSource = null;
                dgvReconciliation.DataSource = _reconciliations;
                
                // Configure columns
                if (dgvReconciliation.Columns.Count > 0)
                {
                    dgvReconciliation.Columns["ReconciliationId"].Visible = false;
                    dgvReconciliation.Columns["BankAccountId"].Visible = false;
                    dgvReconciliation.Columns["ReconciliationDate"].HeaderText = "Reconciliation Date";
                    dgvReconciliation.Columns["ReconciliationDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvReconciliation.Columns["BankBalance"].HeaderText = "Bank Balance";
                    dgvReconciliation.Columns["BankBalance"].DefaultCellStyle.Format = "N2";
                    dgvReconciliation.Columns["BookBalance"].HeaderText = "Book Balance";
                    dgvReconciliation.Columns["BookBalance"].DefaultCellStyle.Format = "N2";
                    dgvReconciliation.Columns["Difference"].HeaderText = "Difference";
                    dgvReconciliation.Columns["Difference"].DefaultCellStyle.Format = "N2";
                    dgvReconciliation.Columns["Status"].HeaderText = "Status";
                    dgvReconciliation.Columns["Notes"].HeaderText = "Notes";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error binding reconciliations: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string tabName = tabControl.SelectedTab.Text;
                
                if (tabName.Contains("Bank Accounts"))
                {
                    if (_currentBankAccount == null)
                    {
                        MessageBox.Show("Please create or select a bank account to save.", "No Selection", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    if (!ValidateBankAccount())
                        return;
                    
                    bool success;
                    if (_currentBankAccount.BankAccountId == 0)
                    {
                        var accountId = _bankAccountService.CreateBankAccount(_currentBankAccount);
                        success = accountId > 0;
                        if (success)
                        {
                            _currentBankAccount.BankAccountId = accountId;
                        }
                    }
                    else
                    {
                        success = _bankAccountService.UpdateBankAccount(_currentBankAccount);
                    }
                    
                    if (success)
                    {
                        MessageBox.Show("Bank account saved successfully.", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBankAccounts();
                        _isEditMode = false;
                    }
                    else
                    {
                        MessageBox.Show("Failed to save bank account.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (tabName.Contains("Bank Statements"))
                {
                    if (_currentBankStatement == null)
                    {
                        MessageBox.Show("Please create or select a bank statement to save.", "No Selection", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    if (!ValidateBankStatement())
                        return;
                    
                    bool success;
                    if (_currentBankStatement.StatementId == 0)
                    {
                        var statementId = _bankStatementService.CreateBankStatement(_currentBankStatement);
                        success = statementId > 0;
                        if (success)
                        {
                            _currentBankStatement.StatementId = statementId;
                        }
                    }
                    else
                    {
                        success = _bankStatementService.UpdateBankStatement(_currentBankStatement);
                    }
                    
                    if (success)
                    {
                        MessageBox.Show("Bank statement saved successfully.", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBankStatements();
                        _isEditMode = false;
                    }
                    else
                    {
                        MessageBox.Show("Failed to save bank statement.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (tabName.Contains("Reconciliation"))
                {
                    if (_currentReconciliation == null)
                    {
                        MessageBox.Show("Please create or select a reconciliation to save.", "No Selection", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    if (!ValidateReconciliation())
                        return;
                    
                    bool success;
                    if (_currentReconciliation.ReconciliationId == 0)
                    {
                        var reconciliationId = _bankReconciliationService.CreateBankReconciliation(_currentReconciliation);
                        success = reconciliationId > 0;
                        if (success)
                        {
                            _currentReconciliation.ReconciliationId = reconciliationId;
                        }
                    }
                    else
                    {
                        success = _bankReconciliationService.UpdateBankReconciliation(_currentReconciliation);
                    }
                    
                    if (success)
                    {
                        MessageBox.Show("Reconciliation saved successfully.", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReconciliations();
                        _isEditMode = false;
                    }
                    else
                    {
                        MessageBox.Show("Failed to save reconciliation.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Selection change handlers
        private void DgvBankAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBankAccounts.SelectedRows.Count > 0)
            {
                _currentBankAccount = (BankAccount)dgvBankAccounts.SelectedRows[0].DataBoundItem;
            }
        }

        private void DgvBankStatements_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBankStatements.SelectedRows.Count > 0)
            {
                _currentBankStatement = (BankStatement)dgvBankStatements.SelectedRows[0].DataBoundItem;
            }
        }

        private void DgvReconciliation_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReconciliation.SelectedRows.Count > 0)
            {
                _currentReconciliation = (BankReconciliation)dgvReconciliation.SelectedRows[0].DataBoundItem;
            }
        }

        // Form display methods
        private void ShowBankAccountForm()
        {
            using (var form = new BankAccountForm(_currentBankAccount, _isEditMode))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _currentBankAccount = form.BankAccount;
                }
            }
        }

        private void ShowBankStatementForm()
        {
            using (var form = new BankStatementForm(_currentBankStatement, _isEditMode))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _currentBankStatement = form.BankStatement;
                }
            }
        }

        private void ShowReconciliationForm()
        {
            using (var form = new ReconciliationForm(_currentReconciliation, _isEditMode))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _currentReconciliation = form.Reconciliation;
                }
            }
        }

        // Validation methods
        private bool ValidateBankAccount()
        {
            if (_currentBankAccount == null) return false;
            
            var errors = _currentBankAccount.GetValidationErrors();
            if (errors.Length > 0)
            {
                MessageBox.Show($"Please fix the following errors:\n\n{string.Join("\n", errors)}", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValidateBankStatement()
        {
            if (_currentBankStatement == null) return false;
            
            var errors = _currentBankStatement.GetValidationErrors();
            if (errors.Length > 0)
            {
                MessageBox.Show($"Please fix the following errors:\n\n{string.Join("\n", errors)}", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool ValidateReconciliation()
        {
            if (_currentReconciliation == null) return false;
            
            var errors = _currentReconciliation.GetValidationErrors();
            if (errors.Length > 0)
            {
                MessageBox.Show($"Please fix the following errors:\n\n{string.Join("\n", errors)}", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                string tabName = tabControl.SelectedTab.Text;
                
                if (tabName.Contains("Bank Accounts"))
                {
                    _currentBankAccount = new BankAccount();
                    _isEditMode = true;
                    ShowBankAccountForm();
                }
                else if (tabName.Contains("Bank Statements"))
                {
                    _currentBankStatement = new BankStatement();
                    _isEditMode = true;
                    ShowBankStatementForm();
                }
                else if (tabName.Contains("Reconciliation"))
                {
                    _currentReconciliation = new BankReconciliation();
                    _isEditMode = true;
                    ShowReconciliationForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating new item: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string tabName = tabControl.SelectedTab.Text;
                
                if (tabName.Contains("Bank Accounts"))
                {
                    if (_currentBankAccount == null)
                    {
                        MessageBox.Show("Please select a bank account to edit.", "No Selection", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _isEditMode = true;
                    ShowBankAccountForm();
                }
                else if (tabName.Contains("Bank Statements"))
                {
                    if (_currentBankStatement == null)
                    {
                        MessageBox.Show("Please select a bank statement to edit.", "No Selection", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _isEditMode = true;
                    ShowBankStatementForm();
                }
                else if (tabName.Contains("Reconciliation"))
                {
                    if (_currentReconciliation == null)
                    {
                        MessageBox.Show("Please select a reconciliation to edit.", "No Selection", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    _isEditMode = true;
                    ShowReconciliationForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing item: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string tabName = tabControl.SelectedTab.Text;
                
                if (tabName.Contains("Bank Accounts"))
                {
                    if (_currentBankAccount == null)
                    {
                        MessageBox.Show("Please select a bank account to delete.", "No Selection", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    var result = MessageBox.Show($"Are you sure you want to delete bank account '{_currentBankAccount.BankName}'?", 
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        if (_bankAccountService.DeleteBankAccount(_currentBankAccount.BankAccountId))
                        {
                            MessageBox.Show("Bank account deleted successfully.", "Success", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadBankAccounts();
                            _currentBankAccount = null;
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete bank account.", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (tabName.Contains("Bank Statements"))
                {
                    if (_currentBankStatement == null)
                    {
                        MessageBox.Show("Please select a bank statement to delete.", "No Selection", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    var result = MessageBox.Show($"Are you sure you want to delete this bank statement?", 
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        if (_bankStatementService.DeleteBankStatement(_currentBankStatement.StatementId))
                        {
                            MessageBox.Show("Bank statement deleted successfully.", "Success", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadBankStatements();
                            _currentBankStatement = null;
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete bank statement.", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (tabName.Contains("Reconciliation"))
                {
                    if (_currentReconciliation == null)
                    {
                        MessageBox.Show("Please select a reconciliation to delete.", "No Selection", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    var result = MessageBox.Show($"Are you sure you want to delete this reconciliation?", 
                        "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        if (_bankReconciliationService.DeleteBankReconciliation(_currentReconciliation.ReconciliationId))
                        {
                            MessageBox.Show("Reconciliation deleted successfully.", "Success", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadReconciliations();
                            _currentReconciliation = null;
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete reconciliation.", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting item: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}