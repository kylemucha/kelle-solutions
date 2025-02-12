using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Models;
using KelleSolutions.Data;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Properties {
    public class CreatePropertyModel : PageModel {
        private readonly KelleSolutionsDbContext _context;
        public CreatePropertyModel(KelleSolutionsDbContext context){
            _context = context;

            // Initialize empty Property
            Property = new Property {
                Address = string.Empty,
                City = string.Empty,
                State = string.Empty,
                ZipCode = string.Empty,
                BedCount = 0,
                BathCount = 0,
                GarageCount = 0,
                // Assuming default to the current year
                YearConstructed = DateTime.Now.Year, 
                // Assuming a default property type
                PropertyType = Property.PropertyTypes.SingleFamilyHome
            };
        }

        // Bind the Property model to the form
        [BindProperty]
        public Property Property { get; set; }

        public IActionResult OnGet() {
            // Loads the page (no specific logic needed for Get request here)
            return Page();
        }
        public async Task<IActionResult> OnPostAsync() {
            // Validates the form input
            if (!ModelState.IsValid) {
                return Page();
            }
            try {
                // Save the new property to the database
                _context.Properties.Add(Property);
                await _context.SaveChangesAsync();
                // Redirect to the ViewProperty page after saving
                return RedirectToPage("/Properties/ViewProperty", new { id = Property.PropertyID });
            }
            catch (Exception ex) {
                ModelState.AddModelError("", "An error occurred while saving the property. Please try again.");
                Console.WriteLine($"Error: {ex.Message}");
                return Page();
            }
        }
    }
}
