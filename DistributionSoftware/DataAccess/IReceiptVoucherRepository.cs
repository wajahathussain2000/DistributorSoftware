using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IReceiptVoucherRepository
    {
        int CreateReceiptVoucher(ReceiptVoucher voucher);
        bool UpdateReceiptVoucher(ReceiptVoucher voucher);
        bool DeleteReceiptVoucher(int voucherId);
        ReceiptVoucher GetReceiptVoucherById(int voucherId);
        ReceiptVoucher GetReceiptVoucherByNumber(string voucherNumber);
        List<ReceiptVoucher> GetAllReceiptVouchers();
        List<ReceiptVoucher> GetReceiptVouchersByDateRange(DateTime startDate, DateTime endDate);
        List<ReceiptVoucher> GetReceiptVouchersByAccount(int accountId);
        List<ReceiptVoucher> GetReceiptVouchersByReceiptMode(string receiptMode);
        List<ReceiptVoucher> GetReceiptVouchersByStatus(string status);
        string GenerateReceiptVoucherNumber();
        decimal GetTotalReceiptsByAccount(int accountId, DateTime startDate, DateTime endDate);
        decimal GetTotalReceiptsByReceiptMode(string receiptMode, DateTime startDate, DateTime endDate);
    }
}

