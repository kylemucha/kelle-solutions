using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using KelleSolutions.Models;
using System;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.People
{
    public class CreatePersonModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public CreatePersonModel(UserManager<User> userManager)
        {
            _userManager = userManager;

            // Initialize an empty person with default values.
            Person = new PersonViewModel
            {
                NameFirst = string.Empty,
                NameMiddle = string.Empty,
                NameLast = string.Empty,
                NameDisplay = string.Empty,
                Headline = string.Empty,
                EmailPrimary = string.Empty,
                EmailSecondary = string.Empty,
                PhonePrimary = string.Empty,
                PhoneSecondary = string.Empty,
                PhonePrimaryLabel = string.Empty,
                PhoneSecondaryLabel = string.Empty,
                Street = string.Empty,
                City = string.Empty,
                StateProvince = string.Empty,
                Postal = string.Empty,
                Country = string.Empty,
                Notes = string.Empty
            };
        }

        [BindProperty]
        public PersonViewModel Person { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var currentUser = await _userManager.GetUserAsync(User);

                // Here you would normally map the view model to your Person entity
                // and set additional fields like Created, Updated, Archived, etc.
                // For now, we assume the data is saved successfully.

                return RedirectToPage("/People/People");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred. Please try again.");
                Console.WriteLine($"Error: {ex.Message}");
                return Page();
            }
        }
    }

    public class PersonViewModel
    {
        public string NameFirst { get; set; }
        public string NameMiddle { get; set; }
        public string NameLast { get; set; }
        public string NameDisplay { get; set; }
        public string Headline { get; set; }
        public string EmailPrimary { get; set; }
        public string EmailSecondary { get; set; }
        public string PhonePrimary { get; set; }
        public string PhoneSecondary { get; set; }
        public string PhonePrimaryLabel { get; set; }
        public string PhoneSecondaryLabel { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Postal { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }

        // Additional fields such as DoNotMarket, DoNotContact, Tracking, Comments, or Bio
        // can be added here if you want them to be editable via the UI.
    }
}
