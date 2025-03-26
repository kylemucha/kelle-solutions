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
            try
            {
                if (!ModelState.IsValid)
                {
                    // Log and show validation errors
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                            ModelState.AddModelError("", $"Error in {state.Key}: {error.ErrorMessage}");
                        }
                    }
                    return Page();
                }

                var currentUser = await _userManager.GetUserAsync(User);
                // Add debugging line here to check TenantID
                Console.WriteLine($"Current user TenantID: {currentUser?.TenantID}");
                
                var userRoles = await _userManager.GetRolesAsync(currentUser);
                string userRole = userRoles.FirstOrDefault();

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
                Person.Category = CategoryEnum.Category1;
                Person.MySource = 0;

                // Save Person to DB first
                _context.Person.Add(Person);
                await _context.SaveChangesAsync();

                // Only create TenantToPerson relationship if user has a TenantID
                if (currentUser?.TenantID != null)
                {
                    // Auto-link to TenantToPeople
                    var tenantLink = new TenantToPerson
                    {
                        TenantID = currentUser.TenantID.Value,
                        PersonID = Person.Code,
                        TenantToPersonID = 1000 + Person.Code,
                        Role = userRole ?? "User"
                    };

                    _context.TenantToPeople.Add(tenantLink);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("/People/People");
            }
            catch (Exception ex)
            {
                // Log the full exception details
                Console.WriteLine($"Error creating person: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                ModelState.AddModelError("", $"Error: {ex.Message}");
                return Page();
            }
        }
    }
}
