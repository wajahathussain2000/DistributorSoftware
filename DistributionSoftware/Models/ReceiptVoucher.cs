using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents a receipt voucher for recording standalone receipts
    /// </summary>
    public class ReceiptVoucher
    {
        #region Primary Properties
        
        /// <summary>
        /// Unique identifier for the receipt voucher
        /// </summary>
        public int ReceiptVoucherId { get; set; }
        
        /// <summary>
        /// Receipt voucher number (e.g., RV-2024-001)
        /// </summary>
        public string VoucherNumber { get; set; }
        
        /// <summary>
        /// Date of the receipt voucher
        /// </summary>
        public DateTime VoucherDate { get; set; }
        
        /// <summary>
        /// Reference to the source transaction
        /// </summary>
        public string Reference { get; set; }
        
        /// <summary>
        /// Description of the receipt voucher
        /// </summary>
        public string Narration { get; set; }
        
        /// <summary>
        /// Receipt mode (CASH, CARD, CHEQUE, BANK_TRANSFER, etc.)
        /// </summary>
        public string ReceiptMode { get; set; }
        
        /// <summary>
        /// Receipt amount
        /// </summary>
        public decimal Amount { get; set; }
        
        /// <summary>
        /// Account ID for the receipt (credit account)
        /// </summary>
        public int AccountId { get; set; }
        
        /// <summary>
        /// Bank account ID for bank-related receipts
        /// </summary>
        public int? BankAccountId { get; set; }
        
        #endregion
        
        #region Receipt Details
        
        /// <summary>
        /// Card number for card receipts
        /// </summary>
        public string CardNumber { get; set; }
        
        /// <summary>
        /// Card type (VISA, MASTERCARD, etc.)
        /// </summary>
        public string CardType { get; set; }
        
        /// <summary>
        /// Transaction ID for electronic receipts
        /// </summary>
        public string TransactionId { get; set; }
        
        /// <summary>
        /// Bank name for bank transfers
        /// </summary>
        public string BankName { get; set; }
        
        /// <summary>
        /// Cheque number for cheque receipts
        /// </summary>
        public string ChequeNumber { get; set; }
        
        /// <summary>
        /// Cheque date for cheque receipts
        /// </summary>
        public DateTime? ChequeDate { get; set; }
        
        /// <summary>
        /// Mobile number for mobile receipts
        /// </summary>
        public string MobileNumber { get; set; }
        
        /// <summary>
        /// Receipt reference number
        /// </summary>
        public string ReceiptReference { get; set; }
        
        #endregion
        
        #region Status & Remarks
        
        /// <summary>
        /// Receipt voucher status (ACTIVE, CANCELLED, etc.)
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
        public ReceiptVoucher()
        {
            VoucherDate = DateTime.Now;
            CreatedDate = DateTime.Now;
            Status = "ACTIVE";
            ReceiptMode = "CASH";
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Validates the receipt voucher
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(VoucherNumber)) return false;
            if (string.IsNullOrWhiteSpace(Narration)) return false;
            if (Amount <= 0) return false;
            if (AccountId <= 0) return false;
            if (string.IsNullOrWhiteSpace(ReceiptMode)) return false;
            
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
            
            if (string.IsNullOrWhiteSpace(ReceiptMode))
                errors.Add("Receipt Mode is required");
            
            return errors;
        }
        
        /// <summary>
        /// Gets receipt mode description
        /// </summary>
        /// <returns>Receipt mode description</returns>
        public string GetReceiptModeDescription()
        {
            switch (ReceiptMode?.ToUpper())
            {
                case "CASH": return "Cash";
                case "CARD": return "Card";
                case "CHEQUE": return "Cheque";
                case "BANK_TRANSFER": return "Bank Transfer";
                case "EASYPAISA": return "Easypaisa";
                case "JAZZCASH": return "Jazzcash";
                default: return ReceiptMode;
            }
        }
        
        #endregion
    }
}

