using System.ComponentModel.DataAnnotations;

namespace KelleSolutions.Models
{
    public class Role
    {
        [Key]
        [Required]
        public int RoleID { get; set; }

        [MaxLength(6)]
        [Required]
        public string RoleName { get; set; } = null!;
        // TODO: dotnet ef migrations add RolePermissionGroupEntity
        // command doesnt work until Role table is implemented
    }
}