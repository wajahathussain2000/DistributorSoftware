using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DistributionSoftware.Models;
using DistributionSoftware.Business;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class TrialBalanceForm : Form
    {
        private IChartOfAccountService _chartOfAccountService;
        private IJournalVoucherService _journalVoucherService;

        public TrialBalanceForm()
        {
            InitializeComponent();
            _chartOfAccountService = new ChartOfAccountService();
            _journalVoucherService = new JournalVoucherService();

            InitializeForm();
        }

        private void InitializeForm()
        {
            SetDefaultDateRange();
            UpdateButtonState();
        }

        private void SetDefaultDateRange()
        {
            dtpAsOfDate.Value = DateTime.Now;
        }

        private void UpdateButtonState()
        {
            btnGenerate.Enabled = true;
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                var asOfDate = dtpAsOfDate.Value.Date;
                GenerateTrialBalance(asOfDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating trial balance: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateTrialBalance(DateTime asOfDate)
        {
            try
            {
                // Get all active accounts
                var accounts = _chartOfAccountService.GetActiveChartOfAccounts();

                // Create trial balance data
                var trialBalanceData = new List<TrialBalanceItem>();
                decimal totalDebits = 0;
                decimal totalCredits = 0;

                foreach (var account in accounts.OrderBy(a => a.AccountCode))
                {
                    var balance = _journalVoucherService.GetAccountBalance(account.AccountId, asOfDate);
                    
                    var item = new TrialBalanceItem
                    {
                        AccountCode = account.AccountCode,
                        AccountName = account.AccountName,
                        AccountType = account.AccountType,
                        NormalBalance = account.NormalBalance,
                        Balance = balance
                    };

                    // Determine debit and credit amounts based on normal balance
                    if (account.NormalBalance == "DEBIT")
                    {
                        if (balance >= 0)
                        {
                            item.DebitAmount = balance;
                            item.CreditAmount = 0;
                        }
                        else
                        {
                            item.DebitAmount = 0;
                            item.CreditAmount = Math.Abs(balance);
                        }
                    }
                    else
                    {
                        if (balance >= 0)
                        {
                            item.DebitAmount = 0;
                            item.CreditAmount = balance;
                        }
                        else
                        {
                            item.DebitAmount = Math.Abs(balance);
                            item.CreditAmount = 0;
                        }
                    }

                    totalDebits += item.DebitAmount;
                    totalCredits += item.CreditAmount;

                    trialBalanceData.Add(item);
                }

                // Display trial balance
                DisplayTrialBalance(trialBalanceData, asOfDate, totalDebits, totalCredits);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating trial balance: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayTrialBalance(List<TrialBalanceItem> trialBalanceData, DateTime asOfDate, decimal totalDebits, decimal totalCredits)
        {
            // Clear existing data
            dgvTrialBalance.DataSource = null;
            dgvTrialBalance.Rows.Clear();

            // Set up columns
            SetupTrialBalanceGrid();

            // Add header information
            lblReportTitle.Text = "Trial Balance";
            lblAsOfDate.Text = $"As of: {asOfDate:dd/MM/yyyy}";
            lblGeneratedOn.Text = $"Generated on: {DateTime.Now:dd/MM/yyyy HH:mm}";

            // Add data rows
            foreach (var item in trialBalanceData)
            {
                dgvTrialBalance.Rows.Add(
                    item.AccountCode,
                    item.AccountName,
                    item.AccountType,
                    item.NormalBalance,
                    item.DebitAmount > 0 ? item.DebitAmount.ToString("N2") : "",
                    item.CreditAmount > 0 ? item.CreditAmount.ToString("N2") : ""
                );
            }

            // Add totals row
            dgvTrialBalance.Rows.Add(
                "", "", "", "TOTALS",
                totalDebits.ToString("N2"),
                totalCredits.ToString("N2")
            );

            // Format totals row
            if (dgvTrialBalance.Rows.Count > 0)
            {
                var totalsRow = dgvTrialBalance.Rows[dgvTrialBalance.Rows.Count - 1];
                totalsRow.DefaultCellStyle.Font = new Font(dgvTrialBalance.Font, FontStyle.Bold);
                totalsRow.DefaultCellStyle.BackColor = Color.LightGray;
            }

            // Show summary
            lblSummary.Text = $"Total Debits: {totalDebits:N2} | Total Credits: {totalCredits:N2}";
            
            // Check if trial balance is balanced
            if (totalDebits == totalCredits)
            {
                lblSummary.Text += " | ✓ BALANCED";
                lblSummary.ForeColor = Color.Green;
            }
            else
            {
                var difference = totalDebits - totalCredits;
                lblSummary.Text += $" | ✗ OUT OF BALANCE (Difference: {difference:N2})";
                lblSummary.ForeColor = Color.Red;
            }
        }

        private void SetupTrialBalanceGrid()
        {
            dgvTrialBalance.Columns.Clear();
            dgvTrialBalance.AllowUserToAddRows = false;
            dgvTrialBalance.ReadOnly = true;
            dgvTrialBalance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTrialBalance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvTrialBalance.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AccountCode",
                HeaderText = "Account Code",
                Width = 100
            });

            dgvTrialBalance.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AccountName",
                HeaderText = "Account Name",
                Width = 200
            });

            dgvTrialBalance.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AccountType",
                HeaderText = "Type",
                Width = 100
            });

            dgvTrialBalance.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NormalBalance",
                HeaderText = "Normal Balance",
                Width = 100
            });

            dgvTrialBalance.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DebitAmount",
                HeaderText = "Debit",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvTrialBalance.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreditAmount",
                HeaderText = "Credit",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTrialBalance.Rows.Count == 0)
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
                if (dgvTrialBalance.Rows.Count == 0)
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

        private void DtpAsOfDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }
    }

    public class TrialBalanceItem
    {
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string NormalBalance { get; set; }
        public decimal Balance { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
    }
}
