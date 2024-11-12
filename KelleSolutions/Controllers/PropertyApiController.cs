using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using KelleSolutions.Data;
using KelleSolutions.Models; // Adjust the namespace if needed

namespace KelleSolutions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesApiController : ControllerBase
    {
        private readonly KelleSolutionsDbContext _context;

        // Constructor to inject the database context
        public PropertiesApiController(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        // Endpoint to get property suggestions for autocomplete
        [HttpGet("GetPropertySuggestions")]
        public async Task<IActionResult> GetPropertySuggestions(string term)
        {
            // Validate the input term
            if (string.IsNullOrEmpty(term))
            {
                return BadRequest("Search term cannot be empty.");
            }

            // Query the Properties table for street addresses containing the search term
            var suggestions = await _context.Properties
                .Where(p => p.StreetAddress.Contains(term))
                .Select(p => new 
                {
                    p.Id,
                    p.StreetAddress,
                    p.City,
                    p.State,
                    p.Price
                })
                .ToListAsync();

            // Return the list as JSON
            return Ok(suggestions);
        }
    }
}
