using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface IPurchaseInvoiceService
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
        bool ValidatePurchaseInvoice(PurchaseInvoice invoice);
        List<string> GetValidationErrors(PurchaseInvoice invoice);
    }
}