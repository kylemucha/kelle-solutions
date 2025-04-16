using KelleSolutions.Data;
using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KelleSolutions.Pages.People
{
    public class ViewPeopleModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User> _userManager;

        public required List<Person> AllPeople { get; set; }
        public required List<Person> ViewUserPeople { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages => (int)Math.Ceiling(AllPeople.Count / (double)PageSize);

        public ViewPeopleModel(KelleSolutionsDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(currentUser);

            bool isAdmin = roles.Contains("Admin");
            IQueryable<Person> query;

            if (isAdmin)
            {
                // Admin sees all people in their tenant
                query = from p in _context.Person
                        join tp in _context.TenantToPeople on p.Code equals tp.PersonID
                        where tp.TenantID == currentUser.TenantID
                        select p;
            }
            else
            {
                // Broker/Agent see only people assigned to them
                query = from p in _context.Person
                        join tp in _context.TenantToPeople on p.Code equals tp.PersonID
                        where tp.TenantID == currentUser.TenantID &&
                              tp.AssignedToUserId == currentUser.Id
                        select p;
            }

            AllPeople = await query.OrderByDescending(p => p.Created).ToListAsync();

            ViewUserPeople = AllPeople
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}