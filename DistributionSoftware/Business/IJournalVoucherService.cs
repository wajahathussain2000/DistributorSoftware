using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    /// <summary>
    /// Interface for Journal Voucher business logic operations
    /// </summary>
    public interface IJournalVoucherService
    {
        #region Basic CRUD Operations
        
        /// <summary>
        /// Creates a new journal voucher
        /// </summary>
        /// <param name="voucher">Journal voucher to create</param>
        /// <returns>ID of the created journal voucher</returns>
        int CreateJournalVoucher(JournalVoucher voucher);
        
        /// <summary>
        /// Updates an existing journal voucher
        /// </summary>
        /// <param name="voucher">Journal voucher to update</param>
        /// <returns>True if successful, false otherwise</returns>
        bool UpdateJournalVoucher(JournalVoucher voucher);
        
        /// <summary>
        /// Deletes a journal voucher
        /// </summary>
        /// <param name="voucherId">ID of the journal voucher to delete</param>
        /// <returns>True if successful, false otherwise</returns>
        bool DeleteJournalVoucher(int voucherId);
        
        /// <summary>
        /// Gets a journal voucher by ID
        /// </summary>
        /// <param name="voucherId">ID of the journal voucher</param>
        /// <returns>Journal voucher or null if not found</returns>
        JournalVoucher GetJournalVoucherById(int voucherId);
        
        /// <summary>
        /// Gets a journal voucher by voucher number
        /// </summary>
        /// <param name="voucherNumber">Voucher number</param>
        /// <returns>Journal voucher or null if not found</returns>
        JournalVoucher GetJournalVoucherByNumber(string voucherNumber);
        
        /// <summary>
        /// Gets all journal vouchers
        /// </summary>
        /// <returns>List of all journal vouchers</returns>
        List<JournalVoucher> GetAllJournalVouchers();
        
        /// <summary>
        /// Gets journal vouchers by date range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>List of journal vouchers in the date range</returns>
        List<JournalVoucher> GetJournalVouchersByDateRange(DateTime startDate, DateTime endDate);
        
        #endregion
        
        #region Business Logic Operations
        
        /// <summary>
        /// Generates a unique journal voucher number
        /// </summary>
        /// <returns>Unique journal voucher number</returns>
        string GenerateJournalVoucherNumber();
        
        /// <summary>
        /// Validates a journal voucher
        /// </summary>
        /// <param name="voucher">Journal voucher to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        bool ValidateJournalVoucher(JournalVoucher voucher);
        
        /// <summary>
        /// Gets validation errors for a journal voucher
        /// </summary>
        /// <param name="voucher">Journal voucher to validate</param>
        /// <returns>List of validation errors</returns>
        List<string> GetValidationErrors(JournalVoucher voucher);
        
        #endregion
        
        #region Sales Integration
        
        /// <summary>
        /// Creates a journal voucher for a sales invoice
        /// </summary>
        /// <param name="salesInvoice">Sales invoice to create journal voucher for</param>
        /// <returns>ID of the created journal voucher</returns>
        int CreateSalesInvoiceJournalVoucher(SalesInvoice salesInvoice);
        
        /// <summary>
        /// Creates a journal voucher for a sales payment
        /// </summary>
        /// <param name="salesPayment">Sales payment to create journal voucher for</param>
        /// <returns>ID of the created journal voucher</returns>
        int CreateSalesPaymentJournalVoucher(SalesPayment salesPayment);
        
        /// <summary>
        /// Creates a journal voucher for a customer receipt
        /// </summary>
        /// <param name="customerReceipt">Customer receipt to create journal voucher for</param>
        /// <returns>ID of the created journal voucher</returns>
        int CreateCustomerReceiptJournalVoucher(CustomerReceipt customerReceipt);

        /// <summary>
        /// Creates a journal voucher for a purchase return
        /// </summary>
        /// <param name="purchaseReturn">Purchase return to create journal voucher for</param>
        /// <returns>ID of the created journal voucher</returns>
        int CreatePurchaseReturnJournalVoucher(PurchaseReturn purchaseReturn);

        /// <summary>
        /// Creates a journal voucher for a sales return
        /// </summary>
        /// <param name="salesReturn">Sales return to create journal voucher for</param>
        /// <returns>ID of the created journal voucher</returns>
        int CreateSalesReturnJournalVoucher(SalesReturn salesReturn);

        /// <summary>
        /// Creates a journal voucher for an expense
        /// </summary>
        /// <param name="expense">Expense to create journal voucher for</param>
        /// <returns>ID of the created journal voucher</returns>
        int CreateExpenseJournalVoucher(Expense expense);

        /// <summary>
        /// Creates a journal voucher for a stock movement
        /// </summary>
        /// <param name="stockMovement">Stock movement to create journal voucher for</param>
        /// <returns>ID of the created journal voucher</returns>
        int CreateStockMovementJournalVoucher(StockMovement stockMovement);
        
        #endregion
        
        #region Account Integration
        
        /// <summary>
        /// Gets the default sales account
        /// </summary>
        /// <returns>Chart of account for sales</returns>
        ChartOfAccount GetDefaultSalesAccount();
        
        /// <summary>
        /// Gets the default cash account
        /// </summary>
        /// <returns>Chart of account for cash</returns>
        ChartOfAccount GetDefaultCashAccount();
        
        /// <summary>
        /// Gets the default accounts receivable account
        /// </summary>
        /// <returns>Chart of account for accounts receivable</returns>
        ChartOfAccount GetDefaultReceivableAccount();
        
        /// <summary>
        /// Gets the default tax account
        /// </summary>
        /// <returns>Chart of account for tax</returns>
        ChartOfAccount GetDefaultTaxAccount();
        
        /// <summary>
        /// Gets account by payment mode
        /// </summary>
        /// <param name="paymentMode">Payment mode (CASH, CARD, BANK_TRANSFER, etc.)</param>
        /// <returns>Appropriate chart of account</returns>
        ChartOfAccount GetAccountByPaymentMode(string paymentMode);
        
        #endregion
        
        #region Reports
        
        /// <summary>
        /// Gets journal voucher details for a specific account
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>List of journal voucher details for the account</returns>
        List<JournalVoucherDetail> GetAccountJournalVoucherDetails(int accountId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Gets account balance for a specific account
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <param name="asOfDate">Balance as of this date</param>
        /// <returns>Account balance</returns>
        decimal GetAccountBalance(int accountId, DateTime asOfDate);
        
        #endregion
    }
}
