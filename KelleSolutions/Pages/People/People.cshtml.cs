using KelleSolutions.Pages.People;
using Microsoft.AspNetCore.Mvc.RazorPages;
  using System.Collections.Generic;

  namespace KelleSolutions.Pages.People
  {
      public class PeopleModel : PageModel
      {
          public List<UserPeople> UserPeople { get; set; }

          public void OnGet()
          {
              UserPeople = new List<UserPeople>
              {
                  new UserPeople 
                  { 
                      Id = 1,
                      UserName = "Randall Watts",
                      PeopleCount = 3
                  },
                  new UserPeople 
                  { 
                      Id = 2,
                      UserName = "Luis Gallarzo",
                      PeopleCount = 2
                  }
              };
          }
      }

      public class UserPeople
      {
          public int Id { get; set; }
          public string UserName { get; set; }
          public int PeopleCount { get; set; }
      }
  }
