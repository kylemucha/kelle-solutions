// Date: 02/11/2025
// Update PropertyApiController.cs to reflect Properties Entity in ERD.
// Adding comments to better explain documentation
// PropertyApiController.cs provides an API endpoint that allows users
// to search for properties based on their address.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
// This specific line connects to KelleSolutionsDbContext.cs!
using KelleSolutions.Data;
// This specific line imports the Property model from Property.cs!
using KelleSolutions.Models;

namespace KelleSolutions.Controllers {
    // This line lets the API automatically send HTTP requests to this controller 
    [Route("api/[controller]")]
    // This line checks to see if the incoming request's syntax is correct
    [ApiController]
    // This is where the response is prepared
    public class PropertiesApiController : ControllerBase {
        private readonly KelleSolutionsDbContext _context;
        public PropertiesApiController(KelleSolutionsDbContext context) {
            _context = context;
        }
        // This is the Endpoint to get property suggestions for autocomplete
        [HttpGet("GetPropertySuggestions")]
        public async Task<IActionResult> GetPropertySuggestions(string term) {
            // Validate the input term
            if (string.IsNullOrEmpty(term)) {
                return BadRequest("Search term cannot be empty.");
            }
            // Query the Properties table for addresses containing the search term
            var suggestions = await _context.Properties
                .Where(p => p.Address.Contains(term))
                .Select(p => new {
                    p.PropertyID,
                    p.Address,
                    p.City,
                    p.State,
                    p.ZipCode,
                })
                .ToListAsync();
            // Return the list as JSON
            return Ok(suggestions);
        }
    }
}
