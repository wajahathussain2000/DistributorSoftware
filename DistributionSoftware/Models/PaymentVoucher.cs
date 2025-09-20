using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents a payment voucher for recording standalone payments
    /// </summary>
    public class PaymentVoucher
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the payment voucher
        /// </summary>
        public int PaymentVoucherId { get; set; }
        
        /// <summary>
        /// Payment voucher number (e.g., PV-2024-001)
        /// </summary>
        public string VoucherNumber { get; set; }
        
        /// <summary>
        /// Date of the payment voucher
        /// </summary>
        public DateTime VoucherDate { get; set; }
        
        /// <summary>
        /// Reference to the source transaction
        /// </summary>
        public string Reference { get; set; }
        
        /// <summary>
        /// Description of the payment voucher
        /// </summary>
        public string Narration { get; set; }
        
        /// <summary>
        /// Payment mode (CASH, CARD, CHEQUE, BANK_TRANSFER, etc.)
        /// </summary>
        public string PaymentMode { get; set; }
        
        /// <summary>
        /// Payment amount
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Account ID for the payment (debit account)
        /// </summary>
        public int AccountId { get; set; }
        
        /// <summary>
        /// Bank account ID for bank-related payments
        /// </summary>
        public int? BankAccountId { get; set; }
        
        #endregion
        
        #region Payment Details
        
        /// <summary>
        /// Card number for card payments
        /// </summary>
        public string CardNumber { get; set; }
        
        /// <summary>
        /// Card type (VISA, MASTERCARD, etc.)
        /// </summary>
        public string CardType { get; set; }
        
        /// <summary>
        /// Transaction ID for electronic payments
        /// </summary>
        public string TransactionId { get; set; }
        
        /// <summary>
        /// Bank name for bank transfers
        /// </summary>
        public string BankName { get; set; }
        
        /// <summary>
        /// Cheque number for cheque payments
        /// </summary>
        public string ChequeNumber { get; set; }
        
        /// <summary>
        /// Cheque date for cheque payments
        /// </summary>
        public DateTime? ChequeDate { get; set; }
        
        /// <summary>
        /// Mobile number for mobile payments
        /// </summary>
        public string MobileNumber { get; set; }
        
        /// <summary>
        /// Payment reference number
        /// </summary>
        public string PaymentReference { get; set; }
        
        #endregion
        
        #region Status & Remarks
        
        /// <summary>
        /// Payment voucher status (ACTIVE, CANCELLED, etc.)
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Additional remarks
        /// </summary>
        public string Remarks { get; set; }
        
        #endregion
        
        #region Audit Fields
        
        /// <summary>
        /// Date when the voucher was created
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// User who created the voucher
        /// </summary>
        public int CreatedBy { get; set; }
        
        /// <summary>
        /// Name of the user who created the voucher
        /// </summary>
        public string CreatedByName { get; set; }
        
        /// <summary>
        /// Date when the voucher was last modified
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        
        /// <summary>
        /// User who last modified the voucher
        /// </summary>
        public int? ModifiedBy { get; set; }
        
        /// <summary>
        /// Name of the user who last modified the voucher
        /// </summary>
        public string ModifiedByName { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public PaymentVoucher()
        {
            VoucherDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            Status = "ACTIVE";
            PaymentMode = "CASH";
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the payment voucher
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(VoucherNumber)) return false;
            if (string.IsNullOrWhiteSpace(Narration)) return false;
            if (Amount <= 0) return false;
            if (AccountId <= 0) return false;
            if (string.IsNullOrWhiteSpace(PaymentMode)) return false;
            
            return true;
        }
        
        /// <summary>
        /// Gets validation errors
        /// </summary>
        /// <returns>List of validation errors</returns>
        public List<string> GetValidationErrors()
        {
            var errors = new List<string>();
            
            if (string.IsNullOrWhiteSpace(VoucherNumber))
                errors.Add("Voucher Number is required");
            
            if (string.IsNullOrWhiteSpace(Narration))
                errors.Add("Narration is required");
            
            if (Amount <= 0)
                errors.Add("Amount must be greater than 0");
            
            if (AccountId <= 0)
                errors.Add("Account is required");
            
            if (string.IsNullOrWhiteSpace(PaymentMode))
                errors.Add("Payment Mode is required");
            
            return errors;
        }
        
        /// <summary>
        /// Gets payment mode description
        /// </summary>
        /// <returns>Payment mode description</returns>
        public string GetPaymentModeDescription()
        {
            switch (PaymentMode?.ToUpper())
            {
                case "CASH": return "Cash";
                case "CARD": return "Card";
                case "CHEQUE": return "Cheque";
                case "BANK_TRANSFER": return "Bank Transfer";
                case "EASYPAISA": return "Easypaisa";
                case "JAZZCASH": return "Jazzcash";
                default: return PaymentMode;
            }
        }
        
        #endregion
    }
}

