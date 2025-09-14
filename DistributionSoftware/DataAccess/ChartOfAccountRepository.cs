using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    /// <summary>
    /// Repository implementation for Chart of Accounts data access operations
    /// </summary>
    public class ChartOfAccountRepository : IChartOfAccountRepository
    {
        private readonly string _connectionString;

        public ChartOfAccountRepository()
        {
            _connectionString = ConfigurationManager.DistributionConnectionString;
        }

        public ChartOfAccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Basic CRUD Operations

        public int CreateChartOfAccount(ChartOfAccount account)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"INSERT INTO ChartOfAccounts 
                               (AccountCode, AccountName, AccountType, AccountCategory, ParentAccountId, 
                                AccountLevel, IsActive, IsSystemAccount, NormalBalance, Description, 
                                CreatedBy, CreatedByName, CreatedDate)
                               VALUES 
                               (@AccountCode, @AccountName, @AccountType, @AccountCategory, @ParentAccountId, 
                                @AccountLevel, @IsActive, @IsSystemAccount, @NormalBalance, @Description, 
                                @CreatedBy, @CreatedByName, @CreatedDate);
                               SELECT SCOPE_IDENTITY();";
                    
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AccountCode", account.AccountCode);
                        command.Parameters.AddWithValue("@AccountName", account.AccountName);
                        command.Parameters.AddWithValue("@AccountType", account.AccountType);
                        command.Parameters.AddWithValue("@AccountCategory", account.AccountCategory);
                        command.Parameters.AddWithValue("@ParentAccountId", account.ParentAccountId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@AccountLevel", account.AccountLevel);
                        command.Parameters.AddWithValue("@IsActive", account.IsActive);
                        command.Parameters.AddWithValue("@IsSystemAccount", account.IsSystemAccount);
                        command.Parameters.AddWithValue("@NormalBalance", account.NormalBalance);
                        command.Parameters.AddWithValue("@Description", account.Description ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", account.CreatedBy);
                        command.Parameters.AddWithValue("@CreatedByName", account.CreatedByName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedDate", account.CreatedDate);

                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.CreateChartOfAccount", ex);
                throw;
            }
        }

        public bool UpdateChartOfAccount(ChartOfAccount account)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = @"UPDATE ChartOfAccounts SET 
                               AccountCode = @AccountCode,
                               AccountName = @AccountName,
                               AccountType = @AccountType,
                               AccountCategory = @AccountCategory,
                               ParentAccountId = @ParentAccountId,
                               AccountLevel = @AccountLevel,
                               IsActive = @IsActive,
                               IsSystemAccount = @IsSystemAccount,
                               NormalBalance = @NormalBalance,
                               Description = @Description,
                               ModifiedBy = @ModifiedBy,
                               ModifiedByName = @ModifiedByName,
                               ModifiedDate = @ModifiedDate
                               WHERE AccountId = @AccountId";
                    
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", account.AccountId);
                        command.Parameters.AddWithValue("@AccountCode", account.AccountCode);
                        command.Parameters.AddWithValue("@AccountName", account.AccountName);
                        command.Parameters.AddWithValue("@AccountType", account.AccountType);
                        command.Parameters.AddWithValue("@AccountCategory", account.AccountCategory);
                        command.Parameters.AddWithValue("@ParentAccountId", account.ParentAccountId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@AccountLevel", account.AccountLevel);
                        command.Parameters.AddWithValue("@IsActive", account.IsActive);
                        command.Parameters.AddWithValue("@IsSystemAccount", account.IsSystemAccount);
                        command.Parameters.AddWithValue("@NormalBalance", account.NormalBalance);
                        command.Parameters.AddWithValue("@Description", account.Description ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", account.ModifiedBy);
                        command.Parameters.AddWithValue("@ModifiedByName", account.ModifiedByName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedDate", account.ModifiedDate);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.UpdateChartOfAccount", ex);
                throw;
            }
        }

        public bool DeleteChartOfAccount(int accountId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "DELETE FROM ChartOfAccounts WHERE AccountId = @AccountId";
                    
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.DeleteChartOfAccount", ex);
                throw;
            }
        }

        public ChartOfAccount GetChartOfAccountById(int accountId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT AccountId, AccountCode, AccountName, AccountType, AccountCategory, 
                               ParentAccountId, AccountLevel, IsActive, IsSystemAccount, 
                               NormalBalance, Description, CreatedDate, CreatedBy, 
                               ModifiedDate, ModifiedBy
                        FROM ChartOfAccounts 
                        WHERE AccountId = @AccountId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AccountId", accountId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapChartOfAccount(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetChartOfAccountById", ex);
                throw;
            }
        }

        public ChartOfAccount GetChartOfAccountByCode(string accountCode)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT AccountId, AccountCode, AccountName, AccountType, AccountCategory, 
                               ParentAccountId, AccountLevel, IsActive, IsSystemAccount, 
                               NormalBalance, Description, CreatedDate, CreatedBy, 
                               ModifiedDate, ModifiedBy
                        FROM ChartOfAccounts 
                        WHERE AccountCode = @AccountCode";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AccountCode", accountCode);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapChartOfAccount(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetChartOfAccountByCode", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetAllChartOfAccounts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT * FROM ChartOfAccounts ORDER BY AccountCode";
                    
                    using (var command = new SqlCommand(sql, connection))
                    {
                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetAllChartOfAccounts", ex);
                throw;
            }
        }

        #endregion

        #region Filtered Retrieval Operations

        public List<ChartOfAccount> GetChartOfAccountsByType(string accountType)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetChartOfAccountsByType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AccountType", accountType);

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetChartOfAccountsByType", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetChartOfAccountsByCategory(string accountCategory)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetChartOfAccountsByCategory", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AccountCategory", accountCategory);

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetChartOfAccountsByCategory", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetActiveChartOfAccounts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT * FROM ChartOfAccounts WHERE IsActive = 1 ORDER BY AccountCode";
                    
                    using (var command = new SqlCommand(sql, connection))
                    {
                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetActiveChartOfAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetChartOfAccountsByParent(int parentAccountId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetChartOfAccountsByParent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ParentAccountId", parentAccountId);

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetChartOfAccountsByParent", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetRootLevelAccounts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetRootLevelAccounts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetRootLevelAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetChartOfAccountsByLevel(int accountLevel)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetChartOfAccountsByLevel", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AccountLevel", accountLevel);

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetChartOfAccountsByLevel", ex);
                throw;
            }
        }

        #endregion

        #region Search and Validation Operations

        public List<ChartOfAccount> SearchChartOfAccounts(string searchTerm)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_SearchChartOfAccounts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SearchTerm", searchTerm);

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.SearchChartOfAccounts", ex);
                throw;
            }
        }

        public bool AccountCodeExists(string accountCode, int? excludeAccountId = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_CheckAccountCodeExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AccountCode", accountCode);
                        command.Parameters.AddWithValue("@ExcludeAccountId", excludeAccountId ?? (object)DBNull.Value);

                        var result = command.ExecuteScalar();
                        return Convert.ToBoolean(result);
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.AccountCodeExists", ex);
                throw;
            }
        }

        public bool AccountNameExists(string accountName, int? excludeAccountId = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_CheckAccountNameExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AccountName", accountName);
                        command.Parameters.AddWithValue("@ExcludeAccountId", excludeAccountId ?? (object)DBNull.Value);

                        var result = command.ExecuteScalar();
                        return Convert.ToBoolean(result);
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.AccountNameExists", ex);
                throw;
            }
        }

        public bool ValidateAccountHierarchy(int accountId, int? parentAccountId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_ValidateAccountHierarchy", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AccountId", accountId);
                        command.Parameters.AddWithValue("@ParentAccountId", parentAccountId ?? (object)DBNull.Value);

                        var result = command.ExecuteScalar();
                        return Convert.ToBoolean(result);
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.ValidateAccountHierarchy", ex);
                throw;
            }
        }

        #endregion

        #region Hierarchical Operations

        public List<ChartOfAccount> GetAccountHierarchy()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetAccountHierarchy", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetAccountHierarchy", ex);
                throw;
            }
        }

        public string GetAccountPath(int accountId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetAccountPath", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AccountId", accountId);

                        var result = command.ExecuteScalar();
                        return result?.ToString() ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetAccountPath", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetAllDescendantAccounts(int parentAccountId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetAllDescendantAccounts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ParentAccountId", parentAccountId);

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetDescendantAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetAccountTree(int? rootAccountId = null)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetAccountTree", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RootAccountId", rootAccountId ?? (object)DBNull.Value);

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetAccountTree", ex);
                throw;
            }
        }

        #endregion

        #region Business Logic Operations

        public List<ChartOfAccount> GetDebitAccounts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetDebitAccounts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetDebitAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetCreditAccounts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetCreditAccounts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetCreditAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetAccountsByNormalBalance(string normalBalance)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetAccountsByNormalBalance", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NormalBalance", normalBalance);

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetAccountsByNormalBalance", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetSystemAccounts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetSystemAccounts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetSystemAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetUserDefinedAccounts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetUserDefinedAccounts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetUserDefinedAccounts", ex);
                throw;
            }
        }

        #endregion

        #region Reporting Operations

        public List<ChartOfAccount> GetAccountSummary()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetAccountSummary", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetAccountSummary", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetAccountsWithUsageStats()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetAccountsWithUsageStats", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetAccountsWithUsageStats", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetInactiveAccounts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetInactiveAccounts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var accounts = new List<ChartOfAccount>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(MapChartOfAccount(reader));
                            }
                        }
                        return accounts;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.GetInactiveAccounts", ex);
                throw;
            }
        }

        #endregion

        #region Bulk Operations

        public int BulkUpdateAccountStatus(List<int> accountIds, bool isActive)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_BulkUpdateAccountStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AccountIds", string.Join(",", accountIds));
                        command.Parameters.AddWithValue("@IsActive", isActive);

                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.BulkUpdateAccountStatus", ex);
                throw;
            }
        }

        public int BulkDeleteAccounts(List<int> accountIds)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_BulkDeleteAccounts", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AccountIds", string.Join(",", accountIds));

                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountRepository.BulkDeleteAccounts", ex);
                throw;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Maps a data reader row to ChartOfAccount object
        /// </summary>
        /// <param name="reader">The data reader</param>
        /// <returns>ChartOfAccount object</returns>
        private ChartOfAccount MapChartOfAccount(IDataReader reader)
        {
            return new ChartOfAccount
            {
                AccountId = Convert.ToInt32(reader["AccountId"]),
                AccountCode = reader["AccountCode"].ToString(),
                AccountName = reader["AccountName"].ToString(),
                AccountType = reader["AccountType"].ToString(),
                AccountCategory = reader["AccountCategory"].ToString(),
                ParentAccountId = reader["ParentAccountId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ParentAccountId"]),
                AccountLevel = Convert.ToInt32(reader["AccountLevel"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                IsSystemAccount = Convert.ToBoolean(reader["IsSystemAccount"]),
                NormalBalance = reader["NormalBalance"].ToString(),
                Description = reader["Description"] == DBNull.Value ? null : reader["Description"].ToString(),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                CreatedByName = reader["CreatedByName"] == DBNull.Value ? null : reader["CreatedByName"].ToString(),
                ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ModifiedBy"]),
                ModifiedByName = reader["ModifiedByName"] == DBNull.Value ? null : reader["ModifiedByName"].ToString()
            };
        }

        #endregion
    }
}
