using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public class BankStatementService : IBankStatementService
    {
        private readonly IBankStatementRepository _bankStatementRepository;

        public BankStatementService(IBankStatementRepository bankStatementRepository)
        {
            _bankStatementRepository = bankStatementRepository;
        }

        public int CreateBankStatement(BankStatement bankStatement)
        {
            try
            {
                if (!ValidateBankStatement(bankStatement))
                    throw new InvalidOperationException("Bank statement validation failed");

                return _bankStatementRepository.Create(bankStatement);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankStatementService", ex);
                throw;
            }
        }

        public bool UpdateBankStatement(BankStatement bankStatement)
        {
            try
            {
                if (!ValidateBankStatement(bankStatement))
                    throw new InvalidOperationException("Bank statement validation failed");

                return _bankStatementRepository.Update(bankStatement);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankStatementService", ex);
                throw;
            }
        }

        public bool DeleteBankStatement(int statementId)
        {
            try
            {
                return _bankStatementRepository.Delete(statementId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankStatementService", ex);
                throw;
            }
        }

        public BankStatement GetBankStatementById(int statementId)
        {
            try
            {
                return _bankStatementRepository.GetById(statementId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankStatementService", ex);
                throw;
            }
        }

        public List<BankStatement> GetAllBankStatements()
        {
            try
            {
                return _bankStatementRepository.GetAll();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankStatementService", ex);
                throw;
            }
        }

        public List<BankStatement> GetActiveBankStatements()
        {
            try
            {
                return _bankStatementRepository.GetActive();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankStatementService", ex);
                throw;
            }
        }

        public List<BankStatement> GetBankStatementsByAccount(int bankAccountId)
        {
            try
            {
                return _bankStatementRepository.GetByBankAccountId(bankAccountId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankStatementService", ex);
                throw;
            }
        }

        public bool ValidateBankStatement(BankStatement bankStatement)
        {
            return GetValidationErrors(bankStatement).Length == 0;
        }

        public string[] GetValidationErrors(BankStatement bankStatement)
        {
            var errors = new List<string>();

            if (bankStatement == null)
            {
                errors.Add("Bank statement cannot be null");
                return errors.ToArray();
            }

            if (bankStatement.BankAccountId <= 0)
                errors.Add("Bank account is required");

            if (string.IsNullOrWhiteSpace(bankStatement.Description))
                errors.Add("Description is required");

            if (bankStatement.Debit < 0)
                errors.Add("Debit amount cannot be negative");

            if (bankStatement.Credit < 0)
                errors.Add("Credit amount cannot be negative");

            if (bankStatement.Debit > 0 && bankStatement.Credit > 0)
                errors.Add("Cannot have both debit and credit amounts");

            if (bankStatement.Debit == 0 && bankStatement.Credit == 0)
                errors.Add("Either debit or credit amount must be greater than 0");

            return errors.ToArray();
        }

        public List<BankStatement> GetBankStatementReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            try
            {
                return _bankStatementRepository.GetReport(startDate, endDate, isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankStatementService", ex);
                throw;
            }
        }

        public int GetBankStatementCount(bool? isActive)
        {
            try
            {
                return _bankStatementRepository.GetCount(isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankStatementService", ex);
                throw;
            }
        }
    }
}
