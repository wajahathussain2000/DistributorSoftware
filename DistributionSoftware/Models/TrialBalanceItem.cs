using System;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents a trial balance item for reporting purposes
    /// </summary>
    public class TrialBalanceItem
    {
        #region Properties
        
        /// <summary>
        /// Account code
        /// </summary>
        public string AccountCode { get; set; }
        
        /// <summary>
        /// Account name
        /// </summary>
        public string AccountName { get; set; }
        
        /// <summary>
        /// Account type (ASSET, LIABILITY, EQUITY, INCOME, EXPENSE)
        /// </summary>
        public string AccountType { get; set; }
        
        /// <summary>
        /// Normal balance (DEBIT or CREDIT)
        /// </summary>
        public string NormalBalance { get; set; }
        
        /// <summary>
        /// Current balance of the account
        /// </summary>
        public decimal Balance { get; set; }
        
        /// <summary>
        /// Debit amount for trial balance
        /// </summary>
        public decimal DebitAmount { get; set; }
        
        /// <summary>
        /// Credit amount for trial balance
        /// </summary>
        public decimal CreditAmount { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public TrialBalanceItem()
        {
            Balance = 0;
            DebitAmount = 0;
            CreditAmount = 0;
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Gets the formatted debit amount for display
        /// </summary>
        /// <returns>Formatted debit amount string</returns>
        public string GetFormattedDebitAmount()
        {
            return DebitAmount > 0 ? DebitAmount.ToString("N2") : "";
        }
        
        /// <summary>
        /// Gets the formatted credit amount for display
        /// </summary>
        /// <returns>Formatted credit amount string</returns>
        public string GetFormattedCreditAmount()
        {
            return CreditAmount > 0 ? CreditAmount.ToString("N2") : "";
        }
        
        /// <summary>
        /// Gets the formatted balance for display
        /// </summary>
        /// <returns>Formatted balance string</returns>
        public string GetFormattedBalance()
        {
            return Balance.ToString("N2");
        }
        
        /// <summary>
        /// Checks if this item has a debit balance
        /// </summary>
        /// <returns>True if has debit balance</returns>
        public bool HasDebitBalance()
        {
            return DebitAmount > 0;
        }
        
        /// <summary>
        /// Checks if this item has a credit balance
        /// </summary>
        /// <returns>True if has credit balance</returns>
        public bool HasCreditBalance()
        {
            return CreditAmount > 0;
        }
        
        #endregion
    }
}
