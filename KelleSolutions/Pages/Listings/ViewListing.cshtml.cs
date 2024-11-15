using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace KelleSolutions.Pages.Listings
{
    public class ViewListingModel : PageModel
    {
        public string UserName { get; set; }
        public List<UserListing> Listings { get; set; } = new List<UserListing>(); // Initialize as an empty list

        public void OnGet(int userId)
        {
            // sample data matching the mockup
            if (userId == 1)
            {
                UserName = "Randall Watts";
                Listings = new List<UserListing>
                {
                    new UserListing 
                    { 
                        Id = 1,
                        Created = "11/6/2024",
                        Status = "On-Hold",
                        Operator = "Randall Watts",
                        Team = "Internal",
                        Price = "$100,000",
                        Address = "6000 J St."
                    }
                };
            }
            else if (userId == 2)
            {
                UserName = "John Doe";
                Listings = new List<UserListing>(); // John Doe has no listings, but Listings is still initialized
            }
            // Default case: if userId does not match any conditions, Listings remains an empty list
        }
    }

    public class UserListing
    {
        public int Id { get; set; }
        public string Created { get; set; }
        public string Status { get; set; }
        public string Operator { get; set; }
        public string Team { get; set; }
        public string Price { get; set; }
        public string Address { get; set; }
    }
}
