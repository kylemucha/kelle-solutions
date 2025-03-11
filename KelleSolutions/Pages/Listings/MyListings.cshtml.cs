using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using KelleSolutions.Data;
using KelleSolutions.Models;
using KelleSolutions.Models.ViewModels;

namespace KelleSolutions.Pages.Listings {
    public class MyListingsModel : PageModel {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User> _userManager;

        public MyListingsModel(KelleSolutionsDbContext context, UserManager<User> userManager) {
            _context = context;
            _userManager = userManager;
        }

        public List<ViewUserListings> AllListings { get; set; } = new();
        public List<ViewUserListings> MyListings { get; set; } = new();
        public CreateListingModalModel CreateListingModel { get; set; }
        public List<KeyValuePair<string, string>> AvailableStatusTypesList { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages => (int)Math.Ceiling((double)AllListings.Count / PageSize);

        public async Task<IActionResult> OnGetAsync() {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) {
                return RedirectToPage("/Account/Login");
            }

            var roles = await _userManager.GetRolesAsync(currentUser);

            // Include Property and its User details (replacing Agent)
            var listingsQuery = _context.Listings
                .Include(l => l.Property)
                .ThenInclude(p => p.User)
                .AsQueryable();

            // If user is Broker or Agent, filter by listings where the property belongs to the current user
            if (roles.Contains("Broker") || roles.Contains("Agent")) {
                listingsQuery = listingsQuery.Where(l => l.Property.User.Id == currentUser.Id);
            }

            // Fetch raw listings ordered by Created date
            var rawListings = await listingsQuery
                .OrderByDescending(l => l.Created)
                .ToListAsync();

            AllListings = rawListings.Select(l => new ViewUserListings {
                ListingID = l.Code,
                ListingDate = l.Created.HasValue ? DateOnly.FromDateTime(l.Created.Value) : DateOnly.FromDateTime(DateTime.MinValue),
                Status = l.MyStatus.ToString(),
                Operator = l.Property.User.FirstName + " " + l.Property.User.LastName,
                Affiliation = l.Property.User.Affiliation ?? "N/A",
                Price = l.Price.HasValue ? (double)l.Price.Value : 0,
                Address = $"{l.Property.Address}, {l.Property.City}, {l.Property.State} {l.Property.ZipCode}"
            }).ToList();

            // Paginate the listings
            MyListings = AllListings
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            // Initialize the CreateListingModalModel
            CreateListingModel = new CreateListingModalModel(_context, _userManager, User);
            await CreateListingModel.OnGetAsync();

            // Get available status types from MyStatusEnum
            AvailableStatusTypesList = Enum.GetValues(typeof(MyStatusEnum))
                .Cast<MyStatusEnum>()
                .Select(status => new KeyValuePair<string, string>(
                    status.ToString(),
                    status.GetType().GetMember(status.ToString())?
                        .FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? status.ToString()
                ))
                .ToList();

            return Page();
        }

        public async Task<JsonResult> OnPostUpdateStatusAsync([FromBody] UpdateStatusModel request) {
            var listing = await _context.Listings.FindAsync(request.Id);
            if (listing == null) {
                return new JsonResult(new { success = false, message = "Listing not found" });
            }

            if (Enum.TryParse(typeof(MyStatusEnum), request.Status, out var status)) {
                listing.MyStatus = (MyStatusEnum)status;
                await _context.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }

            return new JsonResult(new { success = false, message = "Invalid Status" });
        }
    }
}

// View model for user listings
public class ViewUserListings {
    public int ListingID { get; set; }
    public DateOnly ListingDate { get; set; }
    public string Status { get; set; }
    public string Operator { get; set; }
    public string Affiliation { get; set; }
    public double Price { get; set; }
    public string Address { get; set; }
}

// Data transfer class for AJAX status update request
public class UpdateStatusModel {
    public int Id { get; set; }
    public required string Status { get; set; }
}
