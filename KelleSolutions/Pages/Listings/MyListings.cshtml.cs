using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models.ViewModels;
using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KelleSolutions.Pages.Listings;

public class ViewListingsModel(
    KelleSolutionsDbContext context,
    UserManager<User> userManager) : PageModel
{
    private readonly KelleSolutionsDbContext _context = context;
    private readonly UserManager<User> _userManager = userManager;

    public List<ViewUserListings> AllListings { get; set; } = [];
    public List<ViewUserListings> ViewUserListings { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public int PageSize { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    public int TotalPages => (int)Math.Ceiling((double)AllListings.Count / PageSize);

    public async Task OnGetAsync()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var roles = await _userManager.GetRolesAsync(currentUser);

        var listingsQuery = _context.Listings
            .Include(l => l.Property)
            .Include(l => l.Agent)
            .AsQueryable();

        if (roles.Contains("Broker") || roles.Contains("Agent"))
        {
            listingsQuery = listingsQuery.Where(l => l.AgentID == currentUser.Id);
        }
        // Admin role sees all listings, no filtering needed

        AllListings = await listingsQuery.Select(l => new ViewUserListings
        {
            ID = l.ListingID,
            ListingDate = DateOnly.FromDateTime(l.StartDate),
            Status = l.Status,
            Operator = l.Agent.FirstName + " " + l.Agent.LastName,
            Team = l.Agent.Affiliation,
            Price = (double)l.Price,
            Address = $"{l.Property.Address}, {l.Property.City}, {l.Property.State} {l.Property.ZipCode}"
        })
        .ToListAsync();

        ViewUserListings = AllListings
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
            .ToList();
    }
}

public class UpdateStatusModel 
{
        public int Id { get; set; }
        public required string Status { get; set; }
}
