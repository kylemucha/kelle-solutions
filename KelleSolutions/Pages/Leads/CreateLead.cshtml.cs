using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Models;
using KelleSolutions.Data;
using System.Threading.Tasks;
using System;

namespace KelleSolutions.Pages.Leads
{
    public class CreateLeadModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public CreateLeadModel(KelleSolutionsDbContext context)
        {
            _context = context;
            // Initialize a new Lead with default (empty) values.
            Lead = new Lead
            {
                NameFirst = string.Empty,
                NameMiddle = string.Empty,
                NameLast = string.Empty,
                Email = string.Empty,
                Phone = string.Empty,
                Country = string.Empty,
                StateProvince = string.Empty,
                City = string.Empty,
                Postal = string.Empty,
                Street = string.Empty,
                OrganizationName = string.Empty,
                OrganizationTitle = string.Empty,
                Tracking = string.Empty,
                // Set default values for required booleans.
                Archived = false,
                StageWorked = false,
                TempWarm = false,
                DoNotMarket = false,
                DoNotContact = false,
                // Set default enum values.
                Operator = OperatorEnum.Operator1,
                Originator = OriginatorEnum.Originator1,
                Team = TeamEnum.TeamA,
                Visibility = VisibilityEnum.Low,
                OriginatorFeeRate = 0,
                OriginatorFeeFixed = 0
            };
        }

        [BindProperty]
        public Lead Lead { get; set; }

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
                Lead.Created = DateTime.Now;
                Lead.Updated = DateTime.Now;
                _context.Leads.Add(Lead);
                await _context.SaveChangesAsync();
                // Redirect back to the MyLeads page so the new lead is visible.
                return RedirectToPage("/Leads/MyLeads");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the lead. Please try again.");
                Console.WriteLine($"Error: {ex.Message}");
                return Page();
            }
        }
    }
}
