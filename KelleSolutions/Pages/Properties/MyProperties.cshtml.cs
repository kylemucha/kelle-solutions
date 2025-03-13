using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using KelleSolutions.Data;
using KelleSolutions.Models;
using KelleSolutions.Models.ViewModels;

namespace KelleSolutions.Pages.Properties
{
    public class MyPropertiesModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public MyPropertiesModel(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        // Holds the full list of properties (projected to the view model)
        public List<ViewUserProperties> AllProperties { get; set; } = new();

        // Holds the paginated list for display on the page
        public List<ViewUserProperties> ViewUserProperties { get; set; } = new();

        // Paging parameters
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages => (int)Math.Ceiling((double)AllProperties.Count / PageSize);

        public async Task<IActionResult> OnGetAsync()
        {
            // Retrieve all properties using the new field names.
            var propertiesFromDb = await _context.Properties
                .OrderByDescending(p => p.Created)
                .Select(p => new
                {
                    p.Code,
                    p.Created,
                    p.County,
                    p.City,
                    p.Postal,
                    p.Street,
                    p.Beds,
                    p.Baths
                })
                .ToListAsync();

            // Map the database values to the ViewUserProperties view model.
            AllProperties = propertiesFromDb.Select(p => new ViewUserProperties
            {
                ID = p.Code,
                CreationDate = p.Created.HasValue ? DateOnly.FromDateTime(p.Created.Value) : DateOnly.MinValue,
                County = p.County,
                City = p.City,
                Postal = p.Postal ?? "",
                Street = p.Street,
                Bed = p.Beds.HasValue ? p.Beds.Value : 0,
                Bath = p.Baths.HasValue ? p.Baths.Value : 0
            }).ToList();

            // Apply pagination.
            ViewUserProperties = AllProperties
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return Page();
        }
    }
}
