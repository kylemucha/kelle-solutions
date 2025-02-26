using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;            // handles user authentication!
using System.Collections.Generic;               // defines database relationships
using System.Linq;                              // defines LINQ queries (ex: "Where", "OrderBy", "Skip", "Take", etc.)
using System.Security.Claims;                   // for ClaimsPrincipal
using System;
using System.Threading.Tasks;                   // supports non-blocking queries (ex: "async", "wait")
using System.Reflection;                        // required for extracting Display Names
using System.ComponentModel.DataAnnotations;    // required for Display attribute
using KelleSolutions.Data;                      // imports KelleSolutionsDbContext.cs
using KelleSolutions.Models;                    // imports model classes

namespace KelleSolutions.Pages.Listings {
    public class CreateListingModalModel : PageModel {
        // database context for querying lists
        private readonly KelleSolutionsDbContext _context;

        // manages logged-in users
        private readonly UserManager<User> _userManager;

        // stores the user reference
        private readonly ClaimsPrincipal _currentUser;

        // constructor injects database context
        public CreateListingModalModel(KelleSolutionsDbContext context, UserManager<User> userManager, ClaimsPrincipal user) {
            _context = context;
            _userManager = userManager;
            _currentUser = user;
        }

        // stores the list of all listing status types
        public List<KeyValuePair<string, string>> AvailableStatusTypesList { get; set; } = new();

        // dropdown options for user-created properties
        public List<KeyValuePair<string, string>> UserProperties { get; set; } = new();

        public async Task OnGetAsync() {
            // get the logged-in user
            var user = await _userManager.GetUserAsync(_currentUser);

            // fetches properties created by the logged-in user
            if (user != null) {
                UserProperties = await _context.Properties
                    // filter by current user's properties
                    .Where(p => p.UserID == user.Id)
                    .Select(p => new KeyValuePair<string, string>(
                        // store property ID in the dropdown
                        p.UserID.ToString(),
                        // display as "Address, City, State"
                        $"{p.Address}, {p.City}, {p.State}"
                    ))
                    .ToListAsync();
            }
            else {
                // avoid null exception?
                return;
            }

            // gets all the enum values from StatusTypes
            AvailableStatusTypesList = Enum.GetValues(typeof(Listing.StatusTypes))
                .Cast<Listing.StatusTypes>()
                .Select(status => new KeyValuePair<string, string>(
                    // the actual value stored in the database
                    status.ToString(),
                    // get the enum member
                    status.GetType().GetMember(status.ToString())
                        .FirstOrDefault()?
                        // use display name if available
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? status.ToString()
                ))
                .ToList();
        }
    }
}
