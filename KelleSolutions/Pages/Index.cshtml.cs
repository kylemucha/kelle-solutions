using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KelleSolutions.Pages;

public class IndexModel : PageModel {
    public IActionResult OnGet() {
        // ASP.NET looks for /Pages/Index.cshtml by default when the app starts.
        // This page exists solely to redirect visitors from "/" to the intended splash page.
        // The root route ("/") cannot be changed through configuration in Razor Pages,
        // so we handle redirection manually here.
        return RedirectToPage("/Home/Home");
    }
}
