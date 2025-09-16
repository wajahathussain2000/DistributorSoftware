using System;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents customer balance report data for displaying customer balances, credit limits, and transaction summaries
    /// </summary>
    public class CustomerBalanceReportData
    {
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
        /// Customer's outstanding balance from master table
        /// </summary>
        public decimal OutstandingBalance { get; set; }
        
        /// <summary>
        /// Payment terms
        /// </summary>
        public string PaymentTerms { get; set; }
        
        #endregion
        
        #region Balance Properties
        
        /// <summary>
        /// Current balance calculated from all transactions
        /// </summary>
        public decimal CurrentBalance { get; set; }
        
        /// <summary>
        /// Opening balance at the start of the period
        /// </summary>
        public decimal OpeningBalance { get; set; }
        
        /// <summary>
        /// Closing balance at the end of the period
        /// </summary>
        public decimal ClosingBalance { get; set; }
        
        /// <summary>
        /// Credit utilization percentage (CurrentBalance / CreditLimit * 100)
        /// </summary>
        public decimal CreditUtilizationPercent { get; set; }
        
        /// <summary>
        /// Balance status (Outstanding, Credit, Settled)
        /// </summary>
        public string BalanceStatus { get; set; }
        
        /// <summary>
        /// Credit status (Over Limit, Near Limit, Within Limit)
        /// </summary>
        public string CreditStatus { get; set; }
        
        #endregion
        
        #region Period Transaction Properties
        
        /// <summary>
        /// Total sales amount for the period
        /// </summary>
        public decimal PeriodSales { get; set; }
        
        /// <summary>
        /// Total payments received for the period
        /// </summary>
        public decimal PeriodPayments { get; set; }
        
        /// <summary>
        /// Total returns for the period
        /// </summary>
        public decimal PeriodReturns { get; set; }
        
        /// <summary>
        /// Net period activity (Sales - Payments - Returns)
        /// </summary>
        public decimal NetPeriodActivity { get; set; }
        
        /// <summary>
        /// Number of transactions in the period
        /// </summary>
        public int PeriodTransactionCount { get; set; }
        
        /// <summary>
        /// Total number of transactions for the customer
        /// </summary>
        public int TotalTransactionCount { get; set; }
        
        #endregion
        
        #region Transaction Date Properties
        
        /// <summary>
        /// Date of the last transaction
        /// </summary>
        public DateTime? LastTransactionDate { get; set; }
        
        /// <summary>
        /// Date of the first transaction
        /// </summary>
        public DateTime? FirstTransactionDate { get; set; }
        
        #endregion
        
        #region Summary Properties (for report totals)
        
        /// <summary>
        /// Total number of customers in the report
        /// </summary>
        public int TotalCustomers { get; set; }
        
        /// <summary>
        /// Total outstanding balance across all customers
        /// </summary>
        public decimal TotalOutstandingBalance { get; set; }
        
        /// <summary>
        /// Total positive balance across all customers
        /// </summary>
        public decimal TotalPositiveBalance { get; set; }
        
        /// <summary>
        /// Total negative balance across all customers
        /// </summary>
        public decimal TotalNegativeBalance { get; set; }
        
        /// <summary>
        /// Number of customers over their credit limit
        /// </summary>
        public int CustomersOverLimit { get; set; }
        
        /// <summary>
        /// Number of customers near their credit limit (80%+)
        /// </summary>
        public int CustomersNearLimit { get; set; }
        
        /// <summary>
        /// Total sales amount for the period across all customers
        /// </summary>
        public decimal TotalPeriodSales { get; set; }
        
        /// <summary>
        /// Total payments received for the period across all customers
        /// </summary>
        public decimal TotalPeriodPayments { get; set; }
        
        /// <summary>
        /// Total returns for the period across all customers
        /// </summary>
        public decimal TotalPeriodReturns { get; set; }
        
        /// <summary>
        /// Total number of transactions in the period across all customers
        /// </summary>
        public int TotalPeriodTransactions { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomerBalanceReportData()
        {
            CurrentBalance = 0;
            OpeningBalance = 0;
            ClosingBalance = 0;
            CreditLimit = 0;
            OutstandingBalance = 0;
            PeriodSales = 0;
            PeriodPayments = 0;
            PeriodReturns = 0;
            NetPeriodActivity = 0;
            PeriodTransactionCount = 0;
            TotalTransactionCount = 0;
            CreditUtilizationPercent = 0;
            BalanceStatus = "Settled";
            CreditStatus = "Within Limit";
            TotalCustomers = 0;
            TotalOutstandingBalance = 0;
            TotalPositiveBalance = 0;
            TotalNegativeBalance = 0;
            CustomersOverLimit = 0;
            CustomersNearLimit = 0;
            TotalPeriodSales = 0;
            TotalPeriodPayments = 0;
            TotalPeriodReturns = 0;
            TotalPeriodTransactions = 0;
        }
        
        #endregion
        
        #region Helper Properties
        
        /// <summary>
        /// Gets formatted customer display name
        /// </summary>
        public string CustomerDisplayName => $"{CustomerCode} - {CustomerName}";
        
        /// <summary>
        /// Gets formatted address
        /// </summary>
        public string FormattedAddress => $"{Address}, {City}, {State}, {Country}";
        
        /// <summary>
        /// Gets formatted credit utilization percentage
        /// </summary>
        public string FormattedCreditUtilization => $"{CreditUtilizationPercent:F1}%";
        
        /// <summary>
        /// Gets the days since last transaction
        /// </summary>
        public int DaysSinceLastTransaction => LastTransactionDate.HasValue ? 
            (DateTime.Now - LastTransactionDate.Value).Days : -1;
        
        /// <summary>
        /// Gets the days since first transaction
        /// </summary>
        public int DaysSinceFirstTransaction => FirstTransactionDate.HasValue ? 
            (DateTime.Now - FirstTransactionDate.Value).Days : -1;
        
        /// <summary>
        /// Gets whether the customer is active (has transactions)
        /// </summary>
        public bool IsActiveCustomer => TotalTransactionCount > 0;
        
        /// <summary>
        /// Gets whether the customer has period activity
        /// </summary>
        public bool HasPeriodActivity => PeriodTransactionCount > 0;
        
        /// <summary>
        /// Gets the credit limit remaining
        /// </summary>
        public decimal CreditLimitRemaining => CreditLimit - CurrentBalance;
        
        /// <summary>
        /// Gets whether the customer is over credit limit
        /// </summary>
        public bool IsOverCreditLimit => CreditLimit > 0 && CurrentBalance > CreditLimit;
        
        /// <summary>
        /// Gets whether the customer is near credit limit (80%+)
        /// </summary>
        public bool IsNearCreditLimit => CreditLimit > 0 && CurrentBalance > CreditLimit * 0.8m;
        
        #endregion
    }
}
