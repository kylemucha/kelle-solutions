using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Models;
using KelleSolutions.Data;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Properties
{
    public class CreatePropertyModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public CreatePropertyModel(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        // Bind the RealEstateProperty model to the form
        [BindProperty]
        public RealEstateProperty RealEstateProperty { get; set; }

        public IActionResult OnGet()
        {
            // Load the page (no specific logic needed for Get request here)
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate the form input
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save the new property to the database
            _context.Properties.Add(RealEstateProperty);
            await _context.SaveChangesAsync();

            // Redirect to the ViewProperty page after saving
            return RedirectToPage("/Properties/ViewProperty");
        }
    }
}
