using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;
using DistributionSoftware.Business;

namespace DistributionSoftware.Business
{
    /// <summary>
    /// Service implementation for Journal Voucher business logic operations
    /// </summary>
    public class JournalVoucherService : IJournalVoucherService
    {
        private readonly IChartOfAccountService _chartOfAccountService;
        private readonly IJournalVoucherRepository _journalVoucherRepository;
        private readonly IBankIntegrationService _bankIntegrationService;

        public JournalVoucherService()
        {
            var connectionString = Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            _chartOfAccountService = new ChartOfAccountService();
            _journalVoucherRepository = new JournalVoucherRepository(connectionString);
            _bankIntegrationService = new BankIntegrationService(
                new BankAccountRepository(connectionString),
                new JournalVoucherRepository(connectionString),
                new SalesPaymentRepository(connectionString),
                new PurchasePaymentRepository(connectionString),
                new BankStatementRepository(connectionString));
        }

        public JournalVoucherService(IChartOfAccountService chartOfAccountService, IJournalVoucherRepository journalVoucherRepository)
        {
            _chartOfAccountService = chartOfAccountService;
            _journalVoucherRepository = journalVoucherRepository;
            var connectionString = Common.ConfigurationManager.GetConnectionString("DefaultConnection");
            _bankIntegrationService = new BankIntegrationService(
                new BankAccountRepository(connectionString),
                new JournalVoucherRepository(connectionString),
                new SalesPaymentRepository(connectionString),
                new PurchasePaymentRepository(connectionString),
                new BankStatementRepository(connectionString));
        }

        public JournalVoucherService(IChartOfAccountService chartOfAccountService, IJournalVoucherRepository journalVoucherRepository, IBankIntegrationService bankIntegrationService)
        {
            _chartOfAccountService = chartOfAccountService;
            _journalVoucherRepository = journalVoucherRepository;
            _bankIntegrationService = bankIntegrationService;
        }

        #region Basic CRUD Operations

        public int CreateJournalVoucher(JournalVoucher voucher)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"CreateJournalVoucher: VoucherNumber: '{voucher.VoucherNumber}', Details Count: {voucher.Details?.Count ?? 0}");
                System.Diagnostics.Debug.WriteLine($"TotalDebit: PKR {voucher.TotalDebit:N2}, TotalCredit: PKR {voucher.TotalCredit:N2}");
                
                // Generate voucher number if not provided
                if (string.IsNullOrEmpty(voucher.VoucherNumber))
                {
                    voucher.VoucherNumber = GenerateJournalVoucherNumber();
                    System.Diagnostics.Debug.WriteLine($"Generated voucher number: {voucher.VoucherNumber}");
                }

                // Set audit fields
                voucher.CreatedDate = DateTime.Now;
                voucher.CreatedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1; // Default to user ID 1 if not logged in
                voucher.CreatedByName = GetSafeCreatedByName();

                // Auto-detect bank account if not set
                if (!voucher.BankAccountId.HasValue)
                {
                    var bankAccountId = _bankIntegrationService.DetectBankAccountFromReference(voucher.Reference);
                    if (bankAccountId.HasValue)
                    {
                        voucher.BankAccountId = bankAccountId;
                    }
                }

                // Calculate totals
                CalculateVoucherTotals(voucher);
                System.Diagnostics.Debug.WriteLine($"After CalculateVoucherTotals: TotalDebit: PKR {voucher.TotalDebit:N2}, TotalCredit: PKR {voucher.TotalCredit:N2}");
                
                // Validate voucher AFTER setting all required fields
                if (!ValidateJournalVoucher(voucher))
                {
                    var errors = GetValidationErrors(voucher);
                    System.Diagnostics.Debug.WriteLine($"Validation failed: {string.Join(", ", errors)}");
                    throw new ArgumentException($"Invalid journal voucher: {string.Join(", ", errors)}");
                }

                // Save to database using repository
                var voucherId = _journalVoucherRepository.CreateJournalVoucher(voucher);
                voucher.VoucherId = voucherId;

                // Create bank statement entry if bank account is linked
                if (voucher.BankAccountId.HasValue)
                {
                    _bankIntegrationService.CreateBankStatementFromJournalVoucher(voucher);
                }

