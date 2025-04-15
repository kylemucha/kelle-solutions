using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Properties {
    public class AllPropertiesModel : PageModel {
        private readonly KelleSolutionsDbContext _context;

        public AllPropertiesModel(KelleSolutionsDbContext context) {
            _context = context;
        }

        // Updated to use the new Property model fields.
        public List<Property> AllProperties { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        [BindProperty]
        public Property NewProperty { get; set; } = new Property();

        public async Task<IActionResult> OnGetAsync() {
            var query = _context.Properties.AsQueryable();

            int totalProperties = await query.CountAsync();
            TotalPages = (int)System.Math.Ceiling((double)totalProperties / PageSize);

            AllProperties = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return Page();
        }


        public async Task<IActionResult> OnPostCreateAsync()
{
    // Check if the posted form is valid
    if (!ModelState.IsValid)
    {
        
        return Page();
    }
    
    // Add the new property to the context and save changes
    _context.Properties.Add(NewProperty);
    await _context.SaveChangesAsync();
    
    // Redirect to refresh the page and display the updated list
    return RedirectToPage();
}
    }
}
