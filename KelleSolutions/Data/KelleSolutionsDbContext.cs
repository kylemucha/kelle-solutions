using KelleSolutions.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace KelleSolutions.Data
{
    public class KelleSolutionsDbContext : IdentityDbContext<User>
    {
        public KelleSolutionsDbContext(DbContextOptions<KelleSolutionsDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // One Tenant can have Many Users
            builder.Entity<User>()
                .HasOne(u => u.Tenant)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TenantID)
                .OnDelete(DeleteBehavior.Restrict); // Prevents accidental deletions

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Broker", NormalizedName = "BROKER" },
                new IdentityRole { Name = "Agent", NormalizedName = "AGENT" }); 
        }

        // DbSet for Tenants
        public DbSet<Tenant> Tenants { get; set;}

        // DbSet for Properties
        public DbSet<Property> Properties { get; set; }

        // DbSet for Affiliates (Add this line)
        public DbSet<Affiliate> Affiliates { get; set; }

        // DbSet for Entities
        public DbSet<Entity> Entities { get; set; }

        // DbSet for Leads
        public DbSet<Lead> Leads { get; set; }

        // DbSet for Listings
        public DbSet<Listing> Listings { get; set; }

        // DbSet for Transactions
        public DbSet<Transaction> Transactions { get; set; }

    }
}
