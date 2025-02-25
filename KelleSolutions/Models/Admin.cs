using System;
using System.ComponentModel.DataAnnotations;

namespace KelleSolutions.Models
{
    public class Admin
    {
        // Primary key for the Admin entity
        public int Id { get; set; }

        // Name of the Admin
        [Required(ErrorMessage = "Admin name is required")]
        public required string Name { get; set; }

        // Email of the Admin
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public required string Email { get; set; }

        // Date when the admin was added
        public DateTime DateAdded { get; set; }

        // Any additional information for the admin (optional)
        public string? AdditionalInfo { get; set; }
    }
}
