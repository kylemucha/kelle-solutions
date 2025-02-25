using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Actions
{
    public class ViewActionsModel : PageModel
    {
        public List<ViewUserActions> Actions { get; set; } = new();

        public void OnGet()
        {
            // Mock data - replace with actual database retrieval
            Actions = new List<ViewUserActions>
            {
                new ViewUserActions { actionTitle = "Fix Bug", firstName = "John", lastName = "Doe", category = "develop", dueDate = new DateOnly(2024, 10, 15) },
                new ViewUserActions { actionTitle = "Review Code", firstName = "Jane", lastName = "Smith", category = "quality assure", dueDate = new DateOnly(2024, 10, 20) }
            };
        }
    }

    public class ViewUserActions
    {
        public string actionTitle { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string category { get; set; }
        public DateOnly dueDate { get; set; }
    }
}
