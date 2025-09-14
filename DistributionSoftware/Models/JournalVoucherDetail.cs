using System;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents a line item in a journal voucher (debit or credit)
    /// </summary>
    public class JournalVoucherDetail
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the journal voucher detail
        /// </summary>
        public int DetailId { get; set; }
        
        /// <summary>
        /// Reference to the parent journal voucher
        /// </summary>
        public int VoucherId { get; set; }
        
        /// <summary>
        /// Reference to the chart of account
        /// </summary>
        public int AccountId { get; set; }
        
        /// <summary>
        /// Debit amount (0 if this is a credit line)
        /// </summary>
        public decimal DebitAmount { get; set; }
        
        /// <summary>
        /// Credit amount (0 if this is a debit line)
        /// </summary>
        public decimal CreditAmount { get; set; }
        
        /// <summary>
        /// Description for this specific line
        /// </summary>
        public string Narration { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// Reference to the chart of account
        /// </summary>
        public ChartOfAccount Account { get; set; }
        
        /// <summary>
        /// Reference to the parent journal voucher
        /// </summary>
        public JournalVoucher JournalVoucher { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public JournalVoucherDetail()
        {
            DebitAmount = 0;
            CreditAmount = 0;
        }
        
        #endregion
        
        #region Calculated Properties
        
        /// <summary>
        /// Whether this is a debit line
        /// </summary>
        public bool IsDebit => DebitAmount > 0;
        
        /// <summary>
        /// Whether this is a credit line
        /// </summary>
        public bool IsCredit => CreditAmount > 0;
        
        /// <summary>
        /// The amount for this line (debit or credit)
        /// </summary>
        public decimal Amount => IsDebit ? DebitAmount : CreditAmount;
        
        /// <summary>
        /// Display text for debit/credit
        /// </summary>
        public string DebitCreditText => IsDebit ? "Debit" : "Credit";
        
        /// <summary>
        /// Formatted amount with debit/credit indicator
        /// </summary>
        public string FormattedAmount => IsDebit ? $"{DebitAmount:C} Dr" : $"{CreditAmount:C} Cr";
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the journal voucher detail
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            // Must have either debit or credit, but not both
            if (DebitAmount > 0 && CreditAmount > 0) return false;
            if (DebitAmount <= 0 && CreditAmount <= 0) return false;
            
            // Must have account reference
            if (AccountId <= 0) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public System.Collections.Generic.List<string> GetValidationErrors()
        {
            var errors = new System.Collections.Generic.List<string>();
            
            if (DebitAmount > 0 && CreditAmount > 0)
                errors.Add("Cannot have both debit and credit amounts");
            
            if (DebitAmount <= 0 && CreditAmount <= 0)
                errors.Add("Must have either debit or credit amount");
            
            if (AccountId <= 0)
                errors.Add("Account reference is required");
            
            return errors;
        }
        
        #endregion
    }
}
