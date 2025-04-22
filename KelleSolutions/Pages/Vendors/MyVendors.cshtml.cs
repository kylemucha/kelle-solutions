using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace KelleSolutions.Pages.Entities
{
    public class EntitiesModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public EntitiesModel(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        public List<Entity> Entities { get; set; }

        public async Task OnGetAsync()
        {
            Entities = await _context.Entities
                .Where(e => !e.Archived)
                .OrderBy(e => e.EntityName)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            var entity = await _context.Entities.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            try
            {
                // Update entity from form values
                entity.EntityName = Request.Form["EntityName"];
                if (int.TryParse(Request.Form["Category"], out int catValue))
                {
                    entity.Category = (CategoryEnum)catValue;
                }
                entity.Phone = Request.Form["Phone"];
                entity.Country = Request.Form["Country"];
                entity.City = Request.Form["City"];
                entity.StateProvince = Request.Form["StateProvince"];
                entity.Postal = Request.Form["Postal"];
                entity.Street = Request.Form["Street"];
                entity.Updated = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                // Load the entities list before returning the page
                Entities = await _context.Entities
                    .Where(e => !e.Archived)
                    .OrderBy(e => e.EntityName)
                    .ToListAsync();
                ModelState.AddModelError("", "Error updating the entity. Please try again.");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var entity = await _context.Entities.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            try
            {
                // Soft delete by setting Archived flag
                entity.Archived = true;
                entity.Updated = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting the entity. Please try again.");
                return Page();
            }
        }
    }
}
