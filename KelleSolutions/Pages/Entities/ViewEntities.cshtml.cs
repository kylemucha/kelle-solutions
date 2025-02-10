using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Entities
{
    public class ViewEntitiesModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;
        public string UserName { get; set; }
        public int UserId { get; set; }
        public List<Entity> Entities { get; set; }

        public ViewEntitiesModel(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int userId)
        {
            UserId = userId;
            Entities = await _context.Entities.ToListAsync();
            UserName = userId == 1 ? "Randall Watts" : "John Doe";
        }

        public async Task<IActionResult> OnPostCreateAsync(int userId)
        {
            var entity = new Entity
            {
                Name = Request.Form["Name"],
                Category = Request.Form["Category"],
                Phone = Request.Form["Phone"],
                Location = Request.Form["Location"]
            };
            
            _context.Entities.Add(entity);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { userId = userId });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id, int userId)
        {
            var entity = await _context.Entities.FindAsync(id);
            if (entity != null)
            {
                _context.Entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage(new { userId = userId });
        }
    }
}
