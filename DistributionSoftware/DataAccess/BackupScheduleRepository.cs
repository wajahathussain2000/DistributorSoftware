using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    public class BackupScheduleRepository : IBackupScheduleRepository
    {
        private readonly string _connectionString;

        public BackupScheduleRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? Common.ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        public int Create(BackupSchedule backupSchedule)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO BackupSchedules (ScheduleName, BackupType, Frequency, LastRunDate, NextRunDate, BackupLocation, IsActive, CreatedDate, CreatedBy) VALUES (@ScheduleName, @BackupType, @Frequency, @LastRunDate, @NextRunDate, @BackupLocation, @IsActive, @CreatedDate, @CreatedBy); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@ScheduleName", backupSchedule.ScheduleName);
                    command.Parameters.AddWithValue("@BackupType", backupSchedule.BackupType);
                    command.Parameters.AddWithValue("@Frequency", backupSchedule.Frequency);
                    command.Parameters.AddWithValue("@LastRunDate", backupSchedule.LastRunDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NextRunDate", backupSchedule.NextRunDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BackupLocation", backupSchedule.BackupLocation);
                    command.Parameters.AddWithValue("@IsActive", backupSchedule.IsActive);
                    command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@CreatedBy", backupSchedule.CreatedBy);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public bool Update(BackupSchedule backupSchedule)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE BackupSchedules SET ScheduleName = @ScheduleName, BackupType = @BackupType, Frequency = @Frequency, LastRunDate = @LastRunDate, NextRunDate = @NextRunDate, BackupLocation = @BackupLocation, IsActive = @IsActive, ModifiedDate = @ModifiedDate, ModifiedBy = @ModifiedBy WHERE ScheduleId = @ScheduleId", connection))
                {
                    command.Parameters.AddWithValue("@ScheduleId", backupSchedule.ScheduleId);
                    command.Parameters.AddWithValue("@ScheduleName", backupSchedule.ScheduleName);
                    command.Parameters.AddWithValue("@BackupType", backupSchedule.BackupType);
                    command.Parameters.AddWithValue("@Frequency", backupSchedule.Frequency);
                    command.Parameters.AddWithValue("@LastRunDate", backupSchedule.LastRunDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@NextRunDate", backupSchedule.NextRunDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BackupLocation", backupSchedule.BackupLocation);
                    command.Parameters.AddWithValue("@IsActive", backupSchedule.IsActive);
                    command.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                    command.Parameters.AddWithValue("@ModifiedBy", backupSchedule.ModifiedBy);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int scheduleId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM BackupSchedules WHERE ScheduleId = @ScheduleId", connection))
                {
                    command.Parameters.AddWithValue("@ScheduleId", scheduleId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public BackupSchedule GetById(int scheduleId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BackupSchedules WHERE ScheduleId = @ScheduleId", connection))
                {
                    command.Parameters.AddWithValue("@ScheduleId", scheduleId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapBackupSchedule(reader);
                        return null;
                    }
                }
            }
        }

        public BackupSchedule GetByName(string scheduleName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BackupSchedules WHERE ScheduleName = @ScheduleName", connection))
                {
                    command.Parameters.AddWithValue("@ScheduleName", scheduleName);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapBackupSchedule(reader);
                        return null;
                    }
                }
            }
        }

        public List<BackupSchedule> GetAll()
        {
            var backupSchedules = new List<BackupSchedule>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BackupSchedules ORDER BY ScheduleName", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        backupSchedules.Add(MapBackupSchedule(reader));
                }
            }
            return backupSchedules;
        }

        public List<BackupSchedule> GetActive()
        {
            var backupSchedules = new List<BackupSchedule>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM BackupSchedules WHERE IsActive = 1 ORDER BY ScheduleName", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        backupSchedules.Add(MapBackupSchedule(reader));
                }
            }
            return backupSchedules;
        }

        public List<BackupSchedule> GetReport(DateTime? startDate, DateTime? endDate, bool? isActive)
        {
            var backupSchedules = new List<BackupSchedule>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM BackupSchedules WHERE 1=1";
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

                query += " ORDER BY ScheduleName";
                command.CommandText = query;
                command.Connection = connection;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        backupSchedules.Add(MapBackupSchedule(reader));
                }
            }
            return backupSchedules;
        }

        public int GetCount(bool? isActive)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT COUNT(*) FROM BackupSchedules";
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

        private BackupSchedule MapBackupSchedule(IDataReader reader)
        {
            return new BackupSchedule
            {
                ScheduleId = Convert.ToInt32(reader["ScheduleId"]),
                ScheduleName = reader["ScheduleName"].ToString(),
                BackupType = reader["BackupType"].ToString(),
                Frequency = reader["Frequency"].ToString(),
                LastRunDate = reader["LastRunDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastRunDate"]),
                NextRunDate = reader["NextRunDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["NextRunDate"]),
                BackupLocation = reader["BackupLocation"].ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = reader["CreatedBy"] == DBNull.Value ? 0 : Convert.ToInt32(reader["CreatedBy"])
            };
        }
    }
}
