using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    /// <summary>
    /// Interface for Receipt Voucher business logic operations
    /// </summary>
    public interface IReceiptVoucherService
    {
        #region Basic CRUD Operations
        
        /// <summary>
        /// Creates a new receipt voucher
        /// </summary>
        /// <param name="voucher">Receipt voucher to create</param>
        /// <returns>ID of the created receipt voucher</returns>
        int CreateReceiptVoucher(ReceiptVoucher voucher);
        
        /// <summary>
        /// Updates an existing receipt voucher
        /// </summary>
        /// <param name="voucher">Receipt voucher to update</param>
        /// <returns>True if successful, false otherwise</returns>
        bool UpdateReceiptVoucher(ReceiptVoucher voucher);
        
        /// <summary>
        /// Deletes a receipt voucher
        /// </summary>
        /// <param name="voucherId">ID of the receipt voucher to delete</param>
        /// <returns>True if successful, false otherwise</returns>
        bool DeleteReceiptVoucher(int voucherId);
        
        /// <summary>
        /// Gets a receipt voucher by ID
        /// </summary>
        /// <param name="voucherId">ID of the receipt voucher</param>
        /// <returns>Receipt voucher or null if not found</returns>
        ReceiptVoucher GetReceiptVoucherById(int voucherId);
        
        /// <summary>
        /// Gets a receipt voucher by voucher number
        /// </summary>
        /// <param name="voucherNumber">Voucher number</param>
        /// <returns>Receipt voucher or null if not found</returns>
        ReceiptVoucher GetReceiptVoucherByNumber(string voucherNumber);
        
        /// <summary>
        /// Gets all receipt vouchers
        /// </summary>
        /// <returns>List of all receipt vouchers</returns>
        List<ReceiptVoucher> GetAllReceiptVouchers();
        
        /// <summary>
        /// Gets receipt vouchers by date range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>List of receipt vouchers in the date range</returns>
        List<ReceiptVoucher> GetReceiptVouchersByDateRange(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Gets receipt vouchers by account
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>List of receipt vouchers for the account</returns>
        List<ReceiptVoucher> GetReceiptVouchersByAccount(int accountId);
        
        /// <summary>
        /// Gets receipt vouchers by receipt mode
        /// </summary>
        /// <param name="receiptMode">Receipt mode</param>
        /// <returns>List of receipt vouchers with the receipt mode</returns>
        List<ReceiptVoucher> GetReceiptVouchersByReceiptMode(string receiptMode);
        
        /// <summary>
        /// Gets receipt vouchers by status
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List of receipt vouchers with the status</returns>
        List<ReceiptVoucher> GetReceiptVouchersByStatus(string status);
        
        #endregion
        
        #region Business Logic Operations
        
        /// <summary>
        /// Generates a unique receipt voucher number
        /// </summary>
        /// <returns>Unique receipt voucher number</returns>
        string GenerateReceiptVoucherNumber();
        
        /// <summary>
        /// Validates a receipt voucher
        /// </summary>
        /// <param name="voucher">Receipt voucher to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        bool ValidateReceiptVoucher(ReceiptVoucher voucher);
        
        /// <summary>
        /// Gets validation errors for a receipt voucher
        /// </summary>
        /// <param name="voucher">Receipt voucher to validate</param>
        /// <returns>List of validation errors</returns>
        List<string> GetValidationErrors(ReceiptVoucher voucher);
        
        /// <summary>
        /// Processes receipt voucher and creates journal entries
        /// </summary>
        /// <param name="voucher">Receipt voucher to process</param>
        /// <returns>True if successful, false otherwise</returns>
        bool ProcessReceiptVoucher(ReceiptVoucher voucher);
        
        #endregion
        
        #region Reports & Analytics
        
        /// <summary>
        /// Gets total receipts by account for a date range
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Total receipt amount</returns>
        decimal GetTotalReceiptsByAccount(int accountId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Gets total receipts by receipt mode for a date range
        /// </summary>
        /// <param name="receiptMode">Receipt mode</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Total receipt amount</returns>
        decimal GetTotalReceiptsByReceiptMode(string receiptMode, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Gets receipt summary for a date range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Receipt summary data</returns>
        Dictionary<string, decimal> GetReceiptSummary(DateTime startDate, DateTime endDate);
        
        #endregion
    }
}

