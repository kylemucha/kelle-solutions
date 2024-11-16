using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic; // For using List<T> collection
//using KelleSolutions.Models; // To access the Role model from the KelleSolutions.Models namespace

namespace KelleSolutions.Pages
{
    public class RolesModel : PageModel
    {
        // A list of Role objects to store the roles to be displayed on the page
        public List<Role> Roles { get; set; }

        public void OnGet()
        {
            // Creating a new list of Role objects with sample data for the roles.
            // Each role has an Id, Name, and NormalizedName.
            Roles = new List<Role>
            {
                new Role { Id = 1, Name = "Admin", NormalizedName = "AdminRole" },
                new Role { Id = 2, Name = "Tenant", NormalizedName = "TenantRole" },
                new Role { Id = 3, Name = "Agent", NormalizedName = "AgentRole" }
            };
        }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}