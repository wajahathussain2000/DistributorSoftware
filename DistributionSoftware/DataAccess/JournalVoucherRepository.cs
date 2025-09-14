using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class JournalVoucherRepository : IJournalVoucherRepository
    {
        private readonly string _connectionString;

        public JournalVoucherRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public JournalVoucherRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CreateJournalVoucher(JournalVoucher voucher)
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
                            // Insert journal voucher
                            var voucherSql = @"INSERT INTO JournalVouchers 
                                             (VoucherNumber, VoucherDate, Reference, Narration, 
                                              TotalDebit, TotalCredit, CreatedBy, CreatedByName, CreatedDate)
                                             VALUES 
                                             (@VoucherNumber, @VoucherDate, @Reference, @Narration, 
                                              @TotalDebit, @TotalCredit, @CreatedBy, @CreatedByName, @CreatedDate);
                                             SELECT SCOPE_IDENTITY();";

                            int voucherId;
                            using (var command = new SqlCommand(voucherSql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@VoucherNumber", voucher.VoucherNumber);
                                command.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                                command.Parameters.AddWithValue("@Reference", voucher.Reference ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@Narration", voucher.Narration);
                                command.Parameters.AddWithValue("@TotalDebit", voucher.TotalDebit);
                                command.Parameters.AddWithValue("@TotalCredit", voucher.TotalCredit);
                                command.Parameters.AddWithValue("@CreatedBy", voucher.CreatedBy);
                                command.Parameters.AddWithValue("@CreatedByName", voucher.CreatedByName ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@CreatedDate", voucher.CreatedDate);

                                voucherId = Convert.ToInt32(command.ExecuteScalar());
                            }

                            // Insert journal voucher details
                            foreach (var detail in voucher.Details)
                            {
                                var detailSql = @"INSERT INTO JournalVoucherDetails 
                                                (VoucherId, AccountId, DebitAmount, CreditAmount, Narration)
                                                VALUES 
                                                (@VoucherId, @AccountId, @DebitAmount, @CreditAmount, @Narration)";

                                using (var command = new SqlCommand(detailSql, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@VoucherId", voucherId);
                                    command.Parameters.AddWithValue("@AccountId", detail.AccountId);
                                    command.Parameters.AddWithValue("@DebitAmount", detail.DebitAmount);
                                    command.Parameters.AddWithValue("@CreditAmount", detail.CreditAmount);
                                    command.Parameters.AddWithValue("@Narration", detail.Narration ?? (object)DBNull.Value);

                                    command.ExecuteNonQuery();
                                }
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
                DebugHelper.WriteException("JournalVoucherRepository.CreateJournalVoucher", ex);
                throw;
            }
        }

        public bool UpdateJournalVoucher(JournalVoucher voucher)
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
                            // Update journal voucher
                            var voucherSql = @"UPDATE JournalVouchers SET 
                                             VoucherNumber = @VoucherNumber,
                                             VoucherDate = @VoucherDate,
                                             Reference = @Reference,
                                             Narration = @Narration,
                                             TotalDebit = @TotalDebit,
                                             TotalCredit = @TotalCredit,
                                             ModifiedBy = @ModifiedBy,
                                             ModifiedByName = @ModifiedByName,
                                             ModifiedDate = @ModifiedDate
                                             WHERE VoucherId = @VoucherId";

                            using (var command = new SqlCommand(voucherSql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@VoucherId", voucher.VoucherId);
                                command.Parameters.AddWithValue("@VoucherNumber", voucher.VoucherNumber);
                                command.Parameters.AddWithValue("@VoucherDate", voucher.VoucherDate);
                                command.Parameters.AddWithValue("@Reference", voucher.Reference ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@Narration", voucher.Narration);
                                command.Parameters.AddWithValue("@TotalDebit", voucher.TotalDebit);
                                command.Parameters.AddWithValue("@TotalCredit", voucher.TotalCredit);
                                command.Parameters.AddWithValue("@ModifiedBy", voucher.ModifiedBy ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@ModifiedByName", voucher.ModifiedByName ?? (object)DBNull.Value);
                                command.Parameters.AddWithValue("@ModifiedDate", voucher.ModifiedDate ?? (object)DBNull.Value);

                                command.ExecuteNonQuery();
                            }

                            // Delete existing details
                            var deleteSql = "DELETE FROM JournalVoucherDetails WHERE VoucherId = @VoucherId";
                            using (var command = new SqlCommand(deleteSql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@VoucherId", voucher.VoucherId);
                                command.ExecuteNonQuery();
                            }

                            // Insert updated details
                            foreach (var detail in voucher.Details)
                            {
                                var detailSql = @"INSERT INTO JournalVoucherDetails 
                                                (VoucherId, AccountId, DebitAmount, CreditAmount, Narration)
                                                VALUES 
                                                (@VoucherId, @AccountId, @DebitAmount, @CreditAmount, @Narration)";

                                using (var command = new SqlCommand(detailSql, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@VoucherId", voucher.VoucherId);
                                    command.Parameters.AddWithValue("@AccountId", detail.AccountId);
                                    command.Parameters.AddWithValue("@DebitAmount", detail.DebitAmount);
                                    command.Parameters.AddWithValue("@CreditAmount", detail.CreditAmount);
                                    command.Parameters.AddWithValue("@Narration", detail.Narration ?? (object)DBNull.Value);

                                    command.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return true;
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
                DebugHelper.WriteException("JournalVoucherRepository.UpdateJournalVoucher", ex);
                throw;
            }
        }

        public bool DeleteJournalVoucher(int voucherId)
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
                            // Delete details first
                            var deleteDetailsSql = "DELETE FROM JournalVoucherDetails WHERE VoucherId = @VoucherId";
                            using (var command = new SqlCommand(deleteDetailsSql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@VoucherId", voucherId);
                                command.ExecuteNonQuery();
                            }

                            // Delete voucher
                            var deleteVoucherSql = "DELETE FROM JournalVouchers WHERE VoucherId = @VoucherId";
                            using (var command = new SqlCommand(deleteVoucherSql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@VoucherId", voucherId);
                                int rowsAffected = command.ExecuteNonQuery();
                                transaction.Commit();
                                return rowsAffected > 0;
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
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherRepository.DeleteJournalVoucher", ex);
                throw;
            }
        }

        public JournalVoucher GetJournalVoucherById(int voucherId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT VoucherId, VoucherNumber, VoucherDate, Reference, Narration, 
                                      TotalDebit, TotalCredit, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
                               FROM JournalVouchers 
                               WHERE VoucherId = @VoucherId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@VoucherId", voucherId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var voucher = MapJournalVoucher(reader);
                                voucher.Details = GetJournalVoucherDetails(voucherId);
                                return voucher;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherRepository.GetJournalVoucherById", ex);
                throw;
            }
        }

        public JournalVoucher GetJournalVoucherByNumber(string voucherNumber)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT VoucherId, VoucherNumber, VoucherDate, Reference, Narration, 
                                      TotalDebit, TotalCredit, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
                               FROM JournalVouchers 
                               WHERE VoucherNumber = @VoucherNumber";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@VoucherNumber", voucherNumber);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var voucher = MapJournalVoucher(reader);
                                voucher.Details = GetJournalVoucherDetails(voucher.VoucherId);
                                return voucher;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherRepository.GetJournalVoucherByNumber", ex);
                throw;
            }
        }

        public List<JournalVoucher> GetAllJournalVouchers()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT VoucherId, VoucherNumber, VoucherDate, Reference, Narration, 
                                      TotalDebit, TotalCredit, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
                               FROM JournalVouchers 
                               ORDER BY VoucherDate DESC, VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        var vouchers = new List<JournalVoucher>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vouchers.Add(MapJournalVoucher(reader));
                            }
                        }
                        return vouchers;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherRepository.GetAllJournalVouchers", ex);
                throw;
            }
        }

        public List<JournalVoucher> GetJournalVouchersByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT VoucherId, VoucherNumber, VoucherDate, Reference, Narration, 
                                      TotalDebit, TotalCredit, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
                               FROM JournalVouchers 
                               WHERE VoucherDate >= @StartDate AND VoucherDate <= @EndDate
                               ORDER BY VoucherDate DESC, VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Date);
                        command.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1).AddTicks(-1));

                        var vouchers = new List<JournalVoucher>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vouchers.Add(MapJournalVoucher(reader));
                            }
                        }
                        return vouchers;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherRepository.GetJournalVouchersByDateRange", ex);
                throw;
            }
        }

        public List<JournalVoucher> GetJournalVouchersByAccount(int accountId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT DISTINCT v.VoucherId, v.VoucherNumber, v.VoucherDate, v.Reference, v.Narration, 
                                      v.TotalDebit, v.TotalCredit, v.CreatedBy, v.CreatedDate, v.ModifiedBy, v.ModifiedDate
                               FROM JournalVouchers v
                               INNER JOIN JournalVoucherDetails d ON v.VoucherId = d.VoucherId
                               WHERE d.AccountId = @AccountId
                               ORDER BY v.VoucherDate DESC, v.VoucherNumber DESC";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);

                        var vouchers = new List<JournalVoucher>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vouchers.Add(MapJournalVoucher(reader));
                            }
                        }
                        return vouchers;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherRepository.GetJournalVouchersByAccount", ex);
                throw;
            }
        }

        public List<JournalVoucherDetail> GetJournalVoucherDetails(int voucherId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT DetailId, VoucherId, AccountId, DebitAmount, CreditAmount, Narration
                               FROM JournalVoucherDetails 
                               WHERE VoucherId = @VoucherId
                               ORDER BY DetailId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@VoucherId", voucherId);

                        var details = new List<JournalVoucherDetail>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                details.Add(MapJournalVoucherDetail(reader));
                            }
                        }
                        return details;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherRepository.GetJournalVoucherDetails", ex);
                throw;
            }
        }

        public List<JournalVoucherDetail> GetAccountJournalVoucherDetails(int accountId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT d.DetailId, d.VoucherId, d.AccountId, d.DebitAmount, d.CreditAmount, d.Narration,
                                      v.VoucherNumber, v.VoucherDate, v.Reference
                               FROM JournalVoucherDetails d
                               INNER JOIN JournalVouchers v ON d.VoucherId = v.VoucherId
                               WHERE d.AccountId = @AccountId 
                               AND v.VoucherDate >= @StartDate AND v.VoucherDate <= @EndDate
                               ORDER BY v.VoucherDate, v.VoucherNumber, d.DetailId";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);
                        command.Parameters.AddWithValue("@StartDate", startDate.Date);
                        command.Parameters.AddWithValue("@EndDate", endDate.Date.AddDays(1).AddTicks(-1));

                        var details = new List<JournalVoucherDetail>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var detail = MapJournalVoucherDetail(reader);
                                detail.JournalVoucher = new JournalVoucher
                                {
                                    VoucherNumber = reader["VoucherNumber"].ToString(),
                                    VoucherDate = Convert.ToDateTime(reader["VoucherDate"]),
                                    Reference = reader["Reference"]?.ToString()
                                };
                                details.Add(detail);
                            }
                        }
                        return details;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherRepository.GetAccountJournalVoucherDetails", ex);
                throw;
            }
        }

        public decimal GetAccountBalance(int accountId, DateTime asOfDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT 
                               SUM(CASE WHEN d.DebitAmount > 0 THEN d.DebitAmount ELSE 0 END) as TotalDebit,
                               SUM(CASE WHEN d.CreditAmount > 0 THEN d.CreditAmount ELSE 0 END) as TotalCredit
                               FROM JournalVoucherDetails d
                               INNER JOIN JournalVouchers v ON d.VoucherId = v.VoucherId
                               WHERE d.AccountId = @AccountId 
                               AND v.VoucherDate <= @AsOfDate";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);
                        command.Parameters.AddWithValue("@AsOfDate", asOfDate.Date.AddDays(1).AddTicks(-1));

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var totalDebit = reader["TotalDebit"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalDebit"]);
                                var totalCredit = reader["TotalCredit"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalCredit"]);
                                return totalDebit - totalCredit;
                            }
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherRepository.GetAccountBalance", ex);
                throw;
            }
        }

        public string GenerateJournalVoucherNumber()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"SELECT COUNT(*) + 1 FROM JournalVouchers 
                               WHERE VoucherDate >= @Today";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Today", DateTime.Today);

                        var count = Convert.ToInt32(command.ExecuteScalar());
                        return $"JV-{DateTime.Now:yyyy-MM-dd}-{count:D3}";
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("JournalVoucherRepository.GenerateJournalVoucherNumber", ex);
                return $"JV-{DateTime.Now:yyyy-MM-dd}-001";
            }
        }

        public bool ValidateJournalVoucherBalanced(JournalVoucher voucher)
        {
            return voucher.TotalDebit == voucher.TotalCredit;
        }

        private JournalVoucher MapJournalVoucher(SqlDataReader reader)
        {
            return new JournalVoucher
            {
                VoucherId = Convert.ToInt32(reader["VoucherId"]),
                VoucherNumber = reader["VoucherNumber"].ToString(),
                VoucherDate = Convert.ToDateTime(reader["VoucherDate"]),
                Reference = reader["Reference"]?.ToString(),
                Narration = reader["Narration"].ToString(),
                TotalDebit = Convert.ToDecimal(reader["TotalDebit"]),
                TotalCredit = Convert.ToDecimal(reader["TotalCredit"]),
                CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ModifiedBy"]),
                ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"])
            };
        }

        private JournalVoucherDetail MapJournalVoucherDetail(SqlDataReader reader)
        {
            return new JournalVoucherDetail
            {
                DetailId = Convert.ToInt32(reader["DetailId"]),
                VoucherId = Convert.ToInt32(reader["VoucherId"]),
                AccountId = Convert.ToInt32(reader["AccountId"]),
                DebitAmount = Convert.ToDecimal(reader["DebitAmount"]),
                CreditAmount = Convert.ToDecimal(reader["CreditAmount"]),
                Narration = reader["Narration"]?.ToString()
            };
        }
    }
}
