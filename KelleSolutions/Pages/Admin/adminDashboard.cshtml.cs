using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Data;
using KelleSolutions.Models;
using KelleSolutions.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KelleSolutions.Pages.Admin
{
    public class adminDashboardModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public List<ActionEntity> ActionEntities { get; set; }
        public Dictionary<string, int> ListingCounts { get; set; }
        public List<ViewUserListings> OpenHouseListings { get; set; }
        public List<ViewUserListings> ActiveListings { get; set; }
        public List<Property> AllProperties { get; set; }

        public adminDashboardModel(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            // Get all actions ordered by due date
            ActionEntities = await _context.ActionEntities
                .OrderBy(a => a.Due)
                .ToListAsync();

            // Get listing counts by status
            ListingCounts = new Dictionary<string, int>
            {
                { "Active", await _context.Listings.CountAsync(l => l.MyStatus == MyStatusEnum.Active) },
                { "OpenHouse", await _context.Listings.CountAsync(l => l.MyStatus == MyStatusEnum.OpenHouse) },
                { "OnHold", await _context.Listings.CountAsync(l => l.MyStatus == MyStatusEnum.OnHold) },
                { "Closed", await _context.Listings.CountAsync(l => l.MyStatus == MyStatusEnum.Closed) }
            };

            // Get open house listings with their property details
            OpenHouseListings = await _context.Listings
                .Include(l => l.PropertyDetails)
                .Where(l => l.MyStatus == MyStatusEnum.OpenHouse)
                .Select(l => new ViewUserListings
                {
                    ID = l.Code,
                    Street = l.PropertyDetails.Street,
                    Status = l.MyStatus.ToString()
                })
                .ToListAsync();

            // Get active listings with their property details
            ActiveListings = await _context.Listings
                .Include(l => l.PropertyDetails)
                .Where(l => l.MyStatus == MyStatusEnum.Active)
                .Select(l => new ViewUserListings
                {
                    ID = l.Code,
                    Street = l.PropertyDetails.Street,
                    Status = l.MyStatus.ToString()
                })
                .ToListAsync();

            // Get all properties
            AllProperties = await _context.Properties
                .OrderByDescending(p => p.Created)
                .ToListAsync();
        }
    }
} 