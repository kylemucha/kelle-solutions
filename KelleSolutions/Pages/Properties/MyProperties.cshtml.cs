using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;            
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;               
using System.Linq;                              
using System;                                   
using System.Reflection;                        
using System.ComponentModel.DataAnnotations;    
using System.Threading.Tasks;                   
using KelleSolutions.Data;                      
using KelleSolutions.Models;                    
using KelleSolutions.Models.ViewModels;         

namespace KelleSolutions.Pages.Properties {
    public class MyPropertiesModel : PageModel {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User> _userManager;

        public MyPropertiesModel(KelleSolutionsDbContext context, UserManager<User> userManager) {
            _context = context;
            _userManager = userManager;
        }

        // List of all properties converted to the view model
        public List<ViewUserProperties> AllProperties { get; set; } = new();
        public List<ViewUserProperties> ViewUserProperties { get; set; } = new();

        // Available property types list
        public List<KeyValuePair<string, string>> AvailablePropertyTypesList { get; set; } = new();

        // Pagination properties
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;
        public int TotalPages => (int)Math.Ceiling((double)AllProperties.Count / PageSize);

        public async Task<IActionResult> OnGetAsync() {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) {
                return RedirectToPage("/Account/Login");
            }

            // Get properties without filtering by user since UserID/OwnerID no longer exist
            var query = _context.Properties.AsQueryable();

            var propertiesFromDb = await query
                .OrderByDescending(p => p.Created)
                .Select(p => new {
                    p.Code,
                    p.Created,
                    p.County,
                    p.City,
                    p.Postal,
                    p.Street,
                    p.Beds,
                    p.Baths
                })
                .ToListAsync();

            // Map to the view model (updating property names accordingly)
            AllProperties = propertiesFromDb.Select(p => new ViewUserProperties {
                ID = p.Code,
                CreationDate = p.Created.HasValue ? DateOnly.FromDateTime(p.Created.Value) : DateOnly.FromDateTime(DateTime.MinValue),
                County = p.County, // or, if preferred, use p.StateProvince based on your UI needs
                City = p.City,
                Postal = int.TryParse(p.Postal, out int zip) ? zip : 0,
                Street = p.Street,
                Bed = p.Beds ?? 0,
                Bath = p.Baths ?? 0
            }).ToList();

            ViewUserProperties = AllProperties
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            // Update available property types using the new enum
            AvailablePropertyTypesList = Enum.GetValues(typeof(PropertyTypeEnum))
                .Cast<PropertyTypeEnum>()
                .Select(pt => new KeyValuePair<string, string>(
                    pt.ToString(),
                    pt.GetType().GetMember(pt.ToString())?
                        .FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? pt.ToString()
                ))
                .ToList();

            return Page();
        }
    }
}
