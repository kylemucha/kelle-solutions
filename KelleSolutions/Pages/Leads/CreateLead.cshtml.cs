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
            // Initialize a new Lead instance using the updated properties.
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }
            try {
                _context.Leads.Add(Lead);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Leads/ViewLead", new { id = Lead.Code });
            }
            catch (System.Exception ex) {
                ModelState.AddModelError("", "An error occurred while saving the lead. Please try again.");
                System.Console.WriteLine($"Error: {ex.Message}");
                return Page();
            }
        }
    }
}