using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IBankAccountService
    {
        // CRUD Operations
        int CreateBankAccount(BankAccount bankAccount);
        bool UpdateBankAccount(BankAccount bankAccount);
        bool DeleteBankAccount(int bankAccountId);
        BankAccount GetBankAccountById(int bankAccountId);
        BankAccount GetBankAccountByNumber(string accountNumber);
        List<BankAccount> GetAllBankAccounts();
        List<BankAccount> GetActiveBankAccounts();
        
        // Business Logic
        bool ValidateBankAccount(BankAccount bankAccount);
        string[] GetValidationErrors(BankAccount bankAccount);
        bool IsAccountNumberExists(string accountNumber, int? excludeId = null);
        
        // Reports
        List<BankAccount> GetBankAccountReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetBankAccountCount(bool? isActive);
    }
}
