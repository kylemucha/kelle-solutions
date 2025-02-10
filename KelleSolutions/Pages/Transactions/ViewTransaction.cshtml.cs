using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;

namespace KelleSolutions.Pages.Roles
{
    public class ViewTransactionModel : PageModel
    {
        // Contingency Dates
        [BindProperty]
        [Display(Name = "Deposit Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DepositDate { get; set; }

        [BindProperty]
        [Display(Name = "Inspection Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? InspectionDate { get; set; }

        [BindProperty]
        [Display(Name = "Loan Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? LoanDate { get; set; }

        [BindProperty]
        [Display(Name = "Appraisal Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? AppraisalDate { get; set; }

        // Property Information
        [BindProperty]
        [Display(Name = "Property Information")]
        public string PropertyInformation { get; set; }

        // Transition Details
        [BindProperty]
        [Display(Name = "Transition Details")]
        public string TransitionDetails { get; set; }

        // Status Details
        [BindProperty]
        [Display(Name = "Status")]
        public string Status { get; set; }

        // Comments
        [BindProperty]
        [Display(Name = "Comments")]
        public string Comments { get; set; }
        public void OnGet()
        {
            // If you are editing an existing transaction, load data here
        }

        public IActionResult OnPost()
        {
            // Handle form submission: create/update your database record
            // Then redirect back to the Transactions list
            return RedirectToPage("Transactions");
        }
    }
}
