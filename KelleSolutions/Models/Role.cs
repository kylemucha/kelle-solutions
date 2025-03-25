using System.ComponentModel.DataAnnotations;

namespace KelleSolutions.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
