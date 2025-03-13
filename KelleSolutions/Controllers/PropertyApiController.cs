// Date: 02/11/2025
// Update PropertyApiController.cs to reflect Properties Entity in ERD.
// Adding comments to better explain documentation
// PropertyApiController.cs provides an API endpoint that allows users
// to search for properties based on their street address.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using KelleSolutions.Data;
using KelleSolutions.Models;

namespace KelleSolutions.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesApiController : ControllerBase {
        private readonly KelleSolutionsDbContext _context;
        public PropertiesApiController(KelleSolutionsDbContext context) {
            _context = context;
        }
        // This endpoint provides property suggestions for autocomplete based on street address.
        [HttpGet("GetPropertySuggestions")]
        public async Task<IActionResult> GetPropertySuggestions(string term) {
            if (string.IsNullOrEmpty(term)) {
                return BadRequest("Search term cannot be empty.");
            }
            // Query the Properties table for street addresses containing the search term.
            var suggestions = await _context.Properties
                .Where(p => p.Street.Contains(term))
                .Select(p => new {
                    p.Code,
                    p.Street,
                    p.City,
                    p.StateProvince,
                    p.Postal
                })
                .ToListAsync();
            return Ok(suggestions);
        }
    }
}
