using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Data;
using KelleSolutions.Models;
using Microsoft.EntityFrameworkCore;

namespace KelleSolutions.Pages.Admin
{
    public class adminDashboardModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public List<ActionEntity> ActionEntities { get; set; }
        public Dictionary<string, int> ListingCounts { get; set; }

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
        }
    }
} 