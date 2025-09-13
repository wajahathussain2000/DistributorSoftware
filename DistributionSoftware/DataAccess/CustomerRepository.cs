using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        c.CustomerId, c.CustomerCode, c.CustomerName, c.ContactPerson,
                        c.Phone, c.Email, c.Address, c.City, c.State,
                        c.PostalCode, c.Country, c.CategoryId, cc.CategoryName,
                        c.CreditLimit, c.IsActive, c.CreatedDate, c.ModifiedDate
                    FROM Customers c
                    LEFT JOIN CustomerCategories cc ON c.CategoryId = cc.CategoryId
                    ORDER BY c.CustomerName";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(MapCustomer(reader));
                    }
                }
            }
            return customers;
        }

        public List<Customer> GetActiveCustomers()
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        c.CustomerId, c.CustomerCode, c.CustomerName, c.ContactPerson,
                        c.Phone, c.Email, c.Address, c.City, c.State,
                        c.PostalCode, c.Country, c.CategoryId, cc.CategoryName,
                        c.CreditLimit, c.IsActive, c.CreatedDate, c.ModifiedDate
                    FROM Customers c
                    LEFT JOIN CustomerCategories cc ON c.CategoryId = cc.CategoryId
                    WHERE c.IsActive = 1
                    ORDER BY c.CustomerName";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(MapCustomer(reader));
                    }
                }
            }
            return customers;
        }

        public Customer GetCustomerById(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        c.CustomerId, c.CustomerCode, c.CustomerName, c.ContactPerson,
                        c.Phone, c.Email, c.Address, c.City, c.State,
                        c.PostalCode, c.Country, c.CategoryId, cc.CategoryName,
                        c.CreditLimit, c.IsActive, c.CreatedDate, c.ModifiedDate
                    FROM Customers c
                    LEFT JOIN CustomerCategories cc ON c.CategoryId = cc.CategoryId
                    WHERE c.CustomerId = @CustomerId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapCustomer(reader);
                        }
                    }
                }
            }
            return null;
        }

        public Customer GetCustomerByCode(string customerCode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        c.CustomerId, c.CustomerCode, c.CustomerName, c.ContactPerson,
                        c.Phone, c.Email, c.Address, c.City, c.State,
                        c.PostalCode, c.Country, c.CategoryId, cc.CategoryName,
                        c.CreditLimit, c.IsActive, c.CreatedDate, c.ModifiedDate
                    FROM Customers c
                    LEFT JOIN CustomerCategories cc ON c.CategoryId = cc.CategoryId
                    WHERE c.CustomerCode = @CustomerCode";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerCode", customerCode);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapCustomer(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<Customer> GetCustomersByCategory(int categoryId)
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        c.CustomerId, c.CustomerCode, c.CustomerName, c.ContactPerson,
                        c.Phone, c.Email, c.Address, c.City, c.State,
                        c.PostalCode, c.Country, c.CategoryId, cc.CategoryName,
                        c.CreditLimit, c.IsActive, c.CreatedDate, c.ModifiedDate
                    FROM Customers c
                    LEFT JOIN CustomerCategories cc ON c.CategoryId = cc.CategoryId
                    WHERE c.CategoryId = @CategoryId AND c.IsActive = 1
                    ORDER BY c.CustomerName";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(MapCustomer(reader));
                        }
                    }
                }
            }
            return customers;
        }

        public List<Customer> SearchCustomers(string searchTerm)
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT 
                        c.CustomerId, c.CustomerCode, c.CustomerName, c.ContactPerson,
                        c.Phone, c.Email, c.Address, c.City, c.State,
                        c.PostalCode, c.Country, c.CategoryId, cc.CategoryName,
                        c.CreditLimit, c.IsActive, c.CreatedDate, c.ModifiedDate
                    FROM Customers c
                    LEFT JOIN CustomerCategories cc ON c.CategoryId = cc.CategoryId
                    WHERE c.IsActive = 1 
                    AND (c.CustomerName LIKE @SearchTerm 
                         OR c.CustomerCode LIKE @SearchTerm 
                         OR c.ContactPerson LIKE @SearchTerm
                         OR c.Phone LIKE @SearchTerm
                         OR c.Email LIKE @SearchTerm)
                    ORDER BY c.CustomerName";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(MapCustomer(reader));
                        }
                    }
                }
            }
            return customers;
        }

        public bool CreateCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    INSERT INTO Customers (
                        CustomerCode, CustomerName, ContactName, Phone, Mobile, Email,
                        Address, City, State, PostalCode, Country, CategoryId,
                        CreditLimit, OutstandingBalance, PaymentTerms, TaxNumber, GSTNumber,
                        IsActive, Remarks, CreatedBy, CreatedDate
                    ) VALUES (
                        @CustomerCode, @CustomerName, @ContactName, @Phone, @Mobile, @Email,
                        @Address, @City, @State, @PostalCode, @Country, @CategoryId,
                        @CreditLimit, @OutstandingBalance, @PaymentTerms, @TaxNumber, @GSTNumber,
                        @IsActive, @Remarks, @CreatedBy, @CreatedDate
                    )";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerCode", customer.CustomerCode);
                    command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    command.Parameters.AddWithValue("@ContactName", customer.ContactName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", customer.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Mobile", customer.Mobile ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", customer.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Address", customer.Address ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@City", customer.City ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@State", customer.State ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PostalCode", customer.PostalCode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Country", customer.Country ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CategoryId", customer.CustomerCategoryId);
                    command.Parameters.AddWithValue("@CreditLimit", customer.CreditLimit);
                    command.Parameters.AddWithValue("@OutstandingBalance", customer.OutstandingBalance);
                    command.Parameters.AddWithValue("@PaymentTerms", customer.PaymentTerms ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TaxNumber", customer.TaxNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@GSTNumber", customer.GSTNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", customer.IsActive);
                    command.Parameters.AddWithValue("@Remarks", customer.Remarks ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedBy", customer.CreatedBy);
                    command.Parameters.AddWithValue("@CreatedDate", customer.CreatedDate);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    UPDATE Customers SET
                        CustomerCode = @CustomerCode,
                        CustomerName = @CustomerName,
                        ContactName = @ContactName,
                        Phone = @Phone,
                        Mobile = @Mobile,
                        Email = @Email,
                        Address = @Address,
                        City = @City,
                        State = @State,
                        PostalCode = @PostalCode,
                        Country = @Country,
                        CategoryId = @CategoryId,
                        CreditLimit = @CreditLimit,
                        OutstandingBalance = @OutstandingBalance,
                        PaymentTerms = @PaymentTerms,
                        TaxNumber = @TaxNumber,
                        GSTNumber = @GSTNumber,
                        IsActive = @IsActive,
                        Remarks = @Remarks,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = @ModifiedDate
                    WHERE CustomerId = @CustomerId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customer.CustomerId);
                    command.Parameters.AddWithValue("@CustomerCode", customer.CustomerCode);
                    command.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    command.Parameters.AddWithValue("@ContactName", customer.ContactName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", customer.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Mobile", customer.Mobile ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", customer.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Address", customer.Address ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@City", customer.City ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@State", customer.State ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PostalCode", customer.PostalCode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Country", customer.Country ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CategoryId", customer.CustomerCategoryId);
                    command.Parameters.AddWithValue("@CreditLimit", customer.CreditLimit);
                    command.Parameters.AddWithValue("@OutstandingBalance", customer.OutstandingBalance);
                    command.Parameters.AddWithValue("@PaymentTerms", customer.PaymentTerms ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TaxNumber", customer.TaxNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@GSTNumber", customer.GSTNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", customer.IsActive);
                    command.Parameters.AddWithValue("@Remarks", customer.Remarks ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ModifiedBy", customer.ModifiedBy ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ModifiedDate", customer.ModifiedDate ?? (object)DBNull.Value);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteCustomer(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Customers SET IsActive = 0 WHERE CustomerId = @CustomerId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateOutstandingBalance(int customerId, decimal amount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Customers SET OutstandingBalance = OutstandingBalance + @Amount WHERE CustomerId = @CustomerId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    command.Parameters.AddWithValue("@Amount", amount);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public decimal GetOutstandingBalance(int customerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT OutstandingBalance FROM Customers WHERE CustomerId = @CustomerId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        private Customer MapCustomer(SqlDataReader reader)
        {
            try
            {
                return new Customer
                {
                    CustomerId = SafeGetInt32(reader, "CustomerId"),
                    CustomerCode = SafeGetString(reader, "CustomerCode"),
                    CustomerName = SafeGetString(reader, "CustomerName"),
                    ContactName = SafeGetString(reader, "ContactPerson"),
                    Phone = SafeGetString(reader, "Phone"),
                    Mobile = SafeGetString(reader, "Mobile"),
                    Email = SafeGetString(reader, "Email"),
                    Address = SafeGetString(reader, "Address"),
                    City = SafeGetString(reader, "City"),
                    State = SafeGetString(reader, "State"),
                    PostalCode = SafeGetString(reader, "PostalCode"),
                    Country = SafeGetString(reader, "Country"),
                    CustomerCategoryId = SafeGetInt32(reader, "CategoryId"),
                    CustomerCategoryName = SafeGetString(reader, "CategoryName"),
                    CreditLimit = SafeGetDecimal(reader, "CreditLimit"),
                    OutstandingBalance = SafeGetDecimal(reader, "OutstandingBalance"),
                    PaymentTerms = SafeGetString(reader, "PaymentTerms"),
                    TaxNumber = SafeGetString(reader, "TaxNumber"),
                    GSTNumber = SafeGetString(reader, "GSTNumber"),
                    IsActive = SafeGetBoolean(reader, "IsActive"),
                    Remarks = SafeGetString(reader, "Remarks"),
                    CreatedBy = SafeGetInt32(reader, "CreatedBy"),
                    CreatedDate = SafeGetDateTime(reader, "CreatedDate"),
                    ModifiedBy = SafeGetInt32Nullable(reader, "ModifiedBy"),
                    ModifiedDate = SafeGetDateTimeNullable(reader, "ModifiedDate")
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error mapping customer: {ex.Message}");
                throw new Exception($"Error mapping customer data: {ex.Message}", ex);
            }
        }

        // Helper methods for safe data reading
        private string SafeGetString(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? null : value.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading string column '{columnName}': {ex.Message}");
                return null;
            }
        }

        private int SafeGetInt32(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? 0 : Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading int32 column '{columnName}': {ex.Message}");
                return 0;
            }
        }

        private int? SafeGetInt32Nullable(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? (int?)null : Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading int32 nullable column '{columnName}': {ex.Message}");
                return null;
            }
        }

        private decimal SafeGetDecimal(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? 0 : Convert.ToDecimal(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading decimal column '{columnName}': {ex.Message}");
                return 0;
            }
        }

        private bool SafeGetBoolean(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? false : Convert.ToBoolean(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading boolean column '{columnName}': {ex.Message}");
                return false;
            }
        }

        private DateTime SafeGetDateTime(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading datetime column '{columnName}': {ex.Message}");
                return DateTime.MinValue;
            }
        }

        private DateTime? SafeGetDateTimeNullable(SqlDataReader reader, string columnName)
        {
            try
            {
                var value = reader[columnName];
                return value == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error reading datetime nullable column '{columnName}': {ex.Message}");
                return null;
            }
        }
    }
}
