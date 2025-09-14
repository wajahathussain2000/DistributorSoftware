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
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bank accounts: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBankStatements()
        {
            try
            {
                _bankStatements = _bankStatementService.GetAllBankStatements();
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bank statements: {ex.Message}", "Error", 
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
                // UI controls will be available when designer files are created
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading reconciliations: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save functionality - will be implemented when designer files are created
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            // New functionality - will be implemented when designer files are created
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Edit functionality - will be implemented when designer files are created
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Delete functionality - will be implemented when designer files are created
        }
    }
}