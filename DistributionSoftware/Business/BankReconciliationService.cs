using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public class BankReconciliationService : IBankReconciliationService
    {
        private readonly IBankReconciliationRepository _bankReconciliationRepository;

        public BankReconciliationService(IBankReconciliationRepository bankReconciliationRepository)
        {
            _bankReconciliationRepository = bankReconciliationRepository;
        }

        public int CreateBankReconciliation(BankReconciliation bankReconciliation)
        {
            try
            {
                if (!ValidateBankReconciliation(bankReconciliation))
                    throw new InvalidOperationException("Bank reconciliation validation failed");

                return _bankReconciliationRepository.Create(bankReconciliation);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankReconciliationService", ex);
                throw;
            }
        }

        public bool UpdateBankReconciliation(BankReconciliation bankReconciliation)
        {
            try
            {
                if (!ValidateBankReconciliation(bankReconciliation))
                    throw new InvalidOperationException("Bank reconciliation validation failed");

                return _bankReconciliationRepository.Update(bankReconciliation);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankReconciliationService", ex);
                throw;
            }
        }

        public bool DeleteBankReconciliation(int reconciliationId)
        {
            try
            {
                return _bankReconciliationRepository.Delete(reconciliationId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankReconciliationService", ex);
                throw;
            }
        }

        public BankReconciliation GetBankReconciliationById(int reconciliationId)
        {
            try
            {
                return _bankReconciliationRepository.GetById(reconciliationId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankReconciliationService", ex);
                throw;
            }
        }

        public List<BankReconciliation> GetAllBankReconciliations()
        {
            try
            {
                return _bankReconciliationRepository.GetAll();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankReconciliationService", ex);
                throw;
            }
        }

        public List<BankReconciliation> GetAllReconciliations()
        {
            return GetAllBankReconciliations();
        }

        public List<BankReconciliation> GetBankReconciliationsByAccount(int bankAccountId)
        {
            try
            {
                return _bankReconciliationRepository.GetByBankAccountId(bankAccountId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankReconciliationService", ex);
                throw;
            }
        }

        public bool ValidateBankReconciliation(BankReconciliation bankReconciliation)
        {
            return GetValidationErrors(bankReconciliation).Length == 0;
        }

        public string[] GetValidationErrors(BankReconciliation bankReconciliation)
        {
            var errors = new List<string>();

            if (bankReconciliation == null)
            {
                errors.Add("Bank reconciliation cannot be null");
                return errors.ToArray();
            }

            if (bankReconciliation.BankAccountId <= 0)
                errors.Add("Bank account is required");

            if (string.IsNullOrWhiteSpace(bankReconciliation.Status))
                errors.Add("Status is required");

            if (bankReconciliation.BankBalance < 0)
                errors.Add("Bank balance cannot be negative");

            if (bankReconciliation.BookBalance < 0)
                errors.Add("Book balance cannot be negative");

            return errors.ToArray();
        }

        public bool PerformReconciliation(int bankAccountId, DateTime statementEndDate)
        {
            try
            {
                // This would contain the actual reconciliation logic
                // For now, just create a basic reconciliation record
                var reconciliation = new BankReconciliation
                {
                    BankAccountId = bankAccountId,
                    StatementEndDate = statementEndDate,
                    BankBalance = 0, // Would be calculated from bank statements
                    BookBalance = 0, // Would be calculated from journal vouchers
                    ReconciledBy = 0, // System user ID
                    ReconciliationDate = DateTime.Now,
                    Status = "PENDING"
                };

                return CreateBankReconciliation(reconciliation) > 0;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankReconciliationService", ex);
                return false;
            }
        }

        public List<BankReconciliation> GetBankReconciliationReport(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                return _bankReconciliationRepository.GetReport(startDate, endDate);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankReconciliationService", ex);
                throw;
            }
        }

        public int GetBankReconciliationCount()
        {
            try
            {
                return _bankReconciliationRepository.GetCount();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in BankReconciliationService", ex);
                throw;
            }
        }
    }
}
