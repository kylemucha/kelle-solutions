using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;              
using System.Linq;                             
using System.Threading.Tasks;                   
using System.Reflection;                        
using System.ComponentModel.DataAnnotations;    
using KelleSolutions.Data;                      
using KelleSolutions.Models;                     

namespace KelleSolutions.Pages.Properties {
    public class CreatePropertyModalModel : PageModel {
        private readonly KelleSolutionsDbContext _context;

        public CreatePropertyModalModel(KelleSolutionsDbContext context) {
            _context = context;
        }

        // List of available property types for the UI based on the updated enum
        public List<KeyValuePair<string, string>> AvailablePropertyTypesList { get; set; } = new();

        public async Task OnGetAsync() {
            AvailablePropertyTypesList = Enum.GetValues(typeof(PropertyTypeEnum))
                .Cast<PropertyTypeEnum>()
                .Select(pt => new KeyValuePair<string, string>(
                    ((short)pt).ToString(),
                    pt.GetType().GetMember(pt.ToString())
                        .FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? pt.ToString()
                ))
                .ToList();
        }
    }
}
