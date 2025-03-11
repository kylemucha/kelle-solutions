using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using KelleSolutions.Data;
using KelleSolutions.Models;

namespace KelleSolutions.Pages.Listings {
    public class CreateListingModalModel : PageModel {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ClaimsPrincipal _currentUser;

        public CreateListingModalModel(KelleSolutionsDbContext context, UserManager<User> userManager, ClaimsPrincipal user) {
            _context = context;
            _userManager = userManager;
            _currentUser = user;
        }

        public List<KeyValuePair<string, string>> AvailableStatusTypesList { get; set; } = new();
        public List<KeyValuePair<string, string>> UserProperties { get; set; } = new();

        public async Task OnGetAsync() {
            // Get the logged-in user
            var user = await _userManager.GetUserAsync(_currentUser);

            if (user != null) {
                UserProperties = await _context.Properties
                    // Filter by current user's properties using the navigation property "User"
                    .Where(p => p.User.Id == user.Id)
                    .Select(p => new KeyValuePair<string, string>(
                        // Store the property primary key (assumed "Id")
                        p.Id.ToString(),
                        // Display as "Address, City, State"
                        $"{p.Address}, {p.City}, {p.State}"
                    ))
                    .ToListAsync();
            }
            else {
                return;
            }

            // Get all enum values from MyStatusEnum
            AvailableStatusTypesList = Enum.GetValues(typeof(MyStatusEnum))
                .Cast<MyStatusEnum>()
                .Select(status => new KeyValuePair<string, string>(
                    status.ToString(),
                    status.GetType().GetMember(status.ToString())
                        .FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? status.ToString()
                ))
                .ToList();
        }
    }
}
