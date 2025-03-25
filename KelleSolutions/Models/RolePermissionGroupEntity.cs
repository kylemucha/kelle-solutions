using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KelleSolutions.Models
{
    public class RolePermissionGroupEntity
    {
        // Primary Key
        [Key]
        [Required]
        public string RoleID { get; set; } = null!;

        [Required]
        public int PermissionGroupID { get; set; }

        [Required]
        public int PageAccessID { get; set; }

        // Navigation properties
        public virtual IdentityRole RoleNavigation { get; set; } = null!;
        public virtual PermissionGroup PermissionGroupNavigation { get; set; } = null!;
        public virtual PageAccess PageAccess { get; set; } = null!;
    }
}
