using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models
{
    public class PageAccess
    {
        [Key]
        [Required]
        public int RoleID { get; set; }

        [MaxLength(6)]
        [Required]
        public string RoleName { get; set; } = null!;
        // TODO: dotnet ef migrations add RolePermissionGroupEntity
        // command doesnt work until PageAccess table is implemented
    }
}