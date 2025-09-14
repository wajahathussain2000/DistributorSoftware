using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public class BankStatementRepository : IBankStatementRepository
    {
        private readonly string _connectionString;

        public BankStatementRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? Common.ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        public int Create(BankStatement bankStatement)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO BankStatements (BankAccountId, TransactionDate, Description, ReferenceNumber, Debit, Credit, Balance, IsReconciled, ReconciliationId) VALUES (@BankAccountId, @TransactionDate, @Description, @ReferenceNumber, @Debit, @Credit, @Balance, @IsReconciled, @ReconciliationId); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@BankAccountId", bankStatement.BankAccountId);
                    command.Parameters.AddWithValue("@TransactionDate", bankStatement.TransactionDate);
                    command.Parameters.AddWithValue("@Description", bankStatement.Description);
                    command.Parameters.AddWithValue("@ReferenceNumber", bankStatement.ReferenceNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Debit", bankStatement.Debit);
                    command.Parameters.AddWithValue("@Credit", bankStatement.Credit);
                    command.Parameters.AddWithValue("@Balance", bankStatement.Balance);
                    command.Parameters.AddWithValue("@IsReconciled", bankStatement.IsReconciled);
                    command.Parameters.AddWithValue("@ReconciliationId", bankStatement.ReconciliationId ?? (object)DBNull.Value);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(BankStatement bankStatement)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE BankStatements SET BankAccountId = @BankAccountId, TransactionDate = @TransactionDate, Description = @Description, ReferenceNumber = @ReferenceNumber, Debit = @Debit, Credit = @Credit, Balance = @Balance, IsReconciled = @IsReconciled, ReconciliationId = @ReconciliationId WHERE StatementId = @StatementId", connection))
                {
                    command.Parameters.AddWithValue("@StatementId", bankStatement.StatementId);
                    command.Parameters.AddWithValue("@BankAccountId", bankStatement.BankAccountId);
                    command.Parameters.AddWithValue("@TransactionDate", bankStatement.TransactionDate);
                    command.Parameters.AddWithValue("@Description", bankStatement.Description);
                    command.Parameters.AddWithValue("@ReferenceNumber", bankStatement.ReferenceNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Debit", bankStatement.Debit);
                    command.Parameters.AddWithValue("@Credit", bankStatement.Credit);
                    command.Parameters.AddWithValue("@Balance", bankStatement.Balance);
                    command.Parameters.AddWithValue("@IsReconciled", bankStatement.IsReconciled);
                    command.Parameters.AddWithValue("@ReconciliationId", bankStatement.ReconciliationId ?? (object)DBNull.Value);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int statementId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM BankStatements WHERE StatementId = @StatementId", connection))
                {
                    command.Parameters.AddWithValue("@StatementId", statementId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public BankStatement GetById(int statementId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BankStatements WHERE StatementId = @StatementId", connection))
                {
                    command.Parameters.AddWithValue("@StatementId", statementId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapBankStatement(reader);
                        return null;
                    }
                }
            }
        }

        public List<BankStatement> GetAll()
        {
            var bankStatements = new List<BankStatement>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BankStatements ORDER BY TransactionDate DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        bankStatements.Add(MapBankStatement(reader));
                }
            }
            return bankStatements;
        }

        public List<BankStatement> GetActive()
        {
            var bankStatements = new List<BankStatement>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BankStatements WHERE IsReconciled = 0 ORDER BY TransactionDate DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        bankStatements.Add(MapBankStatement(reader));
                }
            }
            return bankStatements;
        }

        public List<BankStatement> GetByBankAccountId(int bankAccountId)
        {
            var bankStatements = new List<BankStatement>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BankStatements WHERE BankAccountId = @BankAccountId ORDER BY TransactionDate DESC", connection))
                {
                    command.Parameters.AddWithValue("@BankAccountId", bankAccountId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            bankStatements.Add(MapBankStatement(reader));
                    }
                }
            }
            return bankStatements;
        }

        public List<BankStatement> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            var bankStatements = new List<BankStatement>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM BankStatements WHERE 1=1";
                var command = new SqlCommand();

                if (startDate.HasValue)
                {
                    query += " AND TransactionDate >= @StartDate";
                    command.Parameters.AddWithValue("@StartDate", startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query += " AND TransactionDate <= @EndDate";
                    command.Parameters.AddWithValue("@EndDate", endDate.Value);
                }

                if (isActive.HasValue)
                {
                    query += " AND IsReconciled = @IsReconciled";
                    command.Parameters.AddWithValue("@IsReconciled", !isActive.Value);
                }

                query += " ORDER BY TransactionDate DESC";
                command.CommandText = query;
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        bankStatements.Add(MapBankStatement(reader));
                }
            }
            return bankStatements;
        }

        public int GetCount(bool? isActive)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM BankStatements";
                var command = new SqlCommand();

                if (isActive.HasValue)
                {
                    query += " WHERE IsReconciled = @IsReconciled";
                    command.Parameters.AddWithValue("@IsReconciled", !isActive.Value);
                }

                command.CommandText = query;
                command.Connection = connection;

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        private BankStatement MapBankStatement(IDataReader reader)
        {
            return new BankStatement
            {
                StatementId = Convert.ToInt32(reader["StatementId"]),
                BankAccountId = Convert.ToInt32(reader["BankAccountId"]),
                TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                Description = reader["Description"].ToString(),
                ReferenceNumber = reader["ReferenceNumber"] == DBNull.Value ? null : reader["ReferenceNumber"].ToString(),
                Debit = Convert.ToDecimal(reader["Debit"]),
                Credit = Convert.ToDecimal(reader["Credit"]),
                Balance = Convert.ToDecimal(reader["Balance"]),
                IsReconciled = Convert.ToBoolean(reader["IsReconciled"]),
                ReconciliationId = reader["ReconciliationId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ReconciliationId"])
            };
        }
    }
}
