using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Listings
{
    public class ViewListingsModel : PageModel
    {
        public List<ViewUserListings> AllListings { get; set; }
        public List<ViewUserListings> ViewUserListings { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10; // Default to 10 listings per page

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages => (int)System.Math.Ceiling((double)AllListings.Count / PageSize);

        public void OnGet()
        {
            // Sample Data
            AllListings = new List<ViewUserListings>();
            for (int i = 1; i <= 128; i++)
            {
                AllListings.Add(new ViewUserListings
                {
                    ID = i,
                    ListingDate = new DateOnly(2024, 2, 1),
                    Status = i % 2 == 0 ? "ON HOLD" : "ACTIVE",
                    Operator = "Randall Watts",
                    Team = "Internal",
                    Price = 120000 + (i * 1000),
                    Address = $"2622 Myers Ranch Ln<br />West Sacramento, CA 9569{i}"
                });
            }

            // Apply Pagination
            ViewUserListings = AllListings.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUpdateStatus([FromBody] UpdateStatusModel model)
        {
            if (model == null || model.Id == 0)
            {
                return BadRequest("Invalid data.");
            }

            var listing = AllListings.FirstOrDefault(l => l.ID == model.Id);
            if (listing == null)
            {
                return NotFound();
            }

            listing.Status = model.Status;

            return new JsonResult(new { success = true, message = "Status updated successfully." });
        }
    }

    public class ViewUserListings
    {
        public int ID { get; set; }
        public DateOnly ListingDate { get; set; }
        public string Status { get; set; }
        public string Operator { get; set; }
        public string Team { get; set; }
        public double Price { get; set; }
        public string Address { get; set; }
    }

    public class UpdateStatusModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
