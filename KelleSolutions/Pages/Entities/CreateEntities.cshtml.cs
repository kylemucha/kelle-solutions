using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System.Threading.Tasks;

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

            _context.Entities.Add(Entity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Entities");
        }
    }
}
