using System;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents customer ledger report data for displaying transaction history and balances
    /// </summary>
    public class CustomerLedgerReportData
    {
        #region Transaction Properties
        
        /// <summary>
        /// Unique identifier for the transaction
        /// </summary>
        public long TransactionId { get; set; }
        
        /// <summary>
        /// Date when the transaction occurred
        /// </summary>
        public DateTime TransactionDate { get; set; }
        
        /// <summary>
        /// Type of transaction (Sales, Payment, Return)
        /// </summary>
        public string TransactionType { get; set; }
        
        /// <summary>
        /// Human-readable description of transaction type
        /// </summary>
        public string TransactionTypeDescription { get; set; }
        
        /// <summary>
        /// Detailed description of the transaction
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Reference number for the transaction (Invoice No, Payment No, etc.)
        /// </summary>
        public string ReferenceNumber { get; set; }
        
        /// <summary>
        /// Debit amount (amount received from customer)
        /// </summary>
        public decimal DebitAmount { get; set; }
        
        /// <summary>
        /// Credit amount (amount customer owes us)
        /// </summary>
        public decimal CreditAmount { get; set; }
        
        /// <summary>
        /// Running balance after this transaction
        /// </summary>
        public decimal RunningBalance { get; set; }
        
        #endregion
        
        #region Customer Properties
        
        /// <summary>
        /// Unique identifier for the customer
        /// </summary>
        public int CustomerId { get; set; }
        
        /// <summary>
        /// Customer's unique code
        /// </summary>
        public string CustomerCode { get; set; }
        
        /// <summary>
        /// Name of the customer
        /// </summary>
        public string CustomerName { get; set; }
        
        /// <summary>
        /// Contact person at the customer
        /// </summary>
        public string ContactName { get; set; }
        
        /// <summary>
        /// Customer's phone number
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Customer's email address
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Customer's address
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Customer's city
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Customer's state/province
        /// </summary>
        public string State { get; set; }
        
        /// <summary>
        /// Customer's country
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Customer's GST number
        /// </summary>
        public string GSTNumber { get; set; }
        
        /// <summary>
        /// Customer's credit limit
        /// </summary>
        public decimal CreditLimit { get; set; }
        
        /// <summary>
        /// Customer's outstanding balance
        /// </summary>
        public decimal OutstandingBalance { get; set; }
        
        /// <summary>
        /// Payment terms
        /// </summary>
        public string PaymentTerms { get; set; }
        
        #endregion
        
        #region Summary Properties (for report totals)
        
        /// <summary>
        /// Total number of transactions in the report period
        /// </summary>
        public int TotalTransactions { get; set; }
        
        /// <summary>
        /// Total credit amount for the period (sales)
        /// </summary>
        public decimal TotalCredits { get; set; }
        
        /// <summary>
        /// Total debit amount for the period (payments)
        /// </summary>
        public decimal TotalDebits { get; set; }
        
        /// <summary>
        /// Total return amount for the period
        /// </summary>
        public decimal TotalReturns { get; set; }
        
        /// <summary>
        /// Opening balance at the start of the period
        /// </summary>
        public decimal OpeningBalance { get; set; }
        
        /// <summary>
        /// Closing balance at the end of the period
        /// </summary>
        public decimal ClosingBalance { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerLedgerReportData()
        {
            TransactionDate = DateTime.Now;
            DebitAmount = 0;
            CreditAmount = 0;
            RunningBalance = 0;
            TotalCredits = 0;
            TotalDebits = 0;
            TotalReturns = 0;
            OpeningBalance = 0;
            ClosingBalance = 0;
            CreditLimit = 0;
            OutstandingBalance = 0;
        }
        
        #endregion
        
        #region Helper Properties
        
        /// <summary>
        /// Gets the net amount for this transaction (Credit - Debit)
        /// </summary>
        public decimal NetAmount => CreditAmount - DebitAmount;
        
        /// <summary>
        /// Gets formatted customer display name
        /// </summary>
        public string CustomerDisplayName => $"{CustomerCode} - {CustomerName}";
        
        /// <summary>
        /// Gets formatted address
        /// </summary>
        public string FormattedAddress => $"{Address}, {City}, {State}, {Country}";
        
        /// <summary>
        /// Gets the balance status (positive = customer owes us, negative = we owe customer)
        /// </summary>
        public string BalanceStatus => RunningBalance > 0 ? "Outstanding" : RunningBalance < 0 ? "Credit" : "Settled";
        
        #endregion
    }
}
