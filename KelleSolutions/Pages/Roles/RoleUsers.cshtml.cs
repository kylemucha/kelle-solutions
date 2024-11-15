using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KelleSolutions.Pages.Roles
{
    public class RoleUsersModel : PageModel
    {
        public string RoleName { get; set; }

        public void OnGet(string roleName)
        {
            // Store the roleName from the query string for later use
            RoleName = roleName;

            // Can use RoleName to fetch users in the future
        }
    }
}