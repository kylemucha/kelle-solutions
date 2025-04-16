// People.cshtml.cs - Handles listing, updating, and deleting People records via Razor Pages
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.People
{
    public class PeopleModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public PeopleModel(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        // Holds the list of people for display in the table
        public List<PersonView> PeopleList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        // This holds form data for Create/Edit submissions
        [BindProperty]
        public Person PersonRecord { get; set; }

        // Pagination helpers
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        // GET: Load and paginate People list
        public async Task OnGetAsync()
        {
            TotalCount = await _context.People
                .Where(p => !p.Archived)
                .CountAsync();

            var people = await _context.People
                .Where(p => !p.Archived)
                .OrderByDescending(p => p.Created)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            PeopleList = people.Select(p => new PersonView
            {
                Code = p.Code,
                NameLast = p.NameLast,
                NameFirst = p.NameFirst,
                PhonePrimary = p.PhonePrimary,
                EmailPrimary = p.EmailPrimary,
                Created = p.Created,
                Category = p.Category.ToString()
            }).ToList();
        }

        // POST: Update an existing person via the modal
        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"{modelState.Key}: {error.ErrorMessage}");
                    }
                }

            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // reload list to preserve view
                return Page();
            }

            var personToUpdate = await _context.People.FindAsync(PersonRecord.Code);

            if (personToUpdate == null)
            {
                return NotFound();
            }

            // Copy fields from bound property to DB entity
            personToUpdate.NameFirst = PersonRecord.NameFirst;
            personToUpdate.NameMiddle = PersonRecord.NameMiddle;
            personToUpdate.NameLast = PersonRecord.NameLast;
            personToUpdate.NameDisplay = PersonRecord.NameDisplay;
            personToUpdate.Headline = PersonRecord.Headline;
            personToUpdate.EmailPrimary = PersonRecord.EmailPrimary;
            personToUpdate.EmailSecondary = PersonRecord.EmailSecondary;
            personToUpdate.EmailPrimaryLabel = PersonRecord.EmailPrimaryLabel;
            personToUpdate.EmailSecondaryLabel = PersonRecord.EmailSecondaryLabel;
            personToUpdate.PhonePrimary = PersonRecord.PhonePrimary;
            personToUpdate.PhoneSecondary = PersonRecord.PhoneSecondary;
            personToUpdate.PhonePrimaryLabel = PersonRecord.PhonePrimaryLabel;
            personToUpdate.PhoneSecondaryLabel = PersonRecord.PhoneSecondaryLabel;
            personToUpdate.Street = PersonRecord.Street;
            personToUpdate.City = PersonRecord.City;
            personToUpdate.StateProvince = PersonRecord.StateProvince;
            personToUpdate.Postal = PersonRecord.Postal;
            personToUpdate.Country = PersonRecord.Country;
            personToUpdate.Category = PersonRecord.Category;
            personToUpdate.DoNotMarket = PersonRecord.DoNotMarket;
            personToUpdate.DoNotContact = PersonRecord.DoNotContact;
            personToUpdate.Comments = PersonRecord.Comments;
            personToUpdate.Bio = PersonRecord.Bio;
            personToUpdate.Updated = DateTime.UtcNow;

            try
            {
                _context.Entry(personToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToPage(); // go back to refreshed /People page
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating person: {ex.Message}");
                await OnGetAsync();
                return Page();
            }
        }

        // POST: Soft-delete a person
        public async Task<IActionResult> OnPostDeleteAsync(int code)
        {
            if (!User.IsInRole("Admin") && !User.IsInRole("Broker"))
            {
                return Forbid();
            }

            var person = await _context.People.FindAsync(code);
            if (person == null)
            {
                return NotFound();
            }
            try
            {
                person.Archived = true;
                person.Updated = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting person: {ex.Message}");
                ModelState.AddModelError("", "Error deleting person. Please try again.");
                await OnGetAsync();
                return Page();
            }
        }
    }

    // View model used to list people in the table
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
