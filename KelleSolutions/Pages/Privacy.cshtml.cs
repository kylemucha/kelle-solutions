using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KelleSolutions.Pages
{
    // Model class for the Privacy page, inheriting from Razor's PageModel.
    public class PrivacyModel : PageModel
    {
        // Private logger instance for logging any information related to the privacy page.
        private readonly ILogger<PrivacyModel> _logger;

        // Constructor that injects an ILogger service to log information.
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        // This method handles GET requests to the Privacy page. 
        // Currently, it does not perform any actions but can be extended in the future.
        public void OnGet()
        {
        }
    }
}
