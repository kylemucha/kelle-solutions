using KelleSolutions.Data;
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
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<KelleSolutionsDbContext>();

            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if roles exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Broker"))
            {
                await roleManager.CreateAsync(new IdentityRole("Broker"));
            }

            if (!await roleManager.RoleExistsAsync("Agent"))
            {
                await roleManager.CreateAsync(new IdentityRole("Agent"));
            }

            // Check if the tenant exists
            var tenant = context.Tenants.FirstOrDefault(t => t.TenantCode == "ABC");
            if (tenant == null)
            {
                tenant = new Tenant
                {
                    TenantCode = "ABC",
                    Website = "https://abc-realestate.com",
                    PhoneNumber = "1234567890",
                    LicenseOperator = 1001
                };
                context.Tenants.Add(tenant);
                await context.SaveChangesAsync();  // Ensure the Tenant gets an ID
            }

            // Check if the admin user exists
            var user = await userManager.FindByEmailAsync("admin@scrumbags.com");
            if (user == null)
            {
                user = new User
                {
                    UserName = "admin@scrumbags.com",
                    Email = "admin@scrumbags.com",
                    FirstName = "Admin",
                    LastName = "User",
                    Affiliation = "Admin",
                    LicenseNumber = "00000000",
                    TenantID = tenant.TenantID,
                    IsTenant = true
                };

                var result = await userManager.CreateAsync(user, "Admin1!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
