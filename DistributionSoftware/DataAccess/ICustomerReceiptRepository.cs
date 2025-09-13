using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public interface ICustomerReceiptRepository
    {
        // Receipt CRUD
        int CreateCustomerReceipt(CustomerReceipt receipt);
        bool UpdateCustomerReceipt(CustomerReceipt receipt);
        bool DeleteCustomerReceipt(int receiptId);
        CustomerReceipt GetCustomerReceiptById(int receiptId);
        CustomerReceipt GetCustomerReceiptByNumber(string receiptNumber);
        List<CustomerReceipt> GetAllCustomerReceipts();
        List<CustomerReceipt> GetCustomerReceiptsByDateRange(DateTime startDate, DateTime endDate);
        List<CustomerReceipt> GetCustomerReceiptsByCustomer(int customerId);
        List<CustomerReceipt> GetCustomerReceiptsByStatus(string status);
        
        // Receipt Number Generation
        string GenerateReceiptNumber();
        
        // Reports
        List<CustomerReceipt> GetReceiptReport(DateTime startDate, DateTime endDate, int? customerId, string status);
        decimal GetTotalReceipts(DateTime startDate, DateTime endDate);
        int GetReceiptCount(DateTime startDate, DateTime endDate);
    }
}
