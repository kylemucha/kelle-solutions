using KelleSolutions.Data;
using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;                        // reads the JSON file
using System.Text.Json;                 // encrypt and decrypt JSON file data

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext with the SQL Server connection string
builder.Services.AddDbContext<KelleSolutionsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        // for future debugging
        .EnableSensitiveDataLogging() 
        .EnableDetailedErrors());

// Update Identity to use the custom User class (User instead of IdentityUser)
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<KelleSolutionsDbContext>();

// Registers Razor Pages for UI Rendering
builder.Services.AddRazorPages();

// Allow frontend JavaScript send requests
builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll",
            builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
    });

var app = builder.Build();

// Here, I can apply migrations and seed data from the JSON file
using (var scope = app.Services.CreateScope()) {
    var context = scope.ServiceProvider.GetRequiredService<KelleSolutionsDbContext>();
    // checks that the database is up to date with the latest migration!
    context.Database.Migrate();

    try {
        // defines the JSON file path inside wwwroot/data/
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "properties.json");
    
        // checks if the JSON file exists before it continues
        if (File.Exists(jsonPath)) {
            // reads the entire JSON file
            var jsonData = File.ReadAllText(jsonPath);
            //converting JSON into a list of Property objects
            var properties = JsonSerializer.Deserialize<List<Property>>(jsonData);

            // prevents duplicate data by checking if Properties table is empty
            if (!context.Properties.Any()) {
                // adds properties to the database
                context.Properties.AddRange(properties);
                // saving the changes made to the database
                context.SaveChanges();
                Console.WriteLine("Property data imported successfully!");
            }
            else {
                Console.WriteLine("Properties already exist! Skipping import...");
            }
        }
        else {
            Console.WriteLine("JSON file not found! No data to import!");
        }
    }
    catch (Exception ex) {
        // logs any errors found
        Console.WriteLine($"Error importing properties: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// redirects all HTTP requests to HTTPS
app.UseHttpsRedirection();

// allows serving static files from wwwroot
app.UseStaticFiles();

// enables endpoint routing for requests
app.UseRouting();

// prevents CORS request block
app.UseCors("AllowAll");

// enables login/logout
app.UseAuthentication();

// makes sure users can only access authorized pages
app.UseAuthorization();

// maps Razor Pages for UI rendering
app.MapRazorPages();

// runs the app!
app.Run();

// log every incoming request
app.Use(async (context, next) =>
{
    Console.WriteLine($" Incoming Request: {context.Request.Method} {context.Request.Path}");

    if (context.Request.ContentLength.HasValue)
    {
        context.Request.EnableBuffering();
        using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            Console.WriteLine($" Request Body: {body}");
        }
    }

    await next();
});
