using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IBankAccountRepository
    {
        // CRUD Operations
        int Create(BankAccount bankAccount);
        bool Update(BankAccount bankAccount);
        bool Delete(int bankAccountId);
        BankAccount GetById(int bankAccountId);
        BankAccount GetByAccountNumber(string accountNumber);
        List<BankAccount> GetAll();
        List<BankAccount> GetActive();
        
        // Reports
        List<BankAccount> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetCount(bool? isActive);
    }
}
