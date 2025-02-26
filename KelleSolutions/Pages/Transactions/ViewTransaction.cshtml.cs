using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Roles
{
        public class ViewTransactionModel : PageModel
        {
            private readonly KelleSolutionsDbContext _context;

            public ViewTransactionModel(KelleSolutionsDbContext context)
            {
                _context = context;
            }

            [BindProperty]
            public Transaction Transaction { get; set; }

            public async Task<IActionResult> OnGetAsync(int? id)
            {
                if (id.HasValue)
                {
                    Transaction = await _context.Transactions.FindAsync(id.Value);
                    if (Transaction == null)
                    {
                        return NotFound();
                    }
                    return Page();
                }

                Transaction = new Transaction();
                return Page();
            }

            public async Task<IActionResult> OnPostAsync()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                if (Transaction.Id == 0)
                {
                    Transaction.CreatedDate = DateTime.Now;
                    _context.Transactions.Add(Transaction);
                }
                else
                {
                    _context.Transactions.Update(Transaction);
                }

                await _context.SaveChangesAsync();
                return RedirectToPage("./Transactions");
        }
    }
}