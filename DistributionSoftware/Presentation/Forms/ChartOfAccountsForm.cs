using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Business;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    /// <summary>
    /// Chart of Accounts Management Form
    /// Provides comprehensive account management functionality
    /// </summary>
    public partial class ChartOfAccountsForm : Form
    {
        #region Private Fields

        private IChartOfAccountService _chartOfAccountService;
        private List<ChartOfAccount> _allAccounts;
        private ChartOfAccount _currentAccount;
        private bool _isEditMode = false;
        private bool _isLoading = false;
        private ToolTip toolTip;

        #endregion

        #region Constructor

        public ChartOfAccountsForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeForm();
            LoadInitialData();
            
            // Initialize tooltip
            toolTip = new ToolTip();
            toolTip.IsBalloon = true;
            toolTip.ToolTipTitle = "Validation Error";
        }

        #endregion

        #region Initialization Methods

        private void InitializeServices()
        {
            _chartOfAccountService = new ChartOfAccountService();
        }

        private void InitializeForm()
        {
            // Set form properties
            this.Text = "Chart of Accounts Management";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.MinimumSize = new Size(1200, 800);

            // Initialize controls
            InitializeControls();
            SetupEventHandlers();
            ConfigureDataGridViews();
        }

        private void InitializeControls()
        {
            // Set up combo boxes
            SetupComboBoxes();
            
            // Set up tree view
            SetupTreeView();
            
            // Set up data grid view
            SetupDataGridView();
            
            // Set up buttons
            SetupButtons();
        }

        private void SetupComboBoxes()
        {
            // Account Type ComboBox
            cmbAccountType.Items.Clear();
            cmbAccountType.Items.AddRange(_chartOfAccountService.GetAccountTypes().ToArray());
            cmbAccountType.DropDownStyle = ComboBoxStyle.DropDownList;

            // Account Category ComboBox
            cmbAccountCategory.Items.Clear();
            cmbAccountCategory.Items.AddRange(_chartOfAccountService.GetAccountCategories().ToArray());
            cmbAccountCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            // Parent Account ComboBox
            cmbParentAccount.Items.Clear();
            cmbParentAccount.DropDownStyle = ComboBoxStyle.DropDownList;

            // Normal Balance ComboBox
            cmbNormalBalance.Items.Clear();
            cmbNormalBalance.Items.AddRange(new[] { "DEBIT", "CREDIT" });
            cmbNormalBalance.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SetupTreeView()
        {
            treeViewAccounts.ImageList = new ImageList();
            treeViewAccounts.ImageList.Images.Add("Account", SystemIcons.Application.ToBitmap());
            treeViewAccounts.ImageList.Images.Add("Folder", SystemIcons.Application.ToBitmap());
            treeViewAccounts.CheckBoxes = false;
            treeViewAccounts.FullRowSelect = true;
            treeViewAccounts.HideSelection = false;
        }

        private void SetupDataGridView()
        {
            dgvAccounts.AutoGenerateColumns = false;
            dgvAccounts.AllowUserToAddRows = false;
            dgvAccounts.AllowUserToDeleteRows = false;
            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.MultiSelect = false;
            dgvAccounts.ReadOnly = true;
            dgvAccounts.BackgroundColor = Color.White;
            dgvAccounts.BorderStyle = BorderStyle.Fixed3D;
            dgvAccounts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvAccounts.RowHeadersVisible = false;
            dgvAccounts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
        }

        private void SetupButtons()
        {
            // Set button styles
            btnNew.BackColor = Color.FromArgb(52, 152, 219);
            btnNew.ForeColor = Color.White;
            btnNew.FlatStyle = FlatStyle.Flat;
            btnNew.FlatAppearance.BorderSize = 0;

            btnEdit.BackColor = Color.FromArgb(46, 204, 113);
            btnEdit.ForeColor = Color.White;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.FlatAppearance.BorderSize = 0;

            btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.FlatAppearance.BorderSize = 0;

            btnSave.BackColor = Color.FromArgb(52, 152, 219);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;

            btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;

            btnRefresh.BackColor = Color.FromArgb(155, 89, 182);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderSize = 0;
        }

        private void SetupEventHandlers()
        {
            // Form events
            this.Load += ChartOfAccountsForm_Load;
            this.FormClosing += ChartOfAccountsForm_FormClosing;

            // Button events
            btnNew.Click += BtnNew_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            btnRefresh.Click += BtnRefresh_Click;

            // Tree view events
            treeViewAccounts.AfterSelect += TreeViewAccounts_AfterSelect;

            // Data grid view events
            dgvAccounts.SelectionChanged += DgvAccounts_SelectionChanged;

            // Combo box events
            cmbAccountType.SelectedIndexChanged += CmbAccountType_SelectedIndexChanged;
            cmbAccountCategory.SelectedIndexChanged += CmbAccountCategory_SelectedIndexChanged;

            // Text box events
            txtAccountCode.TextChanged += TxtAccountCode_TextChanged;
            txtAccountName.TextChanged += TxtAccountName_TextChanged;
        }

        private void ConfigureDataGridViews()
        {
            // Configure columns for accounts grid
            dgvAccounts.Columns.Clear();
            
            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AccountCode",
                HeaderText = "Code",
                DataPropertyName = "AccountCode",
                Width = 80,
                ReadOnly = true
            });

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AccountName",
                HeaderText = "Account Name",
                DataPropertyName = "AccountName",
                Width = 200,
                ReadOnly = true
            });

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AccountType",
                HeaderText = "Type",
                DataPropertyName = "AccountTypeDisplay",
                Width = 100,
                ReadOnly = true
            });

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AccountCategory",
                HeaderText = "Category",
                DataPropertyName = "AccountCategoryDisplay",
                Width = 120,
                ReadOnly = true
            });

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NormalBalance",
                HeaderText = "Normal Balance",
                DataPropertyName = "NormalBalance",
                Width = 100,
                ReadOnly = true
            });

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IsActive",
                HeaderText = "Active",
                DataPropertyName = "StatusText",
                Width = 80,
                ReadOnly = true
            });
        }

        #endregion

        #region Data Loading Methods

        private void LoadInitialData()
        {
            try
            {
                _isLoading = true;
                LoadAllAccounts();
                LoadParentAccounts();
                LoadTreeView();
                LoadDataGridView();
                ClearForm();
                SetFormMode(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void LoadAllAccounts()
        {
            try
            {
                _allAccounts = _chartOfAccountService.GetAllChartOfAccounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _allAccounts = new List<ChartOfAccount>();
            }
        }

        private void LoadTreeView()
        {
            try
            {
                treeViewAccounts.Nodes.Clear();
                var rootAccounts = _allAccounts.Where(a => !a.ParentAccountId.HasValue).ToList();

                foreach (var rootAccount in rootAccounts.OrderBy(a => a.AccountCode))
                {
                    var rootNode = CreateTreeNode(rootAccount);
                    treeViewAccounts.Nodes.Add(rootNode);
                    LoadChildNodes(rootNode, rootAccount.AccountId);
                }

                treeViewAccounts.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tree view: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TreeNode CreateTreeNode(ChartOfAccount account)
        {
            var node = new TreeNode
            {
                Text = $"{account.AccountCode} - {account.AccountName}",
                Tag = account,
                ImageIndex = 0,
                SelectedImageIndex = 0
            };
            return node;
        }

        private void LoadChildNodes(TreeNode parentNode, int parentAccountId)
        {
            var childAccounts = _allAccounts.Where(a => a.ParentAccountId == parentAccountId).ToList();

            foreach (var childAccount in childAccounts.OrderBy(a => a.AccountCode))
            {
                var childNode = CreateTreeNode(childAccount);
                parentNode.Nodes.Add(childNode);
                LoadChildNodes(childNode, childAccount.AccountId);
            }
        }

        private void LoadDataGridView()
        {
            try
            {
                dgvAccounts.DataSource = _allAccounts;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data grid view: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadParentAccounts()
        {
            try
            {
                cmbParentAccount.Items.Clear();
                cmbParentAccount.Items.Add("(No Parent)");

                // Get accounts that can be parents (not leaf accounts)
                var parentAccounts = _allAccounts.Where(a => a.IsActive && !IsLeafAccount(a.AccountId)).ToList();
                foreach (var account in parentAccounts.OrderBy(a => a.AccountCode))
                {
                    cmbParentAccount.Items.Add(account);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading parent accounts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsLeafAccount(int accountId)
        {
            return !_allAccounts.Any(a => a.ParentAccountId == accountId);
        }

        #endregion

        #region Form Mode Management

        private void SetFormMode(bool editMode)
        {
            _isEditMode = editMode;

            // Enable/disable controls based on mode
            bool enableEdit = editMode;
            bool enableView = !editMode;

            // Form controls
            txtAccountCode.Enabled = enableEdit;
            txtAccountName.Enabled = enableEdit;
            cmbAccountType.Enabled = enableEdit;
            cmbAccountCategory.Enabled = enableEdit;
            cmbParentAccount.Enabled = enableEdit;
            cmbNormalBalance.Enabled = enableEdit;
            txtDescription.Enabled = enableEdit;
            chkIsActive.Enabled = enableEdit;

            // Buttons
            btnNew.Enabled = enableView;
            btnEdit.Enabled = enableView && _currentAccount != null;
            btnDelete.Enabled = enableView && _currentAccount != null && !_currentAccount.IsSystemAccount;
            btnSave.Enabled = enableEdit;
            btnCancel.Enabled = enableEdit;
            btnRefresh.Enabled = enableView;

            // Tree view and grid
            treeViewAccounts.Enabled = enableView;
            dgvAccounts.Enabled = enableView;
        }

        private void ClearForm()
        {
            txtAccountCode.Clear();
            txtAccountName.Clear();
            cmbAccountType.SelectedIndex = -1;
            cmbAccountCategory.SelectedIndex = -1;
            if (cmbParentAccount.Items.Count > 0)
                cmbParentAccount.SelectedIndex = -1;
            cmbNormalBalance.SelectedIndex = -1;
            txtDescription.Clear();
            chkIsActive.Checked = true;
            _currentAccount = null;
        }

        #endregion

        #region Event Handlers

        private void ChartOfAccountsForm_Load(object sender, EventArgs e)
        {
            LoadInitialData();
        }

        private void ChartOfAccountsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isEditMode)
            {
                var result = MessageBox.Show("You have unsaved changes. Do you want to save them?", 
                    "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    BtnSave_Click(sender, e);
                    if (_isEditMode) // If save failed
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ClearForm();
                SetFormMode(true);
                txtAccountCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting new account: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentAccount == null)
                {
                    MessageBox.Show("Please select an account to edit.", "No Selection", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LoadAccountToForm(_currentAccount);
                SetFormMode(true);
                txtAccountCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing account: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentAccount == null)
                {
                    MessageBox.Show("Please select an account to delete.", "No Selection", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Use enhanced validation to check if account can be deleted
                if (!_chartOfAccountService.CanDeleteAccount(_currentAccount.AccountId))
                {
                    var errorMessage = "Cannot delete this account:\n\n";
                    
                    if (_currentAccount.IsSystemAccount)
                        errorMessage += "• This is a system account and cannot be deleted\n";
                    
                    var childAccounts = _chartOfAccountService.GetChartOfAccountsByParent(_currentAccount.AccountId);
                    if (childAccounts.Any())
                        errorMessage += $"• This account has {childAccounts.Count} child account(s) that must be deleted first\n";
                    
                    // TODO: Add transaction check when journal entries are integrated
                    // if (HasTransactions(_currentAccount.AccountId))
                    //     errorMessage += "• This account has transactions and cannot be deleted\n";
                    
                    MessageBox.Show(errorMessage, "Cannot Delete Account", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show($"Are you sure you want to delete account '{_currentAccount.AccountName}'?\n\n" +
                    "This action cannot be undone.", 
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (_chartOfAccountService.DeleteChartOfAccount(_currentAccount.AccountId))
                    {
                        MessageBox.Show("Account deleted successfully.", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadInitialData();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete account.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting account: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                var account = CreateAccountFromForm();
                bool success;

                if (_currentAccount == null)
                {
                    // Create new account
                    var accountId = _chartOfAccountService.CreateChartOfAccount(account);
                    success = accountId > 0;
                }
                else
                {
                    // Update existing account
                    account.AccountId = _currentAccount.AccountId;
                    success = _chartOfAccountService.UpdateChartOfAccount(account);
                }

                if (success)
                {
                    MessageBox.Show("Account saved successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInitialData();
                    SetFormMode(false);
                }
                else
                {
                    MessageBox.Show("Failed to save account.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving account: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                SetFormMode(false);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error canceling operation: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInitialData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TreeViewAccounts_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node?.Tag is ChartOfAccount account)
                {
                    _currentAccount = account;
                    LoadAccountToForm(account);
                    SetFormMode(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting account: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvAccounts_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvAccounts.SelectedRows.Count > 0)
                {
                    var account = dgvAccounts.SelectedRows[0].DataBoundItem as ChartOfAccount;
                    if (account != null)
                    {
                        _currentAccount = account;
                        LoadAccountToForm(account);
                        SetFormMode(false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting account: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoading) return;

                if (cmbAccountType.SelectedItem != null)
                {
                    var accountType = cmbAccountType.SelectedItem.ToString();
                    var categories = _chartOfAccountService.GetAccountCategoriesByType(accountType);
                    
                    cmbAccountCategory.Items.Clear();
                    cmbAccountCategory.Items.AddRange(categories.ToArray());
                    
                    // Set normal balance based on account type
                    var normalBalance = _chartOfAccountService.GetNormalBalanceForAccountType(accountType);
                    cmbNormalBalance.SelectedItem = normalBalance;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating account category: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbAccountCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoading) return;
                // Additional logic for category changes if needed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating account category: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtAccountCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoading) return;
                
                // Real-time validation for account code
                var accountCode = txtAccountCode.Text.Trim();
                if (!string.IsNullOrEmpty(accountCode))
                {
                    var accountType = cmbAccountType.SelectedItem?.ToString();
                    var parentAccountId = cmbParentAccount.SelectedValue as int?;
                    var excludeAccountId = _currentAccount?.AccountId;
                    
                    if (!string.IsNullOrEmpty(accountType))
                    {
                        if (!_chartOfAccountService.ValidateAccountCode(accountCode, accountType, parentAccountId, excludeAccountId))
                        {
                            // Show validation error in status or tooltip
                            toolTip.SetToolTip(txtAccountCode, "Invalid account code format or already exists");
                            txtAccountCode.BackColor = Color.LightPink;
                        }
                        else
                        {
                            toolTip.SetToolTip(txtAccountCode, "");
                            txtAccountCode.BackColor = SystemColors.Window;
                        }
                    }
                }
                else
                {
                    txtAccountCode.BackColor = SystemColors.Window;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountsForm.TxtAccountCode_TextChanged", ex);
            }
        }

        private void TxtAccountName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isLoading) return;
                
                // Real-time validation for account name
                var accountName = txtAccountName.Text.Trim();
                if (!string.IsNullOrEmpty(accountName))
                {
                    var excludeAccountId = _currentAccount?.AccountId;
                    
                    if (!_chartOfAccountService.ValidateAccountName(accountName, excludeAccountId))
                    {
                        // Show validation error in status or tooltip
                        toolTip.SetToolTip(txtAccountName, "Account name already exists or contains reserved words");
                        txtAccountName.BackColor = Color.LightPink;
                    }
                    else
                    {
                        toolTip.SetToolTip(txtAccountName, "");
                        txtAccountName.BackColor = SystemColors.Window;
                    }
                }
                else
                {
                    txtAccountName.BackColor = SystemColors.Window;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountsForm.TxtAccountName_TextChanged", ex);
            }
        }

        #endregion

        #region Helper Methods

        private void LoadAccountToForm(ChartOfAccount account)
        {
            try
            {
                _isLoading = true;

                txtAccountCode.Text = account.AccountCode;
                txtAccountName.Text = account.AccountName;
                cmbAccountType.SelectedItem = account.AccountType;
                cmbAccountCategory.SelectedItem = account.AccountCategory;
                cmbNormalBalance.SelectedItem = account.NormalBalance;
                txtDescription.Text = account.Description ?? "";
                chkIsActive.Checked = account.IsActive;

                // Set parent account
                if (account.ParentAccountId.HasValue)
                {
                    var parentAccount = _allAccounts.FirstOrDefault(a => a.AccountId == account.ParentAccountId.Value);
                    if (parentAccount != null)
                    {
                        cmbParentAccount.SelectedItem = parentAccount;
                    }
                }
                else
                {
                    // Only set SelectedIndex if the combo box has items
                    if (cmbParentAccount.Items.Count > 0)
                    {
                        cmbParentAccount.SelectedIndex = 0; // "(No Parent)"
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading account to form: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private ChartOfAccount CreateAccountFromForm()
        {
            var account = new ChartOfAccount
            {
                AccountCode = txtAccountCode.Text.Trim(),
                AccountName = txtAccountName.Text.Trim(),
                AccountType = cmbAccountType.SelectedItem?.ToString(),
                AccountCategory = cmbAccountCategory.SelectedItem?.ToString(),
                NormalBalance = cmbNormalBalance.SelectedItem?.ToString(),
                Description = txtDescription.Text.Trim(),
                IsActive = chkIsActive.Checked,
                IsSystemAccount = false
            };

            // Set parent account
            if (cmbParentAccount.SelectedItem is ChartOfAccount parentAccount)
            {
                account.ParentAccountId = parentAccount.AccountId;
            }
            else
            {
                account.ParentAccountId = null;
            }

            return account;
        }

        private bool ValidateForm()
        {
            try
            {
                // Create account object from form data for comprehensive validation
                var account = CreateAccountFromForm();
                
                // Use enhanced validation from service
                var errors = _chartOfAccountService.GetComprehensiveValidationErrors(account);

                if (errors.Any())
                {
                    var errorMessage = "Please fix the following validation errors:\n\n" + 
                                    string.Join("\n• ", errors);
                    
                    MessageBox.Show(errorMessage, "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating form: {ex.Message}", "Validation Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion
    }
}
