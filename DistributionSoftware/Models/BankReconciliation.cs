using System;
using System.Collections.Generic;
using System.Linq;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents bank reconciliation records
    /// </summary>
    public class BankReconciliation
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the reconciliation
        /// </summary>
        public int ReconciliationId { get; set; }
        
        /// <summary>
        /// Bank account ID being reconciled
        /// </summary>
        public int BankAccountId { get; set; }
        
        /// <summary>
        /// Reconciliation date
        /// </summary>
        public DateTime ReconciliationDate { get; set; }
        
        /// <summary>
        /// Statement end date (alias for ReconciliationDate)
        /// </summary>
        public DateTime StatementEndDate { get => ReconciliationDate; set => ReconciliationDate = value; }
        
        /// <summary>
        /// Bank balance (alias for StatementBalance)
        /// </summary>
        public decimal BankBalance { get => StatementBalance; set => StatementBalance = value; }
        
        /// <summary>
        /// User who reconciled (alias for CreatedBy)
        /// </summary>
        public int ReconciledBy { get => CreatedBy; set => CreatedBy = value; }
        
        /// <summary>
        /// Statement balance from bank
        /// </summary>
        public decimal StatementBalance { get; set; }
        
        /// <summary>
        /// Book balance from accounting system
        /// </summary>
        public decimal BookBalance { get; set; }
        
        /// <summary>
        /// Difference between statement and book balance
        /// </summary>
        public decimal Difference { get; set; }
        
        /// <summary>
        /// Reconciliation status (PENDING, IN_PROGRESS, COMPLETED, DISCREPANCY)
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Notes about the reconciliation
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Whether this reconciliation is completed
        /// </summary>
        public bool IsCompleted { get; set; }
        
        /// <summary>
        /// Completion date
        /// </summary>
        public DateTime? CompletionDate { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the reconciliation was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the reconciliation
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the reconciliation
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the reconciliation was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the reconciliation
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the reconciliation
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// Bank account being reconciled
        /// </summary>
        public BankAccount BankAccount { get; set; }
        
        /// <summary>
        /// Bank statement transactions for this reconciliation
        /// </summary>
        public List<BankStatement> BankStatements { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public BankReconciliation()
        {
            CreatedDate = DateTime.Now;
            Status = "PENDING";
            IsCompleted = false;
            BankStatements = new List<BankStatement>();
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the reconciliation
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (BankAccountId <= 0) return false;
            if (ReconciliationDate == default(DateTime)) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public string[] GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (BankAccountId <= 0)
                errors.Add("Bank account is required");
            
            if (ReconciliationDate == default(DateTime))
                errors.Add("Reconciliation date is required");
            
            return errors.ToArray();
        }
        
        /// <summary>
        /// Calculates the difference between statement and book balance
        /// </summary>
        public void CalculateDifference()
        {
            Difference = StatementBalance - BookBalance;
        }
        
        /// <summary>
        /// Updates the reconciliation status based on current state
        /// </summary>
        public void UpdateStatus()
        {
            if (IsCompleted)
            {
                Status = "COMPLETED";
            }
            else if (Math.Abs(Difference) > 0.01m)
            {
                Status = "DISCREPANCY";
            }
            else if (BankStatements.Any(bs => !bs.IsReconciled))
            {
                Status = "IN_PROGRESS";
            }
            else
            {
                Status = "PENDING";
            }
        }
        
        /// <summary>
        /// Gets unreconciled bank statements
        /// </summary>
        /// <returns>List of unreconciled statements</returns>
        public List<BankStatement> GetUnreconciledStatements()
        {
            return BankStatements.Where(bs => !bs.IsReconciled).ToList();
        }
        
        /// <summary>
        /// Gets reconciled bank statements
        /// </summary>
        /// <returns>List of reconciled statements</returns>
        public List<BankStatement> GetReconciledStatements()
        {
            return BankStatements.Where(bs => bs.IsReconciled).ToList();
        }
        
        /// <summary>
        /// Calculates total unreconciled amount
        /// </summary>
        /// <returns>Total unreconciled amount</returns>
        public decimal GetTotalUnreconciledAmount()
        {
            return GetUnreconciledStatements().Sum(bs => bs.GetNetAmount());
        }
        
        /// <summary>
        /// Calculates total reconciled amount
        /// </summary>
        /// <returns>Total reconciled amount</returns>
        public decimal GetTotalReconciledAmount()
        {
            return GetReconciledStatements().Sum(bs => bs.GetNetAmount());
        }
        
        /// <summary>
        /// Checks if reconciliation is balanced
        /// </summary>
        /// <returns>True if balanced</returns>
        public bool IsBalanced()
        {
            return Math.Abs(Difference) < 0.01m;
        }
        
        /// <summary>
        /// Completes the reconciliation
        /// </summary>
        public void Complete()
        {
            IsCompleted = true;
            CompletionDate = DateTime.Now;
            UpdateStatus();
        }
        
        #endregion
    }
}
