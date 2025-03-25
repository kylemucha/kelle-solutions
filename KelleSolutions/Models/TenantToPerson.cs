using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models
{
    [Table("TenantToPeople")]
    public class TenantToPerson
    {
        public int TenantID { get; set; }
        public int PersonID { get; set; }

        [Required]
        public int TenantToPersonID { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Role { get; set; } = string.Empty;

        // FK to specific user assignment (optional for filtering)
        public string? AssignedToUserId { get; set; }

        // Navigation Properties
        public Tenant Tenant { get; set; } = null!;
        public Person Person { get; set; } = null!;
        public User? AssignedUser { get; set; }
    }
}