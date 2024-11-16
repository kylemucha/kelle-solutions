using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Models; // This is the namespace for the Person class
using System;
using System.Collections.Generic;

namespace KelleSolutions.Pages.People
{
    public class ViewPeopleModel : PageModel
    {
        // Public property for the list of people
        public List<Person> People { get; set; } = new List<Person>();

        public void OnGet()
        {
            // Example data (replace with actual database fetching logic)
            People = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    Name = "Billy Stone",
                    PhoneNumber = "555-555-5555",
                    Email = "billy@test.com",
                    Created = new DateTime(2023, 4, 12),
                    OperatorName = "Agent's Name"
                }
            };
        }
    }
}
