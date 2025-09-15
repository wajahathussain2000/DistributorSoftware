using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly string _connectionString;

        public BankAccountRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? Common.ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        public int Create(BankAccount bankAccount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO BankAccounts (AccountNumber, BankName, Branch, AccountHolderName, AccountType, Address, Phone, Email, ChartOfAccountId, CurrentBalance, LastReconciliationDate, IsActive, CreatedDate, CreatedBy) VALUES (@AccountNumber, @BankName, @Branch, @AccountHolderName, @AccountType, @Address, @Phone, @Email, @ChartOfAccountId, @CurrentBalance, @LastReconciliationDate, @IsActive, @CreatedDate, @CreatedBy); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@AccountNumber", bankAccount.AccountNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BankName", bankAccount.BankName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Branch", bankAccount.Branch ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AccountHolderName", bankAccount.AccountHolderName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AccountType", bankAccount.AccountType ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Address", bankAccount.Address ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", bankAccount.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", bankAccount.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ChartOfAccountId", bankAccount.ChartOfAccountId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CurrentBalance", bankAccount.CurrentBalance);
                    command.Parameters.AddWithValue("@LastReconciliationDate", bankAccount.LastReconciliationDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", bankAccount.IsActive);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@CreatedBy", bankAccount.CreatedBy);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(BankAccount bankAccount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE BankAccounts SET AccountNumber = @AccountNumber, BankName = @BankName, Branch = @Branch, AccountHolderName = @AccountHolderName, AccountType = @AccountType, Address = @Address, Phone = @Phone, Email = @Email, ChartOfAccountId = @ChartOfAccountId, CurrentBalance = @CurrentBalance, LastReconciliationDate = @LastReconciliationDate, IsActive = @IsActive, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy WHERE BankAccountId = @BankAccountId", connection))
                {
                    command.Parameters.AddWithValue("@BankAccountId", bankAccount.BankAccountId);
                    command.Parameters.AddWithValue("@AccountNumber", bankAccount.AccountNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BankName", bankAccount.BankName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Branch", bankAccount.Branch ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AccountHolderName", bankAccount.AccountHolderName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AccountType", bankAccount.AccountType ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Address", bankAccount.Address ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", bankAccount.Phone ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", bankAccount.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ChartOfAccountId", bankAccount.ChartOfAccountId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CurrentBalance", bankAccount.CurrentBalance);
                    command.Parameters.AddWithValue("@LastReconciliationDate", bankAccount.LastReconciliationDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", bankAccount.IsActive);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ModifiedBy", bankAccount.ModifiedBy ?? (object)DBNull.Value);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int bankAccountId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM BankAccounts WHERE BankAccountId = @BankAccountId", connection))
                {
                    command.Parameters.AddWithValue("@BankAccountId", bankAccountId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public BankAccount GetById(int bankAccountId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BankAccounts WHERE BankAccountId = @BankAccountId", connection))
                {
                    command.Parameters.AddWithValue("@BankAccountId", bankAccountId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapBankAccount(reader);
                        return null;
                    }
                }
            }
        }

        public BankAccount GetByAccountNumber(string accountNumber)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BankAccounts WHERE AccountNumber = @AccountNumber", connection))
                {
                    command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapBankAccount(reader);
                        return null;
                    }
                }
            }
        }

        public List<BankAccount> GetAll()
        {
            var bankAccounts = new List<BankAccount>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BankAccounts ORDER BY BankName, AccountNumber", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        bankAccounts.Add(MapBankAccount(reader));
                }
            }
            return bankAccounts;
        }

        public List<BankAccount> GetActive()
        {
            var bankAccounts = new List<BankAccount>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BankAccounts WHERE IsActive = 1 ORDER BY BankName, AccountNumber", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        bankAccounts.Add(MapBankAccount(reader));
                }
            }
            return bankAccounts;
        }

        public List<BankAccount> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            var bankAccounts = new List<BankAccount>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM BankAccounts WHERE 1=1";
                var command = new SqlCommand();

                if (startDate.HasValue)
                {
                    query += " AND CreatedDate >= @StartDate";
                    command.Parameters.AddWithValue("@StartDate", startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query += " AND CreatedDate <= @EndDate";
                    command.Parameters.AddWithValue("@EndDate", endDate.Value);
                }

                if (isActive.HasValue)
                {
                    query += " AND IsActive = @IsActive";
                    command.Parameters.AddWithValue("@IsActive", isActive.Value);
                }

                query += " ORDER BY BankName, AccountNumber";
                command.CommandText = query;
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        bankAccounts.Add(MapBankAccount(reader));
                }
            }
            return bankAccounts;
        }

        public int GetCount(bool? isActive)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM BankAccounts";
                var command = new SqlCommand();

                if (isActive.HasValue)
                {
                    query += " WHERE IsActive = @IsActive";
                    command.Parameters.AddWithValue("@IsActive", isActive.Value);
                }

                command.CommandText = query;
                command.Connection = connection;

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private BankAccount MapBankAccount(IDataReader reader)
        {
            return new BankAccount
            {
                BankAccountId = Convert.ToInt32(reader["BankAccountId"]),
                AccountNumber = reader["AccountNumber"].ToString(),
                BankName = reader["BankName"].ToString(),
                Branch = reader["Branch"] == DBNull.Value ? null : reader["Branch"].ToString(),
                AccountHolderName = reader["AccountHolderName"].ToString(),
                AccountType = reader["AccountType"] == DBNull.Value ? null : reader["AccountType"].ToString(),
                Address = reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                Phone = reader["Phone"] == DBNull.Value ? null : reader["Phone"].ToString(),
                Email = reader["Email"] == DBNull.Value ? null : reader["Email"].ToString(),
                ChartOfAccountId = reader["ChartOfAccountId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ChartOfAccountId"]),
                CurrentBalance = Convert.ToDecimal(reader["CurrentBalance"]),
                LastReconciliationDate = reader["LastReconciliationDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastReconciliationDate"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CreatedBy"]),
                ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ModifiedBy"])
            };
        }
    }
}
