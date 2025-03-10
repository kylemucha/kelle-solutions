// Date: 02/11/2025
// Updating Property.cs to reflect the new Properties table schema in the ERD.
// Removed legacy fields and relationships not present in the new table definition.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models {
    public class Property {
        // Primary key: Code
        [Key]
        public int Code { get; set; }

        // Archived flag (bit NOT NULL)
        [Required]
        public bool Archived { get; set; }

        // Visibility stored as tinyint NOT NULL
        [Required]
        [Column(TypeName = "tinyint")]
        public VisibilityEnum Visibility { get; set; }

        // Operator stored as smallint NOT NULL
        [Required]
        [Column(TypeName = "smallint")]
        public OperatorEnum Operator { get; set; }

        // Team stored as smallint NOT NULL
        [Required]
        [Column(TypeName = "smallint")]
        public TeamEnum Team { get; set; }

        // Created timestamp (datetime2)
        [Column(TypeName = "datetime2")]
        public DateTime? Created { get; set; }

        // Updated timestamp (datetime2)
        [Column(TypeName = "datetime2")]
        public DateTime? Updated { get; set; }

        // Validated timestamp (datetime2)
        [Column(TypeName = "datetime2")]
        public DateTime? Validated { get; set; }

        // Property type stored as smallint NOT NULL
        [Required]
        [Column("MyType", TypeName = "smallint")]
        public PropertyTypeEnum MyType { get; set; }

        // Country code (varchar(3))
        [StringLength(3)]
        public string? Country { get; set; }

        // State or province code (varchar(2))
        [StringLength(2)]
        public string? StateProvince { get; set; }

        // County name (varchar(40))
        [StringLength(40)]
        public string? County { get; set; }

        // City name (varchar(40) NOT NULL)
        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public string City { get; set; }

        // Postal code (varchar(10))
        [StringLength(10)]
        public string? Postal { get; set; }

        // Street address (varchar(50))
        [StringLength(50)]
        public string? Street { get; set; }

        // Residential flag (bit)
        public bool? Residential { get; set; }

        // Number of bedrooms (tinyint)
        public byte? Beds { get; set; }

        // Number of bathrooms (tinyint)
        public byte? Baths { get; set; }

        // Number of partial bathrooms (tinyint)
        public byte? BathsPartial { get; set; }

        // Number of garages (tinyint)
        public byte? Garages { get; set; }

        // Year constructed (smallint)
        [Column(TypeName = "smallint")]
        public short? Constructed { get; set; }

        // Remodeled indicator (smallint) - could represent a flag or year remodeled
        [Column(TypeName = "smallint")]
        public short? Remodeled { get; set; }

        // Land square footage (int)
        public int? SqFt_Land { get; set; }

        // Building square footage (int)
        public int? SqFt_Building { get; set; }

        // APN (varchar(30))
        [StringLength(30)]
        public string? APN { get; set; }

        // Additional comments (varchar(2048))
        [StringLength(2048)]
        public string? Comments { get; set; }
    }


    public enum PropertyTypeEnum : short {
        SingleFamilyHome = 0,
        MultiFamilyHome = 1,
        Condominium = 2,
        Townhouse = 3,
        Apartment = 4,
        OfficeBuilding = 5,
        RetailStore = 6,
        Warehouse = 7,
        VacantLand = 8,
        Other = 9
    }
}
