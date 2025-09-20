using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class PaymentVoucherRepository : IPaymentVoucherRepository
    {
        private readonly string _connectionString;

        public PaymentVoucherRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public PaymentVoucherRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CreatePaymentVoucher(PaymentVoucher voucher)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            var sql = @"INSERT INTO PaymentVouchers 
                                       (VoucherNumber, VoucherDate, Reference, Narration, PaymentMode, Amount, 
                                        AccountId, BankAccountId, CardNumber, CardType, TransactionId, BankName, 
                                        ChequeNumber, ChequeDate, MobileNumber, PaymentReference, Status, Remarks, 
                                        CreatedBy, CreatedDate, CreatedByName)
                                       VALUES 
                                       (@VoucherNumber, @VoucherDate, @Reference, @Narration, @PaymentMode, @Amount, 
                                        @AccountId, @BankAccountId, @CardNumber, @CardType, @TransactionId, @BankName, 
                                        @ChequeNumber, @ChequeDate, @MobileNumber, @PaymentReference, @Status, @Remarks, 
                                        @CreatedBy, @CreatedDate, @CreatedByName);
                                       SELECT SCOPE_IDENTITY();";

                            int voucherId;
                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@VoucherNumber", voucher.VoucherNumber);
                                command.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                                command.Parameters.AddWithValue("@Reference", voucher.Reference ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@Narration", voucher.Narration);
                                command.Parameters.AddWithValue("@PaymentMode", voucher.PaymentMode);
                                command.Parameters.AddWithValue("@Amount", voucher.Amount);
                                command.Parameters.AddWithValue("@AccountId", voucher.AccountId);
                                command.Parameters.AddWithValue("@BankAccountId", voucher.BankAccountId ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@CardNumber", voucher.CardNumber ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@CardType", voucher.CardType ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@TransactionId", voucher.TransactionId ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@BankName", voucher.BankName ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@ChequeNumber", voucher.ChequeNumber ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@ChequeDate", voucher.ChequeDate ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@MobileNumber", voucher.MobileNumber ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@PaymentReference", voucher.PaymentReference ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@Status", voucher.Status);
                                command.Parameters.AddWithValue("@Remarks", voucher.Remarks ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@CreatedBy", voucher.CreatedBy);
                                command.Parameters.AddWithValue("@CreatedDate", voucher.CreatedDate);
                                command.Parameters.AddWithValue("@CreatedByName", voucher.CreatedByName ?? (object)DBNull.Value);

                                voucherId = Convert.ToInt32(command.ExecuteScalar());
                            }

                            transaction.Commit();
                            return voucherId;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.CreatePaymentVoucher", ex);
                throw;
            }
        }

        public bool UpdatePaymentVoucher(PaymentVoucher voucher)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"UPDATE PaymentVouchers SET
                               VoucherNumber = @VoucherNumber,
                               VoucherDate = @VoucherDate,
                               Reference = @Reference,
                               Narration = @Narration,
                               PaymentMode = @PaymentMode,
                               Amount = @Amount,
                               AccountId = @AccountId,
                               BankAccountId = @BankAccountId,
                               CardNumber = @CardNumber,
                               CardType = @CardType,
                               TransactionId = @TransactionId,
                               BankName = @BankName,
                               ChequeNumber = @ChequeNumber,
                               ChequeDate = @ChequeDate,
                               MobileNumber = @MobileNumber,
                               PaymentReference = @PaymentReference,
                               Status = @Status,
                               Remarks = @Remarks,
                               ModifiedBy = @ModifiedBy,
                               ModifiedDate = @ModifiedDate,
                               ModifiedByName = @ModifiedByName
                               WHERE PaymentVoucherId = @PaymentVoucherId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PaymentVoucherId", voucher.PaymentVoucherId);
                        command.Parameters.AddWithValue("@VoucherNumber", voucher.VoucherNumber);
                        command.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                        command.Parameters.AddWithValue("@Reference", voucher.Reference ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Narration", voucher.Narration);
                        command.Parameters.AddWithValue("@PaymentMode", voucher.PaymentMode);
                        command.Parameters.AddWithValue("@Amount", voucher.Amount);
                        command.Parameters.AddWithValue("@AccountId", voucher.AccountId);
                        command.Parameters.AddWithValue("@BankAccountId", voucher.BankAccountId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CardNumber", voucher.CardNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CardType", voucher.CardType ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TransactionId", voucher.TransactionId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BankName", voucher.BankName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ChequeNumber", voucher.ChequeNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ChequeDate", voucher.ChequeDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@MobileNumber", voucher.MobileNumber ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PaymentReference", voucher.PaymentReference ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Status", voucher.Status);
                        command.Parameters.AddWithValue("@Remarks", voucher.Remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", voucher.ModifiedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedDate", voucher.ModifiedDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedByName", voucher.ModifiedByName ?? (object)DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.UpdatePaymentVoucher", ex);
                throw;
            }
        }

        public bool DeletePaymentVoucher(int voucherId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "DELETE FROM PaymentVouchers WHERE PaymentVoucherId = @PaymentVoucherId";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PaymentVoucherId", voucherId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.DeletePaymentVoucher", ex);
                throw;
            }
        }

        public PaymentVoucher GetPaymentVoucherById(int voucherId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM PaymentVouchers pv
                               LEFT JOIN Users u ON pv.CreatedBy = u.UserId
                               WHERE pv.PaymentVoucherId = @PaymentVoucherId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PaymentVoucherId", voucherId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapPaymentVoucher(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.GetPaymentVoucherById", ex);
                throw;
            }
            return null;
        }

        public PaymentVoucher GetPaymentVoucherByNumber(string voucherNumber)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM PaymentVouchers pv
                               LEFT JOIN Users u ON pv.CreatedBy = u.UserId
                               WHERE pv.VoucherNumber = @VoucherNumber";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@VoucherNumber", voucherNumber);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapPaymentVoucher(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.GetPaymentVoucherByNumber", ex);
                throw;
            }
            return null;
        }

        public List<PaymentVoucher> GetAllPaymentVouchers()
        {
            var vouchers = new List<PaymentVoucher>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM PaymentVouchers pv
                               LEFT JOIN Users u ON pv.CreatedBy = u.UserId
                               ORDER BY pv.VoucherDate DESC, pv.VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vouchers.Add(MapPaymentVoucher(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.GetAllPaymentVouchers", ex);
                throw;
            }
            return vouchers;
        }

        public List<PaymentVoucher> GetPaymentVouchersByDateRange(DateTime startDate, DateTime endDate)
        {
            var vouchers = new List<PaymentVoucher>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM PaymentVouchers pv
                               LEFT JOIN Users u ON pv.CreatedBy = u.UserId
                               WHERE pv.VoucherDate >= @StartDate AND pv.VoucherDate <= @EndDate
                               ORDER BY pv.VoucherDate DESC, pv.VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vouchers.Add(MapPaymentVoucher(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.GetPaymentVouchersByDateRange", ex);
                throw;
            }
            return vouchers;
        }

        public List<PaymentVoucher> GetPaymentVouchersByAccount(int accountId)
        {
            var vouchers = new List<PaymentVoucher>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM PaymentVouchers pv
                               LEFT JOIN Users u ON pv.CreatedBy = u.UserId
                               WHERE pv.AccountId = @AccountId
                               ORDER BY pv.VoucherDate DESC, pv.VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vouchers.Add(MapPaymentVoucher(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.GetPaymentVouchersByAccount", ex);
                throw;
            }
            return vouchers;
        }

        public List<PaymentVoucher> GetPaymentVouchersByPaymentMode(string paymentMode)
        {
            var vouchers = new List<PaymentVoucher>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM PaymentVouchers pv
                               LEFT JOIN Users u ON pv.CreatedBy = u.UserId
                               WHERE pv.PaymentMode = @PaymentMode
                               ORDER BY pv.VoucherDate DESC, pv.VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PaymentMode", paymentMode);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vouchers.Add(MapPaymentVoucher(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.GetPaymentVouchersByPaymentMode", ex);
                throw;
            }
            return vouchers;
        }

        public List<PaymentVoucher> GetPaymentVouchersByStatus(string status)
        {
            var vouchers = new List<PaymentVoucher>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT pv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM PaymentVouchers pv
                               LEFT JOIN Users u ON pv.CreatedBy = u.UserId
                               WHERE pv.Status = @Status
                               ORDER BY pv.VoucherDate DESC, pv.VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Status", status);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vouchers.Add(MapPaymentVoucher(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.GetPaymentVouchersByStatus", ex);
                throw;
            }
            return vouchers;
        }

        public string GeneratePaymentVoucherNumber()
        {
            var currentDate = DateTime.Now;
            var yearMonth = currentDate.ToString("yyyyMM");
            var prefix = $"PV{yearMonth}";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT ISNULL(MAX(CAST(SUBSTRING(VoucherNumber, 9, 4) AS INT)), 0) + 1
                               FROM PaymentVouchers 
                               WHERE VoucherNumber LIKE @Prefix + '%'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Prefix", prefix);
                        var nextNumber = Convert.ToInt32(command.ExecuteScalar());
                        return $"{prefix}{nextNumber:D4}";
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.GeneratePaymentVoucherNumber", ex);
                return $"{prefix}0001";
            }
        }

        public decimal GetTotalPaymentsByAccount(int accountId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT ISNULL(SUM(Amount), 0)
                               FROM PaymentVouchers 
                               WHERE AccountId = @AccountId
                               AND VoucherDate >= @StartDate 
                               AND VoucherDate <= @EndDate
                               AND Status = 'ACTIVE'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        return Convert.ToDecimal(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.GetTotalPaymentsByAccount", ex);
                return 0;
            }
        }

        public decimal GetTotalPaymentsByPaymentMode(string paymentMode, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT ISNULL(SUM(Amount), 0)
                               FROM PaymentVouchers 
                               WHERE PaymentMode = @PaymentMode
                               AND VoucherDate >= @StartDate 
                               AND VoucherDate <= @EndDate
                               AND Status = 'ACTIVE'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PaymentMode", paymentMode);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        return Convert.ToDecimal(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PaymentVoucherRepository.GetTotalPaymentsByPaymentMode", ex);
                return 0;
            }
        }

        private PaymentVoucher MapPaymentVoucher(SqlDataReader reader)
        {
            return new PaymentVoucher
            {
                PaymentVoucherId = Convert.ToInt32(reader["PaymentVoucherId"]),
                VoucherNumber = reader["VoucherNumber"].ToString(),
                VoucherDate = Convert.ToDateTime(reader["VoucherDate"]),
                Reference = reader["Reference"]?.ToString(),
                Narration = reader["Narration"].ToString(),
                PaymentMode = reader["PaymentMode"].ToString(),
                Amount = Convert.ToDecimal(reader["Amount"]),
                AccountId = Convert.ToInt32(reader["AccountId"]),
                BankAccountId = reader["BankAccountId"] != DBNull.Value ? Convert.ToInt32(reader["BankAccountId"]) : (int?)null,
                CardNumber = reader["CardNumber"]?.ToString(),
                CardType = reader["CardType"]?.ToString(),
                TransactionId = reader["TransactionId"]?.ToString(),
                BankName = reader["BankName"]?.ToString(),
                ChequeNumber = reader["ChequeNumber"]?.ToString(),
                ChequeDate = reader["ChequeDate"] != DBNull.Value ? Convert.ToDateTime(reader["ChequeDate"]) : (DateTime?)null,
                MobileNumber = reader["MobileNumber"]?.ToString(),
                PaymentReference = reader["PaymentReference"]?.ToString(),
                Status = reader["Status"].ToString(),
                Remarks = reader["Remarks"]?.ToString(),
                CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedByName = reader["CreatedByName"]?.ToString(),
                ModifiedBy = reader["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(reader["ModifiedBy"]) : (int?)null,
                ModifiedDate = reader["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(reader["ModifiedDate"]) : (DateTime?)null,
                ModifiedByName = reader["ModifiedByName"]?.ToString()
            };
        }
    }
}

