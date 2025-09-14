using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class GeneralLedgerForm : Form
    {
        private IChartOfAccountService _chartOfAccountService;
        private IJournalVoucherService _journalVoucherService;

        public GeneralLedgerForm()
        {
            InitializeComponent();
            _chartOfAccountService = new ChartOfAccountService();
            _journalVoucherService = new JournalVoucherService();

            InitializeForm();
        }

        private void InitializeForm()
        {
            LoadAccounts();
            SetDefaultDateRange();
            UpdateButtonState();
        }

        private void LoadAccounts()
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

        private void SetDefaultDateRange()
        {
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            dtpToDate.Value = DateTime.Now;
        }

        private void UpdateButtonState()
        {
            btnGenerate.Enabled = cmbAccount.SelectedValue != null;
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbAccount.SelectedValue == null)
                {
                    MessageBox.Show("Please select an account.", "No Account Selected", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var accountId = (int)cmbAccount.SelectedValue;
                var fromDate = dtpFromDate.Value.Date;
                var toDate = dtpToDate.Value.Date;

                if (fromDate > toDate)
                {
                    MessageBox.Show("From date cannot be greater than To date.", "Invalid Date Range", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GenerateGeneralLedger(accountId, fromDate, toDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating general ledger: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateGeneralLedger(int accountId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                // Get account details
                var account = _chartOfAccountService.GetChartOfAccountById(accountId);
                if (account == null)
                {
                    MessageBox.Show("Selected account not found.", "Account Not Found", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get journal voucher details for the account
                var journalDetails = _journalVoucherService.GetAccountJournalVoucherDetails(accountId, fromDate, toDate);

                // Calculate running balance
                var runningBalance = _journalVoucherService.GetAccountBalance(accountId, fromDate.AddDays(-1));
                
                // Create report data
                var reportData = new List<GeneralLedgerItem>();
                
                foreach (var detail in journalDetails.OrderBy(d => d.JournalVoucher.VoucherDate))
                {
                    var item = new GeneralLedgerItem
                    {
                        Date = detail.JournalVoucher.VoucherDate,
                        VoucherNumber = detail.JournalVoucher.VoucherNumber,
                        Reference = detail.JournalVoucher.Reference,
                        Narration = detail.Narration,
                        DebitAmount = detail.DebitAmount,
                        CreditAmount = detail.CreditAmount
                    };

                    // Calculate running balance
                    if (account.NormalBalance == "DEBIT")
                    {
                        runningBalance += detail.DebitAmount - detail.CreditAmount;
                    }
                    else
                    {
                        runningBalance += detail.CreditAmount - detail.DebitAmount;
                    }

                    item.RunningBalance = runningBalance;
                    reportData.Add(item);
                }

                // Display report
                DisplayGeneralLedgerReport(account, reportData, fromDate, toDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating general ledger: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayGeneralLedgerReport(ChartOfAccount account, List<GeneralLedgerItem> reportData, DateTime fromDate, DateTime toDate)
        {
            // Clear existing data
            dgvGeneralLedger.DataSource = null;
            dgvGeneralLedger.Rows.Clear();

            // Set up columns
            SetupGeneralLedgerGrid();

            // Add header information
            lblAccountName.Text = $"{account.AccountCode} - {account.AccountName}";
            lblAccountType.Text = $"Type: {account.AccountType} | Category: {account.AccountCategory}";
            lblDateRange.Text = $"Period: {fromDate:dd/MM/yyyy} to {toDate:dd/MM/yyyy}";
            lblNormalBalance.Text = $"Normal Balance: {account.NormalBalance}";

            // Add data rows
            foreach (var item in reportData)
            {
                dgvGeneralLedger.Rows.Add(
                    item.Date.ToString("dd/MM/yyyy"),
                    item.VoucherNumber,
                    item.Reference,
                    item.Narration,
                    item.DebitAmount > 0 ? item.DebitAmount.ToString("N2") : "",
                    item.CreditAmount > 0 ? item.CreditAmount.ToString("N2") : "",
                    item.RunningBalance.ToString("N2")
                );
            }

            // Add totals row
            var totalDebit = reportData.Sum(r => r.DebitAmount);
            var totalCredit = reportData.Sum(r => r.CreditAmount);
            var finalBalance = reportData.LastOrDefault()?.RunningBalance ?? 0;

            dgvGeneralLedger.Rows.Add(
                "", "", "", "TOTALS",
                totalDebit.ToString("N2"),
                totalCredit.ToString("N2"),
                finalBalance.ToString("N2")
            );

            // Format totals row
            if (dgvGeneralLedger.Rows.Count > 0)
            {
                var totalsRow = dgvGeneralLedger.Rows[dgvGeneralLedger.Rows.Count - 1];
                totalsRow.DefaultCellStyle.Font = new Font(dgvGeneralLedger.Font, FontStyle.Bold);
                totalsRow.DefaultCellStyle.BackColor = Color.LightGray;
            }

            // Show summary
            lblSummary.Text = $"Total Debits: {totalDebit:N2} | Total Credits: {totalCredit:N2} | Final Balance: {finalBalance:N2}";
        }

        private void SetupGeneralLedgerGrid()
        {
            dgvGeneralLedger.Columns.Clear();
            dgvGeneralLedger.AllowUserToAddRows = false;
            dgvGeneralLedger.ReadOnly = true;
            dgvGeneralLedger.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGeneralLedger.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvGeneralLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "Date",
                Width = 80
            });

            dgvGeneralLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "VoucherNumber",
                HeaderText = "Voucher No.",
                Width = 100
            });

            dgvGeneralLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Reference",
                HeaderText = "Reference",
                Width = 100
            });

            dgvGeneralLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Narration",
                HeaderText = "Narration",
                Width = 200
            });

            dgvGeneralLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DebitAmount",
                HeaderText = "Debit",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvGeneralLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreditAmount",
                HeaderText = "Credit",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvGeneralLedger.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RunningBalance",
                HeaderText = "Balance",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvGeneralLedger.Rows.Count == 0)
                {
                    MessageBox.Show("No data to print. Please generate a report first.", "No Data", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // TODO: Implement printing functionality
                MessageBox.Show("Print functionality will be implemented soon.", "Print", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing report: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvGeneralLedger.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export. Please generate a report first.", "No Data", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // TODO: Implement export functionality
                MessageBox.Show("Export functionality will be implemented soon.", "Export", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        private void DtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        private void DtpToDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }
    }

    public class GeneralLedgerItem
    {
        public DateTime Date { get; set; }
        public string VoucherNumber { get; set; }
        public string Reference { get; set; }
        public string Narration { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal RunningBalance { get; set; }
    }
}
