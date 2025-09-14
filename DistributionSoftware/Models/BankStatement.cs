using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents bank statement transactions
    /// </summary>
    public class BankStatement
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the bank statement entry
        /// </summary>
        public int BankStatementId { get; set; }
        
        /// <summary>
        /// Bank account ID this statement belongs to
        /// </summary>
        public int BankAccountId { get; set; }
        
        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime TransactionDate { get; set; }
        
        /// <summary>
        /// Transaction description
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Reference number
        /// </summary>
        public string ReferenceNumber { get; set; }
        
        /// <summary>
        /// Transaction type (DEBIT, CREDIT)
        /// </summary>
        public string TransactionType { get; set; }
        
        /// <summary>
        /// Transaction amount
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Debit amount (for DEBIT transactions)
        /// </summary>
        public decimal Debit { get; set; }
        
        /// <summary>
        /// Credit amount (for CREDIT transactions)
        /// </summary>
        public decimal Credit { get; set; }
        
        /// <summary>
        /// Statement ID (alias for BankStatementId)
        /// </summary>
        public int StatementId { get => BankStatementId; set => BankStatementId = value; }
        
        /// <summary>
        /// Reconciliation ID (alias for MatchedJournalVoucherId)
        /// </summary>
        public int? ReconciliationId { get => MatchedJournalVoucherId; set => MatchedJournalVoucherId = value; }
        
        /// <summary>
        /// Running balance after this transaction
        /// </summary>
        public decimal Balance { get; set; }
        
        /// <summary>
        /// Whether this transaction is reconciled
        /// </summary>
        public bool IsReconciled { get; set; }
        
        /// <summary>
        /// Reconciliation date
        /// </summary>
        public DateTime? ReconciliationDate { get; set; }
        
        /// <summary>
        /// User who reconciled this transaction
        /// </summary>
        public int? ReconciledBy { get; set; }
        
        /// <summary>
        /// Name of the user who reconciled this transaction
        /// </summary>
        public string ReconciledByName { get; set; }
        
        /// <summary>
        /// Chart of account ID this transaction is matched to
        /// </summary>
        public int? MatchedAccountId { get; set; }
        
        /// <summary>
        /// Journal voucher ID this transaction is matched to
        /// </summary>
        public int? MatchedJournalVoucherId { get; set; }
        
        /// <summary>
        /// Matching notes
        /// </summary>
        public string MatchingNotes { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the bank statement entry was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the bank statement entry
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the bank statement entry
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the bank statement entry was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the bank statement entry
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the bank statement entry
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// Bank account this statement belongs to
        /// </summary>
        public BankAccount BankAccount { get; set; }
        
        /// <summary>
        /// Chart of account this transaction is matched to
        /// </summary>
        public ChartOfAccount MatchedAccount { get; set; }
        
        /// <summary>
        /// Journal voucher this transaction is matched to
        /// </summary>
        public JournalVoucher MatchedJournalVoucher { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public BankStatement()
        {
            CreatedDate = DateTime.Now;
            IsReconciled = false;
            Amount = 0;
            Balance = 0;
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the bank statement entry
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (BankAccountId <= 0) return false;
            if (string.IsNullOrWhiteSpace(Description)) return false;
            if (string.IsNullOrWhiteSpace(TransactionType)) return false;
            if (Amount <= 0) return false;
            
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
            
            if (string.IsNullOrWhiteSpace(Description))
                errors.Add("Description is required");
            
            if (string.IsNullOrWhiteSpace(TransactionType))
                errors.Add("Transaction type is required");
            
            if (Amount <= 0)
                errors.Add("Amount must be greater than zero");
            
            if (TransactionType != "DEBIT" && TransactionType != "CREDIT")
                errors.Add("Transaction type must be DEBIT or CREDIT");
            
            return errors.ToArray();
        }
        
        /// <summary>
        /// Calculates the net amount (positive for credit, negative for debit)
        /// </summary>
        /// <returns>Net amount</returns>
        public decimal GetNetAmount()
        {
            return TransactionType == "CREDIT" ? Amount : -Amount;
        }
        
        /// <summary>
        /// Checks if this transaction can be automatically matched
        /// </summary>
        /// <param name="journalVoucher">Journal voucher to match against</param>
        /// <returns>True if can be matched</returns>
        public bool CanMatchWithJournalVoucher(JournalVoucher journalVoucher)
        {
            if (journalVoucher == null) return false;
            if (IsReconciled) return false;
            
            // Check amount match
            var netAmount = GetNetAmount();
            var journalAmount = journalVoucher.GetNetAmount();
            
            return Math.Abs(netAmount - journalAmount) < 0.01m; // Allow for small rounding differences
        }
        
        #endregion
    }
}
