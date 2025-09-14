using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents bank accounts for reconciliation
    /// </summary>
    public class BankAccount
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the bank account
        /// </summary>
        public int BankAccountId { get; set; }
        
        /// <summary>
        /// Bank name
        /// </summary>
        public string BankName { get; set; }
        
        /// <summary>
        /// Account number
        /// </summary>
        public string AccountNumber { get; set; }
        
        /// <summary>
        /// Account holder name
        /// </summary>
        public string AccountHolderName { get; set; }
        
        /// <summary>
        /// Account type (e.g., "CURRENT", "SAVINGS", "BUSINESS")
        /// </summary>
        public string AccountType { get; set; }
        
        /// <summary>
        /// Bank branch
        /// </summary>
        public string Branch { get; set; }
        
        /// <summary>
        /// Branch name (alias for Branch)
        /// </summary>
        public string BranchName { get => Branch; set => Branch = value; }
        
        /// <summary>
        /// Account holder (alias for AccountHolderName)
        /// </summary>
        public string AccountHolder { get => AccountHolderName; set => AccountHolderName = value; }
        
        /// <summary>
        /// Bank address
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Contact phone
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Contact email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Chart of account ID for this bank account
        /// </summary>
        public int? ChartOfAccountId { get; set; }
        
        /// <summary>
        /// Current balance
        /// </summary>
        public decimal CurrentBalance { get; set; }
        
        /// <summary>
        /// Last reconciliation date
        /// </summary>
        public DateTime? LastReconciliationDate { get; set; }
        
        /// <summary>
        /// Whether this bank account is active
        /// </summary>
        public bool IsActive { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the bank account was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the bank account
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the bank account
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the bank account was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the bank account
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the bank account
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// Chart of account this bank account belongs to
        /// </summary>
        public ChartOfAccount ChartOfAccount { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public BankAccount()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            CurrentBalance = 0;
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the bank account
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(BankName)) return false;
            if (string.IsNullOrWhiteSpace(AccountNumber)) return false;
            if (string.IsNullOrWhiteSpace(AccountHolderName)) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public string[] GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(BankName))
                errors.Add("Bank name is required");
            
            if (string.IsNullOrWhiteSpace(AccountNumber))
                errors.Add("Account number is required");
            
            if (string.IsNullOrWhiteSpace(AccountHolderName))
                errors.Add("Account holder name is required");
            
            return errors.ToArray();
        }
        
        #endregion
    }
}
