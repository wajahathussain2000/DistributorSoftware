using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IPurchasePaymentRepository
    {
        // CRUD Operations
        int Create(PurchasePayment purchasePayment);
        bool Update(PurchasePayment purchasePayment);
        bool Delete(int paymentId);
        PurchasePayment GetById(int paymentId);
        List<PurchasePayment> GetAll();
        List<PurchasePayment> GetByPurchaseInvoiceId(int purchaseInvoiceId);
        List<PurchasePayment> GetByBankAccount(int bankAccountId, DateTime? startDate = null, DateTime? endDate = null);
        
        // Reports
        List<PurchasePayment> GetReport(DateTime? startDate, DateTime? endDate);
        int GetCount();
    }
}
