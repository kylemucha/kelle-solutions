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

        public int TotalPages => (int)System.Math.Ceiling((double)AllListings.Count / PageSize);

        public async Task<IActionResult> OnGetAsync() {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) {
                return RedirectToPage("/Account/Login");
            }

            var roles = await _userManager.GetRolesAsync(currentUser);

            // Include Property details via the renamed navigation property "PropertyDetails"
            var listingsQuery = _context.Listings
                .Include(l => l.PropertyDetails)
                .AsQueryable();

            // Fetch raw listings ordered by Created date
            var rawListings = await listingsQuery
                .OrderByDescending(l => l.Created)
                .OrderByDescending(l => l.Created)
                .ToListAsync();

            AllListings = rawListings.Select(l => new ViewUserListings {
                ListingID = l.Code,
                ListingDate = l.Created.HasValue 
                    ? DateOnly.FromDateTime(l.Created.Value) 
                    : DateOnly.FromDateTime(DateTime.MinValue),
                Status = l.MyStatus.ToString(),
                Operator = l.PropertyDetails.Operator.ToString(),
                Affiliation = "N/A", // No user affiliation in the new model
                Price = l.Price.HasValue ? (double)l.Price.Value : 0,
                Address = $"{l.PropertyDetails.Street}, {l.PropertyDetails.City}, {l.PropertyDetails.StateProvince} {l.PropertyDetails.Postal}"
            }).ToList();

            // Paginate the listings
            // Paginate the listings
            MyListings = AllListings
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            // Initialize the CreateListingModalModel
            // Initialize the CreateListingModalModel
            CreateListingModel = new CreateListingModalModel(_context, _userManager, User);
            await CreateListingModel.OnGetAsync();

            // Get available status types from MyStatusEnum using fully qualified type
            AvailableStatusTypesList = Enum.GetValues(typeof(KelleSolutions.Models.MyStatusEnum))
                .Cast<KelleSolutions.Models.MyStatusEnum>()
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
                return new JsonResult(new { success = false, message = "Listing not found" });
            }

            if (Enum.TryParse(typeof(KelleSolutions.Models.MyStatusEnum), request.Status, out var status)) {
                listing.MyStatus = (KelleSolutions.Models.MyStatusEnum)status;
                await _context.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }

            return new JsonResult(new { success = false, message = "Invalid Status" });
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
}
