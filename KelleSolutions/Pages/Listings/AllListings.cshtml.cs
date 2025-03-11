using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KelleSolutions.Data;
using KelleSolutions.Models;

namespace KelleSolutions.Pages.Listings {
    public class AllListingsModel : PageModel {
        private readonly KelleSolutionsDbContext _context;

        public AllListingsModel(KelleSolutionsDbContext context) {
            _context = context;
        }

        public List<ViewUserListings> AllListings { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync() {
            // Retrieve all listings and include related Property and its User details
            var query = _context.Listings
                .Include(l => l.Property)
                .ThenInclude(p => p.User)
                .Select(l => new ViewUserListings {
                    ListingID = l.Code,
                    Date = l.Created ?? DateTime.MinValue,
                    Status = l.MyStatus.ToString(),
                    OperatorName = l.Property.User.FirstName + " " + l.Property.User.LastName,
                    Affiliation = l.Property.User.Affiliation ?? "N/A",
                    Price = l.Price ?? 0,
                    Address = l.Property.Address
                })
                .AsQueryable();

            int totalListings = await query.CountAsync();
            TotalPages = (int)Math.Ceiling((double)totalListings / PageSize);

            AllListings = await query
                // Newest listings appear first using the Created date
                .OrderByDescending(l => l.Date)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return Page();
        }

        public class ViewUserListings {
            public int ListingID { get; set; }
            public DateTime Date { get; set; }
            public string Status { get; set; }
            public string OperatorName { get; set; }
            public string Affiliation { get; set; }
            public decimal Price { get; set; }
            public string Address { get; set; }
        }
    }
}
