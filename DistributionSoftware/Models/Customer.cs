using System;

namespace DistributionSoftware.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public int CustomerCategoryId { get; set; }
        public string CustomerCategoryName { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal OutstandingBalance { get; set; }
        public string PaymentTerms { get; set; }
        public string TaxNumber { get; set; }
        public string GSTNumber { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        public Customer()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
            CreditLimit = 0;
            OutstandingBalance = 0;
        }
    }
}
