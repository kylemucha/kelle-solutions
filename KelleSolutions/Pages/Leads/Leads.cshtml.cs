using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Leads
{
    public class LeadsModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public LeadsModel(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        public List<UserLeadRow> UserLeads { get; private set; } = new();

        /*──────────────────────── GET ───────────────────────*/
        public async Task<IActionResult> OnGetAsync()
        {
            if (!(User.IsInRole("Admin") || User.IsInRole("Broker")))
                return Forbid();                           // 403

            /* every person + count of their active leads */
            UserLeads = await _context.People
                                      .Where(p => !p.Archived)
                                      .GroupJoin(
                                          _context.PersonToLeads.Where(m => !m.Deprecated),
                                          person => person.Code,
                                          map    => map.Person,
                                          (person, maps) => new { person, maps })
                                      .Select(g => new UserLeadRow
                                      {
                                          Id        = g.person.Code,
                                          UserName  = g.person.NameFirst + " " + g.person.NameLast,
                                          LeadCount = g.maps.Count()
                                      })
                                      .OrderBy(r => r.UserName)
                                      .ToListAsync();

            return Page();
        }

        /*──────────── DTO to Razor ────────────*/
        public class UserLeadRow
        {
            public int    Id        { get; set; }
            public string UserName  { get; set; } = string.Empty;
            public int    LeadCount { get; set; }
        }
    }
}
