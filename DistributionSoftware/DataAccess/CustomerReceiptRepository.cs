using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class CustomerReceiptRepository : ICustomerReceiptRepository
    {
        private readonly string _connectionString;

        public CustomerReceiptRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public int CreateCustomerReceipt(CustomerReceipt receipt)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var query = @"
                            INSERT INTO CustomerReceipts (
                                ReceiptNumber, ReceiptDate, CustomerId, CustomerName, CustomerPhone, CustomerAddress,
                                Amount, PaymentMethod, InvoiceReference, Description, ReceivedBy, Status, Remarks,
                                BankName, ChequeNumber, ChequeDate, TransactionId, CardNumber, CardType,
                                MobileNumber, PaymentReference, CreatedBy, CreatedDate
                            ) VALUES (
                                @ReceiptNumber, @ReceiptDate, @CustomerId, @CustomerName, @CustomerPhone, @CustomerAddress,
                                @Amount, @PaymentMethod, @InvoiceReference, @Description, @ReceivedBy, @Status, @Remarks,
                                @BankName, @ChequeNumber, @ChequeDate, @TransactionId, @CardNumber, @CardType,
                                @MobileNumber, @PaymentReference, @CreatedBy, @CreatedDate
                            );
                            SELECT SCOPE_IDENTITY();";

                        using (var command = new SqlCommand(query, connection, transaction))
                        {
                            command.CommandTimeout = 30;
                            command.Parameters.AddWithValue("@ReceiptNumber", receipt.ReceiptNumber);
                            command.Parameters.AddWithValue("@ReceiptDate", receipt.ReceiptDate);
                            command.Parameters.AddWithValue("@CustomerId", receipt.CustomerId);
                            command.Parameters.AddWithValue("@CustomerName", receipt.CustomerName ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@CustomerPhone", receipt.CustomerPhone ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@CustomerAddress", receipt.CustomerAddress ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Amount", receipt.Amount);
                            command.Parameters.AddWithValue("@PaymentMethod", receipt.PaymentMethod);
                            command.Parameters.AddWithValue("@InvoiceReference", receipt.InvoiceReference ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Description", receipt.Description ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@ReceivedBy", receipt.ReceivedBy);
                            command.Parameters.AddWithValue("@Status", receipt.Status);
                            command.Parameters.AddWithValue("@Remarks", receipt.Remarks ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@BankName", receipt.BankName ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@ChequeNumber", receipt.ChequeNumber ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@ChequeDate", receipt.ChequeDate.HasValue ? receipt.ChequeDate.Value : (object)DBNull.Value);
                            command.Parameters.AddWithValue("@TransactionId", receipt.TransactionId ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@CardNumber", receipt.CardNumber ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@CardType", receipt.CardType ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@MobileNumber", receipt.MobileNumber ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@PaymentReference", receipt.PaymentReference ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@CreatedBy", receipt.CreatedBy);
                            command.Parameters.AddWithValue("@CreatedDate", receipt.CreatedDate);

                            var receiptId = Convert.ToInt32(command.ExecuteScalar());
                            transaction.Commit();
                            return receiptId;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool UpdateCustomerReceipt(CustomerReceipt receipt)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    UPDATE CustomerReceipts SET
                        ReceiptDate = @ReceiptDate,
                        CustomerId = @CustomerId,
                        CustomerName = @CustomerName,
                        CustomerPhone = @CustomerPhone,
                        CustomerAddress = @CustomerAddress,
                        Amount = @Amount,
                        PaymentMethod = @PaymentMethod,
                        InvoiceReference = @InvoiceReference,
                        Description = @Description,
                        ReceivedBy = @ReceivedBy,
                        Status = @Status,
                        Remarks = @Remarks,
                        BankName = @BankName,
                        ChequeNumber = @ChequeNumber,
                        ChequeDate = @ChequeDate,
                        TransactionId = @TransactionId,
                        CardNumber = @CardNumber,
                        CardType = @CardType,
                        MobileNumber = @MobileNumber,
                        PaymentReference = @PaymentReference,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = @ModifiedDate
                    WHERE ReceiptId = @ReceiptId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReceiptId", receipt.ReceiptId);
                    command.Parameters.AddWithValue("@ReceiptDate", receipt.ReceiptDate);
                    command.Parameters.AddWithValue("@CustomerId", receipt.CustomerId);
                    command.Parameters.AddWithValue("@CustomerName", receipt.CustomerName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerPhone", receipt.CustomerPhone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerAddress", receipt.CustomerAddress ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Amount", receipt.Amount);
                    command.Parameters.AddWithValue("@PaymentMethod", receipt.PaymentMethod);
                    command.Parameters.AddWithValue("@InvoiceReference", receipt.InvoiceReference ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", receipt.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ReceivedBy", receipt.ReceivedBy);
                    command.Parameters.AddWithValue("@Status", receipt.Status);
                    command.Parameters.AddWithValue("@Remarks", receipt.Remarks ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BankName", receipt.BankName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ChequeNumber", receipt.ChequeNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ChequeDate", receipt.ChequeDate.HasValue ? receipt.ChequeDate.Value : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TransactionId", receipt.TransactionId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CardNumber", receipt.CardNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CardType", receipt.CardType ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MobileNumber", receipt.MobileNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PaymentReference", receipt.PaymentReference ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ModifiedBy", receipt.ModifiedBy ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ModifiedDate", receipt.ModifiedDate ?? (object)DBNull.Value);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteCustomerReceipt(int receiptId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM CustomerReceipts WHERE ReceiptId = @ReceiptId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReceiptId", receiptId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public CustomerReceipt GetCustomerReceiptById(int receiptId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT cr.*, u.FirstName + ' ' + u.LastName AS CreatedByUser
                    FROM CustomerReceipts cr
                    LEFT JOIN Users u ON cr.CreatedBy = u.UserId
                    WHERE cr.ReceiptId = @ReceiptId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReceiptId", receiptId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapCustomerReceipt(reader);
                        }
                    }
                }
            }
            return null;
        }

        public CustomerReceipt GetCustomerReceiptByNumber(string receiptNumber)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT cr.*, u.FirstName + ' ' + u.LastName AS CreatedByUser
                    FROM CustomerReceipts cr
                    LEFT JOIN Users u ON cr.CreatedBy = u.UserId
                    WHERE cr.ReceiptNumber = @ReceiptNumber";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReceiptNumber", receiptNumber);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapCustomerReceipt(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<CustomerReceipt> GetAllCustomerReceipts()
        {
            var receipts = new List<CustomerReceipt>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT cr.*, u.FirstName + ' ' + u.LastName AS CreatedByUser
                    FROM CustomerReceipts cr
                    LEFT JOIN Users u ON cr.CreatedBy = u.UserId
                    ORDER BY cr.ReceiptDate DESC";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        receipts.Add(MapCustomerReceipt(reader));
                    }
                }
            }
            return receipts;
        }

        public List<CustomerReceipt> GetCustomerReceiptsByDateRange(DateTime startDate, DateTime endDate)
        {
            var receipts = new List<CustomerReceipt>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT cr.*, u.FirstName + ' ' + u.LastName AS CreatedByUser
                    FROM CustomerReceipts cr
                    LEFT JOIN Users u ON cr.CreatedBy = u.UserId
                    WHERE cr.ReceiptDate >= @StartDate AND cr.ReceiptDate <= @EndDate
                    ORDER BY cr.ReceiptDate DESC";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            receipts.Add(MapCustomerReceipt(reader));
                        }
                    }
                }
            }
            return receipts;
        }

        public List<CustomerReceipt> GetCustomerReceiptsByCustomer(int customerId)
        {
            var receipts = new List<CustomerReceipt>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT cr.*, u.FirstName + ' ' + u.LastName AS CreatedByUser
                    FROM CustomerReceipts cr
                    LEFT JOIN Users u ON cr.CreatedBy = u.UserId
                    WHERE cr.CustomerId = @CustomerId
                    ORDER BY cr.ReceiptDate DESC";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", customerId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            receipts.Add(MapCustomerReceipt(reader));
                        }
                    }
                }
            }
            return receipts;
        }

        public List<CustomerReceipt> GetCustomerReceiptsByStatus(string status)
        {
            var receipts = new List<CustomerReceipt>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT cr.*, u.FirstName + ' ' + u.LastName AS CreatedByUser
                    FROM CustomerReceipts cr
                    LEFT JOIN Users u ON cr.CreatedBy = u.UserId
                    WHERE cr.Status = @Status
                    ORDER BY cr.ReceiptDate DESC";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            receipts.Add(MapCustomerReceipt(reader));
                        }
                    }
                }
            }
            return receipts;
        }

        public string GenerateReceiptNumber()
        {
            var currentDate = DateTime.Now;
            var yearMonth = currentDate.ToString("yyyyMM");
            var prefix = $"RCP{yearMonth}";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT ISNULL(MAX(CAST(SUBSTRING(ReceiptNumber, 10, 4) AS INT)), 0) + 1
                    FROM CustomerReceipts 
                    WHERE ReceiptNumber LIKE @Prefix + '%'";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Prefix", prefix);
                    var nextNumber = Convert.ToInt32(command.ExecuteScalar());
                    return $"{prefix}{nextNumber:D4}";
                }
            }
        }

        public List<CustomerReceipt> GetReceiptReport(DateTime startDate, DateTime endDate, int? customerId, string status)
        {
            var receipts = new List<CustomerReceipt>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = new StringBuilder(@"
                    SELECT cr.*, u.FirstName + ' ' + u.LastName AS CreatedByUser
                    FROM CustomerReceipts cr
                    LEFT JOIN Users u ON cr.CreatedBy = u.UserId
                    WHERE cr.ReceiptDate >= @StartDate AND cr.ReceiptDate <= @EndDate");

                if (customerId.HasValue)
                {
                    query.Append(" AND cr.CustomerId = @CustomerId");
                }

                if (!string.IsNullOrEmpty(status))
                {
                    query.Append(" AND cr.Status = @Status");
                }

                query.Append(" ORDER BY cr.ReceiptDate DESC");

                using (var command = new SqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    if (customerId.HasValue)
                    {
                        command.Parameters.AddWithValue("@CustomerId", customerId.Value);
                    }

                    if (!string.IsNullOrEmpty(status))
                    {
                        command.Parameters.AddWithValue("@Status", status);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            receipts.Add(MapCustomerReceipt(reader));
                        }
                    }
                }
            }
            return receipts;
        }

        public decimal GetTotalReceipts(DateTime startDate, DateTime endDate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT ISNULL(SUM(Amount), 0)
                    FROM CustomerReceipts 
                    WHERE ReceiptDate >= @StartDate AND ReceiptDate <= @EndDate
                    AND Status = 'COMPLETED'";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    return Convert.ToDecimal(command.ExecuteScalar());
                }
            }
        }

        public int GetReceiptCount(DateTime startDate, DateTime endDate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"
                    SELECT COUNT(*)
                    FROM CustomerReceipts 
                    WHERE ReceiptDate >= @StartDate AND ReceiptDate <= @EndDate";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private CustomerReceipt MapCustomerReceipt(SqlDataReader reader)
        {
            return new CustomerReceipt
            {
                ReceiptId = Convert.ToInt32(reader["ReceiptId"]),
                ReceiptNumber = reader["ReceiptNumber"].ToString(),
                ReceiptDate = Convert.ToDateTime(reader["ReceiptDate"]),
                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                CustomerName = reader["CustomerName"]?.ToString(),
                CustomerPhone = reader["CustomerPhone"]?.ToString(),
                CustomerAddress = reader["CustomerAddress"]?.ToString(),
                Amount = Convert.ToDecimal(reader["Amount"]),
                PaymentMethod = reader["PaymentMethod"].ToString(),
                InvoiceReference = reader["InvoiceReference"]?.ToString(),
                Description = reader["Description"]?.ToString(),
                ReceivedBy = reader["ReceivedBy"].ToString(),
                Status = reader["Status"].ToString(),
                Remarks = reader["Remarks"]?.ToString(),
                BankName = reader["BankName"]?.ToString(),
                ChequeNumber = reader["ChequeNumber"]?.ToString(),
                ChequeDate = reader["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(reader["ChequeDate"]) : (DateTime?)null,
                TransactionId = reader["TransactionId"]?.ToString(),
                CardNumber = reader["CardNumber"]?.ToString(),
                CardType = reader["CardType"]?.ToString(),
                MobileNumber = reader["MobileNumber"]?.ToString(),
                PaymentReference = reader["PaymentReference"]?.ToString(),
                CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                CreatedByUser = reader["CreatedByUser"]?.ToString(),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : (int?)null,
                ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null
            };
        }
    }
}
