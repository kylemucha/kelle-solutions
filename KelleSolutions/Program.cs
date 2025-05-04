using KelleSolutions.Data;
using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ✅ Load all config files, including environment-specific ones like appsettings.Production.json
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// ✅ Register DbContext with SQL Server connection string
builder.Services.AddDbContext<KelleSolutionsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging()
           .EnableDetailedErrors());

// ✅ Register Identity with custom User class
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<KelleSolutionsDbContext>();

// ✅ Register Razor Pages
builder.Services.AddRazorPages();

// ✅ Register CORS (Allow All - adjust for prod if needed)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// ✅ Register scoped services
builder.Services.AddScoped<EmailService>();

var app = builder.Build();

// ✅ Run Migrations + Seed Logic Safely Inside Scope
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<KelleSolutionsDbContext>();
        await context.Database.MigrateAsync();

        await EnsureRolesAsync(services);
        //await ImportPropertyDataAsync(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during startup migration/seeding: {ex.Message}");
    }
}

// ✅ Middleware & Routing
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

try
{
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Startup Error: {ex.Message}");
    throw;
}


// 🔧 Helper Methods
async Task EnsureRolesAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new List<string> { "Admin", "Agent", "Broker" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
            Console.WriteLine($"Role '{role}' added.");
        }
        else
        {
            Console.WriteLine($"Role '{role}' already exists. Skipping...");
        }
    }
}

async Task ImportPropertyDataAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<KelleSolutionsDbContext>();

    try
    {
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "properties.json");
        Console.WriteLine($"🛠 Attempting to import properties from: {jsonPath}");

        if (File.Exists(jsonPath))
        {
            var jsonData = await File.ReadAllTextAsync(jsonPath);
            var properties = JsonSerializer.Deserialize<List<Property>>(jsonData);

            if (properties == null)
            {
                Console.WriteLine("🚨 Deserialized property list is null. Check JSON format.");
                return;
            }

            if (!context.Properties.Any())
            {
                context.Properties.AddRange(properties);
                await context.SaveChangesAsync();
                Console.WriteLine("✅ Property data imported successfully!");
            }
            else
            {
                Console.WriteLine("ℹ️ Properties already exist. Skipping import.");
            }
        }
        else
        {
            Console.WriteLine("❌ JSON file not found! No data to import!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("🔥 ERROR during property import:");
        Console.WriteLine($"Message: {ex.Message}");
        Console.WriteLine($"StackTrace: {ex.StackTrace}");
    }
}
