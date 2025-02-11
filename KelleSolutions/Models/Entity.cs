using System;
using System.ComponentModel.DataAnnotations;

namespace KelleSolutions.Models
{
    public class Entity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Category { get; set; }
        
        [Required]
        public string Phone { get; set; }
        
        [Required]
        public string Location { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
