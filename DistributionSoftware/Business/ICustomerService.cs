using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface ICustomerService
    {
        // CRUD Operations
        int CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByCode(string customerCode);
        List<Customer> GetAllCustomers();
        List<Customer> GetActiveCustomers();
        
        // Business Logic
        bool ValidateCustomer(Customer customer);
        string[] GetValidationErrors(Customer customer);
        bool IsCustomerCodeExists(string customerCode, int? excludeId = null);
        
        // Reports
        List<Customer> GetCustomerReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetCustomerCount(bool? isActive);
    }
}
