using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    /// <summary>
    /// Service class for Receipt Voucher business logic operations
    /// </summary>
    public class ReceiptVoucherService : IReceiptVoucherService
    {
        private readonly IReceiptVoucherRepository _receiptVoucherRepository;
        private readonly IJournalVoucherService _journalVoucherService;
        private readonly IChartOfAccountService _chartOfAccountService;

        public ReceiptVoucherService()
        {
            _receiptVoucherRepository = new ReceiptVoucherRepository();
            _journalVoucherService = new JournalVoucherService();
            _chartOfAccountService = new ChartOfAccountService();
        }

        public ReceiptVoucherService(IReceiptVoucherRepository receiptVoucherRepository, 
                                    IJournalVoucherService journalVoucherService,
                                    IChartOfAccountService chartOfAccountService)
        {
            _receiptVoucherRepository = receiptVoucherRepository;
            _journalVoucherService = journalVoucherService;
            _chartOfAccountService = chartOfAccountService;
        }

        #region Basic CRUD Operations

        public int CreateReceiptVoucher(ReceiptVoucher voucher)
        {
            try
            {
                // Validate voucher
                if (!ValidateReceiptVoucher(voucher))
                {
                    var errors = GetValidationErrors(voucher);
                    throw new InvalidOperationException($"Receipt voucher validation failed: {string.Join(", ", errors)}");
                }

                // Set audit fields
                voucher.CreatedBy = UserSession.CurrentUser?.UserId ?? 1;
                voucher.CreatedDate = DateTime.Now;
                voucher.CreatedByName = UserSession.CurrentUser?.FirstName + " " + UserSession.CurrentUser?.LastName;

                // Create receipt voucher
                var voucherId = _receiptVoucherRepository.CreateReceiptVoucher(voucher);

                // Process journal entries
                ProcessReceiptVoucher(voucher);

                return voucherId;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.CreateReceiptVoucher", ex);
                throw;
            }
        }

        public bool UpdateReceiptVoucher(ReceiptVoucher voucher)
        {
            try
            {
                // Validate voucher
                if (!ValidateReceiptVoucher(voucher))
                {
                    var errors = GetValidationErrors(voucher);
                    throw new InvalidOperationException($"Receipt voucher validation failed: {string.Join(", ", errors)}");
                }

                // Set audit fields
                voucher.ModifiedBy = UserSession.CurrentUser?.UserId ?? 1;
                voucher.ModifiedDate = DateTime.Now;
                voucher.ModifiedByName = UserSession.CurrentUser?.FirstName + " " + UserSession.CurrentUser?.LastName;

                return _receiptVoucherRepository.UpdateReceiptVoucher(voucher);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.UpdateReceiptVoucher", ex);
                throw;
            }
        }

        public bool DeleteReceiptVoucher(int voucherId)
        {
            try
            {
                return _receiptVoucherRepository.DeleteReceiptVoucher(voucherId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.DeleteReceiptVoucher", ex);
                throw;
            }
        }

        public ReceiptVoucher GetReceiptVoucherById(int voucherId)
        {
            try
            {
                return _receiptVoucherRepository.GetReceiptVoucherById(voucherId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetReceiptVoucherById", ex);
                throw;
            }
        }

        public ReceiptVoucher GetReceiptVoucherByNumber(string voucherNumber)
        {
            try
            {
                return _receiptVoucherRepository.GetReceiptVoucherByNumber(voucherNumber);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetReceiptVoucherByNumber", ex);
                throw;
            }
        }

        public List<ReceiptVoucher> GetAllReceiptVouchers()
        {
            try
            {
                return _receiptVoucherRepository.GetAllReceiptVouchers();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetAllReceiptVouchers", ex);
                throw;
            }
        }

        public List<ReceiptVoucher> GetReceiptVouchersByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _receiptVoucherRepository.GetReceiptVouchersByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetReceiptVouchersByDateRange", ex);
                throw;
            }
        }

        public List<ReceiptVoucher> GetReceiptVouchersByAccount(int accountId)
        {
            try
            {
                return _receiptVoucherRepository.GetReceiptVouchersByAccount(accountId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetReceiptVouchersByAccount", ex);
                throw;
            }
        }

        public List<ReceiptVoucher> GetReceiptVouchersByReceiptMode(string receiptMode)
        {
            try
            {
                return _receiptVoucherRepository.GetReceiptVouchersByReceiptMode(receiptMode);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetReceiptVouchersByReceiptMode", ex);
                throw;
            }
        }

        public List<ReceiptVoucher> GetReceiptVouchersByStatus(string status)
        {
            try
            {
                return _receiptVoucherRepository.GetReceiptVouchersByStatus(status);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetReceiptVouchersByStatus", ex);
                throw;
            }
        }

        #endregion

        #region Business Logic Operations

        public string GenerateReceiptVoucherNumber()
        {
            try
            {
                return _receiptVoucherRepository.GenerateReceiptVoucherNumber();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GenerateReceiptVoucherNumber", ex);
                // Return a fallback number
                var currentDate = DateTime.Now;
                var yearMonth = currentDate.ToString("yyyyMM");
                return $"RV{yearMonth}0001";
            }
        }

        public bool ValidateReceiptVoucher(ReceiptVoucher voucher)
        {
            try
            {
                return voucher.IsValid();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.ValidateReceiptVoucher", ex);
                return false;
            }
        }

        public List<string> GetValidationErrors(ReceiptVoucher voucher)
        {
            try
            {
                return voucher.GetValidationErrors();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetValidationErrors", ex);
                return new List<string> { "Validation error occurred" };
            }
        }

        public bool ProcessReceiptVoucher(ReceiptVoucher voucher)
        {
            try
            {
                // Create journal voucher for the receipt
                var journalVoucher = new JournalVoucher
                {
                    VoucherNumber = $"JV-{voucher.VoucherNumber}",
                    VoucherDate = voucher.VoucherDate,
                    Reference = voucher.VoucherNumber,
                    Narration = $"Receipt Voucher: {voucher.Narration}",
                    BankAccountId = voucher.BankAccountId,
                    CreatedBy = voucher.CreatedBy,
                    CreatedDate = voucher.CreatedDate
                };

                // Add debit entry (cash/bank account)
                var debitAccount = GetDebitAccountForReceipt(voucher.ReceiptMode);
                journalVoucher.Details.Add(new JournalVoucherDetail
                {
                    AccountId = debitAccount.AccountId,
                    DebitAmount = voucher.Amount,
                    CreditAmount = 0,
                    Narration = $"Receipt received: {voucher.Narration}"
                });

                // Add credit entry (receipt account)
                journalVoucher.Details.Add(new JournalVoucherDetail
                {
                    AccountId = voucher.AccountId,
                    DebitAmount = 0,
                    CreditAmount = voucher.Amount,
                    Narration = $"Receipt: {voucher.Narration}"
                });

                // Calculate totals
                journalVoucher.TotalDebit = journalVoucher.Details.Sum(d => d.DebitAmount);
                journalVoucher.TotalCredit = journalVoucher.Details.Sum(d => d.CreditAmount);

                // Create journal voucher
                _journalVoucherService.CreateJournalVoucher(journalVoucher);

                return true;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.ProcessReceiptVoucher", ex);
                throw;
            }
        }

        #endregion

        #region Reports & Analytics

        public decimal GetTotalReceiptsByAccount(int accountId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return _receiptVoucherRepository.GetTotalReceiptsByAccount(accountId, startDate, endDate);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetTotalReceiptsByAccount", ex);
                return 0;
            }
        }

        public decimal GetTotalReceiptsByReceiptMode(string receiptMode, DateTime startDate, DateTime endDate)
        {
            try
            {
                return _receiptVoucherRepository.GetTotalReceiptsByReceiptMode(receiptMode, startDate, endDate);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetTotalReceiptsByReceiptMode", ex);
                return 0;
            }
        }

        public Dictionary<string, decimal> GetReceiptSummary(DateTime startDate, DateTime endDate)
        {
            try
            {
                var summary = new Dictionary<string, decimal>();
                var receiptModes = new[] { "CASH", "CARD", "CHEQUE", "BANK_TRANSFER", "EASYPAISA", "JAZZCASH" };

                foreach (var mode in receiptModes)
                {
                    var total = GetTotalReceiptsByReceiptMode(mode, startDate, endDate);
                    if (total > 0)
                    {
                        summary[mode] = total;
                    }
                }

                return summary;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetReceiptSummary", ex);
                return new Dictionary<string, decimal>();
            }
        }

        #endregion

        #region Helper Methods

        private ChartOfAccount GetDebitAccountForReceipt(string receiptMode)
        {
            try
            {
                // Get appropriate account based on receipt mode
                switch (receiptMode?.ToUpper())
                {
                    case "CASH":
                        return _chartOfAccountService.GetDefaultCashAccount();
                    case "CARD":
                    case "BANK_TRANSFER":
                        return _chartOfAccountService.GetDefaultBankAccount();
                    case "CHEQUE":
                        return _chartOfAccountService.GetDefaultBankAccount();
                    case "EASYPAISA":
                    case "JAZZCASH":
                        return _chartOfAccountService.GetDefaultMobileAccount();
                    default:
                        return _chartOfAccountService.GetDefaultCashAccount();
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherService.GetDebitAccountForReceipt", ex);
                return _chartOfAccountService.GetDefaultCashAccount();
            }
        }

        #endregion
    }
}

