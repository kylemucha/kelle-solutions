using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;    // handles user authentication!
using System.Collections.Generic;       // defines database relationships
using System.Linq;                      // defines LINQ queries (ex: "Where", "OrderBy", "Skip", "Take", etc.)
using System.Threading.Tasks;           // supports non-blocking queries (ex: "async", "wait")
using KelleSolutions.Data;              // imports KelleSolutionsDbContext.cs
using KelleSolutions.Models;            // imports model classes, like "Property.cs" as Property and "User.cs" as User

namespace KelleSolutions.Pages.Properties {
    public class AllPropertiesModel : PageModel {
        // database context for querying properties
        private readonly KelleSolutionsDbContext _context;

        // constructor that injects database context
        public AllPropertiesModel(KelleSolutionsDbContext context) {
            _context = context;
        }

        // storing the list of all properties
        public List<Property> AllProperties { get; set; } = new();

        // page display properties
        // defaults to 10 properties per page
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        // the current page number
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        // the total number of pages
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync() {
            // retreives all properties
            var query = _context.Properties.AsQueryable();
            
            // get the total property count for page display properties
            int totalProperties = await query.CountAsync();
            TotalPages = (int)System.Math.Ceiling((double)totalProperties / PageSize);

            // apply page display properties
            AllProperties = await query
                // skips items from previous pages
                .Skip((PageNumber - 1) * PageSize)
                // limit results to just PageSize
                .Take(PageSize)
                .ToListAsync();

            return Page();
        }
    }
}