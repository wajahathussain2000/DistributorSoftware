using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public class BankIntegrationService : IBankIntegrationService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IJournalVoucherRepository _journalVoucherRepository;
        private readonly ISalesPaymentRepository _salesPaymentRepository;
        private readonly IPurchasePaymentRepository _purchasePaymentRepository;
        private readonly IBankStatementRepository _bankStatementRepository;

        public BankIntegrationService(
            IBankAccountRepository bankAccountRepository,
            IJournalVoucherRepository journalVoucherRepository,
            ISalesPaymentRepository salesPaymentRepository,
            IPurchasePaymentRepository purchasePaymentRepository,
            IBankStatementRepository bankStatementRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _journalVoucherRepository = journalVoucherRepository;
            _salesPaymentRepository = salesPaymentRepository;
            _purchasePaymentRepository = purchasePaymentRepository;
            _bankStatementRepository = bankStatementRepository;
        }

        public void LinkJournalVoucherToBankAccount(JournalVoucher voucher, int bankAccountId)
        {
            try
            {
                if (voucher == null || !ValidateBankAccount(bankAccountId)) return;

                voucher.BankAccountId = bankAccountId;
                _journalVoucherRepository.Update(voucher);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
            }
        }

        public void LinkSalesPaymentToBankAccount(SalesPayment payment, int bankAccountId)
        {
            try
            {
                if (payment == null || !ValidateBankAccount(bankAccountId)) return;

                payment.BankAccountId = bankAccountId;
                _salesPaymentRepository.Update(payment);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
            }
        }

        public void LinkPurchasePaymentToBankAccount(PurchasePayment payment, int bankAccountId)
        {
            try
            {
                if (payment == null || !ValidateBankAccount(bankAccountId)) return;

                payment.BankAccountId = bankAccountId;
                _purchasePaymentRepository.Update(payment);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
            }
        }

        public int? DetectBankAccountFromPaymentMode(string paymentMode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(paymentMode)) return null;

                var bankAccounts = _bankAccountRepository.GetActive();
                
                // Map payment modes to bank accounts
                switch (paymentMode.ToUpper())
                {
                    case "BANK_TRANSFER":
                    case "CHEQUE":
                        // Return the first active bank account
                        return bankAccounts.FirstOrDefault()?.BankAccountId;
                    case "CASH":
                        // Cash payments don't need bank account
                        return null;
                    default:
                        return null;
                }
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
                return null;
            }
        }

        public int? DetectBankAccountFromReference(string reference)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(reference)) return null;

                var bankAccounts = _bankAccountRepository.GetActive();
                
                // Check if reference contains bank account information
                foreach (var account in bankAccounts)
                {
                    if (reference.Contains(account.AccountNumber) || 
                        reference.Contains(account.BankName) ||
                        reference.Contains(account.AccountHolder))
                    {
                        return account.BankAccountId;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
                return null;
            }
        }

        public void CreateBankStatementFromJournalVoucher(JournalVoucher voucher)
        {
            try
            {
                if (voucher == null || !voucher.BankAccountId.HasValue) return;

                var bankStatement = new BankStatement
                {
                    BankAccountId = voucher.BankAccountId.Value,
                    TransactionDate = voucher.VoucherDate,
                    Description = voucher.Narration,
                    ReferenceNumber = voucher.VoucherNumber,
                    Debit = voucher.TotalDebit,
                    Credit = voucher.TotalCredit,
                    Balance = 0, // Will be calculated by reconciliation process
                    IsReconciled = false
                };

                _bankStatementRepository.Create(bankStatement);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
            }
        }

        public void CreateBankStatementFromSalesPayment(SalesPayment payment)
        {
            try
            {
                if (payment == null || payment.BankAccountId <= 0) return;

                var bankStatement = new BankStatement
                {
                    BankAccountId = payment.BankAccountId,
                    TransactionDate = payment.PaymentDate,
                    Description = $"Sales Payment - {payment.PaymentMode}",
                    ReferenceNumber = payment.PaymentReference,
                    Debit = 0,
                    Credit = payment.Amount,
                    Balance = 0, // Will be calculated by reconciliation process
                    IsReconciled = false
                };

                _bankStatementRepository.Create(bankStatement);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
            }
        }

        public void CreateBankStatementFromPurchasePayment(PurchasePayment payment)
        {
            try
            {
                if (payment == null || payment.BankAccountId <= 0) return;

                var bankStatement = new BankStatement
                {
                    BankAccountId = payment.BankAccountId,
                    TransactionDate = payment.PaymentDate,
                    Description = $"Purchase Payment - {payment.PaymentMode}",
                    ReferenceNumber = payment.PaymentReference,
                    Debit = payment.Amount,
                    Credit = 0,
                    Balance = 0, // Will be calculated by reconciliation process
                    IsReconciled = false
                };

                _bankStatementRepository.Create(bankStatement);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
            }
        }

        public bool ValidateBankAccount(int bankAccountId)
        {
            try
            {
                var bankAccount = _bankAccountRepository.GetById(bankAccountId);
                return bankAccount != null && bankAccount.IsActive;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
                return false;
            }
        }

        public bool ValidateBankTransaction(decimal amount, string description)
        {
            try
            {
                if (amount <= 0) return false;
                if (string.IsNullOrWhiteSpace(description)) return false;
                return true;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
                return false;
            }
        }

        public List<JournalVoucher> GetJournalVouchersByBankAccount(int bankAccountId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                return _journalVoucherRepository.GetByBankAccount(bankAccountId, startDate, endDate);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
                return new List<JournalVoucher>();
            }
        }

        public List<SalesPayment> GetSalesPaymentsByBankAccount(int bankAccountId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                return _salesPaymentRepository.GetByBankAccount(bankAccountId, startDate, endDate);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
                return new List<SalesPayment>();
            }
        }

        public List<PurchasePayment> GetPurchasePaymentsByBankAccount(int bankAccountId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                return _purchasePaymentRepository.GetByBankAccount(bankAccountId, startDate, endDate);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankIntegrationService", ex);
                return new List<PurchasePayment>();
            }
        }
    }
}
