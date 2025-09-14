using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public BankAccountService(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public int CreateBankAccount(BankAccount bankAccount)
        {
            try
            {
                if (!ValidateBankAccount(bankAccount))
                    throw new InvalidOperationException("Bank account validation failed");

                return _bankAccountRepository.Create(bankAccount);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankAccountService", ex);
                throw;
            }
        }

        public bool UpdateBankAccount(BankAccount bankAccount)
        {
            try
            {
                if (!ValidateBankAccount(bankAccount))
                    throw new InvalidOperationException("Bank account validation failed");

                return _bankAccountRepository.Update(bankAccount);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankAccountService", ex);
                throw;
            }
        }

        public bool DeleteBankAccount(int bankAccountId)
        {
            try
            {
                return _bankAccountRepository.Delete(bankAccountId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankAccountService", ex);
                throw;
            }
        }

        public BankAccount GetBankAccountById(int bankAccountId)
        {
            try
            {
                return _bankAccountRepository.GetById(bankAccountId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankAccountService", ex);
                throw;
            }
        }

        public BankAccount GetBankAccountByNumber(string accountNumber)
        {
            try
            {
                return _bankAccountRepository.GetByAccountNumber(accountNumber);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankAccountService", ex);
                throw;
            }
        }

        public List<BankAccount> GetAllBankAccounts()
        {
            try
            {
                return _bankAccountRepository.GetAll();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankAccountService", ex);
                throw;
            }
        }

        public List<BankAccount> GetActiveBankAccounts()
        {
            try
            {
                return _bankAccountRepository.GetActive();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankAccountService", ex);
                throw;
            }
        }

        public bool ValidateBankAccount(BankAccount bankAccount)
        {
            return GetValidationErrors(bankAccount).Length == 0;
        }

        public string[] GetValidationErrors(BankAccount bankAccount)
        {
            var errors = new List<string>();

            if (bankAccount == null)
            {
                errors.Add("Bank account cannot be null");
                return errors.ToArray();
            }

            if (string.IsNullOrWhiteSpace(bankAccount.AccountNumber))
                errors.Add("Account number is required");

            if (string.IsNullOrWhiteSpace(bankAccount.BankName))
                errors.Add("Bank name is required");

            if (string.IsNullOrWhiteSpace(bankAccount.AccountHolder))
                errors.Add("Account holder is required");

            if (bankAccount.AccountNumber != null && bankAccount.AccountNumber.Length > 50)
                errors.Add("Account number cannot exceed 50 characters");

            if (bankAccount.BankName != null && bankAccount.BankName.Length > 100)
                errors.Add("Bank name cannot exceed 100 characters");

            if (bankAccount.BranchName != null && bankAccount.BranchName.Length > 100)
                errors.Add("Branch name cannot exceed 100 characters");

            if (bankAccount.AccountHolder != null && bankAccount.AccountHolder.Length > 100)
                errors.Add("Account holder cannot exceed 100 characters");

            return errors.ToArray();
        }

        public bool IsAccountNumberExists(string accountNumber, int? excludeId = null)
        {
            try
            {
                var existingAccount = GetBankAccountByNumber(accountNumber);
                return existingAccount != null && existingAccount.BankAccountId != excludeId;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankAccountService", ex);
                return false;
            }
        }

        public List<BankAccount> GetBankAccountReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            try
            {
                return _bankAccountRepository.GetReport(startDate, endDate, isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankAccountService", ex);
                throw;
            }
        }

        public int GetBankAccountCount(bool? isActive)
        {
            try
            {
                return _bankAccountRepository.GetCount(isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankAccountService", ex);
                throw;
            }
        }
    }
}
