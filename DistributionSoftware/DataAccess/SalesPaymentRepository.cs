using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class SalesPaymentRepository : ISalesPaymentRepository
    {
        private readonly string _connectionString;

        public SalesPaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Create(SalesPayment salesPayment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        INSERT INTO SalesPayments 
                        (SalesInvoiceId, Amount, PaymentDate, PaymentMode, Reference, Notes, BankAccountId, CreatedDate, CreatedBy)
                        VALUES 
                        (@SalesInvoiceId, @Amount, @PaymentDate, @PaymentMode, @Reference, @Notes, @BankAccountId, @CreatedDate, @CreatedBy);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    command.Parameters.AddWithValue("@SalesInvoiceId", salesPayment.SalesInvoiceId);
                    command.Parameters.AddWithValue("@Amount", salesPayment.Amount);
                    command.Parameters.AddWithValue("@PaymentDate", salesPayment.PaymentDate);
                    command.Parameters.AddWithValue("@PaymentMode", salesPayment.PaymentMode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Reference", salesPayment.Reference ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Notes", salesPayment.Notes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BankAccountId", salesPayment.BankAccountId);
                    command.Parameters.AddWithValue("@CreatedDate", salesPayment.CreatedDate);
                    command.Parameters.AddWithValue("@CreatedBy", salesPayment.CreatedBy);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(SalesPayment salesPayment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        UPDATE SalesPayments 
                        SET SalesInvoiceId = @SalesInvoiceId, Amount = @Amount, PaymentDate = @PaymentDate,
                            PaymentMode = @PaymentMode, Reference = @Reference, Notes = @Notes, BankAccountId = @BankAccountId
                        WHERE PaymentId = @PaymentId";

                    command.Parameters.AddWithValue("@PaymentId", salesPayment.PaymentId);
                    command.Parameters.AddWithValue("@SalesInvoiceId", salesPayment.SalesInvoiceId);
                    command.Parameters.AddWithValue("@Amount", salesPayment.Amount);
                    command.Parameters.AddWithValue("@PaymentDate", salesPayment.PaymentDate);
                    command.Parameters.AddWithValue("@PaymentMode", salesPayment.PaymentMode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Reference", salesPayment.Reference ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Notes", salesPayment.Notes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BankAccountId", salesPayment.BankAccountId);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int paymentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM SalesPayments WHERE PaymentId = @PaymentId";
                    command.Parameters.AddWithValue("@PaymentId", paymentId);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public SalesPayment GetById(int paymentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM SalesPayments WHERE PaymentId = @PaymentId";
                    command.Parameters.AddWithValue("@PaymentId", paymentId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return GetSalesPaymentFromReader(reader);
                    }
                }
            }
            return null;
        }

        public List<SalesPayment> GetAll()
        {
            var payments = new List<SalesPayment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM SalesPayments ORDER BY PaymentDate DESC";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(GetSalesPaymentFromReader(reader));
                        }
                    }
                }
            }
            return payments;
        }

        public List<SalesPayment> GetBySalesInvoiceId(int salesInvoiceId)
        {
            var payments = new List<SalesPayment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM SalesPayments WHERE SalesInvoiceId = @SalesInvoiceId ORDER BY PaymentDate DESC";
                    command.Parameters.AddWithValue("@SalesInvoiceId", salesInvoiceId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(GetSalesPaymentFromReader(reader));
                        }
                    }
                }
            }
            return payments;
        }

        public List<SalesPayment> GetReport(DateTime? startDate, DateTime? endDate)
        {
            var payments = new List<SalesPayment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM SalesPayments WHERE 1=1";
                    
                    if (startDate.HasValue)
                    {
                        command.CommandText += " AND PaymentDate >= @StartDate";
                        command.Parameters.AddWithValue("@StartDate", startDate.Value);
                    }
                    
                    if (endDate.HasValue)
                    {
                        command.CommandText += " AND PaymentDate <= @EndDate";
                        command.Parameters.AddWithValue("@EndDate", endDate.Value);
                    }
                    
                    command.CommandText += " ORDER BY PaymentDate DESC";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(GetSalesPaymentFromReader(reader));
                        }
                    }
                }
            }
            return payments;
        }

        public int GetCount()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM SalesPayments";

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private SalesPayment GetSalesPaymentFromReader(SqlDataReader reader)
        {
            return new SalesPayment
            {
                PaymentId = Convert.ToInt32(reader["PaymentId"]),
                SalesInvoiceId = Convert.ToInt32(reader["SalesInvoiceId"]),
                Amount = Convert.ToDecimal(reader["Amount"]),
                PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                PaymentMode = reader["PaymentMode"] == DBNull.Value ? null : reader["PaymentMode"].ToString(),
                Reference = reader["Reference"] == DBNull.Value ? null : reader["Reference"].ToString(),
                Notes = reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                BankAccountId = Convert.ToInt32(reader["BankAccountId"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CreatedBy"])
            };
        }

        private SalesPayment MapSalesPayment(IDataReader reader)
        {
            return new SalesPayment
            {
                PaymentId = Convert.ToInt32(reader["PaymentId"]),
                SalesInvoiceId = Convert.ToInt32(reader["SalesInvoiceId"]),
                Amount = Convert.ToDecimal(reader["Amount"]),
                PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                PaymentMode = reader["PaymentMode"] == DBNull.Value ? null : reader["PaymentMode"].ToString(),
                Reference = reader["Reference"] == DBNull.Value ? null : reader["Reference"].ToString(),
                Notes = reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                BankAccountId = Convert.ToInt32(reader["BankAccountId"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CreatedBy"])
            };
        }

        public List<SalesPayment> GetByBankAccount(int bankAccountId, DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    var query = "SELECT * FROM SalesPayments WHERE BankAccountId = @BankAccountId";
                    if (startDate.HasValue)
                    {
                        query += " AND PaymentDate >= @StartDate";
                    }
                    if (endDate.HasValue)
                    {
                        query += " AND PaymentDate <= @EndDate";
                    }
                    
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@BankAccountId", bankAccountId);
                    if (startDate.HasValue)
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Value);
                    }
                    if (endDate.HasValue)
                    {
                        command.Parameters.AddWithValue("@EndDate", endDate.Value);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        var payments = new List<SalesPayment>();
                        while (reader.Read())
                        {
                            payments.Add(MapSalesPayment(reader));
                        }
                        return payments;
                    }
                }
            }
        }
    }
}
