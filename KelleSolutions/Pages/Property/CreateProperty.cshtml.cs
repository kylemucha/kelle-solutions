using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Data;
using KelleSolutions.Models;

namespace KelleSolutions.Pages.Properties
{
    public class CreatePropertyModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public CreatePropertyModel(KelleSolutionsDbContext context)
        {
            _context = context;
            Property = new Property();
        }

        [BindProperty]
        public Property? Property { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Property == null || !ModelState.IsValid)
                {
                    return Page();
                }


            _context.Properties.Add(Property);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index"); // Redirect to the properties index page after creation
        }
    }
}
