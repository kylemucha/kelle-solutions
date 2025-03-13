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

        // List for populating the status dropdown from MyStatusEnum
        public List<KeyValuePair<string, string>> AvailableStatusTypesList { get; set; } = new();
        // List for populating the property dropdown with properties the user created
        public List<KeyValuePair<string, string>> UserProperties { get; set; } = new();

        public async Task OnGetAsync() {
    // Get the logged-in user (if needed for future filtering)
    var user = await _userManager.GetUserAsync(_currentUser);

    if (user != null) {
        // Since the Property model no longer contains a User navigation property,
        // we are returning all properties.
        UserProperties = await _context.Properties
            //.Where(p => p.OwnerID == user.Id) // Uncomment and update if you add an ownership field later.
            .Select(p => new KeyValuePair<string, string>(
                p.Code.ToString(),
                $"{p.Street}, {p.City}, {p.StateProvince}"
            ))
            .ToListAsync();
    }
    else {
        return;
    }

    // Get all enum values from MyStatusEnum for the listing status dropdown
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
