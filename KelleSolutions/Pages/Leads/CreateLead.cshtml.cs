using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Models;
using KelleSolutions.Data;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Leads {
    public class CreateLeadModel : PageModel {
        private readonly KelleSolutionsDbContext _context;

        public CreateLeadModel(KelleSolutionsDbContext context) {
    _context = context;
    // Initialize a new Lead instance with default values for all required properties.
    Lead = new Lead {
        NameFirst = string.Empty,
        NameMiddle = string.Empty,
        NameLast = string.Empty,
        Email = string.Empty,
        Phone = string.Empty,
        Country = string.Empty,
        StateProvince = string.Empty,
        City = string.Empty,
        Postal = string.Empty,
        Street = string.Empty,
        OrganizationName = string.Empty,
        OrganizationTitle = string.Empty,
        Tracking = string.Empty
    };
}

        // Bind the Lead model to the form
        [BindProperty]
        public Lead Lead { get; set; }

        public IActionResult OnGet() {
            // Load the page (no specific logic needed for GET request here)
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            // Validate the form input
            if (!ModelState.IsValid) {
                return Page();
            }
            try {
                // Save the new lead to the database
                _context.Leads.Add(Lead);
                await _context.SaveChangesAsync();
                // Redirect to the ViewLead page after saving
                return RedirectToPage("/Leads/ViewLead", new { id = Lead.Code });
            }
            catch (Exception ex) {
                ModelState.AddModelError("", "An error occurred while saving the lead. Please try again.");
                Console.WriteLine($"Error: {ex.Message}");
                return Page();
            }
        }
    }
}
