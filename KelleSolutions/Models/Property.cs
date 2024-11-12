using System;
using System.ComponentModel.DataAnnotations;

namespace KelleSolutions.Models
{
    // Represents a real estate property entity in the system
    public class Property
    {
        // Primary key for the Property entity
        public int Id { get; set; }

        // Street address of the property (e.g., "123 Main St") - required field
        [Required(ErrorMessage = "Street Address is required")]
        public string StreetAddress { get; set; }

        // Optional field for apartment or unit number if applicable (e.g., "Apt 1B")
        public string UnitNumber { get; set; }

        // City where the property is located - required field
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        // State or province where the property is located - required field
        [Required(ErrorMessage = "State/Province is required")]
        public string State { get; set; }

        // Zip or postal code for the property location - required field
        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; }

        // Price of the property - must be a positive decimal value
        [Required(ErrorMessage = "Price is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public decimal Price { get; set; }

        // Optional description of the property, can include details or notes about the property
        public string Description { get; set; }

        // Number of bedrooms in the property - required field, must be at least 1
        [Required(ErrorMessage = "Number of bedrooms is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Bedrooms must be at least 1")]
        public int Bedrooms { get; set; }

        // Number of bathrooms in the property - required field, must be at least 1
        [Required(ErrorMessage = "Number of bathrooms is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Bathrooms must be at least 1")]
        public int Bathrooms { get; set; }

        // Date when the property was listed - defaults to the current date when created
        public DateTime DateListed { get; set; } = DateTime.Now;
    }
}
