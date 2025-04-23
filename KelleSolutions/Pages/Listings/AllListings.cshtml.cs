using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using KelleSolutions.Data;
using KelleSolutions.Models;
using KelleSolutions.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.Json;
using System.Linq;
using System.IO;
using System;

namespace KelleSolutions.Pages.Listings {
    public class AllListingsModel : PageModel {
        private readonly KelleSolutionsDbContext _context;

        public AllListingsModel(KelleSolutionsDbContext context) {
            _context = context;
        }

        public List<ViewUserListings> AllListings { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPages { get; set; }

        private static string GetStatusDisplayName(MyStatusEnum status)
        {
            var memberInfo = status.GetType().GetMember(status.ToString()).FirstOrDefault();
            var displayAttribute = memberInfo?.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? status.ToString();
        }

        public async Task<IActionResult> OnGetAsync() {
            // Retrieve all listings including related Property details via PropertyDetails
            var query = _context.Listings
                .Include(l => l.PropertyDetails)
                .Select(l => new ViewUserListings {
                    ListingID = l.Code,
                    Date = l.Created ?? DateTime.MinValue,
                    Status = GetStatusDisplayName(l.MyStatus),
                    OperatorName = l.PropertyDetails.Operator.ToString(),
                    Affiliation = "N/A",
                    Price = l.Price ?? 0,
                    Address = l.PropertyDetails.Street
                })
                .AsQueryable();

            int totalListings = await query.CountAsync();
            TotalPages = (int)Math.Ceiling((double)totalListings / PageSize);
            TotalPages = (int)Math.Ceiling((double)totalListings / PageSize);

            AllListings = await query
                .OrderByDescending(l => l.Date)
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return Page();
        }

        public async Task<JsonResult> OnPostUpdateStatusAsync([FromBody] UpdateStatusModel request)
        {
            // 1. Validate request
            if (request == null || request.Id <= 0)
            {
                return new JsonResult(new { success = false, message = "Invalid request data" });
            }
        
            try
            {
                // 2. Find the listing (including related data if needed)
                var listing = await _context.Listings
                    .FirstOrDefaultAsync(l => l.Code == request.Id);
        
                if (listing == null)
                {
                    return new JsonResult(new { 
                        success = false, 
                        message = $"Listing {request.Id} not found" 
                    });
                }
        
                // 3. Log current values for debugging
                Console.WriteLine($"Current status: {listing.MyStatus}");
                Console.WriteLine($"Attempting to update to: {request.Status}");
        
                // 4. Handle status conversion (with space removal)
                var statusValue = request.Status?.Replace(" ", "") ?? "";
                if (!Enum.TryParse(statusValue, true, out MyStatusEnum newStatus))
                {
                    var validValues = string.Join(", ", Enum.GetNames(typeof(MyStatusEnum)));
                    return new JsonResult(new { 
                        success = false, 
                        message = $"Invalid status '{request.Status}'. Valid values: {validValues}" 
                    });
                }
        
                // 5. Update and save
                listing.MyStatus = newStatus;
                listing.Updated = DateTime.Now;
        
                await _context.SaveChangesAsync();
        
                return new JsonResult(new { 
                    success = true,
                    newStatus = request.Status // Return the formatted status
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating status: {ex}");
                return new JsonResult(new { 
                    success = false, 
                    message = $"Database error: {ex.Message}" 
                });
            }
        }

        public class ViewUserListings {
            public int ListingID { get; set; }
            public DateTime Date { get; set; }
            public string Status { get; set; }
            public string OperatorName { get; set; }
            public string Affiliation { get; set; }
            public decimal Price { get; set; }
            public string Address { get; set; }
        }
    }
}

