using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    /// <summary>
    /// Interface for Payment Voucher business logic operations
    /// </summary>
    public interface IPaymentVoucherService
    {
        #region Basic CRUD Operations
        
        /// <summary>
        /// Creates a new payment voucher
        /// </summary>
        /// <param name="voucher">Payment voucher to create</param>
        /// <returns>ID of the created payment voucher</returns>
        int CreatePaymentVoucher(PaymentVoucher voucher);
        
        /// <summary>
        /// Updates an existing payment voucher
        /// </summary>
        /// <param name="voucher">Payment voucher to update</param>
        /// <returns>True if successful, false otherwise</returns>
        bool UpdatePaymentVoucher(PaymentVoucher voucher);
        
        /// <summary>
        /// Deletes a payment voucher
        /// </summary>
        /// <param name="voucherId">ID of the payment voucher to delete</param>
        /// <returns>True if successful, false otherwise</returns>
        bool DeletePaymentVoucher(int voucherId);
        
        /// <summary>
        /// Gets a payment voucher by ID
        /// </summary>
        /// <param name="voucherId">ID of the payment voucher</param>
        /// <returns>Payment voucher or null if not found</returns>
        PaymentVoucher GetPaymentVoucherById(int voucherId);
        
        /// <summary>
        /// Gets a payment voucher by voucher number
        /// </summary>
        /// <param name="voucherNumber">Voucher number</param>
        /// <returns>Payment voucher or null if not found</returns>
        PaymentVoucher GetPaymentVoucherByNumber(string voucherNumber);
        
        /// <summary>
        /// Gets all payment vouchers
        /// </summary>
        /// <returns>List of all payment vouchers</returns>
        List<PaymentVoucher> GetAllPaymentVouchers();
        
        /// <summary>
        /// Gets payment vouchers by date range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>List of payment vouchers in the date range</returns>
        List<PaymentVoucher> GetPaymentVouchersByDateRange(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Gets payment vouchers by account
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <returns>List of payment vouchers for the account</returns>
        List<PaymentVoucher> GetPaymentVouchersByAccount(int accountId);
        
        /// <summary>
        /// Gets payment vouchers by payment mode
        /// </summary>
        /// <param name="paymentMode">Payment mode</param>
        /// <returns>List of payment vouchers with the payment mode</returns>
        List<PaymentVoucher> GetPaymentVouchersByPaymentMode(string paymentMode);
        
        /// <summary>
        /// Gets payment vouchers by status
        /// </summary>
        /// <param name="status">Status</param>
        /// <returns>List of payment vouchers with the status</returns>
        List<PaymentVoucher> GetPaymentVouchersByStatus(string status);
        
        #endregion
        
        #region Business Logic Operations
        
        /// <summary>
        /// Generates a unique payment voucher number
        /// </summary>
        /// <returns>Unique payment voucher number</returns>
        string GeneratePaymentVoucherNumber();
        
        /// <summary>
        /// Validates a payment voucher
        /// </summary>
        /// <param name="voucher">Payment voucher to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        bool ValidatePaymentVoucher(PaymentVoucher voucher);
        
        /// <summary>
        /// Gets validation errors for a payment voucher
        /// </summary>
        /// <param name="voucher">Payment voucher to validate</param>
        /// <returns>List of validation errors</returns>
        List<string> GetValidationErrors(PaymentVoucher voucher);
        
        /// <summary>
        /// Processes payment voucher and creates journal entries
        /// </summary>
        /// <param name="voucher">Payment voucher to process</param>
        /// <returns>True if successful, false otherwise</returns>
        bool ProcessPaymentVoucher(PaymentVoucher voucher);
        
        #endregion
        
        #region Reports & Analytics
        
        /// <summary>
        /// Gets total payments by account for a date range
        /// </summary>
        /// <param name="accountId">Account ID</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Total payment amount</returns>
        decimal GetTotalPaymentsByAccount(int accountId, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Gets total payments by payment mode for a date range
        /// </summary>
        /// <param name="paymentMode">Payment mode</param>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Total payment amount</returns>
        decimal GetTotalPaymentsByPaymentMode(string paymentMode, DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Gets payment summary for a date range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Payment summary data</returns>
        Dictionary<string, decimal> GetPaymentSummary(DateTime startDate, DateTime endDate);
        
        #endregion
    }
}

