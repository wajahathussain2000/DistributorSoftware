using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    /// <summary>
    /// Service class for Payment Voucher business logic operations
    /// </summary>
    public class PaymentVoucherService : IPaymentVoucherService
    {
        private readonly IPaymentVoucherRepository _paymentVoucherRepository;
        private readonly IJournalVoucherService _journalVoucherService;
        private readonly IChartOfAccountService _chartOfAccountService;

        public PaymentVoucherService()
        {
            _paymentVoucherRepository = new PaymentVoucherRepository();
            _journalVoucherService = new JournalVoucherService();
            _chartOfAccountService = new ChartOfAccountService();
        }

        public PaymentVoucherService(IPaymentVoucherRepository paymentVoucherRepository, 
                                   IJournalVoucherService journalVoucherService,
                                   IChartOfAccountService chartOfAccountService)
        {
            _paymentVoucherRepository = paymentVoucherRepository;
            _journalVoucherService = journalVoucherService;
            _chartOfAccountService = chartOfAccountService;
        }

        #region Basic CRUD Operations

        public int CreatePaymentVoucher(PaymentVoucher voucher)
        {
            try
            {
                // Validate voucher
                if (!ValidatePaymentVoucher(voucher))
                {
                    var errors = GetValidationErrors(voucher);
                    throw new InvalidOperationException($"Payment voucher validation failed: {string.Join(", ", errors)}");
                }

                // Set audit fields
                voucher.CreatedBy = UserSession.CurrentUser?.UserId ?? 1;
                voucher.CreatedDate = DateTime.Now;
                voucher.CreatedByName = UserSession.CurrentUser?.FirstName + " " + UserSession.CurrentUser?.LastName;

                // Create payment voucher
                var voucherId = _paymentVoucherRepository.CreatePaymentVoucher(voucher);

                // Process journal entries
                ProcessPaymentVoucher(voucher);

                return voucherId;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.CreatePaymentVoucher", ex);
                throw;
            }
        }

        public bool UpdatePaymentVoucher(PaymentVoucher voucher)
        {
            try
            {
                // Validate voucher
                if (!ValidatePaymentVoucher(voucher))
                {
                    var errors = GetValidationErrors(voucher);
                    throw new InvalidOperationException($"Payment voucher validation failed: {string.Join(", ", errors)}");
                }

                // Set audit fields
                voucher.ModifiedBy = UserSession.CurrentUser?.UserId ?? 1;
                voucher.ModifiedDate = DateTime.Now;
                voucher.ModifiedByName = UserSession.CurrentUser?.FirstName + " " + UserSession.CurrentUser?.LastName;

                return _paymentVoucherRepository.UpdatePaymentVoucher(voucher);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.UpdatePaymentVoucher", ex);
                throw;
            }
        }

        public bool DeletePaymentVoucher(int voucherId)
        {
            try
            {
                return _paymentVoucherRepository.DeletePaymentVoucher(voucherId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.DeletePaymentVoucher", ex);
                throw;
            }
        }

        public PaymentVoucher GetPaymentVoucherById(int voucherId)
        {
            try
            {
                return _paymentVoucherRepository.GetPaymentVoucherById(voucherId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GetPaymentVoucherById", ex);
                throw;
            }
        }

        public PaymentVoucher GetPaymentVoucherByNumber(string voucherNumber)
        {
            try
            {
                return _paymentVoucherRepository.GetPaymentVoucherByNumber(voucherNumber);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GetPaymentVoucherByNumber", ex);
                throw;
            }
        }

        public List<PaymentVoucher> GetAllPaymentVouchers()
        {
            try
            {
                return _paymentVoucherRepository.GetAllPaymentVouchers();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GetAllPaymentVouchers", ex);
                throw;
            }
        }

        public List<PaymentVoucher> GetPaymentVouchersByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _paymentVoucherRepository.GetPaymentVouchersByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GetPaymentVouchersByDateRange", ex);
                throw;
            }
        }

        public List<PaymentVoucher> GetPaymentVouchersByAccount(int accountId)
        {
            try
            {
                return _paymentVoucherRepository.GetPaymentVouchersByAccount(accountId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GetPaymentVouchersByAccount", ex);
                throw;
            }
        }

        public List<PaymentVoucher> GetPaymentVouchersByPaymentMode(string paymentMode)
        {
            try
            {
                return _paymentVoucherRepository.GetPaymentVouchersByPaymentMode(paymentMode);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GetPaymentVouchersByPaymentMode", ex);
                throw;
            }
        }

        public List<PaymentVoucher> GetPaymentVouchersByStatus(string status)
        {
            try
            {
                return _paymentVoucherRepository.GetPaymentVouchersByStatus(status);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GetPaymentVouchersByStatus", ex);
                throw;
            }
        }

        #endregion

        #region Business Logic Operations

        public string GeneratePaymentVoucherNumber()
        {
            try
            {
                return _paymentVoucherRepository.GeneratePaymentVoucherNumber();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GeneratePaymentVoucherNumber", ex);
                // Return a fallback number
                var currentDate = DateTime.Now;
                var yearMonth = currentDate.ToString("yyyyMM");
                return $"PV{yearMonth}0001";
            }
        }

        public bool ValidatePaymentVoucher(PaymentVoucher voucher)
        {
            try
            {
                return voucher.IsValid();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.ValidatePaymentVoucher", ex);
                return false;
            }
        }

        public List<string> GetValidationErrors(PaymentVoucher voucher)
        {
            try
            {
                return voucher.GetValidationErrors();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GetValidationErrors", ex);
                return new List<string> { "Validation error occurred" };
            }
        }

        public bool ProcessPaymentVoucher(PaymentVoucher voucher)
        {
            try
            {
                // Create journal voucher for the payment
                var journalVoucher = new JournalVoucher
                {
                    VoucherNumber = $"JV-{voucher.VoucherNumber}",
                    VoucherDate = voucher.VoucherDate,
                    Reference = voucher.VoucherNumber,
                    Narration = $"Payment Voucher: {voucher.Narration}",
                    BankAccountId = voucher.BankAccountId,
                    CreatedBy = voucher.CreatedBy,
                    CreatedDate = voucher.CreatedDate
                };

                // Add debit entry (payment account)
                journalVoucher.Details.Add(new JournalVoucherDetail
                {
                    AccountId = voucher.AccountId,
                    DebitAmount = voucher.Amount,
                    CreditAmount = 0,
                    Narration = $"Payment: {voucher.Narration}"
                });

                // Add credit entry (cash/bank account)
                var creditAccount = GetCreditAccountForPayment(voucher.PaymentMode);
                journalVoucher.Details.Add(new JournalVoucherDetail
                {
                    AccountId = creditAccount.AccountId,
                    DebitAmount = 0,
                    CreditAmount = voucher.Amount,
                    Narration = $"Payment received: {voucher.Narration}"
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
                DebugHelper.WriteException("PaymentVoucherService.ProcessPaymentVoucher", ex);
                throw;
            }
        }

        #endregion

        #region Reports & Analytics

        public decimal GetTotalPaymentsByAccount(int accountId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return _paymentVoucherRepository.GetTotalPaymentsByAccount(accountId, startDate, endDate);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GetTotalPaymentsByAccount", ex);
                return 0;
            }
        }

        public decimal GetTotalPaymentsByPaymentMode(string paymentMode, DateTime startDate, DateTime endDate)
        {
            try
            {
                return _paymentVoucherRepository.GetTotalPaymentsByPaymentMode(paymentMode, startDate, endDate);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GetTotalPaymentsByPaymentMode", ex);
                return 0;
            }
        }

        public Dictionary<string, decimal> GetPaymentSummary(DateTime startDate, DateTime endDate)
        {
            try
            {
                var summary = new Dictionary<string, decimal>();
                var paymentModes = new[] { "CASH", "CARD", "CHEQUE", "BANK_TRANSFER", "EASYPAISA", "JAZZCASH" };

                foreach (var mode in paymentModes)
                {
                    var total = GetTotalPaymentsByPaymentMode(mode, startDate, endDate);
                    if (total > 0)
                    {
                        summary[mode] = total;
                    }
                }

                return summary;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherService.GetPaymentSummary", ex);
                return new Dictionary<string, decimal>();
            }
        }

        #endregion

        #region Helper Methods

        private ChartOfAccount GetCreditAccountForPayment(string paymentMode)
        {
            try
            {
                // Get appropriate account based on payment mode
                switch (paymentMode?.ToUpper())
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
                DebugHelper.WriteException("PaymentVoucherService.GetCreditAccountForPayment", ex);
                return _chartOfAccountService.GetDefaultCashAccount();
            }
        }

        #endregion
    }
}

