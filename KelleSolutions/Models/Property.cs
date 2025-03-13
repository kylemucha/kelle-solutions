using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models
{
    [Table("Properties")]
    public class Property
    {
        // Primary key mapping "Code" column (int, NOT NULL)
        [Key]
        [Column("Code")]
        public int Code { get; set; }

        // Archived: bit, NOT NULL
        [Required]
        public bool Archived { get; set; }

        // Visibility: tinyint, NOT NULL (using byte to match tinyint range)
        [Required]
        public byte Visibility { get; set; }

        // Operator: smallint, NOT NULL (using short)
        [Required]
        public short Operator { get; set; }

        // Team: smallint, NOT NULL (using short)
        [Required]
        public short Team { get; set; }

        // Created: datetime2, optional
        public DateTime? Created { get; set; }

        // Updated: datetime2, optional
        public DateTime? Updated { get; set; }

        // Validated: datetime2, optional
        public DateTime? Validated { get; set; }

        // MyType: smallint, NOT NULL using the PropertyTypeEnum
        [Required]
        [Column("MyType", TypeName = "smallint")]
        public PropertyTypeEnum MyType { get; set; }

        // Country: varchar(3)
        [MaxLength(3)]
        public string? Country { get; set; }

        // StateProvince: varchar(2)
        [MaxLength(2)]
        public string? StateProvince { get; set; }

        // County: varchar(40)
        [MaxLength(40)]
        public string? County { get; set; }

        // City: varchar(40) NOT NULL
        [Required(ErrorMessage = "City is required")]
        [MaxLength(40)]
        public string City { get; set; } = string.Empty;

        // Postal: varchar(10)
        [MaxLength(10)]
        public string? Postal { get; set; }

        // Street: varchar(50)
        [MaxLength(50)]
        public string? Street { get; set; }

        // Residential: bit (maps to bool)
        public bool Residential { get; set; }

        // Beds: tinyint (using byte; optional)
        public byte? Beds { get; set; }

        // Baths: tinyint (using byte; optional)
        public byte? Baths { get; set; }

        // BathsPartial: tinyint (using byte; optional)
        public byte? BathsPartial { get; set; }

        // Garages: tinyint (using byte; optional)
        public byte? Garages { get; set; }

        // Constructed: smallint (using short; optional)
        public short? Constructed { get; set; }

        // Remodeled: smallint (using short; optional)
        public short? Remodeled { get; set; }

        // SqFt_Land: int (optional)
        public int? SqFt_Land { get; set; }

        // SqFt_Building: int (optional)
        public int? SqFt_Building { get; set; }

        // APN: varchar(30)
        [MaxLength(30)]
        public string? APN { get; set; }

        // Comments: varchar(2048)
        [MaxLength(2048)]
        public string? Comments { get; set; }
    }

    // Define the property types as an enum.
    public enum PropertyTypeEnum : short
    {
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
