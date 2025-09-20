using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface IPurchaseInvoiceRepository
    {
        int CreatePurchaseInvoice(PurchaseInvoice invoice);
        bool UpdatePurchaseInvoice(PurchaseInvoice invoice);
        bool DeletePurchaseInvoice(int invoiceId);
        PurchaseInvoice GetPurchaseInvoiceById(int invoiceId);
        PurchaseInvoice GetPurchaseInvoiceByNumber(string invoiceNumber);
        List<PurchaseInvoice> GetAllPurchaseInvoices();
        List<PurchaseInvoice> GetPurchaseInvoicesByDateRange(DateTime startDate, DateTime endDate);
        List<PurchaseInvoice> GetPurchaseInvoicesBySupplier(int supplierId);
        string GeneratePurchaseInvoiceNumber();
    }
}
