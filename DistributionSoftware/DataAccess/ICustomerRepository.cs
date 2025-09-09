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
        bool UpdateOutstandingBalance(int customerId, decimal amount);
        decimal GetOutstandingBalance(int customerId);
    }
}
