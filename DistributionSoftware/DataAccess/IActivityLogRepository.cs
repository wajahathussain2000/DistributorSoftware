using DistributionSoftware.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DistributionSoftware.DataAccess
{
    /// <summary>
    /// Interface for user activity logging and reporting operations
    /// </summary>
    public interface IActivityLogRepository
    {
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
        Task<long> LogUserActivityAsync(int userId, string activityType, string activityDescription, 
                                      string module, string ipAddress = null, string userAgent = null, 
                                      string additionalData = null);
        
        /// <summary>
        /// Logs a user login attempt
        /// </summary>
        /// <param name="userId">The ID of the user attempting to login</param>
        /// <param name="loginStatus">Status of the login attempt (Success, Failed, etc.)</param>
        /// <param name="ipAddress">IP address of the user (optional)</param>
        /// <param name="userAgent">User agent string (optional)</param>
        /// <param name="failureReason">Reason for login failure if applicable (optional)</param>
        /// <returns>The ID of the created login history entry</returns>
        Task<long> LogLoginAttemptAsync(int userId, string loginStatus, string ipAddress = null, 
                                      string userAgent = null, string failureReason = null);
        
        /// <summary>
        /// Updates the logout time for a user session
        /// </summary>
        /// <param name="loginId">The ID of the login history entry to update</param>
        /// <param name="logoutDate">The logout date and time</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> UpdateLogoutTimeAsync(long loginId, DateTime logoutDate);
        
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
        Task<IEnumerable<UserActivityLog>> GetUserActivityReportAsync(DateTime startDate, DateTime endDate, 
                                                                    int? userId = null, int? roleId = null, 
                                                                    string activityType = null);
        
        /// <summary>
        /// Retrieves login history report data based on specified filters
        /// </summary>
        /// <param name="startDate">Start date for the report period</param>
        /// <param name="endDate">End date for the report period</param>
        /// <param name="userId">Specific user ID to filter by (optional)</param>
        /// <param name="loginStatus">Specific login status to filter by (optional)</param>
        /// <returns>Collection of login history entries</returns>
        Task<IEnumerable<LoginHistory>> GetLoginHistoryReportAsync(DateTime startDate, DateTime endDate, 
                                                                 int? userId = null, string loginStatus = null);
        
        /// <summary>
        /// Gets summary statistics for user activities within a date range
        /// </summary>
        /// <param name="startDate">Start date for the statistics period</param>
        /// <param name="endDate">End date for the statistics period</param>
        /// <returns>Dictionary containing activity statistics</returns>
        Task<Dictionary<string, object>> GetActivityStatisticsAsync(DateTime startDate, DateTime endDate);
        
        #endregion
        
        #region Data Cleanup
        
        /// <summary>
        /// Archives old activity logs to maintain database performance
        /// </summary>
        /// <param name="cutoffDate">Date before which logs should be archived</param>
        /// <returns>Number of logs archived</returns>
        Task<int> ArchiveOldLogsAsync(DateTime cutoffDate);
        
        /// <summary>
        /// Permanently deletes very old archived logs
        /// </summary>
        /// <param name="cutoffDate">Date before which archived logs should be deleted</param>
        /// <returns>Number of logs deleted</returns>
        Task<int> DeleteArchivedLogsAsync(DateTime cutoffDate);
        
        #endregion
    }
}

































