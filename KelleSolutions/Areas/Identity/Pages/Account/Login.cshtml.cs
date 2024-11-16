// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KelleSolutions.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace KelleSolutions.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {

            [Required(ErrorMessage = "Email or Phone Number is required.")]
            public string EmailOrPhone { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // Check if the input is an email or phone number
                var isEmail = new EmailAddressAttribute().IsValid(Input.EmailOrPhone);
                User user = null;

                if (isEmail)
                {
                    // If it's a valid email, try to find the user by email
                    user = await _userManager.FindByEmailAsync(Input.EmailOrPhone);
                }
                else
                {
                    // Otherwise, check if it's a phone number and find user by phone number
                    user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == Input.EmailOrPhone);
                }

                if (user != null)
                {
                    // Attempt to sign in the user
                    var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }

                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }

                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                }
                else
                {
                    // No user found by email or phone
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got here, something failed, redisplay the form
            return Page();
        }
    }

    // Custom validation to allow for both email or phone number
    public class EmailOrPhoneNumberAttribute : ValidationAttribute
    {
        public EmailOrPhoneNumberAttribute() : base("Invalid email or mobile number.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string input = value as string;

            if (input != null)
            {
                // Email pattern
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                // Phone number pattern (simple international format for mobile numbers)
                var phonePattern = @"^\+?[1-9]\d{1,14}$";  // Matches international phone numbers

                if (Regex.IsMatch(input, emailPattern) || Regex.IsMatch(input, phonePattern))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Please enter a valid email or mobile number.");
        }
    }
}
