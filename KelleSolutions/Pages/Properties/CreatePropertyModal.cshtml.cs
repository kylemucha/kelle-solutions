using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;               // defines database relationships
using System.Linq;                              // defines LINQ queries (ex: "Where", "OrderBy", "Skip", "Take", etc.)
using System.Threading.Tasks;                   // supports non-blocking queries (ex: "async", "wait")
using System.Reflection;                        // required for extracting Display Names
using System.ComponentModel.DataAnnotations;    // required for Display attribute
using KelleSolutions.Data;                      // imports KelleSolutionsDbContext.cs
using KelleSolutions.Models;                    // imports model classes

namespace KelleSolutions.Pages.Properties {
    public class CreatePropertyModalModel : PageModel {

        // database context for querying properties
        private readonly KelleSolutionsDbContext _context;

        // constructor that injects context
        public CreatePropertyModalModel(KelleSolutionsDbContext context) {
            _context = context;
        }

        // storing the list of available property types, retrieved from the database
        public List<KeyValuePair<string, string>> AvailablePropertyTypesList { get; set; } = new();

        public async Task OnGetAsync() {
            // gets all the enum values from PropertyTypes
            AvailablePropertyTypesList = Enum.GetValues(typeof(Property.PropertyTypes))
                .Cast<Property.PropertyTypes>()
                .Select(pt => new KeyValuePair<string, string>(
                    // the actual value stored in the database
                    pt.ToString(),
                    // get the enum member
                    pt.GetType().GetMember(pt.ToString())
                        .FirstOrDefault()?
                        // use display name if available
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? pt.ToString()
                ))
                .ToList();
        }
    }
}
