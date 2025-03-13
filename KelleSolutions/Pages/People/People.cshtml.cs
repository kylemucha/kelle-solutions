using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KelleSolutions.Pages.People
{
    public class PeopleModel : PageModel
    {
        public List<PersonView> PeopleList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        // Total number of people in the data source
        public int TotalCount { get; set; }

        // Calculated total pages for pagination
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public void OnGet()
        {
            // Generate sample data; in a real scenario, replace this with data from your database.
            var allPeople = new List<PersonView>();
            for (int i = 1; i <= 128; i++)
            {
                allPeople.Add(new PersonView
                {
                    Code = i,
                    NameLast = "Stone",
                    NameFirst = "Billy",
                    PhonePrimary = "1234567890",
                    EmailPrimary = "billy@test.com",
                    Created = new DateTime(2024, 4, 12),
                    Category = "Partner"  // Could be mapped from your CategoryEnum
                });
            }

            TotalCount = allPeople.Count;

            // Apply pagination to generate the list for the current page.
            PeopleList = allPeople
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }

    // The view model that aligns with the Person model.
    public class PersonView
    {
        public int Code { get; set; }
        public string NameLast { get; set; }
        public string NameFirst { get; set; }
        public string PhonePrimary { get; set; }
        public string EmailPrimary { get; set; }
        public DateTime Created { get; set; }
        public string Category { get; set; }
    }
}
