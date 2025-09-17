using System;

namespace DistributionSoftware.Models
{
    public class GeneralLedgerReportData
    {
        public int DetailId { get; set; }
        public int VoucherId { get; set; }
        public string VoucherNumber { get; set; }
        public DateTime VoucherDate { get; set; }
        public string Reference { get; set; }
        public string VoucherNarration { get; set; }
        public decimal VoucherTotalDebit { get; set; }
        public decimal VoucherTotalCredit { get; set; }
        public DateTime VoucherCreatedDate { get; set; }
        public int? VoucherCreatedBy { get; set; }
        public string VoucherCreatedByName { get; set; }
        public DateTime? VoucherModifiedDate { get; set; }
        public int? VoucherModifiedBy { get; set; }
        public string VoucherModifiedByName { get; set; }
        public int? BankAccountId { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public int AccountId { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string AccountCategory { get; set; }
        public int? AccountLevel { get; set; }
        public string NormalBalance { get; set; }
        public string AccountDescription { get; set; }
        public int? ParentAccountId { get; set; }
        public string ParentAccountName { get; set; }
        public string ParentAccountCode { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string DetailNarration { get; set; }
        public decimal RunningBalance { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public string TransactionType { get; set; }
        public string NormalBalanceType { get; set; }
        public int DaysSinceTransaction { get; set; }
        public decimal TransactionAmount { get; set; }
        public string AccountStatus { get; set; }
    }
}