                return voucherId;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.CreateJournalVoucher", ex);
                throw;
            }
        }

        public bool UpdateJournalVoucher(JournalVoucher voucher)
        {
            try
            {
                // Validate voucher
                if (!ValidateJournalVoucher(voucher))
                {
                    var errors = GetValidationErrors(voucher);
                    throw new ArgumentException($"Invalid journal voucher: {string.Join(", ", errors)}");
                }

                // Set audit fields
                voucher.ModifiedDate = DateTime.Now;
                voucher.ModifiedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1; // Default to user ID 1 if not logged in
                voucher.ModifiedByName = UserSession.IsLoggedIn ? UserSession.GetDisplayName() : "System User";

                // Calculate totals
                CalculateVoucherTotals(voucher);

                // Update in database using repository
                return _journalVoucherRepository.UpdateJournalVoucher(voucher);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.UpdateJournalVoucher", ex);
                throw;
            }
        }

        public bool DeleteJournalVoucher(int voucherId)
        {
            try
            {
                // Delete from database using repository
                return _journalVoucherRepository.DeleteJournalVoucher(voucherId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.DeleteJournalVoucher", ex);
                throw;
            }
        }

        public JournalVoucher GetJournalVoucherById(int voucherId)
        {
            try
            {
                // Get from database using repository
                return _journalVoucherRepository.GetJournalVoucherById(voucherId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetJournalVoucherById", ex);
                throw;
            }
        }

        public JournalVoucher GetJournalVoucherByNumber(string voucherNumber)
        {
            try
            {
                // TODO: Implement repository call when JournalVoucherRepository is created
                return null;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetJournalVoucherByNumber", ex);
                throw;
            }
        }

        public List<JournalVoucher> GetAllJournalVouchers()
        {
            try
            {
                // Get all from database using repository
                return _journalVoucherRepository.GetAllJournalVouchers();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetAllJournalVouchers", ex);
                throw;
            }
        }

        public List<JournalVoucher> GetJournalVouchersByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                // TODO: Implement repository call when JournalVoucherRepository is created
                return new List<JournalVoucher>();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetJournalVouchersByDateRange", ex);
                throw;
            }
        }

        #endregion

        #region Business Logic Operations

        public string GenerateJournalVoucherNumber()
        {
            try
            {
                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month.ToString("00");
                
                // TODO: Get next sequence number from database
                var sequence = 1; // This should come from database
                
                return $"JV-{year}-{month}-{sequence:000}";
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GenerateJournalVoucherNumber", ex);
                return $"JV-{DateTime.Now:yyyy-MM}-001";
            }
        }

        public bool ValidateJournalVoucher(JournalVoucher voucher)
        {
            try
            {
                if (voucher == null) return false;

                var errors = GetValidationErrors(voucher);
                return !errors.Any();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.ValidateJournalVoucher", ex);
                return false;
            }
        }

        public List<string> GetValidationErrors(JournalVoucher voucher)
        {
            var errors = new List<string>();

            if (voucher == null)
            {
                errors.Add("Journal voucher cannot be null");
                return errors;
            }

            // Basic validation
            errors.AddRange(voucher.GetValidationErrors());

            // Business rule validation
            if (voucher.Details != null && voucher.Details.Any())
            {
                foreach (var detail in voucher.Details)
                {
                    if (!detail.IsValid())
                    {
                        errors.AddRange(detail.GetValidationErrors());
                    }
                }

                // Validate account references
                foreach (var detail in voucher.Details)
                {
                    var account = _chartOfAccountService.GetChartOfAccountById(detail.AccountId);
                    if (account == null)
                    {
                        errors.Add($"Account ID {detail.AccountId} not found");
                    }
                    else if (!account.IsActive)
                    {
                        errors.Add($"Account '{account.AccountName}' is not active");
                    }
                }
            }

            return errors;
        }

        #endregion

        #region Sales Integration

        public int CreateSalesInvoiceJournalVoucher(SalesInvoice salesInvoice)
        {
            try
            {
                if (salesInvoice == null)
                    throw new ArgumentNullException(nameof(salesInvoice));

                var voucher = new JournalVoucher
                {
                    VoucherDate = salesInvoice.InvoiceDate,
                    Reference = $"SI-{salesInvoice.InvoiceNumber}",
                    Narration = $"Sales Invoice {salesInvoice.InvoiceNumber} - {salesInvoice.CustomerName}",
                    CreatedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1,
                    CreatedByName = GetSafeCreatedByName()
                };

                // Create journal voucher details based on payment type
                if (salesInvoice.PaymentType?.ToUpper() == "CASH")
                {
                    // Cash Sales: Debit Cash, Credit Sales, Credit Tax
                    CreateCashSalesJournalDetails(voucher, salesInvoice);
                }
                else
                {
                    // Credit Sales: Debit Receivables, Credit Sales, Credit Tax
                    CreateCreditSalesJournalDetails(voucher, salesInvoice);
                }

                return CreateJournalVoucher(voucher);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.CreateSalesInvoiceJournalVoucher", ex);
                throw;
            }
        }

        public int CreateSalesPaymentJournalVoucher(SalesPayment salesPayment)
        {
            try
            {
                if (salesPayment == null)
                    throw new ArgumentNullException(nameof(salesPayment));

                var voucher = new JournalVoucher
                {
                    VoucherDate = salesPayment.PaymentDate,
                    Reference = $"SP-{salesPayment.PaymentId}",
                    Narration = $"Sales Payment - {salesPayment.CustomerName}",
                    CreatedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1,
                    CreatedByName = UserSession.IsLoggedIn ? UserSession.GetDisplayName() : "System User"
                };

                // Create journal voucher details for payment
                CreatePaymentJournalDetails(voucher, salesPayment);

                return CreateJournalVoucher(voucher);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.CreateSalesPaymentJournalVoucher", ex);
                throw;
            }
        }

        public int CreateCustomerReceiptJournalVoucher(CustomerReceipt customerReceipt)
        {
            try
            {
                if (customerReceipt == null)
                    throw new ArgumentNullException(nameof(customerReceipt));

                var voucher = new JournalVoucher
                {
                    VoucherDate = customerReceipt.ReceiptDate,
                    Reference = $"CR-{customerReceipt.ReceiptNumber}",
                    Narration = $"Customer Receipt - {customerReceipt.CustomerName}",
                    CreatedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1,
                    CreatedByName = GetSafeCreatedByName()
                };

                // Create journal voucher details for receipt
                CreateReceiptJournalDetails(voucher, customerReceipt);

                return CreateJournalVoucher(voucher);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.CreateCustomerReceiptJournalVoucher", ex);
                throw;
            }
        }

        #endregion

        #region Additional Transaction Integration

        /// <summary>
        /// Creates a journal voucher for a purchase return
        /// </summary>
        public int CreatePurchaseReturnJournalVoucher(PurchaseReturn purchaseReturn)
        {
            try
            {
                if (purchaseReturn == null)
                    throw new ArgumentNullException(nameof(purchaseReturn));

                var voucher = new JournalVoucher
                {
                    VoucherDate = purchaseReturn.ReturnDate,
                    Reference = $"PR-{purchaseReturn.ReturnNumber}",
                    Narration = $"Purchase Return {purchaseReturn.ReturnNumber} - {purchaseReturn.SupplierName}",
                    CreatedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1,
                    CreatedByName = UserSession.IsLoggedIn ? UserSession.GetDisplayName() : "System User"
                };

                // Create journal voucher details for purchase return
                CreatePurchaseReturnJournalDetails(voucher, purchaseReturn);

                return CreateJournalVoucher(voucher);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.CreatePurchaseReturnJournalVoucher", ex);
                throw;
            }
        }

        /// <summary>
        /// Creates a journal voucher for a sales return
        /// </summary>
        public int CreateSalesReturnJournalVoucher(SalesReturn salesReturn)
        {
            try
            {
                if (salesReturn == null)
                    throw new ArgumentNullException(nameof(salesReturn));

                var voucher = new JournalVoucher
                {
                    VoucherDate = salesReturn.ReturnDate,
                    Reference = $"SR-{salesReturn.ReturnNumber}",
                    Narration = $"Sales Return {salesReturn.ReturnNumber} - {salesReturn.CustomerId}",
                    CreatedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1,
                    CreatedByName = UserSession.IsLoggedIn ? UserSession.GetDisplayName() : "System User"
                };

                // Create journal voucher details for sales return
                CreateSalesReturnJournalDetails(voucher, salesReturn);

                return CreateJournalVoucher(voucher);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.CreateSalesReturnJournalVoucher", ex);
                throw;
            }
        }

        /// <summary>
        /// Creates a journal voucher for an expense
        /// </summary>
        public int CreateExpenseJournalVoucher(Expense expense)
        {
            try
            {
                if (expense == null)
                    throw new ArgumentNullException(nameof(expense));

                var voucher = new JournalVoucher
                {
                    VoucherDate = expense.ExpenseDate,
                    Reference = $"EXP-{expense.ExpenseCode}",
                    Narration = $"Expense {expense.ExpenseCode} - {expense.Description}",
                    CreatedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1,
                    CreatedByName = GetSafeCreatedByName()
                };

                // Create journal voucher details for expense
                CreateExpenseJournalDetails(voucher, expense);

                return CreateJournalVoucher(voucher);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.CreateExpenseJournalVoucher", ex);
                throw;
            }
        }

        /// <summary>
        /// Creates a journal voucher for a stock movement
        /// </summary>
        public int CreateStockMovementJournalVoucher(StockMovement stockMovement)
        {
            try
            {
                if (stockMovement == null)
                    throw new ArgumentNullException(nameof(stockMovement));

                var voucher = new JournalVoucher
                {
                    VoucherDate = stockMovement.MovementDate,
                    Reference = $"SM-{stockMovement.MovementId}",
                    Narration = $"Stock Movement {stockMovement.MovementType} - {stockMovement.ProductName}",
                    CreatedBy = UserSession.IsLoggedIn ? UserSession.CurrentUserId : 1,
                    CreatedByName = GetSafeCreatedByName()
                };

                // Create journal voucher details for stock movement
                CreateStockMovementJournalDetails(voucher, stockMovement);

                return CreateJournalVoucher(voucher);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.CreateStockMovementJournalVoucher", ex);
                throw;
            }
        }

        #endregion

        #region Account Integration

        public ChartOfAccount GetDefaultSalesAccount()
        {
            try
            {
                // Try to get Product Sales account
                var account = _chartOfAccountService.GetChartOfAccountByCode("4100");
                if (account != null) return account;

                // Fallback: Try to get any sales account
                var salesAccounts = _chartOfAccountService.GetChartOfAccountsByType("REVENUE");
                var salesAccount = salesAccounts?.FirstOrDefault(a => 
                    a.AccountName?.ToUpper().Contains("SALES") == true ||
                    a.AccountName?.ToUpper().Contains("REVENUE") == true);
                
                if (salesAccount != null) return salesAccount;

                // Last resort: Get first revenue account
                return salesAccounts?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetDefaultSalesAccount", ex);
                return null;
            }
        }

        public ChartOfAccount GetDefaultCashAccount()
        {
            try
            {
                return _chartOfAccountService.GetChartOfAccountByCode("1110"); // Cash in Hand
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetDefaultCashAccount", ex);
                return null;
            }
        }

        public ChartOfAccount GetDefaultReceivableAccount()
        {
            try
            {
                // Try to get Trade Receivables account
                System.Diagnostics.Debug.WriteLine("Looking for Trade Receivables account with code 1210...");
                var account = _chartOfAccountService.GetChartOfAccountByCode("1210");
                if (account != null) 
                {
                    System.Diagnostics.Debug.WriteLine($"Found Trade Receivables account: {account.AccountName}");
                    return account;
                }
                System.Diagnostics.Debug.WriteLine("Trade Receivables account with code 1210 not found, trying fallback...");

                // Fallback: Try to get any receivable account
                var receivableAccounts = _chartOfAccountService.GetChartOfAccountsByType("ASSET");
                var receivableAccount = receivableAccounts?.FirstOrDefault(a => 
                    a.AccountName?.ToUpper().Contains("RECEIVABLE") == true ||
                    a.AccountName?.ToUpper().Contains("DEBTOR") == true);
                
                if (receivableAccount != null) return receivableAccount;

                // Last resort: Get first asset account
                return receivableAccounts?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetDefaultReceivableAccount", ex);
                return null;
            }
        }

        public ChartOfAccount GetDefaultTaxAccount()
        {
            try
            {
                // Try to get Tax Payable account
                var account = _chartOfAccountService.GetChartOfAccountByCode("2120");
                if (account != null) return account;

                // Fallback: Try to get any tax account
                var taxAccounts = _chartOfAccountService.GetChartOfAccountsByType("LIABILITY");
                var taxAccount = taxAccounts?.FirstOrDefault(a => 
                    a.AccountName?.ToUpper().Contains("TAX") == true ||
                    a.AccountName?.ToUpper().Contains("PAYABLE") == true);
                
                if (taxAccount != null) return taxAccount;

                // Last resort: Get first liability account
                return taxAccounts?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetDefaultTaxAccount", ex);
                return null;
            }
        }

        public ChartOfAccount GetAccountByPaymentMode(string paymentMode)
        {
            try
            {
                switch (paymentMode?.ToUpper())
                {
                    case "CASH":
                        return GetDefaultCashAccount();
                    case "CARD":
                    case "CREDIT_CARD":
                    case "DEBIT_CARD":
                        return _chartOfAccountService.GetChartOfAccountByCode("1120"); // Bank Account
                    case "BANK_TRANSFER":
                    case "CHEQUE":
                        return _chartOfAccountService.GetChartOfAccountByCode("1120"); // Bank Account
                    default:
                        return GetDefaultCashAccount();
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetAccountByPaymentMode", ex);
                return GetDefaultCashAccount();
            }
        }

        public ChartOfAccount GetDefaultPayableAccount()
        {
            try
            {
                return _chartOfAccountService.GetChartOfAccountByCode("2110"); // Accounts Payable
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetDefaultPayableAccount", ex);
                return null;
            }
        }

        public ChartOfAccount GetDefaultPurchaseReturnAccount()
        {
            try
            {
                return _chartOfAccountService.GetChartOfAccountByCode("5200"); // Purchase Returns
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetDefaultPurchaseReturnAccount", ex);
                return null;
            }
        }

        public ChartOfAccount GetDefaultSalesReturnAccount()
        {
            try
            {
                return _chartOfAccountService.GetChartOfAccountByCode("4200"); // Sales Returns
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetDefaultSalesReturnAccount", ex);
                return null;
            }
        }

        public ChartOfAccount GetDefaultExpenseAccount()
        {
            try
            {
                return _chartOfAccountService.GetChartOfAccountByCode("5100"); // General Expenses
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetDefaultExpenseAccount", ex);
                return null;
            }
        }

        public ChartOfAccount GetDefaultInventoryAccount()
        {
            try
            {
                return _chartOfAccountService.GetChartOfAccountByCode("1300"); // Inventory
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetDefaultInventoryAccount", ex);
                return null;
            }
        }

        /// <summary>
        /// Creates default chart of accounts if they don't exist
        /// </summary>
        public void EnsureDefaultAccountsExist()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Ensuring default accounts exist...");
                
                // Check if required accounts exist, create them if they don't
                System.Diagnostics.Debug.WriteLine("Checking if Trade Receivables account exists...");
                var receivableAccount = GetDefaultReceivableAccount();
                System.Diagnostics.Debug.WriteLine($"Receivable account result: {(receivableAccount != null ? $"Found: {receivableAccount.AccountName}" : "Not found")}");
                if (receivableAccount == null)
                {
                    System.Diagnostics.Debug.WriteLine("Creating Trade Receivables account...");
                    try
                    {
                        // Create Trade Receivables account
                        var newAccount = new ChartOfAccount
                        {
                            AccountCode = "1210",
                            AccountName = "Trade Receivables",
                            AccountType = "ASSET",
                            AccountCategory = "CURRENT_ASSET",
                            NormalBalance = "DEBIT",
                            AccountLevel = 1,
                            ParentAccountId = null,
                            IsActive = true,
                            CreatedBy = 1,
                            CreatedDate = DateTime.Now
                        };
                        var accountId = _chartOfAccountService.CreateChartOfAccount(newAccount);
                        System.Diagnostics.Debug.WriteLine($"Trade Receivables account created with ID: {accountId}");
                    }
                    catch (Exception createEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to create Trade Receivables account: {createEx.Message}");
                        // If creation fails due to "already exists", try to find it again
                        if (createEx.Message.Contains("already exists"))
                        {
                            System.Diagnostics.Debug.WriteLine("Account already exists, trying to find it again...");
                            receivableAccount = _chartOfAccountService.GetChartOfAccountByCode("1210");
                            if (receivableAccount != null)
                            {
                                System.Diagnostics.Debug.WriteLine($"Found existing Trade Receivables account: {receivableAccount.AccountName}");
                            }
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Trade Receivables account already exists: {receivableAccount.AccountName}");
                }

                var salesAccount = GetDefaultSalesAccount();
                if (salesAccount == null)
                {
                    System.Diagnostics.Debug.WriteLine("Creating Product Sales account...");
                    try
                    {
                        // Create Product Sales account
                        var newAccount = new ChartOfAccount
                        {
                            AccountCode = "4100",
                            AccountName = "Product Sales",
                            AccountType = "REVENUE",
                            AccountCategory = "SALES_REVENUE",
                            NormalBalance = "CREDIT",
                            AccountLevel = 1,
                            ParentAccountId = null,
                            IsActive = true,
                            CreatedBy = 1,
                            CreatedDate = DateTime.Now
                        };
                        var accountId = _chartOfAccountService.CreateChartOfAccount(newAccount);
                        System.Diagnostics.Debug.WriteLine($"Product Sales account created with ID: {accountId}");
                    }
                    catch (Exception createEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to create Product Sales account: {createEx.Message}");
                        if (createEx.Message.Contains("already exists"))
                        {
                            System.Diagnostics.Debug.WriteLine("Account already exists, trying to find it again...");
                            salesAccount = _chartOfAccountService.GetChartOfAccountByCode("4100");
                            if (salesAccount != null)
                            {
                                System.Diagnostics.Debug.WriteLine($"Found existing Product Sales account: {salesAccount.AccountName}");
                            }
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Product Sales account already exists: {salesAccount.AccountName}");
                }

                var taxAccount = GetDefaultTaxAccount();
                if (taxAccount == null)
                {
                    System.Diagnostics.Debug.WriteLine("Creating Tax Payable account...");
                    try
                    {
                        // Create Tax Payable account
                        var newAccount = new ChartOfAccount
                        {
                            AccountCode = "2120",
                            AccountName = "Tax Payable",
                            AccountType = "LIABILITY",
                            AccountCategory = "CURRENT_LIABILITY",
                            NormalBalance = "CREDIT",
                            AccountLevel = 1,
                            ParentAccountId = null,
                            IsActive = true,
                            CreatedBy = 1,
                            CreatedDate = DateTime.Now
                        };
                        var accountId = _chartOfAccountService.CreateChartOfAccount(newAccount);
                        System.Diagnostics.Debug.WriteLine($"Tax Payable account created with ID: {accountId}");
                    }
                    catch (Exception createEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to create Tax Payable account: {createEx.Message}");
                        if (createEx.Message.Contains("already exists"))
                        {
                            System.Diagnostics.Debug.WriteLine("Account already exists, trying to find it again...");
                            taxAccount = _chartOfAccountService.GetChartOfAccountByCode("2120");
                            if (taxAccount != null)
                            {
                                System.Diagnostics.Debug.WriteLine($"Found existing Tax Payable account: {taxAccount.AccountName}");
                            }
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Tax Payable account already exists: {taxAccount.AccountName}");
                }

                var cashAccount = GetDefaultCashAccount();
                if (cashAccount == null)
                {
                    System.Diagnostics.Debug.WriteLine("Creating Cash in Hand account...");
                    try
                    {
                        // Create Cash in Hand account
                        var newAccount = new ChartOfAccount
                        {
                            AccountCode = "1110",
                            AccountName = "Cash in Hand",
                            AccountType = "ASSET",
                            AccountCategory = "CURRENT_ASSET",
                            NormalBalance = "DEBIT",
                            AccountLevel = 1,
                            ParentAccountId = null,
                            IsActive = true,
                            CreatedBy = 1,
                            CreatedDate = DateTime.Now
                        };
                        var accountId = _chartOfAccountService.CreateChartOfAccount(newAccount);
                        System.Diagnostics.Debug.WriteLine($"Cash in Hand account created with ID: {accountId}");
                    }
                    catch (Exception createEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"Failed to create Cash in Hand account: {createEx.Message}");
                        if (createEx.Message.Contains("already exists"))
                        {
                            System.Diagnostics.Debug.WriteLine("Account already exists, trying to find it again...");
                            cashAccount = _chartOfAccountService.GetChartOfAccountByCode("1110");
                            if (cashAccount != null)
                            {
                                System.Diagnostics.Debug.WriteLine($"Found existing Cash in Hand account: {cashAccount.AccountName}");
                            }
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Cash in Hand account already exists: {cashAccount.AccountName}");
                }
                
                System.Diagnostics.Debug.WriteLine("Default accounts check completed successfully.");
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.EnsureDefaultAccountsExist", ex);
                throw;
            }
        }

        #endregion

        #region Reports

        public List<JournalVoucherDetail> GetAccountJournalVoucherDetails(int accountId, DateTime startDate, DateTime endDate)
        {
            try
            {
                // Get account details from database using repository
                return _journalVoucherRepository.GetAccountJournalVoucherDetails(accountId, startDate, endDate);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetAccountJournalVoucherDetails", ex);
                throw;
            }
        }

        public decimal GetAccountBalance(int accountId, DateTime asOfDate)
        {
            try
            {
                // Get account balance from database using repository
                return _journalVoucherRepository.GetAccountBalance(accountId, asOfDate);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetAccountBalance", ex);
                throw;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets a safe CreatedByName value, ensuring it's never null or empty
        /// </summary>
        private string GetSafeCreatedByName()
        {
            try
            {
                if (UserSession.IsLoggedIn)
                {
                    var displayName = UserSession.GetDisplayName();
                    if (!string.IsNullOrWhiteSpace(displayName))
                        return displayName;
                }
                return "System User";
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherService.GetSafeCreatedByName", ex);
                return "System User";
            }
        }

        /// <summary>
        /// Calculates total debit and credit amounts for the voucher
        /// </summary>
        private void CalculateVoucherTotals(JournalVoucher voucher)
        {
            System.Diagnostics.Debug.WriteLine($"CalculateVoucherTotals: Details count: {voucher.Details?.Count ?? 0}");
            
            if (voucher.Details != null && voucher.Details.Any())
            {
                voucher.TotalDebit = voucher.Details.Sum(d => d.DebitAmount);
                voucher.TotalCredit = voucher.Details.Sum(d => d.CreditAmount);
                
                System.Diagnostics.Debug.WriteLine($"Calculated totals - Debit: PKR {voucher.TotalDebit:N2}, Credit: PKR {voucher.TotalCredit:N2}");
                System.Diagnostics.Debug.WriteLine($"Balance difference: PKR {voucher.TotalDebit - voucher.TotalCredit:N2}");
                
                // Log each detail
                foreach (var detail in voucher.Details)
                {
                    System.Diagnostics.Debug.WriteLine($"  Detail - AccountId: {detail.AccountId}, Debit: PKR {detail.DebitAmount:N2}, Credit: PKR {detail.CreditAmount:N2}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No details found for voucher!");
                voucher.TotalDebit = 0;
                voucher.TotalCredit = 0;
            }
        }

        /// <summary>
        /// Creates journal voucher details for cash sales
        /// </summary>
        private void CreateCashSalesJournalDetails(JournalVoucher voucher, SalesInvoice salesInvoice)
        {
            var details = new List<JournalVoucherDetail>();

            // Get accounts
            var cashAccount = GetAccountByPaymentMode(salesInvoice.PaymentType) ?? GetDefaultCashAccount();
            var salesAccount = _chartOfAccountService.GetChartOfAccountById(salesInvoice.SalesAccountId ?? 0) ?? GetDefaultSalesAccount();
            var taxAccount = _chartOfAccountService.GetChartOfAccountById(salesInvoice.TaxAccountId ?? 0) ?? GetDefaultTaxAccount();

            // Debit: Cash Account (Total Amount)
            details.Add(new JournalVoucherDetail
            {
                AccountId = cashAccount.AccountId,
                DebitAmount = salesInvoice.TotalAmount,
                CreditAmount = 0,
                Narration = $"Cash received for sales invoice {salesInvoice.InvoiceNumber}"
            });

            // Credit: Sales Account (Net Amount)
            details.Add(new JournalVoucherDetail
            {
                AccountId = salesAccount.AccountId,
                DebitAmount = 0,
                CreditAmount = salesInvoice.NetAmount,
                Narration = $"Sales revenue for invoice {salesInvoice.InvoiceNumber}"
            });

            // Credit: Tax Account (Tax Amount) - if tax exists
            if (salesInvoice.TaxAmount > 0)
            {
                details.Add(new JournalVoucherDetail
                {
                    AccountId = taxAccount.AccountId,
                    DebitAmount = 0,
                    CreditAmount = salesInvoice.TaxAmount,
                    Narration = $"Tax collected for invoice {salesInvoice.InvoiceNumber}"
                });
            }

            voucher.Details = details;
        }

        /// <summary>
        /// Creates journal voucher details for credit sales
        /// </summary>
        private void CreateCreditSalesJournalDetails(JournalVoucher voucher, SalesInvoice salesInvoice)
        {
            System.Diagnostics.Debug.WriteLine($"CreateCreditSalesJournalDetails: Creating journal details for invoice {salesInvoice.InvoiceNumber}");
            System.Diagnostics.Debug.WriteLine($"Sales Invoice TotalAmount: PKR {salesInvoice.TotalAmount:N2}, NetAmount: PKR {salesInvoice.NetAmount:N2}, TaxAmount: PKR {salesInvoice.TaxAmount:N2}");
            
            var details = new List<JournalVoucherDetail>();

            // Get accounts with null checks
            System.Diagnostics.Debug.WriteLine("Getting accounts...");
            var receivableAccount = _chartOfAccountService.GetChartOfAccountById(salesInvoice.ReceivableAccountId ?? 0) ?? GetDefaultReceivableAccount();
            var salesAccount = _chartOfAccountService.GetChartOfAccountById(salesInvoice.SalesAccountId ?? 0) ?? GetDefaultSalesAccount();
            var taxAccount = _chartOfAccountService.GetChartOfAccountById(salesInvoice.TaxAccountId ?? 0) ?? GetDefaultTaxAccount();
            
            System.Diagnostics.Debug.WriteLine($"Receivable Account: {(receivableAccount != null ? receivableAccount.AccountName : "NULL")}");
            System.Diagnostics.Debug.WriteLine($"Sales Account: {(salesAccount != null ? salesAccount.AccountName : "NULL")}");
            System.Diagnostics.Debug.WriteLine($"Tax Account: {(taxAccount != null ? taxAccount.AccountName : "NULL")}");

            // Validate accounts exist
            if (receivableAccount == null)
            {
                throw new InvalidOperationException("Receivable account not found. Please ensure Chart of Accounts is properly configured.");
            }
            if (salesAccount == null)
            {
                throw new InvalidOperationException("Sales account not found. Please ensure Chart of Accounts is properly configured.");
            }
            if (taxAccount == null)
            {
                throw new InvalidOperationException("Tax account not found. Please ensure Chart of Accounts is properly configured.");
            }

            // Debit: Accounts Receivable (Total Amount)
            details.Add(new JournalVoucherDetail
            {
                AccountId = receivableAccount.AccountId,
                DebitAmount = salesInvoice.TotalAmount,
                CreditAmount = 0,
                Narration = $"Amount receivable for sales invoice {salesInvoice.InvoiceNumber}"
            });

            // Credit: Sales Account (Net Amount)
            details.Add(new JournalVoucherDetail
            {
                AccountId = salesAccount.AccountId,
                DebitAmount = 0,
                CreditAmount = salesInvoice.NetAmount,
                Narration = $"Sales revenue for invoice {salesInvoice.InvoiceNumber}"
            });

            // Credit: Tax Account (Tax Amount) - if tax exists
            if (salesInvoice.TaxAmount > 0)
            {
                details.Add(new JournalVoucherDetail
                {
                    AccountId = taxAccount.AccountId,
                    DebitAmount = 0,
                    CreditAmount = salesInvoice.TaxAmount,
                    Narration = $"Tax collected for invoice {salesInvoice.InvoiceNumber}"
                });
            }

            voucher.Details = details;
            
            System.Diagnostics.Debug.WriteLine($"Created {details.Count} journal voucher details:");
            foreach (var detail in details)
            {
                System.Diagnostics.Debug.WriteLine($"  AccountId: {detail.AccountId}, Debit: PKR {detail.DebitAmount:N2}, Credit: PKR {detail.CreditAmount:N2}, Narration: {detail.Narration}");
            }
        }

        /// <summary>
        /// Creates journal voucher details for sales payment
        /// </summary>
        private void CreatePaymentJournalDetails(JournalVoucher voucher, SalesPayment salesPayment)
        {
            var details = new List<JournalVoucherDetail>();

            // Get accounts
            var paymentAccount = GetAccountByPaymentMode(salesPayment.PaymentMode) ?? GetDefaultCashAccount();
            var receivableAccount = GetDefaultReceivableAccount();

            // Debit: Payment Account (Cash/Bank)
            details.Add(new JournalVoucherDetail
            {
                AccountId = paymentAccount.AccountId,
                DebitAmount = salesPayment.Amount,
                CreditAmount = 0,
                Narration = $"Payment received from {salesPayment.CustomerName}"
            });

            // Credit: Accounts Receivable
            details.Add(new JournalVoucherDetail
            {
                AccountId = receivableAccount.AccountId,
                DebitAmount = 0,
                CreditAmount = salesPayment.Amount,
                Narration = $"Receivable cleared for payment from {salesPayment.CustomerName}"
            });

            voucher.Details = details;
        }

        /// <summary>
        /// Creates journal voucher details for customer receipt
        /// </summary>
        private void CreateReceiptJournalDetails(JournalVoucher voucher, CustomerReceipt customerReceipt)
        {
            var details = new List<JournalVoucherDetail>();

            // Get accounts with null checks
            var receiptAccount = GetAccountByPaymentMode(customerReceipt.PaymentMode) ?? GetDefaultCashAccount();
            var receivableAccount = GetDefaultReceivableAccount();

            // Validate accounts exist
            if (receiptAccount == null)
            {
                throw new InvalidOperationException("Receipt account not found. Please ensure Chart of Accounts is properly configured.");
            }
            if (receivableAccount == null)
            {
                throw new InvalidOperationException("Receivable account not found. Please ensure Chart of Accounts is properly configured.");
            }

            // Debit: Receipt Account (Cash/Bank)
            details.Add(new JournalVoucherDetail
            {
                AccountId = receiptAccount.AccountId,
                DebitAmount = customerReceipt.Amount,
                CreditAmount = 0,
                Narration = $"Receipt from {customerReceipt.CustomerName}"
            });

            // Credit: Accounts Receivable
            details.Add(new JournalVoucherDetail
            {
                AccountId = receivableAccount.AccountId,
                DebitAmount = 0,
                CreditAmount = customerReceipt.Amount,
                Narration = $"Receivable cleared for receipt from {customerReceipt.CustomerName}"
            });

            voucher.Details = details;
        }

        /// <summary>
        /// Creates journal voucher details for purchase return
        /// </summary>
        private void CreatePurchaseReturnJournalDetails(JournalVoucher voucher, PurchaseReturn purchaseReturn)
        {
            var details = new List<JournalVoucherDetail>();

            // Get accounts
            var payableAccount = GetDefaultPayableAccount();
            var purchaseReturnAccount = GetDefaultPurchaseReturnAccount();

            // Debit: Accounts Payable (reduce liability)
            details.Add(new JournalVoucherDetail
            {
                AccountId = payableAccount.AccountId,
                DebitAmount = purchaseReturn.NetReturnAmount,
                CreditAmount = 0,
                Narration = $"Payable reduced for purchase return {purchaseReturn.ReturnNumber}"
            });

            // Credit: Purchase Return Account (expense reduction)
            details.Add(new JournalVoucherDetail
            {
                AccountId = purchaseReturnAccount.AccountId,
                DebitAmount = 0,
                CreditAmount = purchaseReturn.NetReturnAmount,
                Narration = $"Purchase return for {purchaseReturn.SupplierName}"
            });

            voucher.Details = details;
        }

        /// <summary>
        /// Creates journal voucher details for sales return
        /// </summary>
        private void CreateSalesReturnJournalDetails(JournalVoucher voucher, SalesReturn salesReturn)
        {
            var details = new List<JournalVoucherDetail>();

            // Get accounts
            var salesReturnAccount = GetDefaultSalesReturnAccount();
            var receivableAccount = GetDefaultReceivableAccount();

            // Debit: Sales Return Account (revenue reduction)
            details.Add(new JournalVoucherDetail
            {
                AccountId = salesReturnAccount.AccountId,
                DebitAmount = salesReturn.TotalAmount,
                CreditAmount = 0,
                Narration = $"Sales return for customer {salesReturn.CustomerId}"
            });

            // Credit: Accounts Receivable (reduce asset)
            details.Add(new JournalVoucherDetail
            {
                AccountId = receivableAccount.AccountId,
                DebitAmount = 0,
                CreditAmount = salesReturn.TotalAmount,
                Narration = $"Receivable reduced for sales return {salesReturn.ReturnNumber}"
            });

            voucher.Details = details;
        }

        /// <summary>
        /// Creates journal voucher details for expense
        /// </summary>
        private void CreateExpenseJournalDetails(JournalVoucher voucher, Expense expense)
        {
            var details = new List<JournalVoucherDetail>();

            // Get accounts
            var expenseAccount = GetDefaultExpenseAccount();
            var paymentAccount = GetAccountByPaymentMode(expense.PaymentMethod) ?? GetDefaultCashAccount();

            // Debit: Expense Account
            details.Add(new JournalVoucherDetail
            {
                AccountId = expenseAccount.AccountId,
                DebitAmount = expense.Amount,
                CreditAmount = 0,
                Narration = $"Expense: {expense.Description}"
            });

            // Credit: Payment Account (Cash/Bank)
            details.Add(new JournalVoucherDetail
            {
                AccountId = paymentAccount.AccountId,
                DebitAmount = 0,
                CreditAmount = expense.Amount,
                Narration = $"Payment for expense {expense.ExpenseCode}"
            });

            voucher.Details = details;
        }

        /// <summary>
        /// Creates journal voucher details for stock movement
        /// </summary>
        private void CreateStockMovementJournalDetails(JournalVoucher voucher, StockMovement stockMovement)
        {
            var details = new List<JournalVoucherDetail>();

            // Get accounts
            var inventoryAccount = GetDefaultInventoryAccount();

            if (stockMovement.MovementType?.ToUpper() == "IN")
            {
                // Stock In: Debit Inventory
                details.Add(new JournalVoucherDetail
                {
                    AccountId = inventoryAccount.AccountId,
                    DebitAmount = stockMovement.TotalValue,
                    CreditAmount = 0,
                    Narration = $"Stock in: {stockMovement.ProductName}"
                });
            }
            else if (stockMovement.MovementType?.ToUpper() == "OUT")
            {
                // Stock Out: Credit Inventory
                details.Add(new JournalVoucherDetail
                {
                    AccountId = inventoryAccount.AccountId,
                    DebitAmount = 0,
                    CreditAmount = stockMovement.TotalValue,
                    Narration = $"Stock out: {stockMovement.ProductName}"
                });
            }

            voucher.Details = details;
        }

        #endregion
    }
}
