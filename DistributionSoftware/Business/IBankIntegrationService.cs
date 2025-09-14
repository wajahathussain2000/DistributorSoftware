using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IBankIntegrationService
    {
        // Bank Transaction Integration
        void LinkJournalVoucherToBankAccount(JournalVoucher voucher, int bankAccountId);
        void LinkSalesPaymentToBankAccount(SalesPayment payment, int bankAccountId);
        void LinkPurchasePaymentToBankAccount(PurchasePayment payment, int bankAccountId);
        
        // Automatic Bank Account Detection
        int? DetectBankAccountFromPaymentMode(string paymentMode);
        int? DetectBankAccountFromReference(string reference);
        
        // Bank Reconciliation Integration
        void CreateBankStatementFromJournalVoucher(JournalVoucher voucher);
        void CreateBankStatementFromSalesPayment(SalesPayment payment);
        void CreateBankStatementFromPurchasePayment(PurchasePayment payment);
        
        // Validation
        bool ValidateBankAccount(int bankAccountId);
        bool ValidateBankTransaction(decimal amount, string description);
        
        // Reports
        List<JournalVoucher> GetJournalVouchersByBankAccount(int bankAccountId, DateTime? startDate, DateTime? endDate);
        List<SalesPayment> GetSalesPaymentsByBankAccount(int bankAccountId, DateTime? startDate, DateTime? endDate);
        List<PurchasePayment> GetPurchasePaymentsByBankAccount(int bankAccountId, DateTime? startDate, DateTime? endDate);
    }
}
