using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Data;
using KelleSolutions.Models;
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

        [BindProperty]
        public Property Property { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Properties.Add(Property);
            await _context.SaveChangesAsync();

            // Set a success message in TempData
            TempData["SuccessMessage"] = "Property successfully added!";

            // Redirect to the AT_Dashboard page
            return RedirectToPage("/AT_Dashboard");
        }
    }
}
