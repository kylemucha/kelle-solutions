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
            // Initialize an empty Lead object with default values
            Lead = new Lead {
                LeadName = string.Empty,
                Email = string.Empty,
                PhoneNumber = string.Empty,
                AssignedTo = string.Empty,
                LeadSource = string.Empty,
                Status = string.Empty
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
                return RedirectToPage("/Leads/ViewLead", new { id = Lead.LeadID });
            }
            catch (Exception ex) {
                ModelState.AddModelError("", "An error occurred while saving the lead. Please try again.");
                Console.WriteLine($"Error: {ex.Message}");
                return Page();
            }
        }
    }
}
