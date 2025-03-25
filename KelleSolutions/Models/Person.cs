using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models
{
    public class Person
    {
        // Primary key: Code (int NOT NULL)
        [Key]
        public int Code { get; set; }

        // Indicates if the person is archived (bit NOT NULL)
        [Required]
        public bool Archived { get; set; }

        // Created timestamp (datetime2 NOT NULL)
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; }

        // Updated timestamp (datetime2 NOT NULL)
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Updated { get; set; }

        // Operator field (smallint NOT NULL)
        [Required]
        [Column(TypeName = "smallint")]
        public OperatorEnum Operator { get; set; }

        // Team field (smallint NOT NULL)
        [Required]
        [Column(TypeName = "smallint")]
        public TeamEnum Team { get; set; }

        // Visibility field (tinyint NOT NULL)
        [Required]
        [Column(TypeName = "tinyint")]
        public VisibilityEnum Visibility { get; set; }

        // Category field (smallint NOT NULL)
        [Required]
        [Column(TypeName = "smallint")]
        public CategoryEnum Category { get; set; }

        // MySource field (smallint NOT NULL)
        [Required]
        [Column("MySource", TypeName = "smallint")]
        public MySourceEnum MySource { get; set; }

        // Verified flag (bit NOT NULL)
        [Required]
        public bool Verified { get; set; }

        // VIP flag (bit NOT NULL)
        [Required]
        public bool VIP { get; set; }

        // First name (varchar(30) NOT NULL)
        [Required(ErrorMessage = "First name is required")]
        [StringLength(30)]
        public required string NameFirst { get; set; }

        // Middle name (varchar(30) NOT NULL)
        [Required(ErrorMessage = "Middle name is required")]
        [StringLength(30)]
        public required string NameMiddle { get; set; }

        // Last name (varchar(30) NOT NULL)
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(30)]
        public required string NameLast { get; set; }

        // Display name (varchar(92) NOT NULL)
        [Required(ErrorMessage = "Display name is required")]
        [StringLength(92)]
        public required string NameDisplay { get; set; }

        // Headline (varchar(80) NOT NULL)
        [Required(ErrorMessage = "Headline is required")]
        [StringLength(80)]
        public required string Headline { get; set; }

        // Primary email (varchar(80) NOT NULL)
        [Required(ErrorMessage = "Primary email is required")]
        [StringLength(80)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string EmailPrimary { get; set; }

        // Secondary email (varchar(80) NOT NULL)
        [Required(ErrorMessage = "Secondary email is required")]
        [StringLength(80)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string EmailSecondary { get; set; }

        // Label for primary email (varchar(20) NOT NULL)
        [Required(ErrorMessage = "Primary email label is required")]
        [StringLength(20)]
        public required string EmailPrimaryLabel { get; set; }

        // Label for secondary email (varchar(20) NOT NULL)
        [Required(ErrorMessage = "Secondary email label is required")]
        [StringLength(20)]
        public required string EmailSecondaryLabel { get; set; }

        // Primary phone (varchar(10) NOT NULL)
        [Required(ErrorMessage = "Primary phone number is required")]
        [StringLength(10)]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public required string PhonePrimary { get; set; }

        // Secondary phone (varchar(10) NOT NULL)
        [Required(ErrorMessage = "Secondary phone number is required")]
        [StringLength(10)]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public required string PhoneSecondary { get; set; }

        // Label for primary phone (varchar(20) NOT NULL)
        [Required(ErrorMessage = "Primary phone label is required")]
        [StringLength(20)]
        public required string PhonePrimaryLabel { get; set; }

        // Label for secondary phone (varchar(20) NOT NULL)
        [Required(ErrorMessage = "Secondary phone label is required")]
        [StringLength(20)]
        public required string PhoneSecondaryLabel { get; set; }

        // Country (varchar(3) NOT NULL)
        [Required(ErrorMessage = "Country is required")]
        [StringLength(3)]
        public required string Country { get; set; }

        // State or province (varchar(2) NOT NULL)
        [Required(ErrorMessage = "State/Province is required")]
        [StringLength(2)]
        public required string StateProvince { get; set; }

        // City (varchar(40) NOT NULL)
        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public required string City { get; set; }

        // Postal code (varchar(10) NOT NULL)
        [Required(ErrorMessage = "Postal code is required")]
        [StringLength(10)]
        public required string Postal { get; set; }

        // Street (varchar(50) NOT NULL)
        [Required(ErrorMessage = "Street is required")]
        [StringLength(50)]
        public required string Street { get; set; }

        // DoNot_Market flag (bit NOT NULL)
        [Required]
        public bool DoNotMarket { get; set; }

        // DoNot_Contact flag (bit NOT NULL)
        [Required]
        public bool DoNotContact { get; set; }

        // Tracking information (varchar(80), optional)
        [StringLength(80)]
        public string? Tracking { get; set; }

        // Comments (varchar(2048), optional)
        [StringLength(2048)]
        public string? Comments { get; set; }

        // Bio (varchar(max), optional)
        public string? Bio { get; set; }
    }

    // Enums for fixed-value fields
    public enum CategoryEnum : short
    {
<<<<<<< Updated upstream
        Category1 = 0,
        Category2 = 1,
        Category3 = 2
=======
        Partner = 0,
        Lead = 1
>>>>>>> Stashed changes
        // Extend with additional categories as needed
    }

    
}
