using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public int CreateCustomer(Customer customer)
        {
            try
            {
                if (!ValidateCustomer(customer))
                    return 0;

                if (IsCustomerCodeExists(customer.CustomerCode))
                    return 0;

                return _customerRepository.Create(customer) ? 1 : 0;
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in CustomerService.CreateCustomer", ex);
                return 0;
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                if (!ValidateCustomer(customer))
                    return false;

                if (IsCustomerCodeExists(customer.CustomerCode, customer.CustomerId))
                    return false;

                return _customerRepository.Update(customer);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in CustomerService.UpdateCustomer", ex);
                return false;
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                return _customerRepository.Delete(customerId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in CustomerService.DeleteCustomer", ex);
                return false;
            }
        }

        public Customer GetCustomerById(int customerId)
        {
            try
            {
                return _customerRepository.GetById(customerId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in CustomerService.GetCustomerById", ex);
                return null;
            }
        }

        public Customer GetCustomerByCode(string customerCode)
        {
            try
            {
                return _customerRepository.GetByCode(customerCode);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in CustomerService.GetCustomerByCode", ex);
                return null;
            }
        }

        public List<Customer> GetAllCustomers()
        {
            try
            {
                return _customerRepository.GetAll();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in CustomerService.GetAllCustomers", ex);
                return new List<Customer>();
            }
        }

        public List<Customer> GetActiveCustomers()
        {
            try
            {
                return _customerRepository.GetActive();
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in CustomerService.GetActiveCustomers", ex);
                return new List<Customer>();
            }
        }

        public bool ValidateCustomer(Customer customer)
        {
            if (customer == null)
                return false;

            if (string.IsNullOrEmpty(customer.CustomerCode))
                return false;

            if (string.IsNullOrEmpty(customer.CustomerName))
                return false;

            return true;
        }

        public string[] GetValidationErrors(Customer customer)
        {
            var errors = new List<string>();

            if (customer == null)
            {
                errors.Add("Customer cannot be null");
                return errors.ToArray();
            }

            if (string.IsNullOrEmpty(customer.CustomerCode))
                errors.Add("Customer code is required");

            if (string.IsNullOrEmpty(customer.CustomerName))
                errors.Add("Customer name is required");

            return errors.ToArray();
        }

        public bool IsCustomerCodeExists(string customerCode, int? excludeId = null)
        {
            try
            {
                return _customerRepository.IsCodeExists(customerCode, excludeId);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in CustomerService.IsCustomerCodeExists", ex);
                return true; // Return true to prevent creation if there's an error
            }
        }

        public List<Customer> GetCustomerReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            try
            {
                return _customerRepository.GetReport(startDate, endDate, isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in CustomerService.GetCustomerReport", ex);
                return new List<Customer>();
            }
        }

        public int GetCustomerCount(bool? isActive)
        {
            try
            {
                return _customerRepository.GetCount(isActive);
            }
            catch (Exception ex)
            {
                Common.DebugHelper.WriteException("Error in CustomerService.GetCustomerCount", ex);
                return 0;
            }
        }
    }
}
