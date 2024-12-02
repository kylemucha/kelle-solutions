using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Models;
using KelleSolutions.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KelleSolutions.Pages.Properties
{
    public class ViewPropertyModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public ViewPropertyModel(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        public List<RealEstateProperty> Properties { get; set; }

        public async Task OnGetAsync()
        {
            // Fetch all properties from the database
            Properties = await _context.Properties.ToListAsync();
        }
    }
}
