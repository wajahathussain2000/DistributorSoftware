using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class ReceiptVoucherRepository : IReceiptVoucherRepository
    {
        private readonly string _connectionString;

        public ReceiptVoucherRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public ReceiptVoucherRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CreateReceiptVoucher(ReceiptVoucher voucher)
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
                            var sql = @"INSERT INTO ReceiptVouchers 
                                       (VoucherNumber, VoucherDate, Reference, Narration, ReceiptMode, Amount, 
                                        AccountId, BankAccountId, CardNumber, CardType, TransactionId, BankName, 
                                        ChequeNumber, ChequeDate, MobileNumber, ReceiptReference, Status, Remarks, 
                                        CreatedBy, CreatedDate, CreatedByName)
                                       VALUES 
                                       (@VoucherNumber, @VoucherDate, @Reference, @Narration, @ReceiptMode, @Amount, 
                                        @AccountId, @BankAccountId, @CardNumber, @CardType, @TransactionId, @BankName, 
                                        @ChequeNumber, @ChequeDate, @MobileNumber, @ReceiptReference, @Status, @Remarks, 
                                        @CreatedBy, @CreatedDate, @CreatedByName);
                                       SELECT SCOPE_IDENTITY();";

                            int voucherId;
                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@VoucherNumber", voucher.VoucherNumber);
                                command.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                                command.Parameters.AddWithValue("@Reference", voucher.Reference ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@Narration", voucher.Narration);
                                command.Parameters.AddWithValue("@ReceiptMode", voucher.ReceiptMode);
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
                                command.Parameters.AddWithValue("@ReceiptReference", voucher.ReceiptReference ?? (object)DBNull.Value);
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
                DebugHelper.WriteException("ReceiptVoucherRepository.CreateReceiptVoucher", ex);
                throw;
            }
        }

        public bool UpdateReceiptVoucher(ReceiptVoucher voucher)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"UPDATE ReceiptVouchers SET
                               VoucherNumber = @VoucherNumber,
                               VoucherDate = @VoucherDate,
                               Reference = @Reference,
                               Narration = @Narration,
                               ReceiptMode = @ReceiptMode,
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
                               ReceiptReference = @ReceiptReference,
                               Status = @Status,
                               Remarks = @Remarks,
                               ModifiedBy = @ModifiedBy,
                               ModifiedDate = @ModifiedDate,
                               ModifiedByName = @ModifiedByName
                               WHERE ReceiptVoucherId = @ReceiptVoucherId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ReceiptVoucherId", voucher.ReceiptVoucherId);
                        command.Parameters.AddWithValue("@VoucherNumber", voucher.VoucherNumber);
                        command.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                        command.Parameters.AddWithValue("@Reference", voucher.Reference ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Narration", voucher.Narration);
                        command.Parameters.AddWithValue("@ReceiptMode", voucher.ReceiptMode);
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
                        command.Parameters.AddWithValue("@ReceiptReference", voucher.ReceiptReference ?? (object)DBNull.Value);
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
                DebugHelper.WriteException("ReceiptVoucherRepository.UpdateReceiptVoucher", ex);
                throw;
            }
        }

        public bool DeleteReceiptVoucher(int voucherId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "DELETE FROM ReceiptVouchers WHERE ReceiptVoucherId = @ReceiptVoucherId";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ReceiptVoucherId", voucherId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherRepository.DeleteReceiptVoucher", ex);
                throw;
            }
        }

        public ReceiptVoucher GetReceiptVoucherById(int voucherId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM ReceiptVouchers rv
                               LEFT JOIN Users u ON rv.CreatedBy = u.UserId
                               WHERE rv.ReceiptVoucherId = @ReceiptVoucherId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ReceiptVoucherId", voucherId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReceiptVoucher(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherRepository.GetReceiptVoucherById", ex);
                throw;
            }
            return null;
        }

        public ReceiptVoucher GetReceiptVoucherByNumber(string voucherNumber)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM ReceiptVouchers rv
                               LEFT JOIN Users u ON rv.CreatedBy = u.UserId
                               WHERE rv.VoucherNumber = @VoucherNumber";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@VoucherNumber", voucherNumber);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReceiptVoucher(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherRepository.GetReceiptVoucherByNumber", ex);
                throw;
            }
            return null;
        }

        public List<ReceiptVoucher> GetAllReceiptVouchers()
        {
            var vouchers = new List<ReceiptVoucher>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM ReceiptVouchers rv
                               LEFT JOIN Users u ON rv.CreatedBy = u.UserId
                               ORDER BY rv.VoucherDate DESC, rv.VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            vouchers.Add(MapReceiptVoucher(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherRepository.GetAllReceiptVouchers", ex);
                throw;
            }
            return vouchers;
        }

        public List<ReceiptVoucher> GetReceiptVouchersByDateRange(DateTime startDate, DateTime endDate)
        {
            var vouchers = new List<ReceiptVoucher>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM ReceiptVouchers rv
                               LEFT JOIN Users u ON rv.CreatedBy = u.UserId
                               WHERE rv.VoucherDate >= @StartDate AND rv.VoucherDate <= @EndDate
                               ORDER BY rv.VoucherDate DESC, rv.VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vouchers.Add(MapReceiptVoucher(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherRepository.GetReceiptVouchersByDateRange", ex);
                throw;
            }
            return vouchers;
        }

        public List<ReceiptVoucher> GetReceiptVouchersByAccount(int accountId)
        {
            var vouchers = new List<ReceiptVoucher>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM ReceiptVouchers rv
                               LEFT JOIN Users u ON rv.CreatedBy = u.UserId
                               WHERE rv.AccountId = @AccountId
                               ORDER BY rv.VoucherDate DESC, rv.VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vouchers.Add(MapReceiptVoucher(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherRepository.GetReceiptVouchersByAccount", ex);
                throw;
            }
            return vouchers;
        }

        public List<ReceiptVoucher> GetReceiptVouchersByReceiptMode(string receiptMode)
        {
            var vouchers = new List<ReceiptVoucher>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM ReceiptVouchers rv
                               LEFT JOIN Users u ON rv.CreatedBy = u.UserId
                               WHERE rv.ReceiptMode = @ReceiptMode
                               ORDER BY rv.VoucherDate DESC, rv.VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ReceiptMode", receiptMode);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vouchers.Add(MapReceiptVoucher(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherRepository.GetReceiptVouchersByReceiptMode", ex);
                throw;
            }
            return vouchers;
        }

        public List<ReceiptVoucher> GetReceiptVouchersByStatus(string status)
        {
            var vouchers = new List<ReceiptVoucher>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT rv.*, u.FirstName + ' ' + u.LastName AS CreatedByName
                               FROM ReceiptVouchers rv
                               LEFT JOIN Users u ON rv.CreatedBy = u.UserId
                               WHERE rv.Status = @Status
                               ORDER BY rv.VoucherDate DESC, rv.VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Status", status);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vouchers.Add(MapReceiptVoucher(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherRepository.GetReceiptVouchersByStatus", ex);
                throw;
            }
            return vouchers;
        }

        public string GenerateReceiptVoucherNumber()
        {
            var currentDate = DateTime.Now;
            var yearMonth = currentDate.ToString("yyyyMM");
            var prefix = $"RV{yearMonth}";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT ISNULL(MAX(CAST(SUBSTRING(VoucherNumber, 9, 4) AS INT)), 0) + 1
                               FROM ReceiptVouchers 
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
                DebugHelper.WriteException("ReceiptVoucherRepository.GenerateReceiptVoucherNumber", ex);
                return $"{prefix}0001";
            }
        }

        public decimal GetTotalReceiptsByAccount(int accountId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT ISNULL(SUM(Amount), 0)
                               FROM ReceiptVouchers 
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
                DebugHelper.WriteException("ReceiptVoucherRepository.GetTotalReceiptsByAccount", ex);
                return 0;
            }
        }

        public decimal GetTotalReceiptsByReceiptMode(string receiptMode, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT ISNULL(SUM(Amount), 0)
                               FROM ReceiptVouchers 
                               WHERE ReceiptMode = @ReceiptMode
                               AND VoucherDate >= @StartDate 
                               AND VoucherDate <= @EndDate
                               AND Status = 'ACTIVE'";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ReceiptMode", receiptMode);
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        return Convert.ToDecimal(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ReceiptVoucherRepository.GetTotalReceiptsByReceiptMode", ex);
                return 0;
            }
        }

        private ReceiptVoucher MapReceiptVoucher(SqlDataReader reader)
        {
            return new ReceiptVoucher
            {
                ReceiptVoucherId = Convert.ToInt32(reader["ReceiptVoucherId"]),
                VoucherNumber = reader["VoucherNumber"].ToString(),
                VoucherDate = Convert.ToDateTime(reader["VoucherDate"]),
                Reference = reader["Reference"]?.ToString(),
                Narration = reader["Narration"].ToString(),
                ReceiptMode = reader["ReceiptMode"].ToString(),
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
                ReceiptReference = reader["ReceiptReference"]?.ToString(),
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

