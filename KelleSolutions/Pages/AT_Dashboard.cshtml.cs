using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace KelleSolutions.Pages
{
    [Authorize] // Restrict access to logged-in users only
    public class AT_DashboardModel : PageModel
    {
        public string Message { get; private set; } = "Welcome to the Dashboard!";

        public void OnGet()
        {
            // Any logic that needs to run when the page loads can go here.
            // Example: Load user-specific data or initialize components.
            Message = "This is your personalized dashboard. Here you will find your recent updates and information.";
        }
    }
}
