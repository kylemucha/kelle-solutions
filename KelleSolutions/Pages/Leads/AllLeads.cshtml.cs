using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System.Text.Json;

namespace KelleSolutions.Pages.Leads
{
    public class AllLeadsModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User>       _userManager;

        public AllLeadsModel(KelleSolutionsDbContext context, UserManager<User> userManager)
        {
            _context     = context;
            _userManager = userManager;
        }

        /*────────────  Query-string parameter  ────────────*/
        [BindProperty(SupportsGet = true)]
        public int? PersonId { get; set; }          //  /AllLeads?personId=42

        /*────────────  Bindables / collections  ────────────*/
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

        public List<Lead> AllLeads { get; private set; } = new();

        [BindProperty(SupportsGet = true)] public int PageSize   { get; set; } = 10;
        [BindProperty(SupportsGet = true)] public int PageNumber { get; set; } = 1;
        public int TotalPages { get; private set; }

        /*──────────────────────── GET ───────────────────────*/
        public async Task<IActionResult> OnGetAsync()
        {
            if (!(User.IsInRole("Admin") || User.IsInRole("Broker")))
                return Forbid();                                // 403

            IQueryable<Lead> query = _context.Leads.Where(l => !l.Archived);

            /* If personId given → only that person’s leads */
            if (PersonId is int pid && pid > 0)
            {
                query = query.Where(l => _context.PersonToLeads
                                       .Any(ptl => ptl.Lead   == l.Code &&
                                                   ptl.Person == pid     &&
                                                  !ptl.Deprecated));
            }

            int total = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(total / (double)PageSize);

            AllLeads = await query.OrderBy(l => l.NameFirst)
                                  .Skip((PageNumber - 1) * PageSize)
                                  .Take(PageSize)
                                  .ToListAsync();

            return Page();
        }

        /*──────────────────────── POST – Create ───────────────────────*/
        public async Task<JsonResult> OnPostCreateListingAsync()
        {
            if (!(User.IsInRole("Admin") || User.IsInRole("Broker")))
                return new JsonResult(new { success = false, message = "Forbidden" }) { StatusCode = 403 };

            try
            {
                /* read body */
                Request.EnableBuffering();
                string body;
                using var reader = new StreamReader(Request.Body, leaveOpen: true);
                body = await reader.ReadToEndAsync();
                Request.Body.Position = 0;

                if (string.IsNullOrWhiteSpace(body))
                    return new JsonResult(new { success = false, message = "Empty request body" });

                var input = JsonSerializer.Deserialize<CreateLeadInputModel>(
                                body,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (input == null ||
                    string.IsNullOrWhiteSpace(input.FirstName) ||
                    string.IsNullOrWhiteSpace(input.LastName)  ||
                    string.IsNullOrWhiteSpace(input.Email))
                {
                    return new JsonResult(new { success = false, message = "Missing required fields" });
                }

                /* create lead */
                var newLead = new Lead
                {
                    NameFirst        = input.FirstName,
                    NameMiddle       = input.MiddleName,
                    NameLast         = input.LastName,
                    Email            = input.Email,
                    Phone            = input.Phone,
                    Country          = input.Country,
                    StateProvince    = input.StateProvince,
                    City             = input.City,
                    Postal           = input.Postal,
                    Street           = input.Street,
                    OrganizationName = input.OrganizationName,
                    OrganizationTitle= input.OrganizationTitle,
                    Tracking         = input.Tracking,
                    Archived         = false,
                    Operator         = 0,
                    Originator       = 0,
                    Team             = 0,
                    Visibility       = 0,
                    StageWorked      = false,
                    TempWarm         = false,
                    Created          = DateTime.UtcNow,
                    Updated          = DateTime.UtcNow
                };

                _context.Leads.Add(newLead);
                await _context.SaveChangesAsync();   // <-- we now have newLead.Code

                /* ---------- link lead → person ---------- */
                if (!int.TryParse(Request.Query["personId"], out int personId))
                    return new JsonResult(new { success = false, message = "personId missing" });

                var map = new PersonToLeads
                {
                    Deprecated = false,
                    Created    = DateTime.UtcNow,
                    Creator    = 0,          // TODO: your user id
                    Person     = personId,
                    Lead       = newLead.Code,
                    Relation   = 0,
                    Comments   = string.Empty
                };
                _context.PersonToLeads.Add(map);
                await _context.SaveChangesAsync();
                /* ---------- end link ---------- */

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        /*──────────────────────── POST – Update ───────────────────────*/
        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            if (!(User.IsInRole("Admin") || User.IsInRole("Broker")))
                return Forbid();

            var lead = await _context.Leads.FindAsync(id);
            if (lead == null) return NotFound();

            lead.NameFirst         = Request.Form["NameFirst"];
            lead.NameMiddle        = Request.Form["NameMiddle"];
            lead.NameLast          = Request.Form["NameLast"];
            lead.Email             = Request.Form["Email"];
            lead.Phone             = Request.Form["Phone"];
            lead.Country           = Request.Form["Country"];
            lead.StateProvince     = Request.Form["StateProvince"];
            lead.City              = Request.Form["City"];
            lead.Postal            = Request.Form["Postal"];
            lead.Street            = Request.Form["Street"];
            lead.OrganizationName  = Request.Form["OrganizationName"];
            lead.OrganizationTitle = Request.Form["OrganizationTitle"];
            lead.Tracking          = Request.Form["Tracking"];
            lead.Updated           = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return RedirectToPage(new { PersonId, PageNumber, PageSize });
        }

        /*──────────────────────── POST – Delete ───────────────────────*/
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (!(User.IsInRole("Admin") || User.IsInRole("Broker")))
                return Forbid();

            var lead = await _context.Leads.FindAsync(id);
            if (lead == null) return NotFound();

            lead.Archived = true;
            lead.Updated  = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return RedirectToPage(new { PersonId, PageNumber, PageSize });
        }

        /*──────────── helper DTO for JSON create ────────────*/
        private sealed class CreateLeadInputModel
        {
            public string? FirstName          { get; set; }
            public string? MiddleName         { get; set; }
            public string? LastName           { get; set; }
            public string? Email              { get; set; }
            public string? Phone              { get; set; }
            public string? Country            { get; set; }
            public string? StateProvince      { get; set; }
            public string? City               { get; set; }
            public string? Postal             { get; set; }
            public string? Street             { get; set; }
            public string? OrganizationName   { get; set; }
            public string? OrganizationTitle  { get; set; }
            public string? Tracking           { get; set; }
        }
    }
}
