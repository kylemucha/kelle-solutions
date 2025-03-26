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
using System.Text.Json;
using System.IO;
using System;

namespace KelleSolutions.Pages.Listings
{
    // Apply the antiforgery ignore at the class level.
    [IgnoreAntiforgeryToken]
    public class MyListingsModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User> _userManager;

        public MyListingsModel(KelleSolutionsDbContext context, UserManager<User> userManager)
        {
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

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var roles = await _userManager.GetRolesAsync(currentUser);

            // Include Property details via the navigation property "PropertyDetails"
            var listingsQuery = _context.Listings
                .Include(l => l.PropertyDetails)
                .AsQueryable();

            // Fetch raw listings ordered by Created date
            var rawListings = await listingsQuery
                .OrderByDescending(l => l.Created)
                .ToListAsync();

            AllListings = rawListings.Select(l => new ViewUserListings
            {
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
            MyListings = AllListings
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

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

        public async Task<JsonResult> OnPostUpdateStatusAsync([FromBody] UpdateStatusModel request)
        {
            var listing = await _context.Listings.FindAsync(request.Id);
            if (listing == null)
            {
                return new JsonResult(new { success = false, message = "Listing not found" });
            }

            if (Enum.TryParse(typeof(KelleSolutions.Models.MyStatusEnum), request.Status, out var status))
            {
                listing.MyStatus = (MyStatusEnum)status;
                await _context.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }

            return new JsonResult(new { success = false, message = "Invalid Status" });
        }

       [IgnoreAntiforgeryToken]
public async Task<JsonResult> OnPostCreateListingAsync()
{
    try
    {
        // Enable buffering so we can read the request body multiple times.
        Request.EnableBuffering();

        string body;
        using (var reader = new StreamReader(Request.Body, leaveOpen: true))
        {
            body = await reader.ReadToEndAsync();
        }
        // Reset the stream position for any further reads.
        Request.Body.Position = 0;

        if (string.IsNullOrWhiteSpace(body))
        {
            return new JsonResult(new { success = false, message = "Empty request body" });
        }

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        CreateListingInputModel input;
        try
        {
            input = JsonSerializer.Deserialize<CreateListingInputModel>(body, options);
        }
        catch (Exception ex)
        {
            return new JsonResult(new { success = false, message = "Invalid JSON: " + ex.Message });
        }

        if (input == null || string.IsNullOrWhiteSpace(input.PropertyId) ||
            string.IsNullOrWhiteSpace(input.Price) || string.IsNullOrWhiteSpace(input.Status) ||
            string.IsNullOrWhiteSpace(input.Source))
        {
            return new JsonResult(new { success = false, message = "Missing required fields" });
        }

        if (!decimal.TryParse(input.Price, out decimal price))
        {
            return new JsonResult(new { success = false, message = "Invalid price" });
        }

        if (!int.TryParse(input.PropertyId, out int propertyCode))
        {
            return new JsonResult(new { success = false, message = "Invalid property" });
        }

        var propertyDetails = await _context.Properties.FindAsync(propertyCode);
        if (propertyDetails == null)
        {
            return new JsonResult(new { success = false, message = "Property not found" });
        }

        if (!Enum.TryParse(typeof(MyStatusEnum), input.Status, ignoreCase: true, out var statusEnum))
        {
            return new JsonResult(new { success = false, message = "Invalid status" });
        }

        if (!Enum.TryParse(typeof(MySourceEnum), input.Source, ignoreCase: true, out var sourceEnum))
        {
            return new JsonResult(new { success = false, message = "Invalid source" });
        }

        // Create new listing and set all required fields.
        Listing newListing = new Listing
        {
            FK_Property = propertyCode, // explicitly set the foreign key
            Price = price,
            MyStatus = (MyStatusEnum)statusEnum,
            MySource = (MySourceEnum)sourceEnum,
            Created = DateTime.Now,
            PropertyDetails = propertyDetails,
            // Set default values for required fields not supplied via the form.
            Archived = false,
            Operator = OperatorEnum.Operator1,
            Team = TeamEnum.TeamA,
            Visibility = VisibilityEnum.Low
        };

        _context.Listings.Add(newListing);
        await _context.SaveChangesAsync();

        return new JsonResult(new { success = true });
    }
    catch (Exception ex)
    {
        return new JsonResult(new { success = false, message = "Unhandled error: " + ex.ToString() });
    }
}

        public class CreateListingInputModel
        {
            public string PropertyId { get; set; }
            public string Price { get; set; }
            public string Status { get; set; }
            public string Source { get; set; }
        }
    }

    // View model for user listings
    public class ViewUserListings
    {
        public int ListingID { get; set; }
        public DateOnly ListingDate { get; set; }
        public string Status { get; set; }
        public string Operator { get; set; }
        public string Affiliation { get; set; }
        public double Price { get; set; }
        public string Address { get; set; }
    }

    // Data transfer class for AJAX status update request
    public class UpdateStatusModel
    {
        public int Id { get; set; }
        public required string Status { get; set; }
    }
}
