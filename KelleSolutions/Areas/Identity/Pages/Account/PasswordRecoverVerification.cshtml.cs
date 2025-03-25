using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KelleSolutions.Areas.Identity.Pages.Account
{
    public class PasswordRecoverVerification : PageModel
    {
        private readonly UserManager<User> _userManager;

        public PasswordRecoverVerification(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Verification { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Retrieve the user's email from TempData
            var email = TempData["Email"] as string;
            var storedCode = TempData["Code"] as string;
            
            // Keep TempData values for the next request
            TempData.Keep("Email");
            TempData.Keep("Code");
            
            if (string.IsNullOrEmpty(email))
            {
                Console.WriteLine(email);
                Console.WriteLine(storedCode);
                ModelState.AddModelError(string.Empty, "Invalid request. Please try again.");
                return Page();
            }

            Console.WriteLine(email);
            Console.WriteLine(storedCode);

            // Find user by email
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || storedCode != Input.Verification || user.ResetCodeExpiry < DateTime.UtcNow)
            {
                ModelState.AddModelError(string.Empty, "Invalid or expired verification code. Please try again.");
                return Page();
            }

            TempData.Remove("Code");
            // Redirect to ResetPassword page with the user's email
            return RedirectToPage("./ResetPassword", new { email });
        }
    }
}
