using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models
{
    public class PermissionGroup
    {
        // Primary Key
        [Key]
        [Required]
        public int PermissionGroupID { get; set; }

        // Primary Key
        [Key]
        [Required]
        public int PermissionID { get; set; }

        // Group names: 1 (FullControl), 2 (ReadOnly), 3 (ReadUpdate)
        [MaxLength(15)]
        [Required]
        public string GroupName { get; set; } = null!;

        // Foreign Keys
        public virtual PermissionGroup PermissionGroups { get; set; } = null!;
        public virtual Permission Permission { get; set; } = null!;
    }
}
