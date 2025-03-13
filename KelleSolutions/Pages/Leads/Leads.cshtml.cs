using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace KelleSolutions.Pages.Leads {
    public class LeadsModel : PageModel {
        public List<UserLeads> UserLeads { get; set; }

        public void OnGet() {
            // Sample data using the updated view model.
            UserLeads = new List<UserLeads> {
                new UserLeads {
                    Id = 1,
                    FullName = "Randall Watts",
                    Phone = "1234567890",
                    Email = "randall@watts.com",
                    CreationDate = new DateOnly(2023, 1, 1)
                },
                new UserLeads {
                    Id = 2,
                    FullName = "Luis Gallarzo",
                    Phone = "9876543210",
                    Email = "luis@gallarzo.com",
                    CreationDate = new DateOnly(2023, 2, 2)
                }
            };
        }
    }

    public class UserLeads {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}
