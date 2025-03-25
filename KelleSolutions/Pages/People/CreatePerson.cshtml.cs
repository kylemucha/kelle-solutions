using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using KelleSolutions.Models;
using KelleSolutions.Data;
<<<<<<< Updated upstream
using System;
=======
>>>>>>> Stashed changes
using System.Threading.Tasks;
using System;

namespace KelleSolutions.Pages.People
{
    public class CreatePersonModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly KelleSolutionsDbContext _context;

<<<<<<< Updated upstream
        public CreatePersonModel(UserManager<User> userManager, KelleSolutionsDbContext context)
=======
        public CreatePersonModel(KelleSolutionsDbContext context, UserManager<User> userManager)
>>>>>>> Stashed changes
        {
            _context = context;
            _userManager = userManager;
<<<<<<< Updated upstream
            _context = context;
            
            // initialize an empty person with default values
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
=======

>>>>>>> Stashed changes
        }

        [BindProperty]
        public Person Person { get; set; }

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

<<<<<<< Updated upstream
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return RedirectToPage("/Account/Login");
                }

                // create a new Person entity and map the view model data to it
                var personEntity = new Person
                {
                    // Basic information
                    NameFirst = Person.NameFirst,
                    NameMiddle = Person.NameMiddle ?? string.Empty,
                    NameLast = Person.NameLast,
                    NameDisplay = !string.IsNullOrEmpty(Person.NameDisplay) ? 
                        Person.NameDisplay : 
                        $"{Person.NameFirst} {Person.NameLast}",
                    Headline = Person.Headline ?? string.Empty,
                    
                    // Contact information
                    EmailPrimary = Person.EmailPrimary,
                    EmailSecondary = Person.EmailSecondary ?? string.Empty,
                    EmailPrimaryLabel = "Primary",
                    EmailSecondaryLabel = "Secondary",
                    PhonePrimary = Person.PhonePrimary,
                    PhoneSecondary = Person.PhoneSecondary ?? string.Empty,
                    PhonePrimaryLabel = Person.PhonePrimaryLabel ?? "Primary",
                    PhoneSecondaryLabel = Person.PhoneSecondaryLabel ?? "Secondary",
                    
                    // Address information
                    Street = Person.Street,
                    City = Person.City,
                    StateProvince = Person.StateProvince,
                    Postal = Person.Postal,
                    Country = Person.Country,
                    
                    // System fields
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    Archived = false,
                    Operator = OperatorEnum.Operator1,
                    Team = TeamEnum.TeamA,
                    Visibility = VisibilityEnum.Medium,
                    Category = CategoryEnum.Category1,
                    MySource = MySourceEnum.Internal,
                    Verified = false,
                    VIP = false,
                    DoNotMarket = false,
                    DoNotContact = false,
                    
                    // Additional fields
                    Tracking = string.Empty,
                    Comments = Person.Notes ?? string.Empty,
                    Bio = string.Empty
                };

                // Add to database and save changes
                _context.People.Add(personEntity);
                await _context.SaveChangesAsync();
=======
            var currentUser = await _userManager.GetUserAsync(User);
            var userRoles = await _userManager.GetRolesAsync(currentUser);
            string userRole = userRoles.FirstOrDefault(); // Admin, Broker, or Agent

            // Backend-required fields
            Person.Archived = false;
            Person.VIP = false;
            Person.Verified = false;
            Person.DoNotContact = false;
            Person.DoNotMarket = false;
            Person.Created = DateTime.UtcNow;
            Person.Updated = DateTime.UtcNow;
            Person.Operator = 0;
            Person.Team = 0;
            Person.Visibility = 0;
            Person.Category = 0;
            Person.MySource = 0;
>>>>>>> Stashed changes

            // Save Person to DB
            _context.Person.Add(Person);
            await _context.SaveChangesAsync(); 

            // Auto-link to TenantToPeople
            var tenantLink = new TenantToPerson
            {
<<<<<<< Updated upstream
                ModelState.AddModelError("", "An error occurred while saving the person. Please try again.");
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
    }
=======
                TenantID = currentUser.TenantID ?? 0,
                PersonID = Person.Code, // Code is the PK from Person (just created)
                TenantToPersonID = 1000 + Person.Code, // temp logic; adjust as needed
                Role = userRole
            };

            _context.TenantToPeople.Add(tenantLink);
            await _context.SaveChangesAsync();

            return RedirectToPage("/People/MyPeople");
        }
    }
>>>>>>> Stashed changes
}