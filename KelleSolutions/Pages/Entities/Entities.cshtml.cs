  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.RazorPages;
  using Microsoft.EntityFrameworkCore;
  using KelleSolutions.Data;
  using KelleSolutions.Models;
  using System.Collections.Generic;
  using System.Threading.Tasks;

  namespace KelleSolutions.Pages.Entities
  {
      public class EntitiesModel : PageModel
      {
          private readonly KelleSolutionsDbContext _context;

          public EntitiesModel(KelleSolutionsDbContext context)
          {
              _context = context;
          }

          public List<Entity> Entities { get; set; }

          public async Task OnGetAsync()
          {
              Entities = await _context.Entities
                  .OrderBy(e => e.Id)
                  .ToListAsync();
          }

          public async Task<IActionResult> OnPostUpdateAsync(int id)
          {
              var entity = await _context.Entities.FindAsync(id);
              if (entity == null)
              {
                  return NotFound();
              }

              entity.Name = Request.Form["Name"];
              entity.Category = Request.Form["Category"];
              entity.Phone = Request.Form["Phone"];
              entity.Location = Request.Form["Location"];

              await _context.SaveChangesAsync();
              return RedirectToPage();
          }

          public async Task<IActionResult> OnPostDeleteAsync(int id)
          {
              var entity = await _context.Entities.FindAsync(id);
              if (entity != null)
              {
                  _context.Entities.Remove(entity);
                  await _context.SaveChangesAsync();

                  await using (var command = _context.Database.GetDbConnection().CreateCommand())
                  {
                      command.CommandText = "DBCC CHECKIDENT ('Entities', RESEED, 0);";
                      await _context.Database.OpenConnectionAsync();
                      await command.ExecuteNonQueryAsync();
                  }

                  var entities = await _context.Entities.OrderBy(e => e.Id).ToListAsync();

                  _context.Entities.RemoveRange(entities);
                  await _context.SaveChangesAsync();

                  foreach (var e in entities)
                  {
                      e.Id = 0;
                      _context.Entities.Add(e);
                  }
                  await _context.SaveChangesAsync();
              }
              return RedirectToPage();
          }
      }
  }