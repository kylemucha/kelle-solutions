using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using KelleSolutions.Models;

public class ForgotPasswordModel : PageModel
{
    private readonly UserManager<User> _userManager;
    private readonly EmailService _emailService;
    private readonly ILogger<ForgotPasswordModel> _logger;

    public ForgotPasswordModel(UserManager<User> userManager, EmailService emailService, ILogger<ForgotPasswordModel> logger)
    {
        _userManager = userManager;
        _emailService = emailService;
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public string Email { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }        

        // Store the email in TempData for use in verification step
        TempData["Email"] = Input.Email;

        // Check if email is associated with an account
        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user == null)
        {
            // Prevent user enumeration attacks by always redirecting to the confirmation page
            return RedirectToPage("./PasswordRecoverVerification");
        }

        // Generate a 6-digit reset code
        string resetCode = GenerateResetCode();

        TempData["Code"] = resetCode;

        // Store the reset code securely (e.g., in the user database or cache)
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        user.PasswordResetToken = resetToken;  // Store the actual token
        int expiryTime = 10;
        user.ResetCodeExpiry = DateTime.UtcNow.AddMinutes(expiryTime);
        await _userManager.UpdateAsync(user);

        // Email content
        string subject = "Your Password Reset Code";
        string body = $"<p>Your password reset code is: <strong>{resetCode}</strong></p><p>This code will expire in {expiryTime} minutes.</p>";

        _logger.LogInformation("Code: {resetCode}", resetCode);

        // Send the email
        await _emailService.SendEmailAsync(Input.Email, subject, body);

        return RedirectToPage("./PasswordRecoverVerification", new { email = Input.Email });
    }

    private string GenerateResetCode()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString(); // Generates a 6-digit code
    }
}
