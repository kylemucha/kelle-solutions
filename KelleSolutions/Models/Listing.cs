using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models
{
    public class Listing
    {
        [Key]
        public int ListingID { get; set; }

        [Required]
        public int PropertyID { get; set; }

        [Required]
        public required string AgentID { get; set; }

        [Required]
        [StringLength(50)]
        public required string ListingType { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public required string Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [StringLength(255)]
        public string? Description { get; set; }

        // Navigation properties marked as required and virtual
        [ForeignKey(nameof(PropertyID))]
        public virtual required Property Property { get; set; }

        [ForeignKey(nameof(AgentID))]
        public virtual required User Agent { get; set; }
    }
}
