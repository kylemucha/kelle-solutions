using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace KelleSolutions.Areas.Identity.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ResetPasswordModel> _logger;

        public ResetPasswordModel(UserManager<User> userManager, ILogger<ResetPasswordModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var email = TempData["Email"] as string;

            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError(string.Empty, "Invalid request. Please try again.");
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return Page();
            }

            var resetToken = user.PasswordResetToken;
            if (string.IsNullOrEmpty(resetToken))
            {
                ModelState.AddModelError(string.Empty, "Invalid reset token.");
                return Page();
            }

            if (user.ResetCodeExpiry < DateTime.UtcNow)
            {
                ModelState.AddModelError(string.Empty, "Reset token has expired.");
                return Page();
            }

            var result = await _userManager.ResetPasswordAsync(user, resetToken, Input.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            user.PasswordResetToken = null;
            user.ResetCodeExpiry = null;
            await _userManager.UpdateAsync(user);

            return RedirectToPage("../Login/Login");
        }
    }
}
