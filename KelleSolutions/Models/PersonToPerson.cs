using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KelleSolutions.Models
{
    public class PersonToPerson
    {
        // Primary Key
        [Key]
        [Required]
        public required int Code {get;set;}

        [Required]
        public required bool Deprecated {get;set;}

        [Required]
        [Column("DateTime2")]
        public required DateTime Created {get;set;}

        public required short Creator {get;set;}

        public int PersonId {get;set;}

        public int Person2Id{get;set;}

        [Required]
        public required short Relation{get;set;}

        [Required]
        [MaxLength(100)]
        public required string Comments {get;set;}

        [ForeignKey("PersonId")]
        public Person ? Person {get;set;}

        [ForeignKey("Person2Id")]
        public Person ? Person2 {get; set;}
        public int RelatedId { get; set; }
    }
}