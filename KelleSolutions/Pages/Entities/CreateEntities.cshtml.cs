using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KelleSolutions.Pages.Entities
{
    public class CreateEntitiesModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public CreateEntitiesModel(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Entity Entity { get; set; }

        public void OnGet()
        {
            Entity = new Entity
            {
                Archived = false,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                Operator = OperatorEnum.Operator1,
                Team = TeamEnum.TeamA,
                Visibility = VisibilityEnum.Medium,
                Website = string.Empty,
                DoNot_Market = false,
                DoNot_Contact = false
            };
        }

        private int GenerateEntityCode()
        {
            // Generate a unique code for the entity
            Random rnd = new Random();
            return rnd.Next(1000, 9999);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Set timestamps and default values
                Entity.Created = DateTime.UtcNow;
                Entity.Updated = DateTime.UtcNow;
                Entity.Archived = false;
                Entity.Website = string.Empty;
                Entity.Comments = string.Empty;
                
                // Add to database
                _context.Entities.Add(Entity);
                await _context.SaveChangesAsync();

                // Redirect to the Entities page
                return RedirectToPage("./Entities");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error saving entity: {ex.Message}");
                return Page();
            }
        }
    }
}
