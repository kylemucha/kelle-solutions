using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KelleSolutions.Pages.People
{
    public class ViewPersonModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User> _userManager;

        public ViewPersonModel(KelleSolutionsDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Person PersonRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int code)
        {
            if (code <= 0)
            {
                return NotFound();
            }

            // Load the person record from the database
            PersonRecord = await _context.People.FindAsync(code);

            if (PersonRecord == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get the existing person record from the database
            var personToUpdate = await _context.People.FindAsync(PersonRecord.Code);

            if (personToUpdate == null)
            {
                return NotFound();
            }

            // Update the person entity with form values
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
            
            // Update the timestamp
            personToUpdate.Updated = DateTime.UtcNow;

            try
            {
                // Mark the entity as modified
                _context.Entry(personToUpdate).State = EntityState.Modified;
                
                await _context.SaveChangesAsync();
                return RedirectToPage("/People/People");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating person: {ex.Message}");
                return Page();
            }
        }
    }
}