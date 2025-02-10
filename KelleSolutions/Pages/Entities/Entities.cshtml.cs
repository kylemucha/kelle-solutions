  using Microsoft.AspNetCore.Mvc.RazorPages;
  using System.Collections.Generic;

  namespace KelleSolutions.Pages.Entities
  {
      public class EntitiesModel : PageModel
      {
          public List<UserEntities> UserEntities { get; set; }

          public void OnGet()
          {
              UserEntities = new List<UserEntities>
              {
                  new UserEntities 
                  { 
                      Id = 1,
                      UserName = "Randall Watts",
                      EntityCount = 3
                  },
                  new UserEntities 
                  { 
                      Id = 2,
                      UserName = "John Doe",
                      EntityCount = 2
                  }
              };
          }
      }

      public class UserEntities
      {
          public int Id { get; set; }
          public string UserName { get; set; }
          public int EntityCount { get; set; }
      }
  }
