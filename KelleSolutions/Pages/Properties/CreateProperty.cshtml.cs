using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using KelleSolutions.Models;
using KelleSolutions.Data;
using System;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Properties
{
    public class CreatePropertyModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User> _userManager;

        public CreatePropertyModel(KelleSolutionsDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            // Initialize new Property with default values.
            Property = new Property
            {
                Street = string.Empty,
                City = string.Empty,
                StateProvince = string.Empty,
                Postal = string.Empty,
                SqFt_Building = null,
                SqFt_Land = null,
                Beds = 0,
                Baths = 0,
                Garages = 0,
                Constructed = (short)DateTime.UtcNow.Year,
                MyType = PropertyTypeEnum.SingleFamilyHome, // Ensure Property.MyType is of type PropertyTypeEnum
                Comments = string.Empty
            };
        }

        [BindProperty]
        public Property Property { get; set; }

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
                var newProperty = new Property
                {
                    Street = Property.Street,
                    City = Property.City,
                    StateProvince = Property.StateProvince,
                    Postal = Property.Postal,
                    SqFt_Building = Property.SqFt_Building,
                    SqFt_Land = Property.SqFt_Land,
                    Beds = Property.Beds,
                    Baths = Property.Baths,
                    Garages = Property.Garages,
                    Constructed = Property.Constructed,
                    MyType = Property.MyType,
                    Comments = Property.Comments,
                    Created = DateTime.UtcNow
                    // Code is auto-generated.
                };

                _context.Properties.Add(newProperty);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Properties/MyProperties");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the property. Please try again.");
                Console.WriteLine($"Error: {ex.Message}");
                return Page();
            }
        }
    }
}
