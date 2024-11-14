using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using KelleSolutions.Models; // Ensure this includes your Role model

namespace KelleSolutions.Pages
{
    public class RolesModel : PageModel
    {
        public List<Role> Roles { get; set; }

        public void OnGet()
        {
            // Example data; replace with data fetching from your database
            Roles = new List<Role>
            {
                new Role { Id = 1, Name = "Admin", NormalizedName = "AdminRole" },
                new Role { Id = 2, Name = "Tenant", NormalizedName = "TenantRole" },
                new Role { Id = 3, Name = "Agent", NormalizedName = "AgentRole" }
            };
        }
    }
}
