using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KelleSolutions.Data;
using KelleSolutions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KelleSolutions.Pages.Roles
{
    public class TransactionsModel : PageModel
    {
        private readonly KelleSolutionsDbContext _context;
        public TransactionsModel(KelleSolutionsDbContext context)
        {
            _context = context;
        }

        public List<Transaction> Transactions { get; set; }

        public async Task OnGetAsync()
        {
            Transactions = await _context.Transactions
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync();
        }
          public async Task<IActionResult> OnPostDeleteAsync(int id)
          {
              var transaction = await _context.Transactions.FindAsync(id);
              if (transaction != null)
              {
                  _context.Transactions.Remove(transaction);
                  await _context.SaveChangesAsync();

                  await using (var command = _context.Database.GetDbConnection().CreateCommand())
                  {
                      command.CommandText = "DBCC CHECKIDENT ('Transactions', RESEED, 0);";
                      await _context.Database.OpenConnectionAsync();
                      await command.ExecuteNonQueryAsync();
                  }

                  var transactions = await _context.Transactions.OrderBy(t => t.Id).ToListAsync();

                  _context.Transactions.RemoveRange(transactions);
                  await _context.SaveChangesAsync();

                  foreach (var t in transactions)
                  {
                      t.Id = 0;
                      _context.Transactions.Add(t);
                  }
                  await _context.SaveChangesAsync();
              }
              return RedirectToPage();
        }
    }
}