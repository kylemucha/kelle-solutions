// Date: 02/11/2025
// Updating Property.cs to reflect Properties Entity in ERD.
// Formerly titled "RealEstateProperty.cs".
// Adding comments to better explain documentation.

// "[Required]" field needed in form and database validations
// "required" keyword complies to C#'s object creation

using System;
using System.ComponentModel.DataAnnotations;

namespace KelleSolutions.Models {
    // Defines the Property entity and its properties (ex: "PropertyId", "PropertyName", etc.)
    public class Property {
        // Unique ID (or APN) for each property (PK), APN will be considered later
        public int PropertyID { get; set; }

        // Track whether the property is archived or not, default false
        public Boolean IsArchived { get; set; } = false;

        // The full address of the property (ex: "6000 J St"), required field
        [Required(ErrorMessage = "Address is required")]
        public required string Address { get; set; }

        // City where the property is located (ex: "Sacramento", "Modesto", etc.), required field
        [Required(ErrorMessage = "City is required")]
        public required string City { get; set; }

        // Stae where the property is located (ex: "California", "Nevada", "Oregon", etc.), required field
        [Required(ErrorMessage = "State is required")]
        public required string State { get; set; }

        // Postal code (ex: "12345", "67890", etc.), required field
        [Required(ErrorMessage = "Zip Code is required")]
        public required string ZipCode { get; set; }

        // Property size in square feet (if applicable)
        public int? Size { get; set; }

        // Land size in square feet (if applicable)
        public int? LandSize { get; set; }

        // Number of bedrooms on the property, required field
        [Required(ErrorMessage = "Number of bedrooms is required")]
        public required int BedCount {get; set; }

        // Number of bathrooms on the property, required field
        [Required(ErrorMessage = "Number of bathrooms is required")]
        public required int BathCount {get; set; }

        // Number of garages on the property, required field
        [Required(ErrorMessage = "Number of garages is required")]
        public required int GarageCount {get; set; }

        // Track whether property is complex or not, default false
        public Boolean IsComplex { get; set; } = false;

        // Number of partials (ex: selling or leasing one room) (if applicable), default 0
        public int PartialCount { get; set; } = 0;

        // Year when the property was constructed, required field
        [Required(ErrorMessage = "Year of construction is required")]
        public required int YearConstructed { get; set; }

        // Track whether property is remodeled or not, default false
        public Boolean IsRemodeled { get; set; } = false;

        // Additional notes made on property (if applicable)
        [StringLength(255, ErrorMessage = "Note cannot exceed 255 characters.")]
        public string Notes {get; set; } = "";

        // Defines the property (ex: "SingleFamilyHome", etc.), required field
        [Required(ErrorMessage = "Property type is required")]
        public required PropertyTypes PropertyType { get; set; }
        
        // List of pre-defined property types
        public enum PropertyTypes {
            SingleFamilyHome,
            MultiFamilyHome,
            Condominium,
            Townhouse,
            Apartment,
            OfficeBuilding,
            RetailStore,
            Warehouse,
            VacantLand,
            Other
        }

        // Date the property was added to the system
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
