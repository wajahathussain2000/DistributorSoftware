using System;
using System.Collections.Generic;

namespace DistributionSoftware.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? RoleId { get; set; } // Reference to Roles table
        public string RoleName { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
        
        // Navigation Properties
        public Role Role { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
