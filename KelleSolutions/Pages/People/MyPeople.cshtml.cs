using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.People
{
    public class ViewPeopleModel : PageModel
    {
        public List<ViewUserPeople> AllPeople { get; set; }
        public List<ViewUserPeople> ViewUserPeople { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10; // Default to 10 properties per page

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages => (int)System.Math.Ceiling((double)AllPeople.Count / PageSize);

        public void OnGet()
        {
            // Sample Data
            AllPeople = new List<ViewUserPeople>();
            for (int i = 1; i <= 128; i++)
            {
                AllPeople.Add(new ViewUserPeople
                {
                    ID = i,
                    LastName = "Stone",
                    FirstName = "Billy",
                    Phone = 1234567890,
                    Email = "billy@test.com",
                    CreationDate = new DateOnly(2024, 4, 12),
                    Category = "Partner" //Agent, Vender, Client, Friend
                });
            }

            // Apply Pagination
            ViewUserPeople = AllPeople.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }

    public class ViewUserPeople
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public DateOnly CreationDate { get; set; }
        public string Category { get; set; }
    }
}
