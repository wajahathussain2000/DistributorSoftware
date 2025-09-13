using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface ICustomerReceiptService
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
        
        // Business Logic
        string GenerateReceiptNumber();
        bool ValidateReceipt(CustomerReceipt receipt);
        bool ProcessReceiptPayment(CustomerReceipt receipt);
        
        // Reports
        List<CustomerReceipt> GetReceiptReport(DateTime startDate, DateTime endDate, int? customerId, string status);
        decimal GetTotalReceipts(DateTime startDate, DateTime endDate);
        int GetReceiptCount(DateTime startDate, DateTime endDate);
        
        // Customer Balance Updates
        bool UpdateCustomerBalance(int customerId, decimal amount, string transactionType);
        decimal GetCustomerOutstandingBalance(int customerId);
    }
}
