using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models
{
    public class PageAccess
    {
        [Key]
        [Required]
        public int PageAccessID { get; set; }

        [MaxLength(50)]
        [Required]
        public string PageAccessName { get; set; } = null!;
        
        [MaxLength(200)]
        [Required]
        public string PageUrl { get; set; } = null!;
        
        [MaxLength(255)]
        public string? Description { get; set; }
        
        [Required]
        public bool IsActive { get; set; } = true;
        
        // Audit fields
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        
        [Required]
        public bool Archived { get; set; } = false;
        
        // Organizational fields
        [Required]
        public OperatorEnum Operator { get; set; } = OperatorEnum.Operator1;
        
        [Required]
        public TeamEnum Team { get; set; } = TeamEnum.TeamA;
        
        [Required]
        public VisibilityEnum Visibility { get; set; } = VisibilityEnum.Medium;
        
        // Permission-related fields
        public int? DefaultPermissionGroupID { get; set; }
        
        [ForeignKey("DefaultPermissionGroupID")]
        public virtual PermissionGroup? DefaultPermissionGroup { get; set; }
        
        [Required]
        public int AccessLevel { get; set; } = 1; // Default basic access level
    }
}