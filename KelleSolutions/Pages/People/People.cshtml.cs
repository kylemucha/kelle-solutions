using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.People
{
    public class PeopleModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;

        public PeopleModel(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        public List<PersonView> PeopleList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        // Total number of people in the data source
        public int TotalCount { get; set; }

        // Calculated total pages for pagination
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public async Task OnGetAsync()
        {
            // Get the count of all people (not archived)
            TotalCount = await _context.People
                .Where(p => !p.Archived)
                .CountAsync();

            // Get paged results from database
            var people = await _context.People
                .Where(p => !p.Archived)
                .OrderByDescending(p => p.Created)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            // Map database entities to view model
            PeopleList = people.Select(p => new PersonView
            {
                Code = p.Code,
                NameLast = p.NameLast,
                NameFirst = p.NameFirst,
                PhonePrimary = p.PhonePrimary,
                EmailPrimary = p.EmailPrimary,
                Created = p.Created,
                Category = p.Category.ToString()  // Enum to string
            }).ToList();
        }
    }

    // The view model
    public class PersonView
    {
        public int Code { get; set; }
        public string NameLast { get; set; }
        public string NameFirst { get; set; }
        public string PhonePrimary { get; set; }
        public string EmailPrimary { get; set; }
        public DateTime Created { get; set; }
        public string Category { get; set; }
    }
}