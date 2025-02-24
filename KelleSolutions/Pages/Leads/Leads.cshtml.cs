using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Leads
{
    public class LeadsModel : PageModel {
        public List<UserLeads> UserLeads { get; set; }

        public void OnGet() {
            UserLeads = new List<UserLeads> {
                new UserLeads {
                    Id = 1,
                    UserName = "Randall Watts",
                    PhoneNumber = 1234567890,
                    Email = "randall@watts.com",
                    CreationDate = new DateOnly(2023, 1, 1)
                },
                new UserLeads {
                    Id = 2,
                    UserName = "Luis Gallarzo",
                    PhoneNumber = 9876543210,
                    Email = "luis@gallarzo.com",
                    CreationDate = new DateOnly(2023, 2, 2)
                }
            };
        }
    }

    public class UserLeads
      {
          public int Id { get; set; }
          public string UserName { get; set; }
          public long PhoneNumber { get; set; }
          public string Email { get; set; }
          public DateOnly CreationDate { get; set; }
      }
}