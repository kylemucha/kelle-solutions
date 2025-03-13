using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace KelleSolutions.Pages.Leads {
    public class ViewLeadsModel : PageModel {
        public List<ViewUserLeads> AllLeads { get; set; }
        public List<ViewUserLeads> ViewUserLeads { get; set; }

        // Pagination properties
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public int TotalPages => (int)System.Math.Ceiling((double)AllLeads.Count / PageSize);

        public void OnGet() {
            // Sample data for demonstration using the updated view model.
            AllLeads = new List<ViewUserLeads>();
            for (int i = 1; i <= 128; i++) {
                AllLeads.Add(new ViewUserLeads {
                    ID = i,
                    FullName = i % 2 == 0 ? "Randall Watts" : "Luis Gallarzo",
                    Phone = i % 2 == 0 ? "1234567890" : "9876543210",
                    Email = i % 2 == 0 ? "randall@watts.com" : "luis@gallarzo.com",
                    CreationDate = new DateOnly(2024, 2, 1)
                });
            }
            ViewUserLeads = AllLeads.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }

    public class ViewUserLeads {
        public int ID { get; set; }
        public DateOnly CreationDate { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
