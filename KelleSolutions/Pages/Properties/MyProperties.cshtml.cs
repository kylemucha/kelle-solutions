using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models;
using KelleSolutions.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Properties
{
    public class MyPropertiesModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User> _userManager;

        public MyPropertiesModel(KelleSolutionsDbContext context, UserManager<User> userManager)
        {
            _context = context;
             _userManager = userManager;
        }

        // The complete list of properties
        public List<ViewUserProperties> AllProperties { get; set; } = new();

        // The paginated list of properties to display
        public List<ViewUserProperties> ViewUserProperties { get; set; } = new();

        // Paging parameters
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages => (int)Math.Ceiling((double)AllProperties.Count / PageSize);

        // Bind property to capture new property input from the modal
        [BindProperty]
        public Property NewProperty { get; set; } = new Property();

        private async Task<int> GetPersonIdForUserAsync(User currentUser)
        {
        // Retrieve the user's email from AspNetUsers using UserManager. The email is stored in the UserName column.
        var userEmail = await _userManager.GetUserNameAsync(currentUser);
        if (string.IsNullOrWhiteSpace(userEmail))
        {
           throw new Exception("Current user email is not set in AspNetUsers.");
        }

        // For debugging purposes, you can log the email:
        Console.WriteLine($"Looking for Person record with EmailPrimary matching: '{userEmail}'");

        // Query the Person table for a record whose EmailPrimary matches the current user's email.
        var person = await _context.Person.FirstOrDefaultAsync(p => p.EmailPrimary == userEmail.Trim());
        if (person == null)
        {
            throw new Exception("No Person record found for the current user.");
        }

        return person.Code;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            // Retrieve the current user
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToPage("/Account/Login");
            }

            // Retrieve the current user's Person ID using the helper method
            int personId = await GetPersonIdForUserAsync(currentUser);

            // Perform a join between Properties and PersonToProperties
            var propertiesFromDb = await (
                from p in _context.Properties
                join ptp in _context.PersonToProperties on p.Code equals ptp.Properties
                where ptp.Person == personId
                orderby p.Created descending
                select new
                {
                    MappingCode = ptp.Code, // Uses the Code value from PersonToProperties instead of the one from the Properties table
                    p.Created,
                    p.County,
                    p.City,
                    p.Postal,
                    p.Street,
                    p.StateProvince,
                    p.Beds,
                    p.Baths
                }
            ).ToListAsync();


            AllProperties = propertiesFromDb.Select(p => new ViewUserProperties
            {
                ID = p.MappingCode, 
                CreationDate = p.Created.HasValue ? DateOnly.FromDateTime(p.Created.Value) : DateOnly.MinValue,
                County = p.County,
                City = p.City,
                Postal = p.Postal ?? "",
                Street = p.Street,
                StateProvince = p.StateProvince,
                Bed = p.Beds.HasValue ? p.Beds.Value : 0,
                Bath = p.Baths.HasValue ? p.Baths.Value : 0
            }).ToList();


            ViewUserProperties = AllProperties
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAddPropertyAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            // Retrieve the current user.
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToPage("/Account/Login");
            }

            // Retrieve the current user's Person ID.
            int personId = await GetPersonIdForUserAsync(currentUser);

            // Save the new property.
            _context.Properties.Add(NewProperty);
            await _context.SaveChangesAsync();

            // Now, add a mapping record to link this property with the current user's Person record.
            // Adjust the property names as they are defined in your PersonToProperties entity.
            var mapping = new PersonToProperties{
            Code = 0,
            Person = personId,
            Properties = NewProperty.Code,
            Deprecated = false,
            Created = DateTime.UtcNow,
            Creator = ((short)personId), 
            Relation = 1,
            Comments = ""
            };
    
            _context.PersonToProperties.Add(mapping);
            await _context.SaveChangesAsync();
        
            // Redirect back to the page to show the updated list of properties.
            return RedirectToPage();
        }
    }
}
