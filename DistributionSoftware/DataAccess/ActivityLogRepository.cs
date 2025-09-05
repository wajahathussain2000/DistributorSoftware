using DistributionSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;

namespace DistributionSoftware.DataAccess
{
    /// <summary>
    /// Implementation of activity log repository for SQL Server database operations
    /// </summary>
    public class ActivityLogRepository : IActivityLogRepository
    {
        #region Private Fields
        
        private readonly string _connectionString;
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the ActivityLogRepository class
        /// </summary>
        /// <param name="connectionString">Database connection string</param>
        public ActivityLogRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
        
        #endregion
        
        #region Activity Logging
        
        /// <summary>
        /// Logs a user activity to the database
        /// </summary>
        /// <param name="userId">The ID of the user performing the activity</param>
        /// <param name="activityType">Type of activity performed</param>
        /// <param name="activityDescription">Description of the activity</param>
        /// <param name="module">Module where the activity occurred</param>
        /// <param name="ipAddress">IP address of the user (optional)</param>
        /// <param name="userAgent">User agent string (optional)</param>
        /// <param name="additionalData">Additional context data in JSON format (optional)</param>
        /// <returns>The ID of the created log entry</returns>
        public async Task<long> LogUserActivityAsync(int userId, string activityType, string activityDescription, 
                                                  string module, string ipAddress = null, string userAgent = null, 
                                                  string additionalData = null)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(activityType))
                throw new ArgumentException("Invalid user ID or activity type");
                
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Use stored procedure for logging user activity
                    const string query = "sp_LogUserActivity";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@ActivityType", activityType);
                        command.Parameters.AddWithValue("@ActivityDescription", 
                            (object)activityDescription ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Module", 
                            (object)module ?? DBNull.Value);
                        command.Parameters.AddWithValue("@IPAddress", 
                            (object)ipAddress ?? DBNull.Value);
                        command.Parameters.AddWithValue("@UserAgent", 
                            (object)userAgent ?? DBNull.Value);
                        command.Parameters.AddWithValue("@AdditionalData", 
                            (object)additionalData ?? DBNull.Value);
                        
                        var result = await command.ExecuteScalarAsync();
                        return Convert.ToInt64(result);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error logging user activity: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// Logs a user login attempt
        /// </summary>
        /// <param name="userId">The ID of the user attempting to login</param>
        /// <param name="loginStatus">Status of the login attempt (Success, Failed, etc.)</param>
        /// <param name="ipAddress">IP address of the user (optional)</param>
        /// <param name="userAgent">User agent string (optional)</param>
        /// <param name="failureReason">Reason for login failure if applicable (optional)</param>
        /// <returns>The ID of the created login history entry</returns>
        public async Task<long> LogLoginAttemptAsync(int userId, string loginStatus, string ipAddress = null, 
                                                  string userAgent = null, string failureReason = null)
        {
            if (userId <= 0 || string.IsNullOrWhiteSpace(loginStatus))
                throw new ArgumentException("Invalid user ID or login status");
                
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Use stored procedure for logging login attempt
                    const string query = "sp_LogLoginAttempt";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@LoginStatus", loginStatus);
                        command.Parameters.AddWithValue("@IPAddress", 
                            (object)ipAddress ?? DBNull.Value);
                        command.Parameters.AddWithValue("@UserAgent", 
                            (object)userAgent ?? DBNull.Value);
                        command.Parameters.AddWithValue("@FailureReason", 
                            (object)failureReason ?? DBNull.Value);
                        
                        var result = await command.ExecuteScalarAsync();
                        return Convert.ToInt64(result);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error logging login attempt: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// Updates the logout time for a user session
        /// </summary>
        /// <param name="loginId">The ID of the login history entry to update</param>
        /// <param name="logoutDate">The logout date and time</param>
        /// <returns>True if successful, false otherwise</returns>
        public async Task<bool> UpdateLogoutTimeAsync(long loginId, DateTime logoutDate)
        {
            if (loginId <= 0)
                return false;
                
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    const string query = @"
                        UPDATE [dbo].[LoginHistory] 
                        SET [LogoutDate] = @LogoutDate,
                            [SessionDuration] = DATEDIFF(MINUTE, [LoginDate], @LogoutDate)
                        WHERE [LoginId] = @LoginId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoginId", loginId);
                        command.Parameters.AddWithValue("@LogoutDate", logoutDate);
                        
                        var result = await command.ExecuteNonQueryAsync();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error updating logout time: {ex.Message}");
                throw;
            }
        }
        
