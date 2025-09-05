using System;

namespace DistributionSoftware.Models
{
    /// <summary>
    /// Represents a log entry for user activities within the system
    /// </summary>
    public class UserActivityLog
    {
        #region Properties
        
        /// <summary>
        /// Unique identifier for the activity log entry
        /// </summary>
        public long LogId { get; set; }
        
        /// <summary>
        /// Reference to the user who performed the activity
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Type or category of the activity performed
        /// </summary>
        public string ActivityType { get; set; }
        
        /// <summary>
        /// Detailed description of the activity
        /// </summary>
        public string ActivityDescription { get; set; }
        
        /// <summary>
        /// Module or section of the system where the activity occurred
        /// </summary>
        public string Module { get; set; }
        
        /// <summary>
        /// IP address of the user when the activity was performed
        /// </summary>
        public string IPAddress { get; set; }
        
        /// <summary>
        /// User agent string from the client browser/application
        /// </summary>
        public string UserAgent { get; set; }
        
        /// <summary>
        /// Date and time when the activity occurred
        /// </summary>
        public DateTime ActivityDate { get; set; }
        
        /// <summary>
        /// Additional data or context related to the activity (JSON format)
        /// </summary>
        public string AdditionalData { get; set; }
        
        #endregion
        
        #region Navigation Properties
        
        /// <summary>
        /// The user associated with this activity log
        /// </summary>
        public virtual User User { get; set; }
        
        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public UserActivityLog()
        {
            ActivityDate = DateTime.Now;
        }
        
        #endregion
    }
}

