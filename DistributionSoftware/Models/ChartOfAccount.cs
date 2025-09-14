using System;
using System.Collections.Generic;
using System.Linq;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents a Chart of Account entry for financial categorization
    /// </summary>
    public class ChartOfAccount
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the account
        /// </summary>
        public int AccountId { get; set; }
        
        /// <summary>
        /// Unique account code (e.g., 1000, 2000, 3000)
        /// </summary>
        public string AccountCode { get; set; }
        
        /// <summary>
        /// Account name (e.g., "Cash in Hand", "Accounts Receivable")
        /// </summary>
        public string AccountName { get; set; }
        
        /// <summary>
        /// Account type: ASSET, LIABILITY, EQUITY, REVENUE, EXPENSE
        /// </summary>
        public string AccountType { get; set; }
        
        /// <summary>
        /// Account category: CURRENT_ASSET, FIXED_ASSET, CURRENT_LIABILITY, etc.
        /// </summary>
        public string AccountCategory { get; set; }
        
        /// <summary>
        /// Parent account ID for hierarchical structure
        /// </summary>
        public int? ParentAccountId { get; set; }
        
        /// <summary>
        /// Level in hierarchy (1 = top level, 2 = sub level, etc.)
        /// </summary>
        public int AccountLevel { get; set; }
        
        /// <summary>
        /// Whether the account is active
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// Whether this is a system account (cannot be deleted)
        /// </summary>
        public bool IsSystemAccount { get; set; }
        
        /// <summary>
        /// Normal balance: DEBIT or CREDIT
        /// </summary>
        public string NormalBalance { get; set; }
        
        /// <summary>
        /// Account description
        /// </summary>
        public string Description { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the account was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User ID who created the account
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of user who created the account
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the account was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User ID who last modified the account
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of user who last modified the account
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// Parent account reference
        /// </summary>
        public ChartOfAccount ParentAccount { get; set; }
        
        /// <summary>
        /// Child accounts list
        /// </summary>
        public List<ChartOfAccount> ChildAccounts { get; set; }
        
        #endregion
        
        #region Calculated Properties
        
        /// <summary>
        /// Full display name with code
        /// </summary>
        public string FullAccountName => $"{AccountCode} - {AccountName}";
        
        /// <summary>
        /// Hierarchical account path
        /// </summary>
        public string AccountPath => GetAccountPath();
        
        /// <summary>
        /// Whether this is a leaf account (no children)
        /// </summary>
        public bool IsLeafAccount => ChildAccounts == null || !ChildAccounts.Any();
        
        /// <summary>
        /// Account type display text
        /// </summary>
        public string AccountTypeDisplay => GetAccountTypeDisplay();
        
        /// <summary>
        /// Account category display text
        /// </summary>
        public string AccountCategoryDisplay => GetAccountCategoryDisplay();
        
        /// <summary>
        /// Status display text
        /// </summary>
        public string StatusText => IsActive ? "Active" : "Inactive";
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public ChartOfAccount()
        {
            ChildAccounts = new List<ChartOfAccount>();
            IsActive = true;
            IsSystemAccount = false;
            AccountLevel = 1;
            CreatedDate = DateTime.Now;
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Gets the hierarchical path of the account
        /// </summary>
        /// <returns>Account path string</returns>
        private string GetAccountPath()
        {
            if (ParentAccount != null)
            {
                return $"{ParentAccount.AccountPath} > {AccountName}";
            }
            return AccountName;
        }
        
        /// <summary>
        /// Gets display text for account type
        /// </summary>
        /// <returns>Formatted account type</returns>
        private string GetAccountTypeDisplay()
        {
            switch (AccountType?.ToUpper())
            {
                case "ASSET":
                    return "Assets";
                case "LIABILITY":
                    return "Liabilities";
                case "EQUITY":
                    return "Equity";
                case "REVENUE":
                    return "Revenue";
                case "EXPENSE":
                    return "Expenses";
                default:
                    return AccountType ?? "Unknown";
            }
        }
        
        /// <summary>
        /// Gets display text for account category
        /// </summary>
        /// <returns>Formatted account category</returns>
        private string GetAccountCategoryDisplay()
        {
            switch (AccountCategory?.ToUpper())
            {
                case "CURRENT_ASSET":
                    return "Current Assets";
                case "FIXED_ASSET":
                    return "Fixed Assets";
                case "CURRENT_LIABILITY":
                    return "Current Liabilities";
                case "LONG_TERM_LIABILITY":
                    return "Long-term Liabilities";
                case "OWNERS_EQUITY":
                    return "Owner's Equity";
                case "SALES_REVENUE":
                    return "Sales Revenue";
                case "OTHER_REVENUE":
                    return "Other Revenue";
                case "COST_OF_GOODS_SOLD":
                    return "Cost of Goods Sold";
                case "OPERATING_EXPENSE":
                    return "Operating Expenses";
                case "ADMINISTRATIVE_EXPENSE":
                    return "Administrative Expenses";
                default:
                    return AccountCategory ?? "Unknown";
            }
        }
        
        #endregion
        
        #region Validation Methods
        
        /// <summary>
        /// Validates the account data
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(AccountCode)) return false;
            if (string.IsNullOrWhiteSpace(AccountName)) return false;
            if (string.IsNullOrWhiteSpace(AccountType)) return false;
            if (string.IsNullOrWhiteSpace(AccountCategory)) return false;
            if (string.IsNullOrWhiteSpace(NormalBalance)) return false;
            if (AccountLevel < 1) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public List<string> GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(AccountCode))
                errors.Add("Account Code is required");
            
            if (string.IsNullOrWhiteSpace(AccountName))
                errors.Add("Account Name is required");
            
            if (string.IsNullOrWhiteSpace(AccountType))
                errors.Add("Account Type is required");
            
            if (string.IsNullOrWhiteSpace(AccountCategory))
                errors.Add("Account Category is required");
            
            if (string.IsNullOrWhiteSpace(NormalBalance))
                errors.Add("Normal Balance is required");
            
            if (AccountLevel < 1)
                errors.Add("Account Level must be at least 1");
            
            return errors;
        }
        
        #endregion
    }
}
