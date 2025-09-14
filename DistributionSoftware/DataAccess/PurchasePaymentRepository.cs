using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class PurchasePaymentRepository : IPurchasePaymentRepository
    {
        private readonly string _connectionString;

        public PurchasePaymentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Create(PurchasePayment purchasePayment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        INSERT INTO PurchasePayments 
                        (PurchaseInvoiceId, Amount, PaymentDate, PaymentMode, Reference, Notes, BankAccountId, CreatedDate, CreatedBy)
                        VALUES 
                        (@PurchaseInvoiceId, @Amount, @PaymentDate, @PaymentMode, @Reference, @Notes, @BankAccountId, @CreatedDate, @CreatedBy);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    command.Parameters.AddWithValue("@PurchaseInvoiceId", purchasePayment.PurchaseInvoiceId);
                    command.Parameters.AddWithValue("@Amount", purchasePayment.Amount);
                    command.Parameters.AddWithValue("@PaymentDate", purchasePayment.PaymentDate);
                    command.Parameters.AddWithValue("@PaymentMode", purchasePayment.PaymentMode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Reference", purchasePayment.Reference ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Notes", purchasePayment.Notes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BankAccountId", purchasePayment.BankAccountId);
                    command.Parameters.AddWithValue("@CreatedDate", purchasePayment.CreatedDate);
                    command.Parameters.AddWithValue("@CreatedBy", purchasePayment.CreatedBy);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(PurchasePayment purchasePayment)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        UPDATE PurchasePayments 
                        SET PurchaseInvoiceId = @PurchaseInvoiceId, Amount = @Amount, PaymentDate = @PaymentDate,
                            PaymentMode = @PaymentMode, Reference = @Reference, Notes = @Notes, BankAccountId = @BankAccountId
                        WHERE PaymentId = @PaymentId";

                    command.Parameters.AddWithValue("@PaymentId", purchasePayment.PaymentId);
                    command.Parameters.AddWithValue("@PurchaseInvoiceId", purchasePayment.PurchaseInvoiceId);
                    command.Parameters.AddWithValue("@Amount", purchasePayment.Amount);
                    command.Parameters.AddWithValue("@PaymentDate", purchasePayment.PaymentDate);
                    command.Parameters.AddWithValue("@PaymentMode", purchasePayment.PaymentMode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Reference", purchasePayment.Reference ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Notes", purchasePayment.Notes ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BankAccountId", purchasePayment.BankAccountId);

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
                    command.CommandText = "DELETE FROM PurchasePayments WHERE PaymentId = @PaymentId";
                    command.Parameters.AddWithValue("@PaymentId", paymentId);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public PurchasePayment GetById(int paymentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM PurchasePayments WHERE PaymentId = @PaymentId";
                    command.Parameters.AddWithValue("@PaymentId", paymentId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return GetPurchasePaymentFromReader(reader);
                    }
                }
            }
            return null;
        }

        public List<PurchasePayment> GetAll()
        {
            var payments = new List<PurchasePayment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM PurchasePayments ORDER BY PaymentDate DESC";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(GetPurchasePaymentFromReader(reader));
                        }
                    }
                }
            }
            return payments;
        }

        public List<PurchasePayment> GetByPurchaseInvoiceId(int purchaseInvoiceId)
        {
            var payments = new List<PurchasePayment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM PurchasePayments WHERE PurchaseInvoiceId = @PurchaseInvoiceId ORDER BY PaymentDate DESC";
                    command.Parameters.AddWithValue("@PurchaseInvoiceId", purchaseInvoiceId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            payments.Add(GetPurchasePaymentFromReader(reader));
                        }
                    }
                }
            }
            return payments;
        }

        public List<PurchasePayment> GetReport(DateTime? startDate, DateTime? endDate)
        {
            var payments = new List<PurchasePayment>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM PurchasePayments WHERE 1=1";
                    
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
                            payments.Add(GetPurchasePaymentFromReader(reader));
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
                    command.CommandText = "SELECT COUNT(*) FROM PurchasePayments";

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private PurchasePayment GetPurchasePaymentFromReader(SqlDataReader reader)
        {
            return new PurchasePayment
            {
                PaymentId = Convert.ToInt32(reader["PaymentId"]),
                PurchaseInvoiceId = Convert.ToInt32(reader["PurchaseInvoiceId"]),
                Amount = Convert.ToDecimal(reader["Amount"]),
                PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                PaymentMode = reader["PaymentMode"] == DBNull.Value ? null : reader["PaymentMode"].ToString(),
                Reference = reader["Reference"] == DBNull.Value ? null : reader["Reference"].ToString(),
                Notes = reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                BankAccountId = Convert.ToInt32(reader["BankAccountId"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? null : reader["CreatedBy"].ToString()
            };
        }

        private PurchasePayment MapPurchasePayment(IDataReader reader)
        {
            return new PurchasePayment
            {
                PaymentId = Convert.ToInt32(reader["PaymentId"]),
                PurchaseInvoiceId = Convert.ToInt32(reader["PurchaseInvoiceId"]),
                Amount = Convert.ToDecimal(reader["Amount"]),
                PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                PaymentMode = reader["PaymentMode"] == DBNull.Value ? null : reader["PaymentMode"].ToString(),
                Reference = reader["Reference"] == DBNull.Value ? null : reader["Reference"].ToString(),
                Notes = reader["Notes"] == DBNull.Value ? null : reader["Notes"].ToString(),
                BankAccountId = Convert.ToInt32(reader["BankAccountId"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? null : reader["CreatedBy"].ToString()
            };
        }

        public List<PurchasePayment> GetByBankAccount(int bankAccountId, DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    var query = "SELECT * FROM PurchasePayments WHERE BankAccountId = @BankAccountId";
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
                        var payments = new List<PurchasePayment>();
                        while (reader.Read())
                        {
                            payments.Add(MapPurchasePayment(reader));
                        }
                        return payments;
                    }
                }
            }
        }
    }
}
