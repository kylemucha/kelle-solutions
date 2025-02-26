using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;            // handles user authentication!
using System.Collections.Generic;               // defines database relationships
using System.Linq;                              // defines LINQ queries (ex: "Where", "OrderBy", "Skip", "Take", etc.)
using System.Security.Claims;                   // for ClaimsPrincipal
using System;                                   
using System.IO;                                // file and stream execution
using System.Text.Json;                         // JSON serial/deserial
using System.Threading.Tasks;                   // supports non-blocking queries (ex: "async", "wait")
using System.Reflection;                        // required for extracting Display Names
using System.ComponentModel.DataAnnotations;    // required for Display attribute
using KelleSolutions.Data;                      // imports KelleSolutionsDbContext.cs
using KelleSolutions.Models;                    // imports model classes, like "Property.cs" as Property and "User.cs" as User

namespace KelleSolutions.Pages.Listings {
    public class MyListingsModel : PageModel {

        // database context for querying listings
        private readonly KelleSolutionsDbContext _context;

        // manages logged-in users
        private readonly UserManager<User> _userManager;

        // constructor that injects database context and user manager
        public MyListingsModel(KelleSolutionsDbContext context, UserManager<User> userManager) {
            _context = context;
            _userManager = userManager;
        }

        // storing the list of listings owned by the logged-in user
        public List<ListingViewModel> MyListings { get; set; } = new();
        public List<ListingViewModel> ViewUserListings { get; set; }
        public CreateListingModalModel CreateListingModel { get; set; }
        public List<KeyValuePair<string, string>> AvailableStatusTypesList { get; set; } = new();

        // page display properties (not house properties, but like components properties),
        // clarification for later documentation
        // defaults to 10 listings per page
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        // the current page number
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        // the total number of pages
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync() {
            // gets the currently logged-in user
            var currentUser = await _userManager.GetUserAsync(User);

            // if no user is logged in, redirect to the login page
            // while in testing phase, keep this commented!
            if (currentUser == null) {
                return RedirectToPage("/Account/Login");
            }

            // ensure UserID is valid before querying the database
            if (string.IsNullOrEmpty(currentUser.Id)) {
                return RedirectToPage("/Account/Login");
            }
            
            // query only the listings submitted by the logged-in user
            var query = _context.Listings
                .Include(l => l.Property)
                //  filters listings based on UserID
                .Where(l => l.Property.UserID == currentUser.Id)
                .Select(l => new ListingViewModel {
                    ListingID = l.ListingID,
                    Date = l.CreatedAt,
                    Status = l.Status.ToString(),
                    Team = l.Team.ToString(),
                    Price = (double)l.Price,
                    Address = l.Property.Address
                })
                .AsQueryable();

            // get the total listings count for page display listings
            int totalListings = await query.CountAsync();
            TotalPages = (int)System.Math.Ceiling((double)totalListings / PageSize);

            // apply page display properties
            MyListings = await query
                // newest listings appear first
                .OrderByDescending(l => l.Date)
                // skips items from previous pages
                .Skip((PageNumber - 1) * PageSize)
                // limit the results to just PageSize
                .Take(PageSize)
                .ToListAsync();

            CreateListingModel = new CreateListingModalModel(_context, _userManager, User);
            // Load properties and status types
            await CreateListingModel.OnGetAsync();

            // fetches available property types, dynamically!
            AvailableStatusTypesList = Enum.GetValues(typeof(Listing.StatusTypes))
                .Cast<Listing.StatusTypes>()
                .Select(status => new KeyValuePair<string, string>(
                    status.ToString(),
                    status.GetType().GetMember(status.ToString())?
                        .FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? status.ToString()
                ))
                .ToList();

            return Page();
        }

        public async Task<JsonResult> OnPostUpdateStatusAsync([FromBody] StatusUpdateRequest request) {
            // find the listing in the database by its ListingID
            var listing = await _context.Listings.FindAsync(request.ListingID);

            // error response if listing is not found
            if (listing == null) {
                return new JsonResult(new { success = false, message = "Listing not found "});
            }

            // parsing the incoming status string into the StatusTypes enum (the pre-defined values)
            // (ex: "Active", "Pending", "Sold", "Withdrawn", etc.)
            if (Enum.TryParse(typeof(Listing.StatusTypes), request.NewStatus, out var status)) {
                // if parsing is successful, this updates the listing status
                listing.Status = (Listing.StatusTypes)status;

                // saving changes
                await _context.SaveChangesAsync();

                // return a success response
                return new JsonResult(new { success = true });
            }

            // if parsing fails, error handler
            return new JsonResult(new { success = false, message = "Invalid Status" });
        }

        // view model to format data properly
        public class ListingViewModel {
            public int ListingID { get; set; }
            public DateTime Date { get; set; }
            public string Status { get; set; }
            public string Team { get; set; }
            public double Price { get; set; }
            public string Address { get; set; }
        }

        // data transfer class for AJAX request payload
        public class StatusUpdateRequest {
            public int ListingID { get; set; }
            public string NewStatus { get; set; }
        }
    }
}
