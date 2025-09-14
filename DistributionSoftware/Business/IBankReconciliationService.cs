using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IBankReconciliationService
    {
        // CRUD Operations
        int CreateBankReconciliation(BankReconciliation bankReconciliation);
        bool UpdateBankReconciliation(BankReconciliation bankReconciliation);
        bool DeleteBankReconciliation(int reconciliationId);
        BankReconciliation GetBankReconciliationById(int reconciliationId);
        List<BankReconciliation> GetAllBankReconciliations();
        List<BankReconciliation> GetAllReconciliations();
        List<BankReconciliation> GetBankReconciliationsByAccount(int bankAccountId);
        
        // Business Logic
        bool ValidateBankReconciliation(BankReconciliation bankReconciliation);
        string[] GetValidationErrors(BankReconciliation bankReconciliation);
        bool PerformReconciliation(int bankAccountId, DateTime statementEndDate);
        
        // Reports
        List<BankReconciliation> GetBankReconciliationReport(DateTime? startDate, DateTime? endDate);
        int GetBankReconciliationCount();
    }
}
