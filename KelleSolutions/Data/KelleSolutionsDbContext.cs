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

            if (!builder.Model.GetEntityTypes().Any(t => t.ClrType == typeof(IdentityRole))) {
                var admin = new IdentityRole("Admin");
                admin.NormalizedName = "Admin";

                var broker = new IdentityRole("Broker");
                broker.NormalizedName = "Broker";

                var agent = new IdentityRole("Agent");
                agent.NormalizedName = "Agent";

                builder.Entity<IdentityRole>().HasData(admin, broker, agent);
            }

            // Defines Relationship: Listing -> Property (one Property can have many Listings)
            builder.Entity<Listing>()
                // each listing belongs to one property
                .HasOne(l => l.Property)
                // one property can have multiple listings (in its lifetime)
                .WithMany(p => p.Listings)
                // PropertyID is the foreign key in Listing.cs
                .HasForeignKey(l => l.PropertyID)
                // this just ensures that deleting a Property also delets its listings
                .OnDelete(DeleteBehavior.Cascade);

            // Defines Relationship: Listing -> User (one User can have many listings)
            builder.Entity<Listing>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(l => l.UserID)
                .OnDelete(DeleteBehavior.NoAction);

            // Explicity set decimal place for Price to prevent warnings
            builder.Entity<Listing>()
                .Property(l => l.Price)
                .HasColumnType("decimal(18,2)");

            // Correct column typing for Status and Team
            builder.Entity<Listing>()
                .Property(l => l.Status)
                .HasColumnType("nvarchar(50)");

            builder.Entity<Listing>()
                .Property(l => l.Team)
                .HasColumnType("nvarchar(50)");
        }

        // DbSet for Properties
        public DbSet<Property> Properties { get; set; }

        // DbSet for Listings
        public DbSet<Listing> Listings { get; set; }

        // DbSet for Affiliates
        public DbSet<Affiliate> Affiliates { get; set; }

        // DbSet for Entities
        public DbSet<Entity> Entities { get; set; }

        // DbSet for Leads
        public DbSet<Lead> Leads { get; set; }
        
    }
}
