using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Models;
using System;

namespace KelleSolutions.Pages.Properties
{
    public class ViewPropertyModel : PageModel
    {
        public RealEstateProperty Property { get; set; }

        public void OnGet()
        {
            // Provide a default object if no data-fetching logic is implemented
            Property = new RealEstateProperty
            {
                Id = 1,
                DateListed = new DateTime(2024, 11, 6),
                County = "Sacramento",
                City = "Sacramento",
                ZipCode = "95825",
                StreetAddress = "J St.",
                Bedrooms = 1,
                Bathrooms = 2
            };
        }
    }
}
