using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace KelleSolutions.Pages
{
    // This attribute ensures that no response is cached, important for error pages to reflect real-time issues.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    
    // This attribute prevents antiforgery validation for the error page, as the page doesn't involve user input or form submissions.
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        // Property to hold the unique Request ID associated with the current request.
        public string? RequestId { get; set; }

        // Boolean property that returns true if a Request ID exists, false otherwise.
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Logger used for recording error details.
        private readonly ILogger<ErrorModel> _logger;

        // Constructor to inject the ILogger service for logging purposes.
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        // This method is called on HTTP GET requests and sets the Request ID.
        public void OnGet()
        {
            // If an Activity ID exists (from diagnostics), use it; otherwise, use the HTTP context's Trace Identifier.
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
