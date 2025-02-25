using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using KelleSolutions.Models.ViewModels;
using KelleSolutions.Data;
using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace KelleSolutions.Pages.Properties
{
    public class ViewPropertiesModel(
        KelleSolutionsDbContext context,
        UserManager<User> userManager) : PageModel
    {
        private readonly KelleSolutionsDbContext _context = context;
        private readonly UserManager<User> _userManager = userManager;

        public List<ViewUserProperties> AllProperties { get; set; } = [];
        public List<ViewUserProperties> ViewUserProperties { get; set; } = [];

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages => (int)Math.Ceiling((double)AllProperties.Count / PageSize);

        public async Task OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(currentUser);

            var query = _context.Properties
                .AsQueryable();

            if (roles.Contains("Broker") || roles.Contains("Agent"))
            {
                query = query.Where(p => p.OwnerID == currentUser.Id);
            }

            var propertiesFromDb = await query
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new
                {
                    p.PropertyID,
                    p.CreatedAt,
                    p.State,
                    p.City,
                    p.ZipCode,
                    p.Address,
                    p.BedCount,
                    p.BathCount
                })
                .ToListAsync();

            AllProperties = propertiesFromDb.Select(p => new ViewUserProperties
            {
                ID = p.PropertyID,
                CreationDate = DateOnly.FromDateTime(p.CreatedAt),
                County = p.State,
                City = p.City,
                Postal = int.TryParse(p.ZipCode, out int zip) ? zip : 0,
                Street = p.Address,
                Bed = p.BedCount,
                Bath = p.BathCount
            })
            .ToList();

            ViewUserProperties = AllProperties
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}
