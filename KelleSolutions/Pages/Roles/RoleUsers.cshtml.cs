using KelleSolutions.Data;
using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KelleSolutions.Pages.Roles
{
    public class RoleUsersModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;
        private readonly UserManager<User> _userManager;  // Inject UserManager

        
        // Displays the role name (e.g., "Admin") at the top of the page
        public string RoleName { get; set; }

        // Will hold the list of user rows for this role
        public IList<User> Users { get; set; }

        [BindProperty]
        public User NewUser { get; set; }

        public RoleUsersModel(KelleSolutionsDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task OnGetAsync(string roleName)
        {
            /*
            Users = await _context.Users.ToListAsync();
            NewUser = new User();  // Ensure NewUser is initialized
            */
            // Ensure that roleName is provided
            if (!string.IsNullOrEmpty(roleName))
            {
                // Fetch the role
                var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

                // Fetch the users belonging to this role
                if (role != null)
                {
                    // Get all users in the role
                    Users = await _userManager.GetUsersInRoleAsync(roleName);
                }
                else
                {
                    Users = new List<User>();  // No users to display if the role doesn't exist
                }

                NewUser = new User(); // Ensure NewUser is initialized for the Create form
                RoleName = roleName;  // Set the RoleName property to display in the view
            }
        }

/*
        public async Task OnGet(string roleName)
        {
            RoleName = roleName;

            // Check if roleName is provided
            if (!string.IsNullOrEmpty(roleName))
            {
                // Get role from RoleManager to ensure it exists
                var role = await _context.Roles
                    .FirstOrDefaultAsync(r => r.Name == roleName);

                if (role != null)
                {
                    // Get the user IDs for the specified role
                    var userIds = await _context.UserRoles
                        .Where(ur => ur.RoleId == role.Id)
                        .Select(ur => ur.UserId.ToString())
                        .ToListAsync();

                    // Now fetch users by comparing their IDs
                    Users = await _context.Users
                        .Where(u => userIds.Contains(u.Id.ToString()))
                        .ToListAsync();
                }
                else
                {
                    // If the role doesn't exist, return an empty list or handle the error accordingly
                    Users = new List<User>();
                }
            }
            else
            {
                // If no roleName is provided, show all users
                Users = await _context.Users.ToListAsync();
            }

            NewUser = new User();  // Initialize NewUser for form binding
        }
        */

        // Handler for creating a new user
        public async Task<IActionResult> OnPostCreateAsync()
        {
            /*
            // Automatically generate a new GUID for the user
            NewUser.Id = Guid.NewGuid();

            // Add the new user to the database
            _context.Users.Add(NewUser);
            await _context.SaveChangesAsync();

            return RedirectToPage();  // Redirect to refresh the page after creating
*/

            if (ModelState.IsValid)
            {
                
                // Ensure the Id is unique
                var existingUser = await _context.Users.FindAsync(NewUser.Id);
                if (existingUser != null)
                {
                    ModelState.AddModelError("NewUser.Id", "The ID already exists.");
                    return Page(); // Return to the form with an error
                }

                // Set CreatedDate to current time if not set already
                NewUser.DateCreated = NewUser.DateCreated == default ? DateTime.Now : NewUser.DateCreated;

                _context.Users.Add(NewUser);
                await _context.SaveChangesAsync();
                return RedirectToPage(); // Reload the page to show the updated list
                
                // Add the new user
/*
                // Ensure that NewUser has the correct values from the form
                if (string.IsNullOrEmpty(NewUser.Email))
                {
                    ModelState.AddModelError("NewUser.Email", "Email is required.");
                    return Page();  // Return the page with validation error
                }
                */
/*
                _context.Users.Add(NewUser);
                await _context.SaveChangesAsync();

                // Reset NewUser to clear the modal form
                NewUser = new User();  // Reset the NewUser object for the next user

                // Optionally, you can also reload the users list
                return RedirectToPage();  // Reload the page to show the updated list of user
            */
            }
            return Page();
        }

        // Handler for editing (updating) an existing user
        public async Task<IActionResult> OnPostEditAsync(int userId)
        {
            NewUser = await _context.Users.FindAsync(userId);
            if(NewUser == null)
            {
                return NotFound();
            }
            return Page();
        }

        // OnPostUpdate to save changes when editing an affiliate
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _context.Users.FindAsync(NewUser.Id);
            if (user == null)
            {
                return NotFound();
            }

            // Update fields
            user.FirstName = NewUser.FirstName;
            user.LastName = NewUser.LastName;
            user.Affiliation = NewUser.Affiliation;
            user.Email = NewUser.Email;
            user.PhoneNumber = NewUser.PhoneNumber;
            user.LicenseNumber = NewUser.LicenseNumber;

            await _context.SaveChangesAsync();

            return RedirectToPage(); // Reload the page to show updates
        }
        


        
        
/*
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
        */
    }
}
