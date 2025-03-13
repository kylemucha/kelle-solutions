using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            // Use Code as the key property
            Entities = await _context.Entities
                .OrderBy(e => e.Code)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            var entity = await _context.Entities.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            // Update entity from form values with new field names
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
            entity.Website = Request.Form["Website"];
            entity.Comments = Request.Form["Comments"];

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var entity = await _context.Entities.FindAsync(id);
            if (entity != null)
            {
                _context.Entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
