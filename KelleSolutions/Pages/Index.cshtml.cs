using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using KelleSolutions.Models;
using System.Threading.Tasks;

namespace KelleSolutions.Pages
{
    // The model class associated with the Index page, inheriting from Razor's PageModel.
    public class IndexModel : PageModel
    {
        // Private field to store the logger instance for logging purposes.
        private readonly ILogger<IndexModel> _logger;

        // Constructor to inject the ILogger service.
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // Method that handles GET requests to the Index page. Currently, it doesn't perform any actions.
        public void OnGet()
        {

        }
    }
}