using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Create the role if it doesn't exist
        var roleExist = await roleManager.RoleExistsAsync("Admin");
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Create the admin user if it doesn't exist
        var user = await userManager.FindByEmailAsync("admin@example.com");
        if (user == null)
        {
            user = new User
            {
                FirstName = "admin@example.com",
                LastName = "admin@example.com",
                Affiliation = "Admin",
                LicenseNumber = "00000000"
            };
            await userManager.CreateAsync(user, "Admin1!"); // Choose a strong password
        }

        // Add the user to the Admin role
        if (!await userManager.IsInRoleAsync(user, "Admin"))
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
