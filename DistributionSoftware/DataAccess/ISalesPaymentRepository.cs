using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface ISalesPaymentRepository
    {
        // CRUD Operations
        int Create(SalesPayment salesPayment);
        bool Update(SalesPayment salesPayment);
        bool Delete(int paymentId);
        SalesPayment GetById(int paymentId);
        List<SalesPayment> GetAll();
        List<SalesPayment> GetBySalesInvoiceId(int salesInvoiceId);
        List<SalesPayment> GetByBankAccount(int bankAccountId, DateTime? startDate = null, DateTime? endDate = null);
        
        // Reports
        List<SalesPayment> GetReport(DateTime? startDate, DateTime? endDate);
        int GetCount();
    }
}
