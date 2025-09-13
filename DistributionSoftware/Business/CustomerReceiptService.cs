using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class CustomerReceiptService : ICustomerReceiptService
    {
        private readonly ICustomerReceiptRepository _customerReceiptRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerReceiptService()
        {
            _customerReceiptRepository = new CustomerReceiptRepository();
            _customerRepository = new CustomerRepository();
        }

        public CustomerReceiptService(ICustomerReceiptRepository customerReceiptRepository, ICustomerRepository customerRepository)
        {
            _customerReceiptRepository = customerReceiptRepository;
            _customerRepository = customerRepository;
        }

        public int CreateCustomerReceipt(CustomerReceipt receipt)
        {
            try
            {
                // Validate receipt
                if (!ValidateReceipt(receipt))
                {
                    throw new InvalidOperationException("Invalid receipt data");
                }

                // Generate receipt number if not provided
                if (string.IsNullOrEmpty(receipt.ReceiptNumber))
                {
                    receipt.ReceiptNumber = GenerateReceiptNumber();
                }

                // Set default values
                receipt.Status = "COMPLETED";
                receipt.CreatedBy = UserSession.CurrentUserId;
                receipt.CreatedDate = DateTime.Now;

                // Create receipt
                var receiptId = _customerReceiptRepository.CreateCustomerReceipt(receipt);
                receipt.ReceiptId = receiptId;

                // Update customer balance
                if (receipt.CustomerId > 0)
                {
                    UpdateCustomerBalance(receipt.CustomerId, receipt.Amount, "RECEIPT");
                }

                return receiptId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating customer receipt: {ex.Message}", ex);
            }
        }

        public bool UpdateCustomerReceipt(CustomerReceipt receipt)
        {
            try
            {
                if (!ValidateReceipt(receipt))
                {
                    return false;
                }

                receipt.ModifiedBy = UserSession.CurrentUserId;
                receipt.ModifiedDate = DateTime.Now;

                return _customerReceiptRepository.UpdateCustomerReceipt(receipt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating customer receipt: {ex.Message}", ex);
            }
        }

        public bool DeleteCustomerReceipt(int receiptId)
        {
            try
            {
                var receipt = GetCustomerReceiptById(receiptId);
                if (receipt != null && receipt.CustomerId > 0)
                {
                    // Reverse customer balance update
                    UpdateCustomerBalance(receipt.CustomerId, -receipt.Amount, "RECEIPT_REVERSAL");
                }

                return _customerReceiptRepository.DeleteCustomerReceipt(receiptId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting customer receipt: {ex.Message}", ex);
            }
        }

        public CustomerReceipt GetCustomerReceiptById(int receiptId)
        {
            return _customerReceiptRepository.GetCustomerReceiptById(receiptId);
        }

        public CustomerReceipt GetCustomerReceiptByNumber(string receiptNumber)
        {
            return _customerReceiptRepository.GetCustomerReceiptByNumber(receiptNumber);
        }

        public List<CustomerReceipt> GetAllCustomerReceipts()
        {
            return _customerReceiptRepository.GetAllCustomerReceipts();
        }

        public List<CustomerReceipt> GetCustomerReceiptsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _customerReceiptRepository.GetCustomerReceiptsByDateRange(startDate, endDate);
        }

        public List<CustomerReceipt> GetCustomerReceiptsByCustomer(int customerId)
        {
            return _customerReceiptRepository.GetCustomerReceiptsByCustomer(customerId);
        }

        public List<CustomerReceipt> GetCustomerReceiptsByStatus(string status)
        {
            return _customerReceiptRepository.GetCustomerReceiptsByStatus(status);
        }

        public string GenerateReceiptNumber()
        {
            return _customerReceiptRepository.GenerateReceiptNumber();
        }

        public bool ValidateReceipt(CustomerReceipt receipt)
        {
            if (receipt == null)
                return false;

            if (receipt.CustomerId <= 0)
                return false;

            if (receipt.Amount <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(receipt.PaymentMethod))
                return false;

            if (string.IsNullOrWhiteSpace(receipt.ReceivedBy))
                return false;

            return true;
        }

        public bool ProcessReceiptPayment(CustomerReceipt receipt)
        {
            try
            {
                // This method can be extended for payment processing integration
                // For now, it just validates and creates the receipt
                return ValidateReceipt(receipt);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing receipt payment: {ex.Message}", ex);
            }
        }

        public List<CustomerReceipt> GetReceiptReport(DateTime startDate, DateTime endDate, int? customerId, string status)
        {
            return _customerReceiptRepository.GetReceiptReport(startDate, endDate, customerId, status);
        }

        public decimal GetTotalReceipts(DateTime startDate, DateTime endDate)
        {
            return _customerReceiptRepository.GetTotalReceipts(startDate, endDate);
        }

        public int GetReceiptCount(DateTime startDate, DateTime endDate)
        {
            return _customerReceiptRepository.GetReceiptCount(startDate, endDate);
        }

        public bool UpdateCustomerBalance(int customerId, decimal amount, string transactionType)
        {
            try
            {
                // Use the targeted balance update method to avoid foreign key constraint issues
                return _customerRepository.UpdateOutstandingBalance(customerId, amount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating customer balance: {ex.Message}", ex);
            }
        }

        public decimal GetCustomerOutstandingBalance(int customerId)
        {
            try
            {
                var customer = _customerRepository.GetCustomerById(customerId);
                return customer?.OutstandingBalance ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting customer outstanding balance: {ex.Message}", ex);
            }
        }
    }
}
