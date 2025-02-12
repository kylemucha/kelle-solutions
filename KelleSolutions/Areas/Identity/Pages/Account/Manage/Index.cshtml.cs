// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KelleSolutions.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public string UserRole { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Required(ErrorMessage = "First Name is required.")]
            [RegularExpression(@"^[A-Za-z\s\-]+$", ErrorMessage = "First Name can only contain letters, spaces, and hyphens.")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Last Name is required.")]
            [RegularExpression(@"^[A-Za-z\s\-]+$", ErrorMessage = "Last Name can only contain letters, spaces, and hyphens.")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Affiliation is required.")]
            [Display(Name = "Affiliation")]
            public string Affiliation { get; set; }

            [Required(ErrorMessage = "Enter a valid 10-digit phone number (e.g., 1234567890).")]
            [RegularExpression(@"^\d{10}$")]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "Agent License Number is required.")]
            [RegularExpression(@"^\d{8}$", ErrorMessage = "License Number must be an 8-digit number.")]
            [Display(Name = "Agent License Number")]
            public string LicenseNumber { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var firstName = user.FirstName;
            var lastName = user.LastName;
            var affiliation = user.Affiliation;
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var licenseNumber = user.LicenseNumber;

            // Fetch User role
            var roles = await _userManager.GetRolesAsync(user);
            UserRole = roles.Count > 0 ? roles[0] : "No Role Assigned";

            Username = user.UserName;
            Input = new InputModel
            {
                FirstName = firstName,
                LastName = lastName,
                Affiliation = affiliation,
                PhoneNumber = phoneNumber,
                LicenseNumber = licenseNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            // Update First Name, Last Name, Affiliation, and License Number fields directly if changed
            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
            }

            if (Input.Affiliation != user.Affiliation)
            {
                user.Affiliation = Input.Affiliation;
            }

            if (Input.LicenseNumber != user.LicenseNumber)
            {
                user.LicenseNumber = Input.LicenseNumber;
            }

            // Update Phone Number if it has changed
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            // Attempt to update the user information in the database
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update profile information.";
                return RedirectToPage();
            }

            // Refresh page to apply changes
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
