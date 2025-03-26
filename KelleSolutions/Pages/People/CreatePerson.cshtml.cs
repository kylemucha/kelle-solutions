using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using KelleSolutions.Models;
using KelleSolutions.Data;
using System.Threading.Tasks;
using System;

namespace KelleSolutions.Pages.People
{
    public class CreatePersonModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User> _userManager;

        public CreatePersonModel(KelleSolutionsDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            // Save Person to DB
            _context.Person.Add(Person);
            await _context.SaveChangesAsync(); 

            // Auto-link to TenantToPeople
            var tenantLink = new TenantToPerson
            {
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
}