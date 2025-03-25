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
            bool isBroker = roles.Contains("Broker");
            bool isAgent = roles.Contains("Agent");

            IQueryable<Person> query = _context.Person
                .Where(p => _context.TenantToPeople
                    .Any(tp => tp.PersonID == p.Code && tp.TenantID == currentUser.TenantID && (
                        (isAdmin) ||
                        (isBroker && tp.Role == "Broker") ||
                        (isAgent && tp.Role == "Agent")
                    )));

            AllPeople = await query.OrderByDescending(p => p.Created).ToListAsync();

            ViewUserPeople = AllPeople
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}