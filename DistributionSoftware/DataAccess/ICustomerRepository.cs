using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomers();
        List<Customer> GetActiveCustomers();
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByCode(string customerCode);
        List<Customer> GetCustomersByCategory(int categoryId);
        List<Customer> SearchCustomers(string searchTerm);
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(int customerId);
        
        // Alias methods for backward compatibility
        bool Create(Customer customer);
        bool Update(Customer customer);
        bool Delete(int customerId);
        Customer GetById(int customerId);
        Customer GetByCode(string customerCode);
        bool UpdateOutstandingBalance(int customerId, decimal amount);
        decimal GetOutstandingBalance(int customerId);
        
        // Additional methods for service compatibility
        List<Customer> GetAll();
        List<Customer> GetActive();
        bool IsCodeExists(string customerCode, int? excludeId = null);
        List<Customer> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive);
        int GetCount(bool? isActive);
    }
}
