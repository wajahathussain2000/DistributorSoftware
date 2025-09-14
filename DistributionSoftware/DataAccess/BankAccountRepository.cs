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
                using (var command = new SqlCommand("INSERT INTO BankAccounts (AccountNumber, BankName, BranchName, AccountHolder, CurrentBalance, IsActive, CreatedDate, CreatedBy) VALUES (@AccountNumber, @BankName, @BranchName, @AccountHolder, @CurrentBalance, @IsActive, @CreatedDate, @CreatedBy); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@AccountNumber", bankAccount.AccountNumber);
                    command.Parameters.AddWithValue("@BankName", bankAccount.BankName);
                    command.Parameters.AddWithValue("@BranchName", bankAccount.BranchName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AccountHolder", bankAccount.AccountHolder);
                    command.Parameters.AddWithValue("@CurrentBalance", bankAccount.CurrentBalance);
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
                using (var command = new SqlCommand("UPDATE BankAccounts SET AccountNumber = @AccountNumber, BankName = @BankName, BranchName = @BranchName, AccountHolder = @AccountHolder, CurrentBalance = @CurrentBalance, IsActive = @IsActive, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy WHERE BankAccountId = @BankAccountId", connection))
                {
                    command.Parameters.AddWithValue("@BankAccountId", bankAccount.BankAccountId);
                    command.Parameters.AddWithValue("@AccountNumber", bankAccount.AccountNumber);
                    command.Parameters.AddWithValue("@BankName", bankAccount.BankName);
                    command.Parameters.AddWithValue("@BranchName", bankAccount.BranchName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AccountHolder", bankAccount.AccountHolder);
                    command.Parameters.AddWithValue("@CurrentBalance", bankAccount.CurrentBalance);
                    command.Parameters.AddWithValue("@IsActive", bankAccount.IsActive);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ModifiedBy", bankAccount.ModifiedBy);

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
                BranchName = reader["BranchName"] == DBNull.Value ? null : reader["BranchName"].ToString(),
                AccountHolder = reader["AccountHolder"].ToString(),
                CurrentBalance = Convert.ToDecimal(reader["CurrentBalance"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CreatedBy"])
            };
        }
    }
}
