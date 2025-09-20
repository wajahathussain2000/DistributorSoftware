using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IPaymentVoucherRepository
    {
        int CreatePaymentVoucher(PaymentVoucher voucher);
        bool UpdatePaymentVoucher(PaymentVoucher voucher);
        bool DeletePaymentVoucher(int voucherId);
        PaymentVoucher GetPaymentVoucherById(int voucherId);
        PaymentVoucher GetPaymentVoucherByNumber(string voucherNumber);
        List<PaymentVoucher> GetAllPaymentVouchers();
        List<PaymentVoucher> GetPaymentVouchersByDateRange(DateTime startDate, DateTime endDate);
        List<PaymentVoucher> GetPaymentVouchersByAccount(int accountId);
        List<PaymentVoucher> GetPaymentVouchersByPaymentMode(string paymentMode);
        List<PaymentVoucher> GetPaymentVouchersByStatus(string status);
        string GeneratePaymentVoucherNumber();
        decimal GetTotalPaymentsByAccount(int accountId, DateTime startDate, DateTime endDate);
        decimal GetTotalPaymentsByPaymentMode(string paymentMode, DateTime startDate, DateTime endDate);
    }
}

