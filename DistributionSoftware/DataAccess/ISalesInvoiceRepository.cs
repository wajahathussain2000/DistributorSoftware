using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface ISalesInvoiceRepository
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
        
        // Invoice Number Generation
        string GenerateInvoiceNumber();
        
        // Barcode
        bool UpdateInvoiceBarcode(int invoiceId, string barcode, byte[] barcodeImage);
        
        // Print Status
        bool UpdatePrintStatus(int invoiceId, bool printStatus, DateTime? printDate);
        
        // Stock Reservation
        bool ReserveStock(int productId, decimal quantity);
        bool ReleaseStock(int productId, decimal quantity);
        bool ConfirmStock(int productId, decimal quantity);
        bool ReduceStockQuantity(int productId, decimal quantity);
        
        // Reports
        List<SalesInvoice> GetSalesReport(DateTime startDate, DateTime endDate, int? customerId, string status);
        decimal GetTotalSales(DateTime startDate, DateTime endDate);
        int GetInvoiceCount(DateTime startDate, DateTime endDate);
    }
}
