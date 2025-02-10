using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System;

namespace KelleSolutions.Pages.Roles
{
    public class TransactionsModel : PageModel
    {
        // handles transaction list display and management
        public required List<TransactionViewModel> Transactions { get; set; }

        public void OnGet()
        {
            // todo: add database context, retrieve transaction records, etc.
            // for now, just initialize an empty list
            Transactions = new List<TransactionViewModel>();
        }
    }

    // data structure for displaying transaction information
    public class TransactionViewModel
    {
        public required string TransactionId { get; set; }
        public required string ListingId { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public required string Status { get; set; }
        public string Comments { get; set; }
    }
}