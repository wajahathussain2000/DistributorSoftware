using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class BackupHistoryRepository : IBackupHistoryRepository
    {
        private readonly string _connectionString;

        public BackupHistoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Create(BackupHistory backupHistory)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        INSERT INTO BackupHistories 
                        (BackupScheduleId, BackupFileName, BackupPath, BackupSize, BackupDate, Status, ErrorMessage, CreatedDate, CreatedBy)
                        VALUES 
                        (@BackupScheduleId, @BackupFileName, @BackupPath, @BackupSize, @BackupDate, @Status, @ErrorMessage, @CreatedDate, @CreatedBy);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    command.Parameters.AddWithValue("@BackupScheduleId", backupHistory.BackupScheduleId);
                    command.Parameters.AddWithValue("@BackupFileName", backupHistory.BackupFileName);
                    command.Parameters.AddWithValue("@BackupPath", backupHistory.BackupPath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BackupSize", backupHistory.BackupSize);
                    command.Parameters.AddWithValue("@BackupDate", backupHistory.BackupDate);
                    command.Parameters.AddWithValue("@Status", backupHistory.Status ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ErrorMessage", backupHistory.ErrorMessage ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedDate", backupHistory.CreatedDate);
                    command.Parameters.AddWithValue("@CreatedBy", backupHistory.CreatedBy);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(BackupHistory backupHistory)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        UPDATE BackupHistories 
                        SET BackupScheduleId = @BackupScheduleId, BackupFileName = @BackupFileName, 
                            BackupPath = @BackupPath, BackupSize = @BackupSize, BackupDate = @BackupDate,
                            Status = @Status, ErrorMessage = @ErrorMessage
                        WHERE BackupHistoryId = @BackupHistoryId";

                    command.Parameters.AddWithValue("@BackupHistoryId", backupHistory.BackupHistoryId);
                    command.Parameters.AddWithValue("@BackupScheduleId", backupHistory.BackupScheduleId);
                    command.Parameters.AddWithValue("@BackupFileName", backupHistory.BackupFileName);
                    command.Parameters.AddWithValue("@BackupPath", backupHistory.BackupPath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BackupSize", backupHistory.BackupSize);
                    command.Parameters.AddWithValue("@BackupDate", backupHistory.BackupDate);
                    command.Parameters.AddWithValue("@Status", backupHistory.Status ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ErrorMessage", backupHistory.ErrorMessage ?? (object)DBNull.Value);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int historyId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM BackupHistories WHERE BackupHistoryId = @BackupHistoryId";
                    command.Parameters.AddWithValue("@BackupHistoryId", historyId);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public BackupHistory GetById(int historyId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM BackupHistories WHERE BackupHistoryId = @BackupHistoryId";
                    command.Parameters.AddWithValue("@BackupHistoryId", historyId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return GetBackupHistoryFromReader(reader);
                    }
                }
            }
            return null;
        }

        public List<BackupHistory> GetAll()
        {
            var histories = new List<BackupHistory>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM BackupHistories ORDER BY BackupDate DESC";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            histories.Add(GetBackupHistoryFromReader(reader));
                        }
                    }
                }
            }
            return histories;
        }

        public List<BackupHistory> GetByScheduleId(int scheduleId)
        {
            var histories = new List<BackupHistory>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM BackupHistories WHERE BackupScheduleId = @BackupScheduleId ORDER BY BackupDate DESC";
                    command.Parameters.AddWithValue("@BackupScheduleId", scheduleId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            histories.Add(GetBackupHistoryFromReader(reader));
                        }
                    }
                }
            }
            return histories;
        }

        public List<BackupHistory> GetReport(DateTime? startDate, DateTime? endDate)
        {
            var histories = new List<BackupHistory>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM BackupHistories WHERE 1=1";
                    
                    if (startDate.HasValue)
                    {
                        command.CommandText += " AND BackupDate >= @StartDate";
                        command.Parameters.AddWithValue("@StartDate", startDate.Value);
                    }
                    
                    if (endDate.HasValue)
                    {
                        command.CommandText += " AND BackupDate <= @EndDate";
                        command.Parameters.AddWithValue("@EndDate", endDate.Value);
                    }
                    
                    command.CommandText += " ORDER BY BackupDate DESC";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            histories.Add(GetBackupHistoryFromReader(reader));
                        }
                    }
                }
            }
            return histories;
        }

        public int GetCount()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM BackupHistories";

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private BackupHistory GetBackupHistoryFromReader(SqlDataReader reader)
        {
            return new BackupHistory
            {
                BackupHistoryId = Convert.ToInt32(reader["BackupHistoryId"]),
                BackupScheduleId = Convert.ToInt32(reader["BackupScheduleId"]),
                BackupFileName = reader["BackupFileName"].ToString(),
                BackupPath = reader["BackupPath"] == DBNull.Value ? null : reader["BackupPath"].ToString(),
                BackupSize = Convert.ToInt64(reader["BackupSize"]),
                BackupDate = Convert.ToDateTime(reader["BackupDate"]),
                Status = reader["Status"] == DBNull.Value ? null : reader["Status"].ToString(),
                ErrorMessage = reader["ErrorMessage"] == DBNull.Value ? null : reader["ErrorMessage"].ToString(),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CreatedBy"])
            };
        }
    }
}
