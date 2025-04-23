using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Leads
{
    public class CreateLeadModel : PageModel
    {
        
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User>       _userManager;

        public CreateLeadModel(KelleSolutionsDbContext context,
                               UserManager<User>       userManager)
        {
            _context     = context;
            _userManager = userManager;
        }

        
        [BindProperty]
        public Lead Lead { get; set; } = new Lead 
        {
            NameFirst        = string.Empty,
            NameMiddle       = string.Empty,
            NameLast         = string.Empty,
            Email            = string.Empty,
            Phone            = string.Empty,
            Country          = string.Empty,
            StateProvince    = string.Empty,
            City             = string.Empty,
            Postal           = string.Empty,
            Street           = string.Empty,
            OrganizationName = string.Empty,
            OrganizationTitle= string.Empty,
            Tracking         = string.Empty
        };

        
        public IActionResult OnGet() => Page();

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            
            var aspUser  = await _userManager.GetUserAsync(User);
            var personId = await _context.People
                                         .Where (p => p.EmailPrimary == aspUser!.UserName)
                                         .Select(p => (int?)p.Code)
                                         .FirstOrDefaultAsync();

            if (personId is null)
            {
                ModelState.AddModelError(string.Empty,
                    "Your login is not linked to a Person record, so the lead cannot be assigned.");
                return Page();
            }

            
            Lead.Archived    = false;
            Lead.Operator    = 0;
            Lead.Originator  = 0;
            Lead.Team        = 0;
            Lead.Visibility  = 0;
            Lead.StageWorked = false;
            Lead.TempWarm    = false;
            Lead.Created     = DateTime.UtcNow;
            Lead.Updated     = DateTime.UtcNow;

            
            _context.Leads.Add(Lead);
            await _context.SaveChangesAsync();   // generates Lead.Code (PK)

            
            var link = new PersonToLeads
            {
                Deprecated = false,
                Created    = DateTime.UtcNow,
                Creator    = (short)personId.Value,   // or another value that fits your schema
                Person     = personId.Value,
                Lead       = Lead.Code,               // newly generated PK
                Relation   = 0,                       // set a sensible default
                Comments   = string.Empty
            };

            _context.PersonToLeads.Add(link);
            await _context.SaveChangesAsync();

            
            return RedirectToPage("/Leads/MyLeads");
        }
    }
}
