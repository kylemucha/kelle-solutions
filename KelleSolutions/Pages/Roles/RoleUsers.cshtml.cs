using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace KelleSolutions.Pages.Roles
{
    public class RoleUsersModel : PageModel
    {
        // Displays the role name (e.g., "Admin") at the top of the page
        public string RoleName { get; set; }

        // Represents each row in the table
        public class RoleUser
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Affiliate { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string LicenseNumber { get; set; }

            // Indicates whether this row is currently in "edit mode"
            public bool IsEditing { get; set; }
        }

        // Will hold the list of user rows for this role
        [BindProperty]
        public List<RoleUser> RoleUsers { get; set; } = new List<RoleUser>();

        public void OnGet(string roleName)
        {
            // Store the roleName from the query string
            RoleName = roleName;

            // In a real scenario, you'd fetch existing users from a database here.
            // For this example, we’ll pre-populate a row or two so that something appears on first load.
            if (RoleUsers.Count == 0)
            {
                RoleUsers.Add(new RoleUser
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Affiliate = "ABC",
                    Email = "john.doe@example.com",
                    PhoneNumber = "555-1234",
                    LicenseNumber = "XYZ123",
                    IsEditing = false
                });
                RoleUsers.Add(new RoleUser
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Affiliate = "XYZ",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "555-5678",
                    LicenseNumber = "ABC987",
                    IsEditing = false
                });
            }
        }

        // Handler method to create a new blank row in "edit mode"
        public IActionResult OnPostAddNew()
        {
            // Add a new empty row at the bottom in edit mode
            RoleUsers.Add(new RoleUser
            {
                IsEditing = true
            });

            // Re-display the same page
            return Page();
        }

        // Handler method to toggle a specific row's edit mode.
        // The "rowIndex" parameter tells us which row button was clicked.
        public IActionResult OnPostToggleEdit(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < RoleUsers.Count)
            {
                // If the row is in edit mode, we assume the user wants to "Enter" (save),
                // so we set IsEditing = false. Otherwise, we allow editing.
                RoleUsers[rowIndex].IsEditing = !RoleUsers[rowIndex].IsEditing;
            }

            return Page();
        }
    }
}
