using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IBankStatementRepository
    {
        // CRUD Operations
        int Create(BankStatement bankStatement);
        bool Update(BankStatement bankStatement);
        bool Delete(int statementId);
        BankStatement GetById(int statementId);
        List<BankStatement> GetAll();
        List<BankStatement> GetActive();
        List<BankStatement> GetByBankAccountId(int bankAccountId);
        
        // Reports
        List<BankStatement> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetCount(bool? isActive);
    }
}
