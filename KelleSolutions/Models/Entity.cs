using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models
{
    public class Entity
    {
        // Primary key: Code (int NOT NULL)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Code { get; set; }

        // Indicates if the entity is archived (bit NOT NULL)
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

        // Operator field (smallint NOT NULL) - using shared OperatorEnum
        [Required]
        [Column(TypeName = "smallint")]
        public OperatorEnum Operator { get; set; }

        // Team field (smallint NOT NULL) - using shared TeamEnum
        [Required]
        [Column(TypeName = "smallint")]
        public TeamEnum Team { get; set; }

        // Visibility field (tinyint NOT NULL) - using shared VisibilityEnum
        [Required]
        [Column(TypeName = "tinyint")]
        public VisibilityEnum Visibility { get; set; }

        // Category field (smallint NOT NULL) - using shared CategoryEnum
        [Required]
        [Column(TypeName = "smallint")]
        public CategoryEnum Category { get; set; }

        // Entity name (varchar(80) NOT NULL)
        [Required(ErrorMessage = "Entity Name is required")]
        [StringLength(80)]
        public string EntityName { get; set; }

        // Phone number (varchar(10) NOT NULL)
        [Required(ErrorMessage = "Phone is required")]
        [StringLength(10)]
        public string Phone { get; set; }

        // Country (varchar(3) NOT NULL)
        [Required(ErrorMessage = "Country is required")]
        [StringLength(3)]
        public string Country { get; set; }

        // State or province (varchar(2) NOT NULL)
        [Required(ErrorMessage = "State/Province is required")]
        [StringLength(2)]
        public string StateProvince { get; set; }

        // City (varchar(40) NOT NULL)
        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public string City { get; set; }

        // Postal code (varchar(10) NOT NULL)
        [Required(ErrorMessage = "Postal is required")]
        [StringLength(10)]
        public string Postal { get; set; }

        // Street (varchar(50) NOT NULL)
        [Required(ErrorMessage = "Street is required")]
        [StringLength(50)]
        public string Street { get; set; }

        // DoNot_Market flag (bit NOT NULL)
        [Required]
        public bool DoNot_Market { get; set; }

        // DoNot_Contact flag (bit NOT NULL)
        [Required]
        public bool DoNot_Contact { get; set; }

        // Website (varchar(2048))
        [StringLength(2048)]
        public string? Website { get; set; }

        // Comments (varchar(2048), optional)
        [StringLength(2048)]
        public string? Comments { get; set; }
    }
}
