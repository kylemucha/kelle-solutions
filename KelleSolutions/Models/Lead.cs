// Date: 02/11/2025
// Updating Lead.cs to reflect Leads Entity in ERD.
// Adding comments to better explain documentation.

// "[Required]" field needed in form and database validations
// "required" keyword complies to C#'s object creation

using System;
using System.ComponentModel.DataAnnotations;

namespace KelleSolutions.Models {
    // Defines the Leads entity and its properties (ex: "LeadID", "LeadName", etc.)
    public class Lead {
        // Unique ID for each lead (PK)
        public int LeadID { get; set; }

        // The full name of the lead
        [Required(ErrorMessage = "Lead name is required")]
        public required string LeadName { get; set; }

        // Contact email of the lead
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }

        // Contact phone number of the lead
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public required string PhoneNumber { get; set; }

        // Agent or Broker assigned to manage the lead
        [Required(ErrorMessage = "Assigned agent is required")]
        public required string AssignedTo { get; set; }

        // Where the lead came from (ex: "Referral", "Website", "Social Media")
        [Required(ErrorMessage = "Lead source is required")]
        public required string LeadSource { get; set; }

        // The lead's progress in the sales pipeline
        [Required(ErrorMessage = "Lead status is required")]
        public required string Status { get; set; }

        // Tracks when the lead was first added
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Tracks the last modification date/time for the lead
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}