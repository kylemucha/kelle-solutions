// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KelleSolutions.Areas.Identity.Pages.Account
{
    public class PasswordRecoverVerification : PageModel
    {
        // Might not be needed
        private readonly UserManager<User> _userManager;

        public PasswordRecoverVerification(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            public string Verification { get; set; }
        }

        public IActionResult OnPost()
        //public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var code = TempData["PasswordResetCode"] as string;
                var verifyCode = Input.Verification;
                
                // Used to test if codes are equal
                //code = "111111";

                // Check if the entered verification code (verifyCode) is equal to the generated verification code (code)
                // If not equal, display error
                // If equal, redirect to ResetPassword page
                if (verifyCode != code && verifyCode != null)
                {
                    ModelState.AddModelError(String.Empty, "Invalid Verification Code");
                    return Page();
                }
                else
                {
                    return RedirectToPage("./ResetPassword");
                }
            }
            
            return Page();
        }
    }
}
