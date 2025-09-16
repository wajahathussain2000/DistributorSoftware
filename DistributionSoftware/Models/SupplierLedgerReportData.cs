using System;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents supplier ledger report data for displaying transaction history and balances
    /// </summary>
    public class SupplierLedgerReportData
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
        /// Type of transaction (Purchase, Payment, Return)
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
        /// Debit amount (amount owed to supplier)
        /// </summary>
        public decimal DebitAmount { get; set; }
        
        /// <summary>
        /// Credit amount (amount paid to supplier)
        /// </summary>
        public decimal CreditAmount { get; set; }
        
        /// <summary>
        /// Running balance after this transaction
        /// </summary>
        public decimal RunningBalance { get; set; }
        
        #endregion
        
        #region Supplier Properties
        
        /// <summary>
        /// Unique identifier for the supplier
        /// </summary>
        public int SupplierId { get; set; }
        
        /// <summary>
        /// Supplier's unique code
        /// </summary>
        public string SupplierCode { get; set; }
        
        /// <summary>
        /// Name of the supplier
        /// </summary>
        public string SupplierName { get; set; }
        
        /// <summary>
        /// Contact person at the supplier
        /// </summary>
        public string ContactPerson { get; set; }
        
        /// <summary>
        /// Supplier's phone number
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Supplier's email address
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Supplier's address
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Supplier's city
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Supplier's state/province
        /// </summary>
        public string State { get; set; }
        
        /// <summary>
        /// Supplier's country
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// Supplier's GST number
        /// </summary>
        public string GST { get; set; }
        
        /// <summary>
        /// Payment terms in days
        /// </summary>
        public int? PaymentTermsDays { get; set; }
        
        #endregion
        
        #region Summary Properties (for report totals)
        
        /// <summary>
        /// Total number of transactions in the report period
        /// </summary>
        public int TotalTransactions { get; set; }
        
        /// <summary>
        /// Total debit amount for the period
        /// </summary>
        public decimal TotalDebits { get; set; }
        
        /// <summary>
        /// Total credit amount for the period
        /// </summary>
        public decimal TotalCredits { get; set; }
        
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
        public SupplierLedgerReportData()
        {
            TransactionDate = DateTime.Now;
            DebitAmount = 0;
            CreditAmount = 0;
            RunningBalance = 0;
            TotalDebits = 0;
            TotalCredits = 0;
            OpeningBalance = 0;
            ClosingBalance = 0;
        }
        
        #endregion
        
        #region Helper Properties
        
        /// <summary>
        /// Gets the net amount for this transaction (Debit - Credit)
        /// </summary>
        public decimal NetAmount => DebitAmount - CreditAmount;
        
        /// <summary>
        /// Gets formatted supplier display name
        /// </summary>
        public string SupplierDisplayName => $"{SupplierCode} - {SupplierName}";
        
        /// <summary>
        /// Gets formatted address
        /// </summary>
        public string FormattedAddress => $"{Address}, {City}, {State}, {Country}";
        
        #endregion
    }
}
