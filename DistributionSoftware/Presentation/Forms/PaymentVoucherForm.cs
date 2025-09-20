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
    public partial class PaymentVoucherForm : Form
    {
        private IPaymentVoucherService _paymentVoucherService;
        private IChartOfAccountService _chartOfAccountService;
        private IBankAccountService _bankAccountService;
        private PaymentVoucher _currentVoucher;
        private bool _isEditMode;

        public PaymentVoucherForm()
        {
            InitializeComponent();
            _paymentVoucherService = new PaymentVoucherService();
            _chartOfAccountService = new ChartOfAccountService();
            
            var connectionString = DistributionSoftware.Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            var bankAccountRepository = new DistributionSoftware.DataAccess.BankAccountRepository(connectionString);
            _bankAccountService = new BankAccountService(bankAccountRepository);
            
            _currentVoucher = new PaymentVoucher();
            _isEditMode = false;

            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadChartOfAccounts();
            LoadBankAccounts();
            LoadPaymentVouchers();
            SetDefaultValues();
            UpdateButtonState();
            SetupPaymentModeComboBox();
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

        private void LoadPaymentVouchers()
        {
            try
            {
                var vouchers = _paymentVoucherService.GetAllPaymentVouchers();
                dgvPaymentVouchers.DataSource = vouchers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payment vouchers: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaultValues()
        {
            txtVoucherNumber.Text = _paymentVoucherService.GeneratePaymentVoucherNumber();
            dtpVoucherDate.Value = DateTime.Now;
            txtNarration.Text = "";
            txtReference.Text = "";
            txtRemarks.Text = "";
            numAmount.Value = 0;
            cmbPaymentMode.SelectedIndex = 0;
            ClearPaymentDetails();
        }

        private void SetupPaymentModeComboBox()
        {
            cmbPaymentMode.Items.Clear();
            cmbPaymentMode.Items.AddRange(new[] { "CASH", "CARD", "CHEQUE", "BANK_TRANSFER", "EASYPAISA", "JAZZCASH" });
            cmbPaymentMode.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            dgvPaymentVouchers.AutoGenerateColumns = false;
            dgvPaymentVouchers.Columns.Clear();

            // Add columns
            dgvPaymentVouchers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "VoucherNumber",
                HeaderText = "Voucher Number",
                Name = "VoucherNumber",
                Width = 150
            });

            dgvPaymentVouchers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "VoucherDate",
                HeaderText = "Date",
                Name = "VoucherDate",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvPaymentVouchers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Narration",
                HeaderText = "Narration",
                Name = "Narration",
                Width = 200
            });

            dgvPaymentVouchers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentMode",
                HeaderText = "Payment Mode",
                Name = "PaymentMode",
                Width = 120
            });

            dgvPaymentVouchers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Amount",
                Name = "Amount",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            dgvPaymentVouchers.Columns.Add(new DataGridViewTextBoxColumn
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
            btnEdit.Enabled = !_isEditMode && _currentVoucher != null && _currentVoucher.PaymentVoucherId > 0;
            btnDelete.Enabled = !_isEditMode && _currentVoucher != null && _currentVoucher.PaymentVoucherId > 0;
        }

        private void ClearPaymentDetails()
        {
            txtCardNumber.Text = "";
            txtCardType.Text = "";
            txtTransactionId.Text = "";
            txtBankName.Text = "";
            txtChequeNumber.Text = "";
            dtpChequeDate.Value = DateTime.Now;
            txtMobileNumber.Text = "";
            txtPaymentReference.Text = "";
            cmbBankAccount.SelectedIndex = -1;
        }

        private void LoadVoucherToForm(PaymentVoucher voucher)
        {
            if (voucher == null) return;

            txtVoucherNumber.Text = voucher.VoucherNumber;
            dtpVoucherDate.Value = voucher.VoucherDate;
            txtNarration.Text = voucher.Narration;
            txtReference.Text = voucher.Reference ?? "";
            txtRemarks.Text = voucher.Remarks ?? "";
            numAmount.Value = voucher.Amount;
            cmbAccount.SelectedValue = voucher.AccountId;
            cmbPaymentMode.Text = voucher.PaymentMode;
            cmbBankAccount.SelectedValue = voucher.BankAccountId ?? -1;

            // Payment details
            txtCardNumber.Text = voucher.CardNumber ?? "";
            txtCardType.Text = voucher.CardType ?? "";
            txtTransactionId.Text = voucher.TransactionId ?? "";
            txtBankName.Text = voucher.BankName ?? "";
            txtChequeNumber.Text = voucher.ChequeNumber ?? "";
            dtpChequeDate.Value = voucher.ChequeDate ?? DateTime.Now;
            txtMobileNumber.Text = voucher.MobileNumber ?? "";
            txtPaymentReference.Text = voucher.PaymentReference ?? "";
        }

        private PaymentVoucher GetVoucherFromForm()
        {
            var voucher = new PaymentVoucher
            {
                PaymentVoucherId = _currentVoucher?.PaymentVoucherId ?? 0,
                VoucherNumber = txtVoucherNumber.Text,
                VoucherDate = dtpVoucherDate.Value,
                Narration = txtNarration.Text,
                Reference = txtReference.Text,
                Remarks = txtRemarks.Text,
                Amount = numAmount.Value,
                AccountId = (int)cmbAccount.SelectedValue,
                PaymentMode = cmbPaymentMode.Text,
                BankAccountId = cmbBankAccount.SelectedValue != null ? (int?)cmbBankAccount.SelectedValue : null,
                CardNumber = txtCardNumber.Text,
                CardType = txtCardType.Text,
                TransactionId = txtTransactionId.Text,
                BankName = txtBankName.Text,
                ChequeNumber = txtChequeNumber.Text,
                ChequeDate = dtpChequeDate.Value,
                MobileNumber = txtMobileNumber.Text,
                PaymentReference = txtPaymentReference.Text,
                Status = "ACTIVE"
            };

            return voucher;
        }

        private void UpdatePaymentDetailsVisibility()
        {
            var paymentMode = cmbPaymentMode.Text?.ToUpper();
            
            // Show/hide fields based on payment mode
            bool showCardFields = paymentMode == "CARD";
            bool showChequeFields = paymentMode == "CHEQUE";
            bool showBankFields = paymentMode == "BANK_TRANSFER";
            bool showMobileFields = paymentMode == "EASYPAISA" || paymentMode == "JAZZCASH";

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
            cmbBankAccount.Visible = paymentMode != "CASH";
            lblBankAccount.Visible = paymentMode != "CASH";
        }

        #region Event Handlers

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            _currentVoucher = new PaymentVoucher();
            _isEditMode = true;
            SetDefaultValues();
            UpdateButtonState();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (_currentVoucher == null)
            {
                MessageBox.Show("Please select a payment voucher to edit.", "No Selection", 
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
                
                if (!_paymentVoucherService.ValidatePaymentVoucher(voucher))
                {
                    var errors = _paymentVoucherService.GetValidationErrors(voucher);
                    MessageBox.Show($"Validation failed:\n{string.Join("\n", errors)}", "Validation Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (voucher.PaymentVoucherId == 0)
                {
                    _paymentVoucherService.CreatePaymentVoucher(voucher);
                    MessageBox.Show("Payment voucher created successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _paymentVoucherService.UpdatePaymentVoucher(voucher);
                    MessageBox.Show("Payment voucher updated successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _isEditMode = false;
                UpdateButtonState();
                LoadPaymentVouchers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving payment voucher: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            _isEditMode = false;
            if (_currentVoucher != null && _currentVoucher.PaymentVoucherId > 0)
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
                MessageBox.Show("Please select a payment voucher to delete.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete payment voucher {_currentVoucher.VoucherNumber}?", 
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _paymentVoucherService.DeletePaymentVoucher(_currentVoucher.PaymentVoucherId);
                    MessageBox.Show("Payment voucher deleted successfully.", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    _currentVoucher = new PaymentVoucher();
                    SetDefaultValues();
                    LoadPaymentVouchers();
                    UpdateButtonState();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting payment voucher: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DgvPaymentVouchers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPaymentVouchers.SelectedRows.Count > 0)
            {
                var selectedVoucher = dgvPaymentVouchers.SelectedRows[0].DataBoundItem as PaymentVoucher;
                if (selectedVoucher != null)
                {
                    _currentVoucher = selectedVoucher;
                    LoadVoucherToForm(selectedVoucher);
                    UpdateButtonState();
                }
            }
        }

        private void CmbPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePaymentDetailsVisibility();
        }

        #endregion
    }
}

