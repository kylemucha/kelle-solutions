using KelleSolutions.Models;
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

        // DbSet for Properties
        public DbSet<Property> Properties { get; set; }

        // DbSet for Affiliates (Add this line)
        public DbSet<Affiliate> Affiliates { get; set; }

        // DbSet for Entities
        public DbSet<Entity> Entities { get; set; }
    }
}
