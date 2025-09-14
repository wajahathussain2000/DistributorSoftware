using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IJournalVoucherRepository
    {
        int CreateJournalVoucher(JournalVoucher voucher);
        bool UpdateJournalVoucher(JournalVoucher voucher);
        bool DeleteJournalVoucher(int voucherId);
        JournalVoucher GetJournalVoucherById(int voucherId);
        JournalVoucher GetJournalVoucherByNumber(string voucherNumber);
        List<JournalVoucher> GetAllJournalVouchers();
        List<JournalVoucher> GetJournalVouchersByDateRange(DateTime startDate, DateTime endDate);
        List<JournalVoucher> GetJournalVouchersByAccount(int accountId);
        List<JournalVoucherDetail> GetJournalVoucherDetails(int voucherId);
        List<JournalVoucherDetail> GetAccountJournalVoucherDetails(int accountId, DateTime startDate, DateTime endDate);
        decimal GetAccountBalance(int accountId, DateTime asOfDate);
        string GenerateJournalVoucherNumber();
        bool ValidateJournalVoucherBalanced(JournalVoucher voucher);
    }
}
