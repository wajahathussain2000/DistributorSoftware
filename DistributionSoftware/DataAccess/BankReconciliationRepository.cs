using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public class BankReconciliationRepository : IBankReconciliationRepository
    {
        private readonly string _connectionString;

        public BankReconciliationRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? Common.ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        public int Create(BankReconciliation bankReconciliation)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO BankReconciliations (BankAccountId, StatementEndDate, BankBalance, BookBalance, ReconciledBy, ReconciliationDate, Status) VALUES (@BankAccountId, @StatementEndDate, @BankBalance, @BookBalance, @ReconciledBy, @ReconciliationDate, @Status); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@BankAccountId", bankReconciliation.BankAccountId);
                    command.Parameters.AddWithValue("@StatementEndDate", bankReconciliation.StatementEndDate);
                    command.Parameters.AddWithValue("@BankBalance", bankReconciliation.BankBalance);
                    command.Parameters.AddWithValue("@BookBalance", bankReconciliation.BookBalance);
                    command.Parameters.AddWithValue("@ReconciledBy", bankReconciliation.ReconciledBy);
                    command.Parameters.AddWithValue("@ReconciliationDate", bankReconciliation.ReconciliationDate);
                    command.Parameters.AddWithValue("@Status", bankReconciliation.Status);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(BankReconciliation bankReconciliation)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE BankReconciliations SET BankAccountId = @BankAccountId, StatementEndDate = @StatementEndDate, BankBalance = @BankBalance, BookBalance = @BookBalance, ReconciledBy = @ReconciledBy, ReconciliationDate = @ReconciliationDate, Status = @Status WHERE ReconciliationId = @ReconciliationId", connection))
                {
                    command.Parameters.AddWithValue("@ReconciliationId", bankReconciliation.ReconciliationId);
                    command.Parameters.AddWithValue("@BankAccountId", bankReconciliation.BankAccountId);
                    command.Parameters.AddWithValue("@StatementEndDate", bankReconciliation.StatementEndDate);
                    command.Parameters.AddWithValue("@BankBalance", bankReconciliation.BankBalance);
                    command.Parameters.AddWithValue("@BookBalance", bankReconciliation.BookBalance);
                    command.Parameters.AddWithValue("@ReconciledBy", bankReconciliation.ReconciledBy);
                    command.Parameters.AddWithValue("@ReconciliationDate", bankReconciliation.ReconciliationDate);
                    command.Parameters.AddWithValue("@Status", bankReconciliation.Status);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int reconciliationId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM BankReconciliations WHERE ReconciliationId = @ReconciliationId", connection))
                {
                    command.Parameters.AddWithValue("@ReconciliationId", reconciliationId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public BankReconciliation GetById(int reconciliationId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BankReconciliations WHERE ReconciliationId = @ReconciliationId", connection))
                {
                    command.Parameters.AddWithValue("@ReconciliationId", reconciliationId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapBankReconciliation(reader);
                        return null;
                    }
                }
            }
        }

        public List<BankReconciliation> GetAll()
        {
            var bankReconciliations = new List<BankReconciliation>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BankReconciliations ORDER BY ReconciliationDate DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        bankReconciliations.Add(MapBankReconciliation(reader));
                }
            }
            return bankReconciliations;
        }

        public List<BankReconciliation> GetByBankAccountId(int bankAccountId)
        {
            var bankReconciliations = new List<BankReconciliation>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BankReconciliations WHERE BankAccountId = @BankAccountId ORDER BY ReconciliationDate DESC", connection))
                {
                    command.Parameters.AddWithValue("@BankAccountId", bankAccountId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            bankReconciliations.Add(MapBankReconciliation(reader));
                    }
                }
            }
            return bankReconciliations;
        }

        public List<BankReconciliation> GetReport(DateTime? startDate, DateTime? endDate)
        {
            var bankReconciliations = new List<BankReconciliation>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM BankReconciliations WHERE 1=1";
                var command = new SqlCommand();

                if (startDate.HasValue)
                {
                    query += " AND ReconciliationDate >= @StartDate";
                    command.Parameters.AddWithValue("@StartDate", startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query += " AND ReconciliationDate <= @EndDate";
                    command.Parameters.AddWithValue("@EndDate", endDate.Value);
                }

                query += " ORDER BY ReconciliationDate DESC";
                command.CommandText = query;
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        bankReconciliations.Add(MapBankReconciliation(reader));
                }
            }
            return bankReconciliations;
        }

        public int GetCount()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT COUNT(*) FROM BankReconciliations", connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private BankReconciliation MapBankReconciliation(IDataReader reader)
        {
            return new BankReconciliation
            {
                ReconciliationId = Convert.ToInt32(reader["ReconciliationId"]),
                BankAccountId = Convert.ToInt32(reader["BankAccountId"]),
                StatementEndDate = Convert.ToDateTime(reader["StatementEndDate"]),
                BankBalance = Convert.ToDecimal(reader["BankBalance"]),
                BookBalance = Convert.ToDecimal(reader["BookBalance"]),
                ReconciledBy = reader["ReconciledBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ReconciledBy"]),
                ReconciliationDate = Convert.ToDateTime(reader["ReconciliationDate"]),
                Status = reader["Status"].ToString()
            };
        }
    }
}
