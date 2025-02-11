using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Properties
{
    public class ViewPropertiesModel : PageModel
    {
        public List<ViewUserProperties> AllProperties { get; set; }
        public List<ViewUserProperties> ViewUserProperties { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10; // Default to 10 properties per page

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages => (int)System.Math.Ceiling((double)AllProperties.Count / PageSize);

        public void OnGet()
        {
            // Sample Data
            AllProperties = new List<ViewUserProperties>();
            for (int i = 1; i <= 128; i++)
            {
                AllProperties.Add(new ViewUserProperties
                {
                    ID = i,
                    CreationDate = new DateOnly(2024, 2, 1),
                    County = "Sacramento",
                    City = "Sacramento",
                    Postal = 95678,
                    Street = "J St.",
                    Bed = 2,
                    Bath = 1
                });
            }

            // Apply Pagination
            ViewUserProperties = AllProperties.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }

    public class ViewUserProperties
    {
        public int ID { get; set; }
        public DateOnly CreationDate { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public int Postal { get; set; }
        public string Street { get; set; }
        public int Bed { get; set; }
        public int Bath { get; set; }
    }
}
