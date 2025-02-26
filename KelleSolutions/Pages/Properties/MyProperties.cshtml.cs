using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;            // handles user authentication!
using System.Collections.Generic;               // defines database relationships
using System.Linq;                              // defines LINQ queries (ex: "Where", "OrderBy", "Skip", "Take", etc.)
using System;                                   
using System.IO;                                // file and stream execution
using System.Text.Json;                         // JSON serial/deserial
using System.Threading.Tasks;                   // supports non-blocking queries (ex: "async", "wait")
using System.Reflection;                        // required for extracting Display Names
using System.ComponentModel.DataAnnotations;    // required for Display attribute
using KelleSolutions.Data;                      // imports KelleSolutionsDbContext.cs
using KelleSolutions.Models;                    // imports model classes, like "Property.cs" as Property and "User.cs" as User

namespace KelleSolutions.Pages.Properties {
    public class MyPropertiesModel : PageModel {

        // database context for querying properties
        private readonly KelleSolutionsDbContext _context;
        
        // manages logged-in users
        private readonly UserManager<User> _userManager;
        
        // constructor that injects database context and user manager
        public MyPropertiesModel(KelleSolutionsDbContext context, UserManager<User> userManager) {
            _context = context;
            _userManager = userManager;
        }

        // storing the list of properties owned by the logged-in user
        public List<Property> MyProperties { get; set; } = new();

        // list of available property types
        public List<KeyValuePair<string, string>> AvailablePropertyTypesList { get; set; } = new();


        // page display properties
        // defaults to 10 properties per page
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        // the current page number
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        // the total number of pages
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync() {
            
            // gets the currently logged-in user
            var currentUser = await _userManager.GetUserAsync(User);

            // if no user is logged in, redirect to the login page
            // while in testing phase, keep this commented!
            if (currentUser == null) {
                return RedirectToPage("/Account/Login");
            }

            // ensure UserID is valid before querying the database
            if (string.IsNullOrEmpty(currentUser.Id)) {
                return RedirectToPage("/Account/Login");
            }

            // query only the properties submitted by the logged-in user
            var query = _context.Properties
                //  filters properties based on UserID
                .Where(p => p.UserID == currentUser.Id);

            // get the total property count for page display properties
            int totalProperties = await query.CountAsync();
            TotalPages = (int)System.Math.Ceiling((double)totalProperties / PageSize);

            // fetch only the properties on the current page!
            MyProperties = await query
                // skips items from previous pages
                .Skip((PageNumber - 1) * PageSize)
                // limits results to just PageSize
                .Take(PageSize)
                .ToListAsync();

            // fetches available property types, dynamically!
            AvailablePropertyTypesList = Enum.GetValues(typeof(Property.PropertyTypes))
                .Cast<Property.PropertyTypes>()
                .Select(pt => new KeyValuePair<string, string>(
                    pt.ToString(),
                    pt.GetType().GetMember(pt.ToString())?
                        .FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? pt.ToString()
                ))
                .ToList();

            return Page();
        }
        /*
        // required!
        [IgnoreAntiforgeryToken]
        [HttpPost("/Properties/MyProperties")]
        [NonAction]
        public async Task<IActionResult> OnPostAsync() {
            Console.WriteLine("OnPostSync Triggered!");
            try {
                // read request body as a JSON string
                using (var reader = new StreamReader(Request.Body))
                {
                    var body = await reader.ReadToEndAsync();
                    Console.WriteLine("Raw Request Body: " + body);

                    if (string.IsNullOrWhiteSpace(body)) {
                        Console.WriteLine("Reuqest body is empty?");
                        return new JsonResult(new { success = false, message = "Request body is empty" }) { StatusCode = 400 };
                    }

                    Property? newProperty;

                    // Deserialize JSON into a Property object
                    try {
                        newProperty = JsonSerializer.Deserialize<Property>(body);
                        if (newProperty == null) {
                            Console.WriteLine("JSON deserialization failed!");
                            return new JsonResult(new { success = false, message = "Invalid property data" }) { StatusCode = 400 };
                        }
                    }
                    catch (Exception jsonEx) {
                        Console.WriteLine("JSON Deserialization Exception: " + jsonEx.Message);
                        return new JsonResult(new { success = false, message = "Invalid JSON format" }) { StatusCode = 400 };
                    }
                    
                    Console.WriteLine("Deserialized property success!");

                    // Retrieve the current logged-in user
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser == null || string.IsNullOrEmpty(currentUser.Id)) {
                        Console.WriteLine("User auth failed!");
                        return new JsonResult(new { success = false, message = "User not authenticated" }) { StatusCode = 401 };
                    }

                    // Assign the user ID to the new property before saving
                    newProperty.UserID = currentUser.Id;

                    // aasign the date created
                    newProperty.CreatedAt = DateTime.UtcNow;

                    // validate the required fields
                    var validationResults = new List<ValidationResult>();
                    var validationContext = new ValidationContext(newProperty);
                    if (!Validator.TryValidateObject(newProperty, validationContext, validationResults, true)) {
                        foreach (var validationError in validationResults) {
                            Console.WriteLine($"Validation Error: {validationError.ErrorMessage}");
                        }
                        return new JsonResult(new { success = false, message = "Validation failed", errors = validationResults }) { StatusCode = 400 };
                    }

                    _context.Properties.Add(newProperty);
                    await _context.SaveChangesAsync();

                    // Return success response
                    Console.WriteLine("Property successfully saved!");
                    return new JsonResult(new { success = true, message = "Property added successfully" });
                }
            }
            catch (Exception ex) {
                // Return error response with exception details
                Console.WriteLine("Server Exception: " + ex.Message);
                return new JsonResult(new { success = false, message = "Server error: " + ex.Message }) { StatusCode = 500 };
            }
        }*/
    }
    
}
