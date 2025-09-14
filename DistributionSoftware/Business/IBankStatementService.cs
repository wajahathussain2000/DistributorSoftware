using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IBankStatementService
    {
        // CRUD Operations
        int CreateBankStatement(BankStatement bankStatement);
        bool UpdateBankStatement(BankStatement bankStatement);
        bool DeleteBankStatement(int statementId);
        BankStatement GetBankStatementById(int statementId);
        List<BankStatement> GetAllBankStatements();
        List<BankStatement> GetActiveBankStatements();
        List<BankStatement> GetBankStatementsByAccount(int bankAccountId);
        
        // Business Logic
        bool ValidateBankStatement(BankStatement bankStatement);
        string[] GetValidationErrors(BankStatement bankStatement);
        
        // Reports
        List<BankStatement> GetBankStatementReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetBankStatementCount(bool? isActive);
    }
}
