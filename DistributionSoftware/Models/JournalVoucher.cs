using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents a journal voucher for double-entry bookkeeping
    /// </summary>
    public class JournalVoucher
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the journal voucher
        /// </summary>
        public int VoucherId { get; set; }
        
        /// <summary>
        /// Alias for VoucherId for backward compatibility
        /// </summary>
        public int JournalVoucherId { get => VoucherId; set => VoucherId = value; }
        
        /// <summary>
        /// Journal voucher number (e.g., JV-2024-001)
        /// </summary>
        public string VoucherNumber { get; set; }
        
        /// <summary>
        /// Date of the journal voucher
        /// </summary>
        public DateTime VoucherDate { get; set; }
        
        /// <summary>
        /// Reference to the source transaction (e.g., SalesInvoiceId, PurchaseInvoiceId)
        /// </summary>
        public string Reference { get; set; }
        
        /// <summary>
        /// Reference to bank account for bank-related transactions
        /// </summary>
        public int? BankAccountId { get; set; }
        
        /// <summary>
        /// Description of the journal voucher
        /// </summary>
        public string Narration { get; set; }
        
        /// <summary>
        /// Total debit amount (should equal total credit amount)
        /// </summary>
        public decimal TotalDebit { get; set; }
        
        /// <summary>
        /// Total credit amount (should equal total debit amount)
        /// </summary>
        public decimal TotalCredit { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the voucher was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the voucher
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the voucher
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the voucher was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the voucher
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the voucher
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// List of journal voucher details (debit and credit lines)
        /// </summary>
        public List<JournalVoucherDetail> Details { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public JournalVoucher()
        {
            Details = new List<JournalVoucherDetail>();
            VoucherDate = DateTime.Now;
            CreatedDate = DateTime.Now;
        }
        
        #endregion
        
        #region Calculated Properties
        
        /// <summary>
        /// Whether the voucher is balanced (debits = credits)
        /// </summary>
        public bool IsBalanced => TotalDebit == TotalCredit;
        
        /// <summary>
        /// Difference between debits and credits (should be 0 for balanced voucher)
        /// </summary>
        public decimal BalanceDifference => TotalDebit - TotalCredit;
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the journal voucher
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(VoucherNumber)) return false;
            if (string.IsNullOrWhiteSpace(Narration)) return false;
            if (TotalDebit <= 0) return false;
            if (TotalCredit <= 0) return false;
            if (!IsBalanced) return false;
            if (Details == null || Details.Count == 0) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public List<string> GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(VoucherNumber))
                errors.Add("Voucher Number is required");
            
            if (string.IsNullOrWhiteSpace(Narration))
                errors.Add("Narration is required");
            
            if (TotalDebit <= 0)
                errors.Add("Total Debit Amount must be greater than 0");
            
            if (TotalCredit <= 0)
                errors.Add("Total Credit Amount must be greater than 0");
            
            if (!IsBalanced)
                errors.Add($"Voucher is not balanced. Difference: {BalanceDifference:C}");
            
            if (Details == null || Details.Count == 0)
                errors.Add("At least one journal voucher detail is required");
            
            return errors;
        }
        
        /// <summary>
        /// Gets the net amount (TotalDebit - TotalCredit)
        /// </summary>
        /// <returns>Net amount</returns>
        public decimal GetNetAmount()
        {
            return TotalDebit - TotalCredit;
        }
        
        #endregion
    }
}