        #endregion
        
        #region Activity Reporting
        
        /// <summary>
        /// Retrieves user activity report data based on specified filters
        /// </summary>
        /// <param name="startDate">Start date for the report period</param>
        /// <param name="endDate">End date for the report period</param>
        /// <param name="userId">Specific user ID to filter by (optional)</param>
        /// <param name="roleId">Specific role ID to filter by (optional)</param>
        /// <param name="activityType">Specific activity type to filter by (optional)</param>
        /// <returns>Collection of user activity log entries</returns>
        public async Task<IEnumerable<UserActivityLog>> GetUserActivityReportAsync(DateTime startDate, DateTime endDate, 
                                                                                int? userId = null, int? roleId = null, 
                                                                                string activityType = null)
        {
            var activities = new List<UserActivityLog>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Use stored procedure for user activity report
                    const string query = "sp_GetUserActivityReport";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@UserId", 
                            (object)userId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@RoleId", 
                            (object)roleId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ActivityType", 
                            (object)activityType ?? DBNull.Value);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                activities.Add(MapUserActivityLogFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error retrieving user activity report: {ex.Message}");
                throw;
            }
            
            return activities;
        }
        
        /// <summary>
        /// Retrieves login history report data based on specified filters
        /// </summary>
        /// <param name="startDate">Start date for the report period</param>
        /// <param name="endDate">End date for the report period</param>
        /// <param name="userId">Specific user ID to filter by (optional)</param>
        /// <param name="loginStatus">Specific login status to filter by (optional)</param>
        /// <returns>Collection of login history entries</returns>
        public async Task<IEnumerable<LoginHistory>> GetLoginHistoryReportAsync(DateTime startDate, DateTime endDate, 
                                                                             int? userId = null, string loginStatus = null)
        {
            var loginHistory = new List<LoginHistory>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // Use stored procedure for login history report
                    const string query = "sp_GetLoginHistoryReport";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@UserId", 
                            (object)userId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@LoginStatus", 
                            (object)loginStatus ?? DBNull.Value);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                loginHistory.Add(MapLoginHistoryFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error retrieving login history report: {ex.Message}");
                throw;
            }
            
            return loginHistory;
        }
        
        /// <summary>
        /// Gets summary statistics for user activities within a date range
        /// </summary>
        /// <param name="startDate">Start date for the statistics period</param>
        /// <param name="endDate">End date for the statistics period</param>
        /// <returns>Dictionary containing activity statistics</returns>
        public async Task<Dictionary<string, object>> GetActivityStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            var statistics = new Dictionary<string, object>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    const string query = @"
                        SELECT 
                            COUNT(DISTINCT [UserId]) AS TotalActiveUsers,
                            COUNT(*) AS TotalActivities,
                            COUNT(DISTINCT [Module]) AS TotalModules,
                            COUNT(CASE WHEN [ActivityType] = 'Login' THEN 1 END) AS TotalLogins,
                            COUNT(CASE WHEN [ActivityType] = 'Logout' THEN 1 END) AS TotalLogouts
                        FROM [dbo].[UserActivityLog]
                        WHERE [ActivityDate] BETWEEN @StartDate AND @EndDate";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                statistics["TotalActiveUsers"] = reader.GetInt32(0);
                                statistics["TotalActivities"] = reader.GetInt32(1);
                                statistics["TotalModules"] = reader.GetInt32(2);
                                statistics["TotalLogins"] = reader.GetInt32(3);
                                statistics["TotalLogouts"] = reader.GetInt32(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error retrieving activity statistics: {ex.Message}");
                throw;
            }
            
            return statistics;
        }
        
        #endregion
        
        #region Data Cleanup
        
        /// <summary>
        /// Archives old activity logs to maintain database performance
        /// </summary>
        /// <param name="cutoffDate">Date before which logs should be archived</param>
        /// <returns>Number of logs archived</returns>
        public async Task<int> ArchiveOldLogsAsync(DateTime cutoffDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    // This is a placeholder implementation - in production, you would
                    // move data to archive tables or files
                    const string query = @"
                        UPDATE [dbo].[UserActivityLog] 
                        SET [AdditionalData] = 'ARCHIVED_' + [AdditionalData]
                        WHERE [ActivityDate] < @CutoffDate 
                        AND ([AdditionalData] IS NULL OR [AdditionalData] NOT LIKE 'ARCHIVED_%')";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CutoffDate", cutoffDate);
                        
                        var result = await command.ExecuteNonQueryAsync();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error archiving old logs: {ex.Message}");
                throw;
            }
        }
        
        /// <summary>
        /// Permanently deletes very old archived logs
        /// </summary>
        /// <param name="cutoffDate">Date before which archived logs should be deleted</param>
        /// <returns>Number of logs deleted</returns>
        public async Task<int> DeleteArchivedLogsAsync(DateTime cutoffDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    
                    const string query = @"
                        DELETE FROM [dbo].[UserActivityLog] 
                        WHERE [ActivityDate] < @CutoffDate 
                        AND [AdditionalData] LIKE 'ARCHIVED_%'";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CutoffDate", cutoffDate);
                        
                        var result = await command.ExecuteNonQueryAsync();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting archived logs: {ex.Message}");
                throw;
            }
        }
        
        #endregion
        
        #region Private Helper Methods
        
        /// <summary>
        /// Maps a database reader result to a UserActivityLog object
        /// </summary>
        /// <param name="reader">The SQL data reader</param>
        /// <returns>A populated UserActivityLog object</returns>
        private static UserActivityLog MapUserActivityLogFromReader(SqlDataReader reader)
        {
            return new UserActivityLog
            {
                LogId = reader.GetInt64(0),
                UserId = reader.GetInt32(1),
                ActivityType = reader.GetString(2),
                ActivityDescription = reader.IsDBNull(3) ? null : reader.GetString(3),
                Module = reader.IsDBNull(4) ? null : reader.GetString(4),
                IPAddress = reader.IsDBNull(5) ? null : reader.GetString(5),
                UserAgent = reader.IsDBNull(6) ? null : reader.GetString(6),
                ActivityDate = reader.GetDateTime(7),
                AdditionalData = reader.IsDBNull(8) ? null : reader.GetString(8)
            };
        }
        
        /// <summary>
        /// Maps a database reader result to a LoginHistory object
        /// </summary>
        /// <param name="reader">The SQL data reader</param>
        /// <returns>A populated LoginHistory object</returns>
        private static LoginHistory MapLoginHistoryFromReader(SqlDataReader reader)
        {
            return new LoginHistory
            {
                LoginId = reader.GetInt64(0),
                UserId = reader.GetInt32(1),
                LoginDate = reader.GetDateTime(2),
                LogoutDate = reader.IsDBNull(3) ? null : (DateTime?)reader.GetDateTime(3),
                IPAddress = reader.IsDBNull(4) ? null : reader.GetString(4),
                LoginStatus = reader.GetString(5),
                FailureReason = reader.IsDBNull(6) ? null : reader.GetString(6),
                SessionDuration = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7)
            };
        }
        
        #endregion
    }
}









