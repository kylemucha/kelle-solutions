using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
                // Fetch only the basic property data without complex string operations
                var properties = await _context.Properties
                    .Select(p => new { 
                        p.Code, 
                        p.Street, 
                        p.City, 
                        p.StateProvince 
                    })
                    .ToListAsync();

                // Perform the string formatting in-memory after the query is executed
                UserProperties = properties.Select(p => new KeyValuePair<string, string>(
                    p.Code.ToString(),
                    FormatAddress(p.Street, p.City, p.StateProvince)
                )).ToList();
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

        // Helper method to format addresses with potentially missing components
        private string FormatAddress(string street, string city, string stateProvince) {
            var parts = new List<string>();
            
            if (!string.IsNullOrWhiteSpace(street))
                parts.Add(street);
                
            if (!string.IsNullOrWhiteSpace(city))
                parts.Add(city);
                
            if (!string.IsNullOrWhiteSpace(stateProvince))
                parts.Add(stateProvince);
                
            return string.Join(", ", parts);
        }

    }
}
