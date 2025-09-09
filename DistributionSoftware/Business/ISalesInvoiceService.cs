using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface ISalesInvoiceService
    {
        // Invoice CRUD
        int CreateSalesInvoice(SalesInvoice invoice);
        bool UpdateSalesInvoice(SalesInvoice invoice);
        bool DeleteSalesInvoice(int invoiceId);
        SalesInvoice GetSalesInvoiceById(int invoiceId);
        SalesInvoice GetSalesInvoiceByNumber(string invoiceNumber);
        List<SalesInvoice> GetAllSalesInvoices();
        List<SalesInvoice> GetSalesInvoicesByDateRange(DateTime startDate, DateTime endDate);
        List<SalesInvoice> GetSalesInvoicesByCustomer(int customerId);
        List<SalesInvoice> GetSalesInvoicesByStatus(string status);
        
        // Invoice Details
        bool CreateSalesInvoiceDetail(SalesInvoiceDetail detail);
        bool UpdateSalesInvoiceDetail(SalesInvoiceDetail detail);
        bool DeleteSalesInvoiceDetail(int detailId);
        List<SalesInvoiceDetail> GetSalesInvoiceDetails(int invoiceId);
        
        // Payments
        bool CreateSalesPayment(SalesPayment payment);
        bool UpdateSalesPayment(SalesPayment payment);
        bool DeleteSalesPayment(int paymentId);
        List<SalesPayment> GetSalesPayments(int invoiceId);
        
        // Business Logic
        string GenerateInvoiceNumber();
        bool ProcessPayment(SalesInvoice invoice, SalesPayment payment);
        bool CalculateInvoiceTotals(SalesInvoice invoice);
        bool ValidateInvoice(SalesInvoice invoice);
        bool ReserveStockForInvoice(SalesInvoice invoice);
        bool ReleaseStockForInvoice(SalesInvoice invoice);
        bool ConfirmStockForInvoice(SalesInvoice invoice);
        
        // Barcode and Printing
        bool GenerateInvoiceBarcode(SalesInvoice invoice);
        bool UpdatePrintStatus(int invoiceId, bool printStatus);
        string GenerateThermalReceipt(SalesInvoice invoice);
        
        // Reports
        List<SalesInvoice> GetSalesReport(DateTime startDate, DateTime endDate, int? customerId, string status);
        decimal GetTotalSales(DateTime startDate, DateTime endDate);
        int GetInvoiceCount(DateTime startDate, DateTime endDate);
        
        // POS Specific
        bool ProcessQuickSale(List<SalesInvoiceDetail> items, string paymentMode, decimal paidAmount);
        bool HoldInvoice(SalesInvoice invoice);
        bool RetrieveHeldInvoice(int invoiceId);
        List<SalesInvoice> GetHeldInvoices();
    }
}
