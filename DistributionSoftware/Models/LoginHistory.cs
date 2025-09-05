using System;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents a login history entry for user authentication tracking
    /// </summary>
    public class LoginHistory
    {
        #region Properties
        
        /// <summary>
        /// Unique identifier for the login history entry
        /// </summary>
        public long LoginId { get; set; }
        
        /// <summary>
        /// Reference to the user who attempted to login
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Date and time when the login attempt occurred
        /// </summary>
        public DateTime LoginDate { get; set; }
        
        /// <summary>
        /// Date and time when the user logged out (null if still active)
        /// </summary>
        public DateTime? LogoutDate { get; set; }
        
        /// <summary>
        /// IP address of the user during the login attempt
        /// </summary>
        public string IPAddress { get; set; }
        
        /// <summary>
        /// User agent string from the client browser/application
        /// </summary>
        public string UserAgent { get; set; }
        
        /// <summary>
        /// Status of the login attempt (Success, Failed, etc.)
        /// </summary>
        public string LoginStatus { get; set; }
        
        /// <summary>
        /// Reason for login failure if applicable
        /// </summary>
        public string FailureReason { get; set; }
        
        /// <summary>
        /// Duration of the user session in minutes
        /// </summary>
        public int? SessionDuration { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// The user associated with this login history
        /// </summary>
        public virtual User User { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginHistory()
        {
            LoginDate = DateTime.Now;
            LoginStatus = "Success";
        }
        
        #endregion
        
        #region Helper Methods
        
        /// <summary>
        /// Calculates the session duration in minutes
        /// </summary>
        /// <returns>Session duration in minutes, or null if still active</returns>
        public int? CalculateSessionDuration()
        {
            if (LogoutDate.HasValue)
            {
                return (int)(LogoutDate.Value - LoginDate).TotalMinutes;
            }
            return null;
        }
        
        /// <summary>
        /// Checks if the user session is currently active
        /// </summary>
        /// <returns>True if the session is active, false otherwise</returns>
        public bool IsSessionActive()
        {
            return !LogoutDate.HasValue;
        }
        
        #endregion
    }
}

