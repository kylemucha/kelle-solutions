using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models {
    public class Listing {
        // Primary key: Code
        // Primary key: Code
        [Key]
        public int Code { get; set; }

        // Indicates if the listing is archived
        [Required]
        public bool Archived { get; set; }

        // Timestamp for record creation
        [Column(TypeName = "datetime2")]
        public DateTime? Created { get; set; }

        // Timestamp for the last update
        [Column(TypeName = "datetime2")]
        public DateTime? Updated { get; set; }

        // Foreign key reference to Property; maps to the "Property" column in the table.
        // Renamed from PropertyID to Property to match the new ERD.
        [Required]
        [Column("Property")]  // (Assuming the underlying database column remains named "Property")
        public int FK_Property { get; set; }  // Renamed from PropertyId

        [ForeignKey(nameof(FK_Property))]
        public virtual Property PropertyDetails { get; set; }


        // Listing status stored as a smallint in the database using MyStatusEnum
        [Required]
        [Column("MyStatus", TypeName = "smallint")]
        public MyStatusEnum MyStatus { get; set; }

        // Listing source stored as a smallint using MySourceEnum
        [Required]
        [Column("MySource", TypeName = "smallint")]
        public MySourceEnum MySource { get; set; }

        // Operator represented by a smallint
        [Required]
        [Column("Operator", TypeName = "smallint")]
        public OperatorEnum Operator { get; set; }

        // Team represented by a smallint
        [Required]
        [Column("Team", TypeName = "smallint")]
        public TeamEnum Team { get; set; }
        

        // Visibility stored as a tinyint
        // Visibility stored as a tinyint
        [Required]
        [Column("Visibility", TypeName = "tinyint")]
        public VisibilityEnum Visibility { get; set; }

        // Indicates if the listing is external
        [Column("ExternalListing")]
        public bool? ExternalListing { get; set; }

        // Indicates if the listing is a pocket listing
        [Column("PocketListing")]
        public bool? PocketListing { get; set; }

        // Date when the listing went on market
        [Column("OnMarket", TypeName = "datetime2")]
        public DateTime? OnMarket { get; set; }

        // Date when the listing was published
        [Column("Listed", TypeName = "datetime2")]
        public DateTime? Listed { get; set; }

        // Date when the listing was accepted
        [Column("Accepted", TypeName = "datetime2")]
        public DateTime? Accepted { get; set; }

        // Date when the listing was closed
        [Column("Closed", TypeName = "datetime2")]
        public DateTime? Closed { get; set; }

        // Listing price; mapped as money
        [Column("Price", TypeName = "money")]
        public decimal? Price { get; set; }

        // Actual price; mapped as money
        [Column("Price_Actual", TypeName = "money")]
        public decimal? PriceActual { get; set; }

        // Commission rate with a precision of 6,4
        [Column("CommissionRate", TypeName = "decimal(6,4)")]
        public decimal? CommissionRate { get; set; }

        // Fixed commission amount; mapped as money
        [Column("CommissionFixed", TypeName = "money")]
        public decimal? CommissionFixed { get; set; }

        // Actual commission amount; mapped as money
        [Column("CommissionActual", TypeName = "money")]
        public decimal? CommissionActual { get; set; }

        // Flag to display the listing on the website
        [Column("DisplayOnWebsite")]
        public bool? DisplayOnWebsite { get; set; }

        // Display priority as a tinyint
        [Column("DisplayPriority", TypeName = "tinyint")]
        public byte? DisplayPriority { get; set; }

        // MLS ID with a max length of 30 characters
        [StringLength(30)]
        [Column("MLS_ID", TypeName = "varchar(30)")]
        public string? MLS_ID { get; set; }

        // MLS URL with a max length of 2048 characters
        [StringLength(2048)]
        [Column("MLS_URL", TypeName = "varchar(2048)")]
        public string? MLS_URL { get; set; }

        // Additional comments with a max length of 2048 characters
        [StringLength(2048)]
        [Column("Comments", TypeName = "varchar(2048)")]
        public string? Comments { get; set; }
    }

    public enum MyStatusEnum : short
    {
        [Display(Name = "ON HOLD")]
        OnHold = 0,

        [Display(Name = "OPEN HOUSE")]
        OpenHouse = 1,

        [Display(Name = "ACTIVE")]
        Active = 2,

        [Display(Name = "PENDING")]
        Pending = 3,

        [Display(Name = "CLOSED")]
        Closed = 4,

        [Display(Name = "EXPIRED")]
        Expired = 5,

        [Display(Name = "WITHDRAWN")]
        Withdrawn = 6,

        [Display(Name = "CANCELED")]
        Canceled = 7
    }

    public enum MySourceEnum : short {
        Internal = 0,
        External = 1
    }
}
