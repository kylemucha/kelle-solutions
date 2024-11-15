using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace KelleSolutions.Pages.Listings
{
    public class ListingsModel : PageModel
    {
        public List<UserListings> UserListings { get; set; }

        public void OnGet()
        {
            // sample data matching the mockup

            UserListings = new List<UserListings>
            {
                new UserListings 
                { 
                    Id = 1,
                    UserName = "Randall Watts",
                    ActiveListings = 3,
                    InProgressListings = 0,
                    ClosedListings = 0
                },
                new UserListings 
                { 
                    Id = 2,
                    UserName = "John Doe",
                    ActiveListings = 2,
                    InProgressListings = 0,
                    ClosedListings = 0
                }
            };
        }
    }

    public class UserListings
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int ActiveListings { get; set; }
        public int InProgressListings { get; set; }
        public int ClosedListings { get; set; }
    }
}