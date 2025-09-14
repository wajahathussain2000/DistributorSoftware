using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IBankReconciliationRepository
    {
        // CRUD Operations
        int Create(BankReconciliation bankReconciliation);
        bool Update(BankReconciliation bankReconciliation);
        bool Delete(int reconciliationId);
        BankReconciliation GetById(int reconciliationId);
        List<BankReconciliation> GetAll();
        List<BankReconciliation> GetByBankAccountId(int bankAccountId);
        
        // Reports
        List<BankReconciliation> GetReport(DateTime? startDate, DateTime? endDate);
        int GetCount();
    }
}
