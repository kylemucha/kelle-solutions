using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Leads
{
    public class ViewLeadsModel : PageModel
    {
        private readonly KelleSolutionsDbContext _ctx;
        private readonly UserManager<User>       _userMgr;

        public ViewLeadsModel(KelleSolutionsDbContext ctx, UserManager<User> userMgr)
        {
            _ctx     = ctx;
            _userMgr = userMgr;
        }

        
        public List<ViewUserLeadRow> AllLeads      { get; private set; } = new();
        public List<ViewUserLeadRow> ViewUserLeads { get; private set; } = new();

        
        public int PageSize   { get; private set; } = 10;
        public int PageNumber { get; private set; } = 1;
        public int TotalPages => (int)Math.Ceiling(AllLeads.Count / (double)PageSize);

       
        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize   = pageSize;

            
            var aspUser   = await _userMgr.GetUserAsync(User);
            var loginMail = aspUser?.Email ?? aspUser?.UserName ?? string.Empty;

            int? personId = await _ctx.People
                                      .Where(p => p.EmailPrimary == loginMail)
                                      .Select(p => (int?)p.Code)
                                      .FirstOrDefaultAsync();

            if (personId is null)
            {
                
                return;
            }
            var leadQuery =
                    _ctx.PersonToLeads
                        .Where(ptl => ptl.Person == personId &&
                                      !ptl.Deprecated)
                        .Include(ptl => ptl.LeadNavigation)
                        .Select(ptl => ptl.LeadNavigation)
                        .Where(l => !l.Archived);

            var rawLeads = await leadQuery
                                  .OrderBy(l => l.NameFirst)
                                  .ToListAsync();

        
            AllLeads = rawLeads
                       .Select(l => new ViewUserLeadRow
                       {
                           ID   = l.Code,
                           CreationDate = l.Created.HasValue
                                          ? DateOnly.FromDateTime(l.Created.Value)
                                          : DateOnly.MinValue,
                           FullName =
                               l.NameFirst +
                               (string.IsNullOrWhiteSpace(l.NameMiddle)
                                 ? " "
                                 : $" {l.NameMiddle} ") +
                               l.NameLast,
                           Phone              = l.Phone,
                           Email              = l.Email,
                           Country            = l.Country,
                           StateProvince      = l.StateProvince,
                           City               = l.City,
                           Postal             = l.Postal,
                           Street             = l.Street,
                           OrganizationName   = l.OrganizationName,
                           OrganizationTitle  = l.OrganizationTitle,
                           Tracking           = l.Tracking
                       })
                       .ToList();

            ViewUserLeads = AllLeads
                           .Skip((PageNumber - 1) * PageSize)
                           .Take(PageSize)
                           .ToList();
        }
    }

    public class ViewUserLeadRow
    {
        public int      ID               { get; set; }
        public DateOnly CreationDate     { get; set; }
        public string   FullName         { get; set; } = string.Empty;
        public string   Phone            { get; set; } = string.Empty;
        public string   Email            { get; set; } = string.Empty;
        public string   Country          { get; set; } = string.Empty;
        public string   StateProvince    { get; set; } = string.Empty;
        public string   City             { get; set; } = string.Empty;
        public string   Postal           { get; set; } = string.Empty;
        public string   Street           { get; set; } = string.Empty;
        public string   OrganizationName { get; set; } = string.Empty;
        public string   OrganizationTitle{ get; set; } = string.Empty;
        public string   Tracking         { get; set; } = string.Empty;
    }
}
