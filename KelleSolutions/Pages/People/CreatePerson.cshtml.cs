  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.RazorPages;
  using Microsoft.AspNetCore.Identity;
  using KelleSolutions.Models;
  using System;

  namespace KelleSolutions.Pages.People
  {
      public class CreatePersonModel : PageModel
      {
          private readonly UserManager<User> _userManager;

          public CreatePersonModel(UserManager<User> userManager)
          {
              _userManager = userManager;

              // initialize empty person
              Person = new PersonViewModel
              {
                  FirstName = string.Empty,
                  LastName = string.Empty,
                  Email = string.Empty,
                  PhoneNumber = string.Empty,
                  Address = string.Empty,
                  City = string.Empty,
                  State = string.Empty,
                  ZipCode = string.Empty,
                  Notes = string.Empty
              };
          }

          [BindProperty]
          public PersonViewModel Person { get; set; }

          public IActionResult OnGet()
          {
              return Page();
          }

          public async Task<IActionResult> OnPostAsync()
          {
              if (!ModelState.IsValid)
              {
                  return Page();
              }

              try
              {
                  var currentUser = await _userManager.GetUserAsync(User);
                
                  // redirect as if it was saved successfully, data model functionality needed
                                
                  return RedirectToPage("/People/People");
              }
              catch (Exception ex)
              {
                  ModelState.AddModelError("", "An error occurred. Please try again.");
                  Console.WriteLine($"Error: {ex.Message}");
                  return Page();
              }
          }
      }

      public class PersonViewModel
      {
          public string FirstName { get; set; }
          public string LastName { get; set; }
          public string Email { get; set; }
          public string PhoneNumber { get; set; }
          public string Address { get; set; }
          public string City { get; set; }
          public string State { get; set; }
          public string ZipCode { get; set; }
          public string Notes { get; set; }
      }
  }
