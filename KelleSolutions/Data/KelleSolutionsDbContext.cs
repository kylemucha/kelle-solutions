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
