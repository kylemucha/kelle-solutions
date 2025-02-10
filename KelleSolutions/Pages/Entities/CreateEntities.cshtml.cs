using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace KelleSolutions.Pages.Entities
{
    public class CreateEntitiesModel : PageModel
    {
        [BindProperty]
        public EntityInputModel Entity { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("ViewEntities");
        }
    }

    public class EntityInputModel
    {
        [Required]
        [Display(Name = "Entity Name")]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; }

        [Required]
        public string Location { get; set; }
    }
}
