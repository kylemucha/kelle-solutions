// Date: 02/22/2025
// Creating Listing.cs to reflect Listings Entity in ERD.
// Adding comments to better explain documentation.

// "[Required]" field needed in form and database validations
// "required" keyword complies to C#'s object creation

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // connects to existing database relationships

namespace KelleSolutions.Models {
    // Defines the Listing entity and its properties (ex: "ListingID", "Price", etc.)
    public class Listing {
        // Unique ID for each listing (PK)
        [Key]
        public int ListingID { get; set; }

        // Date the listing was added to the system
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Date the listing expires
        // Nullable in case expiration isn't set immediately
        public DateTime? EndDate { get; set; }

        // Current listing status (ex: "Active", "Pending", "Sold", "Withdrawn", etc.)
        [Required(ErrorMessage = "Status is required")]
        [Column(TypeName = "nvarchar(50)")]
        public required StatusTypes Status { get; set; }

        // List of pre-defined status types
        public enum StatusTypes {
            [Display(Name = "On Hold")]
            OnHold,
            [Display(Name = "Open House")]
            OpenHouse,
            Active,
            Pending,
            Closed,
            Expired,
            Withdrawn,
            Canceled
        }

        // Type of listing (ex: "Internal", "External")
        [Required(ErrorMessage = "Team type is required")]
        [Column(TypeName = "nvarchar(50)")]
        public required TeamTypes Team { get; set; }

        // pre-defined team types
        public enum TeamTypes {
            Internal,
            External
        }

        // Listing Price
        [Required(ErrorMessage = "Price is required")]
        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }

        // Description about the property (if applicable)
        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public required string Description { get; set; }

        // The PropertyID to track which

        // Foreign key reference to Property.cs
        [ForeignKey("PropertyID")]
        [Required]
        public int PropertyID { get; set; }

        // Foreign key reference to User.cs
        [ForeignKey("UserID")]
        [Required]
        public string UserID { get; set; }

        public virtual User User { get; set; }
        
        // Nullable in case of a compiler error
        public virtual Property Property { get; set; }
    }
}