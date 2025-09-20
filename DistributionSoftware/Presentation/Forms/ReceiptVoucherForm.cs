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
    public partial class ReceiptVoucherForm : Form
    {
        private IReceiptVoucherService _receiptVoucherService;
        private IChartOfAccountService _chartOfAccountService;
        private IBankAccountService _bankAccountService;
        private ReceiptVoucher _currentVoucher;
        private bool _isEditMode;

        public ReceiptVoucherForm()
        {
            InitializeComponent();
            _receiptVoucherService = new ReceiptVoucherService();
            _chartOfAccountService = new ChartOfAccountService();
            
            var connectionString = DistributionSoftware.Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            var bankAccountRepository = new DistributionSoftware.DataAccess.BankAccountRepository(connectionString);
            _bankAccountService = new BankAccountService(bankAccountRepository);
            
            _currentVoucher = new ReceiptVoucher();
            _isEditMode = false;

            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadChartOfAccounts();
            LoadBankAccounts();
            LoadReceiptVouchers();
            SetDefaultValues();
            UpdateButtonState();
            SetupReceiptModeComboBox();
            SetupDataGridView();
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

        private void LoadBankAccounts()
        {
            try
            {
                var bankAccounts = _bankAccountService.GetAllBankAccounts();
                cmbBankAccount.DataSource = bankAccounts;
                cmbBankAccount.DisplayMember = "AccountName";
                cmbBankAccount.ValueMember = "BankAccountId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bank accounts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadReceiptVouchers()
        {
            try
            {
                var vouchers = _receiptVoucherService.GetAllReceiptVouchers();
                dgvReceiptVouchers.DataSource = vouchers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading receipt vouchers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultValues()
        {
            txtVoucherNumber.Text = _receiptVoucherService.GenerateReceiptVoucherNumber();
            dtpVoucherDate.Value = DateTime.Now;
            txtNarration.Text = "";
            txtReference.Text = "";
            txtRemarks.Text = "";
            numAmount.Value = 0;
            cmbReceiptMode.SelectedIndex = 0;
            ClearReceiptDetails();
        }

        private void SetupReceiptModeComboBox()
        {
            cmbReceiptMode.Items.Clear();
            cmbReceiptMode.Items.AddRange(new[] { "CASH", "CARD", "CHEQUE", "BANK_TRANSFER", "EASYPAISA", "JAZZCASH" });
            cmbReceiptMode.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            dgvReceiptVouchers.AutoGenerateColumns = false;
            dgvReceiptVouchers.Columns.Clear();

            // Add columns
            dgvReceiptVouchers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "VoucherNumber",
                HeaderText = "Voucher Number",
                Name = "VoucherNumber",
                Width = 150
            });

            dgvReceiptVouchers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "VoucherDate",
                HeaderText = "Date",
                Name = "VoucherDate",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvReceiptVouchers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Narration",
                HeaderText = "Narration",
                Name = "Narration",
                Width = 200
            });

            dgvReceiptVouchers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ReceiptMode",
                HeaderText = "Receipt Mode",
                Name = "ReceiptMode",
                Width = 120
            });

            dgvReceiptVouchers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount",
                Name = "Amount",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            dgvReceiptVouchers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Name = "Status",
                Width = 80
            });
        }

        private void UpdateButtonState()
        {
            btnSave.Enabled = _isEditMode;
            btnCancel.Enabled = _isEditMode;
            btnNew.Enabled = !_isEditMode;
            btnEdit.Enabled = !_isEditMode && _currentVoucher != null && _currentVoucher.ReceiptVoucherId > 0;
            btnDelete.Enabled = !_isEditMode && _currentVoucher != null && _currentVoucher.ReceiptVoucherId > 0;
        }

        private void ClearReceiptDetails()
        {
            txtCardNumber.Text = "";
            txtCardType.Text = "";
            txtTransactionId.Text = "";
            txtBankName.Text = "";
            txtChequeNumber.Text = "";
            dtpChequeDate.Value = DateTime.Now;
            txtMobileNumber.Text = "";
            txtReceiptReference.Text = "";
            cmbBankAccount.SelectedIndex = -1;
        }

        private void LoadVoucherToForm(ReceiptVoucher voucher)
        {
            if (voucher == null) return;

            txtVoucherNumber.Text = voucher.VoucherNumber;
            dtpVoucherDate.Value = voucher.VoucherDate;
            txtNarration.Text = voucher.Narration;
            txtReference.Text = voucher.Reference ?? "";
            txtRemarks.Text = voucher.Remarks ?? "";
            numAmount.Value = voucher.Amount;
            cmbAccount.SelectedValue = voucher.AccountId;
            cmbReceiptMode.Text = voucher.ReceiptMode;
            cmbBankAccount.SelectedValue = voucher.BankAccountId ?? -1;

            // Receipt details
            txtCardNumber.Text = voucher.CardNumber ?? "";
            txtCardType.Text = voucher.CardType ?? "";
            txtTransactionId.Text = voucher.TransactionId ?? "";
            txtBankName.Text = voucher.BankName ?? "";
            txtChequeNumber.Text = voucher.ChequeNumber ?? "";
            dtpChequeDate.Value = voucher.ChequeDate ?? DateTime.Now;
            txtMobileNumber.Text = voucher.MobileNumber ?? "";
            txtReceiptReference.Text = voucher.ReceiptReference ?? "";
        }

        private ReceiptVoucher GetVoucherFromForm()
        {
            var voucher = new ReceiptVoucher
            {
                ReceiptVoucherId = _currentVoucher?.ReceiptVoucherId ?? 0,
                VoucherNumber = txtVoucherNumber.Text,
                VoucherDate = dtpVoucherDate.Value,
                Narration = txtNarration.Text,
                Reference = txtReference.Text,
                Remarks = txtRemarks.Text,
                Amount = numAmount.Value,
                AccountId = (int)cmbAccount.SelectedValue,
                ReceiptMode = cmbReceiptMode.Text,
                BankAccountId = cmbBankAccount.SelectedValue != null ? (int?)cmbBankAccount.SelectedValue : null,
                CardNumber = txtCardNumber.Text,
                CardType = txtCardType.Text,
                TransactionId = txtTransactionId.Text,
                BankName = txtBankName.Text,
                ChequeNumber = txtChequeNumber.Text,
                ChequeDate = dtpChequeDate.Value,
                MobileNumber = txtMobileNumber.Text,
                ReceiptReference = txtReceiptReference.Text,
                Status = "ACTIVE"
            };

            return voucher;
        }

        private void UpdateReceiptDetailsVisibility()
        {
            var receiptMode = cmbReceiptMode.Text?.ToUpper();
            
            // Show/hide fields based on receipt mode
            bool showCardFields = receiptMode == "CARD";
            bool showChequeFields = receiptMode == "CHEQUE";
            bool showBankFields = receiptMode == "BANK_TRANSFER";
            bool showMobileFields = receiptMode == "EASYPAISA" || receiptMode == "JAZZCASH";

            txtCardNumber.Visible = showCardFields;
            lblCardNumber.Visible = showCardFields;
            txtCardType.Visible = showCardFields;
            lblCardType.Visible = showCardFields;

            txtChequeNumber.Visible = showChequeFields;
            lblChequeNumber.Visible = showChequeFields;
            dtpChequeDate.Visible = showChequeFields;
            lblChequeDate.Visible = showChequeFields;

            txtBankName.Visible = showBankFields;
            lblBankName.Visible = showBankFields;
            txtTransactionId.Visible = showBankFields;
            lblTransactionId.Visible = showBankFields;

            txtMobileNumber.Visible = showMobileFields;
            lblMobileNumber.Visible = showMobileFields;

            // Bank account is visible for all non-cash modes
            cmbBankAccount.Visible = receiptMode != "CASH";
            lblBankAccount.Visible = receiptMode != "CASH";
        }

        #region Event Handlers

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            _currentVoucher = new ReceiptVoucher();
            _isEditMode = true;
            SetDefaultValues();
            UpdateButtonState();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_currentVoucher == null)
            {
                MessageBox.Show("Please select a receipt voucher to edit.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isEditMode = true;
            UpdateButtonState();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var voucher = GetVoucherFromForm();
                
                if (!_receiptVoucherService.ValidateReceiptVoucher(voucher))
                {
                    var errors = _receiptVoucherService.GetValidationErrors(voucher);
                    MessageBox.Show($"Validation failed:\n{string.Join("\n", errors)}", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (voucher.ReceiptVoucherId == 0)
                {
                    _receiptVoucherService.CreateReceiptVoucher(voucher);
                    MessageBox.Show("Receipt voucher created successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _receiptVoucherService.UpdateReceiptVoucher(voucher);
                    MessageBox.Show("Receipt voucher updated successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _isEditMode = false;
                UpdateButtonState();
                LoadReceiptVouchers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving receipt voucher: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            _isEditMode = false;
            if (_currentVoucher != null && _currentVoucher.ReceiptVoucherId > 0)
            {
                LoadVoucherToForm(_currentVoucher);
            }
            else
            {
                SetDefaultValues();
            }
            UpdateButtonState();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_currentVoucher == null)
            {
                MessageBox.Show("Please select a receipt voucher to delete.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete receipt voucher {_currentVoucher.VoucherNumber}?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _receiptVoucherService.DeleteReceiptVoucher(_currentVoucher.ReceiptVoucherId);
                    MessageBox.Show("Receipt voucher deleted successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    _currentVoucher = new ReceiptVoucher();
                    SetDefaultValues();
                    LoadReceiptVouchers();
                    UpdateButtonState();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting receipt voucher: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DgvReceiptVouchers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceiptVouchers.SelectedRows.Count > 0)
            {
                var selectedVoucher = dgvReceiptVouchers.SelectedRows[0].DataBoundItem as ReceiptVoucher;
                if (selectedVoucher != null)
                {
                    _currentVoucher = selectedVoucher;
                    LoadVoucherToForm(selectedVoucher);
                    UpdateButtonState();
                }
            }
        }

        private void CmbReceiptMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateReceiptDetailsVisibility();
        }

        #endregion
    }
}

