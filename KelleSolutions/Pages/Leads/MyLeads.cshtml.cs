using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Leads
{
    public class ViewLeadsModel : PageModel
    {
        public List<ViewUserLeads> AllLeads { get; set; }
        public List<ViewUserLeads> ViewUserLeads { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10; // Default to 10 properties per page

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public List<string> SelectedStatuses { get; set; } = new();

        public int TotalPages => (int)System.Math.Ceiling((double)AllLeads.Count / PageSize);

        public void OnGet()
        {
            // Sample Data
            AllLeads = new List<ViewUserLeads>();
            for (int i = 1; i <= 128; i++)
            {
                AllLeads.Add(new ViewUserLeads
                {
                    ID = i,
                    CreationDate = new DateOnly(2024, 2, 1),
                    Status = i % 2 == 0 ? "ON HOLD" : "ACTIVE",
                    Assignee = "Randall Watts",
                    LastName = "Stone",
                    FirstName = "Billy",
                    Phone = 1234567890,
                    Email = "billy@stone.com"
                });
            }

            // Apply Filtering
            if (SelectedStatuses != null && SelectedStatuses.Count > 0)
            {
                AllLeads = AllLeads.Where(l => SelectedStatuses.Contains(l.Status)).ToList();
            }

            // Apply Pagination
            ViewUserLeads = AllLeads.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostUpdateStatus([FromBody] LeadsUpdateStatusModel model)
        {
            if (model == null || model.Id == 0)
            {
                return BadRequest("Invalid data.");
            }

            var lead = AllLeads.FirstOrDefault(l => l.ID == model.Id);
            if (lead == null)
            {
                return NotFound();
            }

            lead.Status = model.Status;

            return new JsonResult(new { success = true, message = "Status updated successfully." });
        }
    }

    public class ViewUserLeads
    {
        public int ID { get; set; }
        public DateOnly CreationDate { get; set; }
        public string Status { get; set; }
        public string Assignee { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
    }

    public class LeadsUpdateStatusModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
